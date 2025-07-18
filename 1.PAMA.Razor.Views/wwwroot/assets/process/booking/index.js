let bs;
let modules;
let employeeCollection;
let buildingCollection;
let roomCollection;
let capFacility;
let departmentCollection;
let pantryPackagesCollection;
let pantryPackageDetailCollection;

let isFormAjax = "";
let isFormAjaxReserve = "";

// external attendess function
let gPartisipantManual = [];
let gEmployeesSelected = [];
let gEmployeesSelectedArray = [];
let gEmployeesRegistered = [];
let gEmployeePIC = "";

let tbldata;

let reservedTimes = [];
var gTimeSelectBookingRes = [];

let isAdditionalPartisipantManualOpen = false;

const timepickerOptionsTodayFrom = {
    timeFormat: 'HH:mm ',
    interval: 15,
    // minTime: moment(moment().format('YYYY-MM-DD') + " " +moment().format("HH:mm:")+"00").subtract(1, 'minutes').format("HH"),
    minTime: "00",
    maxTime: '23:45',
    // defaultTime: moment().format("HH:mm:")+"00",
    defaultTime: getMinutesForNow(moment()).format("HH:mm"),
    startTime: getMinutesForNow(moment()).format("HH:mm"),
    dynamic: false,
    dropdown: true,
    scrollbar: true,
    change: function(time) {
        // var element = $(this), text;
        // var timepicker = element.timepicker();
        // var dataFrom = moment(time);
        // timepickerOptionsTodayUntil['minTime'] = dataFrom.format("HH"),
        // timepickerOptionsTodayUntil['defaultTime'] = getMinutesForNow(dataFrom).format("HH:mm"),
        // timepickerOptionsTodayUntil['startTime'] =  getMinutesForNow(dataFrom).format("HH:mm"),
        // $('#id_book_filter_until').timepicker(timepickerOptionsTodayUntil);
        // $('#id_book_filter_until').timepicker().destroy();
        // $('#id_book_filter_until').timepicker(timepickerOptionsTodayUntil);
        // filterbooking_search['from'] = dataFrom.format("HH:mm")+":00";

        var dataFrom = getMinutesForNow(moment(time)).toDate();
        $('#id_book_filter_until').timepicker("setTime", dataFrom);
    }
}

const timepickerOptionsTodayUntil = {
    timeFormat: 'HH:mm',
    interval: 15,
    // minTime: moment(moment().format('YYYY-MM-DD') + " " +moment().format("HH:mm:")+"00").subtract(1, 'minutes').format("HH"),
    minTime: "00",
    maxTime: '23:45',
    defaultTime: getMinutesForNow(moment()).add(5, 'minutes').format("HH:mm"),
    startTime:  getMinutesForNow(moment()).format("HH:mm"),
    dynamic: false,
    dropdown: true,
    scrollbar: true,
    /* change: function(time) {
        var element = $(this), text;
        var timepicker = element.timepicker();
        var dataMoment = moment(time);
        // filterbooking_search['until'] = dataMoment.format("HH:mm")+":00";
    } */
}

const capSeats = {
    '1_5' : {text : "1-5", min : 1, max:5},
    '6_10' : {text : "6-10", min : 6, max:10},
    '11_20' : {text : "11-20", min : 11, max:20},
    '20' : {text : "20+", min : 20, max:null},
    '' : {text : "Any", min : 0, max:null},
}

let lastSelectedHost;

$(function () {
    bs = $('#id_baseurl').val();
    
    init_modules();
    
    init_datetimepicker_filter();
    init_date_book_filter();
    init_time_book_filter();

    runTooltip();

    init_master_data();
    // get_employees();
    // get_buildings();
    // get_rooms();
    // get_facilities();
    // get_departments();
    // get_pantry_packages();

    // init_table_booking_list();

    // $("#form-find-reserve").trigger("submit");
});

async function init_master_data() {
    try {
        $("#id_area_skeleton_loading").removeClass("hidden");

        await get_employees();
        await get_buildings();
        await get_rooms();
        await get_facilities();
        await get_departments();
        await get_pantry_packages();
    } finally {
        await $("#form-find-reserve").trigger("submit");
        await init_table_booking_list();
    }
}

$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    let roomCategory = $(e.target).data("roomCategory");

    if (roomCategory != undefined) {
        // let lastRoomCategory = $("#id_book_filter_room_category").val();

        // if (roomCategory != lastRoomCategory) {
        //     $("#id_book_filter_room_category").val(roomCategory);

        //     $("#form-find-reserve").trigger("submit");
        // }

        if (roomCategory == "trainingroom") {
            $("#section_id_book_filter_date_until").show();
        } else {
            $("#section_id_book_filter_date_until").hide();
        }

        // reset date until filter
        let startDate = $("#id_book_filter_when").val();
        let endDatePicker = $('#id_book_filter_date_until').data('daterangepicker');
        $('#id_book_filter_date_until').val(startDate);
        endDatePicker.setStartDate(startDate);
        endDatePicker.setEndDate(startDate);

        init_date_book_filter(roomCategory);

        $("#id_book_filter_room_category").val(roomCategory);

        $("#form-find-reserve").trigger("submit");
    }
    
});

$("#id_frm_crt_alocation_id").on("change", function () {
    const t = $(this);
    $("#id_frm_crt_alocation_name").val(t.find(":selected").text());
});

$("#id_frm_crt_meeting_category").on("change", function () {
    const t = $(this);
    get_pantry_package_detail(t.val());
});

// change organizer
$("#id_frm_crt_pic").on("change", function () {
    const t = $(this);

    genDepartmentFromPic(t.val());
    addInternalParticipantByHost(t.val());
});

$(document).on("keyup", ".inp_qty_package_detail", function () {
    const t = $(this);
    let row = t.parents("tr");
    let qty = t.val();
    let packageDetailID = row.data("pkgDetailId");
    
    $.each(pantryPackageDetailCollection, function (key, item) { 
        if (item.id == packageDetailID) {
            pantryPackageDetailCollection[key]["qty"] = parseInt(qty);
        }
    });
});

$(document).on("keyup", ".inp_note_package_detail", function () {
    const t = $(this);
    let row = t.parents("tr");
    let note = t.val();
    let packageDetailID = row.data("pkgDetailId");
    
    $.each(pantryPackageDetailCollection, function (key, item) { 
        if (item.id == packageDetailID) {
            pantryPackageDetailCollection[key]["note"] = note;
        }
    });
});

$("#form-find-reserve").submit(function(event) {
    event.preventDefault();
    if (isFormAjax != "") {
        isFormAjax.abort();
    }
    
    const form = $(this);

    // let formData = new FormData(form[0]);

    // console.log(formData);
    // console.log(form.serialize());


    isFormAjax = $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: bs + ajax.url.get_available_rooms,
        data: form.serialize(),
        beforeSend: function() {
            $("#id_area_booking_ad_room").empty();
            $("#id_area_skeleton_loading").removeClass("hidden");
        },
        success: function (data) {
            if (data.status == "success")
            {
                renderRoomCard(data.collection);
            } else {
                var msg = "Your session is expired, login again !!!";
                showNotification('alert-danger', msg, 'top', 'center');
            }
        },
        error: errorAjax,
        complete: function() {
            $("#id_area_skeleton_loading").addClass("hidden");
        }
    });
});

$("#id_frm_crt").submit(function(event) {
    event.preventDefault();
    if (isFormAjaxReserve != "") {
        isFormAjaxReserve.abort();
    }
    
    const form = $(this);
    let data = form.serializeArray();

    data.push({name: "device", value: "web"});

    if (gPartisipantManual.length > 0) {
        $.each(gPartisipantManual, function (_, item) {
            // company // email // name // position
            data.push({name: "external_attendees[]", value: JSON.stringify(item)});
        });
    }

    if (gEmployeesSelected.length > 0)  {
        $.each(gEmployeesSelectedArray, function (_, item) { 
            let d = {
                id: item.id,
                nik: item.nik,
                company: item.company_name,
                email: item.email,
                name: item.name,
                position: item.department_name,
                is_vip: item.is_vip
            };

            data.push({name: "internal_attendees[]", value: JSON.stringify(d)});
        });
    }

    if (pantryPackageDetailCollection != null) {
        $.each(pantryPackageDetailCollection, function (_, item) { 
            let d = {
                menu_id: item.id,
                pantry_id: item.pantry_id,
                qty: item.qty,
                note: item.note,
            }
            data.push({name: "menu_items[]", value: JSON.stringify(d)});
        });
    }

    // Trigger HTML5 validity.
    var reportValidity = form[0].reportValidity();

    if(reportValidity) {

        isFormAjaxReserve = $.ajax({
            type: "POST",
            // contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.post_create_booking,
            // data: objectify_form(form.serializeArray()),
            // data: form.serialize(),
            data: $.param(data),
            beforeSend: function()
            {
                $('#id_loader').html('<div class="linePreloader"></div>');
                disableAllForm();
            },
            success: function (data) {
                $('#id_loader').html('');
                enabledAllForm();
                if (data.status == "success") {
                    onButtonBack();
                    reloadTable();
                    tabToIndex(1); // to booking list
                } else {
                    var msg = "Your session is expired, login again !!!";
                    if (data.msg != undefined || data.msg != "") {
                        msg = data.msg;
                    }
                    showNotification('alert-danger', msg, 'top', 'center');
                }
            },
            error: errorAjax,
            complete: function(data) {
                enabledAllForm();
            }
        });
    }
});

$("#id_schedule_search").click(function (e) { 
    e.preventDefault();
    reloadTable();
});

$("#id_frm_crt_timestart").change(function (e) { 
    e.preventDefault();
    let val = $(this).val();

    let times = generateTimes(moment(val, "HH:mm"));
    generateOptTime("#id_frm_crt_timeend", times, reservedTimes, val);
});

$('#frm_reschedule').submit(function(e) {
    e.preventDefault();
    var bs = $('#id_baseurl').val();
    var form = $('#frm_reschedule').serialize();
    var dtt = $('#id_frm_res_date').val();
    var unixTimeStart = moment($('#id_frm_res_start_input').val()).format('X');
    var unixTimeEnd = moment($('#id_frm_res_end_input').val()).format('X');


    // console.log($('#id_frm_res_start_input').val())
    if ($('#id_frm_res_date').val() == "") {
        Swal.fire('Attention !!!', 'Date coloumn cannot be empty!', 'warning')
        // showNotification('alert-danger', "Date coloumn cannot be empty",'top','center')
        $('#id_frm_res_date_dummy').focus();
        return false;
    } else if ($('#id_frm_res_start_input').val() == "" || $('#id_frm_res_end_input').val() == "") {
        Swal.fire('Attention !!!', 'Please selected the time!', 'warning')
        // showNotification('alert-danger', "Please selected the time",'top','center')
        return false;
    } else if (unixTimeStart > unixTimeEnd || unixTimeStart == unixTimeEnd) {
        Swal.fire('Attention !!!', 'Start time must to be more than Finish time, or start & finish time cannot be equal!', 'warning')
        // showNotification('alert-danger', "Start time must to be more than Finish time, or start & finish time cannot be equal.",'top','center')
        return false;
    }
    if ($('#id_frm_res_date_dummy').val() == "") {

        Swal.fire('Attention !!!', 'Please select a date ', 'warning')
        return false;
    }
    Swal.fire({
        title: 'Confirmation?',
        text: "Are you sure to reschedule this meeting? ",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Reschedule Meeting !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            $.ajax({
                // url: bs + "booking/post/rebook",
                url: bs + ajax.url.post_reschedule_booking,
                type: "post",
                dataType: "json",
                data: form,
                beforeSend: function() {
                    $('#id_loader').html('<div class="linePreloader"></div>');
                    Swal.fire({
                        title: 'Please Wait !',
                        html: 'Process saving',
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
                        showNotification('alert-success', "Succes reschedule a booking " + $('#id_res_text_name').html(), 'top', 'center')
                        $('#id_mdl_reschedule').modal('hide')
                        Swal.fire({
                            title: 'Messaage',
                            text: "Your schedule of meeting success to save ",
                            type: "warning",
                            showCancelButton: false,
                            allowOutsideClick: false,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Close !',
                            cancelButtonText: 'Close !',
                            reverseButtons: true
                        }).then((result) => {
                            if (result.value) {
                                window.location.reload();
                            } else {

                            }
                        })
                    } else {
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg, 'top', 'center')
                    }
                },
                error: errorAjax
            })
        } else { }
    });
});

