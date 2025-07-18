times = generateTimes();

let claims = {};

const templateAvailableRoom = `
<li id="list-[[ROOM_ID]]" class="list-group-item" style=" padding: 10px; border-radius: 10px; border: 1px solid #ddd; margin-bottom: 10px; box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1); display: flex; flex-direction: column; gap: 8px; "> 
        <div style="font-weight: bold; font-size: 14px;"> Room [[ROOM_NAME]] </div> <div style=" display: flex; align-items: flex-start; gap: 10px; ">
        <div>
            <img src="[[IMAGE]]" alt="Room Image" style=" width: 80px; height: 80px; border-radius: 10px; object-fit: cover; "> 
        </div>
        <div style=" display: flex; flex-direction: column; gap: 10px; width: 100%; ">
            <div style="flex-grow: 1; line-height: 1.2;">
                <div style="color: #666; font-size: 12px;">Building: [[BUILDING_NAME]]</div>
                <div style="color: #666; font-size: 12px;">Seats: [[ROOM_SEATS]]</div>
                <div style="color: #888; font-size: 10px; margin-top: 3px;">
                    [[ROOM_FACILITIES]]
                </div>
            </div>
            <div style=" flex-shrink: 0; display: flex; align-items: center; justify-content: flex-end; ">
                <button class="select-room-btn" data-room-id="[[ROOM_ID]]" style="
                    background-color: #32CD32;
                    color: white;
                    border: none;
                    padding: 2px 5px;
                    border-radius: 8px;
                    font-size: 12px;
                    white-space: nowrap;
                    cursor: pointer;
                    transition: background-color 0.3s;
                ">
                    Select
                </button>
            </div>
        </div>
    </div>
</li>
`;

const templateNotAvailableRoom = `
<li class="px-0" style="display: flex; align-items: center; justify-content: space-between; padding: 10px;">
    <b class="text-center">No rooms available matching your search criteria.</b>
</li>
`;

const seats = [
    { id: "1_5", text: "1-5" },
    { id: "6_10", text: "6-10" },
    { id: "11_20", text: "11-20" },
    { id: "20", text: "20+" },
];

let buildingCollection;
let facilityCollection;
let pantryPakcageCollection;
let employeeCollection;
let alocationCollection;

let selectedExternalAttendees = [];

let selectedInternalAttendees = [];
let selectedInternalAttendanceNIKs = [];
let selectedNikOrganizer = null;

let pantryPackageDetailCollection = [];

let isFormAjax = "";
let isFormAjaxReserve = "";

$(function () {
    loadInit();
});

async function loadInit()
{
    try {
        loadDateFilter();
        loadSeatOptions();
        setTimeout(() => {
            const nowtime = getMinutesForNow(moment());
            generateTimeOptions("#start-time", nowtime.format("HH:mm"));
            generateTimeOptions("#end-time", nowtime.add(30, 'minutes').format("HH:mm"));
        }, 500);
        await loadClaims();
        await loadBuildings();
        await loadFacilites();
        await loadEmployees();
        await loadAlocations();
        await loadPantryPackages();
    } finally {
        setTimeout(() => {
            $("#filter-btn").trigger("click");
        }, 550);
    }
}

async function loadClaims() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bsApiSmr + ajax.url.get_claims,
        });

        claims = {
        role: data["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
        nik: data["http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata"],
        }
    } catch (err) {
        console.log(err);
        window.location.href = `/webview/error/401`;
    }
}

async function loadBuildings() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bsApiSmr + ajax.url.get_buildings
        });

        if (data.status == "success")
        {
            buildingCollection =  data.collection;
            loadBuildingOptions();
        } else {
            showErrorLoadNotification();
        }
    } catch (err) {
        console.log(err);
        showErrorLoadNotification();
    }
}

function loadBuildingOptions() {
    $("#building").empty();
    $("#building").append(
        $("<option></option>").val("").html("-- Select Building --")
    );
    $.each(buildingCollection, function (index, item) {
        $("#building").append(
            $("<option></option>").val(item.id).html(item.name)
        );
    });
    $('#building').select2();
}

async function loadFacilites() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bsApiSmr + ajax.url.get_facilities
        });

        if (data.status == "success")
        {
            facilityCollection =  data.collection;

            loadFacilityOptions();
        } else {
            showErrorLoadNotification();
        }
    } catch (err) {
        console.log(err);
        showErrorLoadNotification();
    }
}

