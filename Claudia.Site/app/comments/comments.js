(function () {
    'use strict';

    var controllerId = 'comments';

    // TODO: replace app with your module name
    angular.module('app').controller(controllerId,
        ['common', '$route', '$location', 'datacontext', comments]);

    function comments(common, $route, $location, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = $route.current.$$route.settings.content;

        vm.totalCount = 0;
        vm.dataList = [];

        activate();

        function activate() {
            var promises = [getCommentsList()];

            common.activateController([promises], controllerId)
                .then(function () { log('Activated view', null, false); });
        }

        function getCommentsList() {
            return datacontext.getCommentsList().then(function (data) {
                vm.totalCount = data.inlineCount;
                return vm.dataList = data;
            });
        }

        function goToDetail(id) {
            if (id) {
                $location.path('/comment/' + id);
            }
        }
    }
})();
