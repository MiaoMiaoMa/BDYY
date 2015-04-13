
//Controller for 添加预约
function AddPatientBasicInfo($scope, $window)
{
    callAJAX('../Home/GetPatientInfoEmptyModel', null, 'JSON', function (data) {
        if (data) {
            $scope.PatientInfo = data.patient;
            //$scope.IdentityType = data.identityType;
            //$scope.Province = data.province;
            //$scope.Hospital = data.hospital;
        }
    }, null);

    $scope.Submit = function () {
        patientData = angular.copy($scope.PatientInfo);
        if (patientData.IdentityType == "1" && !isCardNo(patientData.IdentityNumber)) {
            return;
        }
        if ($("#indetityNumber2").val() != patientData.IdentityNumber) {
            alert("请确认身份证号码！");
            $("#indetityNumber2").focus();
            return;
        }
        if ($("#gzs").attr("checked") != "checked")
        {
            alert("请确认您已阅读告知书！");
            $("#gzs").focus();
            return;
        }
        //获取吸烟年限
        if (patientData.SmokingHisType == '2')
        {
           patientData.SmokingHis = $("#smoking2").val();            
        }
        else if (patientData.SmokingHisType == '3') {
            patientData.SmokingHis = $("#smoking3").val();
        }

        patientData = { data: JSON.stringify(patientData) };

        //防止多次提交
        $("#savebutton").hidden();
        
        callPostAJAX('../Home/AddPatientInfo', patientData, 'JSON', function (data) {
            var result = angular.fromJson(data);
            if (result.IsSuccess) {
                $window.location.href = '../Home/Success';
            }
            else if(result.errormsg != "") {
                alert(result.errormsg);
            }
        }, null);  
    }
}

//Controller for 用户基本信息页
function PatientInfo($scope) {
    $scope.SmokingHisType = "不吸烟";
    callAJAX('../Account/GetPatientAllInfo', null, 'JSON', function (data) {
        if (data) {
            $scope.PatientInfo = data;
            if ($scope.PatientInfo.SmokingHisType == '1')
                $scope.SmokingHisType = "不吸烟";
            else if ($scope.PatientInfo.SmokingHisType == '2')
                $scope.SmokingHisType = "吸烟" + $scope.PatientInfo.SmokingHis + "年";
            else if ($scope.PatientInfo.SmokingHisType == '3')
                $scope.SmokingHisType = "已戒烟" + $scope.PatientInfo.SmokingHis + "年";

        }
    }, null);
}