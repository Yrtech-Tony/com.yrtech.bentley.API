var $table = $('#myMarketFund');
InitMarketFundLst();

$(window).resize(function () {
    $('#myMarketFund').bootstrapTable('destroy');// 销毁表格数据
    InitMarketFundLst();

});

function GetTransName(columnName) {
    return document.getElementById(columnName).value;
}

function InitMarketFundLst() {
    //生成用户数据
    $('#myMarketFund').bootstrapTable({
        pagination: true,
        striped: true, //是否显示行间隔色
        height: getClientHeight() - 280 + 80,
        showColumns: false, // 开启自定义列显示功能
        sortable: true,
        sortName: 'DMFItemName ',
        sortOrder: 'asc',
        pageNumber: 1,
        pageSize: 20,
        pageList: [5, 10, 20, 50],
        columns:
            [{
                checkbox: true,
                valign: "middle",
                align: "center"
            },
         {
             title: $('#NO').val(),
             field: 'DMFItemId ',
             width: 30,
             valign: "middle",
             align: "center",
             formatter: function (value, row, index) {
                 return index + 1;
             }
         },
        {
            title: $('#MarketFund_Name').val(),
            field: 'DMFItemName',
            width: "400px",
            valign: "middle",
            align: "center",
            sortable: false,
            align: 'left',
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                    if (!v) return isZH() ? '项目不能为空' : 'The project cannot be empty';
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "DMFItemName", value: value };
                    var html = '<a href="javascript:void(0)" data-name="DMFItemName" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="DMFItemName" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#EnglishName').val(),
            field: 'DMFItemNameEn',
            width: "400px",
            valign: "middle",
            align: "center",
            sortable: false,
            align: 'left',
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                    if (!v) return isZH() ? '项目英语名不能为空' : 'The project English name cannot be empty';
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "DMFItemNameEn", value: value };
                    var html = '<a href="javascript:void(0)" data-name="DMFItemNameEn" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="DMFItemNameEn" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#Tdesc').val(),
            field: "DMFItemRemark",
            width: "500px",
            valign: "middle",
            align: "center",
            editable: {
                type: 'text',
                //title: GetTransName('Tdesc'),
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "DMFItemRemark", value: value };
                    var html = '<a href="javascript:void(0)" data-name="DMFItemRemark" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="DMFItemRemark" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                    }
                    return html;
                }
            }

        },

        {
            title: $('#Treim').val(),
            field: "ExpenseAccountChk",
            valign: "middle",
            align: "center",
            editable: {
                type: 'checkbox',
                title: $('#Treim').val(),
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "ExpenseAccountChk", value: value };
                    var txml = '';
                    if (result.value)
                        txml = '<input id="{0}" type="checkbox" checked >';
                    else
                        txml = '<input id="{0}" type="checkbox">';
                    txml = txml.replace("{0}", row.DMFItemId  + "ExpenseAccountChk");

                    return txml;
                }
            }

        },
        {
            title: $('#Tpublic').val(),
            valign: "middle",
            align: "center",
            field: "PublishChk",
            editable: {
                type: 'checkbox',
                //title: $('#Tpublic').val(),
                validate: function (v) {
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "PublishChk", value: value };
                    var txml = '';
                    if (result.value)
                        txml = '<input id="{0}" type="checkbox" checked >';
                    else
                        txml = '<input id="{0}" type="checkbox">';
                    txml = txml.replace("{0}", row.DMFItemId  + "PublishChk");

                    return txml;
                }
            }
        }],
        onClickCell: function (field, value, row, $element) {
            if ($element.find(":checkbox").length == 0) {
                return false;
            }
            $element.find(":checkbox").prop("checked", true);
            row[field] = !value;
            saveMarketFund(row);
        },
        onEditableSave: function (field, row, oldValue, $el) {
            console.log("onEditableSave");
            saveMarketFund(row);
        }
    });
}

function saveMarketFund(row) {
    if (row.DMFItemName && row.DMFItemNameEn) {
        $.commonPost("DMF/DMFItemSave", row, function (data) {
            console.log(data);
            row = data;
        });
    }
}

function filterWords(msg) {
    if (!isZH()) {
        return msg.replace("保存成功！", "Save Successfully !").replace("保存失败：项目名称和英文名称不能重复！", "Save failure: Tick-box Items and English name cannot be repeated!")
    }
    else {
        return msg;
    }
}

var maxDMFItemId = 0;
function Add() {
    var index = $table.bootstrapTable('getData').length;
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            DMFItemId: maxDMFItemId++,
            DMFItemName: '',
            DMFItemNameEn: '',
            DMFItemRemark: '',
            Budget: false,
            Status: false
        }
    });
}
function DeleteMarke() {
    var ids = $.map($table.bootstrapTable('getSelections'), function (row) {
        return row.DMFItemId ;
    });
    if (ids.length != 1) {
        layer.alert(isZH() ? "请选择一行删除!" : "Please select one line to delete!");
        return;
    }
    var requestData = { Id: ids[0] };
    $.ajax({
        type: "post",
        url: "/Master/MarketFundDelete",
        data: requestData,
        dataType: 'JSON',
        success: function (data, status) {
            if (data.returnValue) {
                //console.log('提交数据成功');
            }
        },
        error: function () {
            //layer.alert('删除失败');
        },
        complete: function () {
            $table.bootstrapTable('remove', {
                field: 'DMFItemId ',
                values: ids
            });
        }
    });

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
        //clientHeight = (document.body.clientHeight > document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
        clientHeight = document.documentElement.clientHeight;
    }
    else {
        //clientHeight = (document.body.clientHeight > document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
        clientHeight = document.documentElement.clientHeight;
    }
    return clientHeight;
}