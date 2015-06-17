(function () {
    'use strict';

    var serviceId = 'controller.list.abstract';

    // TODO: replace app with your module name
    angular.module('app').factory(serviceId, ['common', 'config', '$route', controller]);

    function controller(common, config, $route) {

        function Ctor() {
            //inheritable properties
            this.contentTitle = $route.current.$$route.settings.content;
            this.contentIcon = $route.current.$$route.settings.icon;

            this.list = [];
            this.listCount = 0;
            this.listFilteredCount = 0;
            this.listSearch = "";
            this.listFiltered = [];

            this.currentPage = 1;
            this.maxPagesToShow = 5;
            this.pageSize = 15;
        }

        Ctor.extend = function (object, controllerId) {
            Ctor.call(object);

            Object.defineProperty(object, 'log', {
                get: function () {
                    return common.logger.getLogFn(controllerId);
                }
            });
            Object.defineProperty(object, 'pageCount', {
                get: function () {
                    return Math.floor(this.listFilteredCount / this.pageSize) + 1;
                }
            });
            Object.defineProperty(object, 'loadingShow', {
                get: function () {
                    return common.showBusy(true);
                }
            });
            Object.defineProperty(object, 'loadingHide', {
                get: function () {
                    return common.showBusy(false);
                }
            });


            //overiders
            var _pagechanged = function () { object.log("pageChanged not implemented!"); };
            Object.defineProperty(object, 'pageChanged', {
                get: function (page) {
                    if (!page) { return _pagechanged; }
                    this.currentPage = page;
                    return _pagechanged;
                },
                set: function (newValue) {
                    _pagechanged = newValue;
                }
            });

            var _refresh = function () { object.log("Feature not implemented!"); };
            Object.defineProperty(object, 'refresh', {
                get: function () {
                    return _refresh;
                },
                set: function (newValue) {
                    _refresh = newValue;
                }
            });

            var _search = function () { object.log("Feature not implemented!"); };
            Object.defineProperty(object, 'search', {
                get: function () {
                    return _search;
                },
                set: function (newValue) {
                    _search = newValue;
                }
            });

            var _gotodetail = function () { object.log("Feature not implemented!"); };
            Object.defineProperty(object, 'goToDetail', {
                get: function () { return _gotodetail; },
                set: function (newValue) { _gotodetail = newValue; }
            });
        };


        // Define the functions and properties to reveal.
        return Ctor;

        //#region Internal Methods
        //#endregion
    }
})();