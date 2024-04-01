
function AlertSuccess(title, text) {
    // Muestra el SweetAlert
    Swal.fire({
        title: title,
        text: text,
        icon: 'success',
        confirmButtonText: 'Aceptar',
        confirmButtonColor: "#789461"
    });
}

function AlertError(title, text) {
    // Muestra el SweetAlert
    Swal.fire({
        icon: "error",
        title: title,
        text: text,
        confirmButtonColor: "#789461"
    });
}

function AlertConfirmDialog(title, text){
    Swal.fire({
        title: title,
        text: text,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#789461",
        cancelButtonColor: "#d33",
        cancelButtonText: "Cancelar",
        confirmButtonText: "Si"
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: "¡Hecho!",
                text: "El proceso se ha completado correctamente.",
                icon: "success",
                confirmButtonColor: "#789461"
            });
        }
    });
}