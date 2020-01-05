
/**
API 公用方法
需要引用jquery
*/
var host = "http://39.106.71.65:8001/"
var baseApi = "http://39.106.71.65:8001/bentley/api/";
//var baseApi = "http://localhost:57328/bentley/api/";


$.commonGet = function (url, params, callback) {
    $.get(baseApi + url, params, function (data) {       
        if (data && data.Status) {
            if (data.Body) {
                var lst = JSON.parse(data.Body);
                if (callback) {
                    callback(lst);
                }
            } else {
                if (callback) {
                    callback();
                }
            }
        } else {
            console.log(url + " execute error " + data.Body);
            layer.alert(data.Body);
        }
    }).error(function (jqXHR, textStatus, errorThrown) {
        console.log(url+" execute error ");
    })
}

$.commonPost = function (url, params, callback) {
    $.post(baseApi + url, params, function (data) {
        if (data && data.Status) {
            if (data.Body) {
                var lst = JSON.parse(data.Body);
                if (callback) {
                    callback(lst);
                }
            } else {
                if (callback) {
                    callback();
                }
            }
        } else {
            console.log(url + " execute error " + data.Body);
            layer.alert(data.Body);
        }
    }).error(function (jqXHR, textStatus, errorThrown) {
        console.log(url + " execute error ");
    })
}