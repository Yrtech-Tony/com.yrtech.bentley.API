
function isZH() {
    //return true;
    if (localStorage.language && localStorage.language == 'en')
        return false;
    else
        return true;
}
function setLanguage(language) {
    if (localStorage.language && language == 'en') {
        localStorage.language = 'en';
    }
    else {
        localStorage.language = 'zh';
    }

}