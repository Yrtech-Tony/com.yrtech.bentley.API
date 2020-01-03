var $table = $('#myCueReport');

//车型列表
var carTypeArr = [{ value: "飞驰", text: "飞驰" }, { value: "慕尚", text: "慕尚" }, { value: "欧陆GT", text: "欧陆GT" }, { value: "添越", text: "添越" }];

InitCueLst();

//$(window).resize(function () {
//    $('#myCueReport').bootstrapTable('destroy');// 销毁表格数据
//    InitCueLst();
//});

function Del() {
    var ids = $.map($table.bootstrapTable('getSelections'), function (row) {
        return row.ID;
    });
    if (ids.length != 1) {
        layer.alert(isZH() ? "请选择一行删除!" : "Please select one line to delete!");
        return;
    }
    var requestData = { baseId: ids[0] };
    $.ajax({
        type: "post",
        url: "/Editable/CueDelete",
        data: requestData,
        dataType: 'JSON',
        success: function (data, status) {
            if (data.returnValue) {
                console.log('提交数据成功');
            }
        },
        error: function () {
            layer.alert(isZH() ? '编辑失败' : 'Edit failure');
        },
        complete: function () {
            $table.bootstrapTable('remove', {
                field: 'ID',
                values: ids
            });
        }

    });

}

function Update() {
    var promotionId = $('#ID').val();
    var requestData = { baseId: promotionId, requestrequestType: "P4" };
    $.ajax({
        type: "post",
        url: "/Editable/CueUpdateState",
        data: requestData,
        dataType: 'JSON',
        success: function (data, status) {
            if (data.returnValue) {
                layer.alert('更新成功');
            }
        },
        error: function () {
            layer.alert('更新失败');
        }
    });
}

function Add() {
    var index = $table.bootstrapTable('getData').length;
    $table.bootstrapTable('insertRow', {
        // index: $table.bootstrapTable('getOptions').totalRows,
        index: index,
        row: {
            Dealer: $('#Dealer').val(),
            DealerId: $('#DealerId').val(),
            PromotionName: $('#Name').val(),
            PromotionId: $('#ID').val(),
            CueOwner: "",
            CueMobilePhone: '',
            CueBP: '',
            IsOwner: false,
            IsDeal: false,
            IsAttend: false,
            IsClue: false,
            Volume: 0,
            Model: ''
        }
    });
}

var curRow = {};

