
function InitActivityFlowTable() {

    //活动流程
    $('#ActivityFlowTable').bootstrapTable({
        pagination: true,
        striped: true, //是否显示行间隔色
        sortable: true,
        sortName: 'ActivityDateTime',
        sortOrder: 'asc',
        columns: [
        {
            title: $('#TTime').val(),
            field: 'ActivityDateTime',
            valign: "left",
            align: "left",
            sortable: true,
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "ActivityDateTime", value: value };
                    var html = '<a href="javascript:void(0)" data-name="ActivityDateTime" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="ActivityDateTime" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#TProcess').val(),
            field: 'Item',
            valign: "left",
            align: "left",
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "Item", value: value };
                    var html = '<a href="javascript:void(0)" data-name="Item" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="Item" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#TContent').val(),
            field: 'Contents',
            valign: "left",
            align: "left",
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "Contents", value: value };
                    var html = '<a href="javascript:void(0)" data-name="Contents" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="Contents" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }
            }

        },
        {
            title: $('#TComments').val(),
            valign: "left",
            align: "left",
            field: 'Remark',
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "Remark", value: value };
                    var html = '<a href="javascript:void(0)" data-name="Remark" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="Remark" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#TEdit').val(),
            field: 'Edit',
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                var e = "<label onclick='DeleteActivityFlowRow(" + row.SeqNO + ")'><i class='icon-pencil icon-white'></i>" + (isZH() ? '删除' : 'Delete') + "</label>";
                return e;
            }
        }
        ],
        onClickCell: function (field, value, row, $element) {
            return false;

        },
        onClickRow: function (row, $element) {
            curRow = row;
        },
        onEditableSave: function (field, row, oldValue, $el) {

        }
    });
}

var maxActivityFlowSeqNO = 0;
function AddActivityFlowTable() {
    var $table = $('#ActivityFlowTable');
    var index = $table.bootstrapTable('getData').length;//尾添加行
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            SeqNO: maxActivityFlowSeqNO++,
            ActivityDateTime: '',
            Item: '',
            Contents: '',
            Remark: ''
        }
    });
}
function DeleteActivityFlowRow(id) {
    var $table = $('#ActivityFlowTable');
    $table.bootstrapTable('remove', {
        field: 'SeqNO',
        values: [id]
    });
}

function deletetr(tdobject) {
    var td = $(tdobject);
    td.parents("tr").remove();
}
function deletetrNoData(tableName) {
    var obj = '#' + tableName + ' .no-records-found';
    var tr = $(obj)[0];
    if (tr != undefined) {
        tr.remove()
    }
}

function InitDisplayModelsTable() {
    //展示车型
    $('#DisplayModelsTable').bootstrapTable({
        pagination: true,
        striped: true, //是否显示行间隔色
        columns: [
        {
            title: $('#TDisplayModel').val(),
            field: 'DisplayModelColor',
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "DisplayModelColor", value: value };
                    var html = '<a href="javascript:void(0)" data-name="DisplayModelColor" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="DisplayModelColor" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#TProvider').val(),
            field: 'Provider',
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "Provider", value: value };
                    var html = '<a href="javascript:void(0)" data-name="Provider" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="Provider" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#TEdit').val(),
            field: 'Edit',
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                var e = "<label onclick='DeleteDisplayModelsRow(" + row.SeqNO + ")'><i class='icon-pencil icon-white'></i>" + (isZH() ? '删除' : 'Delete') + "</label>";
                return e;
            }
        }
        ],
        onClickCell: function (field, value, row, $element) {
            return false;

        },
        onClickRow: function (row, $element) {
            curRow = row;
        },
        onEditableSave: function (field, row, oldValue, $el) {

        }
    });
}

var maxDisplayModelsSeqNO = 0;
function AddDisplayModelsTable() {
    var $table = $('#DisplayModelsTable');
    var index = $table.bootstrapTable('getData').length;//尾添加行
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            SeqNO: maxDisplayModelsSeqNO++,
            DisplayModelColor: '',
            Provider: ''
        }
    });
}
function DeleteDisplayModelsRow(id) {
    var $table = $('#DisplayModelsTable');
    $table.bootstrapTable('remove', {
        field: 'SeqNO',
        values: [id]
    });
}