function loadFacilityOptions() {
    $("#facilities").empty();
    $.each(facilityCollection, function (index, item) {
        $("#facilities").append(
            $("<option></option>").val(item.id).html(item.name)
        );
    });
    $('#facilities').select2();
}

async function loadEmployees () {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bsApiSmr + ajax.url.get_employees
        });

        if (data.status == "success")
        {
            employeeCollection =  data.collection;
        } else {
            showErrorLoadNotification();
        }
    } catch (err) {
        console.log(err);
        showErrorLoadNotification();
    }
}

function loadInternalAttendeesOptions() {
    let internalAttendeeSelect = $("#internalAttendeeSelect");
    internalAttendeeSelect.empty();
    let availableEmployees = employeeCollection.filter(
    (emp) => !selectedInternalAttendanceNIKs.includes(emp.id)
    );
    availableEmployees.forEach((emp) => {
        internalAttendeeSelect.append(
            `<option value="${emp.nik}">${emp.name} - ${emp.department_name}</option>`
        );
    });
    internalAttendeeSelect.select2();

    regeneratePackageDetailQty();
}

async function loadAlocations() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bsApiSmr + ajax.url.get_alocations
        });

        if (data.status == "success")
        {
            alocationCollection =  data.collection;
        } else {
            showErrorLoadNotification();
        }
    } catch (err) {
        console.log(err);
        showErrorLoadNotification();
    }
}

async function loadPantryPackages() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bsApiSmr + ajax.url.get_pantry_packages
        });

        if (data.status == "success")
        {
            pantryPakcageCollection =  data.collection;

            loadPantryPackageOptions();
        } else {
            showErrorLoadNotification();
        }
    } catch (err) {
        console.log(err);
        showErrorLoadNotification();
    }
}

function loadSeatOptions() {
    $("#seats").empty();
    $("#seats").append(
        $("<option></option>").val("").html("-- Select seats --")
    );
    $.each(seats, function (index, item) {
        $("#seats").append(
            $("<option></option>").val(item.id).html(item.text)
        );
    });
    $('#seats').select2();
}

function loadDateFilter() {
    if ($('#when-date').data('daterangepicker') != undefined) {
        $('#when-date').data('daterangepicker').remove(); // Hapus instance
    }
    if ($('#until-date').data('daterangepicker') != undefined) {
        $('#until-date').data('daterangepicker').remove(); // Hapus instance
    }

    let opt = {
        singleDatePicker: true,
        showDropdowns: true,
        minDate: moment(),
        drops: "auto",
        locale: {
            format: 'YYYY-MM-DD'
        },
        isInvalidDate: function(date) {
            if (roomType == "noroom") {
                return date.day() === 0 || date.day() === 6;
            }
        }
    };

    let optUntil = {
        singleDatePicker: true,
        showDropdowns: true,
        minDate: moment(),
        drops: "auto",
        locale: {
            format: 'YYYY-MM-DD'
        },
    };

    $('#until-date').parent().parent().hide();
    if (roomType == "noroom" || roomType == "room") {
        opt["minDate"] = moment();
        // opt["maxDate"] = moment().add(3, 'days');
    } else if (roomType == "trainingroom") {
        $('#until-date').parent().parent().show();
        optUntil["minDate"] = moment();
        optUntil["maxDate"] = moment().add(1, 'year');
    }

    $("#when-date").daterangepicker(opt, function(start, end, label) {
         // Ensure the format is consistent
        $("#when-date").val(start.format('YYYY-MM-DD'));
    });

    $("#when-date").on('apply.daterangepicker', function(ev, picker) {
        let startDate = picker.startDate;
        let endDatePicker = $('#until-date').data('daterangepicker');
        // endDatePicker.minDate = startDate.add(1, 'days'); // Ensure end date is later than start date
        endDatePicker.setStartDate(startDate);
        endDatePicker.setEndDate(startDate);
        endDatePicker.minDate = startDate;

        if (roomType == "trainingroom") {
            let maxEndDate = startDate.clone().add(1, 'year');
            endDatePicker.maxDate = maxEndDate;
        }
        endDatePicker.updateView();

        // Update until-date to match the new start date
        $(this).val(startDate.format('YYYY-MM-DD'));
        $('#until-date').val(startDate.format('YYYY-MM-DD'));
        // $('#until-date').val("");
    });

    $("#until-date").daterangepicker(optUntil, function(start, end, label) {
        // Ensure the format is consistent
        $("#until-date").val(start.format('YYYY-MM-DD'));
    });
}

