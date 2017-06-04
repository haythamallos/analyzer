function CheckCompatibility()
{
    var iscompatible = true;
    window.indexedDB = window.indexedDB || window.mozIndexedDB || window.webkitIndexedDB || window.msIndexedDB;
    window.IDBTransaction = window.IDBTransaction || window.webkitIDBTransaction || window.msIDBTransaction;
    window.IDBKeyRange = window.IDBKeyRange || window.webkitIDBKeyRange || window.msIDBKeyRange

    if (!window.indexedDB) {
        //window.alert("Your browser doesn't support a stable version of IndexedDB.");
        iscompatible = false;
    }

    return false;
}
function log(txt) {
    $('#logger').append(txt);
}
function InitOffline(dbname) {
    log("Starting offline init ...\n");
    var isCompatible = CheckCompatibility();
    if (!iscompatible) {
        log("Your browser doesn't support a stable version of IndexedDB.  Exiting\n");
        return;
    }
}

//function Calculate() {
//    var x = parseInt(MyCalculator.firstNumber.value);
//    var y = parseInt(MyCalculator.secondNumber.value);
//    if (x === undefined || isNaN(x)) x = 0;
//    if (y === undefined || isNaN(y)) y = 0;
//    var result = x + y;
//    MyCalculator.txtResult.value = result;
//};