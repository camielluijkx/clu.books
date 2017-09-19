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

            var defaultBook = {
                index: 1,
                author: "Ernesto Guevara",
                title: "The motorcycle diaries",
                publishedDate: "2003",
                language: "EN",
                description: null
            };
            defaultBook.information = ko.computed(function () {
                return defaultBook.index + ') ' + defaultBook.title + ' - ' + defaultBook.author + ' - ' + defaultBook.publishedDate + ' (' + defaultBook.languageCode + ')';
            });
            //self.books.push(defaultBook);

            return http.jsonp('http://localhost/clu.books.web.api/Search/Anything/test', { maxResults: 40 }, 'jsoncallback')
                .then(function (response) {
                    self.books(response.books);
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