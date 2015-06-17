﻿(function () {
    'use strict';

    var controllerId = 'carousel';

    angular.module('app').controller(controllerId,
        ['$scope', '$location', 'datacontext', 'common', 'config', 'controller.list.abstract', carousel]);

    function carousel($scope, $location, datacontext, common, config, cabs) {

        var keyCodes = config.keyCodes;
        var $q = common.$q;

        var vm = this;

        //inharitance
        cabs.extend(vm, controllerId);

        
        //overiders
        vm.pageChanged = pageChanged;
        vm.refresh = refresh;
        vm.search = search;
        vm.goToDetail = goToDetail;


        activate();

        function activate() {
            common.activateController([getList()], controllerId)
                .then(function () {
                    vm.log('Activated view', null, false);
                });
        }

        function getListCount() {
            return datacontext.carousel.getCount()
                .then(function (data) {
                vm.listCount = data;
            });
        }

        function getListFilteredCount() {
            vm.listFilteredCount = datacontext.carousel.getFilteredCount(vm.listSearch);
        }

        function getList(forceRefresh) {
            vm.loadingShow("Fetchng data...");

            return datacontext.carousel.getAll(forceRefresh, vm.currentPage, vm.pageSize, vm.listSearch)
                .then(processdata);

                function processdata(data) {
                    vm.list = data;

                    if (!vm.listCount || forceRefresh) {
                        // Only grab the full count once or on refresh
                        getListCount();
                    }

                    vm.loadingHide();
                    return $q.when(getListFilteredCount());
                }
        }

        function goToDetail(id) {
            if (id) {
                $location.path('/carousel/' + id);
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