// Get API
async function init_modules() {
    try {
        var data = await get_modules(
            "module_pantry",
            "module_automation",
            "module_price",
            "module_int_365",
            "module_int_google",
            "module_room_advance",
            "module_user_vip"
        );
        
        if (data.status == "success")
        {
            modules =  data.collection;
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

async function get_employees () {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_employees
        });

        if (data.status == "success")
        {
            employeeCollection =  data.collection;

            init_employee_filter();
            init_pic_reserve_form();
            init_internal_attendees_reverse_form();
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

async function get_buildings() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_buildings
        });

        if (data.status == "success")
        {
            buildingCollection =  data.collection;

            init_building_filter();
            init_location_filter();
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

async function get_rooms() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_rooms
        });

        if (data.status == "success")
        {
            roomCollection =  data.collection;

            init_room_filter();
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

async function get_facilities() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_facilities
        });

        if (data.status == "success")
        {
            capFacility =  data.collection;

            init_capability_filter();
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

async function get_departments() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_departments
        });

        if (data.status == "success")
        {
            departmentCollection =  data.collection;

            init_alocation_reserve_form();
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

async function get_pantry_packages() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_pantry_packages
        });

        if (data.status == "success")
        {
            pantryPackagesCollection =  data.collection;

            init_meeting_category_reserve_form();
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

async function get_pantry_package_detail(pantryPackageId) {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_pantry_details + pantryPackageId
        });

        if (data.status == "success")
        {
            let collection  = data.collection
            let detail = [];

            $.each(collection.detail, function (_, item) { 
                detail.push({
                    id: item.id,
                    pantry_id: item.pantry_id,
                    name: item.name,
                    // note: item.note,
                    note: "",
                    description: item.description,
                    qty: gPartisipantManual.length + gEmployeesSelectedArray.length,
                });
            });

            pantryPackageDetailCollection =  detail;

            genPantryDetail();
            
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
} 


// Booking list filter
function init_datetimepicker_filter() {
    $('.input-group #id_schedule_daterange_search').daterangepicker({
        "showWeekNumbers": true,
        "showISOWeekNumbers": true,
        "opens": "center",
        "drops": "down",

        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        "alwaysShowCalendars": true,
    }, function(start, end, label) {
        // console.log(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'))
        // init(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'))
        // console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
    });
}

function init_employee_filter() {

    $("#id_schedule_employee_search").empty();

    $("#id_schedule_employee_search").append(`<option value="">All Organizer</option>`);

    $.each(employeeCollection, function (_, item) { 
        let opt = document.createElement("option");

        $(opt).text(`${item.name} - ${item.department_name}`);
        // $(opt).val(item.department_id);
        // $(opt).val(item.id);
        $(opt).val(item.name);

        $("#id_schedule_employee_search").append(opt);
        select_enable();
    });
}

function init_building_filter() {
    $("#id_schedule_building_search").empty();

    $("#id_schedule_building_search").append(`<option value="">All Building</option>`);
    
    $.each(buildingCollection, function (_, item) { 
        let opt = document.createElement("option");

        $(opt).text(`${item.name}`);
        $(opt).val(item.id);
        
        $("#id_schedule_building_search").append(opt);
        select_enable();
    });
}

function init_room_filter() {
    $("#id_schedule_room_search").empty();
    
    $("#id_schedule_room_search").append(`<option value="">All Room</option>`);

    $.each(roomCollection, function (_, item) { 
        let opt = document.createElement("option");

        $(opt).text(`${item.name}`);
        $(opt).val(item.radid);
        
        $("#id_schedule_room_search").append(opt);
        select_enable();
    });
}

// Find & reverse a room filter
function init_location_filter() {
    $("#id_book_filter_location").empty();

    $("#id_book_filter_location").append(`<option value="">All Location</option>`);
    
    $.each(buildingCollection, function (_, item) { 
        let opt = document.createElement("option");

        $(opt).text(`${item.name}`);
        $(opt).val(item.id);
        
        $("#id_book_filter_location").append(opt);
        select_enable();
    });
}

function init_date_book_filter(roomType = "room")
{
    if ($('#id_book_filter_when').data('daterangepicker') != undefined) {
        $('#id_book_filter_when').data('daterangepicker').remove(); // Hapus instance
    }
    if ($('#id_book_filter_date_until').data('daterangepicker') != undefined) {
        $('#id_book_filter_date_until').data('daterangepicker').remove(); // Hapus instance
    }

    let opt = {
        singleDatePicker: true,
        showDropdowns: true,
        minDate: moment(),
        drops: "auto",
        locale: {
            // format: 'DD MMM YYYY'
            format: 'YYYY-MM-DD'
        },
        isInvalidDate: function(date) {
            // if (roomType == "noroom" || roomType == "room") {
            //     return date.day() === 0 || date.day() === 6;
            // }
        }
        // minYear: 1901,
        // maxYear: parseInt(moment().format('YYYY'),10),
    };

    let optUntil = {
        singleDatePicker: true,
        showDropdowns: true,
        minDate: moment(),
        drops: "auto",
        locale: {
            // format: 'DD MMM YYYY'
            format: 'YYYY-MM-DD'
        },
    };

    // if (app.auth.level == "2") {
        // if (roomType == "noroom" || roomType == "room") {
        //     opt["minDate"] = moment();
        //     opt["maxDate"] = moment().add(3, 'days');
        // } else if (roomType == "trainingroom") {
        //     optUntil["minDate"] = moment($('#id_book_filter_when').val());
        //     optUntil["maxDate"] = moment($('#id_book_filter_when').val()).add(1, 'year');
        // }
        if (roomType == "trainingroom") {
            optUntil["minDate"] = moment($('#id_book_filter_when').val());
            optUntil["maxDate"] = moment($('#id_book_filter_when').val()).add(1, 'year');
        }
    // }

    $("#id_book_filter_when").daterangepicker(opt, function(start, end, label) {
         // Ensure the format is consistent
        $("#id_book_filter_when").val(start.format('YYYY-MM-DD'));
    });

    $("#id_book_filter_when").on('apply.daterangepicker', function(ev, picker) {
        let startDate = picker.startDate;
        let endDatePicker = $('#id_book_filter_date_until').data('daterangepicker');
        // endDatePicker.minDate = startDate.add(1, 'days'); // Ensure end date is later than start date
        endDatePicker.setStartDate(startDate);
        endDatePicker.setEndDate(startDate);
        endDatePicker.minDate = startDate;

        if (roomType == "trainingroom") {
            let maxEndDate = startDate.clone().add(1, 'year');
            endDatePicker.maxDate = maxEndDate;
        }
        endDatePicker.updateView();

        // Update id_book_filter_date_until to match the new start date
        $(this).val(startDate.format('YYYY-MM-DD'));
        $('#id_book_filter_date_until').val(startDate.format('YYYY-MM-DD'));
        // $('#id_book_filter_date_until').val("");
    });

    $("#id_book_filter_date_until").daterangepicker(optUntil, function(start, end, label) {
        // Ensure the format is consistent
        $("#id_book_filter_date_until").val(start.format('YYYY-MM-DD'));
    });
}

function init_time_book_filter() {
    $('#id_book_filter_from').timepicker(timepickerOptionsTodayFrom);
    $('#id_book_filter_until').timepicker(timepickerOptionsTodayUntil);
}

function init_capability_filter() {
    let htfac = genegrate_cap_facilities("");
    let htseats = genegrate_cap_seats("");

    tippy($("#id_book_filter_capabilities")[0],{
        theme: 'light',
        // hideOnClick: false,
        allowHTML: true,
        trigger: 'click',
        interactive: true,
        animation: 'fade',
        placement: 'auto',
        zIndex: 9999,
        content: `
        <div class="row" style="width: 300px;">
            <div class="col-xs-6">
                <h5>Facilities</h5>
                ${htfac}
            </div>
            <div class="col-xs-6">
                <h5>Seats</h5>
                ${htseats}
            </div>
        </div>`,
        // content: `<span>test</test>`
    });
}

function generateTimes(startTime = moment().startOf('day'), intervalMinutes = 5)
{
    let times = [];
    
    let endTime = moment().endOf('day'); // 23:59
    
    while (startTime.isBefore(endTime)) {
        times.push(startTime.format('HH:mm'));
        startTime.add(intervalMinutes, 'minutes');
    }
    
    return times;
}

function generateOptTime(id, times = [], disableTimes = [], selected = "")
{
    $(id).empty();
    // $(id).append(document.createElement("option"));

    let isSelectedCount = 0;

    $.each(times, function (_, item) { 
        var opt = document.createElement("option");
        var disabled = false;
        var isSelected = false;
        
        if (disableTimes.includes(item))
        {
            disabled = true;
        }

        if (selected != "")
        {
            isSelected = item == selected ? true : false;
        } else {
            if (disabled == false && isSelectedCount == 0) {
                isSelected = true;
                isSelectedCount = 1;
            }
        }
        
        $(opt).text(item);
        $(opt).attr("value", item);
        $(opt).prop("disabled", disabled);
        if (isSelected == true) {
            $(opt).attr("selected", "selected");
        }
        $(id).append(opt);
        $(id).selectpicker("refresh");
    });
}

function init_time_reserve_form(date, from, to, disableTimes = [])
{
    let now = getMinutesForNow(moment());
    // let times = generateTimes(now);
    let times = generateTimes();

    from = from == "" ? getMinutesForNow(moment()).format("HH:mm") : from.trim();
    to = to == "" ? getMinutesForNow(moment()).format("HH:mm") : to.trim();

    // let fromTimes = generateTimes(getMinutesForNow(moment()));
    // generateOptTime("#id_frm_crt_timestart", times, disableTimes);
    // generateOptTime("#id_frm_crt_timestart", times, disableTimes, now.format("HH:mm"));
    generateOptTime("#id_frm_crt_timestart", times, disableTimes, from);
    
    // let toTimes = generateTimes(getMinutesForNow(moment()).add(5, "minutes"));
    // generateOptTime("#id_frm_crt_timeend", times, disableTimes);
    generateOptTime("#id_frm_crt_timeend", times, disableTimes, to);
}

function init_time_reserve_form_old(date, from, to, disableTimes = []) {
    // console.log(disableTimes);
    let optFrom = timepickerOptionsTodayFrom;
    let optUntil = timepickerOptionsTodayUntil;

    // optFrom["defaultTime"] = `00:00:00`;
    // optFrom["startTime"] = getMinutesForNow(moment(`${date} 00:00:00`, 'YYYY-MM-DD HH:mm')).format("HH:mm");
    // optFrom["defaultTime"] = `${from}:00`;
    // optFrom["startTime"] = getMinutesForNow(moment(`${date} ${from}:00`, 'YYYY-MM-DD HH:mm')).format("HH:mm");
    optFrom["scrollbar"] = false;
    optFrom["change"] =  function(time) {
        var dataFrom = getMinutesForNow(moment(time)).toDate();
        $('#id_frm_crt_timeend').timepicker("setTime", dataFrom);
    }
    optFrom["disableTimeRanges"] = [disableTimes];
    
    
    optUntil["scrollbar"] = false;
    optUntil["disableTimeRanges"] = [disableTimes];
    // optUntil["defaultTime"] = `${to}:59`;
    // optUntil["startTime"] = getMinutesForNow(moment(`${date} ${to}:59`, 'YYYY-MM-DD HH:mm')).format("HH:mm");
    // optUntil["defaultTime"] = `23:59:59`;
    // optUntil["startTime"] = getMinutesForNow(moment(`${date} 23:59:59`, 'YYYY-MM-DD HH:mm')).format("HH:mm");

    // $('#id_frm_crt_timestart').timepicker(optFrom);
    $('#id_frm_crt_timeend').timepicker(optUntil);

    let setStart = moment(`${date} ${from}:00`, 'YYYY-MM-DD HH:mm').toDate();
    // $('#id_frm_crt_timestart').timepicker("setTime", setStart);
    
    let setEnd = moment(`${date} ${to}:59`, 'YYYY-MM-DD HH:mm').toDate();
    $('#id_frm_crt_timeend').timepicker("setTime", setEnd);
}

