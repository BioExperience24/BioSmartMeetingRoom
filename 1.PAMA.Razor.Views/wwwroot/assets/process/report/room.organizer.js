var tblOrganizer;
var dataOrganizer = {};

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