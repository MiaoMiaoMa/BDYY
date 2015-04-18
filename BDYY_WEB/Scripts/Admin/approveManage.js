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
    $scope.needShow = true;

    var paras = getURLParas($window.location.href);
    var uid = typeof paras["patientID"] != "undefined" ? paras["patientID"] : "";
    //获取Patient全部信息（基本信息，病例信息等）
    MyFactory.callAJAX('../Admin/GetPatinetALlInfo', { uid: uid }, 'JSON', function (data) {
        if (data) {
            $scope.patientInfo = data.patient;
            $scope.comments = data.comments;
            $scope.commentsList = data.commentsList;
            $scope.filesList = data.filesList;
            $scope.SmokingHisType = MyFactory.showSmokingStr($scope.patientInfo.SmokingHisType, $scope.patientInfo.SmokingHis);
        }
    }, null);

    //更新部门或者状态
    $scope.updateDepartOrStatus = function (isDepart) {
        var data;
        if (isDepart) {
            departID = $("#departSelect").find("option:selected").val();
            data = { patientID: $scope.patientInfo.UserID, departTo: departID, statusTo: "", isAppointment: "" };
        }
        else {
            statusID = $("#statusSelect").find("option:selected").val();
            data = { patientID: $scope.patientInfo.UserID, departTo: "", statusTo: statusID, isAppointment: "" };
        }

        //更新
        MyFactory.callAJAX('../Admin/UpdateDepartOrStatus', data, 'JSON', function (data) {
            if (data && data.result) {
                $scope.msg = isDepart ? "部门更新成功！" : "状态更新成功！";
            }
        }, null);
    }

    //添加注释
    $scope.addComments = function () {
        MyFactory.callAJAX('../Admin/AddComment', { data: $('#commentContent').val(), patientID: $scope.patientInfo.UserID}, 'JSON', function (data) {
            if (data.length >0) {
                $scope.commentsList = data;
                $('#commentContent').val('');
            }
        }, null);
    }

    //上传附件
   $scope.addFiles = function () {
        if ($("#file-list").html() == "") {
            $window.alert("请选择上传文件！");
            return false;
        }

        var formData = new FormData($("#formApprove")[0]);
        $.ajax({
            url: "../Admin/AddFile",
            type: 'POST',
            data: formData,
            async: false,
            cache: false,
            dataType: 'JSON',
            success: function (data) {
                $scope.filesList = data;
                $("#file-list").html("");
            },
            cache: false,
            contentType: false,
            processData: false
        });
    }

}




$('#attachmentFile').MultiFile({ list: '#file-list' });