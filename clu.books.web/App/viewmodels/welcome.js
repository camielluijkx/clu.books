define(['durandal/app'], function (app) {
    var ctor = function () {
        this.displayName = 'Welcome to Clu Books Web';
        this.description = 'Clu Books Web is using Google Books API to search for books.';
        this.features = [
            'Search for books by anything',
            'Search for books by author',
            'Search for books by ISBN'
        ];
    };

    //Note: This module exports a function. That means that you, the developer, can create multiple instances.
    //This pattern is also recognized by Durandal so that it can create instances on demand.
    //If you wish to create a singleton, you should export an object instead of a function.
    //See the "search" module for an example of object export.

    return ctor;
});