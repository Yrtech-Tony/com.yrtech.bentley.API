$('#PromotionCreate1').bootstrapValidator({
    message: isZH() ? '该数据无效' : 'The data is invalid',
    feedbackIcons: {
        valid: 'glyphicon glyphicon-ok',
        invalid: 'glyphicon glyphicon-remove',
        validating: 'glyphicon glyphicon-refresh'
    },
    fields: {
        Name: {
            message: isZH() ? '活动名称无效' : 'Invalid name of activity',
            validators: {
                notEmpty: {
                    message: isZH() ? '活动名称不能为空' : 'The activity name cannot be empty',
                },
                stringLength: {
                    min: 2,
                    max: 30,
                    message: isZH() ? '活动名称不能小于2个字符' : 'The activity name cannot be less than 2 characters',
                }
            }
        },
        StartDate: {
            message: isZH() ? '起止日期无效' : 'The start date is invalid',
            validators: {
                notEmpty: {
                    message: isZH() ? '开始不能为空' : 'The start date cannot be empty',
                },
                date: {
                    format: 'YYYY/MM/DD',
                    message: isZH() ? '日期格式不正确' : 'The date format is not correct'
                }
            }
        },
        EndDate: {
            validators: {
                notEmpty: {
                    message: isZH() ? '结束不能为空' : 'The end date cannot be empty',
                },
                date: {
                    format: 'YYYY/MM/DD',
                    message: isZH() ? '日期格式不正确' : 'The date format is not correct'
                }
            }
        },
        //lipeng add 验证
        Budget: {
            notEmpty: {
                message: isZH() ? '不能为空' : 'The data cannot be empty',
            },
        },
        ExpectedCarOwner: {
            notEmpty: {
                message: isZH() ? '不能为空' : 'The data cannot be empty',
            },
        },
        ExpectedPotentialCustomer: {
            notEmpty: {
                message: isZH() ? '不能为空' : 'The data cannot be empty',
            },
        },
        ExpectedCluesCarOwner: {
            notEmpty: {
                message: isZH() ? '不能为空' : 'The data cannot be empty',
            },
        },
        ExpectedCluesPotentialCustomer: {
            notEmpty: {
                message: isZH() ? '不能为空' : 'The data cannot be empty',
            },
        },

        ActualClueTotal: {
            notEmpty: {
                message: isZH() ? '不能为空' : 'The data cannot be empty',
            },
        },
        ExpectedDealTotal: {
            notEmpty: {
                message: isZH() ? '不能为空' : 'The data cannot be empty',
            },
        },
        ActualCost: {
            notEmpty: {
                message: isZH() ? '不能为空' : 'The data cannot be empty',
            },
        },

    }
});


