//生成用户数据
$('#AreaTable').bootstrapTable({
    pagination: true,
    pageNumber: 1,
    height: getClientHeight() - 160,
    striped: true, //是否显示行间隔色
    columns: [
    {
        title: "{{'Name'|translate}}",
        field: 'AreaName'
    },
    {
        title: "{{'EnName'|translate}}",
        field: 'AreaNameEn'
    },
    {
        title: "{{'Edit'|translate}}",
        field: "AreaId",
        formatter: function (value, row, index) {
            var areaName = row.AreaName;
            var e = '<a href="/Home/AreaEdit?Id=' + value + '" class="btn btn-link" style="width:auto !important"><i class="icon-pencil icon-white"></i>' + $('#G_Edit').val() + '</a>';
            e += '<a onclick="delArea(' + row.AreaId + ')" class="btn btn-link" style="width:auto !important"><i class="icon-remove icon-white"></i>' + $('#G_Delete').val() + '</a>';
            return e;
        }
    }]
});
function delArea(id) {
    if (window.confirm('确实要删除该内容吗？')) {
        $.commonPost("Master/AreaDelete", {
            ListJson: JSON.stringify([{ AreaId: id }])
        }, function () {
            console.log('删除区域数据成功');
            loadArea();
        });
    }
}