
//生成用户数据
$('#AgentTable').bootstrapTable({
    pagination: true,
    striped: true, //是否显示行间隔色
    pageNumber: 1,
    pageSize: 10,
    height: getClientHeight() - 150,
    pageList: [5, 10, 20, 50],
    columns: [{
        title: "{{'Name'|translate}}",
        field: 'ShopName'
    }, {
        title: "{{'EnName'|translate}}",
        field: 'ShopNameEn'
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
        formatter: function (value, row) {
            var e = '<a href="/Home/AgentEdit?Id=' + value + '" class="btn btn-link" style="width:auto !important"><i class="icon-pencil icon-white"></i>' + $('#G_Edit').val() + '</a>';
            e += '<a href="javascript:delShop(' + value + ')" class="btn btn-link" style="width:auto !important"><i class="icon-remove icon-white"></i>' + $('#G_Delete').val() + '</a>';
            return e;
        }
    }],
    onPageChange: function (number, size) {
        window.localStorage.pageNumberAgent = number;
    }
});

function delShop(id) {
    if (window.confirm('确实要删除该经销商吗？')) {
        $.commonPost("Master/ShopDelete", {
            ListJson: JSON.stringify([{ ShopId: id }])
        }, function () {
            console.log('删除经销商数据成功');
            loadAgent();
        });
    }
}