//活动流程
$('#ActivityFlowTable').bootstrapTable({
    pagination: true,
    striped: true, //是否显示行间隔色
    columns: [
    {
        title: $('#TTime').val(),
        field: 'ActivityDateTime',
        valign: "left",
        align: "left",
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
            var e = "<label onclick='DeleteActivityFlowRow(this)'><i class='icon-pencil icon-white'></i>" + (isZH() ? '删除' : 'Delete') + "</label>";
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
function AddActivityFlowTable() {
    var $table = $('#ActivityFlowTable');
    var index = $table.bootstrapTable('getData').length;//尾添加行
    //index =$table.bootstrapTable('getOptions').totalRows//头添加行
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            ActivityTime: '',
            ActivityLink: '',
            ActivityContent: '',
            ActivityRemark: ''
        }
    });
}
function DeleteActivityFlowRow(tdobject) {
    var td = $(tdobject);
    td.parents("tr").remove();
    $('#ActivityFlowTable').bootstrapTable('getData').length = $('#ActivityFlowTable').bootstrapTable('getData').length - 1;
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
//展示车型
$('#DisplayModelsTable').bootstrapTable({
    pagination: true,
    striped: true, //是否显示行间隔色
    columns: [
    {
        title: $('#TDisplayModel').val(),
        field: 'DisplayModel',
        editable: {
            type: 'text',
            title: '',
            validate: function (v) {
            },
            noeditFormatter: function (value, row, index) {
                var result = { filed: "DisplayModel", value: value };
                var html = '<a href="javascript:void(0)" data-name="DisplayModel" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                if (!result.value) {
                    html = '<a href="javascript:void(0)" data-name="DisplayModel" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                }
                return html;
            }
        }
    },
    {
        title: $('#TProvider').val(),
        field: 'DisclosingParty',
        editable: {
            type: 'text',
            title: '',
            validate: function (v) {
            },
            noeditFormatter: function (value, row, index) {
                var result = { filed: "DisclosingParty", value: value };
                var html = '<a href="javascript:void(0)" data-name="DisclosingParty" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                if (!result.value) {
                    html = '<a href="javascript:void(0)" data-name="DisclosingParty" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
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
            var e = "<label onclick='DeleteDisplayModelsRow(this)'><i class='icon-pencil icon-white'></i>" + (isZH() ? '删除' : 'Delete') + "</label>";
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
function AddDisplayModelsTable() {
    var $table = $('#DisplayModelsTable');
    var index = $table.bootstrapTable('getData').length;//尾添加行
    //index =$table.bootstrapTable('getOptions').totalRows//头添加行
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            DisplayModel: '',
            DisclosingParty: ''
        }
    });
}
function DeleteDisplayModelsRow(tdobject) {
    var td = $(tdobject);
    td.parents("tr").remove();
    $('#DisplayModelsTable').bootstrapTable('getData').length = $('#DisplayModelsTable').bootstrapTable('getData').length - 1;
}
//试驾车辆
$('#TestDriveTable').bootstrapTable({
    pagination: true,
    columns: [
    {
        title: $('#TDisplayModel').val(),
        field: 'DisplayModel',
        editable: {
            type: 'text',
            title: '',
            validate: function (v) {
            },
            noeditFormatter: function (value, row, index) {
                var result = { filed: "DisplayModel", value: value };
                var html = '<a href="javascript:void(0)" data-name="DisplayModel" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                if (!result.value) {
                    html = '<a href="javascript:void(0)" data-name="DisplayModel" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                }
                return html;
            }
        }
    },
    {
        title: $('#TProvider').val(),
        field: 'DisclosingParty',
        editable: {
            type: 'text',
            title: '',
            validate: function (v) {
            },
            noeditFormatter: function (value, row, index) {
                var result = { filed: "DisclosingParty", value: value };
                var html = '<a href="javascript:void(0)" data-name="DisclosingParty" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                if (!result.value) {
                    html = '<a href="javascript:void(0)" data-name="DisclosingParty" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
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
            var e = "<label onclick='DeleteTestDriveRow(this)'><i class='icon-pencil icon-white'></i>" + (isZH() ? '删除' : 'Delete') + "</label>";
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
function AddTestDriveTable() {
    var $table = $('#TestDriveTable');
    var index = $table.bootstrapTable('getData').length;//尾添加行
    //index =$table.bootstrapTable('getOptions').totalRows//头添加行
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            DisplayModel: '',
            DisclosingParty: ''
        }
    });
}
function DeleteTestDriveRow(tdobject) {
    var td = $(tdobject);
    td.parents("tr").remove();
    $('#TestDriveTable').bootstrapTable('getData').length = $('#TestDriveTable').bootstrapTable('getData').length - 1;
}
//活动预算详情
$('#ActivityBudgetTable').bootstrapTable({
    pagination: true,
    columns: [
    {
        title: $('#ExpenseItem').val(),
        field: 'ProjectName',
        editable: {
            type: 'text',
            title: '',
            validate: function (v) {
            },
            noeditFormatter: function (value, row, index) {
                var result = { filed: "ProjectName", value: value };
                var html = '<a href="javascript:void(0)" data-name="ProjectName" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                if (!result.value) {
                    html = '<a href="javascript:void(0)" data-name="ProjectName" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                }
                return html;
            }
        }
    },
    {
        title: $('#TDESC').val(),
        field: 'OverView',
        editable: {
            type: 'text',
            title: '',
            validate: function (v) {
            },
            noeditFormatter: function (value, row, index) {
                var result = { filed: "OverView", value: value };
                var html = '<a href="javascript:void(0)" data-name="OverView" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                if (!result.value) {
                    html = '<a href="javascript:void(0)" data-name="OverView" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
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
        field: 'Count',
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
                var result = { filed: "Count", value: value };
                var html = '<a href="javascript:void(0)" data-name="Count" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                if (!result.value) {
                    html = '<a href="javascript:void(0)" data-name="Count" data-pk="undefined" data-value="" class="editable editable-click editable-empty">0</a>';
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
            var sum = $('#ActivityBudgetSum').text().replace(/,/g, '');
            var num = parseInt(sum);
            if (!isNaN(num)) {
                num = num + value;
                $('#ActivityBudgetSum').text(dealNumber(num));
            }
            else {
                $('#ActivityBudgetSum').text(dealNumber(value));
            }
            return dealNumber(value);
        }
    },
    {
        title: $('#TEdit').val(),
        field: 'Edit',
        valign: "middle",
        align: "center",
        formatter: function (value, row, index) {
            var e = "<label onclick='DeleteActivityBudgetRow(this)'><i class='icon-pencil icon-white'></i>" + (isZH() ? "删除" : "Delete") + "</label>";
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
function AddActivityBudgetTable() {
    var $table = $('#ActivityBudgetTable');
    var index = $table.bootstrapTable('getData').length;//尾添加行
    //index =$table.bootstrapTable('getOptions').totalRows//头添加行
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            ProjectName: '',
            OverView: '',
            UnitPrice: 0,
            Count: 0
        }
    });
}
function DeleteActivityBudgetRow(tdobject) {
    var td = $(tdobject);
    td.parents("tr").remove();
    $('#ActivityBudgetTable').bootstrapTable('getData').length = $('#ActivityBudgetTable').bootstrapTable('getData').length - 1;
}
//客户邀约名单
$('#CustomerInvitationTable').bootstrapTable({
    method: 'get',
    contentType: "application/x-www-form-urlencoded",//必须要有！！！！
    dataType: "json",
    url: "/Home/CustomerInvitationTable",//要请求数据的文件路径
    pagination: true,
    sidePagination: "server", //服务端处理分页
    //toolbar: '#toolbar',//指定工具栏
    striped: true, //是否显示行间隔色
    queryParams: function (params) {
        return {
            promotionId: $('#ID').val(),
            type: 1
        }
    },
    columns: [
         {
             title: $('#NO').val(),
             field: '#',
             width: 30,
             valign: "middle",
             align: "center",
             formatter: function (value, row, index) {
                 return index + 1;
             }

         },
    {
        title: "客户名称",
        field: 'CustomerName',
        valign: "left",
        align: "left",
        editable: {
            type: 'text',
            title: '客户名称',
            validate: function (v) {
            },
            noeditFormatter: function (value, row, index) {
                var result = { filed: "CustomerName", value: value };
                var html = '<a href="javascript:void(0)" data-name="CustomerName" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                if (!result.value) {
                    html = '<a href="javascript:void(0)" data-name="CustomerName" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                }
                return html;
            }
        }
    },
    {
        title: "联系方式",
        field: 'ContactInformation',
        valign: "left",
        align: "left",
        editable: {
            type: 'text',
            title: '联系方式',
            validate: function (v) {
            },
            noeditFormatter: function (value, row, index) {
                var result = { filed: "ContactInformation", value: value };
                var html = '<a href="javascript:void(0)" data-name="ContactInformation" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                if (!result.value) {
                    html = '<a href="javascript:void(0)" data-name="ContactInformation" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                }
                return html;
            }
        }
    }
    ,
    {
        title: "BP号",
        field: 'BPCode',
        valign: "left",
        align: "left",
        editable: {
            type: 'text',
            title: 'BP号',
            validate: function (v) {
            },
            noeditFormatter: function (value, row, index) {
                var result = { filed: "BPCode", value: value };
                var html = '<a href="javascript:void(0)" data-name="BPCode" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                if (!result.value) {
                    html = '<a href="javascript:void(0)" data-name="BPCode" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                }
                return html;
            }
        }
    },
    {
        title: "是否车主",
        field: 'IsOwner',
        valign: "middle",
        align: "center",
        formatter: function (value, row, index) {
            var txml = '';
            if (value == '1')
                txml = '<input id="{0}" type="checkbox" checked>';
            else
                txml = '<input id="{0}" type="checkbox">';
            txml = txml.replace("{0}", row.ID);

            return txml;
        }

    },
    {
        title: $('#TEdit').val(),
        field: 'Edit',
        valign: "middle",
        align: "center",
        formatter: function (value, row, index) {
            var e = "<label onclick='deletetr(this)'><i class='icon-pencil icon-white'></i>" + (isZH() ? '删除' : 'Delete') + "</label>";
            return e;
        }
    }
    ],
    onClickCell: function (field, value, row, $element) {
        if (field != 'IsOwner') {
            return false;
        }
        $('#id'.replace('id', row.ID))[0].checked = !value;
        curRow = row;
        curRow.IsOwner = !value;

    },
    onClickRow: function (row, $element) {
        curRow = row;
    }
});
function AddCustomerInvitationTable() {
    var $table = $('#CustomerInvitationTable');
    var index = $table.bootstrapTable('getData').length;
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            CustomerName: '',
            ContactInformation: '',
            BPCode: '',
            IsOwner: false
        }
    });
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
        columns: [
            {
                title: $('#TTime').val(),
                field: 'ActivityTime',
                editable: {
                    type: 'text',
                    title: '',
                    validate: function (v) {
                    },
                    noeditFormatter: function (value, row, index) {
                        var result = { filed: "ActivityTime", value: value };
                        var html = '<a href="javascript:void(0)" data-name="ActivityTime" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                        if (!result.value) {
                            html = '<a href="javascript:void(0)" data-name="ActivityTime" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                        }
                        return html;
                    }
                }
            },
            {
                title: $('#TProcess').val(),
                field: 'ActivityLink',
                editable: {
                    type: 'text',
                    title: '',
                    validate: function (v) {
                    },
                    noeditFormatter: function (value, row, index) {
                        var result = { filed: "ActivityLink", value: value };
                        var html = '<a href="javascript:void(0)" data-name="ActivityLink" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                        if (!result.value) {
                            html = '<a href="javascript:void(0)" data-name="ActivityLink" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                        }
                        return html;
                    }
                }

            },
            {
                title: $('#TContent').val(),
                field: 'ActivityContent',
                editable: {
                    type: 'text',
                    title: '',
                    validate: function (v) {

                    },
                    noeditFormatter: function (value, row, index) {
                        var result = { filed: "ActivityContent", value: value };
                        var html = '<a href="javascript:void(0)" data-name="ActivityContent" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                        if (!result.value) {
                            html = '<a href="javascript:void(0)" data-name="ActivityContent" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                        }
                        return html;
                    }
                }

            },
            {
                title: $('#TComments').val(),
                field: 'ActivityRemark',
                editable: {
                    type: 'text',
                    title: '',
                    validate: function (v) {


                    },
                    noeditFormatter: function (value, row, index) {
                        var result = { filed: "ActivityRemark", value: value };
                        var html = '<a href="javascript:void(0)" data-name="ActivityRemark" data-pk="undefined" data-value="" class="editable editable-click editable-empty">' + result.value + '</a>';
                        if (!result.value) {
                            html = '<a href="javascript:void(0)" data-name="ActivityRemark" data-pk="undefined" data-value="" class="editable editable-click editable-empty">NULL</a>';
                        }
                        return html;
                    }
                }
            },
            {
                title: $('#TEdit').val(),
                field: 'Edit',
                formatter: function (value, row, index) {
                    var e = "<a href='javascript:;' onclick='DeleteInitActualActivity(\"{id}\")'><i class='icon-pencil icon-white'></i>" + (isZH() ? '删除' : 'Delete') + "</a>";
                    e = e.replace("{id}", row.ID);
                    return e;
                }
            }
        ]
    });

}

function InitCustomerInvitation2Table() {
    //初始化表格
    $('#CustomerInvitation2Table').bootstrapTable({
        pagination: true,
        columns: [
                    [
                    {
                        title: "编号",//NO
                        field: '#',
                        width: 30,
                        valign: "middle",
                        align: "center",
                        colspan: 1,
                        rowspan: 3,
                        formatter: function (value, row, index) {
                            return index + 1;
                        }

                    },
                    {
                        title: "基本信息",//"{{'Dealer'|translate}}",
                        field: "BaseInfo",
                        width: "100px",
                        valign: "middle",
                        align: "center",
                        colspan: 2,
                        rowspan: 2
                    },
                    {
                        title: "客户类型",//"{{'Event Name'|translate}}",
                        field: 'CustomerType',
                        width: "200px",
                        valign: "middle",
                        align: "center",
                        colspan: 3,
                        rowspan: 1,
                    },
                    {
                        title: "是否试驾",
                        valign: "middle",
                        align: "center",
                        field: "IsTestDrive",
                        colspan: 1,
                        rowspan: 3,
                        formatter: function (value, row, index) {
                            var txml = '';
                            if (value == '1')
                                txml = '<input id="{0}" type="checkbox" checked>';
                            else
                                txml = '<input id="{0}" type="checkbox">';
                            txml = txml.replace("{0}", row.ID);

                            return txml;
                        }
                    },
                    {
                        title: "是否在邀约名单中",
                        valign: "middle",
                        align: "center",
                        field: "Inviter",
                        colspan: 1,
                        rowspan: 3,
                        formatter: function (value, row, index) {
                            var txml = '';
                            if (value == '1')
                                txml = '<input id="{0}" type="checkbox" checked >';
                            else
                                txml = '<input id="{0}" type="checkbox" >';
                            txml = txml.replace("{0}", row.ID);

                            return txml;
                        }
                    },
                    {
                        title: $('#TEdit').val(),
                        field: 'Edit',
                        valign: "middle",
                        align: "center",
                        colspan: 1,
                        rowspan: 3,
                        formatter: function (value, row, index) {
                            var e = "<a href='javascript:;' onclick='DeleteCustomerInvitation2Table(\"{id}\")'><i class='icon-pencil icon-white'></i>" + (isZH() ? '删除' : 'Delete') + "</a>";
                            e = e.replace("{id}", row.ID);
                            return e;
                        }
                    }
                    ],
                    [
                    {
                        title: "宾利车主",//"{{'Dealer'|translate}}",
                        field: "#",
                        width: "100px",
                        valign: "middle",
                        align: "center",
                        colspan: 1,
                        rowspan: 1,
                        formatter: function (value, row, index) {
                            return '<div style="min-width:100px">' + value + '</div>';
                        }
                    },
                    {
                        title: "意向客户",//"{{'Event Name'|translate}}",
                        field: '#',
                        width: "200px",
                        valign: "middle",
                        align: "center",
                        colspan: 2,
                        rowspan: 1,
                    }
                    ],
                    [
                    {
                        field: "CustomerName",
                        title: "客户名称",
                        editable: {
                            type: 'text',
                            title: '客户名称',
                            validate: function (v) {


                            }
                        }
                    },
                    {
                        field: "ContactInformation",
                        title: "联系方式",
                        editable: {
                            type: 'text',
                            title: '联系方式',
                            validate: function (v) {

                            }
                        }
                    },
                    {
                        field: "BPCode",
                        title: "BP",
                        editable: {
                            type: 'text',
                            title: 'BP',
                            validate: function (v) {


                            }
                        }
                    },
                    {
                        field: "Model",
                        title: "意向车型",
                        editable: {
                            type: 'text',
                            title: '意向车型'
                        }
                    },
                    {
                        field: "BP2",
                        title: "BP",
                        editable: {
                            type: 'text',
                            title: 'BP',
                            validate: function (v) {


                            }
                        }
                    }]
        ],
        onClickCell: function (field, value, row, $element) {
            if (field != 'IsTestDrive' && field != 'Inviter')
                return false;
            if (field == 'IsTestDrive') {
                $('#id'.replace('id', row.ID))[0].checked = !value;
                curRow = row;
                curRow.IsTestDrive = !value;
            }
            if (field == 'Inviter') {
                $('#id'.replace('id', row.ID))[0].checked = !value;
                curRow = row;
                curRow.Inviter = !value;
            }
        },
        onClickRow: function (row, $element) {
            curRow = row;
        },
        onEditableSave: function (field, row, oldValue, $el) {

        }
    });

}

function simple_uuid(index) {
    var s = "00000000-0000-0000-000000000000";
    s = s.substring(0, s.length - index.length) + index;
    return s;
}

function AddInitActualActivity() {
    var $table = $('#ActivityFlow2Table');
    var index = $table.bootstrapTable('getData').length;
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            ID: simple_uuid(index.toString()),
            ActivityTime: '',
            ActivityLink: '',
            ActivityContent: '',
            ActivityRemark: ''

        }
    });
}

function DeleteInitActualActivity(id) {
    var $table = $('#ActivityFlow2Table');
    $table.bootstrapTable('remove', {
        field: 'ID',
        values: [id]
    });

}

function AddCustomerInvitation2Table() {
    var $table = $('#CustomerInvitation2Table');
    var index = $table.bootstrapTable('getData').length;
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            ID: simple_uuid(index.toString()),
            PromotionId: $('#ID').val(),
            Type: 2,
            CustomerName: null,
            ContactInformation: null,
            BPCode: null,
            Model: null,
            BP2: null,
            IsTestDrive: false,
            Inviter: false,
        }
    });
}

