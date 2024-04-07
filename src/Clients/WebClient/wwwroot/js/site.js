﻿//Upload a file
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

document.addEventListener('DOMContentLoaded', () => {
    var btnCreateDeadlines = document.getElementById('btnCreateDeadlines');
    btnCreateDeadlines.addEventListener('click', () => {
        $('#contentCreateCustomer').load('/Deadlines/showDeadlines');
        $('#modalCreateDeadlines').modal('show');
    });
});

document.addEventListener('DOMContentLoaded', () => {
    var btnCreateInterestRates = document.getElementById('btnCreateInterestRates');
    btnCreateInterestRates.addEventListener('click', () => {
        $('#contentCreateCustomer').load('/InterestRates/showIntesrestRates');
        $('#modalCreateIntesrestRates').modal('show');
    });
});

document.addEventListener('DOMContentLoaded', () => {
    var btnCreateTypeCurrency = document.getElementById('btnCreateTypeCurrency');
    btnCreateTypeCurrency.addEventListener('click', () => {
        $('#contentCreateTypeCurrency').load('/TypeCurrency/showTypeCurrency');
        $('#modalCreateTypeCurrency').modal('show');
    });
});

document.addEventListener('DOMContentLoaded', () => {
    var btnCreateTypeAccount = document.getElementById('btnCreateTypeAccount');
    btnCreateTypeAccount.addEventListener('click', () => {
        $('#contentCreateTypeAccount').load('/TypeAccount/showTypeAccount');
        $('#modalCreateTypeAccount').modal('show');
    });
});

document.addEventListener('DOMContentLoaded', () => {
    var btnCreateTypeLoan = document.getElementById('btnCreateTypeLoan');
    btnCreateTypeLoan.addEventListener('click', () => {
        $('#contentCreateTypeLoan').load('/TypeLoan/showTypeLoan');
        $('#modalCreateTypeLoan').modal('show');
    });
});

document.addEventListener('DOMContentLoaded', () => {
    var btnCreateRoles = document.getElementById('btnCreateRoles');
    btnCreateRoles.addEventListener('click', () => {
        $('#contentCreateRoles').load('/Roles/showRoles');
        $('#modalCreateRoles').modal('show');
    });
});