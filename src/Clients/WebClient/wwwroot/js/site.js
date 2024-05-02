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

function ModalCreate($btnElement, contentElement, modalElement, path) {

    function loadContentAndShowModal() {
        $(contentElement).load(path);
        $(modalElement).modal('show');
    }

    function handleClick(e) {
        if (e.target === $btnElement) loadContentAndShowModal();
    }

    document.addEventListener('click', handleClick);
}

function ModalUpdate(btnElement, contentElement, modalElement, path) {

    function loadContentAndShowModal(id) {
        $(contentElement).load(path + id);
        $(modalElement).modal('show');
    }

    function handleClick(e) {
        if (e.target.classList.contains(btnElement)) loadContentAndShowModal(e.target.dataset.id);
        else if (e.target.parentNode.classList.contains(btnElement)) loadContentAndShowModal(e.target.parentNode.dataset.id);      
    }

    document.addEventListener('click', handleClick);
}

document.addEventListener('DOMContentLoaded', () => {
    const $btnCreateNotificationJudicial = document.getElementById('btnCreateNotificationJudicialCollection'),
        $btnCreateUsers = document.getElementById('btnCreateUsers'),
        $btnCreateDeadline = document.getElementById('btnCreateDeadline'),
        $btnCreateInterest = document.getElementById('btnCreateInterest'),
        $btnCreateTypeCurrency = document.getElementById('btnCreateTypeCurrency'),
        $btnCreateTypeAccount = document.getElementById('btnCreateTypeAccount'),
        $btnCreateTypeLoan = document.getElementById('btnCreateTypeLoan'),
        $btnCreateRole = document.getElementById('btnCreateRole'),
        $btnCreateEmployee = document.getElementById('btnCreateEmployee'),
        $btnCreateCustomer = document.getElementById('btnCreateCustomer');

    //Modals for Create
    ModalCreate($btnCreateNotificationJudicial, '#contentCreateNotificationJudicialCollection', '#modalCreateNotificationJudicialCollection', '/JudicialCollectionClients/CreateNotificationJudicialCollection');
    ModalCreate($btnCreateUsers, '#contentCreateUsers', '#modalCreateUsers', '/Users/CreateUsers');
    ModalCreate($btnCreateDeadline, '#contentCreateDeadline', '#modalCreateDeadline', '/Deadlines/CreateDeadline');
    ModalCreate($btnCreateInterest, '#contentCreateInterest', '#modalCreateInterest', '/InterestRates/CreateInterest');
    ModalCreate($btnCreateTypeCurrency, '#contentCreateTypeCurrency', '#modalCreateTypeCurrency', '/TypeCurrency/CreateTypeCurrency');
    ModalCreate($btnCreateTypeAccount, '#contentCreateTypeAccount', '#modalCreateTypeAccount', '/TypeAccount/CreateTypeAccount');
    ModalCreate($btnCreateTypeLoan, '#contentCreateTypeLoan', '#modalCreateTypeLoan', '/TypeLoan/CreateTypeLoan');
    ModalCreate($btnCreateRole, '#contentCreateRole', '#modalCreateRole', '/Roles/CreateRole');
    ModalCreate($btnCreateEmployee, '#contentCreateEmployee', '#modalCreateEmployee', '/Employee/CreateEmployee');
    ModalCreate($btnCreateCustomer, '#contentCreateCustomer', '#modalCreateCustomer', '/Customer/ShowCustomer');

    //Modals for Update
    ModalUpdate('btnUpdateCustomer', '#contentUpdateCustomer', '#modalUpdateCustomer', '/Customer/ShowCustomerUpdate?id=');
    ModalUpdate('btnCreateRenegotiation', '#contentCreateRenegotiation', '#modalCreateRenegotiation', '/RecoveryLoan/createRenegotiation?id=');
    ModalUpdate('btnViewApplication', '#contentViewApplication', '#modalViewApplication', '/GrantLoan/ViewApplication?id=');
    ModalUpdate('btnUpdateEmployee', '#contentUpdateEmployee', '#modalUpdateEmployee', '/Employee/UpdateEmployee?id=');
    ModalUpdate('btnUpdateUser', '#contentUpdateUser', '#modalUpdateUser', '/Users/UpdateUser?id=');
});