function loadOrganizerOptions() { 
    $("#organizer").empty();
    $("#organizer").append(
        $("<option></option>").val("").html("-- Select Organizer (Host) --")
    );
    $.each(employeeCollection, function (index, item) {
        $("#organizer").append(
            $("<option></option>").val(item.nik).html(`${item.name} - ${item.department_name}`)
        );
    });
    $('#organizer').select2();
}

function loadPantryPackageOptions() {
    $("#category").empty();
    $("#category").append(
        $("<option></option>").val("").html("-- Select Meeting Category --")
    );
    $.each(pantryPakcageCollection, function (index, item) {
        $("#category").append(
            $("<option></option>").val(item.id).html(item.name)
        );
    });
    $('#category').select2();
}

function renderRoomCard(collections) {
    $("#room-list").empty();
    if (collections.length > 0) {
        $.each(collections, function (_, item) { 
            let template = templateAvailableRoom
                .replace(/\[\[ROOM_ID\]\]/g, item.radid)
                .replace(/\[\[IMAGE\]\]/g, bsApi + "Room/GetRoomDetailView/" + 
                    item.image)
                .replace(/\[\[ROOM_NAME\]\]/g, item.name)
                .replace(/\[\[BUILDING_NAME\]\]/g, item.building_name)
                .replace(/\[\[ROOM_SEATS\]\]/g, item.capacity)
                .replace(/\[\[ROOM_FACILITIES\]\]/g, generateSelectedFacility(item.facility_room));

            
            $("#room-list").append(template);
            $("#list-" + item.radid).data(item);
        });
    } else {
        $("#room-list").append(templateNotAvailableRoom);
    }
}

function generateSelectedFacility(facilityIds = []) {
    let selectedFacilities = facilityCollection.map((facility) => {
        return facilityIds.includes(facility.id) ? facility.name : null;
    });
    return selectedFacilities.filter(Boolean).join(", ");
}

function generatePicAndDepartment() {
    let employee = employeeCollection.find(emp => emp.nik === claims.nik);
    if (employee != undefined && claims.role == "2") {
        $("#section-organizer").hide();
        $("#id_frm_crt_pic").val(employee.nik);
        
        selectedNikOrganizer = employee.nik;
        let alocation = alocationCollection.find(aloc => aloc.id === employee.department_id);

        if (alocation != undefined) {
            $("#id_frm_crt_alocation_id").val(alocation.id);
            $("#id_frm_crt_alocation_name").val(alocation.name);
        }
        
        populateInternalAttendess(employee.nik, true);
    } else {
        $("#section-organizer").show();
        loadOrganizerOptions();
    }
    
}

function populateInternalAttendess(nik, isPic = false) {
    let employee = employeeCollection.find((e) => e.nik === nik);
    if (!selectedInternalAttendanceNIKs.includes(employee.nik)) {
        let newAttendee = `
            <li id="list-attendance-${employee.nik}" class="list-group-item">
                <strong>${employee.name}</strong> - ${employee.department_name}
                <button class="btn btn-sm btn-danger float-right remove-attendee">Remove</button>
            </li>`;
        if (isPic) {
            newAttendee = `
            <li id="list-attendance-${employee.nik}" class="list-group-item">
                <strong>${employee.name}</strong> - ${employee.department_name}
            </li>`;

            if (selectedNikOrganizer != null) {
                // remove selected organizer and replace with new one
                $("#list-attendance-" + selectedNikOrganizer).remove();

                // remove from selectedInternalAttendanceNIKs and selectedInternalAttendees
                selectedInternalAttendanceNIKs = selectedInternalAttendanceNIKs.filter(nik => nik != selectedNikOrganizer);
                selectedInternalAttendees = selectedInternalAttendees.filter(emp => emp.nik != selectedNikOrganizer);
            }
            selectedNikOrganizer = employee.nik;
        }
        $("#internal-attendees").append(newAttendee);
        $(`#list-attendance-${employee.nik}`).data(employee);
        selectedInternalAttendees.push(employee);
        selectedInternalAttendanceNIKs.push(employee.nik);
    }

    if (selectedInternalAttendanceNIKs.length > 0) {
        $("#no-internal-attendees").hide();
    }
}

