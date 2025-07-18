const bs = $("#id_baseurl").val();
const bsApp = $("#id_appurl").val();
let localtimezone = moment.tz.guess();
let gPantryPackage = [];
let globalStatusInvoice = [];
let gHeadEmployees = [];
let tblApproval;

$(function () {
    initRangeDate();
    initMasterData();
});

$("#id_search").on("click", function () {
    reloadTable();
});

async function initMasterData() {
    try {
        await fetchPantryPackages();
        await fetchHeadEmployees();
    } finally {
        // load table
        initTable();
    }
}

async function fetchPantryPackages() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_pantry_packages
        });

        if (data.status == "success")
        {
            gPantryPackage =  data.collection;
            
            initFilterPantryPackages();
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

async function fetchHeadEmployees() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_head_employees
        });

        if (data.status == "success")
        {
            $.each(data.collection, function (key, val) { 
                gHeadEmployees.push({
                    id: val.id,
                    name: val.name
                });
            });            
            initFilterHeadEmployees();
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

function initFilterPantryPackages() {
    $(`#id_package_search`).empty();
    
    $(`#id_package_search`).append(`<option value="">All Package</option>`);

    $.each(gPantryPackage, function (_, item) { 
        let opt = document.createElement("option");

        $(opt).text(`${item.name}`);
        $(opt).val(item.id);
        
        $(`#id_package_search`).append(opt);
        selectEnable();
    });
}

function initFilterHeadEmployees() {
    $(`#id_head_search`).empty();
    
    $(`#id_head_search`).append(`<option value="">All Head Employee</option>`);

    $.each(gHeadEmployees, function (_, item) { 
        let opt = document.createElement("option");

        $(opt).text(`${item.name}`);
        $(opt).val(item.id);
        
        $(`#id_head_search`).append(opt);
        selectEnable();
    });
}

function initTable() {
    let columns = [
        {data:"no", name:"no", searchable:false, orderable:false},
        {data:"order_id", name:"order_id", searchable:false, orderable:false, render: function(_, _, item) {
            return `
                <a data-toggle="tooltip" data-placement="top" title="Order Id" data-html="true" onclick="openDetail($(this))" style="cursor:pointer;font-size:16px;font-weight:bold;">
                    ${item.id}
                </a>
            `;
        }},
        {data:"order_datetime", name:"order_datetime", searchable:false, orderable:false, render: function(_, _, item) {
            return moment(item.order_datetime).format("DD MMM YYYY HH:mm");
        }},
        {data:"updated_by", name:"updated_by", searchable:false, orderable:false, render: function(_, _, item) {
            return item.updated_by ?? "";
        }},
        {data:"meeting", name:"meeting", searchable:false, orderable:false, render: function(_, _, item) {
            let date = `${moment(item.booking_date).format("DD MMM YYYY")} ${moment(item.booking_start).format("HH:mm")} - ${moment(item.booking_end).format("HH:mm")}`;
            return `
                ${item.booking_room_name} - ${item.booking_title}
                <br>
                <small><b>(${date})</b></small>
            `;
        }},
        {data:"approved_head_by", name:"approved_head_by", searchable:false, orderable:false, render: function(_, _, item) {
            if (
                item.booking_is_approve == 1 
                && item.order_st == 0 
                && item.approval_head == 0
                && item.head_employee_id == app.auth.nik
            ) {
                btn = `
                    <button type="button" class="btn btn-info waves-effect" onclick="processToAcceptHead($(this))">Accept</button>
                    <button type="button" class="btn btn-danger waves-effect" onclick="processToRejectHead($(this))">Reject</button>
                `;
                return btn;
            } else {
                return item.approved_head_by ?? "";
            }
            return "";
        }},
        {data:"approved_by", name:"approved_by", searchable:false, orderable:false, render: function(_, _, item) {
            let reviewer = "";
            if (item.booking_is_approve == 1 && item.order_st == 1) {
                reviewer = item.approved_by != "" ? item.approved_by : "Admin";
            } else if ((item.booking_is_approve == 2 || item.booking_is_approve == 1) && item.order_st == 5) {
                reviewer = item.rejected_by != "" ? item.rejected_by : "Admin";
            } else  if (item.order_st == 4 || item.booking_is_canceled == 1) {
                reviewer = item.canceled_by != "" ? item.canceled_by : "Admin";
            }
            return reviewer;
        }},
        {data:"status", name:"status", searchable:false, orderable:false, render: function(_, _, item) {
            let status = "";
            let momentEnd = moment(item.order_datetime);
            // let momentEnd = moment(item.order_datetime_before);
            if (item.order_st == 4 || item.booking_is_canceled == 1) {
                status = "Canceled";
            } else if (item.booking_is_approve == 0 || (item.booking_is_approve == 1 && item.order_st == 0)) {
                if (item.approval_head == 1) {
                    let waitingApprovalMeeting = (item.booking_is_approve == 0) ? " (Waiting Approval Meeting)" : "";
                    status = "Pending" + waitingApprovalMeeting;
                    
                } else {
                    status = "Pending (Waiting Approval Head)";
                }
            } else if (item.booking_is_approve == 0 || item.order_st == 0 && (moment().unix() > momentEnd.unix())) {
                status = "Order Expired";
            } else if (item.booking_is_approve == 1 && item.order_st == 1) {
                status = "Approved";
            } else if ((item.booking_is_approve == 2 || item.booking_is_approve == 1) && item.order_st == 5) {
                status = "Rejected";
            }
            
            return status;
        }},
        {data:"action", name:"action", searchable:false, orderable:false, render: function(_, _, item) {
            let momentEnd = moment(item.order_datetime);
            // let momentEnd = moment(item.order_datetime_before);
            
            let btn = ``;

            // if (item.booking_is_approve == 1 && (moment().unix() <= momentEnd.unix()) && item.order_st == 0) {
            if (item.approval_head == 1 && app.auth.level != "2") {
                if (item.booking_is_approve == 1 && item.order_st == 0) {
                    btn = `
                        <button type="button" class="btn btn-info waves-effect" onclick="processToAccept($(this))">Accept</button>
                        <button type="button" class="btn btn-danger waves-effect" onclick="processToReject($(this))">Reject</button>
                    `;
                } else if (item.booking_is_approve == 1 && item.order_st == 1) {
                    // btn = ` <a href="${bsApp}approval-order/print-memo?bid=${item.booking_id}" target="_blank" class="btn btn-success waves-effect">Print Memo</a>`;
                    btn = ` <a href="${bsApp}approval-order/print-memo?pid=${item.id}" target="_blank" class="btn btn-success waves-effect">Print Memo</a>`;
                }
            }

            return btn;
        }},
    ];

    tblApproval = $("#id_tbl").DataTable({
        searching: false,
        bLengthChange: false,
        bInfo: true,
        ordering: false,
        columns: columns,
        // "order": [[ 0, 'asc' ]],
        ajax: {
            url: bs + ajax.url.get_pantrytransaksi_approval_datatables,
            contentType: 'application/json',
            beforeSend: function() {
                if (typeof tblApproval != "undefined" && tblApproval.hasOwnProperty('settings') && tblApproval.settings()[0].jqXHR != null) {
                    tblApproval.settings()[0].jqXHR.abort();
                }
            },
            data: function (param) {
                delete param.columns;
                
                let daterange = $("#id_daterange_search").val();
                let start = daterange.split(" - ")[0];
                let end = daterange.split(" - ")[1];
                let startDate = moment(start.trim()).format('YYYY-MM-DD');
                let endDate = moment(end.trim()).format('YYYY-MM-DD');

                param.start_date = startDate;
                param.end_date = endDate;
                param.package_id = $("#id_package_search").val();
                param.head_employee_id = $("#id_head_search").val();
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
            $(row).attr('id', `approval-${index}`);
            $(row).data("approvalData", item);
            $(row).find("td:eq(0)").addClass("text-center");
            $(row).find("td:eq(1)").addClass("text-center");
        },
        drawCallback: function (settings) {
            // console.log("--------drawCallback--------");
            // console.log(settings);
        },
    });
}

function reloadTable() {
    if (typeof tblApproval != "undefined") {
        tblApproval.ajax.reload();
    }
};

function initRangeDate() {
    $(`.input-group #id_daterange_search`).daterangepicker({
        "showDropdowns": true,
        "showWeekNumbers": true,
        "showISOWeekNumbers": true,
        "opens": "center",
        // "drops": "up",
        "minDate": moment().subtract(29, 'days').format('MM/DD/YYYY'),
        // "minDate": moment(),
        // "startDate": moment().subtract(29, 'days').format('MM/DD/YYYY'),
        "startDate": moment().format('MM/DD/YYYY'),
        "endDate": moment().format('MM/DD/YYYY'),

        // "startDate": moment().format('YYYY-MM-DD'),
        // "endDate": moment().format('YYYY-MM-DD'), 
        // locale: {
        //     format: 'YYYY-MM-DD'
        // },
        isInvalidDate: function(date) {
            // Disable Saturdays and Sundays
            return date.day() === 0 || date.day() === 6;
        },
        // ranges: {
        //     'Today': [moment(), moment()],
        //     'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
        //     'Last 7 Days': [moment().subtract(6, 'days'), moment()],
        //     'Last 30 Days': [moment().subtract(29, 'days'), moment()],
        //     'This Month': [moment().startOf('month'), moment().endOf('month')],
        //     'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
        //     'Last Year': [moment().subtract(1, 'year').startOf('year'), moment().subtract(1, 'year').endOf('year')]
        // },
        "alwaysShowCalendars": true,
    }, function(start, end, label) {
        // initRoom(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'))
    });
}

function selectEnable() {
    $('select').selectpicker("refresh");
    $('select').selectpicker("initialize");
}

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

function changeTimeIntoTimezone(time, $timezone) {
    var dataMoment = moment.tz(time, $timezone);
    var localtimezone = moment.tz.guess(true);
    if ($timezone != localtimezone) {
        return dataMoment.clone().tz(localtimezone);
    }else{
        return dataMoment;
    }
}

function processToAccept(t) {
    let row = t.parents("tr");
    let data = row.data("approvalData");
    
    Swal.fire({
        title: 'Are you sure you want to accept order ' + data.id + '?',
        type: "question",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Accept Order !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                title: 'Add Note (Optional)',
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
                    var bs = $('#id_baseurl').val();
                    var form = new FormData();
                    form.append('id', data.id);
                    form.append('approval', 1);
                    form.append('note', result.value);
                    $.ajax({
                        // url: bs + "booking/post/cancelbook",
                        url: bs + ajax.url.post_process_order_approval,
                        type: "POST",
                        data: form,
                        processData: false,
                        contentType: false,
                        dataType: "json",
                        beforeSend: function() {
                            $('#id_loader').html('<div class="linePreloader"></div>');
                            Swal.fire({
                                title: 'Please Wait !',
                                html: 'Process to cancel meeting',
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
                                showNotification('alert-success', "Succes accept " + data.id, 'top', 'center')
                                reloadTable();
                            } else {
                                let msg = data.msg ?? "Accept order is failed!!!";
                                showNotification('alert-danger', msg, 'bottom', 'left')
                            }
                        },
                        complete: function() {
                            $('#id_loader').html('');
                        },
                        error: errorAjax,
                    });
                }
            });
            
        }
    });
}

