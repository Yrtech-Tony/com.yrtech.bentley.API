$('#agentCreate').bootstrapValidator({
    message: isZH()?'该数据无效':'This value is not valid',
    feedbackIcons: {
        valid: 'glyphicon glyphicon-ok',
        invalid: 'glyphicon glyphicon-remove',
        validating: 'glyphicon glyphicon-refresh'
    },
    fields: {
        DealerName: {
            message: isZH() ? '名称无效' : 'Invalid name',
            validators: {
                notEmpty: {
                    message: isZH() ? '名称不能为空' : 'The name cannot be empty',
                },
                stringLength: {
                    min: 2,
                    max: 30,
                    message: isZH() ? '名称不能小于2个字符' : 'The name cannot be less than 2 characters',
                }
            }
        },
        City: {
            message: isZH() ? '城市无效' : 'Invalid city name',
            validators: {
                notEmpty: {
                    message: isZH()?'城市不能为空':'The city name cannot be empty',
                },
                stringLength: {
                    min: 2,
                    max: 30,
                    message: isZH() ? '城市名称不能小于2个字符' : 'The city name cannot be less than 2 characters',
                }
            }
        }
    }
});