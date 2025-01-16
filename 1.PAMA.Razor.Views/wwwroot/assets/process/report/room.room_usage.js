var tblRoomUsage;
function clearTableeRoomUsage() {
    if (tblRoomUsage != null) {
        tblRoomUsage.destroy();
    }
}

function initTableRoomUsage() {
    tblRoomUsage = $('#id_tbl_room').DataTable({
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
        // console.log(dataTB.rows({selected:true}).data().length)

        // if(dataTB.rows({selected:true}).data().length < count_room ){
        //     $('#ck_all').attr("checked", false);
        // }
    }).on('select.dt', function(e, dt, type, indexes) {
        // console.log(dataTB.rows({selected:true}).data().length, count_room)
        // // $('#ck_all').find(".filled-in")
        // if(dataTB.rows({selected:true}).data().length == count_room ){
        //     $('#ck_all').attr("checked", true);
        // }
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

function ocBuilding() {
    var bbb = $('#id_roomusage_building_search').val();
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
    $('#id_roomusage_room_search').html(html);
    select_enable();
}


function clickSubmit(btn) {
    $('#' + btn)[0].click();
}


function checkInvoiceStatus(enabled_stt, stt) {
    // console.log('globalStatusInvoice', globalStatusInvoice);
    if (enabled_stt == 0 && enabled_stt == "0") {
        // console.log(globalStatusInvoice)
        return globalStatusInvoice[3].name;
    } else {
        var sttus = stt - 0;
        var ret = "";
        $.each(globalStatusInvoice, (index, item) => {
            if (item.id == sttus) {
                ret = item.name
            }
        })
        return ret;
    }


}


function initRoom(date1 = "", date2 = "") {
    var modules = $('#id_modules').val();
    var daterange = $("#id_roomusage_daterange_search").val();
    var daterangesp = daterange.split("-");
    var spp1 = daterangesp[0].split("/");
    var spp2 = daterangesp[1].split("/");
    var exxtdt1 = `${spp1[2].trim()}-${spp1[0].trim()}-${spp1[1].trim()}`;
    var exxtdt2 = `${spp2[2].trim()}-${spp2[0].trim()}-${spp2[1].trim()}`;
    var date1 = moment(exxtdt1.trim()).format('YYYY-MM-DD');
    var date2 = moment(exxtdt2.trim()).format('YYYY-MM-DD');
    var building_search = $('#id_roomusage_building_search').val();
    var room_search = $('#id_roomusage_room_search').val();
    var department_search = $('#id_roomusage_department_search').val();
    var bodyreq = {
        date1_search: date1,
        date2_search: date2,
        building_search: building_search,
        room_search: room_search,
        department_search: department_search,
        gmt: localtimezone,
        timezone: localtimezone,
    }

    // conso
    var bs = $('#id_baseurl').val();
    $.ajax({
        // url : bs+"report/get/room-usage/"+date1+"/"+date2+"/"+alo,
        url: bs + "report/get/room-usage",
        type: "POST",
        dataType: "json",
        data: bodyreq,
        beforeSend: function() {
            // $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            if (data.status == "success") {
                clearTableeRoomUsage();
                var col = data.collection.result;
                var userg = data.collection.user;
                var html = '';
                var num = 0;
                var maxtotalmeeting = col.length;
                $('#id_count_total_meeting').html(maxtotalmeeting);
                $.each(col, (index, item) => {
                    num++;
                    var extendTime = item.extended_duration - 0;
                    var time1 = moment(item.start).format('HH:mm');
                    // var time2 = moment(item.end).add().format('HH:mm');
                    var time2 = moment(item.end).add(extendTime, 'minutes').format("HH:mm ");

                    var datetime = item.date + " " + time1 + " - " + time2;
                    var dur = (item.total_duration - 0) + (item.extended_duration - 0);
                    var dur_per_meeting = item.duration_per_meeting;
                    var sH = 60;
                    var sDVHours = sH / dur_per_meeting; // divide hour into minutes
                    var setHourString = "";
                    if (dur_per_meeting == 60) {
                        var setHour = dur / item.duration_per_meeting;
                    } else {
                        var setHour = dur / (dur_per_meeting * sDVHours);
                    }
                    if (isInt(setHour) == false) {
                        var splitJamFloat = setHour.toString().split(".");
                        var dataMinute = ((splitJamFloat[1] - 0) * 6) / 10;
                        setHourString = `${splitJamFloat[0]} hours ${dataMinute} minutes`
                    } else {
                        setHourString = `${setHour} hours`;
                    }

                    setHourString = getTimeFromMins(dur);

                    var rent_cost = numeral(item.cost_total_booking).format('$0,0.0');
                    var status_invoice = "";
                    if (item.alcoation_type_invoice_status == item.alocation_invoice_status) {
                        status_invoice = checkInvoiceStatus(item.alcoation_type_invoice_status, item.invoice_status);
                    } else {
                        status_invoice = checkInvoiceStatus(item.alocation_invoice_status, item.invoice_status);
                    }
                    html += '<tr>';
                    html += '<td>' + num + '</td>';
                    html += '<td>';
                    if (userg == 1) {
                        html += '<div class="btn-group">\
                                    <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">\
                                        ACTION <span class="caret"></span>\
                                    </button>\
                                    <ul class="dropdown-menu">\
                                        <li><a href="javascript:void(0);" data-type="excell" data-bookid="' + item.booking_id + '" onclick="alertExport($(this))" >EXPORT TO EXCELL</a></li>\
                                    </ul>\
                                </div>';

                    } else if (userg == 2) {
                        html += '<div class="btn-group">\
                                    <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">\
                                        ACTION <span class="caret"></span>\
                                    </button>\
                                    <ul class="dropdown-menu">\
                                        <li><a href="javascript:void(0);" data-type="excell" data-bookid="' + item.booking_id + '" onclick="alertExport($(this))" >EXPORT TO EXCELL</a></li>\
                                        <li role="separator" class="divider"></li>\
                                    </ul>\
                                </div>';
                    } else {
                        html += '<div class="btn-group">\
                                    <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">\
                                        ACTION <span class="caret"></span>\
                                    </button>\
                                    <ul class="dropdown-menu">\
                                        <li><a href="javascript:void(0);" data-type="excell" data-bookid="' + item.booking_id + '" onclick="alertExport($(this))" >EXPORT TO EXCELL</a></li>\
                                        <li role="separator" class="divider"></li>\
                                    </ul>\
                                </div>';
                    }

                    var room_name = `<b>${item.room_name}</b><br>${item.room_location}`;
                    html += '</td>';
                    html += '<td>' + item.booking_id + '</td>';
                    html += '<td>' + item.title + '</td>';
                    html += '<td>' + datetime + '</td>';
                    html += '<td>' + room_name + '</td>';
                    // html += '<td>'+item.room_location+'</td>';
                    html += '<td>' + item.alocation_name + '</td>';
                    html += '<td>' + item.num_partisipant + ' Attendees</td>';
                    html += '<td>' + setHourString + '</td>';
                    if (modules['price']['is_enabled'] == 1) {
                        html += '<td>' + rent_cost + '</td>';
                        html += '<td>' + status_invoice + ' </td>';
                        // html += '<td>'+numeral(price).format('$ 0,0.00');+'</td>';
                    }
                    html += '</tr>';
                });
                $('#id_tbl_room tbody').html(html);
                initTableRoomUsage();
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

function alertExportToAll(type) {
    var bs = $('#id_baseurl').val();
    var daterange = $("#id_roomusage_daterange_search").val();
    var daterangesp = daterange.split(" - ");
    var date1 = moment(daterangesp[0]).format('YYYY-MM-DD');
    var date2 = moment(daterangesp[1]).format('YYYY-MM-DD');
    var alo = $('#id_roomusage_department_search').val();
    var building = $('#id_roomusage_building_search').val();
    var room = $('#id_roomusage_room_search').val();


    // if(alo == ""){
    //     alo = "all"
    // }
    $q = `department=${alo}&date1=${date1}&date2=${date2}&room=${room}&building=${building}`;
    switch (type) {
        case "excell":
            // var url = bs+"report/export-all/room/excell/"+date1+"/"+date2+"/"+alo;
            var url = bs + "report/export-all/room/excell?" + $q;
            exportExcell(url)
            break;
        case "pdf":
            var url = bs + "report/export-all/room/excell/" + date1 + "/" + date2 + "/" + alo;
            exportExcell(url)
            break;
        default:
            var msg = "Something wrong to export format";
            showNotification('alert-danger', msg, 'top', 'center')
            break;

    }
}

function alertExport(t) {
    var bs = $('#id_baseurl').val();
    var type = t.data("type");
    var bookid = t.data("bookid");
    var daterange = $("#id_roomusage_daterange_search").val();
    var daterangesp = daterange.split(" - ");
    var date1 = moment(daterangesp[0]).format('YYYY-MM-DD');
    var date2 = moment(daterangesp[1]).format('YYYY-MM-DD');
    switch (type) {
        case "excell":
            var url = bs + "report/export/room/excell/" + bookid + "/" + date1 + "/" + date2;
            exportExcell(url)
            break;
        case "pdf":
            var url_download = bs + "api/" + 'schedule/report-meeting/' + bookid;
            exportExcell(url_download)
            break;
        default:
            var msg = "Something wrong to export format";
            showNotification('alert-danger', msg, 'top', 'center')
            break;

    }
}

function exportExcell(url) {
    Swal.fire({
        title: 'Are you sure you want export it?',
        text: "",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Export it !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            // window.location.href = url
            window.open(url, '_blank');

        } else {

        }
    })
}