function init_pic_reserve_form() {
    $("#id_frm_crt_pic").empty();

    let employeeByAuth = employeeCollection.filter(emp =>
        emp.id === app.auth.nik || emp.nik === app.auth.nik
    );

    if (app.auth.level == "2" && employeeByAuth.length > 0) {
        $.each(employeeByAuth, function (_, item) { 
            let opt = document.createElement("option");
    
            $(opt).text(`${item.name}`);
            // $(opt).val(item.department_id);
            $(opt).val(item.id);

            $(opt).attr("selected", true);
    
            $("#id_frm_crt_pic").append(opt);
            select_enable();
            $("#id_frm_crt_pic").trigger("change");
        });
    } else {
        $.each(employeeCollection, function (_, item) { 
            let opt = document.createElement("option");
    
            $(opt).text(`${item.name}`);
            // $(opt).val(item.department_id);
            $(opt).val(item.id);
    
            $("#id_frm_crt_pic").append(opt);
            select_enable();
        });
    }
}

function init_alocation_reserve_form() {
    $("#id_frm_crt_alocation_id").empty();

    $.each(departmentCollection, function (_, item) { 
        let opt = document.createElement("option");

        $(opt).text(`${item.name}`);
        // $(opt).val(item.department_id);
        $(opt).val(item.id);

        $("#id_frm_crt_alocation_id").append(opt);
        select_enable();
    });
}

function init_meeting_category_reserve_form() {
    $("#id_frm_crt_meeting_category").empty();

    $.each(pantryPackagesCollection, function (_, item) { 
        let opt = document.createElement("option");

        $(opt).text(`${item.name}`);
        $(opt).val(item.id);

        $("#id_frm_crt_meeting_category").append(opt);
        select_enable();
    });
}

function init_internal_attendees_reverse_form() {
    $("#id_frm_crt_participant").empty();

    $.each(employeeCollection, function (_, item) { 
        let opt = document.createElement("option");

        $(opt).text(`${item.name} - ${item.department_name}`);
        // $(opt).val(item.department_id);
        $(opt).val(item.id);

        $("#id_frm_crt_participant").append(opt);
        select_enable();
    });
}

function genegrate_cap_facilities(value = "") {
    var ht =``;
    var v = value.split("#")
    for(var x in capFacility){
        var ck = '';
        if(v.includes(capFacility[x].id) == true){ck = "checked"}
        ht += `
            <input 
                value="${capFacility[x].id}"
                name="book_filter_cap_facilities[]"
                type="checkbox" 
                id="id_book_filter_cap_fac_${capFacility[x].id}" 
                class="filled-in chk-col-green filter_ck_cap_fac" 
                ${ck} />
            <label for="id_book_filter_cap_fac_${capFacility[x].id}" style="margin-bottom: 6px;">${capFacility[x].name}</label>
            <br>
        `;
    }
    return ht;
}

function genegrate_cap_seats(value = "") {
    var ht =``;
    for(var x in capSeats){
        var ck = '';
        if(value == x){ck = "checked"}
        ht += `
            <input 
                value="${x}" 
                name="book_filter_cap_seat"
                type="radio" 
                id="id_book_filter_cap_seat_${x}" 
                class="radio-col-blue filter_ck_cap_seats" 
                ${ck}/>
            <label for="id_book_filter_cap_seat_${x}">${capSeats[x].text}</label>
            <br>
        `;
    }
    return ht;
}

function select_enable() {
    $('select').selectpicker("refresh");
    $('select').selectpicker("initialize");
}

function getMinutesForNow(nowtime){
    var f = nowtime.format("mm") - 0;
    var f2 = nowtime.format("YYYY-MM-DD HH:")+"00:00";
    var formatth =  moment(f2);
    if(f == 0 ){
        return formatth.add(15, 'minutes');
    }else if(f < 15 ){
        return formatth.add(30, 'minutes');
    }else if(f == 15 ){
        return nowtime.add(15, 'minutes');
    }else if(f > 15 && f < 30 ){
        return formatth.add(30, 'minutes');
    }else if(f > 30 && f < 45){
        return formatth.add(60, 'minutes');
    }else if(f == 30 ){
        return nowtime.add(15, 'minutes');
    }else if(f == 45   ){
        return nowtime.add(15, 'minutes');
    }else if(f > 45 && f <= 59 ){
        return formatth.add(75, 'minutes');
    }else{
        return formatth.add(15, 'minutes');
    }
}

function errorAjax(xhr, ajaxOptions, thrownError) {
    Swal.close();
    $('#id_loader').html('');
    if (ajaxOptions != "abort") {
        if (ajaxOptions == "parsererror") {
            var msg = "Status Code 500, Error Server bad parsing";
            showNotification('alert-danger', msg, 'bottom', 'left')
        } else {
            var msg = "Status Code " + xhr.status + " Please check your connection !!!";
            showNotification('alert-danger', msg, 'bottom', 'left')
        }
    }
}

function renderRoomCard(data) {
    if (data.length > 0) {
        $.each(data, function (_, item) {
            let card = roomCard;

            card = card.replace("%room_id%", item.radid);
            card = card.replace("%room_image%", item.image);
            // card = card.replace("%room_image%", item.image2);
            card = card.replace("%room_name%", item.name);
            card = card.replace("%room_building%", `${item.building_name} / ${item.name}`);
            card = card.replace("%room_capacity%", item.capacity);

            $("#id_area_booking_ad_room").append(card);

            $(`#room-card-${item.radid}`).data("roomCardData", item);
        });
    } else {
        $("#id_area_booking_ad_room").append(`
            <div class="col-12 text-center">
                <h5>No rooms available matching your search criteria.</h5>
            </div>
        `);
    }
}

function onReserveRoom(_this)
{
    $('.page-loader-wrapper').fadeIn();

    setTimeout(function () { 
        let t = $(_this);
    
        let room = t.data("roomCardData");
        // console.log(room);

        let formFilter = objectify_form($("#form-find-reserve").serializeArray());
        // console.log(formFilter);

        let rCategory = formFilter["book_filter_room_category"] == "room" ? "general" : formFilter["book_filter_room_category"];
        let sDate = formFilter["book_filter_date"];
        let eDate = formFilter["book_filter_date_until"];
        if (rCategory == "trainingroom") {
            sDateFormatted = moment(sDate).format("DD MMM YYYY");
            if (sDate == eDate) {
                $("#id_frm_crt_room_date").text(`(${sDateFormatted})`);
            } else {
                eDateFormatted = moment(eDate).format("DD MMM YYYY");
                $("#id_frm_crt_room_date").text(`(${sDateFormatted} - ${eDateFormatted})`);
            }
        } else {
            sDateFormatted = moment(sDate).format("DD MMM YYYY");
            $("#id_frm_crt_room_date").text(`(${sDateFormatted})`);
        }

        reservedTimes = room.reserved_times;

        $("#id_frm_crt_room_name").text(room.name);
        $("#id_frm_crt_booking_type").val(formFilter["book_filter_room_category"] == "room" ? "general" : formFilter["book_filter_room_category"]);
        $("#id_frm_crt_date").val(formFilter["book_filter_date"]);
        $("#id_frm_crt_date_until").val(formFilter["book_filter_date_until"]);
        $("#id_frm_crt_room_id").val(room.radid);

        init_time_reserve_form(
            formFilter["book_filter_date"],
            formFilter["book_filter_time_from"], 
            formFilter["book_filter_time_until"],
            reservedTimes
        );
        init_pic_reserve_form();
        init_alocation_reserve_form();
        
        tabsChangeTo("disabled");
        $("#id_area_find_room").addClass("hidden");
        $("#id_area_booklist").addClass("hidden");
        $("#id_area_reserve_room").removeClass("hidden");
        $('.page-loader-wrapper').fadeOut(); 
    }, 500);
}

function onButtonBack() {
    clearReserveRoom();
    enabledAllForm();

    // clear attendees
    gPartisipantManual = [];
    gEmployeesSelected = [];
    gEmployeesSelectedArray = [];
    gEmployeePIC = "";

    reloadPartisipantManual();
    reloadPartisipant();
    reloadPartisipantSelected();

    // clear pantry detail
    pantryPackageDetailCollection = [];
    genPantryDetail();

    // close book / reverse page
    tabsChangeTo("enabled");
    $("#id_area_reserve_room").addClass("hidden");
    $("#id_area_find_room").removeClass("hidden");
    $("#id_area_booklist").removeClass("hidden");
    $("#id_btn_crt_submit_booking").removeClass("hidden");
    $(".add-attendance-section").removeClass("hidden");

    reservedTimes = [];

    let roomCategory = $(".nav-tabs").find('li.active').find('a').data("roomCategory");

    if (roomCategory != undefined)
    {
        $("#form-find-reserve").trigger("submit");
    }

}

function clearReserveRoom() {
    // $("#id_frm_crt")[0].reset();
    $(':input','#id_frm_crt')
        // .not(':button, :submit, :reset, :hidden')
        .not(':button, :submit, :reset')
        .val('')
        .prop('checked', false)
        .prop('selected', false);

    $("#id_frm_crt_room_name").empty();
}

function tabsChangeTo(status) {
    if (status == "disabled") {
        $(".nav-tabs").find("li").addClass("disabled");
        $(".nav-tabs").find("a[data-toggle=tab]").removeAttr("data-toggle");
    } else {
        $(".nav-tabs").find("li").removeClass("disabled");
        $(".nav-tabs").find("a").attr("data-toggle","tab");
    }
}

function tabToIndex(n) {
    $(`ul.nav-tabs`).find(`li:nth-child(${n})`).find(`a`).trigger(`click`);
}

function runTooltip() {
    $('[data-toggle="popover"]').popover({
        container: 'body'
    }).on('shown.bs.popover', function() {
        $('[data-toggle="tooltip"]').tooltip({
            container: 'body'
        });
    })


    $('[data-toggle="tooltip"]').tooltip({
        container: 'body'
    });
}

function onSubmitReserveRoom(_this)
{
    let t = $(_this);
    $("#id_frm_crt").trigger("submit");
}

function genDepartmentFromPic(itemID) {
    let selectedData = employeeCollection.find(item => item.id == itemID);

    // remove element meeting category kalau dia employee dan tidak ada head_employeenya
    if (selectedData.head_employee_id == null || selectedData.head_employee_id == "") {
        $('#element_meeting_category').hide();
    } else {
        $('#element_meeting_category').show();
    }

    if (selectedData) {
        $("#id_frm_crt_alocation_name").val(selectedData.department_name);
        $("#id_frm_crt_alocation_id").val(selectedData.department_id);
    }
}

