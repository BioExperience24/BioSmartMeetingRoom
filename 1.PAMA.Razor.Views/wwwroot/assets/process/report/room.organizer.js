var tblOrganizer;
var tblOrganizerUsage;

$("#id_organizer_search").click(function (e) { 
    e.preventDefault();
    reloadOrganizerTable();
});

function initOrganizerUsageTable() {
    let module = getModule();
    
    let columns = [
        {data:"no", name:"no", searchable:false, orderable:false},
        {data:"name", name:"name", searchable:false, orderable:false},
        {data:"company_department_name", name:"company_department_name", searchable:false, orderable:false,render: function(_, _, item) {return `${item.company_name} <br> <b>${item.department_name}</b>`}},
        {data:"total_meeting", name:"total_meeting", searchable:false, orderable:false},
        {data:"total_reschedule", name:"total_reschedule", searchable:false, orderable:false},
        {data:"total_cancel", name:"total_cancel", searchable:false, orderable:false},
        {data:"total_duration", name:"total_duration", searchable:false, orderable:false, render: function(_, _, item) { return getTimeFromMins(item.total_duration); }},
        {data:"total_attendees", name:"total_attendees", searchable:false, orderable:false},
    ];

    if (module.room_adv.is_enabled == 1) {
        columns.push(
            {data:"total_attendees_checkin", name:"total_attendees_checkin", searchable:false, orderable:false},
            {data:"total_approve", name:"total_approve", searchable:false, orderable:false},
        );
    }

    tblOrganizerUsage = $("#id_tbl_organizer").DataTable({
        searching: false,
        bLengthChange: false,
        bInfo: true,
        ordering: false,
        columns: columns,
        // "order": [[ 0, 'asc' ]],
        ajax: {
            url: bs + ajax.url.get_organizer_usage_datatable,
            contentType: 'application/json',
            beforeSend: function() {
                if (typeof tblOrganizerUsage != "undefined" && tblOrganizerUsage.hasOwnProperty('settings') && tblOrganizerUsage.settings()[0].jqXHR != null) {
                    tblOrganizerUsage.settings()[0].jqXHR.abort();
                }
            },
            data: function (param) {
                delete param.columns;
                param.date = $("#id_organizer_daterange_search").val();
                param.nik = $("#id_organizer_employee_search").val();
                param.building_id = $("#id_organizer_building_search").val();
                param.room_id = $("#id_organizer_room_search").val();
            },
            dataSrc: function (json) {
                // Map properties to the expected structure
                json.draw = json.collection.draw;
                json.recordsFiltered = json.collection.recordsFiltered;
                json.recordsTotal = json.collection.recordsTotal;

                $("#id_count_total_organizer").text(json.collection.recordsTotal);

                return json.collection.data;
            } 
        },
        processing: true,
        serverSide: true,
        createdRow: function( row, data, dataIndex ) {
            // console.log("--------createdRow--------");
            // console.log(row);
            // console.log(data);
            // console.log(dataIndex);
            $(row).attr('id', `organizer-${data.nik}`);
            $(row).data("organizerData", data);
        },
        drawCallback: function (settings) {
            // console.log("--------drawCallback--------");
            // console.log(settings);
        },
    });
}

function reloadOrganizerTable() {
    if (typeof tblOrganizerUsage != "undefined") {
        tblOrganizerUsage.ajax.reload();
    }
};

function clearTableOrganizer() {
    if (tblOrganizer != null) {
        tblOrganizer.destroy();
    }
}

function initTableOrganizer() {
    tblOrganizer = $('#id_tbl_organizer').DataTable({
        "scrollX": true,
        "scrollCollapse": true,
        "fixedHeader": true,
        paging: true,
        // searching:        false,
        // bFilter :         false,
        info: false,
        // scrollResize:     true,
        order: [
            [0, "asc"]
        ],
        // lengthMenu: [[5, 10, 20, 100,-1], [5, 10, 20,100, 'ALL']],
        fixedColumns: {
            leftColumns: 3,
            // rightColumns: 1
        },
        columnDefs: [{
                orderable: false,
                // className: 'select-checkbox',
                targets: 1,
                searchable: false,
            },

            {
                orderable: true,

            },
        ],
        select: {
            // style:    'multi',
            // selector: 'td:first-child'
        },
    }).on('deselect.dt', function(e, dt, type, indexes) {
       
    }).on('select.dt', function(e, dt, type, indexes) {
       
    });

    // $('#id_search').on( 'keyup', function () {
    //     // console.log(this.value)
    //     dataTB
    //         .columns( 3 )
    //         .search( this.value )
    //         .draw();
    // } );
    // $('#tbldata_wrapper').find('#tbldata_filter').hide()
}

function ocOrganizerBuilding() {
    var bbb = $('#id_organizer_building_search').val();
    var gg = [];
    var html = '<option selected value="">All Room</option>';
    if (bbb == "") {
        for (var x in gRoom) {
            html += '<option value="' + gRoom[x].radid + '">' + gRoom[x].name + '</option>';
        }

    } else {
        for (var x in gRoom) {
            if (gRoom[x].building_id != bbb) {
                continue;
            }
            html += '<option value="' + gRoom[x].radid + '">' + gRoom[x].name + '</option>';
        }
    }
    $(`#id_organizer_room_search`).html(html);
    select_enable();
}


