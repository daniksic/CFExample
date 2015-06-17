(function() {
    'use strict';
    
    var serviceId = 'entityManagerFactory';
    angular.module('app')
        .factory(serviceId, ['breeze', 'config', 'models', emFactory]);

    function emFactory(breeze, config, model) {
        breeze.config.initializeAdapterInstance('modelLibrary', 'backingStore', true);
        breeze.NamingConvention.camelCase.setAsDefault();

        // Tell breeze not to validate when we attach a newly created entity to any manager.
        // We could also set this per entityManager
        new breeze.ValidationOptions({ validateOnAttach: false }).setAsDefault();

        var serviceName = config.remoteServiceName;
        var metadataStore = createMetadataStore(); // for custom ctor objects // new breeze.MetadataStore();

        var provider = {
            metadataStore: metadataStore,
            newManager: newManager
        };

        return provider;

        function createMetadataStore() {
            var store = new breeze.MetadataStore();
            model.configureMetadataStore(store);
            return store;
        }

        function newManager() {
            var mgr = new breeze.EntityManager({
                serviceName: serviceName,
                metadataStore: metadataStore
            });

            //if (mgr.metadataStore.isEmpty()) {
            //    mgr.fetchMetadata().then(function() { metadataStore = mgr.metadataStore });
            //}

            return mgr;
        }
    }
})();