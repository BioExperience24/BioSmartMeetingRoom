
function swalShowNotification(icon, title, loc = "", loc2 = "") {
    var ic = "";
    if (icon == "alert-success") {
        ic = "success";
    } else if (icon == "alert-danger") {
        ic = "danger";
    } else if (icon == "alert-warning") {
        ic = "warning";
    } else if (icon == "alert-info") {
        ic = "info";
    }
    Swal.fire(
        title,
        '',
        ic
    )
}

function loadingg(title = "", body = "") {
    Swal.fire({
        title: title,
        html: body,
        allowOutsideClick: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        },
    });
}

function errorAjax(xhr, ajaxOptions, thrownError) {
    swal.close();

    $('#id_loader').html('');
    if (ajaxOptions == "parsererror") {
        var msg = "Status Code 500, Error Server bad parsing";
        showNotification('alert-danger', msg, 'bottom', 'left')
    } else {
        var msg = "Status Code " + xhr.status + " Please check your connection !!!";
        showNotification('alert-danger', msg, 'bottom', 'left')
    }
}