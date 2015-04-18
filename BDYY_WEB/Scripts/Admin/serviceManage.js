function ServiceManage($scope, $window) {
    $scope.currentPage = 1;
    $scope.pageSize = PAGESIZE;

    $scope.SearchCondition = new Object();
    $scope.SearchCondition.searchType = '0';
    $scope.SearchCondition.reviewType = '0';
    $scope.SearchCondition.centent = '';


    $scope.onSearch = function () {
        callAJAX('../Admin/GetApplicationReviewList', $scope.conditionCombin(), 'JSON', function (data) {
            if (data) {
                $scope.patientList = data;
            }
        }, null);
    }

    $scope.conditionCombin = function () {
        return { searchType: $scope.SearchCondition.searchType, reviewType: $scope.SearchCondition.reviewType, department: '2', searchContentStart: $scope.SearchCondition.centent, searchContentEnd: "" };
    }

    $scope.showManagePage = function (uid) {
        $window.location.href = "../Admin/ApproveManage?patientID=" + uid;
    }

    //初始化数据
    $scope.onSearch();
}