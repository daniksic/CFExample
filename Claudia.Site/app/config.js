(function () {
    'use strict';

    var app = angular.module('app');

    // Configure Toastr
    toastr.options.timeOut = 4000;
    toastr.options.positionClass = 'toast-bottom-right';

    var keyCodes = {
        backspace: 8,
        tab: 9,
        enter: 13,
        esc: 27,
        space: 32,
        pageup: 33,
        pagedown: 34,
        end: 35,
        home: 36,
        left: 37,
        up: 38,
        right: 39,
        down: 40,
        insert: 45,
        del: 46
    };

    // For use with the HotTowel-Angular-Breeze add-on that uses Breeze
    var remoteServiceName = 'bapi';

    var imageSettings = {
        imageBasePath: '../images/',
        unknownPersonImageSource: 'unknown_person.jpg', //TODO add default user picture
        gallery: {
            basePath: '/Images/gallery/',
            imageSuffix: '_glimg.jpg',
            thumbSuffix: '_gltmb.jpg',
            getImageUrl: function (filename) { return this.basePath + filename + this.imageSuffix; },
            getThumbUrl: function (filename) { return this.basePath + filename + this.thumbSuffix; }
        },
        youtube: { //http://img.youtube.com/vi/WaNYnBkEhkk/0.jpg or https://i.ytimg.com/vi/WaNYnBkEhkk/hqdefault.jpg
            basePath: 'http://img.youtube.com/vi/',
            imageSuffix: '/0.jpg',
            thumbSuffix: '',
            getImageUrl: function (filename) { return this.basePath + filename + this.imageSuffix; },
            getThumbUrl: function (filename) { return this.basePath + filename + this.thumbSuffix; }
        },
        carousel: {
            basePath: '/Images/gallery/',
            imageSuffix: '_jtimg.jpg',
            thumbSuffix: '_jttmb.jpg',
            getImageUrl: function (filename) { return this.basePath + filename + this.imageSuffix; },
            getThumbUrl: function (filename) { return this.basePath + filename + this.thumbSuffix; }
        }
    };

    var events = {
        controllerActivateSuccess: 'controller.activateSuccess',
        hasChangesChanged: 'datacontext.hasChangesChanged',
        entitiesChanged: 'datacontext.entitiesChanged',
        spinnerToggle: 'spinner.toggle'
    };

    var google = {
        CLIENT_ID: '9314897158'
    };

    var config = {
        appErrorPrefix: '[Error] ', //Configure the exceptionHandler decorator
        docTitle: 'CF: ',
        events: events,
        imageSettings: imageSettings,
        keyCodes: keyCodes,
        remoteServiceName: remoteServiceName,
        version: '2.1.0',
        google: google
    };

    app.value('config', config);

    app.config(['$logProvider', function ($logProvider) {
        // turn debugging off/on (no info or warn)
        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }
    }]);

    // needed to alaw cross origin iframes like youtube
    app.config(['$sceDelegateProvider', function ($sceDelegateProvider) {
        $sceDelegateProvider.resourceUrlWhitelist(['self', 'http://www.youtube.com/**']);
    }]);

    //#region Configure the common services via commonConfig
    app.config(['commonConfigProvider', function (cfg) {
        cfg.config.controllerActivateSuccessEvent = config.events.controllerActivateSuccess;
        cfg.config.spinnerToggleEvent = config.events.spinnerToggle;
    }]);
    //#endregion
})();