function populateExternalAttendess(data) {
    selectedExternalAttendees.push(data);

    let li = document.createElement("li");

    $(li).addClass("list-group-item");

    $(li).html(`
        <strong>${data.name}</strong>
        <div class="text-muted">${data.company} - ${data.position} - ${data.email}</div>
        <button class="btn btn-sm btn-danger float-right remove-external-attendee">Remove</button>
    `);

    $(li).data(data);
    
    // let newAttendee = `
    // <li class="list-group-item">
    // <strong>${data.name}</strong>
    // <div class="text-muted">${data.company} - ${data.position} - ${data.email}</div>
    // <button class="btn btn-sm btn-danger float-right remove-external-attendee">Remove</button>
    // </li>`;
    

    $("#external-attendees").append(li);

    if (selectedExternalAttendees.length > 0) {
        $("#no-external-attendees").hide();
    } else {
        $("#no-external-attendees").show();
    }
}

async function loadPackageDetail(pantryPackageId) {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bsApiSmr + ajax.url.get_pantry_package_by_id + pantryPackageId
        });

        if (data.status == "success")
        {
            let collection  = data.collection
            let items = collection.detail;
            
            let detail = [];
            $.each(items, function (_, item) { 
                detail.push({
                    id: item.id,
                    pantry_id: item.pantry_id,
                    name: item.name,
                    note: "",
                    description: item.description,
                    qty: selectedExternalAttendees.length + selectedInternalAttendees.length,
                });
            });

            pantryPackageDetailCollection =  detail;

            generatePackageDetail();
            
        } else {
            showErrorLoadNotification();
        }
    } catch (err) {
        // console.log(err);
        showErrorLoadNotification();
    }
}

function generatePackageDetail() {
    
    $("#item-list").empty();
    if (pantryPackageDetailCollection != null)  {
        let itemHtml;
        
        $.each(pantryPackageDetailCollection, function (key, item) { 
            let li  = document.createElement("li");
            $(li).data(item);
            $(li).addClass("list-group-item d-flex justify-content-between");
            $(li).attr("id", "list-package-" + item.id);
            
            let itemHtml = `
                <span>${item.name}</span>
                <div class="d-flex align-items-center">
                        <button
                        class="btn btn-sm btn-secondary btn-danger rounded-circle circle-btn decrement"
                        style="
                            width: 32px;
                            height: 32px;
                            min-width: 32px;
                            min-height: 32px;
                            border-radius: 50%;
                            padding: 0;
                            display: flex;
                            align-items: center;
                            justify-content: center;
                        "
                        >
                        -
                        </button>
                        <span class="mx-3 count">${item.qty}</span>
                        <button
                        class="btn btn-sm btn-secondary btn-success rounded-circle circle-btn increment"
                        style="
                            width: 32px;
                            height: 32px;
                            min-width: 32px;
                            min-height: 32px;
                            border-radius: 50%;
                            padding: 0;
                            display: flex;
                            align-items: center;
                            justify-content: center;
                        "
                        >
                        +
                        </button>
                </div>
            `;

            $(li).html(itemHtml);

            $("#item-list").append(li);
        });
    }
}

function regeneratePackageDetailQty() {
    let ttlParticipant = selectedExternalAttendees.length + selectedInternalAttendees.length;

    $.each(pantryPackageDetailCollection, function (key, item) { 
        pantryPackageDetailCollection[key]["qty"] = ttlParticipant;
    });

    generatePackageDetail();
}

$("#start-time").on("change", function () {
    let selectedTime = moment($(this).val(), "HH:mm");
    // generateTimeOptions("#end-time", selectedTime.add(30, 'minutes').format("HH:mm"));
    generateTimeOptions("#end-time", getMinutesForNow(selectedTime).format("HH:mm"));
});

$("#id_frm_crt_timestart").on("change", function () {
    let selectedTime = moment($(this).val(), "HH:mm");
    // generateTimeOptions("#end-time", selectedTime.add(30, 'minutes').format("HH:mm"));
    generateTimeOptions("#id_frm_crt_timeend", selectedTime.format("HH:mm"));
});

$("#organizer").on("change", function () {
    let employee = employeeCollection.find(emp => emp.nik === $(this).val());
    if (employee != undefined) {
        $("#id_frm_crt_pic").val(employee.nik);

        let alocation = alocationCollection.find(aloc => aloc.id === employee.department_id);

        if (alocation != undefined) {
            $("#id_frm_crt_alocation_id").val(alocation.id);
            $("#id_frm_crt_alocation_name").val(alocation.name);
        }
        
        populateInternalAttendess(employee.nik, true);
        loadInternalAttendeesOptions();
    }
});

