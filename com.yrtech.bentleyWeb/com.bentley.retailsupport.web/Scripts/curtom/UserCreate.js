$('#userCreate').bootstrapValidator({
    message: isZH()?'该数据无效':'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            AccountName: {
                message: isZH()?'姓名无效':'The username is not valid',
                validators: {
                    notEmpty: {
                        message: isZH()?'姓名不能为空':'The username is required and cannot be empty'
                    },
                    stringLength: {
                        min: 2,
                        max: 30,
                        message: isZH()?'姓名不能小于2个字符':'The username must be more than 2 and less than 30 characters long'
                    }
                }
            },
            AccountId: {
                message: isZH()?'用户名无效':'The AccountName is not valid',
                validators: {
                    notEmpty: {
                        message:isZH()? '用户名不能为空':'The AccountName is required and cannot be empty'
                    },
                    stringLength: {
                        min: 2,
                        max: 30,
                        message: isZH()?'用户名不能小于2个字符':'The AccountName must be more than 4 and less than 30 characters long'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9_\.]+$/,
                        message: isZH()?'用户名只能由字母和数字组成':'The username can only consist of alphabetical, number, dot and underscore'
                    }
                }
            },
            Password: {
                validators: {
                    notEmpty: {
                        message: isZH()?'密码不能为空':'The password is required and cannot be empty',
                    }
                }
            },
            Email: {
                validators: {
                    //notEmpty: {
                    //    message: isZH() ?'邮箱不能为空':'The Email is required and cannot be empty',
                    //},
                    emailAddress: {
                        message: isZH()?'输入不是有效的电子邮件地址':'The input is not a valid email address',
                    }
                }
            },
            Phone: {
                message: isZH() ?'手机号码无效':'The Phone is not valid,//',
                validators: {
                    //notEmpty: {
                    //    message: isZH() ? '手机号码不能为空' : 'The phone number can\'t be empty',//'The Phone is required and cannot be empty'
                    //},
                    stringLength: {
                        min: 11,
                        max: 11,
                        message: isZH()?'请输入11位手机号码':'Please enter 11 mobile phone Numbers',
                    },
                    regexp: {
                        regexp: /^1\d{10}$/,
                        message: isZH()?'请输入正确的手机号码':'Please enter the correct mobile number',
                    }
                }
            }
        }
    });