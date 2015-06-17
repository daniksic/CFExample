(function () {
    'use strict';
    var controllerId = 'dashboard';
    angular.module('app').controller(controllerId,
        ['common', 'datacontext', 'controller.abstract', '$location', 'google.analytics', dashboard]);

    function dashboard(common, datacontext, cabs, $location, ga) {

        var vm = this;

        vm.galleryCount = 0;
        vm.youtubeCount = 0;

        vm.gotoGallery = gotoGallery;
        vm.gotoYoutube = gotoYoutube;

        cabs.extend(vm, controllerId);

        activate();

        function activate() {
            var promises = [getGalleryCount(), getYoutubeCount()];
            common.activateController(promises, controllerId)
                .then(function () { vm.log('Activated Dashboard View'); });
        }

        function getGalleryCount() {
            return datacontext.gallery.getCount()
                .then(function (data) {
                    return vm.galleryCount = data;
                });
        }

        function getYoutubeCount() {
            return datacontext.youtube.getCount()
                .then(function (data) {
                    return vm.youtubeCount = data;
                });
        }


        //TODO in config add clientid then activate google
        function activeGoogleAnalytics() {
            ga.init().startup();
        }

        function gotoGallery() {
            $location.path('/gallery');
        }
        function gotoYoutube() {
            $location.path('/youtube');
        }
    }
})();