function InitCueLst() {
    //生成用户数据
    $('#myCueReport').bootstrapTable({
        pagination: true,
        toolbar: '#toolbar',//指定工具栏
        striped: true, //是否显示行间隔色
        height: getClientHeight() - 280 + 80,
        showColumns: false, // 开启自定义列显示功能
        sortable: true,
        pageNumber: 1,
        pageSize: 100000,
        sortName: 'SeqNO',
        sortOrder: 'asc',
        columns: [
        [{
            checkbox: true,
            valign: "middle",
            align: "center",
            colspan: 1,
            rowspan: 2
        },
        {
            title: $('#NO').val(),//NO
            field: '#',
            width: 30,
            valign: "middle",
            align: "center",
            colspan: 1,
            rowspan: 2,
            formatter: function (value, row, index) {
                return index + 1;
            }
        },
         {
             title: "{{'Dealer'|translate}}",
             field: "Dealer",
             width: "100px",
             valign: "middle",
             align: "center",
             colspan: 1,
             rowspan: 2,
             formatter: function (value, row, index) {
                 return '<div style="min-width:100px">' + value + '</div>';
             }
         },
        {
            title: $('#Promotion_Name').val(),//"{{'Event Name'|translate}}",
            field: 'PromotionName',
            width: "200px",
            valign: "middle",
            align: "center",
            colspan: 1,
            rowspan: 2,
            align: 'left'
        },
        {
            title: $('#CustomerIL').val(),
            valign: "middle",
            align: "center",
            field: "RCue",
            colspan: 7,
            rowspan: 1
        },
        {
            title: $('#ActualOrdered').val(),
            valign: "middle",
            align: "center",
            field: "RDeal",
            colspan: 2,
            rowspan: 1
        }],
        [{
            field: "CueOwner",
            title: $('#CustomerName').val(),
            valign: "middle",
            align: "center",
            editable: {
                type: 'text',
                title: '',
                validate: function (v) {
                    if (!v) return isZH() ? '客户姓名不能为空' : 'Customer name cannot be empty';
                },
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "CueOwner", value: value };
                    var html = '<a href="javascript:void(0)" data-name="CueOwner" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                    if (result.value == "") {
                        html = '<a href="javascript:void(0)" data-name="CueOwner" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            field: "CueBP",
            title: $('#BPNumber').val(),
            valign: "middle",
            align: "center",
            editable: {
                type: 'text',
                title: '',
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "CueBP", value: value };
                    var html = '<a href="javascript:void(0)" data-name="CueBP" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                    if (result.value == "") {
                        html = '<a href="javascript:void(0)" data-name="CueBP" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                    }
                    return html;
                }
            }
        },
          {
              field: "IsOwner",
              title: $('#HasCars').val(),
              valign: "middle",
              align: "center",
              formatter: function (value, row, index) {
                  var txml = '';
                  if (value)
                      txml = '<input id="{0}_IsOwner" type="checkbox" checked>';
                  else
                      txml = '<input id="{0}_IsOwner" type="checkbox">';
                  txml = txml.replace("{0}", row.ID);

                  return txml;
              }
          },
          {
              field: "IsAttend",
              title: $('#WillJoin').val(),
              valign: "middle",
              align: "center",
              formatter: function (value, row, index) {
                  var txml = '';
                  if (value)
                      txml = '<input id="{0}_IsAttend" type="checkbox" checked >';
                  else
                      txml = '<input id="{0}_IsAttend" type="checkbox">';
                  txml = txml.replace("{0}", row.ID);

                  return txml;
              }
          },
        {
            field: "IsClue",
            title: $('#IsLeads').val(),
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                var txml = '';
                if (value)
                    txml = '<input id="{0}_IsClue" type="checkbox" checked >';
                else
                    txml = '<input id="{0}_IsClue" type="checkbox">';
                txml = txml.replace("{0}", row.ID);

                return txml;
            }
        },
        // lipeng  add 感兴趣车型
        {
            field: "LikeModel",
            title: $('#LikeCar').val(),
            valign: "middle",
            align: "center",
            editable: {
                type: 'select',
                title: '请选择车型',
                source: carTypeArr,
            }
        },
        {
            field: "IsDeal",
            title: $('#WhetherDeal').val(),
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                var txml = '';
                if (value)
                    txml = '<input id="{0}_IsDeal" type="checkbox" checked >';
                else
                    txml = '<input id="{0}_IsDeal" type="checkbox">';
                txml = txml.replace("{0}", row.ID);

                return txml;
            }
        },
        {//成交车型
            field: "Model",
            title: $('#ModelSold').val(),
            valign: "middle",
            align: "center",
            editable: {
                type: 'select',
                title: '请选择车型',
                source: carTypeArr,

                //noeditFormatter: function (value, row, index) {
                //    var result = { filed: "Model", value: value };
                //    var html = '<a href="javascript:void(0)" data-name="Model" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                //    if (result.value == "") {
                //        html = '<a href="javascript:void(0)" data-name="Model" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                //    }
                //    return html;
                //}
            }
        }
        ]],
        onClickCell: function (field, value, row, $element) {
            console.log(field, value, row, $element);
            console.log("onClickCell");
            if (field != "IsOwner" && field != "IsDeal" && field != "IsAttend" && field != "IsClue") {
                return false;
            }

            if (field == "IsOwner") {
                try {
                    $('#id'.replace('id', row.ID) + '_IsOwner')[0].checked = !value;
                } catch (e) {

                }

                curRow = row;
                curRow.IsOwner = !value;
            }
            if (field == "IsDeal") {

                try {
                    $('#id'.replace('id', row.ID) + '_IsDeal')[0].checked = !value;
                } catch (e) {

                }
                curRow = row;
                curRow.IsDeal = !value;
            }
            if (field == "IsAttend") {

                try {
                    $('#id'.replace('id', row.ID) + '_IsAttend')[0].checked = !value;
                } catch (e) {

                }
                curRow = row;
                curRow.IsAttend = !value;
            }
            if (field == "IsClue") {

                try {
                    $('#id'.replace('id', row.ID) + '_IsClue')[0].checked = !value;
                } catch (e) {

                }
                curRow = row;
                curRow.IsClue = !value;
            }
            console.log("ajax");
            $.ajax({
                type: "post",
                url: "/Editable/CueEdit",
                data: curRow,
                dataType: 'JSON',
                success: function (data, status) {
                    if (data.returnValue) {
                        //console.log('提交数据成功');
                        curRow.ID = data.EntityID;
                    }
                    //window.location.reload();
                },
                error: function () {
                    layer.alert(isZH() ? '编辑失败' : 'Edit failure');
                },
                complete: function () {

                }
            });
        },
        onClickRow: function (row, $element) {
            console.log("onClickRow");
            curRow = row;
        },
        onEditableSave: function (field, row, oldValue, $el) {
            console.log("onEditableSave");
            $.ajax({
                type: "post",
                url: "/Editable/CueEdit",
                data: row,
                dataType: 'JSON',
                success: function (data, status) {
                    if (data.returnValue) {
                        //console.log('提交数据成功');
                        curRow.ID = data.EntityID;
                    }
                    //window.location.reload();
                },
                error: function () {
                    layer.alert(isZH() ? '编辑失败' : 'Edit failure');
                },
                complete: function () {

                }

            });
        }

    });
}
function SetIsOwner(target) {
    //curRow.IsOwner = target.checked;

}

function EmptyValue() {
    window.localStorage.Empty = "true";
}


function downloadFile() {

    var url = "/Master/DownloadCueLst?Id={0}";
    url = url.replace('{0}', $("#ID").val());

    var iframe = document.createElement("iframe");
    iframe.src = url;

    iframe.style.width = "100%";
    iframe.style.height = "100%";
    iframe.style.display = 'none';
    var div = document.getElementById('footer');
    div.appendChild(iframe);

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