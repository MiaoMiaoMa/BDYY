function ApproveManageList($scope, $window)
{
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
        return { searchType: $scope.SearchCondition.searchType, reviewType: $scope.SearchCondition.reviewType, department:'1', searchContentStart: "", searchContentEnd: "" };
    }

    $scope.showManagePage = function (uid) {
        $window.location.href = "../Admin/ApproveManage?patientID=" + uid;
    }

    //初始化数据
    $scope.onSearch();
}

function ApproveManage($scope, $window, MyFactory)
{
    
    var paras = getURLParas($window.location.href);
    var uid = typeof paras["patientID"] != "undefined" ? paras["patientID"] : "";
    MyFactory.callAJAX('../Admin/GetPatinetALlInfo', { uid: uid}, 'JSON', function (data) {
        if (data) {
            $scope.patientInfo = data.patient;
            $scope.comments = data.comments;
            $scope.commentsList = data.commentsList;
            $scope.SmokingHisType = MyFactory.showSmokingStr($scope.patientInfo.SmokingHisType, $scope.patientInfo.SmokingHis);
        }
    }, null);

    $scope.addComments = function () {
        MyFactory.callAJAX('../Admin/AddComment', { data: $('#commentContent').val(), patientID: $scope.patientInfo.UserID}, 'JSON', function (data) {
            if (data.length >0) {
                $scope.commentsList = data;
                $('#commentContent').val('');
            }
        }, null);
    }
}