function genPantryDetail() {
    $("#tbl_meeting_category").find("tbody").empty();
    if (pantryPackageDetailCollection != null)  {
        let itemHtml;

        $.each(pantryPackageDetailCollection, function (key, item) { 
            itemHtml = `
                <tr id="pkg-detail-row-${item.id}">
                    <td style="word-wrap: break-word">${key+1}</td>
                    <td style="word-wrap: break-word">${item.name}</td>
                    <td style="word-wrap: break-word">${item.description}</td>
                    <td style="word-wrap: break-word">
                        <div class="form-line">
                            <input autocomplete="off" type="text" class="form-control number-without-arrow inp_note_package_detail" value="${item.note}">
                        </div>
                    </td>
                    <td>
                        <div class="form-line">
                            <input autocomplete="off" type="number" class="form-control text-center number-without-arrow inp_qty_package_detail" value="${item.qty}">
                        </div>
                    </td>
                </tr>
            `;
    
            $("#tbl_meeting_category").find("tbody").append(itemHtml);
            $(`#pkg-detail-row-${item.id}`).data("pkgDetailId", item.id);
        });
    }
}

function reGenQtyPatryDetail() {
    let ttlParticipant = gPartisipantManual.length + gEmployeesSelectedArray.length;

    $.each(pantryPackageDetailCollection, function (key, item) { 
        pantryPackageDetailCollection[key]["qty"] = ttlParticipant;
    });

    genPantryDetail();
}

// external attendess function
function clickPartisipantManualOpen(tp = "", dd = 0) {
    if (tp == "update") {
        var name = gPartisipantManual[dd]['name'];
        var email = gPartisipantManual[dd]['email'];
        var company = gPartisipantManual[dd]['company'];
        var position = gPartisipantManual[dd]['position'];
    } else {
        var name = "";
        var email = "";
        var company = "";
        var position = "";
    }
    var html = '<form id="id_frm_crt_manual" >\
                        <label for="id_frm_crt_manual_name">NAME</label>\
                        <div class="form-group">\
                            <div class="form-line">\
                                <input autocomplete="off" value="' + name + '" required type="text" id="id_frm_crt_manual_name" class="form-control" placeholder="Enter the name">\
                            </div>\
                        </div>\
                        <label for="id_frm_crt_manual_email">EMAIL</label>\
                        <div class="form-group">\
                            <div class="form-line">\
                                <input autocomplete="off"  value="' + email + '" type="email" id="id_frm_crt_manual_email" class="form-control" placeholder="Enter the email address">\
                            </div>\
                        </div>\
                        <label for="id_frm_crt_manual_org">COMPANY/ORGANIZATION</label>\
                        <div class="form-group">\
                            <div class="form-line">\
                                <input autocomplete="off" value="' + company + '"  required type="text" id="id_frm_crt_manual_company" class="form-control" placeholder="Enter the company/organization">\
                            </div>\
                        </div>\
                        <label for="id_frm_crt_manual_position">POSITION</label>\
                        <div class="form-group">\
                            <div class="form-line">\
                                <input autocomplete="off" value="' + position + '"  type="text" id="id_frm_crt_manual_position" class="form-control" placeholder="Enter the position">\
                            </div>\
                        </div>\
                        <br>\
                        <button style="display:none;" id="id_btn_crt_manual" class="btn btn-primary m-t-15 waves-effect">LOGIN</button>\
                    </form>';
    Swal.fire({
        title: 'Add external attendees',
        html: html,
        focusConfirm: false,
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        reverseButtons: true,
        confirmButtonText: tp == "update" ? "Update !" : 'Add !',
        confirmButtonAriaLabel: 'manually!',
        preConfirm: function(r) {
            if ($('#id_frm_crt_manual').valid()) {
                return true;
            } else {
                return false;
            }
            // console.log($('#id_frm_crt_manual').valid())  
        }
    }).then((result) => {
        if (result.value) {
            var data = {
                name: $('#id_frm_crt_manual_name').val(),
                email: $('#id_frm_crt_manual_email').val(),
                company: $('#id_frm_crt_manual_company').val(),
                position: $('#id_frm_crt_manual_position').val(),
            }
            if (tp == "update") {
                gPartisipantManual[dd] = data;
            } else {

                gPartisipantManual.push(data);
            }
            reloadPartisipantManual();

        }
        return false;
    });
}

function reloadPartisipantManual(withDelete = true) {
    var htmlEmp = '';
    $.each(gPartisipantManual, (index, item) => {
        var s = "update";
        htmlEmp += '<tr  id="id_tr_idmanual_' + index + '" onclick="clickPartisipantManualOpen(&quot;' + s + '&quot;,' + index + ')" style="cursor:pointer;">';
        htmlEmp += '<td style="width: 90%;">' + item.name + '</td>';
        if (withDelete == true)
        {
            htmlEmp += '<td>\
                <button data-item="' + index + '" onclick="removeCrtParticipantManual($(this))" type="button" class="btn bg-red btn-sm waves-effect">\
                    <i class="material-icons" >delete</i> \
                </button>\
            </td>';
        }
        htmlEmp += '</tr>';
    });
    $('#id_list_tbl_participant_manual tbody').html(htmlEmp);
    reGenQtyPatryDetail();
}

function removeCrtParticipantManual(t) {
    var item = t.data('item');
    var idtr = '#id_tr_idmanual_' + item;
    var removeN = item;
    gPartisipantManual.splice(removeN, 1);
    $(idtr).remove().delay(200);
    reloadPartisipantManual();
}

function clickPartisipantAdd(t) {
    // var value =t.val();
    var value = $('#id_frm_crt_participant').val()
    if (value != null) {
        $.each(value, (index, item) => {
            if (item != "") {
                gEmployeesSelected.push(item);
                for (var x in employeeCollection) {
                    if (employeeCollection[x].nik == item) {
                        gEmployeesSelectedArray.push(employeeCollection[x]);
                        break;
                    }
                }
            }
    
        })
        reloadPartisipant();
        reloadPartisipantSelected();
    }
}

function reloadPartisipant() {
    // var htmlEmp = '<option value=""></option>';
    var htmlEmp = '';
    $.each(employeeCollection, (index, item) => {
        if (gEmployeesSelected.indexOf(item.nik) < 0) {
            if (gEmployeePIC != item.nik) {
                htmlEmp += '<option value="' + item.nik + '">' + item.name + ' - ' + item.department_name + '</option>';
            }
        }
    });
    $('#id_frm_crt_participant').html(htmlEmp);
    reGenQtyPatryDetail();
    select_enable();
}

function reloadPartisipantSelected(withDelete = true) {
    var htmlEmp = '';
    $.each(gEmployeesSelected, (index, item) => {
        htmlEmp += '<tr id="id_tr_id_' + item + '" data-id="' + item + '">';
        htmlEmp += '<td style="width: 90%;">' + gEmployeesSelectedArray[index].name + '</td>';
        if (withDelete == true && (gEmployeesSelectedArray[index].nik != lastSelectedHost || gEmployeesSelectedArray[index].id != lastSelectedHost))
        {
            htmlEmp += '<td>\
                <button data-item="' + item + '" onclick="removeCrtParticipant($(this))" type="button" class="btn bg-red btn-sm waves-effect">\
                    <i class="material-icons" >delete</i> \
                </button>\
            </td>';
        }
        htmlEmp += '</tr>';
    });
    $('#id_list_tbl_participant tbody').html(htmlEmp);
    reGenQtyPatryDetail();
}

function removeCrtParticipant(t) {
    var item = t.data('item');
    var idtr = '#id_tr_id_' + item;
    var removeN = gEmployeesSelected.indexOf(item);
    // Hapus dari array string
    gEmployeesSelected = gEmployeesSelected.filter(id => id !== item.toString());
    // Hapus dari array object berdasarkan `nik` atau `id`
    gEmployeesSelectedArray = gEmployeesSelectedArray.filter(emp => emp.nik !== item.toString() || emp.id !== item.toString());
    reloadPartisipant();
    reloadPartisipantSelected();
    // gEmployeesSelected.splice(removeN, 1);
    // gEmployeesSelectedArray.splice(removeN, 1);
    // $(idtr).remove().delay(200);
}

