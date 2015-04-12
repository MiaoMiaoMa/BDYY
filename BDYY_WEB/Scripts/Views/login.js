$(document).ready(function () {
    $("body").keydown(function (e) {
        if (e.keyCode == 13) {
            login();
        }
    });
    var t = getRandomCode();
    $("#vcode").html(t);
    $("#valcode").val(t);
})

function login() {
    var uid = $("#uid").val();
    var pwd = $("#pwd").val();
    var vcodeInput = $("#valcode").val();
    var vcode = $("#vcode").html();
    if ($.trim(uid) == "") {
        $("#uid").focus();
    }
    else if ($.trim(pwd) == "") {
        $("#pwd").focus();
    }
    else if ($.trim(vcodeInput) == "" || vcodeInput.toLowerCase() != vcode.toLowerCase()) {
        $("#valcode").focus();
    }
    else {
        var dataObj = { uid: $("#uid").val(), pwd: $("#pwd").val() }
        callAJAX('../Home/LoginValidate', dataObj, 'JSON', function (data) {
            if (data.IsSuccess) {
                window.location.href = '../Account/AccountInfo';
            }
            else {
                alert("账户名或密码错误！");
            }
        }, null);
    }
}

function getValidationCode()
{
    var t = getRandomCode();
    $("#vcode").html(t);
}

function updatePWD()
{
    if ($.trim($('#oldpwd').val()) == "") {
        $('#oldpwd').focus();
    }
    else if ($.trim($('#newpwd').val()) == "") {
        $('#newpwd').focus();
    }
    else if ($.trim($('#newpwd2').val()) == "" || $('#newpwd').val != $('#newpwd2')) {
        $('#newpwd2').focus();
    }
    else {
        var dataObj = { newpwd: $('#newpwd').val() };
        callAJAX('../Account/UpdatePWD', dataObj, 'JSON', function (data) {
            if (data.IsSuccess) {
                alert("修改成功！");
            }            
        }, null);
    }
}