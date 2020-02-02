$('#areaCreate').bootstrapValidator({
    message: isZH() ? '该数据无效' : 'This value is not valid',
    feedbackIcons: {
        valid: 'glyphicon glyphicon-ok',
        invalid: 'glyphicon glyphicon-remove',
        validating: 'glyphicon glyphicon-refresh'
    },
    fields: {
        AreaName: {
            message: isZH() ? '名称无效' : 'Invalid name',
            validators: {
                notEmpty: {
                    message: isZH() ? '名称不能为空' : 'The name cannot be empty',
                },
                stringLength: {
                    min: 1,
                    max: 30,
                    message: isZH() ? '名称不能小于2个字符' : 'The name cannot be less than 2 characters',
                }
            }
        }
    }
});