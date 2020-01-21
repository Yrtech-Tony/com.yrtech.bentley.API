var $table = $('#myBudgetCost');
var role = $('#G_RoleName').val();

var dealNumber = function (money) {
    if (money && money != null) {
        money = String(money);
        var left = money.split('.')[0], right = money.split('.')[1];
        right = right ? (right.length >= 2 ? '.' + right.substr(0, 2) : '.' + right + '0') : '.00';
        var temp = left.split('').reverse().join('').match(/(\d{1,3})/g);
        return (Number(money) < 0 ? "-" : "") + temp.join(',').split('').reverse().join('') + right;
    } else if (money === 0) {   //注意===在这里的使用，如果传入的money为0,if中会将其判定为boolean类型，故而要另外做===判断
        return '0.00';
    } else {
        return "";
    }
};

/*将100,000.00转为100000形式*/
var undoNubmer = function (money) {
    if (money && money != null) {
        money = String(money);
        var group = money.split('.');
        var left = group[0].split(',').join('');
        return Number(left + "." + group[1]);
    } else {
        return "";
    }
};

function InitMarketFundLst() {
    //生成用户数据
    $('#myBudgetCost').bootstrapTable({
        pagination: true,
        striped: true, //是否显示行间隔色
        height: getClientHeight() - 280 + 80,
        showColumns: false, // 开启自定义列显示功能
        sortable: true,
        sortName: 'ShopId',
        sortOrder: 'asc',
        pageNumber: 1,
        pageSize: 15,
        pageList: [5, 10, 20, 50],
        columns: [{
            title: "{{'NO'|translate}}",
            field: 'ShopId',
            width: 30,
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                return index + 1;
            }
        }, {
            title: "{{'Dealer'|translate}}",
            field: "ShopName",
            width: "400px",
            valign: "middle",
            align: "center",
            sortable: false,
            formatter: function (value, row, index) {
                var name = isZH() ? row.ShopName : row.ShopNameEn;
                return '<a href="javascript:return false;" style="min-width:100px" onclick="GoDetail(' + row.ShopId + ')">' + name + '</a>';
            }
        }, {
            title: "{{'Year'|translate}}",
            field: "Quarters",
            width: "400px",
            valign: "middle",
            align: "center",
            sortable: false
                ,
            formatter: function (value, row, index) {
                return value;
            }
        }, {
            title: "{{'AAH'|translate}}",
            field: "ActualMonthSaleCount",
            width: "400px",
            valign: "middle",
            align: "center",
            sortable: false
                ,
            formatter: function (value, row, index) {
                return value;
            }
        }, {
            title: "{{'0.8% MKT Fund (RMB)'|translate}}",
            field: "ActualMonthSaleAmt",
            width: "400px",
            valign: "middle",
            align: "center",
            sortable: false
                ,
            formatter: function (value, row, index) {
                return dealNumber(value);
            }
        }, {
            title: "{{'Actual Expense (RMB)'|translate}}",
            field: "ActualAmt",
            width: "400px",
            valign: "middle",
            align: "center",
            sortable: false
                ,
            formatter: function (value, row, index) {
                return dealNumber(value);
            }
        }, {
            title: "{{'Balance(RMB)'|translate}}",
            field: "DiffAmt",
            width: "400px",
            valign: "middle",
            align: "center",
            sortable: false
                ,
            formatter: function (value, row, index) {
                return dealNumber(value);
            }
        }],
        onClickCell: function (field, value, row, $element) {
            return false;
        },
        onClickRow: function (row, $element) {
            curRow = row;
        }
    });
    HideColumn();
}

function GoDetail(ShopId) {
    window.location.href = "/DMF/DMFDetail?ShopId=" + ShopId;
}


function EmptyValue() {
    window.localStorage.Empty = "true";
}

function HideColumn() {
    $table.bootstrapTable('hideColumn', 'Season');
}

/*
 取窗口可视范围的高度 
*/
function getClientHeight() {
    var clientHeight = 0;
    if (document.body.clientHeight && document.documentElement.clientHeight) {
        clientHeight = document.documentElement.clientHeight;
    }
    else {
        clientHeight = document.documentElement.clientHeight;
    }
    return clientHeight;
}