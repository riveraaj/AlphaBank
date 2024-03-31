
function AlertSucces(title, text) {
    // Muestra el SweetAlert
    Swal.fire({
        title: title,
        text: text,
        icon: 'success',
        confirmButtonText: 'Aceptar'
    });
}

function AlertError(title, text) {
    // Muestra el SweetAlert
    Swal.fire({
        icon: "error",
        title: title,
        text: text

    });
}

function AlertConfirmDialog(title, text){
    Swal.fire({
        title: title,
        text: text,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        cancelButtonText: "Cancelar",
        confirmButtonText: "Si"
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: "¡Hecho!",
                text: "El proceso se ha completado correctamente.",
                icon: "success"
            });
        }
    });
}