function InitTestDriverTable() {
    //试驾车辆
    $('#TestDriveTable').bootstrapTable({
        pagination: true,
        columns: [
        {
            title: $('#TDisplayModel').val(),
            field: 'DisplayModelColor',
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "DisplayModelColor", value: value };
                    var html = '<a href="javascript:void(0)" data-name="DisplayModelColor" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="DisplayModelColor" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#TProvider').val(),
            field: 'Provider',
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "Provider", value: value };
                    var html = '<a href="javascript:void(0)" data-name="Provider" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="Provider" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }
            }
        }, {
            title: $('#TEdit').val(),
            field: 'Edit',
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                var e = "<label onclick='DeleteTestDriveRow(" + row.SeqNO + ")'><i class='icon-pencil icon-white'></i>" + (isZH() ? '删除' : 'Delete') + "</label>";
                return e;
            }
        }],
        onClickCell: function (field, value, row, $element) {
            return false;

        },
        onClickRow: function (row, $element) {
            curRow = row;
        },
        onEditableSave: function (field, row, oldValue, $el) {

        }
    });
}

var maxTestDriveSeqNO = 0;
function AddTestDriveTable() {
    var $table = $('#TestDriveTable');
    var index = $table.bootstrapTable('getData').length;//尾添加行
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            SeqNO: maxTestDriveSeqNO++,
            DisplayModelColor: '',
            Provider: ''
        }
    });
}
function DeleteTestDriveRow(id) {
    var $table = $('#TestDriveTable');
    $table.bootstrapTable('remove', {
        field: 'SeqNO',
        values: [id]
    });
}

function InitActivityBudgetTable() {
    //活动预算详情
    $('#ActivityBudgetTable').bootstrapTable({
        pagination: true,
        columns: [
        {
            title: $('#ExpenseItem').val(),
            field: 'ItemName',
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "ItemName", value: value };
                    var html = '<a href="javascript:void(0)" data-name="ItemName" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="ItemName" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#TDESC').val(),
            field: 'Descs',
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "Descs", value: value };
                    var html = '<a href="javascript:void(0)" data-name="Descs" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="Descs" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }

            }
        }
        ,
        {
            title: $('#UnitPrice').val(),
            field: 'UnitPrice',
            valign: "middle",
            align: "center",
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                    v = $.trim(v);
                    if (!v) {
                        return isZH() ? '单价不能为空，且必须是数字' : 'The unit price cannot be null and only numbers accepted';
                    }
                    if (!/^(-?\d+)(\.\d+)?$/.test(v)) {
                        return isZH() ? '单价不能为空，且必须是数字' : 'The unit price cannot be null and only numbers accepted';
                    }
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "UnitPrice", value: value };
                    var html = '<a href="javascript:void(0)" data-name="UnitPrice" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + dealNumber(result.value) + '</a>';
                    if (!result.value || result.value == undefined) {
                        html = '<a href="javascript:void(0)" data-name="UnitPrice" data-pk="undefined" data-value="" class="editable editable-click editable-empty">0</a>';
                    }
                    return html;
                }
            }

        },
        {
            title: $('#TQuantity').val(),
            field: 'Counts',
            valign: "middle",
            align: "center",
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                    v = $.trim(v);
                    if (!v) {
                        return isZH() ? '数量不能为空，且必须是整数' : 'Quantities cannot be empty and only numbers accepted';
                    }
                    if (!/^\+?[1-9]\d*$/.test(v)) {
                        return isZH() ? '数量不能为空，且必须是整数' : 'Quantities cannot be empty and  only numbers accepted';
                    }
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "Counts", value: value };
                    var html = '<a href="javascript:void(0)" data-name="Counts" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="Counts" data-pk="undefined" data-value="" class="editable editable-click editable-empty">0</a>';
                    }
                    return html;
                }
            }
        }
        ,
        {
            title: $('#TotalAmount').val(),
            field: 'Total',
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                return dealNumber(value);
            }
        },
        {
            title: $('#TEdit').val(),
            field: 'Edit',
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                var e = "<label onclick='DeleteActivityBudgetRow(" + row.SeqNO + ")'><i class='icon-pencil icon-white'></i>" + (isZH() ? "删除" : "Delete") + "</label>";
                return e;
            }
        }
        ],
        onClickCell: function (field, value, row, $element) {
            return false;

        },
        onClickRow: function (row, $element) {
            curRow = row;
        },
        onEditableSave: function (field, row, oldValue, $el) {
            $('#ActivityBudgetTable').bootstrapTable('resetView');//重置Table大小
        }
    });
}

