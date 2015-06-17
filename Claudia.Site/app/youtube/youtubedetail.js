(function () {
    'use strict';

    var controllerId = 'youtubedetail';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['$window', '$scope', '$route', '$location', '$routeParams', 'common', 'config', 'datacontext', 'models', 'bootstrap.dialog', 'helper', youtubedetail]);

    function youtubedetail($window, $scope, $route, $location, $routeParams, common, config, datacontext, model, bsDialog, helper) {
        var vm = this;
        var entityName = model.entityNames.link;
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var logError = getLogFn(controllerId, 'error');
        var $q = common.$q;

        vm.contentTitle = $route.current.$$route.settings.content;
        vm.contentIcon = $route.current.$$route.settings.icon;

        vm.isNew = false;
        vm.cancel = cancel;
        vm.delete = deleteLink;
        vm.goBack = goBack;
        vm.gotoList = gotoList;
        vm.hasChanges = false;
        vm.isSaving = false;
        vm.save = save;
        //vm.checkLink = checkLink;
        vm.previewLink = undefined;

        $scope.model = undefined;

        activate();

        Object.defineProperty(vm, 'canSave', { get: canSave });

        function activate() {
            onDestroy();
            onHasChanges();

            common.activateController([getRequestedLink()], controllerId)
                .then(function() {
                 log('Activated view', null, false);
            });
        }

        function cancel() {
            datacontext.cancel();
        }

        function canSave() {
             return vm.hasChanges && !vm.isSaving;
        }

        function deleteLink() {
            return bsDialog.deleteDialog(vm.contentTitle).then(confirmDelete);

            function confirmDelete() {
                datacontext.markDeleted($scope.model);
                vm.save().then(success).catch(failed);

                function success() {
                    gotoList();
                }

                function failed() {
                    cancel();
                }
            }
        }

        function getRequestedLink() {
            var val = $routeParams.id;
            if (val === 'new') {
                vm.isNew = true;
                return $scope.model = datacontext.youtube.createEntity();
            }

            return $scope.model = datacontext.youtube._getById(entityName, val)
                .then(foundEntity);

            function foundEntity(data) {
                $scope.model = data;
            }
        }

        function goBack() {
            datacontext.cancel();
            $window.history.back();
        }

        function gotoList() {
             $location.path('/youtube');
        }

        function onDestroy() {
            $scope.$on('$destroy', function () {
                datacontext.cancel();
            });
        }

        function onHasChanges() {
            $scope.$on(config.events.hasChangesChanged,
                function (event, data) {
                    vm.hasChanges = data.hasChanges;
                });
        }

        function save() {
            if (!canSave()) { return $q.when(null); } // Must return a promise

            vm.isSaving = true;

            $scope.model.categoryId = 2;

            return datacontext.save().then(function (saveResult) {
                vm.isSaving = false;
                //helper.replaceLocationUrlGuidWithId($scope.model.id);
                vm.gotoList();
            }, function (error) {
                vm.isSaving = false;
                logError(error);
            });
        }
    }
})();
