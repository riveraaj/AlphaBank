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

//Create Customer Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateCustomer = document.getElementById('btnCreateCustomer');
    btnCreateCustomer.addEventListener('click', () => {
        $('#contentCreateCustomer').load('/Customer/ShowCustomer');
        $('#modalCreateCustomer').modal('show');
    });
});

//Update Customer Modal
//document.addEventListener('DOMContentLoaded', () => {
//    var btnUpdateCustomer = document.querySelector('.btnUpdateCustomer');
//    btnUpdateCustomer.addEventListener('click', function () {
//        var id = $(this).data('id');
//        $('#contentUpdateCustomer').load('/Customer/ShowCustomerUpdate?id=' + id);
//        $('#modalUpdateCustomer').modal('show');
//    });
//});

$(document).ready(function () {
    // Usando delegación de eventos para manejar clicks en los botones
    $(document).on('click', '.btnUpdateCustomer', function () {
        var id = $(this).data('id'); // Obtiene el ID desde el atributo data-id del botón
        $('#contentUpdateCustomer').load('/Customer/ShowCustomerUpdate?id=' + id, function (response, status, xhr) {
            if (status == "error") {
                alert("Error al cargar los datos: " + xhr.status + " " + xhr.statusText);
            } else {
                $('#modalUpdateCustomer').modal('show');
            }
        });
    });
});
//Create Renegotiation Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateRoles = document.getElementById('btnCreateRenegotiation');
    btnCreateRoles.addEventListener('click', function () {
        var loanId = $(this).data('id');
        $('#contentCreateRenegotiation').load('/RecoveryLoan/createRenegotiation?id=' + loanId);
        $('#modalCreateRenegotiation').modal('show');
    });
});

document.addEventListener('DOMContentLoaded', () => {
    var btnCreateRoles = document.getElementById('btnCreateNotificationJudicialCollection');
    btnCreateRoles.addEventListener('click', () => {
        $('#contentCreateNotificationJudicialCollection').load('/JudicialCollectionClients/CreateNotificationJudicialCollection');
        $('#modalCreateNotificationJudicialCollection').modal('show');
    });
});

//View Loan Application Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateRoles = document.getElementById('btnViewApplication');
    btnCreateRoles.addEventListener('click', function () {
        var loanId = $(this).data('id');
        $('#contentViewApplication').load('/GrantLoan/ViewApplication?id=' + loanId);
        $('#modalViewApplication').modal('show');
    });
});

//Create User Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateRoles = document.getElementById('btnCreateUsers');
    btnCreateRoles.addEventListener('click', () => {
        $('#contentCreateUsers').load('/Users/CreateUsers');
        $('#modalCreateUsers').modal('show');
    });
});

//Update User Modal
$(document).ready(function () {
    // Usando delegación de eventos para manejar clicks en los botones
    $(document).on('click', '.btnUpdateUser', function () {
        var id = $(this).data('id'); // Obtiene el ID desde el atributo data-id del botón
        $('#contentUpdateUser').load('/Users/UpdateUser?id=' + id, function (response, status, xhr) {
            if (status == "error") {
                alert("Error al cargar los datos: " + xhr.status + " " + xhr.statusText);
            } else {
                $('#modalUpdateUser').modal('show');
            }
        });
    });
});

//Create Deadline Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateRoles = document.getElementById('btnCreateDeadline');
    btnCreateRoles.addEventListener('click', () => {
        $('#contentCreateDeadline').load('/Deadlines/CreateDeadline');
        $('#modalCreateDeadline').modal('show');
    });
});

//Update Deadline Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateCustomer = document.getElementById('btnUpdateDeadline');
    btnCreateCustomer.addEventListener('click', function () {
        var id = $(this).data('id');
        $('#contentUpdateDeadline').load('/Deadlines/UpdateDeadline?id=' + id);
        $('#modalUpdateDeadline').modal('show');
    });
});

//Create Deadline Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateRoles = document.getElementById('btnCreateInterest');
    btnCreateRoles.addEventListener('click', () => {
        $('#contentCreateInterest').load('/InterestRates/CreateInterest');
        $('#modalCreateInterest').modal('show');
    });
});

