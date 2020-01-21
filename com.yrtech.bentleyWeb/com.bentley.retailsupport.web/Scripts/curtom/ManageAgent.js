
//生成用户数据
$('#AgentTable').bootstrapTable({
    pagination: true,
    striped: true, //是否显示行间隔色
    pageNumber: 1,
    pageSize: 10,
    pageList: [5, 10, 20, 50],
    columns: [{
        title: "{{'Name'|translate}}",
        field: 'DealerName'
    }, {
        title: "{{'EnName'|translate}}",
        field: 'DealerEnName'
    }, {
        title: "{{'City'|translate}}",
        field: 'City'
    }, {
        title: "{{'Budget'|translate}}",
        field: 'Budget'
    }, {
        title: "{{'Balance'|translate}}",
        field: 'Balance'
    }, {
        title: "{{'AreaName'|translate}}",
        field: "AreaName"
    }, {
        title: "{{'Edit'|translate}}",
        field: "ShopId",
        formatter: function (value) {
            var e = '<a href="/Home/AgentEdit?Id=' + value + '" class="btn btn-link" style="width:auto !important"><i class="icon-pencil icon-white"></i>' + $('#G_Edit').val() + '</a>';
            e += '<a href="/Home/AgentDelete?Id=' + value + '" class="btn btn-link" style="width:auto !important"><i class="icon-remove icon-white"></i>' + $('#G_Delete').val() + '</a>';
            return e;
        }
    }
    ]
});