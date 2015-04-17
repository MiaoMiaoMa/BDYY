
//Controller for 添加预约
function AddPatientBasicInfo($scope, $window)
{
    callAJAX('../Home/GetPatientInfoEmptyModel', null, 'JSON', function (data) {
        if (data) {
            $scope.PatientInfo = data.patient;
        }
    }, null);

    $scope.Submit = function () {

        patientData = angular.copy($scope.PatientInfo);

        if (!check(patientData.Birthday)) {
            //alert(patientData.MobilPhoneNumber);
            alert("请填写正确的出生日期！");
            $("#textfield2").focus();
            return;
        }
        if (!checkMobile(patientData.MobilPhoneNumber)) {
            //alert(patientData.MobilPhoneNumber);
            alert("请填写正确的手机号！");
            $("#textfield4").focus();
            return;
        }
        if (patientData.IdentityType == "1" && !IdCardValidate(patientData.IdentityNumber)) {
            alert("身份证输入不合法");
            $("#textfield3").focus();
            return;
        }
        if ($("#indetityNumber2").val() != patientData.IdentityNumber) {
            alert("请确认两次身份证号码输入一致！");
            $("#indetityNumber2").focus();
            return;
        }
        if (patientData.Province == '0') {
            alert("请选择省份！");
            $("#to_cn").focus();
            return;
        }
        patientData = angular.copy($scope.PatientInfo);
        if (patientData.City == '0') {
            alert("请选择城市！");
            $("#city").focus();
            return;
        }
        if (!check(patientData.FirstUseDate)) {
            alert("请填写正确的首次用药时间！");
            $("#textfield9").focus();
            return;
        }
        if ($("#gzs").attr("checked") != "checked") {
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
        //$("#savebutton").hidden();
        
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
function PatientInfo($scope, MyFactory) {
    $scope.SmokingHisType = "不吸烟";
    MyFactory.callAJAX('../Account/GetPatientAllInfo', null, 'JSON', function (data) {
        if (data) {
            $scope.PatientInfo = data;
            $scope.SmokingHisType = MyFactory.showSmokingStr($scope.PatientInfo.SmokingHisType, $scope.PatientInfo.SmokingHis);
        }
    }, null);
}