function DeleteCustomerInvitation2Table(id) {
    var $table = $('#CustomerInvitation2Table');
    $table.bootstrapTable('remove', {
        field: 'ID',
        values: [id]
    });
}

function InitOrderDetail() {
    //初始化表格
    $('#OrderDetailTable').bootstrapTable({
        method: 'get',
        contentType: "application/x-www-form-urlencoded",//必须要有！！！！
        dataType: "json",
        url: "/Home/OrderDetail",//要请求数据的文件路径
        pagination: true,
        sidePagination: "server", //服务端处理分页
        //toolbar: '#toolbar',//指定工具栏
        striped: true, //是否显示行间隔色
        queryParams: function (params) {
            return {
                promotionId: $('#ID').val()
            }
        },
        columns:
        [
            {
                title: "客户名称",
                field: 'CustomerName',
                editable: {
                    type: 'text',
                    title: '客户名称',
                    validate: function (v) {


                    }
                }
            },
            {
                title: "电话",
                field: 'MobilePhone',
                editable: {
                    type: 'text',
                    title: '电话',
                    validate: function (v) {


                    }
                }

            },
            {
                title: "预估交车时间",
                field: 'DeliveryTime',
                editable: {
                    type: 'date',
                    title: '预估交车时间',
                    validate: function (v) {


                    }
                }

            },
            {
                title: "VIN No.",
                field: 'VIN',
                editable: {
                    type: 'text',
                    title: 'VIN No.',
                    validate: function (v) {


                    }
                }
            },
            {
                title: "是否在邀约名单中",
                field: 'Inviter',
                valign: "middle",
                align: "center",
                formatter: function (value, row, index) {
                    var txml = '';
                    if (value == '1')
                        txml = '<input id="{0}" type="checkbox" checked >';
                    else
                        txml = '<input id="{0}" type="checkbox">';
                    txml = txml.replace("{0}", row.ID);

                    return txml;
                }
            },
            {
                title: $('#TEdit').val(),
                field: 'Edit',
                formatter: function (value, row, index) {
                    var e = "<a href='javascript:;' onclick='DeleteOrderDetail(\"{id}\")'><i class='icon-pencil icon-white'></i>" + (isZH() ? '删除' : 'Delete') + "</a>";
                    e = e.replace("{id}", row.ID);
                    return e;
                }
            }
        ],
        onClickCell: function (field, value, row, $element) {
            if (field != 'Inviter')
                return false;

            $('#id'.replace('id', row.ID))[0].checked = !value;
            curRow = row;
            curRow.Inviter = !value;

        },
        onClickRow: function (row, $element) {
            curRow = row;
        },
        onEditableSave: function (field, row, oldValue, $el) {

        }
    });

}

