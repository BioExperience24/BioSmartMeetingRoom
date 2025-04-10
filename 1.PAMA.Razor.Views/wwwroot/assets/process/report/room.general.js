// let globalStatusInvoice = [];
// let modules;

$(function () {
    intrTime();
    initGlobalData();
    // initialLoad();
});

async function initGlobalData() {
    try {
        initRangeDate("roomusage");
        initRangeDate("organizer");
        initRangeDate("attendees");

        await fetchModules();
        await fetchBuilding();
        await fetchRoom();
        await fetchDepartment();
        await fetchEmployee();
        await fetchSettingInvoiceText();
    } finally {
        // load table
        initTableUsage();
    }
}

async function initTableUsage() {
    try {
        await initRoomUsageTable();
        await initOrganizerUsageTable();
        await initAttendeesTable();
    } finally { }
}

function intrTime() {
    setInterval(
        function () {
            var tm = moment().format('hh:mm A');
            $('#time1').html(tm);
        }, 500
    );
}

function initRangeDate(tab = "roomusage") {
    // $(`.input-group #id_${gGlobalTabs}_daterange_search`).daterangepicker({
    $(`.input-group #id_${tab}_daterange_search`).daterangepicker({
        "showDropdowns": true,
        "showWeekNumbers": true,
        "showISOWeekNumbers": true,
        "opens": "center",
        // "drops": "up",
        "startDate": moment().subtract(29, 'days').format('MM/DD/YYYY'),
        "endDate": moment().format('MM/DD/YYYY'),
        // "autoApply": true,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
            'Last Year': [moment().subtract(1, 'year').startOf('year'), moment().subtract(1, 'year').endOf('year')]
        },
        "alwaysShowCalendars": true,
    }, function (start, end, label) {
        // initRoom(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'))
    });
}

// Get API
async function fetchModules() {
    try {
        var data = await get_modules(
            "module_automation",
            "module_price",
            "module_room_advance",
            "module_int_365",
            "module_int_google",
            "module_invoice",
        );

        if (data.status == "success") {
            modules = data.collection;

            initTableHead();
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

function initTableHead() {
    // table room usage
    // if (modules.price.is_enabled == 1) {
    //     let rentCostHead = document.createElement("th");
    //     $(rentCostHead).text("Rent Cost");
    //     let statusInvHead = document.createElement("th");
    //     $(statusInvHead).text("Status Invoicing");

    //     $("#id_tbl_room").find("thead tr").append(rentCostHead);
    //     $("#id_tbl_room").find("thead tr").append(statusInvHead);
    // }

    if (modules.room_adv.is_enabled == 1) {
        // table organizer usage
        let totalAttendeesCheckinOrganizer = document.createElement("th");
        $(totalAttendeesCheckinOrganizer).text("Attendees Check-in");
        let totalApprove = document.createElement("th");
        $(totalApprove).text("Approve");

        $("#id_tbl_organizer").find("thead tr").append(totalAttendeesCheckinOrganizer);
        $("#id_tbl_organizer").find("thead tr").append(totalApprove);

        // table attendees
        let totalAttendeesCheckin = document.createElement("th");
        $(totalAttendeesCheckin).text("Attendees Check-in");
        $("#id_tbl_attendees").find("thead tr").append(totalAttendeesCheckin);
    }
}

function getModule() {
    return modules;
}

async function fetchBuilding() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_buildings
        });

        if (data.status == "success") {
            gBuilding = data.collection;

            initFilterBuilding("roomusage");
            initFilterBuilding("organizer");
            initFilterBuilding("attendees");
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

function initFilterBuilding(tab = "roomusage") {
    $(`#id_${tab}_building_search`).empty();

    $(`#id_${tab}_building_search`).append(`<option value="">All Building</option>`);

    $.each(gBuilding, function (_, item) {
        let opt = document.createElement("option");

        $(opt).text(`${item.name}`);
        $(opt).val(item.id);

        $(`#id_${tab}_building_search`).append(opt);
        select_enable();
    });
}

async function fetchRoom() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_rooms
        });

        if (data.status == "success") {
            gRoom = data.collection;

            initFilterRoom("roomusage");
            initFilterRoom("organizer");
            initFilterRoom("attendees");
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

function initFilterRoom(tab = "roomusage") {
    $(`#id_${tab}_room_search`).empty();

    $(`#id_${tab}_room_search`).append(`<option value="">All Room</option>`);

    $.each(gRoom, function (_, item) {
        let opt = document.createElement("option");

        $(opt).text(`${item.name}`);
        $(opt).val(item.radid);

        $(`#id_${tab}_room_search`).append(opt);
        select_enable();
    });
}

async function fetchDepartment() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_departments
        });

        if (data.status == "success") {
            gDepartment = data.collection;

            initFilterDepartment();
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

function initFilterDepartment() {
    $("#id_roomusage_department_search").empty();

    $("#id_roomusage_department_search").append(`<option value="">All Department</option>`);

    $.each(gDepartment, function (_, item) {
        let opt = document.createElement("option");

        $(opt).text(`${item.name}`);
        // $(opt).val(item.department_id);
        $(opt).val(item.id);

        $("#id_roomusage_department_search").append(opt);
        select_enable();
    });
}

async function fetchEmployee() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_employees
        });

        if (data.status == "success") {
            gEmployee = data.collection;

            initFilterEmployee("organizer");
            initFilterEmployee("attendees");

        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

function initFilterEmployee(tab = "organizer") {
    $(`#id_${tab}_employee_search`).empty();

    $(`#id_${tab}_employee_search`).append(`<option value="">All Employee</option>`);

    $.each(gEmployee, function (_, item) {
        let opt = document.createElement("option");

        $(opt).text(`${item.name}`);
        $(opt).val(item.nik);

        $(`#id_${tab}_employee_search`).append(opt);
        select_enable();
    });
}

async function fetchSettingInvoiceText() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_setting_invoice_texts
        });

        if (data.status == "success") {
            globalStatusInvoice = data.collection;
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        errorAjax;
    }
    // globalStatusInvoice = [];
    // $.ajax({
    //     url : bs + "report/get/status-invoice",
    //     type : "GET",
    //     dataType: "json",
    //     beforeSend: function(){
    //         $('#id_loader').html('<div class="linePreloader"></div>');
    //     },
    //     success:function(data){
    //         if(data.status == "success"){
    //             globalStatusInvoice = data.collection;
    //             initRoom();
    //         }else{
    //             var msg = "Your session is expired, login again !!!";
    //             showNotification('alert-danger', msg,'top','center')
    //         }
    //         $('#id_loader').html('');
    //     },
    //     error: errorAjax
    // });
}

