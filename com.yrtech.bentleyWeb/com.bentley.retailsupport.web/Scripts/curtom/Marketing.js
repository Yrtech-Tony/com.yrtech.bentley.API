function SetValue() {
    if (window.localStorage.Empty != "true") {
        window.localStorage.pname = "";
        window.localStorage.pyear = "";
        window.localStorage.pmonth = "";
        window.localStorage.pstate = "";
        window.localStorage.pdealer = "";
        window.localStorage.ptype = "";
    }
    else {
        window.localStorage.Empty = "false";
    }
    $("#pname").val(window.localStorage.pname);
    $("#pyear").val(window.localStorage.pyear);
    $("#pmonth").val(window.localStorage.pmonth);
    $("#pstate").val(window.localStorage.pstate);
    $("#pdealer").val(window.localStorage.pdealer);
    $("#ptype").val(window.localStorage.ptype);
}
function EmptyValue() {
    window.localStorage.Empty = "true";
}

function HideFiledDealer() {
    var roleTypeCode = $('#G_RoleTypeCode').val();
    if (roleTypeCode == "SHOP") {
        $('#myMarketing').bootstrapTable('hideColumn', isZH() ? 'ShopName' : 'ShopNameEn');
    }
}

function NameQuery() {
    window.localStorage.pname = $("#pname").val();
    window.localStorage.pyear = $("#pyear").val();
    window.localStorage.pmonth = $("#pmonth").val();
    window.localStorage.pstate = $("#pstate").val();
    window.localStorage.pdealer = $("#pdealer").val();
    window.localStorage.ptype = $("#ptype").val();

    loadMarketings(true);
}

var types = isZH() ? ['数字营销', '广告及宣传', '线上平台线索获取'] : ['Digital Marketing', 'Advertisement', 'Online Sales Leads Generation'];
function statusFormatter(value, row, index, field) {
    if (row.MarketActionStatusNameEn == "Canceled") {
        return '';
    }

    if (field == "Before3Days" || field == "TheDays") {
        if (isZH()) {
            if (types.indexOf(row.EventTypeName) >= 0) {
                return '';
            }
        } else {
            if (types.indexOf(row.EventTypeNameEn) >= 0) {
                return '';
            }
        }
    }

    var href = '/Marketing/' + field + "?id=" + row.MarketActionId;
    if (field == "After2Days") {
        href += "&ShopName=" + (isZH() ? row.ShopName : row.ShopNameEn);
        href += "&ActionName=" + row.ActionName;
    }
    if (value == "Commited") {
        var txt = $("#Pending").val();
        return '<a href="' + href + '" onclick="EmptyValue();" class="btn btn-primary btn-sm" style="background-color:gray !important;border-color:lightgrey !important;"><i class="icon-plus" style="padding-right:5px;"></i><span style="color: white;">' + txt + '</span></a>';
    } else if (value == "UnCommitTime") {
        var txt = $("#Draft").val();
        return '<a href="' + href + '" onclick="EmptyValue();" class="btn btn-primary btn-sm" style=""><i class="icon-plus" style="padding-right:5px;"></i><span style="color: white;">' + txt + '</span></a>';
    } else if (value == "UnCommit") {
        var txt = $("#Draft").val();
        return '<a href="' + href + '" onclick="EmptyValue();" class="btn btn-primary btn-sm" style="background-color:#E0E0E0 !important;border-color:lightgrey !important;"><i class="icon-plus" style="padding-right:5px;"></i><span style="color: white;" >' + txt + '</span></a>';
    }
}

function nameFormatter(value, row, index) {
    return ' <div style="min-width:180px;"><a href="/Marketing/Create?Id=' + row.MarketActionId + '" onclick="EmptyValue();">' + value + '</a></div>';
}

function dateFormatter(value, row, index) {
    if (value) {
        var date = value.substr(0, value.indexOf(' ')).replace(/-/g, '\/');
        return date;
    }
    return "";
}

function expenseAccountFormatter(value, row, index) {
    var chk = (value) ? 'checked' : '';
    return '<input class="check" type="checkbox" ' + chk + '/>';
}

