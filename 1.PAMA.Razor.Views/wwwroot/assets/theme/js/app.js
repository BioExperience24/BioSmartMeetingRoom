function get_modules(...module_text)
{
    return $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: app.url.get_modules,
        data: { module_text : module_text.toString() }
    });
}

function objectify_form(formArray) {
    //serialize data function
    var returnArray = {};
    for (var i = 0; i < formArray.length; i++){
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
}

$(document).on("ajaxSend", function(event, jqXHR, settings) {
    if (settings.type === "POST") {
        // console.log("URL:", settings.url);
        Swal.fire({
            title: 'Please Wait !',
            allowOutsideClick: false,
            onBeforeOpen: () => {
                Swal.showLoading()
            },
        });
    }
});

$(document).on("ajaxComplete", function(event, jqXHR, settings) {
    if (settings.type === "POST") {
        Swal.close();
    }
});