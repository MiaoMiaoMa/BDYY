function ServiceManage($scope) {
    $scope.currentPage = 1;
    $scope.pageSize = PAGESIZE;

    $scope.SearchCondition = new Object();
    $scope.SearchCondition.searchType = '0';
    $scope.SearchCondition.reviewType = '0';



    $scope.onSearch = function () {
        callAJAX('../Admin/GetApplicationReviewList', $scope.conditionCombin(), 'JSON', function (data) {
            if (data) {
                $scope.patientList = data;
            }
        }, null);
    }

    $scope.conditionCombin = function () {
        return { searchType: $scope.SearchCondition.searchType, reviewType: $scope.SearchCondition.reviewType, department: '2', searchContentStart: "", searchContentEnd: "" };
    }

    //显示列表序号
    $scope.showIndex = function (index) {
        if ($scope.currentPage == 1) {
            return index + 1;
        }
        else {
            return ($scope.currentPage - 1) * PAGESIZE + index + 1;
        }
    }

    //初始化数据
    $scope.onSearch();
}