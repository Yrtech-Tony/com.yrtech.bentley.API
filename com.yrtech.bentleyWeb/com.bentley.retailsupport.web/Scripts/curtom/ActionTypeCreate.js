$('#actionTypeCreate').bootstrapValidator({
    message: isZH() ? '该数据无效' : 'This value is not valid',
    feedbackIcons: {
        valid: 'glyphicon glyphicon-ok',
        invalid: 'glyphicon glyphicon-remove',
        validating: 'glyphicon glyphicon-refresh'
    },
    fields: {
        ActionTypeName: {
            message: isZH() ? '活动类型名称无效' : 'The Event Category name is not valid',
            validators: {
                notEmpty: {
                    message: isZH() ? '活动类型名称不能为空' : 'The Event Category name cannot be empty',
                },
                stringLength: {
                    min: 2,
                    max: 30,
                    message: isZH() ? '活动类型名称不能小于2个字符' : 'The event type name cannot be less than 2 characters',
                }
            }
        },
        MaximumAmount: {
            message: isZH() ? '最大申请金额无效' : 'The maximum application amount is invalid',
            validators: {
                notEmpty: {
                    message: isZH() ? '最大申请金额不能为空' : 'The maximum application amount cannot be blank',
                },
                regexp: {
                    regexp: /^[0-9_\.]+$/,
                    message: isZH() ? '最大申请金额只能是数字' : 'The maximum application amount can only be figures',
                }
            }
        }
    }
});