$(document).ready(function () {
    $("body").keydown(function (e) {
        if (e.keyCode == 13) {
            if (window.location.href.toLowerCase().indexOf("admin") > 0)
                login('admin');
            else
                login();
        }
    });
    var t = getRandomCode();
    $("#vcode").html(t);
    $("#valcode").val(t);
})

function login(logintype) {
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
        var url = '../Home/LoginValidate';
        var successurl = '../Account/AccountInfo';
        if (typeof logintype != "undefined" && logintype == "admin")
        {
            url = '../Admin/AdminLoginValidate';
            successurl = '../Admin/Default';
        }
        callAJAX(url, dataObj, 'JSON', function (data) {
            if (data.IsSuccess) {
                window.location.href = successurl;
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