function clickSubmit(btn) {
    $('#' + btn)[0].click();
}


// function checkInvoiceStatus(enabled_stt, stt) {
//     if (enabled_stt == 0 && enabled_stt == "0") {
//         // console.log(globalStatusInvoice)
//         return globalStatusInvoice[3].name;
//     } else {
//         var sttus = stt - 0;
//         var ret = "";
//         $.each(globalStatusInvoice, (index, item) => {
//             if (item.id == sttus) {
//                 ret = item.name
//             }
//         })
//         return ret;
//     }


// }


function filterOrganizer() {
    var modules = getModule();
    var daterange = $("#id_organizer_daterange_search").val();
    var daterangesp = daterange.split("-");
    var spp1 = daterangesp[0].split("/");
    var spp2 = daterangesp[1].split("/");
    var exxtdt1 = `${spp1[2].trim()}-${spp1[0].trim()}-${spp1[1].trim()}`;
    var exxtdt2 = `${spp2[2].trim()}-${spp2[0].trim()}-${spp2[1].trim()}`;
    
    var date1 = moment(exxtdt1.trim()).format('YYYY-MM-DD');
    var date2 = moment(exxtdt2.trim()).format('YYYY-MM-DD');
    var building_search = $('#id_organizer_building_search').val();
    var room_search = $('#id_organizer_room_search').val();
    var department_search = $('#id_organizer_department_search').val();
    var employee_search = $('#id_organizer_employee_search').val();
    var bodyreq = {
        date1_search: date1,
        date2_search: date2,
        building_search: building_search,
        room_search: room_search,
        department_search: department_search,
        gmt: localtimezone,
        timezone: localtimezone,
        employee_search: employee_search,
    }

    // conso
    var bs = $('#id_baseurl').val();
    $.ajax({
        url: bs + "report/get/room-organizer",
        type: "POST",
        dataType: "json",
        data: bodyreq,
        beforeSend: function() {
            // $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            if (data.status == "success") {
                clearTableOrganizer();
                var col = data.collection;
                // var userg = data.collection.user;
                var html = '';
                var num = 0;
                var maxtotal = col.length;
                $('#id_count_total_organizer').html(maxtotal);
                var period =  moment(date1).format('ll') + " - " + moment(date2).format('ll') ;
                dataAttendees['data'] = col;
                dataAttendees['building'] = $('#id_organizer_building_search option:selected').text();;
                dataAttendees['room'] = $('#id_organizer_room_search option:selected').text();
                dataAttendees['department'] = $('#id_organizer_department_search option:selected').text();
                dataAttendees['employee'] = $('#id_organizer_employee_search option:selected').text();
                dataAttendees['period'] = period;
                dataAttendees['total'] = col.length;
                dataAttendees['modules'] = modules;
                $.each(col, (index, item) => {
                    num++;
                    html += '<tr>';
                    html += `<td>${num}</td>`;
                    html += `<td>${item.name}<br><small>${item.nik_display}</small></td>`;
                    html += `<td>${item.company_name}<br><small><b>${item.department_name}</b></small></td>`;
                    html += `<td>${item.total_meeting}</td>`;
                    html += `<td>${item.total_reschedule}</td>`;
                    html += `<td>${item.total_cancel}</td>`;
                    html += `<td>${item.total_duration}</td>`;
                    html += `<td>${item.total_attendees}</td>`;
                    if (modules['room_adv']['is_enabled'] == 1) {
                        html += `<td>${item.total_attendees_checkin}</td>`;
                        html += `<td>${item.total_duration}</td>`;
                        html += `<td>${item.total_approve}</td>`;
                        html += `<td>${item.total_reject}</td>`;
                    }
                    html += '</tr>';
                    num++;
                });

                $('#id_tbl_organizer tbody').html(html);
                initTableOrganizer();
            } else {
                var msg = "Your session is expired, login again !!!";
                showNotification('alert-danger', msg, 'top', 'center')
            }
            $('#id_loader').html('');
        },
        error: errorAjax
    })
}

function isInt(n) {
    return Number(n) === n && n % 1 === 0;
}

function alertExportToOrganizerToExcell(type) {
    var bs = $('#id_baseurl').val();
    $.ajax({
        url: `${bs}report/get/room-preview-organizer?action=${type}&save=true&report_type=organizer`,
        type: "POST",
        // dataType:'json',
        data: dataAttendees,
        beforeSend: function() {
            loadingg('Please wait ! ', 'Loading . . . ')
        },
        success: function(data) {
            swal.close();
            $('#id_loader').html('');
            var response = {status:"fail"};
            try{
                response = JSON.parse(data);
            }catch(e){}
            if (response.status == "success") {
                var url = `${bs}report/get/room-preview-organizer?action=${type}&save=false&report_type=organizer`;
                window.open(url, '_blank');

            } else {
            }
        },
        error: errorAjax
    })
}