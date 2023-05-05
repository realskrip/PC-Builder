var sortOrder = document.getElementById('sortOrder');
sortOrder.onchange = function () {

    // задаём время, через которое наш cookie истечёт
    //var date = new Date();
    //date.setDate(date.getDate() + 7);

    // записываем cookie
    document.cookie = 'sortOrder=' + sortOrder.value;

    // вариант с временем, через которое наш cookie истечёт
    //document.cookie = 'sortOrder=' + sortOrder.value + '; path=/; expires=' + date.toUTCString();

};