$("#filter-btn").on("click", function (e) {
    e.preventDefault();
    if (isFormAjax != "") {
        isFormAjax.abort();
    }

    let param = {
        book_filter_room_category: roomType,
        book_filter_date: $("#when-date").val(),
        book_filter_date_until: $("#until-date").val(),
        book_filter_time_from: $("#start-time").val(),
        book_filter_time_until: $("#end-time").val(),
        book_filter_location: 0,
    };

    if ($("#building").val() != "") {
        param.book_filter_location = $("#building").val();
    }

    if($("#seats").val() != "") {
        param.book_filter_cap_seat = $("#seats").val();
    }

    if ($("#facilities").val().length > 0) {
        param.book_filter_cap_facilities = $("#facilities").val();
    }

    isFormAjax = $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: bsApiSmr + ajax.url.get_available_rooms,
        data: param,
        beforeSend: function() {
            $("#room-list").empty();
            $("#loader-list").show();
        },
        success: function (data) {
            if (data.status == "success") {
                renderRoomCard(data.collection);
            } else {
                renderRoomCard([]);
                showErrorLoadNotification();
            }
        },
        error: function(err) {
            console.log(err);
            showErrorLoadNotification();
        },
        complete: function() {
            $("#loader-list").hide();
        }
    });
});

$(document).on("click", ".select-room-btn", function (e) {
    e.preventDefault();
    // console.log(roomType);
    const rId = $(this).data("room-id");
    const room = $("#list-" + rId).data();
    // console.log(room);
    const formFilter = objectifyForm($("#id_frm").serializeArray());
    // console.log(formFilter);

    let rCategory = roomType == "room" ? "general" : roomType;
    let sDate = formFilter["when_date"];
    let eDate = formFilter["until_date"];
    
    let sDateFormatted = moment(sDate).format("DD MMM YYYY");
    let tDate = `(${sDateFormatted})`;

    let isDisablePastTime = true;
    if (rCategory == "trainingroom") {
        sDateFormatted = moment(sDate).format("DD MMM YYYY");
        if (sDate != eDate) {
            let eDateFormatted = moment(eDate).format("DD MMM YYYY");
            tDate = `(${sDateFormatted} - ${eDateFormatted})`;
        }

        isDisablePastTime = false;
    }

    reservedTimes = room.reserved_times;

    $("#selected-room").html(`
        ${room.name}
        <br>
        <small><b>${tDate}</b></small>
    `);
    $("#id_frm_crt_booking_type").val(rCategory);
    $("#id_frm_crt_date").val(sDate);
    $("#id_frm_crt_date_until").val(eDate);
    $("#id_frm_crt_room_id").val(room.radid);

    let now = getMinutesForNow(moment()).format("HH:mm");
    let startTime = formFilter.start_time ?? now;
    let endTime = formFilter.end_time ?? now;
    generateTimeOptions("#id_frm_crt_timestart", startTime, reservedTimes, isDisablePastTime);
    generateTimeOptions("#id_frm_crt_timeend", endTime, reservedTimes, isDisablePastTime);

    generatePicAndDepartment();

    loadInternalAttendeesOptions();

    $("#create-meeting").hide();
    $("#meeting-details").show();
    $("html, body").animate({ scrollTop: 0 }, "slow");
});

$("#internalAttendeeSelect").select2({
    allowClear: true,
    width: "100%",
});

$("#addInternalAttendeeBtn").click(function () {
    let selectedEmployees = $("#internalAttendeeSelect").val();
    if (selectedEmployees.length === 0) {
        showNotification(message = "Please select at least one employee.", type = "warning")
        return;
    }
    selectedEmployees.forEach((nik) => {
        populateInternalAttendess(nik);
    });
    $("#internalAttendeeModal").modal("hide");
    loadInternalAttendeesOptions();
});

$(document).on("click", ".remove-attendee", function () {
    const row = $(this).closest("li");
    const item = row.data();
    // console.log(item);
    
    // remove from selectedInternalAttendanceNIKs and selectedInternalAttendees
    selectedInternalAttendanceNIKs = selectedInternalAttendanceNIKs.filter(nik => nik != item.nik);
    selectedInternalAttendees = selectedInternalAttendees.filter(emp => emp.nik != item.nik);

    // remove from list
    $(this).parent().remove();

    if (selectedInternalAttendanceNIKs.length < 1) {
        $("#no-internal-attendees").show();
    } else {
        $("#no-internal-attendees").hide();
    }
    
    loadInternalAttendeesOptions();   
});

