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
let gEmployeePIC = "";

let tbldata;

const timepickerOptionsTodayFrom = {
    timeFormat: 'HH:mm ',
    interval: 15,
    // minTime: moment(moment().format('YYYY-MM-DD') + " " +moment().format("HH:mm:")+"00").subtract(1, 'minutes').format("HH"),
    minTime: "00",
    maxTime: '23:45',
    defaultTime: moment().format("HH:mm:")+"00",
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
    defaultTime: getMinutesForNow(moment()).format("HH:mm"),
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

$(function () {
    bs = $('#id_baseurl').val();
    
    init_modules();
    
    init_datetimepicker_filter();
    init_date_book_filter();
    init_time_book_filter();

    runTooltip();

    get_employees();
    get_buildings();
    get_rooms();
    get_facilities();
    get_departments();
    get_pantry_packages();

    init_table_booking_list();

    $("#form-find-reserve").trigger("submit");
});

$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    let roomCategory = $(e.target).data("roomCategory");

    if (roomCategory != undefined) {
        let lastRoomCategory = $("#id_book_filter_room_category").val();

        if (roomCategory != lastRoomCategory) {
            $("#id_book_filter_room_category").val(roomCategory);

            $("#form-find-reserve").trigger("submit");
        }
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

$("#id_frm_crt_pic").on("change", function () {
    const t = $(this);

    genDepartmentFromPic(t.val());
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
                qty: item.qty
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
            success: function (data) {
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
            error: errorAjax
        });
    }
});

