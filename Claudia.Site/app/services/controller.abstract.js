(function () {
    'use strict';

    var serviceId = 'controller.abstract';

    // TODO: replace app with your module name
    angular.module('app').factory(serviceId,
        ['common', 'config', '$route', controller]);

    function controller(common, config, $route) {
        function Ctor() {
            //inheritable properties
            this.contentTitle = $route.current.$$route.settings.content;
            this.contentIcon = $route.current.$$route.settings.icon;

        }

        Ctor.extend = function (object, controllerId) {
            Ctor.call(object);

            Object.defineProperty(object, 'log', {
                get: function () {
                    return common.logger.getLogFn(controllerId);
                }
            });


            //overiders
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