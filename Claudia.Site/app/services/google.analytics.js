(function () {
    'use strict';

    var serviceId = 'google.analytics';

    // TODO: replace app with your module name
    angular.module('app').factory(serviceId, ['$window', '$document', 'config', analytics]);

    function analytics($window, $document, config) {
        // Define the functions and properties to reveal.


        var service = {
            init: init,
            startup: startup
        };

        return service;


        //#region Internal Methods
        function init() {
            activate($window, $document[0]);
            return this;
        }

        function activate(w, d, s, g, js, fjs) {
            s = s || 'script';
            g = w.gapi || (w.gapi = {});
            g.analytics = { q: [], ready: function (cb) { this.q.push(cb) } };
            js = d.createElement(s);
            fjs = d.getElementsByTagName(s)[0];
            js.src = 'https://apis.google.com/js/platform.js';
            fjs.parentNode.insertBefore(js, fjs);
            js.onload = function () { g.load('analytics') };
        }

        function startup() {
            $window.gapi.analytics.ready(function () {

                // Step 3: Authorize the user.

                var CLIENT_ID = config.google.CLIENT_ID;

                gapi.analytics.auth.authorize({
                    container: 'auth-button',
                    clientid: CLIENT_ID,
                });

                // Step 4: Create the view selector.

                var viewSelector = new gapi.analytics.ViewSelector({
                    container: 'view-selector'
                });

                // Step 5: Create the timeline chart.

                var timeline = new gapi.analytics.googleCharts.DataChart({
                    reportType: 'ga',
                    query: {
                        'dimensions': 'ga:date',
                        'metrics': 'ga:sessions',
                        'start-date': '30daysAgo',
                        'end-date': 'yesterday',
                    },
                    chart: {
                        type: 'LINE',
                        container: 'timeline'
                    }
                });

                // Step 6: Hook up the components to work together.

                gapi.analytics.auth.on('success', function (response) {
                    viewSelector.execute();
                });

                viewSelector.on('change', function (ids) {
                    var newIds = {
                        query: {
                            ids: ids
                        }
                    }
                    timeline.set(newIds).execute();
                });
            });
        }
        //#endregion
    }
})();