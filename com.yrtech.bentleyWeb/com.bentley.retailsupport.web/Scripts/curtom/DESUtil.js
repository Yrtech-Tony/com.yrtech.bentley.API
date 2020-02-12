
var aseKey = "76159843"     //秘钥必须为：8/16/32位
var message = "宾利青岛";
var iv = "12344321";

//加密
var encrypt = CryptoJS.DES.encrypt(message, CryptoJS.enc.Utf8.parse(aseKey), {
    iv: CryptoJS.enc.Utf8.parse(iv),
    mode: CryptoJS.mode.CBC,
    padding: CryptoJS.pad.Pkcs7
}).toString();
console.log(encrypt);

//解密
var decrypt = CryptoJS.DES.decrypt(encrypt, CryptoJS.enc.Utf8.parse(aseKey), {
    iv: CryptoJS.enc.Utf8.parse(iv),
    mode: CryptoJS.mode.CBC,
    padding: CryptoJS.pad.Pkcs7
}).toString(CryptoJS.enc.Utf8);
console.log(decrypt);

function toUtf8(str) {   
    var out, i, len, c;   
    out = "";   
    len = str.length;   
    for(i = 0; i < len; i++) {   
        c = str.charCodeAt(i);   
        if ((c >= 0x0001) && (c <= 0x007F)) {   
            out += str.charAt(i);   
        } else if (c > 0x07FF) {   
            out += String.fromCharCode(0xE0 | ((c >> 12) & 0x0F));   
            out += String.fromCharCode(0x80 | ((c >>  6) & 0x3F));   
            out += String.fromCharCode(0x80 | ((c >>  0) & 0x3F));   
        } else {   
            out += String.fromCharCode(0xC0 | ((c >>  6) & 0x1F));   
            out += String.fromCharCode(0x80 | ((c >>  0) & 0x3F));   
        }   
    }   
    return out;   
}
