
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

var curRow;
function InitIncomeCostList() {
    //生成用户数据
    $('#myIncomeCost').bootstrapTable({
        pagination: true,
        striped: true, //是否显示行间隔色
        height: getClientHeight() - 280 + 80,
        showColumns: false, // 开启自定义列显示功能
        sortable: true,
        sortName: 'MonthSaleId',
        sortOrder: 'asc',
        pageNumber: 1,
        pageSize: 10,
        pageList: [5, 10, 20, 50],
        columns: [{
            checkbox: true,
            valign: "middle",
            align: "center"
        }, {
            title: $('#NO').val(),
            field: 'MonthSaleId',
            width: 30,
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                return index + 1;
            }
        }, {
            title: $('#YearMonth').val(),
            field: "YearMonth",
            width: "300px",
            valign: "middle",
            align: "center",
            sortable: false,
            editable: {
                type: 'text',
                //title: $('#YearMonth').val(),
                validate: function (v) {
                    v = $.trim(v);
                    if (!v) {
                        return isZH() ? '预算不能为空，且必须是“月份/年”' : "The budget cannot be empty and must be \"month/year\"";
                    }

                    if (v.length >= 7) {
                        var test = v.split('/');
                        try {
                            new Date(eval(test[1]), eval(test[0]) - 1, 0);
                        } catch (err) {
                            return isZH() ? '必须是“月份/年,例如：12/2017”' : "Must be \"month/year, e.g. 12/2017\"";
                        }
                    }
                    else { return isZH() ? '必须是“月份/年,例如：12/2017”' : "Must be \"month/year, e.g. 12/2017\"" }
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "Remark", value: value };
                    if (roleType != "SHOP") {
                        var html = '<a href="javascript:void(0)" data-name="YearMonth" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                        if (result.value == "") {
                            html = '<a href="javascript:void(0)" data-name="YearMonth" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                        }
                        return html;
                    } else { return result.value; }
                }
            }
        }, {
            title: $('#Dealer').val(),
            field: 'ShopId',
            width: "300px",
            valign: "middle",
            align: "center",
            sortable: false,
            align: 'left',
            formatter: function (value, row, index) {
                var name = isZH() ? row.ShopName : row.ShopNameEn;
                return '<div style="min-width:100px">' + name + '</div>';
            },
            editable: {
                type: 'select',
                source: hdData,
                validate: function (v) {
                    if (!v) return isZH() ? '经销商不能为空' : 'The dealer cannot be empty.';
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "ShopId", value: value };
                    var real = '';
                    for (var i = 0; i < hdData.length; i++) {
                        if (hdData[i].value == value) {
                            real = hdData[i].text;
                        }
                    }
                    if (roleType != "SHOP") {
                        var html = '<a href="javascript:void(0)" data-name="ShopId" data-pk="undefined" data-value="" class="editable editable-click">' + real + '</a>';
                        if (!result.value) {
                            html = '<a href="javascript:void(0)" data-name="ShopId" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                        }
                        return html;
                    } else { return real; }
                }
            }
        }, {
            title: $('#AAH').val(),
            field: "ActualSaleCount",
            valign: "middle",
            align: "center",
            editable: {
                type: 'text',
                //title: aah,
                validate: function (v) {
                    v = $.trim(v);
                    var vil = isZH() ? '实际批售不能为空，且必须是数字' : 'The actual sale cannot be null, and only numbers accepted.';
                    if (!v) {
                        return vil;
                    }
                    if (!/^(-?\d+)(\.\d+)?$/.test(v)) {
                        return vil;
                    }
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "ActualSaleCount", value: value };
                    if (roleType != "SHOP") {
                        var html = '<a href="javascript:void(0)" data-name="ActualSaleCount" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                        if (!result.value) {
                            html = '<a href="javascript:void(0)" data-name="ActualSaleCount" data-pk="undefined" data-value="" class="editable editable-click">0</a>';
                        }
                        return html;
                    } else { return result.value; }
                }
            }
        }, {
            title: $('#RMB').val(),
            valign: "middle",
            align: "center",
            field: "ActualSaleAmt",
            editable: {
                type: 'text',
                validate: function (v) {
                    v = $.trim(v);
                    var vil = isZH() ? '金额不能为空，且必须是数字' : 'The XXX cannot be null, and only numbers accepted.';
                    if (!v) {
                        return vil;
                    }
                    if (!/^(-?\d+)(\.\d+)?$/.test(v)) {
                        return vil;
                    }
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "ActualSaleAmt", value: value };
                    if (roleType != "SHOP") {
                        var html = '<a href="javascript:void(0)" data-name="ActualSaleAmt" data-pk="undefined" data-value="" class="editable editable-click">' + dealNumber(result.value) + '</a>';
                        if (result.value == "") {
                            html = '<a href="javascript:void(0)" data-name="ActualSaleAmt" data-pk="undefined" data-value="" class="editable editable-click">0</a>';
                        }
                        return html;
                    } else {
                        return dealNumber(result.value);
                    }
                }
            }
        }],
        onClickCell: function (field, value, row, $element) {
            return false;
        },
        onClickRow: function (row, $element) {
            curRow = row;
        },
        onLoadSuccess: function (data) {
        },
        onEditableSave: function (field, row, oldValue, $el) {
            if (row.YearMonth && row.ShopId) {
                row.InUserId = $("#G_UserId").val();
                row.ModifyUserId = $("#G_UserId").val();
                row.ActualSaleCount = DESEncrypt(row.ActualSaleCount);
                row.ActualSaleAmt = DESEncrypt(row.ActualSaleAmt);
                $.commonPost("DMF/MonthSaleSave", row, function (data) {
                    if (data) {
                        row.MonthSaleId = data.MonthSaleId;
                    }
                    curRow = row;
                    IsEdit = true;
                    loadMonthSale();
                }, function (msg) {
                    var error = isZH() ? '编辑失败!' : 'Edit failure!';
                    error += (msg || '')
                    layer.alert(error);
                })
            }
        }
    });
}

function DeleteBudgetCost() {
    var rows = $table.bootstrapTable('getSelections');
    if (rows.length == 0) {
        layer.alert(isZH() ? "请选择需要删除的行!" : "Please select rows to delete!");
        return;
    }

    $.commonPost("DMF/MonthSaleDelete", {
        ListJson: JSON.stringify(rows)
    }, function () {
        console.log('删除数据成功');
        loadMonthSale();
    });
}

function Add() {
    var index = $table.bootstrapTable('getData').length;
    var newRow = {
        MonthSaleId: 0,
        YearMonth: '',
        ShopId: '',
        ActualSaleCount: 0,
        ActualSaleAmt: 0
    }
    $table.bootstrapTable('insertRow', {
        index: index,
        row: newRow
    });
    curRow = newRow;
}

function EmptyValue() {
    window.localStorage.Empty = "true";
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
