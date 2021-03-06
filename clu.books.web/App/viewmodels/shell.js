﻿define(['plugins/router', 'durandal/app'], function (router, app) {
    return {
        router: router,
        activate: function () {
            router.map([
                { route: '', title:'Welcome', moduleId: 'viewmodels/welcome', nav: true },
                { route: 'search', moduleId: 'viewmodels/search', nav: true }
            ]).buildNavigationModel();

            return router.activate();
        }
    };
});