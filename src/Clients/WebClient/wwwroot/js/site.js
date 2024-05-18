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

async function loadContentAndShowModal(contentElement, modalElement, path) {
    try {
        //Get Content
        const response = await fetch(path);

        if (!response.ok) throw new Error('Network response was not ok');

        //Show Content and Modal
        document.querySelector(contentElement).innerHTML = await response.text();
        $(modalElement).modal('show');
    } catch (e) {
        throw new Error('Something went wrong...');
    }
}

function modalCreate($btnElement, contentElement, modalElement, path) {

    async function load() {
        await loadContentAndShowModal(contentElement, modalElement, path);
    }

    function handleClick(e) {
        if (e.target === $btnElement) load();
    }

    document.addEventListener('click', handleClick);
}

async function modalUpdate(btnElement, contentElement, modalElement, path) {

    async function load(id) {
        await loadContentAndShowModal(contentElement, modalElement, (path + id));
    }

    function handleClick(e) {
        if (e.target.classList.contains(btnElement)) load(e.target.dataset.id);
        else if (e.target.parentNode.classList.contains(btnElement)) load(e.target.parentNode.dataset.id);      
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
    modalCreate($btnCreateNotificationJudicial, '#contentCreateNotificationJudicialCollection', '#modalCreateNotificationJudicialCollection', '/JudicialCollectionClients/CreateNotificationJudicialCollection');
    modalCreate($btnCreateUsers, '#contentCreateUsers', '#modalCreateUsers', '/Users/CreateUsers');
    modalCreate($btnCreateDeadline, '#contentCreateDeadline', '#modalCreateDeadline', '/Deadlines/CreateDeadline');
    modalCreate($btnCreateInterest, '#contentCreateInterest', '#modalCreateInterest', '/InterestRates/CreateInterest');
    modalCreate($btnCreateTypeCurrency, '#contentCreateTypeCurrency', '#modalCreateTypeCurrency', '/TypeCurrency/CreateTypeCurrency');
    modalCreate($btnCreateTypeAccount, '#contentCreateTypeAccount', '#modalCreateTypeAccount', '/TypeAccount/CreateTypeAccount');
    modalCreate($btnCreateTypeLoan, '#contentCreateTypeLoan', '#modalCreateTypeLoan', '/TypeLoan/CreateTypeLoan');
    modalCreate($btnCreateRole, '#contentCreateRole', '#modalCreateRole', '/Roles/CreateRole');
    modalCreate($btnCreateEmployee, '#contentCreateEmployee', '#modalCreateEmployee', '/Employee/CreateEmployee');
    modalCreate($btnCreateCustomer, '#contentCreateCustomer', '#modalCreateCustomer', '/Customer/ShowCustomer');

    //Modals for Update
    modalUpdate('btnUpdateCustomer', '#contentUpdateCustomer', '#modalUpdateCustomer', '/Customer/ShowCustomerUpdate?id=');
    modalUpdate('btnCreateRenegotiation', '#contentCreateRenegotiation', '#modalCreateRenegotiation', '/RecoveryLoan/createRenegotiation?id=');
    modalUpdate('btnViewApplication', '#contentViewApplication', '#modalViewApplication', '/GrantLoan/ViewApplication?id=');
    modalUpdate('btnUpdateEmployee', '#contentUpdateEmployee', '#modalUpdateEmployee', '/Employee/UpdateEmployee?id=');
    modalUpdate('btnUpdateUser', '#contentUpdateUser', '#modalUpdateUser', '/Users/UpdateUser?id=');
});
