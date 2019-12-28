
function isZH() {
    //return true;

    if (localStorage.language && localStorage.language == 'en')
        return false;
    else
        return true;
}
function setLanguage(language) {
    try {
        $.post("/Master/SetLanguage", { userId: $('#G_UserId').val(), language: language }, function (result) {
            console.log(result);
        });
    }
    catch (e) {
        console.log(e);
    }

    if (localStorage.language && language == 'en') {
        localStorage.language = 'en';
    }
    else {
        localStorage.language = 'zh';
    }

}
function setLanguage2(language) {
    if (localStorage.language && language == 'en') {
        localStorage.language = 'en';
    }
    else {
        localStorage.language = 'zh';
    }

}