(function () {
    'use strict';

    var serviceId = 'datacontext';
    angular.module('app')
        .factory(serviceId, ['$rootScope', 'common', 'config', '$http', '$location', 'entityManagerFactory', 'models', 'breeze', 'repositories', datacontext]);

    function datacontext($rootScope, common, config, $http, $location, emFactory, models, breeze, repositories) {
        var entityNames = models.entityNames;
        var events = config.events;
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(serviceId);
        var logError = getLogFn(serviceId, 'error');
        var logSuccess = getLogFn(serviceId, 'success');
        var manager = emFactory.newManager();
        var primePromise;
        var repoNames = ['gallery', 'youtube', 'carousel', 'recipe'];
        var $q = common.$q;        //var webapi = $location.$$absUrl.split("/",3).join("/") + "/" + config.remoteServiceName;

        // used for initial load of data
        //var storeMeta = {
        //    isLoaded: {
        //        gallery: false,
        //        youtube: false,
        //        comments: false
        //    }
        //};

        var service = {
            cancel: cancel,
            markDeleted: markDeleted,
            prime: prime,
            save: save,
            // sub-services
            //zStorage: zStorage,
            //zStorageWip: zStorageWip
            // Repositories to be added on demand:
            //      gallery
            //      youtubes
        };

        init();

        return service;

        function init() {
            repositories.init(manager);
            defineLazyLoadedRepos();
            setupEventForHasChangesChanged();
            setupEventForEntitiesChanged();
        }



        function cancel() {
            if (manager.hasChanges()) {
                manager.rejectChanges();
                logSuccess('Canceled changes', null, true);
            }
        }

        function defineLazyLoadedRepos() {
            repoNames.forEach(function (name) {
                Object.defineProperty(service, name, {
                    configurable: true, // will redefine this property once
                    get: function () {
                        // The 1st time the repo is request via this property, 
                        // we ask the repositories for it (which will inject it).
                        var repo = repositories.getRepo(name);
                        // Rewrite this property to always return this repo;
                        // no longer redefinable
                        Object.defineProperty(service, name, {
                            value: repo,
                            configurable: false,
                            enumerable: true
                        });
                        return repo;
                    }
                });
            });
        }

        function markDeleted(entity) {
            entity.isDeleted = true;
        }

        // gets initial data for dashboard
        function prime() {
            if (primePromise) return primePromise;

            // look in local storage, if data is here, 
            // grab it. otherwise get from 'resources'
            var storageEnabledAndHasData = zStorage.load(manager);
            primePromise = storageEnabledAndHasData ?
                $q.when(log('Loading entities and metadata from local storage')) :
                $q.all([service.lookup.getAll(), service.speaker.getPartials(true)])
                    .then(extendMetadata);
            return primePromise.then(success);

            function success() {
                service.lookup.setLookups();
                zStorage.save();
                log('Primed the data');
            }

            function extendMetadata() {
                var metadataStore = manager.metadataStore;
                //models.extendMetadata(metadataStore);
                registerResourceNames(metadataStore);
            }

            // Wait to call until entityTypes are loaded in metadata
            function registerResourceNames(metadataStore) {
                var types = metadataStore.getEntityTypes();
                types.forEach(function (type) {
                    if (type instanceof breeze.EntityType) {
                        set(type.shortName, type);
                    }
                });

                var personEntityName = entityNames.person;
                ['Speakers', 'Speaker', 'Attendees', 'Attendee'].forEach(function (r) {
                    set(r, personEntityName);
                });

                function set(resourceName, entityName) {
                    metadataStore.setEntityTypeForResourceName(resourceName, entityName);
                }
            }
        }


        //TODO saving logic
        function save() {
            return manager.saveChanges()
                .then(saveSuccess)
                .catch(_queryFailed);

            function saveSuccess(result) {
                logSuccess("Saved!", result, true);
            }
        }

        //#region private

        function _queryFailed(error) {
            var msg = config.appErrorPrefix + error.message;
            logError(msg, error);
            throw error;
        }

        function setupEventForHasChangesChanged() {
            manager.hasChangesChanged.subscribe(function (eventArgs) {
                var data = { hasChanges: eventArgs.hasChanges };
                // send the message (the ctrl receives it)
                //log("hasChangesChanged");
                common.$broadcast(events.hasChangesChanged, data);
            });
        }

        function setupEventForEntitiesChanged() {
            // We use this for detecting changes of any kind so we can save them to local storage
            manager.entityChanged.subscribe(function (changeArgs) {
                if (changeArgs.entityAction === breeze.EntityAction.PropertyChange) {
                    //interceptPropertyChange(changeArgs);
                    //log("entityChanged");
                    common.$broadcast(events.entitiesChanged, changeArgs);
                }
            });
        }

        //#endregion
    }
})();