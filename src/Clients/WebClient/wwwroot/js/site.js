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