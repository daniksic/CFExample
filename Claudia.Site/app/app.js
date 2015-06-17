(function () {
    'use strict';

    var app = angular.module('app', [
        // Angular modules 
        'ngAnimate',        // animations
        'ngRoute',          // routing
        'ngSanitize',       // sanitizes html bindings (ex: sidebar.js)
        //'angularFileUpload',// file uploader

        // Custom modules 
        'common',           // common functions, logger, spinner
        'common.bootstrap', // bootstrap dialog wrapper functions

        // 3rd Party Modules
        'breeze.angular',    // configures breeze for an angular app
        'breeze.directives', // contains the breeze validation directive (zValidate)

        'ui.bootstrap',      // ui-bootstrap (ex: carousel, pagination, dialog)
        //'xeditable',         // inline editor : http://vitalets.github.io/angular-xeditable/
        'textAngular'       // html text editor : http://textangular.com/ : <div text-angular ng-model="scopeVariable"></div>
        //'angularFileUpload' // file uploader https://github.com/danialfarid/angular-file-upload
    ]);
    
    // Handle routing errors and success events
    app.run(['$route', '$rootScope', 'breeze', 'routemediator', 
        function ($route, $rootScope, breeze, routemediator) {
            // Include $route to kick start the router.
            routemediator.setRoutingHandlers();

            //editableThemes.bs3.inputClass = 'input-sm';
            //editableThemes.bs3.buttonsClass = 'btn-sm';
            //editableOptions.theme = 'bs3';
        }
    ]);
})();