var maxActivityBudgetSeqNO = 0;
function AddActivityBudgetTable() {
    var $table = $('#ActivityBudgetTable');
    var index = $table.bootstrapTable('getData').length;//尾添加行
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            SeqNO: maxActivityBudgetSeqNO++,
            ItemName: '',
            Descs: '',
            UnitPrice: 0,
            Counts: 0
        }
    });
    $("#ActivityBudgetSum").html("");
}
function DeleteActivityBudgetRow(id) {
    var $table = $('#ActivityBudgetTable');
    $table.bootstrapTable('remove', {
        field: 'SeqNO',
        values: [id]
    });
    $("#ActivityBudgetSum").html("");
}

function ShowPlan() {
    if ($('#activityplan')[0].style.display == 'none') {
        $('#activityplan')[0].style.display = 'block';
    }
    else {
        $('#activityplan')[0].style.display = 'none';
    }
}

function ShowSolution() {
    if ($('#ultimatesolution')[0].style.display == 'none') {
        $('#ultimatesolution')[0].style.display = 'block';
    }
    else {
        $('#ultimatesolution')[0].style.display = 'none';
    }
}

function ShowReport() {
    if ($('#activityreport')[0].style.display == 'none') {
        $('#activityreport')[0].style.display = 'block';
    }
    else {
        $('#activityreport')[0].style.display = 'none';
    }
}

function InitActualActivityProcess() {
    //初始化表格
    $('#ActivityFlow2Table').bootstrapTable({
        pagination: true,
        striped: true, //是否显示行间隔色
        sortable: true,
        sortName: 'ActivityDateTime',
        sortOrder: 'asc',
        columns: [
        {
            title: $('#TTime').val(),
            field: 'ActivityDateTime',
            valign: "left",
            align: "left",
            sortable: true,
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "ActivityDateTime", value: value };
                    var html = '<a href="javascript:void(0)" data-name="ActivityDateTime" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="ActivityDateTime" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#TProcess').val(),
            field: 'Item',
            valign: "left",
            align: "left",
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "Item", value: value };
                    var html = '<a href="javascript:void(0)" data-name="Item" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="Item" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#TContent').val(),
            field: 'Contents',
            valign: "left",
            align: "left",
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "Contents", value: value };
                    var html = '<a href="javascript:void(0)" data-name="Contents" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="Contents" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#TComments').val(),
            valign: "left",
            align: "left",
            field: 'Remark',
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "Remark", value: value };
                    var html = '<a href="javascript:void(0)" data-name="Remark" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="Remark" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#TEdit').val(),
            field: 'Edit',
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                var e = "<label onclick='DeleteInitActualActivity(" + row.SeqNO + ")'><i class='icon-pencil icon-white'></i>" + (isZH() ? '删除' : 'Delete') + "</label>";
                return e;
            }
        }
        ],
        onClickCell: function (field, value, row, $element) {
            return false;

        },
        onClickRow: function (row, $element) {
            curRow = row;
        },
        onEditableSave: function (field, row, oldValue, $el) {

        }

    });

}

var maxActualActivitySeqNo = 0;
function AddInitActualActivity() {
    var $table = $('#ActivityFlow2Table');
    var index = $table.bootstrapTable('getData').length;
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            SeqNO: maxActualActivitySeqNo++,
            ActivityDateTime: '',
            Item: '',
            Contents: '',
            Remark: ''
        }
    });
}

function DeleteInitActualActivity(id) {
    var $table = $('#ActivityFlow2Table');
    $table.bootstrapTable('remove', {
        field: 'SeqNO',
        values: [id]
    });
}

