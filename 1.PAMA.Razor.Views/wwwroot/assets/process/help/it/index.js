const bs = $("#id_baseurl").val();
let tblHelp;

$(function () {
    // load table
    initTable();
});

function initTable() {
    let columns = [
        {data:"no", name:"no", searchable:false, orderable:false},
        // {data:"id", name:"id", searchable:false, orderable:false},
        {data:"subject", name:"subject", searchable:false, orderable:false, render: function(_, _, item) {
            return `
            <b>${item.subject}</b> 
            <br>
            <small>${item.description}</small>
            `;
        }},
        {data:"datetime", name:"datetime", searchable:false, orderable:false, render: function(_, _, item) {
            return item.datetime ?? "";
        }},
        {data:"created_by", name:"created_by", searchable:false, orderable:false},
        {data:"meeting", name:"meeting", searchable:false, orderable:false, render: function(_, _, item) {
            return `${item.room_name} - ${item.booking_name}`;
        }},
        {data:"user_approved", name:"user_approved", searchable:false, orderable:false},
        {data:"status", name:"status", searchable:false, orderable:false},
        {data:"action", name:"action", searchable:false, orderable:false, render: function(_, _, item) {
            if (item.status == "pending") {
                return `
                    <button type="button" class="btn btn-info waves-effect" onclick="processToAccept($(this))">Accept</button>
                    <button type="button" class="btn btn-danger waves-effect" onclick="processToReject($(this))">Reject</button>
                `;
            } else if (item.status == "process") {
                return `
                    <button type="button" class="btn btn-info waves-effect" onclick="processToDone($(this))">Done</button>
                `;
            } else {
                return "";
            }
        }},
    ];

    tblHelp = $("#id_tbl").DataTable({
        searching: true,
        bLengthChange: true,
        bInfo: true,
        ordering: false,
        columns: columns,
        // "order": [[ 0, 'asc' ]],
        ajax: {
            url: bs + ajax.url.get_datatables,
            contentType: 'application/json',
            beforeSend: function() {
                if (typeof tblHelp != "undefined" && tblHelp.hasOwnProperty('settings') && tblHelp.settings()[0].jqXHR != null) {
                    tblHelp.settings()[0].jqXHR.abort();
                }
            },
            data: function (param) {
                delete param.columns;
                param.type = "it"
            },
            dataSrc: function (json) {
                // Map properties to the expected structure
                json.draw = json.collection.draw;
                json.recordsFiltered = json.collection.recordsFiltered;
                json.recordsTotal = json.collection.recordsTotal;

                return json.collection.data;
            } 
        },
        processing: true,
        serverSide: true,
        createdRow: function( row, item, index ) {
            $(row).attr('id', `help-${index}`);
            $(row).data("helpData", item);
            // $(row).find("td:eq(0)").addClass("text-center");
            // $(row).find("td:eq(1)").addClass("text-center");
        },
        drawCallback: function (settings) {
            // console.log("--------drawCallback--------");
            // console.log(settings);
        },
    });
}

function reloadTable() {
    if (typeof tblHelp != "undefined") {
        tblHelp.ajax.reload();
    }
};


function errorAjax(xhr, ajaxOptions, thrownError){
    $('#id_loader').html('');
    if(ajaxOptions == "parsererror"){
        var msg = "Status Code 500, Error Server bad parsing";
        showNotification('alert-danger', msg,'bottom','left')
    }else{
        var msg ="Status Code "+ xhr.status + " Please check your connection !!!";
        showNotification('alert-danger', msg,'bottom','left')
    }
}

function processToAccept(t) {
    let row = t.parents("tr");
    let data = row.data("helpData");
    
    Swal.fire({
        title: 'Are you sure you want to accept this request?',
        text: "Once accepted, you will be responsible for handling it.",
        type: "question",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Accept !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            var form = new FormData();
            form.append('id', data.id);
            form.append('st', 'process');
            processChangeStatus(form);
        }
    });
}

function processToReject(t) {
    let row = t.parents("tr");
    let data = row.data("helpData");
    
    Swal.fire({
        title: "Are you sure you want to reject this request?",
        // text: "Once accepted, you will be responsible for handling it.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Reject !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                title: 'Please provide a reason for rejection.',
                input: "text",
                inputAttributes: {
                    autocapitalize: "off",
                },
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Submit',
                cancelButtonText: 'Close !',
                reverseButtons: true,
                preConfirm: (result) => {
                    if (result == "" || result == null) {
                        return Swal.showValidationMessage(`Reason for Rejection is required`);
                    }
                }
            }).then((result) => {
                if (result.value !== undefined) {
                    var form = new FormData();
                    form.append('id', data.id);
                    form.append('st', 'reject');
                    form.append('note', result.value);
                    processChangeStatus(form);
                }
            });
        }
    });
}

function processToDone(t) {
    let row = t.parents("tr");
    let data = row.data("helpData");
    
    Swal.fire({
        title: "Are you sure you want to mark this request as done?",
        text: "Make sure all required actions have been completed.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Submit !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                title: 'Note (Optional)',
                input: "text",
                inputAttributes: {
                    autocapitalize: "off",
                },
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Submit',
                cancelButtonText: 'Close !',
                reverseButtons: true,
            }).then((result) => {
                if (result.value !== undefined) {
                    var form = new FormData();
                    form.append('id', data.id);
                    form.append('st', 'done');
                    form.append('note', result.value);
                    processChangeStatus(form);
                }
            });
        }
    });
}

function processChangeStatus(form) {
    var bs = $('#id_baseurl').val();

    $.ajax({
        url: bs + ajax.url.post_change_status,
        type: "POST",
        data: form,
        processData: false,
        contentType: false,
        dataType: "json",
        beforeSend: function() {
            $('#id_loader').html('<div class="linePreloader"></div>');
            Swal.fire({
                title: 'Please Wait !',
                html: `Process to change the status`,
                allowOutsideClick: false,
                onBeforeOpen: () => {
                    Swal.showLoading()
                },
            });
        },
        success: function(data) {
            Swal.close();
            $('#id_loader').html('');
            if (data.status == "success") {
                showNotification('alert-success', "Process Succes", 'top', 'center')
                // init();
                reloadTable();
            } else {
                showNotification('alert-danger', "Process Failed!!!", 'bottom', 'left')
            }
        },
        complete: function() {
            $('#id_loader').html('');
        },
        error: errorAjax,
    });
}