// table booking list
function init_table_booking_list() {
    tbldata = $("#tbldata").DataTable({
        searching: false,
        bLengthChange: false,
        bInfo: true,
        ordering: true,
        columns: [
            {data:"no", name:"no", searchable:false, orderable:false},
            {data:"status", name:"status", searchable:false, orderable:false, render: function(_, _, item) {
                let bookingTz = item.timezone == "SE Asia Standard Time" ? "Asia/Jakarta" : item.timezone;
                let status = "";
                // let momentEnd = changeTimeIntoTimezone(item.server_end, bookingTz);
                // let momentStart = changeTimeIntoTimezone(item.server_start, bookingTz);
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

                return status;
            }},
            {data:"title", name:"title", searchable:false, orderable:true},
            {data:"room_name", name:"room_name", searchable:false, orderable:true},
            {data:"booking_date", name:"booking_date", searchable:false, orderable:true},
            {data:"time", name:"time", searchable:false, orderable:false},
            {data:"attendees", name:"attendees", searchable:false, orderable:false, render: function(_, _, item) {
                return `
                    <a 
                        data-toggle="tooltip" 
                        data-placement="top" 
                        title="Attendees" 
                        data-html="true"
                        onclick="openPartispant($(this))" 
                        data-booking_id="${item.booking_id}" 
                        data-title="${item.title}" 
                        style="cursor:pointer;font-size:16px;font-weight:bold;" >
                        ${item.attendees} participants
                    </a>
                `;
                // return `${item.attendees} participants`;
            }},
            {data:"pic", name:"pic", searchable:false, orderable:false, render: function(_, _, item) {
                return `
                    <a 
                        data-toggle="tooltip" 
                        data-placement="top" 
                        title="Organizer" 
                        data-html="true"
                        onclick="clickPIC($(this))" 
                        data-id="${item.booking_id}" 
                        style="cursor:pointer;font-size:16px;font-weight:bold;" >
                        ${item.pic}
                    </a>
                `;
            }},
            {data:"action", name:"action", searchable:false, orderable:false, render: function(_, _, item) {
                let bookingTz = item.timezone == "SE Asia Standard Time" ? "Asia/Jakarta" : item.timezone;
                // let momentEnd = changeTimeIntoTimezone(item.server_end, bookingTz);
                // let momentStart = changeTimeIntoTimezone(item.server_start, bookingTz);
                let momentEnd = moment(item.server_end);
                let momentStart = moment(item.server_start);
                let extendTime = item.extended_duration -0;
                let diffMomentEnd = momentEnd.add(extendTime, 'minutes');

                // let html = `
                //     <a onclick="detailData($(this))" >
                //         <i 
                //         class="material-icons col-blue" 
                //         style="vertical-align: middle; cursor:pointer;" 
                //         data-original-title="" 
                //         title="">visibility</i>
                //     </a>
                // `;
                let html = ``;
                let bookExpr = false;
                
                if ( moment().diff(diffMomentEnd) >= 0 || item.is_expired == 1 ) {
                    bookExpr = true;
                }

                let bookRun = false;

                if (item.end_early_meeting == 1 || (
                    moment().unix() <= momentEnd.unix() &&
                    moment().unix() >= momentStart.unix()
                )) {
                    bookRun = true;
                }

                let authUserAttends = item.attendees_list.internal_attendess.filter(item => item.nik == app.auth.nik);
                let authUserAttend = authUserAttends.length > 0 ? authUserAttends[0] : null;
                
                if(
                    bookExpr == false 
                    && item.is_canceled == 0
                ) {
                    if (bookRun) {
                        if (item.end_early_meeting == 0 && item.is_approve == 1) {
                            /* 
                                html += '<a \
                                onclick="extendMeeting($(this))" \
                                data-id="' + item.id + '" \
                                data-booking_id="' + item.booking_id + '" \
                                data-name="' + item.title + '" \
                                title="Extend Meeting" \
                                ><i class="material-icons col-cyan" style="vertical-align: middle; cursor:pointer;" data-original-title="" title="">open_with</i></a>';
                            */
                            if (
                                app.auth.level != "2" 
                                || app.auth.nik == item.created_by
                                || ( app.auth.level == "2" &&  app.auth.nik == item.pic_nik )
                            ) {
                                html += '<button \
                                    onclick="extendMeeting($(this))" \
                                    data-id="' + item.id + '" \
                                    data-booking_id="' + item.booking_id + '" \
                                    data-name="' + item.title + '" \
                                    type="button" class="btn btn-info waves-effect ">Extend Meeting</button>';
                                html += '<button \
                                    onclick="endMeeting($(this))" \
                                    data-id="' + item.id + '" \
                                    data-booking_id="' + item.booking_id + '" \
                                    data-name="' + item.title + '" \
                                    type="button" class="btn btn-danger waves-effect ">End Meeting</button>';
                            }

                            if (authUserAttends.length > 0 && (authUserAttend != null && authUserAttend.attendance_status == "0")) {
                                html += '<button \
                                    onclick="openAttendStatus($(this))" \
                                    data-id="' + item.id + '" \
                                    data-booking_id="' + item.booking_id + '" \
                                    data-name="' + item.title + '" \
                                    type="button" class="btn btn-success waves-effect ">Attend / Decline</button>';
                            }
                        }
                    } else if (
                        app.auth.level != "2" 
                        || app.auth.nik == item.created_by
                        || ( app.auth.level == "2" &&  app.auth.nik == item.pic_nik )
                    ) {
                            if (item.is_approve == 1) {
                                html += `
                                <a   
                                data-toggle="tooltip" 
                                data-placement="top" 
                                title="Edit/Reschedule" 
                                data-html="true"
                                onclick="addInvitation($(this))" >
                                    <i 
                                    class="material-icons col-cyan" 
                                    style="vertical-align: middle; cursor:pointer;" 
                                    data-original-title="" 
                                    title="">group</i>
                                </a>
                                `;

                                html += `
                                <a  
                    
                                data-id="${item.id}" 
                                data-booking_id="${item.booking_id}" 
                                data-name="${item.title}" 
                                data-date="${item.date}" 
                                data-start="${item.start}" 
                                data-end="${item.end}" 
                                data-room_name="${item.room_name}" 
                                data-room_id="${item.room_id}" 
                                data-toggle="tooltip" 
                                data-placement="top" 
                                title="Edit/Reschedule" 
                                data-html="true"
                                onclick="rescheduleMeeting($(this))" ><i 
                                    class="material-icons col-cyan" 
                                    style="vertical-align: middle; cursor:pointer;" 
                                    data-original-title="" 
                                    title="">edit</i></a>
                                `;
                            }

                            let isShowCancel = true;
                            if (item.booking_type == "trainingroom" && !isHPlus(item.date, 14)) {
                                isShowCancel = false;
                            }
                            if (isShowCancel == true) {
                                let cancelMeetingFunc = item.booking_type != "trainingroom" 
                                ? "cancelMeeting($(this))"
                                : "cancelMeetingTraining($(this))";
                                html += `
                                <a  
                                data-id="${item.id}" 
                                data-booking_id="${item.booking_id}" 
                                data-name="${item.title}" 
                                data-toggle="tooltip" 
                                data-placement="top" 
                                title="Remove/Cancel Meeting" 
                                data-html="true"
                                onclick="${cancelMeetingFunc}" ><i 
                                    class="material-icons col-red" 
                                    style="vertical-align: middle ;cursor:pointer;" 
                                    data-original-title="" 
                                    title="">close</i></a>
                                `;
                            }
                    }
                }
                return html;
            }},
        ],
        "order": [[ 0, 'desc' ]],
        ajax: {
            url: bs + ajax.url.get_bookings,
            contentType: 'application/json',
            beforeSend: function() {
                if (typeof tbldata != "undefined" && tbldata.hasOwnProperty('settings') && tbldata.settings()[0].jqXHR != null) {
                    tbldata.settings()[0].jqXHR.abort();
                }
            },
            data: function (param) {
                let orders = param.order;
                let columns = param.columns;
                delete param.order;
                delete param.columns;
                param.booking_date = $("#id_schedule_daterange_search").val();
                param.booking_organizer = $("#id_schedule_employee_search").val();
                param.booking_building = $("#id_schedule_building_search").val();
                param.booking_room = $("#id_schedule_room_search").val();
                

                param.sort_column = columns[orders[0]["column"]]["name"];
                param.sort_dir = orders[0]["dir"];
                
                // let sort = [];
                // $.each(orders, function (key, val) { 
                //     sort.push({
                //         "column": columns[val.column]["name"],
                //         "dir": val.dir
                //     });
                // });
                // console.log(sort);
                // param.sort = sort;
            },
            dataSrc: function (json) {  
                json.draw =  json.collection.draw;
                json.recordsFiltered =  json.collection.recordsFiltered;
                json.recordsTotal =  json.collection.recordsTotal;
                return json.collection.data;
            } 
        },
        processing: true,
        serverSide: true,
        drawCallback: function (settings) {
            // console.log("--------drawCallback--------");
            // console.log(settings);
        },
        createdRow: function( row, data, dataIndex ) {
            // console.log("--------createdRow--------");
            // console.log(row);
            // console.log(data);
            // console.log(dataIndex);
            $(row).attr('id', `booking-row-${data.booking_id}`);
            $(row).data("bookingData", data);
            $(row).find('td:eq(8)')
                .addClass("dtfc-fixed-right")
                .css({"position": "sticky", "right": "0px"});
        },
    });
}

function reloadTable() {
    if (typeof tbldata != "undefined") {
        tbldata.ajax.reload();
    }
};

function detailData(_this)
{
    // init_time_reserve_form();
    init_pic_reserve_form();
    init_alocation_reserve_form();

    let row = _this.parents("tr");
    let item = row.data("bookingData");
    // console.log(item);

    $("#id_frm_crt_room_name").text(item.room_name);
    $("#id_frm_crt_booking_type").val(item.booking_type);
    $("#id_frm_crt_date").val(item.date);
    $("#id_frm_crt_room_id").val(item.room_id);


    $("#id_frm_crt_title").val(item.title);
    
    let start = moment(item.start).format("HH:mm");
    generateOptTime("#id_frm_crt_timestart", [start], [], start);
    let end = moment(item.end).format("HH:mm");
    generateOptTime("#id_frm_crt_timeend", [end], [], end);

    $(`#id_frm_crt_pic option:contains('${item.pic}')`).prop("selected", true).trigger("change");

    $("#id_frm_crt_alocation_name").val(item.alocation_name);
    $("#id_frm_crt_note").val(item.note);
    $("#id_frm_crt_external_link").val(item.external_link);
    if (item.is_private == 1) {
        $("#id_frm_crt_is_private").prop("checked", true);
    }

    if  (item.attendees_list.internal_attendess.length > 0)
    {
        gEmployeesSelectedArray = item.attendees_list.internal_attendess;
        gEmployeesSelected = gEmployeesSelectedArray.map(item => item.nik);
        reloadPartisipantSelected(false);
    }

    if  (item.attendees_list.external_attendess.length > 0)
    {
        gPartisipantManual = item.attendees_list.external_attendess;
        reloadPartisipantManual(false);
    }

    if (item.package_menus.length > 0)
    {
        $("#id_frm_crt_meeting_category").val(item.package_id).trigger("change");
        
        pantryPackageDetailCollection = item.package_menus;
        genPantryDetail();
    }



    
    tabsChangeTo("disabled");
    disableAllForm();
    $("#id_btn_crt_submit_booking").addClass("hidden");
    $(".add-attendance-section").addClass("hidden");
    $("#id_area_find_room").addClass("hidden");
    $("#id_area_booklist").addClass("hidden");
    $("#id_area_reserve_room").removeClass("hidden");
    
}

function disableAllForm()
{
    $(':input','#id_frm_crt').prop("disabled", true);
}

function enabledAllForm()
{
    $(':input','#id_frm_crt').prop("disabled", false);
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

function rescheduleMeeting(t) {
    var date = t.data('date');
    var booking_id = t.data('booking_id');
    var room_name = t.data('room_name');
    var room_id = t.data('room_id');
    var start = t.data('start');
    var end = t.data('end');
    var name = t.data('name');
    $('#id_frm_res_timezone').val(moment.tz.guess(true));
    $('#id_frm_res_booking_id').val(booking_id);
    $('#id_frm_res_start_input').val(start)
    $('#id_frm_res_end_input').val(end)
    // var display_button_start = moment(start).format("hh:mm A");
    // var display_button_end = moment(end).format("hh:mm A");
    var display_button_start = moment(start).format("HH:mm");
    var display_button_end = moment(end).format("HH:mm");
    var startDate = new Date(date)
    $('#id_frm_res_date').val(date);
    $('#id_frm_res_date_dummy').val(moment(startDate).format(' D MMMM YYYY'));
    $('#id_res_text_name').html(name);
    $('#id_frm_res_start').html(display_button_start);
    $('#id_frm_res_finish').html(display_button_end);
    activeResDatepicker(booking_id, room_id)
    checkBookingDateRes(booking_id, date, room_id, "");
    $('#id_mdl_reschedule').modal('show');
}

function activeResDatepicker(booking_id, room_id) {
    var endDate = moment().add(365, 'days');
    var startDate = moment().add(0, 'days');
    $('#id_frm_res_date_dummy').datepicker({
        autoclose: true,
        todayHighlight: true,
        toggleActive: true,
        startDate: new Date(startDate),
        endDate: new Date(endDate),
        format: "dd MM yyyy",
    }).on('changeDate', function(e) {
        var tm = moment(e.date).format('YYYY-MM-DD');
        var dates = moment(e.date).format(' D MMMM YYYY');
        var date = tm;
        $('#id_frm_res_date_dummy').val(dates);
        $('#id_frm_res_date').val(tm);
        checkBookingDateRes(booking_id, date, room_id, "changed");
    })
}

function checkBookingDateRes(bookid = "", date = "", radid = "", status = "") {
    var bs = $('#id_baseurl').val();
    // var url = bs + "booking/check/res-date/booking/" + bookid + "/" + date + "/" + radid;
    var url = bs + ajax.url.get_check_reschedule_date + "/" + bookid + "/" + date + "/" + radid;
    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        beforeSend: function() {
            $('#id_loader').html('<div class="linePreloader"></div>');
            Swal.fire({
                title: 'Please Wait !',
                html: 'Process saving',
                allowOutsideClick: false,
                onBeforeOpen: () => {
                    Swal.showLoading()
                },
            });
        },
        success: function(data) {
            $('#id_loader').html('');
            Swal.close();
            if (data.status == "success") {
                var col = data.collection;
                var html = "";
                gTimeSelectBookingRes = col;
                var datatime = reStructureSchedule(col);
                if (col.length <= 0) {
                    return;
                }
                var item = col[0];
                var today_name = moment().format("dddd");
                var room_work_day = item.work_day;
                today_name = today_name.toUpperCase();
                var ifDayExist = room_work_day.indexOf("today_name");
                var image_room = "";
                var d1 = moment().format('YYYY-MM-DD'); //today
                var d2 = moment(date).format('YYYY-MM-DD'); //pick date
                var pick = date;
                if (d1 == d2) {
                    pick = "today";
                }
                var check_available = checkNowTimeRoomRes(datatime, item.work_start, item.work_end, pick)
                if (check_available == null) {
                    return true;
                }
                if (status == "") { // no change date
                    // var ssSTART = $('#id_frm_res_start_input').val()
                    // var ssEND = $('#id_frm_res_end_input').val()
                    // var display_button_start = moment(ssSTART).format("hh:mm A");
                    // var display_button_end = moment(ssEND).format("hh:mm A");
                    // var display_button_start = moment(ssSTART).format("HH:mm");
                    // var display_button_end = moment(ssEND).format("HH:mm");
                    // $('#id_frm_res_start').html(display_button_start);
                    // $('#id_frm_res_finish').html(display_button_end);
                } else {
                    var time_available_start = datatime[check_available]['time_array'];
                    var time_available_end = datatime[check_available + 1]['time_array'];
                    var display_button_start = moment(date + " " + time_available_start).format("hh:mm A");
                    var display_button_end = moment(date + " " + time_available_end).format("hh:mm A");
                    // $('#id_frm_res_start_input').val(date + " "+time_available_start);
                    // $('#id_frm_res_end_input').val(date + " "+time_available_end);
                    // $('#id_frm_res_start').html(display_button_start);
                    // $('#id_frm_res_finish').html(display_button_end);
                    $('#id_frm_res_start_input').val("");
                    $('#id_frm_res_end_input').val("");
                    $('#id_frm_res_start').html(" --:-- ");
                    $('#id_frm_res_finish').html(" --:-- ");
                }
            } else {
                var msg = "Your session is expired, login again !!!";
                showNotification('alert-danger', msg, 'top', 'center')
            }
        },
        error: errorAjax
    })
}

