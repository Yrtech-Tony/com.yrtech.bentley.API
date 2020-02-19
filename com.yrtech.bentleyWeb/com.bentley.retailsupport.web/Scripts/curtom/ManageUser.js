
//生成用户数据
$('#userTable').bootstrapTable({
    pagination: true,
    striped: true, //是否显示行间隔色
    pageNumber: 1,
    pageSize: 10,
    pageList: [5, 10, 20, 50],
    height: getClientHeight() - 150,
    columns: [{
        title: "{{'Account'|translate}}",
        field: 'AccountId'
    }, {
        title: "{{'Name'|translate}}",
        field: 'AccountName'
    }, {
        title: "{{'Phone'|translate}}",
        field: 'TelNO'
    }, {
        title: "{{'Email'|translate}}",
        field: 'Email'
    }, {
        title: "{{'Role'|translate}}",
        field: "RoleTypeName"
    }, {
        title: "{{'Dealer'|translate}}",
        field: "ShopName"
    }, {
        title: "{{'AreaName'|translate}}",
        field: "AreaName"
    }, {
        title: "{{'Edit'|translate}}",
        field: "UserId",
        formatter: function (value) {
            var e = '<a href="/Account/UserEdit?Id=' + value + '" class="btn btn-link" style="width:auto !important"><i class="icon-pencil icon-white"></i>' + $('#G_Edit').val() + '</a>';
            e += '<a href="javascript:del(' + value + ')" class="btn btn-link" style="width:auto !important"><i class="icon-remove icon-white"></i>' + $('#G_Delete').val() + '</a>';
            //e += '<a href="/Account/ResetToPassword?Id=' + value + '" onclick="return reset_confirm()" class="btn btn-link" style="width:auto !important"><i class="fa fa-key"></i>' + $('#G_ResetPassword').val() + '</a>';
            return e;
        }
    }],
    onPageChange: function (number, size) {
        window.localStorage.pageNumberUser = number;
    }
});

function del(id) {
    var rows = [{ UserId: id }];
    $.commonPost("Master/UserInfoDelete", {
        ListJson:JSON.stringify( rows)
    }, function () {
        loadUser();
    })
}
function reset_confirm() {
    if (window.confirm('确定要重置此用户的密码吗？')) {
        //alert("确定");
        return true;
    } else {
        //alert("取消");
        return false;
    }
}