
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


function getHeight() {
    var myHeight = getClientHeight() - 445 + 80;
    if (roleType != "SHOP") {
        myHeight = 474;
    }
    return myHeight;
}

var curRow;
function InitDMFDetail() {
    //生成用户数据
    $('#myBudgetCost').bootstrapTable({
        pagination: true,
        striped: true, //是否显示行间隔色
        height: getClientHeight() - 280 + 80 - 211,
        showColumns: false, // 开启自定义列显示功能
        sortable: true,
        sortName: 'DMFDetailId',
        sortOrder: 'asc',
        pageNumber: 1,
        pageSize: 10,
        pageList: [5, 10, 20, 50],
        columns: [{
            field: 'checkedId',
            checkbox: true,
            valign: "middle",
            align: "center"
        }, {
            title: $('#NO').val(),
            field: 'DMFDetailId',
            width: 30,
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                return index + 1;
            }
        }, {
            title: $('#Dealer').val(),
            field: 'ShopId',
            width: "120px",
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
                title: '',
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
                            html = '<a href="javascript:void(0)" data-name="ShopId" data-pk="undefined" data-value="" class="editable editable-click">&nbsp;</a>';
                        }
                        return html;
                    } else { return real; }
                }
            }

        }, {
            title: $('#TEI').val(),
            field: "DMFItemId",
            width: "400px",
            valign: "middle",
            align: "center",
            sortable: false,
            editable: {
                type: 'select',
                title: '',
                source: dmfData,
                validate: function (v) {
                    if (!v) return isZH() ? '项目不能为空' : 'The project cannot be empty';
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "DMFItemId", value: value };
                    var real = '';
                    for (var i = 0; i < dmfData.length; i++) {
                        if (dmfData[i].value == value) {
                            real = dmfData[i].text;
                        }
                    }
                    if (roleType != "SHOP") {
                        var html = '<a href="javascript:void(0)" data-name="DMFItemId" data-pk="undefined" data-value="" class="editable editable-click">' + real + '</a>';
                        if (!result.value) {
                            html = '<a href="javascript:void(0)" data-name="DMFItemId" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                        }
                        return html;
                    } else { return real; }
                }
            }
        },
        {
            title: $('#TBudget').val(),
            field: "Budget",
            valign: "middle",
            align: "center",
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                    v = $.trim(v);
                    var vil = isZH() ? '预算不能为空，且必须是数字' : 'Budgets cannot be empty, and only numbers accepted.';
                    if (!v) {
                        return vil;
                    }
                    if (!/^(-?\d+)(\.\d+)?$/.test(v)) {
                        return vil;
                    }
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "Budget", value: value };
                    if (roleType != "SHOP") {
                        var html = '<a href="javascript:void(0)" data-name="Budget" data-pk="undefined" data-value="" class="editable editable-click">' + dealNumber(result.value) + '</a>';
                        if (result.value == "") {
                            html = '<a href="javascript:void(0)" data-name="Budget" data-pk="undefined" data-value="" class="editable editable-click">0</a>';
                        }
                        return html;
                    } else { return dealNumber(result.value); }
                }
            }
        },
        {
            title: $('#TAE').val(),
            valign: "middle",
            align: "center",
            field: "AcutalAmt",
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                    v = $.trim(v);
                    var vil = isZH() ? '实际花费不能为空，且必须是数字' : 'The actual cost cannot be null, and only numbers accepted';
                    if (!v) {
                        return vil;
                    }
                    if (!/^(-?\d+)(\.\d+)?$/.test(v)) {
                        return vil;
                    }
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "AcutalAmt", value: value };

                    if (roleType != "SHOP" && !row.isReimburse) {
                        var html = '<a href="javascript:void(0)" data-name="AcutalAmt" data-pk="undefined" data-value="" class="editable editable-click">' + dealNumber(result.value) + '</a>';
                        if (result.value == "") {
                            html = '<a href="javascript:void(0)" data-name="AcutalAmt" data-pk="undefined" data-value="" class="editable editable-click">0</a>';
                        }
                        return html;
                    } else { return dealNumber(result.value); }
                }
            }
        },
        {
            title: $('#TRemark').val(),
            field: "Remark",
            width: "500px",
            valign: "middle",
            align: "center",
            sortable: false,
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "Remark", value: value };
                    if (roleType != "SHOP") {
                        var html = '<a href="javascript:void(0)" data-name="Remark" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                        if (result.value == "") {
                            html = '<a href="javascript:void(0)" data-name="Remark" data-pk="undefined" data-value="" class="editable editable-click">未填写</a>';
                        }
                        return html;
                    } else { return result.value; }
                }
            }
        }, {
            title: 'HIDE',
            field: "isReimburse",
            width: "1px"
        }],
        onClickCell: function (field, value, row, $element) {
            return false;
        },
        onClickRow: function (row, $element) {
            curRow = row;
        },
        onEditableSave: function (field, row, oldValue, $el) {
            if (row.DMFItemId && row.ShopId) {
                row.InUserId = $("#G_UserId").val();
                row.ModifyUserId = $("#G_UserId").val();
                $.commonPost("DMF/DMFDetailSave", row, function (data) {
                    if (data) {
                        row.DMFDetailId = data.DMFDetailId;
                    }
                    curRow = row;
                    IsEdit = true;
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

    $.commonPost("DMF/DMFDetailDelete", {
        ListJson: JSON.stringify(rows)
    }, function () {
        console.log('删除数据成功');
        loadDMFDetail();
    });
}

function Add() {
    var index = $table.bootstrapTable('getData').length;
    var newRow = {
        DMFDetailId: 0,
        ShopId: '',
        DMFItemId: '',
        Budget: 0,
        AcutalAmt: 0,
        Remark: ''
    };
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