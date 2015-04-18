// JavaScript Document
//加载判断文档高度，确定脚的位置
$(document).ready(function () {
    var documentheight = $(document.body).height();
    var windowheight = $(window).height();
    var foottop = windowheight - documentheight;
    if (foottop > 30) {
        var footmargintop = windowheight - 30;
        $(".mainfoot").offset({ top: footmargintop });
    }
    var minHeadShow = getStorage("minHeadShow");
    if (minHeadShow == "true") {
        $('.mainhead,.mainfoot').css("display", "none");
        $('.minhead').css("display", "block");
    }
    else {
        $('.mainhead,.mainfoot').css("display", "block");
        $('.minhead').css("display", "none");
    }
});
$(window).resize(function () {
    var documentheight = $(document.body).height();
    var windowheight = $(window).height();
    var mainheight = $(".mainbox").height();
    var footrealtop = mainheight + 110
    $(".mainfoot").offset({ top: footrealtop });
    var foottop = windowheight - documentheight;
    if (foottop > 30) {
        var footmargintop = windowheight - 30;
        $(".mainfoot").offset({ top: footmargintop });
    }
})

//搜索中选择样式切换
$(".itemtab").click(function () {
    $(this).siblings().removeClass("selectsearchitem");
    $(this).siblings().addClass("searchitem");
    $(this).addClass("selectsearchitem");
});

//日期选择样式切换
$(".itemcheck").click(function () {
    if ($(this).hasClass("selectsearchitem")) {
        $(this).removeClass("selectsearchitem");
        $(this).addClass("searchitem");
    } else {
        $(this).removeClass("searchitem");
        $(this).addClass("selectsearchitem");
    }
});

//高级搜索打开隐藏
function searchadvanced() {
    var saerchAdSt = $('.serach_advanced').css("display")
    if (saerchAdSt == "none") {
        $('.serach_advanced').css("display", "block")
        $(".bt_advanced").val("隐藏高级")
    } else {
        $('.serach_advanced').css("display", "none")
        $(".bt_advanced").val("高级")
    }
}

//行点击打开追踪详情
$('table#track tr').click(function () {
    //alert($(this).index())
    window.location.href = '#'
});

//全屏
$('.Screen').click(function () {
    var mainhead = $('.mainhead').css("display")
    var minhead = $('.minhead').css("display")
    if (mainhead == "block") {
        $('.mainhead,.mainfoot').css("display", "none");
        $('.minhead').css("display", "block");
        clearStorage("minHeadShow");
        setStorage("minHeadShow", "true");
    }
    if (minhead == "block") {
        $('.mainhead,.mainfoot').css("display", "block");
        $('.minhead').css("display", "none");
        clearStorage("minHeadShow");
        setStorage("minHeadShow", "false");
    }
});


//控件激活换样式
$(function () {
    $(".txt_box,.txt_area").focus(function () {
        $(this).addClass("focus");
    }).blur(function () {
        $(this).removeClass("focus");
    });
})

function indexjump(ajaxlink, navlink) {
    window.open(ajaxlink, '_self')
    $.ajax({
        url: navlink,
        dataType: 'html',
        success: function (data) {
            $('#mainajax').html(data);
        }
    });
};

function getStorage(name) {
    if (window.localStorage) {
        return localStorage[name];
    }
}

function setStorage(name, value) {
    if (window.localStorage) {
        localStorage[name] = value;
    }
}

function clearStorage(name) {
    if (window.localStorage) {
        localStorage.removeItem(name);
    }
}