$("#addExternalAttendeeBtn").click(function () {
    let name = $("#attendeeName").val().trim();
    let email = $("#attendeeEmail").val().trim();
    let company = $("#attendeeCompany").val().trim();
    let position = $("#attendeePosition").val();

    if (
    name === "" ||
    email === "" ||
    company === "" ||
    position === ""
    ) {
        alert("Please enter all required fields.");
        return;
    }

    let data = {
        "name": name,
        "email": email,
        "company": company,
        "position": position,
    };

    populateExternalAttendess(data);
    regeneratePackageDetailQty();

    $("#attendeeName").val("");
    $("#attendeeEmail").val("");
    $("#attendeeCompany").val("");
    $("#attendeePosition").val("");
    $("#externalAttendeeModal").modal("hide");
});

$(document).on("click", ".remove-external-attendee", function () {
    const row = $(this).closest("li");
    const item = row.data();

    selectedExternalAttendees = selectedExternalAttendees.filter(p => p.email != item.email);
    
    // remove from list
    $(this).parent().remove();

    if (selectedExternalAttendees.length > 0) {
        $("#no-external-attendees").hide();
    } else {
        $("#no-external-attendees").show();
    }

    regeneratePackageDetailQty();
});

$("#category").on("change", function () {
    const t = $(this);
    loadPackageDetail(t.val());
});

$(document).on("click", ".circle-btn", function () {
    const row = $(this).closest("li");
    const data = row.data();
    const isIncrement = $(this).hasClass("increment") ? true : false;

    let qty = parseInt(data.qty);
    if (isIncrement) {
        qty++;
    } else {
        qty--;
    }

    if (qty < 0) {
        qty = 0;
    }
    
    $.each(pantryPackageDetailCollection, function (key, item) { 
        if (item.id == data.id) {
            pantryPackageDetailCollection[key]["qty"] = parseInt(qty);
        }
    });

    generatePackageDetail();
});

$("#submit-btn").click(function (e) {
    e.preventDefault();
    $("#confirmationModal").modal("show");
});

$("#confirm-submit").click(function () {
    $("#confirmationModal").modal("hide");
    // window.location.href = "index-0.html";
    $("#id_frm_crt").trigger("submit");
});

$("#confirmationModal").modal({
    backdrop: false,
    keyboard: false,
    show: false,
});

$("#id_frm_crt").submit(function(event) {
    event.preventDefault();
    if (isFormAjaxReserve != "") {
        isFormAjaxReserve.abort();
    }
    
    const form = $(this);
    let data = form.serializeArray();

    data = data.map(item => {
        if (item.name === "note" || item.name === "external_link" || item.name === "meeting_category") {
            item.value = item.value === null || item.value === "" ? "-" : item.value;
        }
        return item;
    });

    data.push({name: "device", value: "web"});

    if (selectedExternalAttendees.length > 0) {
        $.each(selectedExternalAttendees, function (_, item) {
            // company // email // name // position
            data.push({name: "external_attendees[]", value: JSON.stringify(item)});
        });
    }

    if (selectedInternalAttendanceNIKs.length > 0)  {
        $.each(selectedInternalAttendees, function (_, item) { 
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
            url: bsApiSmr + ajax.url.post_create_booking,
            // data: objectify_form(form.serializeArray()),
            // data: form.serialize(),
            data: $.param(data),
            beforeSend: function() { },
            success: function (data) {
                if (data.status == "success") {
                    window.location.href = window.location.href.replace("find-room", "index");
                } else {
                    setTimeout(() => {
                        var msg = "Failed to save booking, please try again !!!";
                        if (data.msg != "") {
                            msg = data.msg;
                        }
                        showNotification(msg, "error");
                    }, 500);
                }
            },
            error: function(err) {
                var msg = "Failed to save booking, please try again !!!";
                showNotification(msg, "error");
            },
            complete: function(data) { }
        });
    }
});