function loadMarketings(refresh) {
    $.commonGet("MarketAction/MarketActionSearch", {
        actionName: $('#pname').val() || '',
        year: $('#pyear').val() || '',
        month: $('#pmonth').val() || '',
        marketActionStatusCode: $('#pstate').val() || '',
        shopId: $('#pdealer').val() || ' ',
        eventTypeId: $('#ptype').val() || ' ',
        userId: $("#G_UserId").val(),
        roleTypeCode: $("#G_RoleTypeCode").val(),
        expenseAccountChk: ''
    }, function (data) {
        if (data) {
            $('#myMarketing').bootstrapTable("load", data);
            $("#myMarketing").bootstrapTable("selectPage", parseInt(window.localStorage.pageNumberMarket)||1);
        }
    });
}

function initTable() {
    var columns = [[{
        title: $("#NO").val(),
        field: '',
        align: 'center',
        valign: 'middle',
        formatter: function (value, row, index) {
            return index + 1;
        },
        rowspan: 2
    }, {
        title: $("#G_ReimbursementApplication").val(),
        field: 'ExpenseAccount',
        align: 'center',
        valign: 'middle',
        rowspan: 2,
        formatter: expenseAccountFormatter,
        events: {
            'change .check': function (e, value, row, index) {
                var chhecked = $(e.target).is(":checked");
                $.commonGet("MarketAction/MarketActionSearchById", { marketActionId: row.MarketActionId }, function (data) {
                    if (data && data.length > 0) {
                        data[0].ExpenseAccount = chhecked;
                        $.commonPost('MarketAction/MarketActionSave', data[0], function (res) {
                            //window.location.href = "/Marketing/Index";
                        })
                    }
                });
            }
        }
    }, {
        title: $("#G_Dealer").val(),
        field: isZH() ? 'ShopName' : 'ShopNameEn',
        width: "100px",
        align: 'center',
        valign: 'middle',
        sortable: true,
        rowspan: 2,
        formatter: function (value, row, index) {
            return '<div style="min-width:100px">' + value + '</div>';
        }
    }, {
        title: $("#G_Status").val(),
        field: 'MarketActionStatusCode',
        align: 'center',
        valign: 'middle',
        sortable: true,
        rowspan: 2,
        formatter: PSFromat
    }, {
        title: $("#G_ActivityName").val(),
        field: 'ActionName',
        align: 'center',
        valign: 'middle',
        sortable: true,
        rowspan: 2,
        formatter: nameFormatter
    }, {
        title: $("#G_EventType").val(),
        field: isZH() ? 'EventTypeName' : 'EventTypeNameEn',
        align: 'center',
        valign: 'middle',
        sortable: true,
        rowspan: 2
    }, {
        title: $("#G_StartDate").val(),
        field: 'StartDate',
        align: 'center',
        valign: 'middle',
        sortable: true,
        rowspan: 2,
        formatter: dateFormatter
    }, {
        title: $("#G_EndDate").val(),
        field: 'EndDate',
        align: 'center',
        valign: 'middle',
        sortable: true,
        rowspan: 2,
        formatter: dateFormatter
    }, {
        title: $("#G_ThreeWeeksBefore").val(),
        field: '',
        align: 'center',
        valign: 'middle'
    }, {
        title: $("#G_ThreeDaysBefore").val(),
        field: '',
        align: 'center',
        valign: 'middle'
    }, {
        title: $("#G_Today").val(),
        field: '',
        align: 'center',
        valign: 'middle'
    }, {
        title: $("#G_AfterDays2").val(),
        field: '',
        align: 'center',
        valign: 'middle'
    }, {
        title: $("#G_AfterDays7").val(),
        field: '',
        align: 'center',
        valign: 'middle'
    }, {
        title: $("#G_AfterMonth1").val(),
        field: '',
        align: 'center',
        valign: 'middle'
    }, {
        title: $("#G_AfterMonth3").val(),
        field: '',
        align: 'center',
        valign: 'middle'
    }], [{
        title: $("#PlanPPT").val(),
        field: 'Before3Weeks',
        align: 'center',
        valign: 'middle',
        formatter: function (value, row, index) {
            return statusFormatter(value, row, index, 'Before3Weeks');
        }
    }, {
        title: $("#FinalPPT").val(),
        field: 'Before3Days',
        align: 'center',
        valign: 'middle',
        formatter: function (value, row, index) {
            return statusFormatter(value, row, index, 'Before3Days');
        }
    }, {
        title: $("#G_OnSiteSign").val(),
        field: 'TheDays',
        align: 'center',
        valign: 'middle',
        formatter: function (value, row, index) {
            return statusFormatter(value, row, index, 'TheDays');
        }
    }, {
        title: $("#G_ClueReport").val(),
        field: 'After2Days',
        align: 'center',
        valign: 'middle',
        formatter: function (value, row, index) {
            return statusFormatter(value, row, index, 'After2Days');
        }
    }, {
        title: $("#G_EventReport").val(),
        field: 'After7Days',
        align: 'center',
        valign: 'middle',
        formatter: function (value, row, index) {
            return statusFormatter(value, row, index, 'After7Days');
        }
    }, {
        title: $("#G_UpdateReport").val(),
        field: 'After1Months',
        align: 'center',
        valign: 'middle',
        formatter: function (value, row, index) {
            return statusFormatter(value, row, index, 'After1Months');
        }
    }, {
        title: $("#G_ProofDocument").val(),
        field: 'After3Months',
        align: 'center',
        valign: 'middle',
        formatter: function (value, row, index) {
            return statusFormatter(value, row, index, 'After3Months');
        }
    }]];

    $("#myMarketing").bootstrapTable({
        columns: columns,
        pagination: true,
        striped: true, //是否显示行间隔色
        height: getClientHeight() - 280 + 80,
        showColumns: false, // 开启自定义列显示功能
        sortable: true,
        sortName: 'StartDate',
        sortOrder: 'desc',
        pageSize: 20,
        pageList: [5, 10, 20, 50],
        onPageChange: function (number, size) {
            window.localStorage.pageNumberMarket = number;
        }
    });
}

