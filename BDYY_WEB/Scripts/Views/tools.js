function getURLParas(url) {
    var URLParams = new Object();
    if (url.indexOf("?") >= 0) {
        var aParams = url.substr(1).split('&');
        for (i = 0 ; i < aParams.length ; i++) {
            var aParam = aParams[i].split('=');
            URLParams[aParam[0]] = aParam[1];
        }
    }
    return URLParams;
}

function getRandomCode() {
    var str = "ABCDEFGHIGKLMNOPQRSTUVWXYZabcdefghigklmnopqrstuvwxyz0123456789";
    var s = str.split("");
    var t = "";
    for (var i = 0; i < 4; i++) {
        t += s[getRandomNum(1, 62)];
    }
    return t;
}

function getRandomNum(lbound, ubound) {
    return (Math.floor(Math.random() * (ubound - lbound)) + lbound);
}

function isCardNo(card) {
    // 身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X  
    var reg = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;
    if (reg.test(card) === false) {
        alert("身份证输入不合法");
        return false;
    }
    return true;
}