initTable();

//生成用户数据
function initTable() {
    $('#ActionTypeTable').bootstrapTable({
        pagination: true,
        striped: true, //是否显示行间隔色
        pageNumber: 1,
        pageSize: 10,
        pageList: [5, 10, 20, 50],
        height: getClientHeight() - 150,
        columns: [{
            title: $("#T_Name").val(),
            field: 'EventTypeName'
        }, {
            title: $("#T_EnName").val(),
            field: 'EventTypeNameEn'
        }, {
            title: $("#T_ActivityMode").val(),
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
            title: $("#T_AreaName").val(),
            field: 'AreaName'
        }, {
            title: $("#T_MaximumAmount").val(),
            field: 'ApprovalMaxAmt'
        }, {
            title: $("#T_State").val(),
            field: 'ShowStatus',
            formatter: function (value) {
                if (value) {
                    return '<span class="label label-success">' + $('#G_Show').val() + '</span>';
                } else {
                    return '<span class="label label-important">' + $('#G_NoShow').val() + '</span>';
                }
            }
        }, {
            title: $("#G_Edit").val(),
            field: "EventTypeId",
            formatter: function (value) {
                var e = '<a href="/Home/ActionTypeEdit?Id=' + value + '" class="btn btn-link" style="width:auto !important"><i class="icon-pencil icon-white"></i>' + $('#G_Edit').val() + '</a>';
                return e;
            }
        }],
        onPageChange: function (number, size) {
            window.localStorage.pageNumberMA = number;
        }
    });
}