//Update Deadline Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateCustomer = document.getElementById('btnUpdateInterest');
    btnCreateCustomer.addEventListener('click', function () {
        var id = $(this).data('id');
        $('#contentUpdateInterest').load('/InterestRates/UpdateInterest?id=' + id);
        $('#modalUpdateInterest').modal('show');
    });
});

//Create TypeCurrency Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateRoles = document.getElementById('btnCreateTypeCurrency');
    btnCreateRoles.addEventListener('click', () => {
        $('#contentCreateTypeCurrency').load('/TypeCurrency/CreateTypeCurrency');
        $('#modalCreateTypeCurrency').modal('show');
    });
});

//Update TypeCurrency Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateCustomer = document.getElementById('btnUpdateTypeCurrency');
    btnCreateCustomer.addEventListener('click', function () {
        var id = $(this).data('id');
        $('#contentUpdateTypeCurrency').load('/TypeCurrency/UpdateTypeCurrency?id=' + id);
        $('#modalUpdateTypeCurrency').modal('show');
    });
});

//Create TypeAccount Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateRoles = document.getElementById('btnCreateTypeAccount');
    btnCreateRoles.addEventListener('click', () => {
        $('#contentCreateTypeAccount').load('/TypeAccount/CreateTypeAccount');
        $('#modalCreateTypeAccount').modal('show');
    });
});

//Update TypeAccount Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateCustomer = document.getElementById('btnUpdateTypeAccount');
    btnCreateCustomer.addEventListener('click', function () {
        var id = $(this).data('id');
        $('#contentUpdateTypeAccount').load('/TypeAccount/UpdateTypeAccount?id=' + id);
        $('#modalUpdateTypeAccount').modal('show');
    });
});

//Create TypeLoan Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateRoles = document.getElementById('btnCreateTypeLoan');
    btnCreateRoles.addEventListener('click', () => {
        $('#contentCreateTypeLoan').load('/TypeLoan/CreateTypeLoan');
        $('#modalCreateTypeLoan').modal('show');
    });
});

//Update TypeLoan Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateCustomer = document.getElementById('btnUpdateTypeLoan');
    btnCreateCustomer.addEventListener('click', function () {
        var id = $(this).data('id');
        $('#contentUpdateTypeLoan').load('/TypeLoan/UpdateTypeLoan?id=' + id);
        $('#modalUpdateTypeLoan').modal('show');
    });
});

//Create Role Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateRoles = document.getElementById('btnCreateRole');
    btnCreateRoles.addEventListener('click', () => {
        $('#contentCreateRole').load('/Roles/CreateRole');
        $('#modalCreateRole').modal('show');
    });
});

//Update Role Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateCustomer = document.getElementById('btnUpdateRole');
    btnCreateCustomer.addEventListener('click', function () {
        var id = $(this).data('id');
        $('#contentUpdateRole').load('/Roles/UpdateRole?id=' + id);
        $('#modalUpdateRole').modal('show');
    });
});

//Create Employee Modal
document.addEventListener('DOMContentLoaded', () => {
    var btnCreateRoles = document.getElementById('btnCreateEmployee');
    btnCreateRoles.addEventListener('click', () => {
        $('#contentCreateEmployee').load('/Employee/CreateEmployee');
        $('#modalCreateEmployee').modal('show');
    });
});

//Update Employee Modal
$(document).ready(function () {
    // Usando delegación de eventos para manejar clicks en los botones
    $(document).on('click', '.btnUpdateEmployee', function () {
        var id = $(this).data('id'); // Obtiene el ID desde el atributo data-id del botón
        $('#contentUpdateEmployee').load('/Employee/UpdateEmployee?id=' + id, function (response, status, xhr) {
            if (status == "error") {
                alert("Error al cargar los datos: " + xhr.status + " " + xhr.statusText);
            } else {
                $('#modalUpdateEmployee').modal('show');
            }
        });
    });
});