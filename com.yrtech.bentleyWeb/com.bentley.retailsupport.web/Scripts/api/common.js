
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
        console.log(url + " execute error ");
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


function loadActiveStatuss() {
    $.commonGet("Master/HiddenCodeSearch", {
        hiddenCodeGroup: "MarketActionStatus",
        hiddenCode: ""
    }, function (data) {
        if ($("#MarketActionStatusCode").length > 0) {
            data.forEach(function (item) {
                $("#MarketActionStatusCode").append($("<option>").val(item.HiddenCodeId).text(isZH() ? item.HiddenCodeName : item.HiddenCodeNameEn));
            });
            $("#MarketActionStatusCode").selectpicker("refresh");
        }
        if ($("#pstate").length > 0) {
            data.forEach(function (item) {
                $("#pstate").append($("<option>").val(item.HiddenCodeId).text(isZH() ? item.HiddenCodeName : item.HiddenCodeNameEn));
            });
        }
    })
}

function loadTargetModels() {
    $.commonGet("Master/HiddenCodeSearch", {
        hiddenCodeGroup: "TargetModels",
        hiddenCode: ""
    }, function (data) {
        if ($("#MarketActionTargetModelCode").length > 0) {
            data.forEach(function (item) {
                $("#MarketActionTargetModelCode").append($("<option>").val(item.HiddenCodeId).text(isZH() ? item.HiddenCodeName : item.HiddenCodeNameEn))
            });
            $("#MarketActionTargetModelCode").selectpicker("refresh");
        }
    })
}

function loadEventTypes() {
    $.commonGet("Master/EventTypeSearch", {
        eventTypeId: "",
        eventTypeName: "",
        eventTypeNameEn: "",
        showStatus: 'true'
    }, function (data) {
        if ($("#EventTypeId").length > 0) {
            data.forEach(function (item) {
                $("#EventTypeId").append($("<option>").val(item.EventTypeId).text(isZH() ? item.EventTypeName : item.EventTypeNameEn));
            });
            $("#EventTypeId").selectpicker("refresh");
        }
        if ($("#ptype").length > 0) {
            data.forEach(function (item) {
                $("#ptype").append($("<option>").val(item.EventTypeId).text(isZH() ? item.EventTypeName : item.EventTypeNameEn));
            });
        }
    })
}
