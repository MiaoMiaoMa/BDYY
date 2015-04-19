function DrugStoreManage($scope, $window) {
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
        return { searchType: $scope.SearchCondition.searchType, reviewType: $scope.SearchCondition.reviewType, department: '3', searchContentStart: $scope.SearchCondition.centent, searchContentEnd: "" };
    }

    $scope.showManagePage = function (uid) {
        $window.location.href = "../Admin/ApproveManage?patientID=" + uid;
    }

    //初始化数据
    $scope.onSearch();


    $scope.showApprove = function ($index) {
        //alert($index);
        var bh = $(document).height();
        var bw = $(window).width();
        var wh = $(window).height();
        var popwidth = 900;
        var popheight = 550;
        var v_left = (bw - popwidth) / 2;
        var v_top = bh > popheight ? (bh - popheight) / 2 : 0;
        $('.edialogheadertitle').text($scope.titleBig);
        $(".bg").css({ width: bw, height: bh, display: "block" })
        $('.pop_up_box').css({ width: popwidth, height: popheight });
        $('.pop_up_box').css({ left: v_left, top: v_top, display: "block" });
        $("#ApproveDiv").show();

        $window.callAJAX('../Account/GetPatientAllInfo', { uid: $index }, 'JSON', function (data) {
            if (data) {
                $scope.patientInfo = data;
                $scope.SmokingHisType = MyFactory.showSmokingStr($scope.patientInfo.SmokingHisType, $scope.patientInfo.SmokingHis);
            }
        }, null);
    }


    $scope.approve = function () {
        alert("发药登记成功！");
        $("#ApproveDiv").hide();
    }

    $scope.closeSE = function () {
        $("#ApproveDiv").hide();
    }





}



