(function () {
    'use strict';

    var controllerId = 'carouseldetail';

    angular.module('app').controller(controllerId,
        ['$window', '$scope', '$route', '$location', '$routeParams', 'common', 'config', 'datacontext', 'models', 'bootstrap.dialog', 'helper', '$http', carousel]);

    function carousel($window, $scope, $route, $location, $routeParams, common, config, datacontext, model, bsDialog, helper, $http) {
        var vm = this;
        var entityName = model.entityNames.link;
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var logError = getLogFn(controllerId, 'error');
        var $q = common.$q;

        vm.contentTitle = $route.current.$$route.settings.content;
        vm.contentIcon = $route.current.$$route.settings.icon;

        vm.isNew = false;
        vm.isFileSend = false;
        vm.cancel = cancel;
        vm.delete = deleteLink;
        vm.goBack = goBack;
        vm.gotoList = gotoList;
        vm.hasChanges = false;
        vm.isSaving = false;
        vm.save = save;

        //vm.checkLink = checkLink;
        vm.previewLink = undefined;
        vm.imgtool = { imagetool: undefined, init: false, imageLoaded: false };

        $scope.model = undefined;
        $scope.files = [];
        $scope.filesAttr = { crop: "", size: "" };

        activate();

        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                //add the file object to the scope's files collection
                $scope.files.push(args.file);
            });
        });


        function activate() {
            Object.defineProperty(vm, 'canSave', { get: canSave });

            onDestroy();
            onHasChanges();

            common.activateController([getRequestedLink()], controllerId)
                .then(function () {
                    log('Activated view', null, false);
                });
        }

        function cancel() {
            datacontext.cancel();
        }

        function canSave() {
            var check = vm.hasChanges && !vm.isSaving && (vm.imgtool.init ? $scope.files.length > 0 : true);
            return check;
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

        function getImageTool() {
            if (vm.imgtool.init) {
                return vm.imgtool.imagetool;
            }

            var it = new ImageCropTool("#imgtool");

            //it.options.container.toolbox.buttons._choose.attr = {
            //    "type": "file",
            //    "accept": "image/*",
            //    //"style": "display:none;",
            //    "file-upload": ""
            //};
            it.options.container.toolbox.buttons.open.text = "Select picture";
            it.options.container.toolbox.buttons.open.callback = function (file) {
                if (it.Scenes[0]) {
                    it.Scenes[0].removeSelf();
                    it.Scenes = [];
                }
                $scope.files = [];
                $scope.files.push(file);

            };
            it.options.control.toolbox.show = false;
            //it.options.control.toolbox.buttons.preview.show = false;
            //it.options.control.toolbox.buttons.save.callback = function (e) {
            //    $scope.model.crop = this.getCropCord();
            //    //    sendFile($("input[type=file]")[0].files[0]);
            //};
            it.options.control.scene.input.show = false;
            it.options.control.result.size = {"width":1200,"height":400};

            it.init();
            vm.imgtool.init = true;
            return it;
        }

        function getRequestedLink() {
            var val = $routeParams.id;
            if (val === 'new') {
                vm.isNew = true;
                setUpImageTool();
                return $scope.model = datacontext.carousel.createEntity();
            }

            common.showBusy(true);
            return $scope.model = datacontext.carousel._getById(entityName, val)
                .then(foundEntity);

            function foundEntity(data) {
                $scope.model = data;
                common.showBusy(false);
            }
        }

        function goBack() {
            datacontext.cancel();
            $window.history.back();
        }

        function gotoList() {
            $location.path('/carousel');
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

            return (vm.isNew && !vm.isFileSend) ? saveNew() : saveEdit();

            function saveEdit() {
                return datacontext.save().then(function (saveResult) {
                    vm.isSaving = false;
                    gotoList();
                }, function (error) {
                    vm.isSaving = false;
                    logError(error);
                });
            }
            function saveNew() {
                return $q.when(sendFiles()).then(function (response) {
                    vm.isSaving = false;

                    var data = angular.fromJson(response.data);

                    if (!data["ServerFileName"] && !data["ClientLocalFileName"]) {
                        logError("Error ending file: " + data);
                        vm.isSaving = false;
                        cancel();
                    }

                    $scope.model.serverFileName = data["ServerFileName"];
                    $scope.model.clientLocalFileName = data["ClientLocalFileName"];
                    //$scope.model.dateTimeStamp = Date.now();
                    $scope.model.categoryId = 6;

                    return datacontext.save().then(function (saveResult) {
                        vm.isSaving = false;
                        //helper.replaceLocationUrlGuidWithId($scope.model.id);
                        vm.gotoList();
                    }, function (error) {
                        vm.isSaving = false;
                        logError(error);
                    });
                });
            }
        }

        function sendFiles() {
            var img = vm.imgtool.imagetool.Scenes[0];
            $scope.filesAttr.crop = img.getCropCord();
            $scope.filesAttr.size = img.getOrginalSize();

             return $http({
                method: 'POST',
                url: "/api/gallery/new",
                //IMPORTANT!!! You might think this should be set to 'multipart/form-data' 
                // but this is not true because when we are sending up files the request 
                // needs to include a 'boundary' parameter which identifies the boundary 
                // name between parts in this multi-part request and setting the Content-type 
                // manually will not set this boundary parameter. For whatever reason, 
                // setting the Content-type to 'false' will force the request to automatically
                // populate the headers properly including the boundary parameter.
                headers: { 'Content-Type': undefined },
                //This method will allow us to change how the data is sent up to the server
                // for which we'll need to encapsulate the model data in 'FormData'
                transformRequest: function (data) {
                    var formData = new FormData();
                    
                    //now add all of the assigned files
                    for (var i = 0; i < data.files.length; i++) {
                        //need to convert our json object to a string version of json otherwise
                        // the browser will do a 'toString()' on the object which will result 
                        // in the value '[Object object]' on the server.
                        formData.append("fileAttr" + i, angular.toJson({ crop: data.model.crop, size: data.model.size, imageCategory: "carousel" }));

                        var file = data.files[i];
                        file.crop = data.model.crop;
                        file.imgsize = data.model.size;

                        //add each file to the form data and iteratively name them
                        formData.append("file" + i, file);
                    }
                    return formData;
                },
                //Create an object that contains the model and files which will be transformed
                // in the above transformRequest method
                data: { model: $scope.filesAttr, files: $scope.files }
            }).
            success(function (data, status, headers, config) {
                vm.isFileSend = true;
            }).
            error(function (data, status, headers, config) {
                logError("failed to upload!");
            });
        }

        function setUpImageTool() {
            vm.imgtool.imagetool = getImageTool();
        }

    }
})();
