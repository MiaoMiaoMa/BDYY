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