function getTimeFromMins(mins) {
    if (mins < 0) {
        throw new RangeError("Valid input should be greater than or equal to 0 and less than 1440.");
    }
    var h = mins / 60 | 0,
        m = mins % 60 | 0;
    var dd = moment.utc().hours(h).minutes(m).format("HH:mm");
    spDd = dd.split(":");
    var frm = "";
    if (spDd[0] != "00") {
        frm += (spDd[0] - 0) + " hour ";
    }
    if (spDd[1] != "00") {
        frm += (spDd[1] - 0) + " minute ";
    }
    return frm;
}

function initialLoad() {
    gGlobalTabs = "roomusage";
    initGlobalData()
    initRoom();
    gGlobalTabs = "organizer";
    initGlobalData();
    filterOrganizer()
    gGlobalTabs = "attendees";
    initGlobalData();
    filterAttendees()

    gGlobalTabs = initialTab;
    initGlobalData();
}

function initTable(selector) {
    selector.DataTable();
}

function clearTable(selector) {
    if (selector != null) {
        selector.DataTable().destroy();
    }
}

function select_enable() {
    $('select').selectpicker("refresh");
    $('select').selectpicker("initialize");
}

function enable_datetimepicker() {
    $('.timepicker').bootstrapMaterialDatePicker({
        format: 'HH:mm',
        clearButton: true,
        date: false
    });
}

function loadingg(title = "", body = "") {
    Swal.fire({
        title: title,
        html: body,
        allowOutsideClick: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        },
    });
}

function changeTabs(argument) {
    if (gGlobalTabs != argument) {
        gGlobalTabs = argument;
    }
}

function errorAjax(xhr, ajaxOptions, thrownError) {
    $('#id_loader').html('');
    if (ajaxOptions == "parsererror") {
        var msg = "Status Code 500, Error Server bad parsing";
        showNotification('alert-danger', msg, 'bottom', 'left')
    } else {
        var msg = "Status Code " + xhr.status + " Please check your connection !!!";
        showNotification('alert-danger', msg, 'bottom', 'left')
    }
}