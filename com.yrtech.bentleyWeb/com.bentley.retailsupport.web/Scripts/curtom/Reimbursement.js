
var IsEdit = false;
var curRow;
function GoUpload(id, type) {
    if (!curRow || curRow.ExpenseAccountId == 0) {
        var rows = $table.bootstrapTable('getData');
        var filters = rows.filter(function (item) {
            return item.ExpenseAccountId == id;
        })
        if (filters && filters.length > 0) {
            curRow = filters[0];
        } else {
            curRow = rows[0]
        }
    }
    if (!curRow.DMFItemId && !IsEdit) {
        layer.alert(isZH() ? '请填写项目，再上传文件！' : 'Please fill in the project and upload the files!');
        return;
    }
    if (!curRow.MarketActionId && !IsEdit) {
        layer.alert(isZH() ? '请填写活动名称，再上传文件！' : 'Please fill in the activity name and upload the file!');
        return;
    }

    IsEdit = false;
    var url = '/DMF/ExpenseAccountFile?id=' + id + '&fileType=' + type;
    window.location.href = url;
}


function InitMarketFundLst() {
    //生成用户数据
    $('#myReimbursement').bootstrapTable({
        pagination: true,
        //striped: true, //是否显示行间隔色
        height: getClientHeight() - 280 + 80,
        showColumns: false, // 开启自定义列显示功能
        sortable: true,
        sortName: 'ExpenseAccountId',
        sortOrder: 'asc',
        pageSize: 10,
        pageList: [5, 10, 20, 50],
        columns: [{
            checkbox: true,
            valign: "middle",
            align: "center",
            field: "Ischecked"
        }, {
            title: $('#NO').val(),
            field: 'ExpenseAccountId',
            width: 30,
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                return index + 1;
            }
        }, {
            title: $('#Dealer').val(),
            field: "ShopName",
            valign: "middle",
            align: "center",
            width: "100px",
            formatter: function (value, row, index) {
                var name = isZH() ? row.ShopName : row.ShopNameEn;
                return '<div style="min-width:100px">' + name + '</div>';
            }
        }, {
            title: $('#ExpenseItem').val(),
            field: 'DMFItemId',
            width: "300px",
            valign: "middle",
            align: "center",
            sortable: false,
            align: 'left',
            editable: {
                type: 'select',
                title: '',
                source: hdData,
                validate: function (v) {
                    if (!v) return isZH() ? '项目不能为空' : 'The Tick-box Item cannot be empty';
                },
                noeditFormatter: function (value, row, index) {
                    value = isZH() ? row.DMFItemName : row.DMFItemNameEn;
                    var html = '<a href="javascript:void(0)" data-name="DMFItemId" data-pk="undefined" data-value="" class="editable editable-click">' + value + '</a>';
                    if (!value) {
                        html = '<a href="javascript:void(0)" data-name="DMFItemId" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                    }
                    return html;
                }
            }
        }, {
            title: $('#Promotion_Name').val(),
            field: "MarketActionId",
            width: "300px",
            valign: "middle",
            align: "center",
            sortable: false,
            editable: {
                type: 'select',
                title: '',
                source: activityData,
                validate: function (v) {
                    if (!v) return isZH() ? '活动名称不能为空' : 'The activity name cannot be empty';
                },
                noeditFormatter: function (value, row, index) {
                    var html = '<a href="javascript:void(0)" data-name="MarketActionId" data-pk="undefined" data-value="" class="editable editable-click">' + row.ActionName + '</a>';
                    if (!row.ActionName) {
                        html = '<a href="javascript:void(0)" data-name="MarketActionId" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                    }
                    return html;
                }
            }
        }, {
            title: $('#TExpense').val(),
            field: "ExpenseAmt",
            valign: "middle",
            align: "center",
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
                    var result = { filed: "ExpenseAmt", value: value };
                    var html = '<a href="javascript:void(0)" data-name="ExpenseAmt" data-pk="undefined" data-value="" class="editable editable-click">' + dealNumber(result.value) + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="ExpenseAmt" data-pk="undefined" data-value="" class="editable editable-click">0</a>';
                    }
                    if (roleType != "SYSADMIN" && row.ApplyStatus == '通过') {
                        html = dealNumber(result.value);
                    }
                    return html;
                }
            }
        }, {
            title: $('#TAD').val(),
            valign: "middle",
            width: "170px",
            align: "center",
            field: "",
            formatter: function (value, row, index) {
                var cnt = '<button onclick="GoUpload(\'' + row.ExpenseAccountId + '\',\'1\');">' + $('#TRAN_Quotation').val() + '</button> &nbsp;';
                cnt += '<button onclick="GoUpload(\'' + row.ExpenseAccountId + '\',\'2\');">' + $('#_ht_emailSnapshot').val() + '</button>';
                return cnt;
            }
        }, {
            title: $('#TAppSta').val(),
            width: '90px',
            valign: "middle",
            align: "center",
            field: "ApplyStatus",
            editable: {
                type: 'select',
                title: '',
                source: arySelect,
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "ApplyStatus", value: value };
                    var real = '未提交';
                    for (var i = 0; i < ary.length; i++) {
                        if (ary[i].value == value) {
                            real = ary[i].text;
                        }
                    }
                    if (roleType != 'SHOP') {
                        var html = '<a href="javascript:void(0)" data-name="ApplyStatus" data-pk="undefined" data-value="" onclick="if(this.value ==\'通过\'){this.className=\'btn btn-success btn-sm\'}else if(this.value ==\'修改\'){this.className=\'btn btn-warning  btn-sm\'}else{this.className=\'btn btn-primary  btn-sm\'}">' + real + '</a>';
                        if (real == '通过') {
                            html = real;
                        }
                        if (!real || real == '未提交') {
                            html = '<a href="javascript:void(0)" class="btn btn-success btn-sm" data-name="ApplyStatus" data-pk="undefined" data-value="" class="editable editable-click">' + real + '</a>';
                        }
                        return html;
                    } else {
                        return real;
                    }
                }
            }
        }, {
            title: $('#Tnae').val(),
            field: "ApprovalReason",
            width: "150px",
            valign: "middle",
            align: "center",
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "ApprovalReason", value: value };
                    if (roleType != 'SHOP') {
                        var html = '<a href="javascript:void(0)" data-name="ApprovalReason" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                        if (!result.value) {
                            html = '<a href="javascript:void(0)" data-name="ApprovalReason" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                        }
                        return html;
                    } else { return value; }
                }
            }
        }, {
            title: $('#Trsd').val(),
            valign: "middle",
            width: "260px",
            align: "center",
            field: "",
            formatter: function (value, row, index) {
                var cnt = '<button onclick="GoUpload(\'' + row.ExpenseAccountId + '\',\'3\');">' + $('#TContract').val() + '</button> &nbsp;';
                cnt += '<button onclick="GoUpload(\'' + row.ExpenseAccountId + '\',\'4\');">' + $('#TInvoice').val() + '</button> &nbsp;';
                cnt += '<button onclick="GoUpload(\'' + row.ExpenseAccountId + '\',\'5\');">' + $('#TRAN_Quotation').val() + '</button> &nbsp;';
                cnt += '<button onclick="GoUpload(\'' + row.ExpenseAccountId + '\',\'6\');">' + $('#TRAN_OTHERS').val() + '</button>';
                return cnt;
            }
        }, {
            title: $('#TRAN_AR').val(),
            field: "ReplyStatus",
            valign: "middle",
            align: "center",
            sortable: false,
            editable: {
                type: 'select',
                title: '',
                source: arySelect,
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "ReplyStatus", value: value };
                    var real = '未提交';
                    for (var i = 0; i < ary.length; i++) {
                        if (ary[i].value == value) {
                            real = ary[i].text;
                        }
                    }

                    if (roleType != 'SHOP') {
                        var html = '<a href="javascript:void(0)" data-name="ReplyStatus" data-pk="undefined" data-value="">' + real + '</a>';
                        if (!real || real == '未提交') {
                            html = '<a href="javascript:void(0)" class="btn btn-success btn-sm" data-name="ReplyStatus" class="editable editable-click">' + real + '</a>';
                        }
                        return html;
                    } else { return real }
                }
            }
        }, {
            title: $('#NAE').val(),
            field: "ReplyReason",
            width: "150px",
            valign: "middle",
            align: "center",
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "ReplyReason", value: value };
                    if (roleType != 'SHOP') {
                        var html = '<a href="javascript:void(0)" data-name="ReplyReason" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                        if (!result.value) {
                            html = '<a href="javascript:void(0)" data-name="ReplyReason" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                        }
                        return html;
                    } else { return value; }
                }
            }
        }],
        onPageChange: function (number, size) {
            window.localStorage.pageNumberReimb = number;
        },
        onClickCell: function (field, value, row, $element) {
            return false;
        },
        onClickRow: function (row, $element) {
            curRow = row;
        },
        onEditableSave: function (field, row, oldValue, $el) {
            row.InUserId = $("#G_UserId").val();
            row.ModifyUserId = $("#G_UserId").val();
            row.ExpenseAmt = DESEncrypt(row.ExpenseAmt);
            $.commonPost("DMF/ExpenseAccountSave", row, function (data) {
                if (data) {
                    row.ExpenseAccountId = data.ExpenseAccountId;
                }
                curRow = row;
                IsEdit = true;
                loadExpenseAccount();
            }, function (msg) {
                var error = isZH() ? '编辑失败!' : 'Edit failure!';
                error += (msg || '')
                layer.alert(error);
            })
            var ShopName = isZH() ? row.ShopName : row.ShopNameEn;
            if (row.ExpenseAmt && row.DMFItemId && ShopName && row.ApplyStatus == '通过') {
                //调用更新市场基金实际费用的api  待确认
            }
        }
    });
}

function Add() {
    //NameQuery();//重新刷Table
    //setTimeout(AddRow, 200);

    AddRow();
}

function AddRow() {
    var index = $table.bootstrapTable('getData').length;
    var newRow = {
        ExpenseAccountId: 0,
        ShopId: $("#ShopId").val(),
        ShopName: $('#_hd_dealName').val(),
        DMFItemId: '',
        MarketActionId: '',
        ExpenseAmt: 0,
        ApplyStatus: '',
        ApprovalReason: '',
        ReplyStatus: '',
        ReplyReason: ''
    };
    $table.bootstrapTable('insertRow', {
        index: index,
        row: newRow
    });
    curRow = newRow;
}

function Del() {
    var rows = $table.bootstrapTable('getSelections');
    if (rows.length == 0) {
        layer.alert(isZH() ? "请选择需要删除的行!" : "Please select rows to delete!");
        return;
    }

    $.commonPost("DMF/ExpenseAccountDelete", {
        ListJson: JSON.stringify(rows)
    }, function () {
        console.log('删除数据成功');
        loadExpenseAccount();
    });
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
