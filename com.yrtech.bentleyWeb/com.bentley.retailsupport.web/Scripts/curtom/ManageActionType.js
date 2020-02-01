
//生成用户数据
$('#ActionTypeTable').bootstrapTable({
    pagination: true,
    striped: true, //是否显示行间隔色
    pageNumber: 1,
    pageSize: 10,
    pageList: [5, 10, 20, 50],
    columns: [{
        title: "{{'Name'|translate}}",
        field: 'EventTypeName'
    }, {
        title: "{{'EnName'|translate}}",
        field: 'EventTypeNameEn'
    }, {
        title: "{{'Activity Mode'|translate}}",
        field: 'EventMode',
        formatter: function (value) {
            var e = $('#G_Online').val(); //在线活动
            if (value == 2)//线下活动
            {
                e = $('#G_Offline').val();
            }
            return e;
        }
    }, {
        title: "{{'AreaName'|translate}}",
        field: 'AreaName'
    }, {
        title: "{{'MaximumAmount'|translate}}",
        field: 'ApprovalMaxAmt'
    }, {
        title: "{{'State'|translate}}",
        field: 'ShowStatus',
        formatter: function (value) {
            if (value) {
                return '<span class="label label-success">' + $('#G_Show').val() + '</span>';
            } else {
                return '<span class="label label-important">' + $('#G_NoShow').val() + '</span>';
            }
        }
    }, {
        title: "{{'Edit'|translate}}",
        field: "EventTypeId",
        formatter: function (value) {
            var e = '<a href="/Home/ActionTypeEdit?Id=' + value + '" class="btn btn-link" style="width:auto !important"><i class="icon-pencil icon-white"></i>' + $('#G_Edit').val() + '</a>';
            return e;
        }
    }]
});