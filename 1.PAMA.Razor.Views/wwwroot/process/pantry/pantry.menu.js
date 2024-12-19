var tblMenu;
var dataMenu = {};

function clearTableMenu() {
    if (tblMenu != null) {
       tblMenu.destroy();
    }
}

function initTableMenu() {
    
    tblMenu = $('#id_tbl_menu').DataTable({
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


function filterMenu() {
    var modules = getModule();
    var pantry_search = $('#id_menu_pantry_search').val();
    var bodyreq = {
       
        pantry_search: pantry_search,
        gmt: localtimezone,
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