function AddOrderDetail() {
    var $table = $('#OrderDetailTable');
    var index = $table.bootstrapTable('getData').length;
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            ID: simple_uuid(index.toString()),
            PromotionId: $('#ID').val(),
            CustomerName: null,
            MobilePhone: null,
            Model: null,
            DeliveryTime: null,
            Inviter: '0',
            VIN: null
        }
    });
}

function DeleteOrderDetail(id) {
    var $table = $('#OrderDetailTable');
    $table.bootstrapTable('remove', {
        field: 'ID',
        values: [id]
    });

}

function InitActualCost() {
    //初始化表格
    $('#ActualCostTable').bootstrapTable({
        method: 'get',
        contentType: "application/x-www-form-urlencoded",//必须要有！！！！
        dataType: "json",
        url: "/Home/ActualCost",//要请求数据的文件路径
        pagination: true,
        sidePagination: "server", //服务端处理分页
        //toolbar: '#toolbar',//指定工具栏
        striped: true, //是否显示行间隔色
        queryParams: function (params) {
            return {
                promotionId: $('#ID').val()
            }
        },
        columns:
        [
            {
                title: $('#ExpenseItem').val(),
                field: 'ProjectName',
                editable: {
                    type: 'text',
                    title: '',
                    validate: function (v) {
                    },
                    noeditFormatter: function (value, row, index) {
                        var result = { filed: "ProjectName", value: value };
                        var html = '<a href="javascript:void(0)" data-name="ProjectName" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                        if (!result.value) {
                            html = '<a href="javascript:void(0)" data-name="ProjectName" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                        }
                        return html;
                    }
                }
            },
            {
                title: $('#TDESC').val(),
                field: 'Description',
                editable: {
                    type: 'text',
                    title: '',
                    validate: function (v) {
                    },
                    noeditFormatter: function (value, row, index) {
                        var result = { filed: "Description", value: value };
                        var html = '<a href="javascript:void(0)" data-name="Description" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                        if (!result.value) {
                            html = '<a href="javascript:void(0)" data-name="Description" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                        }
                        return html;
                    }
                }
            },
            {
                title: $('#UnitPrice').val(),
                field: 'Price',
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
                        var result = { filed: "Price", value: value };
                        var html = '<a href="javascript:void(0)" data-name="Price" data-pk="undefined" data-value="" class="editable editable-click">' + dealNumber(result.value) + '</a>';
                        if (result.value == undefined) {
                            html = '<a href="javascript:void(0)" data-name="Price" data-pk="undefined" data-value="" class="editable editable-click">0</a>';
                        }
                        return html;
                    }
                }

            },
            {
                title: $('#TQuantity').val(),
                field: 'Num',
                valign: "middle",
                align: "center",
                editable: {
                    type: 'text',
                    title: '',
                    validate: function (v) {
                        v = $.trim(v);
                        if (!v) {
                            return isZH() ? '数量不能为空，且必须是整数' : 'Quantities cannot be empty and  only numbers accepted';
                        }
                        if (!/^\+?[1-9]\d*$/.test(v)) {
                            return isZH() ? '数量不能为空，且必须是整数' : 'Quantities cannot be empty and  only numbers accepted';
                        }
                    }
                }
            },
            {
                title: $('#TotalAmount').val(),
                field: 'Sum',
                valign: "middle",
                align: "center",
                formatter: function (value, row, index) {
                    var sum = $('#Sum').text().replace(/,/g, '');
                    var num = parseInt(sum);
                    if (!isNaN(num)) {
                        num = num + value;
                        $('#Sum').text(dealNumber(num));
                    }
                    else {
                        $('#Sum').text(dealNumber(value));
                    }
                    return dealNumber(value);
                }
            },
            {
                title: $('#TEdit').val(),
                field: 'Edit',
                formatter: function (value, row, index) {
                    var e = "<a href='javascript:;' onclick='DeleteActualCost(\"{id}\")'><i class='icon-pencil icon-white'></i>" + (isZH() ? '删除' : 'Delete') + "</a>";
                    e = e.replace("{id}", row.ID);
                    return e;
                }
            }
        ],
        onClickRow: function (row, $element) {
            curRow = row;
        },
        onEditableSave: function (field, row, oldValue, $el) {

        }
    });

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
function AddActualCost() {
    var $table = $('#ActualCostTable');
    var index = $table.bootstrapTable('getData').length;
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            ID: simple_uuid(index.toString()),
            PromotionId: $('#ID').val(),
            ProjectName: null,
            Description: null,
            Price: 0,
            Num: 0,
            Sum: 0
        }
    });
}

function DeleteActualCost(id) {
    var $table = $('#ActualCostTable');
    $table.bootstrapTable('remove', {
        field: 'ID',
        values: [id]
    });

}