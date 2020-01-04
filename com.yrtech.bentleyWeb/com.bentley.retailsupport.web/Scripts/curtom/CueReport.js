var $table = $('#myCueReport');

//车型列表
var carTypeArr = [];//[{ value: "飞驰", text: "飞驰" }, { value: "慕尚", text: "慕尚" }, { value: "欧陆GT", text: "欧陆GT" }, { value: "添越", text: "添越" }];
$.commonGet("Master/HiddenCodeSearch", {
    hiddenCodeGroup: "TargetModels",
    hiddenCode: ""
}, function (data) {
    data.forEach(function (item) {
        carTypeArr.push({ value: item.HiddenCodeId, text: isZH() ? item.HiddenCodeName : item.HiddenCodeNameEn })
    });
    InitCueLst();
})


function Del() {
    var rows = $table.bootstrapTable('getSelections');
    if (ids.length == 0) {
        layer.alert(isZH() ? "请选择一行删除!" : "Please select one line to delete!");
        return;
    }
    $.commonPost("MarketAction/MarketActionAfter2LeadsReportDelete", {
        UserId: $("G_UserId").val(),
        ListJson: JSON.stringify(rows)
    }, function () {
        console.log('删除数据成功');
    });
}

function Update() {
    var mainDto = {
        MarketActionId: $("#MarketActionId").val(),
        MarketActionAfter30LeadsReportUpdate: {
            MarketActionId: $("#MarketActionId").val(),
            InUserId: $("#G_UserId").val(),
            ModifyUserId:$("#G_UserId").val()
        }
    }
    
    $.commonPost("MarketAction/MarketActionAfter30LeadsReportUpdate", {
        UserId: $("G_UserId").val(),
        ListJson: JSON.stringify(mainDto)
    }, function () {
        layer.alert('更新成功');
    });
}

function Add() {
    var index = $table.bootstrapTable('getData').length;
    $table.bootstrapTable('insertRow', {
        // index: $table.bootstrapTable('getOptions').totalRows,
        index: index,
        row: {
            ShopName: $('#ShopName').val(),
            ActionName: $('#ActionName').val(),
            MarketActionId: $('#MarketActionId').val(),
            CustomerName: "",
            BPNO: '',
            OwnerCheck: false,
            DealCheck: false,
            LeadsCheck: false,
            TestDriverCheck: false,
            InterestedModel: '',
            InterestedModelName: '',
            InterestedModelNameEn: '',
            DealModelName: '',
            DealModelNameEn: '',
            DealModel: ''
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
             title: $("#Dealer").val(),
             field: "ShopName",
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
            title: $('#Promotion_Name').val(),
            field: 'ActionName',
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
            field: "",
            colspan: 7,
            rowspan: 1
        },
        {
            title: $('#ActualOrdered').val(),
            valign: "middle",
            align: "center",
            field: "",
            colspan: 2,
            rowspan: 1
        }],
        [{
            field: "CustomerName",
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
                    var result = { filed: "CustomerName", value: value };
                    var html = '<a href="javascript:void(0)" data-name="CustomerName" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="CustomerName" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                    }
                    return html;
                }
            }
        },
        {
            title: $('#BPNumber').val(),
            field: "BPNO",
            valign: "middle",
            align: "center",
            editable: {
                type: 'text',
                title: '',
                noeditFormatter: function (value, row, index) {
                    var result = { filed: "BPNO", value: value };
                    var html = '<a href="javascript:void(0)" data-name="BPNO" data-pk="undefined" data-value="" class="editable editable-click">' + result.value + '</a>';
                    if (!result.value) {
                        html = '<a href="javascript:void(0)" data-name="BPNO" data-pk="undefined" data-value="" class="editable editable-click">NULL</a>';
                    }
                    return html;
                }
            }
        },
          {
              title: $('#HasCars').val(),
              field: "OwnerCheck",
              valign: "middle",
              align: "center",
              formatter: function (value, row, index) {
                  var txml = '';
                  if (value)
                      txml = '<input id="{0}_OwnerCheck" type="checkbox" checked>';
                  else
                      txml = '<input id="{0}_OwnerCheck" type="checkbox">';
                  txml = txml.replace("{0}", index);

                  return txml;
              }
          },
          {
              field: "TestDriverCheck",
              title: $('#WillJoin').val(),
              valign: "middle",
              align: "center",
              formatter: function (value, row, index) {
                  var txml = '';
                  if (value)
                      txml = '<input id="{0}_TestDriverCheck" type="checkbox" checked >';
                  else
                      txml = '<input id="{0}_TestDriverCheck" type="checkbox">';
                  txml = txml.replace("{0}", index);

                  return txml;
              }
          },
        {
            title: $('#IsLeads').val(),
            field: "LeadsCheck",
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                var txml = '';
                if (value)
                    txml = '<input id="{0}_LeadsCheck" type="checkbox" checked >';
                else
                    txml = '<input id="{0}_LeadsCheck" type="checkbox">';
                txml = txml.replace("{0}", index);

                return txml;
            }
        },
        // lipeng  add 感兴趣车型
        {

            title: $('#LikeCar').val(),
            field: "InterestedModel",
            valign: "middle",
            align: "center",
            editable: {
                type: 'select',
                title: '请选择车型',
                source: carTypeArr,
            }
        },
        {
            title: $('#WhetherDeal').val(),
            field: "DealCheck",
            valign: "middle",
            align: "center",
            formatter: function (value, row, index) {
                var txml = '';
                if (value)
                    txml = '<input id="{0}_DealCheck" type="checkbox" checked >';
                else
                    txml = '<input id="{0}_DealCheck" type="checkbox">';
                txml = txml.replace("{0}", index);

                return txml;
            }
        },
        {//成交车型            
            title: $('#ModelSold').val(),
            field: "DealModel",
            valign: "middle",
            align: "center",
            editable: {
                type: 'select',
                title: '请选择车型',
                source: carTypeArr
            }
        }
        ]],
        onClickCell: function (field, value, row, $element) {
            if ($element.find(":checkbox").length == 0) {
                return false;
            }
            $element.find(":checkbox").prop("checked",true);
            row[field] = !value;
            $.commonPost("MarketAction/MarketActionAfter2LeadsReportSave", {
                UserId: $("#G_UserId").val(),
                ListJson: JSON.stringify([row])
            }, function () {
                loadCueReport();
            });
        },
        onClickRow: function (row, $element) {
            console.log("onClickRow");
            curRow = row;
        },
        onEditableSave: function (field, row, oldValue, $el) {
            console.log("onEditableSave");
            $.commonPost("MarketAction/MarketActionAfter2LeadsReportSave", {
                UserId: $("#G_UserId").val(),
                ListJson: JSON.stringify([row])
            }, function () {
                loadCueReport();
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
    url = url.replace('{0}', $("#MarketActionId").val());

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