function reStructureSchedule(colll) {
    if (colll.length <= 0) {
        return null;
    }
    var getDatatime = colll[0]['datatime'];
    for (var x in getDatatime) {
        var numIndex = x;
        for (var rr in colll) {
            var tmp_data = colll[rr]['datatime'][numIndex];
            // console.log(tmp_data);
            if (tmp_data.book == 1 || tmp_data.book == "1") {
                getDatatime[numIndex]['book'] = "1";
            }
        }

    }
    return getDatatime;
}

function openAlertPilihRes(t) {
    var ele = t.data('id');
    var roomNum = 0; // first
    var timeType = t.data('type');
    var selectRoom = gTimeSelectBookingRes[0]; // first
    var timeArray = selectRoom['datatime'];
    var start = selectRoom['work_start'];
    var end = selectRoom['work_end'];
    // var text = gBookingCrt['date'] == "today" ? "" : gBookingCrt['date'];
    var html = '';
    var getdate = $('#id_frm_res_date').val();
    html += '<table class="table table-hover select" >';
    html += '<tbody>';
    var bookqueue = false;
    $.each(timeArray, (index, item) => {
        var tnow = moment().format('hh:mm') + ":00";
        var checkData = checkThatTimeRoomRes(timeArray, index, item, start, end, getdate);
        var date = moment().format('YYYY-MM-DD');
        // var display_text = moment(date + " " + item.time_array).format("hh:mm A");
        var display_text = moment(date + " " + item.time_array).format("HH:mm");
        if (checkData) {
            if (item.book > 0) {
                bookqueue = true;
            }
            if (timeType == "end") {

                if (bookqueue == true) {
                    html += '<tr class="disabled" ><td>' + display_text + '</td></tr>';
                } else {
                    var strStart = $('#id_frm_res_start_input').val()
                    var timestart = moment(strStart).format('hh:mm') + ":00";
                    var unixTimeStart = moment(date + " " + timestart).format('X');
                    var unixTimeEnd = moment(date + " " + item.time_array).format('X');
                    // console.log(timestart, item.time_array)
                    if (unixTimeStart > unixTimeEnd || unixTimeStart == unixTimeEnd) {
                        html += '<tr class="disabled" ><td>' + display_text + '</td></tr>';
                    } else {
                        html += '<tr data-timeType="' + timeType + '" data-ele="' + ele + '" \
                        class="" data-timenum="' + index + '"  \
                        data-value="' + item.time_array + '"   \
                        data-text="' + display_text + '" data-roomnum="' + roomNum + '" \
                        onclick="setTimeCrtBookingInsideRes($(this))" \
                        style="cursor:pointer;" ><td>' + display_text + '</td></tr>';
                    }
                }
            } else {
                // start
                html += '<tr data-timeType="' + timeType + '" data-ele="' + ele + '" \
                    class="" data-timenum="' + index + '"  \
                    data-value="' + item.time_array + '"   \
                    data-text="' + display_text + '" data-roomnum="' + roomNum + '" \
                    onclick="setTimeCrtBookingInsideRes($(this))" \
                    style="cursor:pointer;" ><td>' + display_text + '</td></tr>';
            }

        } else {
            html += '<tr class="disabled" ><td>' + display_text + '</td></tr>';
        }
    });
    html += '</tbody>';
    html += '</table>';
    Swal.fire({
        title: 'Choose Time',
        icon: 'info',
        html: html,
        showCloseButton: true,
        focusConfirm: false,
        confirmButtonText: 'Close!',
        confirmButtonAriaLabel: 'Close!',
    });
}

function checkThatTimeRoomRes(timerange, index, thattime, start, end, pick = "") {
    // console.log(pick);
    var datenow = moment().format('YYYY-MM-DD');
    var date = moment().format('YYYY-MM-DD');
    var now = moment().format('HH:mm');
    var unixStart = moment(pick + " " + start).format('X');
    var unixEnd = moment(pick + " " + end).format('X');
    var timeactive = false;
    var unixNow = moment(pick + " " + now).format('X');

    var completeTime = pick + " " + thattime.time_array;
    var unixTime = moment(completeTime).format('X');
    // console.log(pick,datenow)
    var indexGet = index + 1;
    if (indexGet >= timerange.length) {

        return timeactive;
    }
    if (pick == datenow) {
        // today
        // console.log(unixStart , unixTime, unixEnd);
        if (timerange[index]['book'] == 0) {
            if (unixStart <= unixTime && unixEnd >= unixTime) {
                // cek bahwa waktu diantara jam buka dan tutup
                if (unixTime > unixNow) {
                    // cek bahwa waktu ini lewat
                    if (timerange[index]['book'] == 0) {
                        timeactive = true;
                    } else {

                    }
                }
            }
        }
    } else {
        // other day
        if (timerange[index]['book'] == 0) {
            if (unixStart <= unixTime && unixEnd >= unixTime) {
                timeactive = true;

            }
        }
    }

    return timeactive;
}

function checkNowTimeRoomRes(timerange, start, end, pick = "") {
    var date = moment().format('YYYY-MM-DD');
    var now = moment().format('HH:mm');
    var unixStart = moment(date + " " + start).format('X');
    var unixEnd = moment(date + " " + end).format('X');
    var timeactive = null;
    var unixNow = moment(date + " " + now).format('X');
    if (timerange != null) {
        $.each(timerange, (index, item) => {
            var completeTime = date + " " + item.time_array;
            var unixTime = moment(completeTime).format('X');
            if (item.book == 0) {
                // cek waktu belum terbooking
                if (unixStart <= unixTime && unixEnd > unixTime) {
                    // cek bahwa waktu diantara jam buka dan tutup
                    if (unixTime > unixNow || pick != "today") {
                        // cek bahwa waktu ini lewat
                        if (timerange[index + 1]['book'] == 0) {
                            // 30 or 60 MIN
                            timeactive = index;
                            return false;
                        } else {
    
                        }
                    }
                }
            }
        });
    }
    return timeactive;
}

function setTimeCrtBookingInsideRes(t) {
    var ele = t.data('ele');
    var roomnum = t.data('roomnum');
    var text = t.data('text');
    var value = t.data('value');
    var timenum = t.data('timenum');
    var timeType = t.data('timetype');
    var ddd = moment().format("YYYY-MM-DD");
    var valueOri = "";
    var html = "";
    var selectRoom = gTimeSelectBookingRes[0]; // first
    var timeArray = selectRoom['datatime'];
    var dddd = [];
    if (timeType == "end") {
        var str = $('#id_frm_res_start_input').val()
        var stsp = str.split(" ");
        var stdate = stsp[0];
        var dtt = $('#id_frm_res_date').val();
        // $('#id_frm_res_end_input').val(dtt + " " +value);
        var endtime = timeArray[timenum];
        var sttmoment = moment(str).format("YYYY-MM-DD HH:mm");
        var endmoment = moment(stdate + " " + endtime.time_array).format("YYYY-MM-DD HH:mm");
        var nnn = 0;
        $.each(timeArray, (index, item) => {
            var ttmm = moment(stdate + " " + item.time_array).format("YYYY-MM-DD HH:mm");
            if (ttmm > sttmoment && ttmm < endmoment) {
                if (item.book == "1" || item.book == 1) {
                    nnn++;
                    dddd.push(item)
                }
            }
        })
        if (nnn > 1) {
            Swal.fire('Attention !!!', 'Time is used up, change your time!', 'warning')
        } else {
            if (sttmoment == endmoment) {
                Swal.fire('Attention !!!', 'Finish/End time must more than start time', 'warning')
            } else {
                // var elemantHtml = moment(ddd + " " + value).format("hh:mm A");
                var elemantHtml = moment(ddd + " " + value).format("HH:mm");
                $('#' + ele).html(elemantHtml);
                $('#id_frm_res_end_input').val(dtt + " " + value);
                Swal.close();
                // Swal.fire('Attention !!!','Time is used up, change your time!','warning')
            }

        }

    } else {
        var dtt = $('#id_frm_res_date').val();
        $('#id_frm_res_start_input').val(dtt + " " + value)
        // var elemantHtml = moment(ddd + " " + value).format("hh:mm A");
        var elemantHtml = moment(ddd + " " + value).format("HH:mm");
        $('#' + ele).html(elemantHtml);

        $('#id_frm_res_finish').html(" --:-- ");
        $('#id_frm_res_end_input').val("");
        Swal.close();
    }

}

function clickSubmit(ele) {
    $('#' + ele).click();
}

function cancelMeeting(t) {
    var id = t.data('id');
    var booking_id = t.data('booking_id');
    var name = t.data('name');
    var form = new FormData();
    form.append('id', id);
    form.append('booking_id', booking_id);
    form.append('name', name);
    Swal.fire({
        title: 'Are you sure want cancel ' + name + ' of meeting?',
        text: "You will cancel the data booking " + name + " !",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Cancel Meeting !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                title: 'Reason for Cancellation',
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
                        return Swal.showValidationMessage(`Reason for Cancellation is required`);
                    }
                }
            }).then((result) => {
                if (result.value !== undefined) {
                    var reason = result.value;
                    form.append('reason', reason);
                    var bs = $('#id_baseurl').val();
                    $.ajax({
                        // url: bs + "booking/post/cancelbook",
                        url: bs + ajax.url.post_cancel_booking,
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
                                showNotification('alert-success', "Succes cancel " + name, 'top', 'center')
                                // init();
                                reloadTable();
                            } else {
                                showNotification('alert-danger', "Cancel " + name + " meeting is failed!!!", 'bottom', 'left')
                            }
                        },
                        error: errorAjax,
                    });
                }
            });
        } 
    });
}

function cancelMeetingTraining(t) {
    var id = t.data('id');
    var booking_id = t.data('booking_id');
    var name = t.data('name');
    var form = new FormData();
    form.append('id', id);
    form.append('booking_id', booking_id);
    form.append('name', name);
    Swal.fire({
        title: 'Are you sure want cancel ' + name + ' of meeting?',
        text: "You will cancel the data booking " + name + " !",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Cancel Meeting !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                // title: 'Are you sure want cancel ' + name + ' of meeting?',
                // text: "You will cancel the data booking " + name + " !",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Just This Meeting !',
                cancelButtonText: 'Cancel All Meeting !!',
                reverseButtons: true
            }).then((result) => {
                if (result.value == undefined) {
                    form.append('is_all', true);
                } else {
                    form.append('is_all', false);
                }

                Swal.fire({
                    title: 'Reason for Cancellation',
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
                            return Swal.showValidationMessage(`Reason for Cancellation is required`);
                        }
                    }
                }).then((result) => {
                    if (result.value !== undefined) {
                        var reason = result.value;
                        form.append('reason', reason);
                        var bs = $('#id_baseurl').val();
                        $.ajax({
                            // url: bs + "booking/post/cancelbook",
                            url: bs + ajax.url.post_cancel_all_booking,
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
                                    showNotification('alert-success', "Succes cancel " + name, 'top', 'center')
                                    // init();
                                    reloadTable();
                                } else {
                                    showNotification('alert-danger', "Cancel " + name + " meeting is failed!!!", 'bottom', 'left')
                                }
                            },
                            error: errorAjax,
                        });
                    }
                });
            });
        }
    });
}

