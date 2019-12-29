
/**
API 公用方法
需要引用jquery
*/
var baseApi = "http://39.106.71.65:8001/bentley/api/";

$.commonGet = function (url, params, callback) {
    $.get(baseApi + url, params, function (data) {       
        if (data && data.Status) {
            var lst = JSON.parse( data.Body);
            if (callback) {
                callback(lst);
            }
        } else {
            console.log(url + " execute error " + data.Body);
            alert(data.Body);
        }
    }).error(function (jqXHR, textStatus, errorThrown) {
        debugger
        console.log(url+" execute error ");
    })
}

$.commonPost = function (url, params, callback) {
    $.post(baseApi + url, params, function (data) {
        if (data && data.Status) {
            var lst = JSON.parse(data.Body);
            if (callback) {
                callback(lst);
            }
        } else {
            console.log(url + " execute error " + data.Body);
            alert(data.Body);
        }
    }).error(function (jqXHR, textStatus, errorThrown) {
        debugger
        console.log(url + " execute error ");
    })
}