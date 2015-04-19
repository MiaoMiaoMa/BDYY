//Controller for 用户基本信息页
function ApllyFor($scope, $window) {
    callAJAX('../Account/GetApplyFor', null, 'JSON', function (data) {
        if (data) {
            $scope.patientStatus = data.patientStatus;
            $scope.PatientOtherInfo = data.patientOtherInfo;
        }
    }, null);
    if ($scope.patientStatus != '3') {
        $("#submit").removeClass("btnsave").attr("disabled", "disabled");
    }
    else {
        $("#submit").addClass("btnsave").removeAttr("disabled");
    }

    $scope.save = function () {
        var patientData = { data: JSON.stringify($scope.PatientOtherInfo) };
        callAJAX('../Account/AddApplyFor', patientData, 'JSON', function (data) {
            if (data.IsSuccess) {
                $window.location.href = "../Account/ApplyStatusCheck";
            }
            else {
                if (data.errormsg != "")
                {
                    $window.alert(data.errormsg);
                }
            }
        }, null);
    }
}

function ApplyForAgain($scope, $window)
{
    $scope.EMSNumber = "";

    $scope.add = function () {
        callAJAX('../Account/AddApplyForAgain?emsNumber=' + $scope.EMSNumber, null, 'JSON', function (data) {
            if (data.IsSuccess) {
                $window.location.href = "../Account/ApplyStatusCheck";
            }
            else {
                if (data.errormsg != "") {
                    $window.alert(data.errormsg);
                }
            }
        }, null);
    }
}

function ApplyStatusCheck($scope)
{
    callAJAX('../Account/GetStatusCheck', null, 'JSON', function (data) {
        $scope.applyStatus = data;
    }, null);
}