const bs = $("#id_baseurl").val();
let localtimezone = moment.tz.guess();
let gRoom = [];
let globalStatusInvoice = [];
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
        await fetchRoom();
    } finally {
        // load table
        initTable();
    }
}

async function fetchRoom() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_rooms
        });

        if (data.status == "success")
        {
            gRoom =  data.collection;

            initFilterRoom();
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

function initFilterRoom() {
    $(`#id_room_search`).empty();
    
    $(`#id_room_search`).append(`<option value="">All Room</option>`);

    $.each(gRoom, function (_, item) { 
        let opt = document.createElement("option");

        $(opt).text(`${item.name}`);
        $(opt).val(item.radid);
        
        $(`#id_room_search`).append(opt);
        selectEnable();
    });
}

function initTable() {
    let columns = [
        {data:"no", name:"no", searchable:false, orderable:false},
        {data:"booking_id", name:"booking_id", searchable:false, orderable:false},
        {data:"meeting_time", name:"meeting_time", searchable:false, orderable:false, render: function(_, _, item) {
            return `${item.booking_date} ${item.time}`;
        }},
        {data:"created_by", name:"created_by", searchable:false, orderable:false, render: function(_, _, item) {
            return item.created_by ?? "";
        }},
        {data:"meeting", name:"meeting", searchable:false, orderable:false, render: function(_, _, item) {
            let status = "";
            let momentEnd = moment(item.server_end);
            let momentStart = moment(item.server_start);
            let extendTime = item.extended_duration -0;
            let diffMomentEnd = momentEnd.add(extendTime, 'minutes');
            
            if (item.is_approve == 1) {
                if (moment().unix() > momentEnd.unix()) {
                    status = "<i style='color:red;'>Expired</i>"
                } else if (
                    moment().unix() <= momentEnd.unix() &&
                    moment().unix() >= momentStart.unix()
                ) {
                    status = "<i style='color:light-green;'>Meeting in progress</i>"; // meeting dimulai
                } 
                else if (
                    momentStart.diff(moment(), 'minutes') <= 0
                ) {
                    status = "<i style='color:blue;'>Ongoing</i>"; // antrian
                }
                else if (
                    // moment().unix() <= moment(item.date + " " + item.start).unix()
                    moment().unix() <= moment(item.start).unix()
                ) {
                    status = "<i style='color:blue;'>Meeting queue</i>"; // antrian
                }
                else if (
                    moment().diff(diffMomentEnd) >= 0
                ) {
                    status = "<i style='color:light-green;'>Meeting Expired</i>";
                }
            } else {
                if (moment().unix() > momentEnd.unix()) {
                    status = "<i style='color:red;'>Expired</i>"
                } else if (
                    moment().diff(diffMomentEnd) >= 0
                ) {
                    status = "<i style='color:light-green;'>Meeting Expired</i>";
                } else  {
                    // status = "<i style='color:blue;'>Meeting Awaiting Approval</i>"; // antrian
                    status = "<i style='color:blue;'>Pending</i>"; // antrian
                }
            }
            
            if ((item.is_alive-0)  == 0) {
                status = "<i style='color:light-green;'>Pending</i>"; // antrian
            }

            if (item.is_approve == 2) {
                status = "<i style='color:red;'>Meeting Rejected</i>"; // antrian
            }

            if (item.is_canceled == 1) {
                status = `<i style='color:red; cursor:pointer;' onclick="openPopupReasonCanceled($(this))">Meeting Canceled</i>`; // antrian
            }

            if (item.is_expired == 1 || item.end_early_meeting == 1) {
                var color = item.end_early_meeting == 1 ? "light-green" : "red";
                status = `<i style='color:${color};'>Meeting Expired</i>`; // antrian
            }
            return `${item.room_name} - ${item.title} <br> (${status})`;
        }},
        {data:"user_approval", name:"user_approval", searchable:false, orderable:false, render: function(_, _, item) {
            return item.user_approval ?? "";
        }},
        {data:"status", name:"status", searchable:false, orderable:false, render: function(_, _, item) {
            let status = "";

            let bookingTz = item.timezone == "SE Asia Standard Time" ? "Asia/Jakarta" : item.timezone;
            let momentEnd = changeTimeIntoTimezone(item.server_end, bookingTz);
            let extendTime = item.extended_duration -0;
            let diffMomentEnd = momentEnd.add(extendTime, 'minutes');
            
            switch (item.is_approve) {
                case 0:
                    status = "Pending"
                    break;

                case 1:
                    status = "Approved"
                    break;

                case 2:
                    status = "Rejected"
                    break;
            }
            
            if ((moment().unix() > momentEnd.unix() || moment().diff(diffMomentEnd) >= 0) && item.is_approve == 0) {
                status = "Expired";
            }

            if (item.is_canceled == 1) {
                status = "Canceled";
            }
            
            return status;
        }},
        {data:"action", name:"action", searchable:false, orderable:false, render: function(_, _, item) {
            let bookingTz = item.timezone == "SE Asia Standard Time" ? "Asia/Jakarta" : item.timezone;
            let momentEnd = changeTimeIntoTimezone(item.server_end, bookingTz);
            let extendTime = item.extended_duration -0;
            let diffMomentEnd = momentEnd.add(extendTime, 'minutes');

            if (item.is_approve != 0 || (moment().unix() > momentEnd.unix() || moment().diff(diffMomentEnd) >= 0) || item.is_canceled == 1) {
                return ``;
            }
            return `
                <button type="button" class="btn btn-info waves-effect" onclick="processToAccept($(this))">Accept</button>
                <button type="button" class="btn btn-danger waves-effect" onclick="processToReject($(this))">Reject</button>
            `;
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
            url: bs + ajax.url.get_booking_approval_datatables,
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
                param.room_id = $("#id_room_search").val();
            },
            dataSrc: function (json) {
                // Map properties to the expected structure
                json.draw = json.collection.draw;
                json.recordsFiltered = json.collection.recordsFiltered;
                json.recordsTotal = json.collection.recordsTotal;

                // $("#id_count_total_meeting").text(json.collection.recordsTotal);

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
        "minDate": moment(),
        // "drops": "up",
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
        title: 'Are you sure you want to accept Meeting ' + data.title + '?',
        type: "question",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Accept Meeting !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            var bs = $('#id_baseurl').val();
            var form = new FormData();
            form.append('booking_id', data.booking_id);
            form.append('approval', 1);
            $.ajax({
                // url: bs + "booking/post/cancelbook",
                url: bs + ajax.url.post_process_meeting_approval,
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
                        showNotification('alert-success', "Succes accept " + data.title, 'top', 'center')
                        // init();
                        reloadTable();
                    } else {
                        showNotification('alert-danger', "Accept " + data.title + " meeting is failed!!!", 'bottom', 'left')
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

function processToReject(t) {
    let row = t.parents("tr");
    let data = row.data("approvalData");
    
    Swal.fire({
        title: 'Are you sure want reject ' + data.title + ' of meeting?',
        text: "You will reject the data booking " + data.title + " !",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Reject Meeting !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            var form = new FormData();
            form.append('booking_id', data.booking_id);
            form.append('approval', 2);
            var bs = $('#id_baseurl').val();
            $.ajax({
                // url: bs + "booking/post/cancelbook",
                url: bs + ajax.url.post_process_meeting_approval,
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
                        showNotification('alert-success', "Succes reject " + data.title, 'top', 'center')
                        // init();
                        reloadTable();
                    } else {
                        showNotification('alert-danger', "Reject " + data.title + " meeting is failed!!!", 'bottom', 'left')
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