function processToReject(t) {
    let row = t.parents("tr");
    let data = row.data("approvalData");
    
    Swal.fire({
        title: 'Are you sure want reject order ' + data.id + '?',
        text: "You will reject the data order " + data.id + " !",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Reject Order !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                title: 'Reason for Rejection',
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
                    form.append('approval', 2);
                    form.append('note', result.value);
                    var bs = $('#id_baseurl').val();
                    $.ajax({
                        // url: bs + "booking/post/cancelbook",
                        url: bs + ajax.url.post_process_order_approval,
                        type: "POST",
                        data: form,
                        processData: false,
                        contentType: false,
                        dataType: "json",
                        beforeSend: function() {
                            $('#id_loader').html('<div class="linePreloader"></div>');
                            Swal.fire({
                                title: 'Please Wait !',
                                html: 'Process to cancel meeting',
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
                                showNotification('alert-success', "Succes reject " + data.id, 'top', 'center')
                                reloadTable();
                            } else {
                                let msg = data.msg ?? "Reject order is failed!!!";
                                showNotification('alert-danger', msg, 'bottom', 'left')
                            }
                        },
                        complete: function() {
                            $('#id_loader').html('');
                        },
                        error: errorAjax,
                    });
                }
            });
        }
    });
}

function processToAcceptHead(t) {
    let row = t.parents("tr");
    let data = row.data("approvalData");
    
    Swal.fire({
        title: 'Are you sure you want to accept order ' + data.id + '?',
        type: "question",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Accept Order !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            var bs = $('#id_baseurl').val();
            var form = new FormData();
            form.append('id', data.id);
            form.append('approval', 1);
            $.ajax({
                // url: bs + "booking/post/cancelbook",
                url: bs + ajax.url.post_process_order_approval_head,
                type: "POST",
                data: form,
                processData: false,
                contentType: false,
                dataType: "json",
                beforeSend: function() {
                    $('#id_loader').html('<div class="linePreloader"></div>');
                    Swal.fire({
                        title: 'Please Wait !',
                        html: 'Process to cancel meeting',
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
                        showNotification('alert-success', "Succes accept " + data.id, 'top', 'center')
                        reloadTable();
                    } else {
                        let msg = data.msg ?? "Accept order is failed!!!";
                        showNotification('alert-danger', msg, 'bottom', 'left')
                    }
                },
                complete: function() {
                    $('#id_loader').html('');
                },
                error: errorAjax,
            });
        }
    });
}

