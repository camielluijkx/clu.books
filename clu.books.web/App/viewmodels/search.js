define(['plugins/http', 'durandal/app', 'knockout'], function (http, app, ko) {
    //Note: This module exports an object.
    //That means that every module that "requires" it will get the same object instance.
    //If you wish to be able to create multiple instances, instead export a function.
    //See the "welcome" module for an example of function export.

    return {
        displayName: 'Search',
        books: ko.observableArray([]),
        activate: function () {

            //the router's activator calls this function and waits for it to complete before proceeding
            if (this.books().length > 0) {
                return;
            }

            var self = this;

            return http.get('http://localhost/clu.books.web.api/Search/Anything/test', { maxResults: 40 }, {}).then(
                function (response) {
                    var books = response.books;
                    books.forEach(function(book) {
                        book.information = ko.computed(function () {
                            return book.index + ') ' + book.title + ' - ' + book.author + ' - ' + book.publishedDate + ' (' + book.languageCode + ')';
                        });
                    });
                    self.books(response.books);
                },
                function(error) {
                    alert(error);
                });
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