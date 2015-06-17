(function () {
    'use strict';
    var controllerId = 'youtube';
    angular.module('app').controller(controllerId,
        ['$scope', 'common', 'config', '$location', 'datacontext', 'controller.list.abstract', youtube]);

    function youtube($scope, common, config, $location, datacontext, clabs) {
        var keyCodes = config.keyCodes;
        var $q = common.$q;

        var vm = this;

        //inharitance
        clabs.extend(vm, controllerId);

        //overiders
        vm.pageChanged = pageChanged;
        vm.refresh = refresh;
        vm.search = search;
        vm.goToDetail = goToDetail;

        activate();

        function activate() {
            var promises = [getList()];

            common.activateController([promises], controllerId)
                .then(function () { vm.log('Activated view', null, false); });
        }


        function getListCount() {
            return datacontext.youtube.getCount()
                .then(function (data) {
                    vm.listCount = data;
                });
        }

        function getListFilteredCount() {
            vm.listFilteredCount = datacontext.youtube.getFilteredCount(vm.listSearch);
        }

        function getList(forceRefresh) {
            common.showBusy(true);
            return datacontext.youtube.getAll(forceRefresh, vm.currentPage, vm.pageSize, vm.listSearch)
                .then(processdata);

            function processdata(data) {
                vm.list = data;
                if (!vm.listCount || forceRefresh) {
                    // Only grab the full count once or on refresh
                    getListCount();
                }
                getListFilteredCount();

                common.showBusy(false);
                return data;
            }
        }

        function goToDetail(id) {
            if (id) {
                $location.path('/youtube/' + id);
            }
        }

        function pageChanged(page) {
            if (!page) { return; }
            vm.paging.currentPage = page;
            getList();
        }

        function refresh() {
            getList(true);
        }

        function search($event) {
            if ($event.keyCode === keyCodes.esc) { vm.listSearch = ''; }
            getList();
        }
    }
})();