function extendMeeting(t) {
    var date = t.data('date');
    var booking_id = t.data('booking_id');
    var room_name = t.data('room_name');
    var room_id = t.data('room_id');
    var start = t.data('start');
    var end = t.data('end');
    var name = t.data('name');
    var bs = $('#id_baseurl').val();
    $.ajax({
        // url: bs + "booking/get/extend-meeting",
        url: bs + ajax.url.get_check_extendmeeting_time,
        type: "GET",
        data: {
            booking_id: booking_id,
            time: moment().format("HH:mm:ss"),
            date: moment().format('YYYY-MM-DD'),
        },
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
                var dt = data.collection;
                var html = "";
                html += '<table class="table table-hover select" >';
                html += '<tbody>';
                $.each(dt, (index, item) => {
                    if (item.book == "0" || item.book == 0) {
                        html += '<tr data-name="' + name + '" data-booking_id="' + booking_id + '" data-index="' + index + '"  data-duration="' + item.duration + '"   onclick="setExtendTimeBooking($(this))" style="cursor:pointer;" ><td>' + item.duration + ' mins</td></tr>';
                    }
                });
                html += '</tbody>';
                html += '</table>';
                Swal.fire({
                    title: 'Extend Time of ' + name,
                    // icon: 'info',
                    html: html,
                    showCloseButton: true,
                    focusConfirm: false,
                    confirmButtonText: 'Close',
                    confirmButtonAriaLabel: 'Close',
                })
                // gAlocation = data.collection;
            } else {
                Swal.fire({
                    title: data.msg,
                    icon: "error",
                    showCloseButton: true,
                    confirmButtonText: 'Close',
                })
            }
            // gBookingCrt['change'] = ;

        },
        error: errorAjax
    })
}

function setExtendTimeBooking(t) {
    var booking_id = t.data('booking_id');
    var duration = t.data('duration');
    var index = t.data('index');
    var name = t.data('name');
    var form = new FormData();
    form.append('booking_id', booking_id);
    form.append('index', index);
    form.append('extend', duration);
    form.append('name', name);
    Swal.fire({
        title: 'Are you sure want extend ' + name + ' of meeting?',
        text: "You will extend the data booking " + name + " !",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Extend Meeting !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            var bs = $('#id_baseurl').val();
            $.ajax({
                // url: bs + "booking/post/extend-book",
                url: bs + ajax.url.post_set_extendmeeting,
                type: "POST",
                data: form,
                processData: false,
                contentType: false,
                dataType: "json",
                beforeSend: function() {
                    $('#id_loader').html('<div class="linePreloader"></div>');
                    Swal.fire({
                        title: 'Please Wait !',
                        html: 'Process to extend meeting',
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
                        showNotification('alert-success', "Succes extend " + name, 'top', 'center')
                        // init();
                        reloadTable();
                    } else {
                        showNotification('alert-danger', "Extend " + name + " meeting is failed!!!", 'bottom', 'left')
                    }
                },
                error: errorAjax,
            })
        } else {

        }
    })
}

function endMeeting(t) {
    var id = t.data('id');
    var booking_id = t.data('booking_id');
    var name = t.data('name');
    var form = new FormData();
    form.append('id', id);
    form.append('booking_id', booking_id);
    form.append('name', name);
    form.append('user', false);

    Swal.fire({
        title: 'Are you sure want end ' + name + ' of meeting?',
        text: "You will end the data booking " + name + " !",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'End Meeting !',
        cancelButtonText: 'Close !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            var bs = $('#id_baseurl').val();
            $.ajax({
                // url: bs + "booking/post/endbook",
                url: bs + ajax.url.post_end_meeting,
                type: "POST",
                data: form,
                processData: false,
                contentType: false,
                dataType: "json",
                beforeSend: function() {
                    $('#id_loader').html('<div class="linePreloader"></div>');
                    Swal.fire({
                        title: 'Please Wait !',
                        html: 'Process to end meeting',
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
                        showNotification('alert-success', "Succes end " + name, 'top', 'center')
                        // init();
                        reloadTable();
                    } else {
                        showNotification('alert-danger', "End " + name + " meeting is failed!!!", 'bottom', 'left')
                    }
                },
                error: errorAjax,
            })
        } else {

        }
    })
}

function clickPIC(t) {
    let row = t.parents("tr");
    let item = row.data("bookingData");
    var id = t.data('id');
    var bs = $('#id_baseurl').val();
    $.ajax({
        // url: bs + "booking/get/data/pic/" + id,
        url: bs + ajax.url.get_pic_by_bookingid + "/" + id,
        type: "GET",
        dataType: "json",
        beforeSend: function() {},
        success: function(data) {
            if (data['status'] == "success") {
                var item = data.collection;
                var html = '';
                var npo = (item.no_phone == null || item.no_phone == undefined || item.no_phone == "null") ? "-" : item.no_phone;
                var ext = (item.no_ext == null || item.no_ext == undefined || item.no_ext == "null") ? "-" : item.no_ext;
                html += '<div class="row clearfix">\
                            <div class="col-xs-12 align-left">\
                                <b style="font-size:18px;">Name</b>\
                            </div>\
                        </div>';
                html += '<div class="row clearfix">\
                            <div class="col-xs-12 align-left">\
                                <b  style="font-size:20px;">' + item.name + '</b>\
                            </div>\
                        </div>';
                html += '<br>';
                html += '<div class="row clearfix">\
                            <div class="col-xs-12 align-left">\
                                <b  style="font-size:18px;">Email</b>\
                            </div>\
                        </div>';
                html += '<div class="row clearfix">\
                            <div class="col-xs-12 align-left">\
                                <b  style="font-size:20px;">' + item.email + '</b>\
                            </div>\
                        </div>';
                html += '<br>';
                html += '<div class="row clearfix">\
                            <div class="col-xs-12 align-left">\
                                <b  style="font-size:18px;">No.Phone </b>\
                            </div>\
                        </div>';
                html += '<div class="row clearfix">\
                            <div class="col-xs-12 align-left">\
                                <b  style="font-size:20px;">' + npo + '</b>\
                            </div>\
                        </div>';
                html += '<br>';
                html += '<div class="row clearfix">\
                            <div class="col-xs-12 align-left">\
                                <b  style="font-size:18px;">No.Extension </b>\
                            </div>\
                        </div>'
                html += '<div class="row clearfix">\
                            <div class="col-xs-12 align-left">\
                                <b  style="font-size:20px;">' + ext + '</b>\
                            </div>\
                        </div>';
                setTimeout(function() {
                    Swal.fire({
                        title: 'Organizer information',
                        type: "info",
                        // 
                        html: html,
                        showCloseButton: true,
                        focusConfirm: false,
                        confirmButtonText: 'C L O S E',
                        confirmButtonAriaLabel: 'C L O S E',
                    })
                }, 0)
            }
        }
    });
}

function openPartispant(t) {
    let row = t.parents("tr");
    let item = row.data("bookingData");
    let title = t.data('title');
    // console.log(item.attendees_list);

    clearTable($('#tbldataInternal'));
    clearTable($('#tbldataEksternal'));
    let colin = item.attendees_list.internal_attendess;
    let colek = item.attendees_list.external_attendess;
    let htmlin = '';
    let htmlek = '';
    let nnn = 0;
    let mmm = 0;
    $('#id_partisipant_title').html(title)
    $.each(colin, function(index, item) {
        nnn++;
        // var stt = item.attendance_status == 1 ? "Attend" : "No Attend";
        var stt = "";
        switch (item.attendance_status) {
            case "1":
                stt = "Attend";
                break;

            case "2":
                stt = "No Attend";
                break;
        
            default:
                stt = "-";
                break;
        }
        stt = item.execute_attendance == 0 ? "" : stt;
        htmlin += '<tr>'
        htmlin += '<td>' + nnn + '</td>';
        htmlin += '<td>' + (item.employee_name == null ? "" : item.employee_name.toUpperCase()) + '</td>';
        htmlin += '<td >' + (item.employee_email == null ? "" : item.employee_email) + '</td>';
        htmlin += '<td >' + (item.employee_phone == null ? "" : item.employee_phone) + '</td>';
        htmlin += '<td >' + (item.employee_extnum == null ? "" : item.employee_extnum) + '</td>';
        htmlin += '<td>' + stt + '</td>';
        htmlin += '</tr>'
    })
    $.each(colek, function(index, item1) {
        mmm++;
        // var stt = item1.attendance_status == 1 ? "Attend" : "No Attend";
        var stt = "";
        switch (item.attendance_status) {
            case "1":
                stt = "Attend";
                break;

            case "2":
                stt = "No Attend";
                break;
        
            default:
                stt = "-";
                break;
        }
        stt = item1.execute_attendance == 0 ? "" : stt;
        htmlek += '<tr>'
        htmlek += '<td>' + mmm + '</td>';
        htmlek += '<td>' + (item1.name == null ? "" : item1.name.toUpperCase()) + '</td>';
        htmlek += '<td>' + (item1.email == null ? "" : item1.email) + '</td>';
        htmlek += '<td>' + (item1.company == null ? "" : item1.company) + '</td>';
        // htmlek += '<td>' + (item1.position == null ? "" : item1.position) + '</td>';
        htmlek += '<td>' + stt + '</td>';
        htmlek += '</tr>'
    })
    $('#tbldataInternal tbody').html(htmlin)
    $('#tbldataEksternal tbody').html(htmlek)
    initTable($('#tbldataInternal'));
    initTable($('#tbldataEksternal'));
    $('#id_loader').html('');
    $('#id_mdl_partisipant').modal('show');
}

function initTable(selector) {
    // selector.DataTable();
    selector.DataTable({
        // "scrollX": true,
        // "scrollCollapse": true,
        "fixedHeader": true,
        paging: true,
        searching:        true,
        // bFilter :         false,
        info: false,
        // scrollResize:     true,
        order: [
            [0, "asc"]
        ],
        // lengthMenu: [[5, 10, 20, 100,-1], [5, 10, 20,100, 'ALL']],
        fixedColumns: {
            leftColumns: 1,
            rightColumns: 1
        },
        columnDefs: [{
                orderable: true,
                // className: 'select-checkbox',
                targets: 0,
                searchable: false,
            },
            
        ],
    })
}

function clearTable(selector) {
    selector.DataTable().destroy();
}

function openPopupReasonCanceled(t) {
    const row = t.parents("tr");
    const item = row.data("bookingData");

    Swal.fire({
        title: "Reason for Cancellation",
        text: item.canceled_note,
        type: "",
        showConfirmButton: false,
        showCancelButton: true,
        cancelButtonColor: '#d33',
        cancelButtonText: 'Close !',
        reverseButtons: true
    })
}

function openAttendStatus(t) {
    const row = t.parents("tr");
    const item = row.data("bookingData");
    const booking_id = item.booking_id;
    const nik = app.auth.nik;

    var form = new FormData();
    form.append('booking_id', booking_id);
    form.append('nik', nik);

    Swal.fire({
        title: 'Please confirm your attendance for the following event',
        type: "info",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Attend',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Decline',
        reverseButtons: true
    }).then((result) => {
        let status = result.value ? 1 : 2;
        form.append('status', status);

        let message = status == 1 ? "Note (Optional)" : "Reason for Not Attending";
        Swal.fire({
            title: message,
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
                if (status == 2 && (result == "" || result == null)) {
                    return Swal.showValidationMessage(`Reason for Cancellation is required`);
                }
            }
        }).then((result) => {
            if (result.value !== undefined) {
                let bs = $('#id_baseurl').val();
                
                let attendance_reason = result.value;
                form.append('attendance_reason', attendance_reason);
    
                $.ajax({
                    url: bs + ajax.url.post_confirm_attendance,
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
                            showNotification('alert-success', "Your attendance has been successfully confirmed! ", 'top', 'center');
                            // init();
                            reloadTable();
                        } else {
                            showNotification('alert-danger', "Failed to confirm attendance. Please try again.", 'bottom', 'left')
                        }
                    },
                    error: errorAjax,
                });
            }
        });
    });
}