function refreshTable(data) {
    $("#myMarketing").bootstrapTable("load", {
        data: data
    });
}

function EnterPress(e) { //传入 event
    var e = e || window.event;
    if (e.keyCode == 13) { //13代表回车符
        NameQuery();
    }
}

function downloadByClueReportFile() {
    $.commonGet("MarketAction/MarketActionAllLeadsReportExport", { 
        year: '',  
        userId: $("#G_UserId").val(),
        roleTypeCode: $("#G_RoleTypeCode").val()
    }, function (data) {
        if (data) {
            window.location.href = host + "download/DownloadFile?filepath=" + data.FilePath;
        }
    })
}

function downloadFile() {
    $.commonGet("MarketAction/MarketActionExport", {
        actionName: $('#pname').val() || '',
        year: $('#pyear').val() || '',
        month: $('#pmonth').val() || '',
        marketActionStatusCode: $('#pstate').val() || '',
        shopId: $('#pdealer').val() || ' ',
        eventTypeId: $('#ptype').val() || ' ',
        userId: $("#G_UserId").val(),
        roleTypeCode: $("#G_RoleTypeCode").val(),
        expenseAccountChk: ''
    }, function (data) {
        if (data) {
            window.location.href = host + "download/DownloadFile?filepath=" + data.FilePath;
        }
    })
}

function PSFromat(value, row, index) {
    var displayValue = isZH() ? row.MarketActionStatusName : row.MarketActionStatusNameEn;
    switch (value) {
        case '1':
            return '<span style="color: green; border: 1px solid">' + displayValue + '</span>';
        case '2':
            return '<span style="color: red; ">' + displayValue + '</span>';
        case '3':
            return '<span style="color: green;">' + displayValue + '</span>';
        case '4':
            return '<span style="color:gray;">' + displayValue + '</span>';
        default:
            return '<span style="color: green; border: 1px solid">' + displayValue + '</span>';

    }
}
