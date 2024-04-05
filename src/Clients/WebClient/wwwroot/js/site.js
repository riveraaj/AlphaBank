//Upload a file
function fileUpload(inputId, ulId) {

    const files = [],
        fileInput = document.getElementById(inputId),
        fileList = document.getElementById(ulId),
        fileName = fileInput.value.split("\\").pop();

    fileList.innerHTML = '';

    files.push(fileName)
    console.log(files);

    //When the file is uploaded
    if (fileInput.value != "") {
        setTimeout(() => {
            //create list of files
            files.forEach(file => {
                file = file.toLowerCase();
                let list = document.createElement("li");

                list.innerText = file;

                fileList.appendChild(list);
                $(list).addClass("li-drag-drop");
            });
        }, 400);
    }
}

document.addEventListener('DOMContentLoaded', () => {
    var btnCreateCustomer = document.getElementById('btnCreateCustomer');
    btnCreateCustomer.addEventListener('click', () => {
        $('#contentCreateCustomer').load('/Customer/showCustomer');
        $('#modalCreateCustomer').modal('show');
    });
});

//$(document).ready(() => {
//    var btnCreateCustomer = document.getElementById('btnCreateCustomer');
//    btnCreateCustomer.addEventListener('click', () => {

//    });
//    $('#btnCreateCustomer').click(() => {
//        $('#contentCreateCustomer').load('/Customer/showCustomer');
//        $('#modalCreateCustomer').modal('show');
//    });
//});

$(document).ready(function () {
    $('#btnCreateDeadlines').click(function () {
        $('#contentCreateCustomer').load('/Deadlines/showDeadlines');
        $('#modalCreateDeadlines').modal('show');
    });
});

$(document).ready(function () {
    $('#btnCreateInterestRates').click(function () {
        $('#contentCreateCustomer').load('/InterestRates/showIntesrestRates');
        $('#modalCreateIntesrestRates').modal('show');
    });
});
$(document).ready(function () {
    $('#btnCreateTypeCurrency').click(function () {
        $('#contentCreateTypeCurrency').load('/TypeCurrency/showTypeCurrency');
        $('#modalCreateTypeCurrency').modal('show');
    });
});
$(document).ready(function () {
    $('#btnCreateTypeAccount').click(function () {
        $('#contentCreateTypeAccount').load('/TypeAccount/showTypeAccount');
        $('#modalCreateTypeAccount').modal('show');
    });
});
$(document).ready(function () {
    $('#btnCreateTypeLoan').click(function () {
        $('#contentCreateTypeLoan').load('/TypeLoan/showTypeLoan');
        $('#modalCreateTypeLoan').modal('show');
    });
});
$(document).ready(function () {
    $('#btnCreateRoles').click(function () {
        $('#contentCreateRoles').load('/Roles/showRoles');
        $('#modalCreateRoles').modal('show');
    });
});