// --------------------------------------------- //
/* $(document).ready(function () {
    $(".selectpicker").selectpicker();
    $(".selectpicker").selectpicker("refresh");

    // Inisialisasi daterangepicker untuk "When" dan "Until"
    $("#when-date").daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: { format: "YYYY-MM-DD" },
    });

    $("#until-date").daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: { format: "YYYY-MM-DD" },
    });

    $("#start-time, #end-time").timepicker({
        timeFormat: "H:i",
        step: 5,
        minTime: "00:00",
        maxTime: "23:55",
        dynamic: false,
        dropdown: true,
        scrollbar: true,
    });

    $("#building").select2({
        placeholder: "-- Select Building --",
        allowClear: true,
        width: "100%",
    });
    $("#seats").select2({
        placeholder: "-- Select Seats --",
        allowClear: true,
        width: "100%",
    });
    $("#facilities").select2({
        allowClear: true,
        width: "100%",
    });
    $("#category").select2({
        placeholder: "-- Select Meeting Category --",
        allowClear: true,
        width: "100%",
    });

    // Ambil tipe meeting dari sessionStorage
    const meetingType = sessionStorage.getItem("meetingType");
    const isTraining = meetingType !== "training";

    // Fungsi untuk mengatur visibilitas dan akses field "Until"
    function toggleUntilField() {
        const $untilField = $("#until-date").closest(".row");
        const $untilDisplay = $("#meeting-until").prev("label").addBack();
        if (isTraining) {
            $untilField.hide(); // hide field until di create meeting
            $("#until-date").prop("disabled", true); // nonaktifkan input
            $untilDisplay.hide(); // hide label dan value di meeting-details
        } else {
            $untilField.show(); // tampilkan until di field meeting
            $("#until-date").prop("disabled", false); // Aktifkan input
            $untilDisplay.show(); // Tampilkan label dan value di meeting-details
        }
    }

    // Panggil fungsi saat halaman dimuat
    toggleUntilField();

    const populateRooms = () => {
        let selectedBuilding = $("#building").val();
        let selectedWhenDate = $("#when-date").val();
        let selectedUntilDate = isTraining ? null : $("#until-date").val(); // Abaikan Until jika training
        let selectedStartTime = $("#start-time").val();
        let selectedEndTime = $("#end-time").val();
        let selectedSeats = $("#seats").val();
        let selectedFacilities = $("#facilities").val() || [];

        let availableRooms = [
        {
            id: 101,
            building: "A",
            seats: 5,
            image: "../assets/img/rooms/room-3.jpg",
            facilities: ["Projector", "Whiteboard"],
            availableTimes: ["09:00", "14:00"],
            availableUntil: "2025-12-31", // Tanggal hingga kapan tersedia
        },
        {
            id: 102,
            building: "A",
            seats: 15,
            image: "../assets/img/rooms/room-1.jpg",
            facilities: ["Projector", "Microphone", "Video Conferencing"],
            availableTimes: ["07:00", "10:00"],
            availableUntil: "2025-12-31",
        },
        {
            id: 103,
            building: "A",
            seats: 20,
            image: "../assets/img/rooms/room-2.jpg",
            facilities: ["Projector", "Microphone"],
            availableTimes: ["11:00", "14:00"],
            availableUntil: "2025-12-31",
        },
        {
            id: 201,
            building: "B",
            seats: 5,
            image: "../assets/img/rooms/room-3.jpg",
            facilities: ["Video Conferencing", "Whiteboard"],
            availableTimes: ["09:00", "17:00"],
            availableUntil: "2025-12-31",
        },
        {
            id: 301,
            building: "C",
            seats: 25,
            image: "../assets/img/rooms/room-5.jpg",
            facilities: ["Video Conferencing", "Microphone", "Projector"],
            availableTimes: ["12:00", "17:00"],
            availableUntil: "2025-12-31",
        },
        ];

        let filteredRooms = availableRooms.filter((room) => {
        let buildingMatch =
            !selectedBuilding || room.building === selectedBuilding;
        let seatsMatch =
            !selectedSeats ||
            room.seats >= parseInt(selectedSeats.split("-")[0]);
        let facilitiesMatch =
            selectedFacilities.length === 0 ||
            selectedFacilities.every((facility) =>
            room.facilities.includes(facility)
            );
        let startTimeMatch =
            !selectedStartTime || selectedStartTime >= room.availableTimes[0];
        let endTimeMatch =
            !selectedEndTime || selectedEndTime <= room.availableTimes[1];
        return (
            buildingMatch &&
            seatsMatch &&
            facilitiesMatch &&
            startTimeMatch &&
            endTimeMatch
        );
        });

        let $roomList = $("#room-list");
        $roomList.empty();

        if (filteredRooms.length > 0) {
        filteredRooms.forEach((room) => {
            $roomList.append(`
        <li class="list-group-item" style="display: flex; align-items: center; justify-content: space-between; padding: 10px; border-radius: 10px; border: 1px solid #ddd; margin-bottom: 10px; box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1);">
            <img src="${
            room.image
            }" alt="Room Image" style="width: 80px; height: 80px; border-radius: 10px; object-fit: cover;">
            <div style="flex-grow: 1; margin-left: 10px;">
            <div style="font-weight: bold; font-size: 16px;">Room ${
                room.id
            }</div>
            <div style="color: #666; font-size: 14px;">Building: ${
                room.building
            }</div>
            <div style="color: #666; font-size: 14px;">Seats: ${
                room.seats
            }</div>
            <div style="color: #888; font-size: 12px; margin-top: 3px;">${room.facilities.join(
                ", "
            )}</div>
            </div>
            <button class="select-room-btn" data-room-id="${
            room.id
            }" style="background-color: #90EE90; color: white; border: none; padding: 8px 15px; width: 100px; height: 50px; min-width: 100px; min-height: 50px; border-radius: 20px; font-size: 12px; cursor: pointer; transition: background-color 0.3s;">
            Select Room
            </button>
        </li>
        `);
        });
        $("#room-container").show();
        } else {
        $("#room-container").hide();
        }
    };

    $(document).on("click", ".select-room-btn", function (e) {
        e.preventDefault();

        let selectedButton = $(".select-room-btn.selected");

        if (selectedButton.length) {
        selectedButton
            .css("background-color", "#90EE90")
            .text("Select Room")
            .removeClass("selected");
        }

        $(this)
        .css("background-color", "#1abc6b")
        .text("Selected ✅")
        .addClass("selected");

        let selectedRoomId = $(this).data("room-id");
        let availableRooms = [
        { id: 101, building: "A", seats: 5 },
        { id: 102, building: "A", seats: 15 },
        { id: 103, building: "A", seats: 20 },
        { id: 104, building: "A", seats: 10 },
        { id: 201, building: "B", seats: 5 },
        { id: 202, building: "B", seats: 15 },
        { id: 203, building: "B", seats: 15 },
        { id: 301, building: "C", seats: 25 },
        { id: 302, building: "C", seats: 10 },
        ];
        let selectedRoom = availableRooms.find(
        (room) => room.id === selectedRoomId
        );

        $("#selected-room").text(
        `Room ${selectedRoom.id} (Building ${selectedRoom.building}, Seats: ${selectedRoom.seats})`
        );
        let whenDate = $("#when-date").val();
        let untilDate = isTraining ? "" : $("#until-date").val(); // Kosongkan jika training
        let startTime = $("#create-meeting #start-time").val();
        let endTime = $("#create-meeting #end-time").val();

        $("#meeting-when").text(`${whenDate} ${startTime} - ${endTime}`);
        $("#meeting-until").text(untilDate ? `${untilDate} ${endTime}` : "");

        $("#create-meeting").hide();
        $("#meeting-details").removeAttr("hidden").show();
        $("html, body").animate({ scrollTop: 0 }, "fast");
    });

    $("#filter-btn").click(function (e) {
        e.preventDefault();
        $("#error-message").addClass("d-none");
        populateRooms();
    });

    let employees = [
        { name: "Bobby D", department: "Engineering" },
        { name: "John B", department: "Marketing" },
        { name: "Alice A", department: "HR" },
        { name: "Jack S", department: "Finance" },
        { name: "Stanley M", department: "Engineering" },
        { name: "Steve T", department: "Marketing" },
    ];
    let addedEmployees = [];

    function populateDropdown() {
        let internalAttendeeSelect = $("#internalAttendeeSelect");
        internalAttendeeSelect.empty();
        let availableEmployees = employees.filter(
        (emp) => !addedEmployees.includes(emp.name)
        );
        availableEmployees.forEach((emp) => {
        internalAttendeeSelect.append(
            `<option value="${emp.name}">${emp.name} - ${emp.department}</option>`
        );
        });
        $(".selectpicker").selectpicker("refresh");
    }
    populateDropdown();

    $(".increment, .decrement").click(function (e) {
        e.preventDefault();
        let countSpan = $(this).siblings(".count");
        let currentValue = parseInt(countSpan.text());
        if ($(this).hasClass("increment")) {
            countSpan.text(currentValue + 1);
        } else if ($(this).hasClass("decrement") && currentValue > 0) {
            countSpan.text(currentValue - 1);
        }
    });
}); */