$('#id_mdl_additional_partisipant').on('hidden.bs.modal', function (e) {
    if (!isAdditionalPartisipantManualOpen) {
        $("#id_frm_additional_booking_id").val("");
        
        gEmployeesSelectedArray = [];
        gEmployeesSelected = [];
        gEmployeesRegistered = [];
        $('#id_list_tbl_additional_participant tbody').html('');

        gPartisipantManual = [];
        $('#id_list_tbl_additional_participant_manual tbody').html("");
    }
});

function addInvitation(t) {
    const row = t.parents("tr");
    const item = row.data("bookingData");

    $("#id_frm_additional_booking_id").val(item.booking_id);

    if  (item.attendees_list.internal_attendess.length > 0)
    {
        gEmployeesSelectedArray = [];
        gEmployeesSelected = [];
        gEmployeesRegistered = item.attendees_list.internal_attendess.map(item => item.nik);
        reloadAdditionalPartisipant();
    }

    $("#id_mdl_additional_partisipant").modal({
        backdrop: 'static',
        keyboard: false
    });
}

function clickPartisipantAdditional(t) {
    var value = $('#id_frm_additional_participant').val()
    if (value != null) {
        $.each(value, (index, item) => {
            if (item != "") {
                gEmployeesSelected.push(item);
                for (var x in employeeCollection) {
                    if (employeeCollection[x].nik == item) {
                        gEmployeesSelectedArray.push(employeeCollection[x]);
                        break;
                    }
                }
            }
    
        })
        reloadAdditionalPartisipant();
        reloadAdditionalPartisipantSelected();
    }
}

function reloadAdditionalPartisipant() {
    let concatEmployeeSelected = gEmployeesRegistered
                                    .concat(gEmployeesSelected)
                                    .filter((value, index, self) => self.indexOf(value) === index);
    // var htmlEmp = '<option value=""></option>';
    var htmlEmp = '';
    $.each(employeeCollection, (index, item) => {
        if (concatEmployeeSelected.indexOf(item.nik) < 0) {
            htmlEmp += '<option value="' + item.nik + '">' + item.name + ' - ' + item.department_name + '</option>';
        }
    });
    $('#id_frm_additional_participant').html(htmlEmp);
    select_enable();
}

function reloadAdditionalPartisipantSelected() {
    var htmlEmp = '';
    $.each(gEmployeesSelected, (index, item) => {
        htmlEmp += '<tr id="id_tr_id_' + item + '" data-id="' + item + '">';
        htmlEmp += '<td style="width: 90%;">' + gEmployeesSelectedArray[index].name + '</td>';
        htmlEmp += '<td>\
            <button data-item="' + item + '" onclick="removeAdditionalParticipant($(this))" type="button" class="btn bg-red btn-sm waves-effect">\
                <i class="material-icons" >delete</i> \
            </button>\
        </td>';
        htmlEmp += '</tr>';
    });
    $('#id_list_tbl_additional_participant tbody').html(htmlEmp);
}

function removeAdditionalParticipant(t) {
    var item = t.data('item');
    var idtr = '#id_tr_id_' + item;
    var removeN = gEmployeesSelected.indexOf(item);
    gEmployeesSelected.splice(removeN, 1);
    gEmployeesSelectedArray.splice(removeN, 1);
    $(idtr).remove().delay(200);
    reloadAdditionalPartisipant();
}

function clickAdditionalPartisipantManualOpen(tp = "", dd = 0) {
    isAdditionalPartisipantManualOpen = true;
    $('#id_mdl_additional_partisipant').modal('hide');

    if (tp == "update") {
        var name = gPartisipantManual[dd]['name'];
        var email = gPartisipantManual[dd]['email'];
        var company = gPartisipantManual[dd]['company'];
        var position = gPartisipantManual[dd]['position'];
    } else {
        var name = "";
        var email = "";
        var company = "";
        var position = "";
    }
    var html = '<form id="id_frm_crt_manual" >\
                        <label for="id_frm_crt_manual_name">NAME</label>\
                        <div class="form-group">\
                            <div class="form-line">\
                                <input autocomplete="off" value="' + name + '" required type="text" id="id_frm_crt_manual_name" class="form-control" placeholder="Enter the name">\
                            </div>\
                        </div>\
                        <label for="id_frm_crt_manual_email">EMAIL</label>\
                        <div class="form-group">\
                            <div class="form-line">\
                                <input autocomplete="off"  value="' + email + '" type="email" id="id_frm_crt_manual_email" class="form-control" placeholder="Enter the email address">\
                            </div>\
                        </div>\
                        <label for="id_frm_crt_manual_org">COMPANY/ORGANIZATION</label>\
                        <div class="form-group">\
                            <div class="form-line">\
                                <input autocomplete="off" value="' + company + '"  required type="text" id="id_frm_crt_manual_company" class="form-control" placeholder="Enter the company/organization">\
                            </div>\
                        </div>\
                        <label for="id_frm_crt_manual_position">POSITION</label>\
                        <div class="form-group">\
                            <div class="form-line">\
                                <input autocomplete="off" value="' + position + '"  type="text" id="id_frm_crt_manual_position" class="form-control" placeholder="Enter the position">\
                            </div>\
                        </div>\
                        <br>\
                        <button style="display:none;" id="id_btn_crt_manual" class="btn btn-primary m-t-15 waves-effect">LOGIN</button>\
                    </form>';
    Swal.fire({
        title: 'Add external attendees',
        html: html,
        focusConfirm: false,
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        reverseButtons: true,
        confirmButtonText: tp == "update" ? "Update !" : 'Add !',
        confirmButtonAriaLabel: 'manually!',
        preConfirm: function(r) {
            if ($('#id_frm_crt_manual').valid()) {
                return true;
            } else {
                return false;
            }
            // console.log($('#id_frm_crt_manual').valid())  
        }
    }).then((result) => {
        if (result.value) {
            var data = {
                name: $('#id_frm_crt_manual_name').val(),
                email: $('#id_frm_crt_manual_email').val(),
                company: $('#id_frm_crt_manual_company').val(),
                position: $('#id_frm_crt_manual_position').val(),
            }
            if (tp == "update") {
                gPartisipantManual[dd] = data;
            } else {

                gPartisipantManual.push(data);
            }
            reloadAdditionalPartisipantManual();

        }

        $("#id_mdl_additional_partisipant").modal({
            backdrop: 'static',
            keyboard: false
        });
        isAdditionalPartisipantManualOpen = false;
        return false;
    });
}

function reloadAdditionalPartisipantManual() {
    var htmlEmp = '';
    $.each(gPartisipantManual, (index, item) => {
        var s = "update";
        htmlEmp += '<tr  id="id_tr_idmanual_' + index + '" style="cursor:pointer;">';
        htmlEmp += '<td style="width: 80%;">' + item.name + '</td>';
        htmlEmp += '<td>\
            <div class="btn-group" role="group" aria-label="...">\
                <button data-item="' + index + '" \
                    onclick="clickAdditionalPartisipantManualOpen(&quot;' + s + '&quot;,' + index + ')"  \
                    type="button" class="btn bg-orange btn-sm waves-effect">\
                    <i class="material-icons" >edit</i> \
                </button>\
                <button data-item="' + index + '" onclick="removeAdditionalParticipantManual($(this))" type="button" class="btn bg-red btn-sm waves-effect">\
                    <i class="material-icons" >delete</i> \
                </button>\
            </div> \
        </td>';
        htmlEmp += '</tr>';
    });
    $('#id_list_tbl_additional_participant_manual tbody').html(htmlEmp);
}

function removeAdditionalParticipantManual(t) {
    var item = t.data('item');
    var idtr = '#id_tr_idmanual_' + item;
    var removeN = item;
    gPartisipantManual.splice(removeN, 1);
    $(idtr).remove().delay(200);
    reloadAdditionalPartisipantManual();
}

function onSubmitAdditionalParticipants() {
    $("#id_frm_additional").trigger("submit");
}

let isFormAjaxAdditionalParticipant = "";
$("#id_frm_additional").submit(function(event) {
    event.preventDefault();
    
    if (isFormAjaxAdditionalParticipant != "") {
        isFormAjaxAdditionalParticipant.abort();
    }
    
    const form = $(this);
    let data = form.serializeArray();

    let bookingId = $("#id_frm_additional_booking_id").val();
    data.push({name: "booking_id", value: bookingId});

    if (gPartisipantManual.length > 0) {
        $.each(gPartisipantManual, function (_, item) {
            // company // email // name // position
            data.push({name: "external_attendees[]", value: JSON.stringify(item)});
        });
    }

    if (gEmployeesSelected.length > 0)  {
        $.each(gEmployeesSelectedArray, function (_, item) { 
            let d = {
                id: item.id,
                nik: item.nik,
                company: item.company_name,
                email: item.email,
                name: item.name,
                position: item.department_name,
                is_vip: item.is_vip
            };

            data.push({name: "internal_attendees[]", value: JSON.stringify(d)});
        });
    }

    // console.log(data);

    isFormAjaxAdditionalParticipant = $.ajax({
        type: "POST",
        dataType: "json",
        url: bs + ajax.url.post_additional_attendees,
        data: $.param(data),
        beforeSend: function()
        {
            $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function (data) {
            $('#id_loader').html('');
            enabledAllForm();
            if (data.status == "success") {
                $("#id_mdl_additional_partisipant").modal("hide");
                reloadTable();
            } else {
                var msg = "Your session is expired, login again !!!";
                if (data.msg != undefined || data.msg != "") {
                    msg = data.msg;
                }
                showNotification('alert-danger', msg, 'top', 'center');
            }
        },
        error: errorAjax,
        complete: function(data) { }
    });
});

function isHMinus(inputDate, minus = 14) {
    const today = moment().startOf('day'); // Ambil tanggal hari ini tanpa jam
    const targetDate = moment(inputDate).startOf('day'); // Ambil tanggal input tanpa jam

    // return today.isSameOrBefore(targetDate.subtract(14, 'days'), 'day'); // Cek apakah sama dengan H-14
    return targetDate.diff(today, 'days') <= minus; // Cek apakah sama dengan H-14
}

function isHPlus(inputDate, plus = 14) {
    const today = moment().startOf('day'); // Ambil tanggal hari ini tanpa jam
    const targetDate = moment(inputDate).startOf('day'); // Ambil tanggal input tanpa jam

    return targetDate.diff(today, 'days') > plus; // Cek apakah sama dengan lebi dari H+14 atau lebih
}

function addInternalParticipantByHost(id) {
    if (lastSelectedHost != undefined) {
        // Hapus dari array string
        gEmployeesSelected = gEmployeesSelected.filter(id => id !== lastSelectedHost);

        // Hapus dari array object berdasarkan `nik` atau `id`
        gEmployeesSelectedArray = gEmployeesSelectedArray.filter(emp => emp.nik !== lastSelectedHost || emp.id !== lastSelectedHost);
    }
    gEmployeesSelected.push(id);
    let employee = employeeCollection.find((item) => item.id == id || item.nik == id);
    gEmployeesSelectedArray.push(employee);
    lastSelectedHost = id;
    reloadPartisipant();
    reloadPartisipantSelected();
}