var $table = $('#myIncomeCost');
var mytxt;
var marketfunddata;
var role = $('#G_RoleName').val();
var hdData = eval($('#_hd_dealer').val());

if (role == "经销商") {
    $('#pdbuttons2').hide();
}
InitIncomeCostList();

$(window).resize(function () {
    $('#myIncomeCost').bootstrapTable('destroy');// 销毁表格数据
    InitIncomeCostList();

});

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


function InitIncomeCostList() {
    //生成用户数据
    $('#myIncomeCost').bootstrapTable({
        pagination: true,
        striped: true, //是否显示行间隔色
        height: getClientHeight() - 280 + 80,
        showColumns: false, // 开启自定义列显示功能
        sortable: true,
        sortName: 'Name',
        sortOrder: 'asc',
        pageNumber: 1,
        pageSize: 10,
        pageList: [5, 10, 20, 50],
        queryParams: function (params) {
            return {
                pagesize: this.pageSize,
                currentpage: this.pageNumber,
                dealerId:$('#pdealer').val(),
                userId: $('#G_UserId').val()
            }
        },
        columns:
            [
                {
                    checkbox: true,
                    valign: "middle",
                    align: "center"
                },
                {
                    title: $('#NO').val(),
                    field: 'Id',
                    width: 30,
                    valign: "middle",
                    align: "center",
                    formatter: function (value, row, index) {
                        return index + 1;
                    }
                },
        {
            title: $('#YearMonth').val(),
            field: "YearAndMonth",
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
                    if (role != "经销商") {
                        var html = '<a href="javascript:void(0)" data-name="YearAndMonth" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                        if (result.value == "") {
                            html = '<a href="javascript:void(0)" data-name="YearAndMonth" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                        }
                        return html;
                    } else { return result.value; }
                }
            }
        },
            {
                title: $('#Dealer').val(),
                field: 'DealerId',
                width: "300px",
                valign: "middle",
                align: "center",
                sortable: false,
                align: 'left',
                formatter: function (value, row, index) {
                    var txml = '<select id=' + row.ID + ' selected="true" length="1">';
                    var mySelect = document.getElementById('pdealer');
                    for (i = 0; i < mySelect.length ; i++) {
                        txml += '<option value=\'' + mySelect.options[i].value + '\'>' + mySelect.options[i].text + '</option>';
                    }
                    txml += '</select>'
                    return txml;
                },

                editable: {
                    type: 'select',
                    //title: $('#Dealer').val(),
                    source: hdData,
                    validate: function (v) {
                        if (!v) return isZH() ? '经销商不能为空' : 'The dealer cannot be empty.';
                    },
                    noeditFormatter: function (value, row, index) {
                        var result = { filed: "DealerId", value: value };
                        var real = '';
                        for (var i = 0; i < hdData.length; i++) {
                            if (hdData[i].value == value) {
                                real = hdData[i].text;
                            }
                        }
                        if (role != "经销商") {
                            var html = '<a href="javascript:void(0)" data-name="DealerId" data-pk="undefined" data-value="" class="editable editable-click">' + real + '</a>';
                            if (result.value == "" || result.value == undefined) {
                                html = '<a href="javascript:void(0)" data-name="DealerId" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                            }
                            return html;
                        } else { return real; }
                    }
                }

            },
        {
            title: $('#AAH').val(),
            field: "Number",
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
                    var result = { filed: "Number", value: value };
                    if (role != "经销商") {
                        var html = '<a href="javascript:void(0)" data-name="Number" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                        if (result.value == "") {
                            html = '<a href="javascript:void(0)" data-name="Number" data-pk="undefined" data-value="" class="editable editable-click">0</a>';
                        }
                        return html;
                    } else { return result.value; }
                }
            }
        },
        {
            title: $('#RMB').val(),
            valign: "middle",
            align: "center",
            field: "Income",
            editable: {
                type: 'text',
                //title: rmb,
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
                    var result = { filed: "Income", value: value };
                    if (role != "经销商") {
                        var html = '<a href="javascript:void(0)" data-name="Income" data-pk="undefined" data-value="" class="editable editable-click">' + dealNumber(result.value) + '</a>';
                        if (result.value == "") {
                            html = '<a href="javascript:void(0)" data-name="Income" data-pk="undefined" data-value="" class="editable editable-click">0</a>';
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
        }
        , onLoadSuccess: function (data) {
        },
        onEditableSave: function (field, row, oldValue, $el) {


            if ((row.YearAndMonth != undefined && row.YearAndMonth != '') && (row.DealerId != undefined && row.DealerId != '')) {
                $.ajax({
                    type: "post",
                    url: "/BudgetCost/IncomeCostEdit",
                    data: row,
                    dataType: 'JSON',
                    success: function (data, status) {
                        if (data.returnValue) {
                            //console.log('提交数据成功');
                            curRow.ID = data.EntityID;
                        } else {
                            //layer.alert(data.message);

                            layer.alert(data.message, {
                                skin: 'layui-layer-molv', closeBtn: 0
                            }, function () {
                                window.location.reload();
                            });
                        }
                    },
                    error: function () {
                        layer.alert(isZH() ? '编辑失败' : 'Edit failure');
                    },
                    complete: function () {

                    }

                });
            }
        }
    });

}

function DeleteBudgetCost() {
    var ids = $.map($table.bootstrapTable('getSelections'), function (row) {
        return row.Id;
    });
    if (ids.length != 1) {
        layer.alert(isZH() ? "请选择一行删除!" : "Please select one line to delete!");
        return;
    }
    var requestData = { baseId: ids[0] };
    $.ajax({
        type: "post",
        url: "/BudgetCost/IncomeCostDel",
        data: requestData,
        dataType: 'JSON',
        success: function (data, status) {
            if (data.returnValue) {
                console.log('提交数据成功');
            }
        },
        error: function () {
            //layer.alert('删除失败');
        },
        complete: function () {
            $table.bootstrapTable('remove', {
                field: 'Id',
                values: ids
            });
        }
    });

}


function SetIsOwner(target) {
    //curRow.IsOwner = target.checked;

}

function uuid() {
    var s = [];
    var hexDigits = "0123456789abcdef";
    for (var i = 0; i < 36; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
    }
    s[14] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
    s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01
    s[8] = s[13] = s[18] = s[23] = "-";

    var uuid = s.join("");
    return uuid;
}
function simple_uuid(index) {
    var s = "00000000-0000-0000-0000-000000000000";
    s = s.substring(0, s.length - index.length) + index;
    return s;
}

function Add() {
    var index = $table.bootstrapTable('getData').length;
    $table.bootstrapTable('insertRow', {
        index: index,
        row: {
            ID: simple_uuid(index.toString()),
            DealerId: '',
            Income: 0,
            Number: 0,
            YearAndMonth: ''

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
function NameQuery() {
	$('#myIncomeCost').bootstrapTable(
        "refresh",
        {
        	url: "/BudgetCost/IncomeCostList"
        }
    );
}