document.getElementById('Name').value = readCookie('nameCookie');
document.getElementById('Surname').value = readCookie('surnameCookie');
document.getElementById('PhoneNumber').value = readCookie('phoneNumberCookie');
document.getElementById('Country').value = readCookie('countryCookie');
document.getElementById('Region').value = readCookie('regionCookie');
document.getElementById('City').value = readCookie('cityCookie');
document.getElementById('Address').value = readCookie('addressCookie');
document.getElementById('Postcode').value = readCookie('postcodeCookie');

function readCookie(name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}