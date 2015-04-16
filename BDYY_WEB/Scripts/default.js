var app = angular.module('mainApp', []);
var PAGESIZE = 10;
//分页
app.filter('paging', function () {
    return function (input, start, end) {
        if (!input) return input;
        start = +start; //parse to int
        var result = [];
        for (var i = start; i < input.length; i++) {
            result.push(input[i]);
        }
        return result;
    }
});

app.factory('MyFactory', function ($http) {    
    return {
        callAJAX : function callAJAX(url, data, dataType, callbackSuccess, callbackError)
        {
            $.ajax({
                async: false,
                cache: false,
                url: url,
                data: data,
                dataType: dataType,//'JSON'
                success: function (data) {
                    if (typeof callbackSuccess == "function") {
                        callbackSuccess(data);
                    }
                },
                error: function (data) {
                    if (typeof callbackError == "function") {
                        callbackError(data);
                    }
                }
            });
        },
        showSmokingStr: function (SmokingHisType, SmokingHis) {
            var str = "不吸烟";
            if (SmokingHisType == '1')
                str = "不吸烟";
            else if (SmokingHisType == '2')
                str = "吸烟" + SmokingHis + "年";
            else if (SmokingHisType == '3')
                str = "已戒烟" + SmokingHis + "年";
            return str;
        },
        showDialog2: function (divid, tableid, divtitle) {
            var html = '<div id="' + divid + '" style="display:none">' +
        '<div class="bg"></div>' +
        '<div style="width: 750px; height: 550px; left: 292.5px; top: -257px; display: block;" class="pop_up_box">' +
        '<div class="pop_up_contnet">' +
            '<div class="pop_up_contnet_top">' +
                '<div class="edialogheaderbg_l">' +
                    '<div class="edialogheaderbg_r">' +
                        '<div class="edialogheadertitle" name="DragTitle">' + divtitle + '</div>' +
                        '<div click="closeSE()" class="edialogclose" title="点此关闭"></div>' +
                    '</div>' +
                '</div>' +
            '</div> ' +
            '<div class="Guide_content">' +
                '<div class="edialogbodybg_l">' +
                    '<div class="edialogbodybg_r">' +
                        '<div class="edialogbody">' +
                                '<div class="l_height_30 mr_10" class="l_height_30 mr_10">' +
                                      '<div class="mt_5  gridiframe border_t_n" style="overflow-y:scroll; max-height:350px;">   ' +
                                      '</div>'+
                                        '</div>'+
                                    '</div>  '+
                                '</div>'+
                          '</div>'+
                        '</div>'+
                    '</div>'+
                '</div>'+
                '<div class="edialogfooterbg_l">'+
				    '<div class="edialogfooterbg_r">'+
					    '<div class="edialogfooterbg"></div>'+
				    '</div>'+
			    '</div>'+
            '</div>';
            $('#'+tableid).wrap(function () {
                return html;
            });

            var bh = $(document).height();
            var bw = $(window).width();
            var wh = $(window).height();
            var popwidth = 900;
            var popheight = 450;
            var v_left = (bw - popwidth) / 2;
            var v_top = bh > popheight ? (bh - popheight) / 2 : 0;
            
            $(".bg").css({ width: bw, height: bh, display: "block" })
            $('.pop_up_box').css({ width: popwidth, height: popheight });
            $('.pop_up_box').css({ left: v_left, top: v_top, display: "block" });
            $("#" + divid).show();

        }
    }
});

//日期控件
app.directive('datepickers', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            $(function () {
                element.datetimepicker({
                    dateFormat: 'yy-mm-dd',
                    showTimepicker: false,
                    onSelect: function (date) {
                        scope.$apply(function () {
                            ngModelCtrl.$setViewValue(date);
                        });
                    }
                });
            });
        }
    }

});


function callAJAX(url, data, dataType, callbackSuccess, callbackError)
{
    $.ajax({
        async: false,
        cache: false,
        url: url,
        data: data,
        dataType: dataType,//'JSON'
        success: function (data) {
            if (typeof callbackSuccess == "function") {
                callbackSuccess(data);
            }
        },
        error: function (data) {
            if (typeof callbackError == "function") {
                callbackError(data);
            }
        }
    });
}

function callPostAJAX(url, data, dataType, callbackSuccess, callbackError) {
    $.ajax({
        async: false,
        cache: false,
        type: 'POST',
        url: url,
        data: data,
        dataType: dataType,
        success: function (data) {
            if (typeof callbackSuccess == "function") {
                callbackSuccess(data);
            }
        },
        error: function (data) {
            if (typeof callbackError == "function") {
                callbackError(data);
            }
        }
    });
}

//关闭弹出窗口
function closeDialog(divid)
{
    $("#" + divid).hide();
    $(".bg").css({ display: "none" })
    $('.pop_up_box').css({ display: "none" });
}

//关闭弹出
function closeBg() {
    $(".pop_up_Iframe,.bg,.pop_up_box").remove();
}