(function () {
    'use strict';

    var serviceId = 'repository.youtube';

    // TODO: replace app with your module name
    angular.module('app').factory(serviceId,
        ['models', 'repository.abstract', repository]);

    function repository(models, AbstractRepository) {
        var entityName = models.entityNames.link;
        var EntityQuery = breeze.EntityQuery;
        var Predicate = breeze.Predicate;
        var showDeleted = Predicate.create("isDeleted", "==", false),
            objectCategory = Predicate.create("categoryId", "==", 2), //change to type
            activePredicate = Predicate.create("isDeleted", "==", false).and("categoryId", "==", 2);

        function Ctor(mgr) {
            this.serviceId = serviceId;
            this.entityName = entityName;
            this.manager = mgr;
            //this.zStorage = zStorage;

            // Exposed data access functions
            this.createEntity = createEntity;
            this.getAll = getAll;
            this.getCount = getCount;
            this.getFilteredCount = getFilteredCount;
        }

        AbstractRepository.extend(Ctor);

        return Ctor;

        function createEntity() {
            return this.manager.createEntity(entityName);
        }

        // Formerly known as datacontext.getYoutubeList()
        function getAll(forceRemote, page, size, nameFilter) {
            var self = this;

            var orderBy = 'dateTimeStamp';
            var take = size || 20;
            var skip = page ? (page - 1) * size : 0;

            if (self.isLoaded && !forceRemote) {
                // Get the page of gallery from local cache
                return self.$q.when(getByPage());
            }

            // Load all list to cache via remote query
            return EntityQuery.from('Links')
                            .select()
                            .where(showDeleted).where(objectCategory)
                            .orderByDesc(orderBy)
                            .toType(entityName)
                            .inlineCount(true)
                            .using(self.manager).execute()
                            .then(querySucceeded)
                            .catch(self._queryFailed);

            function querySucceeded(data) {
                self.isLoaded = true;
                self.log('Retrieved [Youtube] list from remote data source', data.results.length, false);
                return getByPage();
            }

            function getByPage() {
                var predicate = self._predicates.isNotNullo;

                if (nameFilter) {
                    predicate = Predicate.create("title", "contains", nameFilter);
                }

                var gallery = EntityQuery.from(entityName)
                    .where(showDeleted).where(objectCategory)
                    .where(predicate)
                    .orderByDesc(orderBy)
                    .take(take).skip(skip)
                    .toType(entityName)
                    .using(self.manager)
                    .executeLocally();

                return gallery;
            }
        }

        function getCount() {
            var self = this;
            if (self.isLoaded) {
                return self.$q.when(self._getLocalEntityCount(entityName, activePredicate));
            }


            // List aint loaded; ask the server for a count.
            return EntityQuery.from('Links').take(0).inlineCount()
                .where(showDeleted).where(objectCategory)
                .using(self.manager).execute()
                //.toType(entityName)
                .then(self._getInlineCount);
        }

        function getFilteredCount(nameFilter) {
            var predicate = Predicate.create("title", "contains", nameFilter);

            var gallery = EntityQuery.from(entityName)
                .where(showDeleted).where(objectCategory)
                .where(predicate)
                .using(this.manager)
                .toType(entityName)
                .executeLocally();

            return gallery.length;
        }

    }
})();