$("#id_schedule_search").click(function (e) { 
    e.preventDefault();
    reloadTable();
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
                    note: item.note,
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

function init_date_book_filter()
{
    $("#id_book_filter_when").daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        minDate: moment(),
        drops: "auto",
        locale: {
            // format: 'DD MMM YYYY'
            format: 'YYYY-MM-DD'
        }
        // minYear: 1901,
        // maxYear: parseInt(moment().format('YYYY'),10),
    }, function(start, end, label) {
        // console.log(`start: ${start}`);
        // console.log(`end: ${end}`);
        // console.log(`label: ${label}`);
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

function init_time_reserve_form(date, from, to) {
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
    
    
    optUntil["scrollbar"] = false;
    // optUntil["defaultTime"] = `${to}:59`;
    // optUntil["startTime"] = getMinutesForNow(moment(`${date} ${to}:59`, 'YYYY-MM-DD HH:mm')).format("HH:mm");
    // optUntil["defaultTime"] = `23:59:59`;
    // optUntil["startTime"] = getMinutesForNow(moment(`${date} 23:59:59`, 'YYYY-MM-DD HH:mm')).format("HH:mm");

    $('#id_frm_crt_timestart').timepicker(optFrom);
    $('#id_frm_crt_timeend').timepicker(optUntil);

    let setStart = moment(`${date} ${from}:00`, 'YYYY-MM-DD HH:mm').toDate();
    $('#id_frm_crt_timestart').timepicker("setTime", setStart);
    
    let setEnd = moment(`${date} ${to}:59`, 'YYYY-MM-DD HH:mm').toDate();
    $('#id_frm_crt_timeend').timepicker("setTime", setEnd);
}

function init_pic_reserve_form() {
    $("#id_frm_crt_pic").empty();

    $.each(employeeCollection, function (_, item) { 
        let opt = document.createElement("option");

        $(opt).text(`${item.name}`);
        // $(opt).val(item.department_id);
        $(opt).val(item.id);

        $("#id_frm_crt_pic").append(opt);
        select_enable();
    });
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
    let t = $(_this);
    
    let room = t.data("roomCardData");
    // console.log(room);

    let formFilter = objectify_form($("#form-find-reserve").serializeArray());
    // console.log(formFilter);

    $("#id_frm_crt_room_name").text(room.name);
    $("#id_frm_crt_booking_type").val(formFilter["book_filter_room_category"] == "room" ? "general" : formFilter["book_filter_room_category"]);
    $("#id_frm_crt_date").val(formFilter["book_filter_date"]);
    $("#id_frm_crt_room_id").val(room.radid);

    init_time_reserve_form(
        formFilter["book_filter_date"],
        formFilter["book_filter_time_from"], 
        formFilter["book_filter_time_until"]);
    init_pic_reserve_form();
    init_alocation_reserve_form();
    
    tabsChangeTo("disabled");
    $("#id_area_find_room").addClass("hidden");
    $("#id_area_reserve_room").removeClass("hidden");
}

function onButtonBack() {
    clearReserveRoom();

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
    let selectedData;
    $.each(employeeCollection, function (_, item) { 
        if (item.id == itemID) {
            selectedData = item;
            $("#id_frm_crt_alocation_name").val(item.department_name);
            $("#id_frm_crt_alocation_id").val(item.department_id);
        }
    });
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
                    <td style="word-wrap: break-word">${item.note}</td>
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

function reloadPartisipantManual() {
    var htmlEmp = '';
    $.each(gPartisipantManual, (index, item) => {
        var s = "update";
        htmlEmp += '<tr  id="id_tr_idmanual_' + index + '" onclick="clickPartisipantManualOpen(&quot;' + s + '&quot;,' + index + ')" style="cursor:pointer;">';
        htmlEmp += '<td style="width: 90%;">' + item.name + '</td>';
        htmlEmp += '<td>\
            <button data-item="' + index + '" onclick="removeCrtParticipantManual($(this))" type="button" class="btn bg-red btn-sm waves-effect">\
                <i class="material-icons" >delete</i> \
            </button>\
        </td>';
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

function reloadPartisipantSelected() {
    var htmlEmp = '';
    $.each(gEmployeesSelected, (index, item) => {
        htmlEmp += '<tr id="id_tr_id_' + item + '" data-id="' + item + '">';
        htmlEmp += '<td style="width: 90%;">' + gEmployeesSelectedArray[index].name + '</td>';
        htmlEmp += '<td>\
            <button data-item="' + item + '" onclick="removeCrtParticipant($(this))" type="button" class="btn bg-red btn-sm waves-effect">\
                <i class="material-icons" >delete</i> \
            </button>\
        </td>';
        htmlEmp += '</tr>';
    });
    $('#id_list_tbl_participant tbody').html(htmlEmp);
    reGenQtyPatryDetail();
}

function removeCrtParticipant(t) {
    var item = t.data('item');
    var idtr = '#id_tr_id_' + item;
    var removeN = gEmployeesSelected.indexOf(item);
    gEmployeesSelected.splice(removeN, 1);
    gEmployeesSelectedArray.splice(removeN, 1);
    $(idtr).remove().delay(200);
    reloadPartisipant();
}

// table booking list
function init_table_booking_list() {
    tbldata = $("#tbldata").DataTable({
        searching: false,
        bLengthChange: false,
        bInfo: true,
        ordering: false,
        columns: [
            {data:"no", name:"no", searchable:false, orderable:false},
            {data:"title", name:"title", searchable:false, orderable:false},
            {data:"room_name", name:"room_name", searchable:false, orderable:false},
            {data:"booking_date", name:"booking_date", searchable:false, orderable:false},
            {data:"time", name:"time", searchable:false, orderable:false},
            {data:"attendees", name:"attendees", searchable:false, orderable:false},
            {data:"pic", name:"pic", searchable:false, orderable:false},
            // {data:"action", name:"action", searchable:false, orderable:false},
        ],
        "order": [[ 0, 'asc' ]],
        ajax: {
            url: bs + ajax.url.get_bookings,
            contentType: 'application/json',
            beforeSend: function() {
                if (typeof tbldata != "undefined" && tbldata.hasOwnProperty('settings') && tbldata.settings()[0].jqXHR != null) {
                    tbldata.settings()[0].jqXHR.abort();
                }
            },
            data: function (param) {
                param.booking_date = $("#id_schedule_daterange_search").val();
                param.booking_organizer = $("#id_schedule_employee_search").val();
                param.booking_building = $("#id_schedule_building_search").val();
                param.booking_room = $("#id_schedule_room_search").val();
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
            $(row).find('td:eq(5)').text(`${data.attendees} participants`);
        },
    });
}

function reloadTable() {
    if (typeof tbldata != "undefined") {
        tbldata.ajax.reload();
    }
};