$('#PromotionCreate').bootstrapValidator({
    message: isZH() ? '该数据无效' : 'The data is invalid',
    feedbackIcons: {
        valid: 'glyphicon glyphicon-ok',
        invalid: 'glyphicon glyphicon-remove',
        validating: 'glyphicon glyphicon-refresh'
    },
    fields: {
        ActionName: {
            trigger: "change",
            message: isZH() ? '活动名称无效' : 'Invalid Action Name of activity',
            validators: {
                notEmpty: {
                    message: isZH() ? '活动名称不能为空' : 'The activity name cannot be empty',
                }
            }
        },
        StartDate: {
            message: isZH() ? '起止日期无效' : 'The start date is invalid',
            trigger: "change",
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
            trigger: "change",
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
        ActivityBudget: {
            message: isZH() ? '活动预算无效' : 'The activity budget is invalid',
            validators: {
                notEmpty: {
                    message: isZH() ? '活动预算不能为空' : 'The activity budget cannot be blank',
                },
                regexp: {
                    regexp: /^[0-9_\.]+$/,
                    message: isZH() ? '活动预算只能是数字' : 'The activity budget can only be figures',
                }
            }
        },
        ExpectLeadsCount: {
            message: isZH() ? '预计线索无效' : 'The expect leads is invalid',
            validators: {
                notEmpty: {
                    message: isZH() ? '预计线索不能为空' : 'The expect leads cannot be blank',
                },
                regexp: {
                    regexp: /^\+?[1-9]\d*$/,
                    message: isZH() ? '预计线索只能是整数' : 'The expect leads only integer accepted',
                }
            }
        }
    }
});