function processToRejectHead(t) {
    let row = t.parents("tr");
    let data = row.data("approvalData");
    
    Swal.fire({
        title: 'Are you sure want reject order ' + data.id + '?',
        text: "You will reject the data order " + data.id + " !",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Reject Order !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                title: 'Reason for Rejection',
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
                    form.append('approval', 2);
                    form.append('note', result.value);
                    var bs = $('#id_baseurl').val();
                    $.ajax({
                        // url: bs + "booking/post/cancelbook",
                        url: bs + ajax.url.post_process_order_approval_head,
                        type: "POST",
                        data: form,
                        processData: false,
                        contentType: false,
                        dataType: "json",
                        beforeSend: function() {
                            $('#id_loader').html('<div class="linePreloader"></div>');
                            Swal.fire({
                                title: 'Please Wait !',
                                html: 'Process to cancel meeting',
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
                                showNotification('alert-success', "Succes reject " + data.id, 'top', 'center')
                                reloadTable();
                            } else {
                                let msg = data.msg ?? "Reject order is failed!!!";
                                showNotification('alert-danger', msg, 'bottom', 'left')
                            }
                        },
                        complete: function() {
                            $('#id_loader').html('');
                        },
                        error: errorAjax,
                    });
                }
            });
        }
    });
}

function openDetail(t) {
    let row = t.parents("tr");
    let item = row.data("approvalData");
    $.ajax({
        url: bs + ajax.url.get_order_detail + "/" + item.id,
        dataType: "json",
        type: "GET",
        // data: form,
        // processData: false,
        // contentType: false,
        beforeSend: function() {
            $('#id_loader').html('<div class="linePreloader"></div>');
            Swal.fire({
                title: 'Please Wait !',
                html: 'Process to cancel meeting',
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
                let collection = data.collection;
                let orderDetail = collection.order_detail;
                let tbody = "";
                if (orderDetail.length > 0) {
                    orderDetail.forEach((item, index) => {
                        tbody += `
                            <tr>
                                <td style="text-align: center;">${index + 1}</td>
                                <td>${item.name}</td>
                                <td style="text-align: center;">${item.qty}</td>
                                <td>${item.note}</td>
                            </tr>
                        `;
                    });
                } else {
                    tbody = `
                        <tr>
                            <td colspan="4" style="text-align: center;">No data available</td>
                        </tr>
                    `;
                }
                
                Swal.fire({
                    title: "",
                    text: "",
                    type: "",
                    customClass: "swal-wide",
                    showConfirmButton: false,
                    showCancelButton: true,
                    cancelButtonColor: '#d33',
                    cancelButtonText: 'Close !',
                    html: `
                        <div class="card" style="width: 100%; text-align: left; border: 0px !important; box-shadow: none;">
                            <div class="card-body">
                                <div class="card-title" style="font-size: 20px; padding-bottom: 16px;"><b>Order Detail</b></div>
                                <div class"card-text" style="font-size: 14px; padding-bottom: 16px;">
                                    <table class="table table-striped">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center;">No</th>
                                                <th style="text-align: center;">Order Item</th>
                                                <th style="text-align: center;">Qty</th>
                                                <th style="text-align: center;">Note</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            ${tbody}
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    `
                });
            } else {
                showNotification('alert-danger', "Get data failed!!!", 'bottom', 'left')
            }
        },
        complete: function() {
            $('#id_loader').html('');
        },
        error: errorAjax,
    });
}