var tblAttendees;
var dataAttendees = {};

function clearTableAttendees() {
    if (tblAttendees != null) {
       tblAttendees.destroy();
    }
}

function initTableAttendees() {
    
    tblAttendees = $('#id_tbl_attendees').DataTable({
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
       
    }).on('deselect.dt', function(e, dt, type, indexes) {
       
    }).on('select.dt', function(e, dt, type, indexes) {
       
    });

}

function ocOrganizerBuilding() {
    var bbb = $('#id_attendees_building_search').val();
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
    $(`#id_attendees_room_search`).html(html);
    select_enable();
}


function clickSubmit(btn) {
    $('#' + btn)[0].click();
}

function filterAttendees() {
    var modules = getModule();
    var daterange = $("#id_attendees_daterange_search").val();
    var daterangesp = daterange.split("-");
    var spp1 = daterangesp[0].split("/");
    var spp2 = daterangesp[1].split("/");
    var exxtdt1 = `${spp1[2].trim()}-${spp1[0].trim()}-${spp1[1].trim()}`;
    var exxtdt2 = `${spp2[2].trim()}-${spp2[0].trim()}-${spp2[1].trim()}`;
    var date1 = moment(exxtdt1.trim()).format('YYYY-MM-DD');
    var date2 = moment(exxtdt2.trim()).format('YYYY-MM-DD');
    var building_search = $('#id_attendees_building_search').val();
    var room_search = $('#id_attendees_room_search').val();
    var department_search = $('#id_attendees_department_search').val();
    var employee_search = $('#id_attendees_employee_search').val();
    var bodyreq = {
        date1_search: date1,
        date2_search: date2,
        building_search: building_search,
        room_search: room_search,
        department_search: department_search,
        gmt: localtimezone,
        employee_search: employee_search,
        timezone: localtimezone,
    }
    var bs = $('#id_baseurl').val();
    $.ajax({
        url: bs + "report/get/room-attendees",
        type: "POST",
        dataType: "json",
        data: bodyreq,
        beforeSend: function() {
            // $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            if (data.status == "success") {
                clearTableAttendees();
                var col = data.collection;
                var html = '';
                var num = 0;
                var maxtotal = col.length;
                $('#id_count_total_attendees').html(maxtotal);
                var period =  moment(date1).format('ll') + " - " + moment(date2).format('ll') ;
                dataAttendees['data'] = col;
                dataAttendees['building'] = $('#id_attendees_building_search option:selected').text();;
                dataAttendees['room'] = $('#id_attendees_room_search option:selected').text();
                dataAttendees['department'] = $('#id_attendees_department_search option:selected').text();
                dataAttendees['employee'] = $('#id_attendees_employee_search option:selected').text();
                dataAttendees['period'] = period;
                dataAttendees['total'] = col.length;
                dataAttendees['modules'] = modules;
                // dataAttendees['total'] = col.length;

                $.each(col, (index, item) => {
                    num++;
                    html += '<tr>';
                    html += `<td>${num}</td>`;
                    html += `<td>${item.name}<br><small>${item.nik_display}</small></td>`;
                    html += `<td>${item.company_name}<br><small><b>${item.department_name}</b></small></td>`;
                    html += `<td>${item.total_meeting}</td>`;
                    html += `<td>${item.total_present}</td>`;
                    html += `<td>${item.total_absent}</td>`;
                    html += `<td>${item.total_duration}</td>`;
                    if (modules['room_adv']['is_enabled'] == 1) {
                        html += `<td>${item.total_attendees_checkin}</td>`;
                    }
                   

                   
                    html += '</tr>';
                    num++;
                });
                $('#id_tbl_attendees tbody').html(html);
                initTableAttendees();
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

function alertExportToAttendeesToExcell(type) {
    var bs = $('#id_baseurl').val();
    $.ajax({
        url: `${bs}report/get/room-preview-attendees?action=${type}&save=true&report_type=attendees`,
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
                var url = `${bs}report/get/room-preview-attendees?action=${type}&save=false&report_type=attendees`;
                window.open(url, '_blank');

            } else {
            }
        },
        error: errorAjax
    })
}
