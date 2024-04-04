//Upload a file
function fileUpload1() {
    let files = [];
    let fileName = document.getElementById('fileEmployerOrder').value.split("\\").pop();
    files.push(fileName);
    console.log(files);
    // when the file is uploaded
    if (document.getElementById('fileEmployerOrder').value != "") {
        setTimeout(() => {
            //           create list of files
            files.forEach(function (item) {
                item = item.toLowerCase();
                let list = document.createElement("li");

                list.innerText = item;

                document.getElementById('orderList').appendChild(list);
                $(list).addClass("li-drag-drop");

            })
        }, 400);
    }
    if (files.length > 0) {

        let reader = new FileReader();

        reader.onload = function (event) {
            let arrayBuffer = event.target.result;

            console.log(arrayBuffer);

            let byteArray = new Uint8Array(arrayBuffer);
            console.log(byteArray);
        };

        let blob = new Blob([files]);

        reader.readAsArrayBuffer(blob);
    } else {
        console.error("No se ha seleccionado ningún archivo.");
    }
}

function fileUpload2() {
    let files = [];
    let fileName = document.getElementById('fileSalaryStatement').value.split("\\").pop();
    files.push(fileName);
    console.log(files);
    // when the file is uploaded
    if (document.getElementById('fileSalaryStatement').value != "") {
        setTimeout(() => {
            //           create list of files
            files.forEach(function (item) {
                item = item.toLowerCase();
                let list = document.createElement("li");

                list.innerText = item;

                document.getElementById('salaryList').appendChild(list);
                $(list).addClass("li-drag-drop");

            })
        }, 400);
    }
    if (files.length > 0) {

        let reader = new FileReader();

        reader.onload = function (event) {
            let arrayBuffer = event.target.result;

            console.log(arrayBuffer);

            let byteArray = new Uint8Array(arrayBuffer);
            console.log(byteArray);
        };

        let blob = new Blob([files]);

        reader.readAsArrayBuffer(blob);
    } else {
        console.error("No se ha seleccionado ningún archivo.");
    }
}


$(document).ready(function () {
    $('#btnCreateCustomer').click(function () {
        $('#contentCreateCustomer').load('/Customer/showCustomer');
        $('#modalCreateCustomer').modal('show');
    });
});