//活动实际费用清单
function InitActualCost() {
    //初始化表格
    $('#ActualCostTable').bootstrapTable({
        pagination: true,
        striped: true, //是否显示行间隔色
        columns:
        [
            {
                title: $('#ExpenseItem').val(),
                field: 'Item',
                editable: {
                    type: 'text',
                    title: '',
                    validate: function (v) {
                    },
                    noeditFormatter: function (value, row, index) {
                        var result = { filed: "Item", value: value };
                        var html = '<a href="javascript:void(0)" data-name="Item" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                        if (result.value == "" || result.value == null) {
                            html = '<a href="javascript:void(0)" data-name="Item" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                        }
                        return html;
                    }
                }
            },
            {
                title: $('#TDESC').val(),
                field: 'Descs',
                editable: {
                    type: 'text',
                    title: '',
                    validate: function (v) {
                    },
                    noeditFormatter: function (value, row, index) {
                        var result = { filed: "Descs", value: value };
                        var html = '<a href="javascript:void(0)" data-name="Descs" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                        if (result.value == "" || result.value == null) {
                            html = '<a href="javascript:void(0)" data-name="Descs" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                        }
                        return html;
                    }
                }
            },
            {
                title: $('#UnitPrice').val(),
                field: 'UnitPrice',
                valign: "middle",
                align: "center",
                editable: {
                    type: 'text',
                    title: '',
                    validate: function (v) {
                        v = $.trim(v);
                        if (!v) {
                            return isZH() ? '单价不能为空，且必须是数字' : 'The unit price cannot be null and only numbers accepted';
                        }
                        if (!/^(-?\d+)(\.\d+)?$/.test(v)) {
                            return isZH() ? '单价不能为空，且必须是数字' : 'The unit price cannot be null and only numbers accepted';
                        }
                    },
                    noeditFormatter: function (value, row, index) {
                        var result = { filed: "UnitPrice", value: value };
                        var html = '<a href="javascript:void(0)" data-name="UnitPrice" data-pk="undefined" data-value="" class="editable editable-click">' + dealNumber(result.value) + '</a>';
                        if (result.value == undefined) {
                            html = '<a href="javascript:void(0)" data-name="UnitPrice" data-pk="undefined" data-value="" class="editable editable-click">0</a>';
                        }
                        return html;
                    }
                }
            },
            {
                title: $('#TQuantity').val(),
                field: 'Counts',
                valign: "middle",
                align: "center",
                editable: {
                    type: 'text',
                    title: '',
                    validate: function (v) {
                        v = $.trim(v);
                        if (!v) {
                            return isZH() ? '数量不能为空，且必须是整数' : 'Quantities cannot be empty and only numbers accepted';
                        }
                        if (!/^\+?[1-9]\d*$/.test(v)) {
                            return isZH() ? '数量不能为空，且必须是整数' : 'Quantities cannot be empty and only numbers accepted';
                        }
                    },
                    noeditFormatter: function (value, row, index) {
                        var result = { filed: "Counts", value: value };
                        var html = '<a href="javascript:void(0)" data-name="Counts" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                        if (!result.value) {
                            html = '<a href="javascript:void(0)" data-name="Counts" data-pk="undefined" data-value="" class="editable editable-click editable-empty">0</a>';
                        }
                        return html;
                    }
                }
            },
            {
                title: $('#TotalAmount').val(),
                field: 'Total',
                valign: "middle",
                align: "center",
                formatter: function (value, row, index) {
                    return dealNumber(value);
                }
            },
            {
                title: $('#TEdit').val(),
                field: '',
                formatter: function (value, row, index) {
                    return "<a href='javascript:;' onclick='DeleteActualCost(" + row.SeqNO + ")'><i class='icon-pencil icon-white'></i>" + (isZH() ? '删除' : 'Delete') + "</a>";;
                }
            }
        ],
        onClickRow: function (row, $element) {
            curRow = row;
        },
        onEditableSave: function (field, row, oldValue) {
        }
    });

}

var maxActualCostSeqNo = 0;
function AddActualCost() {
    var $table = $('#ActualCostTable');
    var index = $table.bootstrapTable('getData').length;
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            SeqNO: maxActualCostSeqNo++,
            Item: null,
            Descs: null,
            Description: null,
            UnitPrice: 0,
            Counts: 0,
            Total: 0
        }
    });
    $("#ExpenseTotalAmt").html("");
}

function DeleteActualCost(id) {
    var $table = $('#ActualCostTable');
    $table.bootstrapTable('remove', {
        field: 'SeqNO',
        values: [id]
    });
    $("#ExpenseTotalAmt").html("");
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
