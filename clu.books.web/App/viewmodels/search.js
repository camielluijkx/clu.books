define(['plugins/http', 'durandal/app', 'knockout'], function (http, app, ko) {
    //Note: This module exports an object.
    //That means that every module that "requires" it will get the same object instance.
    //If you wish to be able to create multiple instances, instead export a function.
    //See the "welcome" module for an example of function export.

    var books = ko.observableArray([]);

    var searchTerm = ko.observable('');

    var search = function () {

        var searchTerm = this.searchTerm();

        if (!searchTerm) {
            return;
        }

        var self = this;

        return http.get('http://localhost/clu.books.web.api/Search/Anything/' + searchTerm, { maxResults: 40 }, {}).then(
            function (response) {
                var books = response.books;
                books.forEach(function (book) {
                    book.information = ko.computed(function () {
                        return book.index + ') ' + book.title + ' - ' + book.author + ' - ' + book.publishedDate + ' (' + book.languageCode + ')';
                    });
                });
                self.books(response.books);
            },
            function (error) {
                alert(error);
            });
    }

    return {
        displayName: 'Search',
        searchTerm: searchTerm,
        search: search,
        books: books,
        activate: function () {

            //the router's activator calls this function and waits for it to complete before proceeding
            if (this.books().length > 0) {
                return;
            }

            this.search();
        },
        select: function (item) {
            //the app model allows easy display of modal dialogs by passing a view model
            //views are usually located by convention, but you an specify it as well with viewUrl
            item.viewUrl = 'views/detail';
            app.showDialog(item);
        },
        canDeactivate: function () {
            //the router's activator calls this function to see if it can leave the screen
            return app.showMessage('Are you sure you want to leave this page?', 'Navigate', ['Yes', 'No']);
        }
    };
});