// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// upload a file
function fileUpload() {
    let files = [];
    let fileName = document.getElementById('file').value.split("\\").pop();
    files.push(fileName);
    console.log(files);
    // when the file is uploaded
    if (document.getElementById('file').value != "") {
        setTimeout(() => {
            //           create list of files
            files.forEach(function (item) {
                item = item.toLowerCase();
                let list = document.createElement("li");
                
                list.innerText = item;
                
                document.getElementById('list').appendChild(list);
                $(list).addClass("li-drag-drop");
                
            })
        }, 400);
    }
}
