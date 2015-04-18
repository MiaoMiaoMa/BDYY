function AppointApprove($scope, MyFactory)
{
    $scope.currentPage = 1;
    $scope.pageSize = PAGESIZE;

    $scope.SearchCondition = new Object();
    $scope.SearchCondition.searchType = '0';
    $scope.SearchCondition.reviewType = '1';
    $scope.SearchCondition.content = '';
        

    $scope.onSearch = function () {
        callAJAX('../Admin/GetAppointReviewList', $scope.conditionCombin(), 'JSON', function (data) {
            if (data) {
                $scope.patientList = data;
            }
        }, null);

    }

    $scope.conditionCombin = function () {
        return { searchType: $scope.SearchCondition.searchType, reviewType: $scope.SearchCondition.reviewType, searchContentStart: $scope.SearchCondition.content, searchContentEnd: "" };
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

    $scope.showApprove = function ($index) {
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


        MyFactory.callAJAX('../Account/GetPatientAllInfo', { uid: $scope.patientList[$index].UserID }, 'JSON', function (data) {
            if (data) {
                $scope.patientInfo = data;
                $scope.SmokingHisType = MyFactory.showSmokingStr($scope.patientInfo.SmokingHisType, $scope.patientInfo.SmokingHis);
            }
        }, null);
    }

    $scope.approve = function (petientID) {
        MyFactory.callAJAX('../Admin/Approve', { petientID: petientID }, 'JSON', function (data) {
            if (data && data.result) {
                $scope.closeSE();
                for (var obj in $scope.patientList) {
                    if (obj["petientID"] = patientID)
                    {
                        obj["Isverify"] = "3";
                    }
                }
                alert("审批成功！");
            }
        }, null);
    }

    $scope.closeSE = function() {
        $("#ApproveDiv").hide();
        $(".bg").css({ display: "none" })
        $('.pop_up_box').css({ display: "none" });
    }

    //初始化数据
    $scope.onSearch();
}

