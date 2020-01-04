$('#PromotionCreate').bootstrapValidator({
    message: isZH() ? '该数据无效' : 'The data is invalid',
    feedbackIcons: {
        valid: 'glyphicon glyphicon-ok',
        invalid: 'glyphicon glyphicon-remove',
        validating: 'glyphicon glyphicon-refresh'
    },
    fields: {
        ActionName: {
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
        }
    }
});
