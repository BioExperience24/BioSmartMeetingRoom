var intervalBookingCrt;
var enabledVar = [1, 0];
var enabledString = ["Disabled", "Enabled"];
var booleanString = ["False", "True"];
var gBuilding;
var gFloor;
var roomSelected = [];
var defaulticonroom = "bottom.png";
var bs = app.url.api;
var bsApp = app.url.app;
var urlicon = `${bsApp}assets/web/`;

var generalarea = $('.generalarea');
var displayinformationarea = $('.displayinformationarea');

var listicon = {
    'walking.png' : "Walking",
    'up.png' : "Up",
    'right.png' : "Right",
    'left.png' : "Left",
    'bottom.png' : "Bottom",
    'turn-right.png' : "Turn Right",
    'turn-left.png' : "Turn Left",
    'ladder-top.png' : "Ladder Top",
    'ladder-bottom.png' : "Ladder Bottom",
    'lift.png' : "Lift",
    'chair.png' : "Chair",
    'door.png' : "Door",
    'diagonal-bottom-right.png' : "Diagonal Bottom Right",
    'diagonal-bottom-left.png' : "Diagonal Bottom Left",
    'diagonal-right.png' : "Diagonal Left",
    'diagonal-left.png' : "Diagonal Right",
};

var gDisplayType = [
    { 'value': 'general', 'name': "General", 'description': '' },
    { 'value': 'allroom', 'name': "Display Informasi", 'description': '' },
];

var gTypeText = {
    general : 'General',
    allroom : 'Display Informasi',
}
var gRoom = [];

var path = "assets/file/display/";
var bgpath = path + "background/";
var signagepath = path + "signage/";
var groom = [];
var isOpenSidebar = false;

var selectedIconFinder = "";

var modules;

$(function() {
    moment.locale("en");
    init_load_data();
});

async function init_load_data() {
    try {
        await init_modules();
        await init_buildings();
        await init_building_floors();
        await getRoom();
    } finally {
        await init();

        intrTime();
        displayinformationarea.hide('fase');
    }
}

async function init_modules() {
    try {
        var data = await get_modules(
            "module_display",
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

async function init_buildings() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_buildings
        });

        if (data.status == "success")
        {
            gBuilding =  data.collection;
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

async function init_building_floors() {
    try {
        var data = await $.ajax({
            type: "Get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: bs + ajax.url.get_building_floors
        });

        if (data.status == "success")
        {
            gFloor =  data.collection;
        } else {
            var msg = "Your session is expired, login again !!!";
            showNotification('alert-danger', msg, 'top', 'center');
        }
    } catch (err) {
        // console.log(err);
        errorAjax;
    }
}

$(window).click(function(e) {
    var $target = $(e.target);
    // console.log($target, $target.closest('.iconroom'))
    if($target.closest('.iconroom').length > 0){

    }else{
        tippy.hideAll()

    }
    //Hide the menus if visible
});

var modalIconFinderInstance ;

function intrTime() {
    setInterval(
        function() {
            var tm = moment().format('HH:mm');
            $('#time1').html(tm);
        }, 500
    );
}

function tootipEnable() {
    $('[data-toggle="tooltip"]').tooltip({
        container: 'body'
    });

    //Popover
    $('[data-toggle="popover"]').popover();
}

function clickSubmit(id) {
    $('#' + id).click();

}

function initTable(selector) {
    selector.DataTable();
}

function clearTable(selector) {
    selector.DataTable().destroy();
}

function select_enable() {
    $('select').selectpicker("refresh");
    $('select').selectpicker("initialize");
}

function colorpicker() {
    $('.colorpicker').colorpicker({
        format: 'hex'
    });
}

function genegrateIconroom(value = ""){
    var ht =``;
    var v = value.split("#")
    for(var x in listicon){
        var ck = '';
        if(x == value){ck = "checked"}
        ht += `<input value="${x}" ${ck} onclick="onClickIconSelectedFinder($(this))" type="radio" id="id_iconroom_${x}" class="radio-col-red radio_iconroom" />
        <label for="id_iconroom_${x}">${listicon[x]}</label><br>`;
    }
    return ht;
}

function generateRoom(value = "") {
    var gehtml = "";
    let rooms = [];
    let buildingId = $("#id_crt_building").val();
    let building;
    if (buildingId != "") {
        building = gBuilding.find(building => building.encrypt == buildingId);
        if(building != undefined) {
            rooms = gRoom.filter(room => room.building_id == building.id);
        }
    }
    let selected = "";
    $.each(rooms, (index, item) => {
        selected = "";
        if (value != "") {
            selected = value == item.radid ? "selected" : "";
        }
        gehtml += `<option data-subtext="${item.building_name}" value="${item.radid}" ${selected}>${ item.name}</option>`;
        // if (item.background == null) {
        // }
    });
    /* $.each(gRoom, (index, item) => {
        gehtml += `<option data-subtext="${item.building_name}" value="${item.radid}" >${ item.name}</option>`;
        // if (item.background == null) {
        // }
    }) */
    return gehtml;
}

function reGenerateRooms() {
    var gehtml = "";
    let rooms = [];
    let buildingId = $("#id_crt_building").val();
    let building;
    if (buildingId != "") {
        building = gBuilding.find(building => building.encrypt == buildingId);
        if(building != undefined) {
            rooms = gRoom.filter(room => room.building_id == building.id);
        }
    }
    $.each(rooms, (index, item) => {
        gehtml += `<option data-subtext="${item.building_name}" value="${item.radid}" >${ item.name}</option>`;
        // if (item.background == null) {
        // }
    })
    $('#id_crt_select_room').html(gehtml);
    // $('#id_crt_room').html("");
    select_enable();
    ocRoom();
    return;
}

function reGenerateRoom() {
    var gehtml = "";
    let rooms = [];
    let buildingId = $("#id_crt_building").val();
    let building;
    if (buildingId != "") {
        building = gBuilding.find(building => building.encrypt == buildingId);
        if(building != undefined) {
            rooms = gRoom.filter(room => room.building_id == building.id);
        }
    }
    $.each(rooms, (index, item) => {
        gehtml += `<option data-subtext="${item.building_name}" value="${item.radid}" >${ item.name}</option>`;
        // if (item.background == null) {
        // }
    })
    $('#id_crt_room').html(gehtml);
    // $('#id_crt_select_room').html("");
    select_enable();
    return;
}

function generateBuilding(value = "") {
    var gehtml = "";
    $.each(gBuilding, (index, item) => {
        // var d = value == item.id ? "selected" : "";
        // gehtml += `<option ${d} value="${item.id}" >${ item.name}</option>`;
        var d = value == item.encrypt ? "selected" : "";
        gehtml += `<option ${d} value="${item.encrypt}" >${ item.name}</option>`;
    })
    return gehtml;
}

function generateFloor(buildingId = "", value = "") {
    var gehtml = "";

    if(buildingId == ""){
        $.each(gFloor, (index, item) => {
            // var d = value == item.id ? "selected" : "";
            // gehtml += `<option ${d} value="${item.id}" >${item.name}</option>`;
            var d = value == item.enc_id ? "selected" : "";
            gehtml += `<option ${d} value="${item.enc_id}" >${item.name}</option>`;
        })
    }else{
        $.each(gFloor, (index, item) => {
            /* if(item.building_id == buildingId){

                var d = value == item.id ? "selected" : "";
                gehtml += `<option ${d} value="${item.id}" >${item.name}</option>`;
            } */
            if(item.enc_building_id == buildingId){
                // var d = value == item.id ? "selected" : "";
                // gehtml += `<option ${d} value="${item.id}" >${item.name}</option>`;
                var d = value == item.enc_id ? "selected" : "";
                gehtml += `<option ${d} value="${item.enc_id}" >${item.name}</option>`;
            }
        })
    }
    
    return gehtml;
}

function generateRoomSelect(value = []) {

    var gehtml = "";
    $.each(gRoom, (index, item) => {

        var s = value.indexOf(item.radid) >= 0 ? 'selected' : '';

        gehtml += `<option ${s} data-subtext="${item.building_name}" value="${item.radid}" >${item.name}</option>`;
    })
    return gehtml;
}

function generateDisplayType(value = "") {
    var html = ``;
    for (var x in gDisplayType) {
        var s = value == gDisplayType[x].value ? 'selected' : '';
        html += `<option ${s} value='${gDisplayType[x].value}'>${gDisplayType[x].name}</option>`;
    }
    return html;
}

function enable_datetimepicker() {
    $('.input-group #daterangepicker').daterangepicker({
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
        init(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'))
        // console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
    });

}

function ocDisplayType(action = '') {
    var v = $('#id_crt_type').val();
        if (v == "general") {
            displayinformationarea.hide('fase');
            generalarea.show('fast');
            // $('#id_crt_background').attr("required", true)
            
        } else if (v == "allroom" || v == "receptionist") {
            generalarea.hide('fase');
            displayinformationarea.show('fast');
            // $('#id_crt_background').attr("required", false)

        }

        reGenerateRooms();
        reGenerateRoom();
}

function ocBuilding(action = '') {
        var v = $('#id_crt_building').val();
        var html = generateFloor(v, "");
        $('#id_crt_floor').html(html);
        select_enable();
        reGenerateRooms();
        reGenerateRoom();
}

function selectedBuilding(building = "", floor = "") {
        var v = building;
    var html = generateFloor(v, floor);
    $('#id_crt_floor').html(html)
    select_enable()
}
function findRoom(id = ""){
    var f  = null;
    for(var x in gRoom){
        if(gRoom[x].radid == id){
            f = gRoom[x];
            break
        }
    }
    return f;
}
function findSelectedRoom(id = ""){
    var f  = null;
    for(var x in roomSelected){
        if(roomSelected[x].room_id == id){
            f = roomSelected[x];
            break
        }
    }
    return f;
}
function ocRoom(action = ''){
    var selected = $('#id_crt_select_room').val();
    var tem = [];
    for(var i in selected){
        var item = selected[i];
        var dataroom = findRoom(item);
        // console.log(dataroom)
        var dataselectedroom = findSelectedRoom(item);
        if(dataselectedroom != null){
            tem.push(dataselectedroom);
        }else{
            tem.push({
                room_id : dataroom.radid,
                name : dataroom.name,
                distance : 0,
                icon : defaulticonroom,
            });
        }
    }

    roomSelected = JSON.parse(JSON.stringify(tem));
    var ht = generateTableDisplayInformation()
    $('.tbl-display-information').html(ht)
    if(modalIconFinderInstance != null){
        destroyTippy();
    }
}
function destroyTippy(){
    for(var x in modalIconFinderInstance){
        modalIconFinderInstance[x].destroy();
    }
}
function onClickIconSelectedFinder(t){
    var val = t.val();
    // console.log(val)
    // console.log(roomSelected)
    roomSelected[selectedIconFinder].icon = val;
    var ht = generateTableDisplayInformation()
    $('.tbl-display-information').html(ht);

}


function onClickIconFinder(t){
    var index = t.data('index');
    var icon =roomSelected[index].icon || "";
    var cgicon = genegrateIconroom(icon)
    console.log(icon);
    if(modalIconFinderInstance != null){
        destroyTippy();
    }
    modalIconFinderInstance = tippy(document.querySelectorAll('#iconfinder_'+index),{
        theme: 'light',
        hideOnClick: false,
        allowHTML: true,
        trigger: 'focus',
        interactive: true,
        animation: 'fade',
        placement: 'auto',
        zIndex: 19999,
        content: `<div class="row" style="width: 300px;">
            <div class="col-xs-12">
                <h5>Icon</h5>
                ${cgicon}
            </div>
        </div>`,
    });
    modalIconFinderInstance[0].show()
    selectedIconFinder = index;
}
function generateTableDisplayInformation(){
    var html = ``;
    var m = 0;
    for(var x in roomSelected){
        m++;
        html += `<tr>`;
        html += `<td>${m}</td>`
        html += `<td>${roomSelected[x].name}</td>`
        html += `<td><input type="number" onkeyup="ochangeDistance($(this))" class="form-control" data-icon="${roomSelected[x].icon}" data-index="${x}" value="${roomSelected[x].distance}" ></td>`
        html += `<td><div data-index="${x}" id="iconfinder_${x}" onclick="onClickIconFinder($(this))" class="iconroom iconfinder"><img src="${urlicon}${roomSelected[x].icon}"></div></td>`
        html += `</tr>`
    }

    return html;

}

function ochangeDistance(t){
    var val = t.val();
    var index = t.data('index');
    if(roomSelected[index] == null){
        return;
    }
    var f = roomSelected[index];
    roomSelected[index].distance = val;


}

function init() {
    // var bs = $('#id_baseurl').val();
    $.ajax({
        // url: bs + "display/get/data",
        url: bs + ajax.url.get_room_displays,
        type: "GET",
        dataType: "json",
        beforeSend: function() {
            $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            if (data.status == "success") {
                groom = [];
                clearTable($('#tbldata'));
                var col = data.collection;
                var len = data.collection.length;
                var html = "";
                var numm = 0;
                groom = col;
                $.each(col, function(index, item) {
                    numm++;
                    var status = item.enable_signage == 1 ? "Enabled" : "Disabled";
                    var display_status = ckLastTime(item.hardware_lastsync);
                    var status_sync = statusSycn(item.status_sync - 0);
                    var enabled = item.enabled - 0;
                    var bg = "";
                    if (enabled == 0) {
                        bg = "background-color:lightgrey";
                    }
                    var roomname = "";
                    var roomnametooltip = "";
                    if (item.type == "general") {
                        roomname = item.room_name;
                        roomnametooltip = item.room_name;
                    } else {
                        var arrRoomSelect = [];
                        var arrRoomSelectToolTip = [];
                        if (item.room_select_data != null) {
                            for (var ri in item.room_select_data) {
                                if (ri < 2) {

                                    arrRoomSelect.push(`${item.room_select_data [ri].name}`);
                                }
                                arrRoomSelectToolTip.push(`${item.room_select_data [ri].name}`);
                            }
                            if (arrRoomSelectToolTip.length < 2) {
                                roomname = arrRoomSelect.join(',');
                                roomnametooltip = arrRoomSelectToolTip.join(',');
                            } else {
                                roomname = arrRoomSelect.join(',') + "...";
                                roomnametooltip = arrRoomSelectToolTip.join(',');
                            }
                        }
                    }

                    html += `<tr style="${bg}" 
                    data-id ="${item.id}" >`
                    html += '<td>' + numm + '</td>';
                    html +=  `<td><a class="aClickDetail" data-num ="${index}"  onclick="openDetailUpdate($(this))" >${item.display_serial}</a></td>`;
                    html += '<td data-toggle="tooltip" data-placement="top" title="" data-original-title="' + roomnametooltip + '" >' + roomname + '</td>';
                    // html += '<td><img src="' + bs + bgpath + item.background + '"  style="width:80px;height:auto;" ></td>';
                    html += '<td><img src="' + item.background + '"  style="width:80px;height:auto;" ></td>';
                    html += '<td><div style="width:25px;height:25px;border:2px solid #000;background:' + item.color_occupied + ' "></div></td>';
                    html += '<td><div style="width:25px;height:25px;border:2px solid #000;background:' + item.color_available + ' "></div></td>';
                    // html += '<td>'+status; +'</td>';
                    // html += `<td>${status_sync}</td>`;
                    html += `<td>${display_status}</td>`;

                    var enabled = item.enabled - 0;
                    var iconEnabled = 'close';
                    var colorEnabled = 'btn-warning';
                    var actionEnabled = 0;
                    if (enabled == 0) {
                        iconEnabled = 'done';
                        colorEnabled = 'btn-success';
                        actionEnabled = 1;
                    }

                    html += '<td>'; // 

                    html += `<button  onclick="openUpdate($(this))" 
                    data-id ="${item.id}" 
                    data-num ="${index}" 
                    type="button" 
                    class="btn btn-info waves-effect"><i class="material-icons">edit</i>
                    </button>`;
                    html += '&nbsp;'; // 
                    html += `<button  onclick="openEnabled($(this))" 
                    title="Click this for enable/disable display"
                    data-toggle="tooltip" data-placement="top"
                    data-action ="${actionEnabled}" 
                    data-id ="${item.id}" 
                    data-num ="${index}" 
                    type="button" 
                    class="btn ${colorEnabled} waves-effect"><i class="material-icons">${iconEnabled}</i>
                    </button>`;
                    html += '&nbsp;'; // 
                    html += `<button onclick="removeData($(this))" 
                    data-id ="${item.id}" 
                    data-num ="${index}" 
                    type="button" 
                    class="btn btn-danger waves-effect"><i class="material-icons">delete</i> 
                    </button>`;
                    html += '</td>'; // 

                    // html += '<td>'; // 
                    // html += '<button \
                    //         onclick="openSignage($(this))" \
                    //         data-room_id="'+item.room_id+'" \
                    //         data-num="'+index+'" \
                    //         type="button" class="btn btn-info waves-effect ">Sigange</button>';
                    // html += '</td>'; // 
                })

                $('#id_count_total').html(len);
                $('#id_count_total_license').html(len);
                $('#tbldata tbody').html(html);
                tootipEnable();
                // initTable($('#tbldata'));
            } else {
                var msg = "Your session is expired, login again !!!";
                showNotification('alert-danger', msg, 'top', 'center')
            }
            $('#id_loader').html('');
        },
        error: errorAjax
    });
}

function statusSycn(status) {
    if (status == 0) {
        return "Not Synchronized";
    } else if (status == 1) {
        return "Synchronized";
    } else if (status == 2) {
        return "Update Synchronized";
    }
}

function ckLastTime(datetime) {
    var now = moment();
    var ck = moment(datetime);
    // var diff =ck.diff(now);
    var diff = now.diff(ck);
    var durr = moment.duration(diff).asMinutes();
    if (durr > 3) {
        return "Disconnected"
    } else {
        return "Connected"
    }
    // console.log(durr)
}

function openSignage(t) {
    var bs = $('#id_baseurl').val();
    var id = t.data('room_id');
    var num = t.data('num');
    var display = groom[num];
    var pathfile = bs + signagepath + display.signage_media
    $('#id_si_old').attr('src', pathfile)
    $('#id_mdl_sigange').modal('show');

}


$('#frm_create').submit(function(e) {
    e.preventDefault();
    if($('#frm_create').valid() == false){
        return;
    }

    var id = $('#id_crt_room_id').val();
    Swal.fire({
        title: 'Confirmation',
        text: id != "" ? "Save display?" : "Create display?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Save !',
        cancelButtonText: 'Cancel !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            processCreateSubmit();
        }
    })
})
function processCreateSubmit(){
    var form = $('#frm_create')[0];
    // var bs = $('#id_baseurl').val();
    var f = new FormData(form);

    // const replacer = (key, value) => {
    //     if (typeof value === 'string' && !isNaN(value) && value.trim() !== "") {
    //         return parseInt(value, 10);
    //     }
    //     return value;
    // };

    const replacer = (key, value) => {
        if (key === 'distance' && typeof value === 'string') {
            return parseInt(value, 10);
        }
        return value;
    };

    f.append("roomSelected", JSON.stringify(roomSelected, replacer));

    $.ajax({
        // url: bs + "display/post/room",
        url: bs + ajax.url.post_create_room_display,
        type: "POST",
        dataType: "json",
        data: f,
        contentType: false,
        cache: false,
        processData: false,
        beforeSend: function() {
            loadingg('Please wait ! ', 'Loading . . . ');
            $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            Swal.close();
            if (data.status == "success") {
                $('#frm_create')[0].reset();
                $('#id_mdl_create').modal('hide');
                swalShowNotification('alert-success', data.msg, 'top', 'center')
                init();
            } else {
                swalShowNotification('alert-danger', data.msg, 'top', 'center')
            }
            $('#id_loader').html('');
        },
        error: errorAjax
    })
}
$('#frm_update').submit(function(e) {
    e.preventDefault();
    var form = $('#frm_create').serialize();
    var bs = $('#id_baseurl').val();
    $.ajax({
        url: bs + "display/post/room/update",
        type: "POST",
        dataType: "json",
        data: new FormData(this),
        contentType: false,
        cache: false,
        processData: false,
        beforeSend: function() {
            loadingg("Please Wait", "Loading . . . ");
            $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            $('#id_loader').html('');
            Swal.close();
            if (data.status == "success") {
                $('#frm_update')[0].reset();
                init();
                $('#id_mdl_update').modal('hide');
                swalShowNotification('alert-success', data.msg, 'top', 'center')
            } else {
                swalShowNotification('alert-danger', data.msg, 'top', 'center')
            }
        },
        error: errorAjax
    })
})
$('#frm_signage').submit(function(e) {
    e.preventDefault();
    // var form = $('#frm_signage').serialize();
    var bs = $('#id_baseurl').val();
    $.ajax({
        url: bs + "display/post/signage",
        type: "POST",
        dataType: "json",
        data: new FormData(this),
        contentType: false,
        cache: false,
        processData: false,
        beforeSend: function() {
            loadingg("Please Wait", "Loading . . . ");
            $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            $('#id_loader').html('');
            Swal.close();
            if (data.status == "success") {

                $('#frm_signage')[0].reset();
                init();
                $('#id_mdl_sigange').modal('hide');
                $('#id_si_old')[0].pause();
                showNotification('alert-success', data.msg, 'top', 'center')
            } else {
                $('#id_si_old')[0].pause();
                Swal.fire(
                    'Upload failed !!!',
                    data.msg,
                    'question'
                )
                showNotification('alert-danger', data.msg, 'top', 'center')
            }

        },
        error: errorAjax
    })
})

function getRoom() {
    // var bs = $('#id_baseurl').val();
    $.ajax({
        // url: bs + "display/get/data-room",
        url: bs + ajax.url.get_room_room_displays,
        type: "GET",
        dataType: "json",
        beforeSend: function() {
            // $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            Swal.close()
            if (data.status == "success") {
                gRoom = data.collection;

            } else {
                $('#id_loader').html('');

            }
        },
        error: errorAjax
    });
}

function openDetailUpdate(t) {
    var _this = this;
    var $target = $(_this.target);
    var $overlay = $('.overlay');
    var $sidebar = $('#rightsidebar');
    
    $sidebar.addClass('openside');
    $overlay.fadeIn();
    var bs = $('#id_baseurl').val();
    var id = t.data('id');
    var num = t.data('num');

    var display = groom[num];
    var bname = display.building_name || "-";
    var fname = display.floor_name || "-";

    console.log(display.type);
    if(display.type == "allroom"){
        displayinformationarea.show('fast');
        generalarea.hide('fast');
    }else if(display.type == "general") {
        displayinformationarea.hide('fast');
        generalarea.show('fast');
    }

    $('#side_paircode').text(display.display_serial || "-" );
    $('#side_name').text(display.name || "");
    $('#side_desc').text(display.description || "-");
    $('#side_type').text(gTypeText[display.type]);
    $('#side_building').text(bname);
    $('#side_floor').text(fname); 
    $('#side_room').text(display.room_name || "-");
    var rsd = display.room_select_data;
    var enable = display.enabled == "1" ? "Enabled" : "Disabled";
    var h =``;
    $.each(rsd, function (index, room) { 
        // var separator = index == 0 ? "" : ", ";
        // h+= `${separator}${room.name}`
        h+= `<button class="btn bg-blue waves-effect waves-light pull-right" style="margin: 5px 0px;">${room.name}</button>`
    });
    $('#side_room_selected').html(h);
    $('#side_status').text(enable);
    $('#side_disable').text(display.disable_msg || "-");
    $('#side_hardwareid').text(display.hardware_uuid || "-");
    $('#side_hardwareinfo').text(display.hardware_info || "-");
    if(display.hardware_lastsync == null){
        $('#side_sync').text(" - ");
        moment(display.hardware_lastsync).format('YYYY-MM-DD')
    }else{
        var m =  moment(display.hardware_lastsync).format('YYYY-MM-DD H:i:s')
        $('#side_sync').text(m);
    }
    setTimeout(function(){
        isOpenSidebar = true;
    }, 500)



}
$(window).click(function (e) {
    var $target = $(e.target);
    if(isOpenSidebar){
            var $overlay = $('.overlay');
            var $sidebar = $('#rightsidebar');
            if($target.closest(".openside").hasClass('openside')){

            }else{
                isOpenSidebar = false;
                $overlay.fadeOut();
                    $sidebar.removeClass('openside');
            }
    }
});

function createData() {

    $('#id_crt_room_id').val("");
    var gehtml = generateRoom("");
    var htmlSelectRoom = generateRoom("");
    var htmlDisplayType = generateDisplayType("");
    var htmlBuilding = generateBuilding("");
    var en = '';
    for (var x in enabledVar) {
        var vdd = enabledVar[x]
        en += '<option value="' + vdd + '" >' + enabledString[vdd] + '</option>';
    }
    $('#id_crt_enable_signage').html(en);
    $('#id_crt_room').html(gehtml);
    $('#id_crt_type').html(htmlDisplayType);
    $('#id_crt_select_room').html(htmlSelectRoom);
    $('#id_crt_floor').html("");
    $('#id_crt_building').html(htmlBuilding);
    select_enable();
    colorpicker();
    displayinformationarea.hide('fast');
    $('#id_crt_background').attr("required",true);
    $('#id_mdl_create').modal('show');
}
    
function openUpdate(t) {
    $('#id_crt_background').attr("required",false);
    var bs = $('#id_baseurl').val();
    var id = t.data('id');
    var num = t.data('num');
    var display = groom[num];
    $('#id_crt_room_id').val(id);
    // console.log(display)
    $('#id_crt_room_id').val(id);
    $('#id_crt_color_occupied').val(display.color_occupied)
    $('#id_crt_color_available').val(display.color_available)
    var room_select = display.room_select == null ? "" : display.room_select;
    var sp_room_select = room_select.split("##");
    var htmlDisplayType = generateDisplayType(display.type);
    var htmlBuilding = generateBuilding(display.building_id);
    selectedBuilding(display.building_id, display.floor_id)
    $('#id_crt_building').html(htmlBuilding);
    var gehtml = generateRoom(display.room_id);
    var htmlSelectRoom = generateRoomSelect(sp_room_select);
    
    var en = '';
    for (var x in enabledVar) {
        var vdd = enabledVar[x]
        var selc = vdd == display.enable_signage ? "selected" : "";
        en += `<option ${selc} value="${vdd}" >${enabledString[vdd]}</option>`;
    }
    // console.log(display.type)
    if(display.type == "allroom"){
        displayinformationarea.show('fast');
        generalarea.hide('fast');
    }else if(display.type == "general") {
        displayinformationarea.hide('fast');
        generalarea.show('fast');
    }
    $('#id_crt_enable_signage').html(en);
    $('#id_crt_room').html(gehtml);
    $('#id_crt_type').html(htmlDisplayType);
    $('#id_crt_select_room').html(htmlSelectRoom);
    $('#id_crt_room_id').val(id);
    $('#id_crt_displayserial').val(display.display_serial);
    $('#id_crt_name').val(display.name);
    $('#id_crt_description').val(display.description);
    // ocDisplayType("upd");
    colorpicker();
    select_enable();
    roomSelected = display.room_select_data;

    ocRoom();

    $('#id_mdl_create').modal('show');

    // $overlay.fadeIn();


}

function openEnabled(t) {
    // var bs = $('#id_baseurl').val();
    var id = t.data('id');
    var num = t.data('num');
    var action = t.data('action') - 0;
    var display = groom[num];
    var serial = display.display_serial;
    var text = "Are you sure you want disabled this display?";
    var confText = "Disabled";
    var html = ``;
    if (action == 1) {
        confText = "Enabled";
        html = "<p>Are you sure you want enabled this display?<p>";
        html += `<input type="hidden" id="id_enable_text" class="swal2-input" placeholder="Disable Messages">`;
    } else if (action == 0) {
        confText = "Disabled";
        html = "<p>Are you sure you want disabled this display?<p>";
        html += `<input type="text" id="id_enable_text" class="swal2-input" placeholder="Disable Messages">`;
    }
    Swal.fire({
        title: 'Confirmation',
        // text: text,
        type: "warning",
        html: html,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: confText,
        cancelButtonText: 'Cancel !',
        reverseButtons: true
    }).then((result) => {
        var enable_text = $('#id_enable_text').val();
        if (result.value) {
            // var bs = $('#id_baseurl').val();
            $.ajax({
                // url: bs + "display/post/enable",
                url: bs + ajax.url.post_change_status_room_display,
                type: "POST",
                data: {
                    id: id,
                    action: action,
                    disable_msg: enable_text
                },
                dataType: "json",
                beforeSend: function() {
                    loadingg('Please Wait', "Loading . . .");
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success: function(data) {
                    Swal.close();
                    $('#id_loader').html('');
                    if (data.status == "success") {
                        init();
                        swalShowNotification('alert-success', `Success change display status ${serial}`, 'top', 'center')
                    } else {
                        swalShowNotification('alert-danger', "Data not found", 'bottom', 'left')
                    }
                },
                error: errorAjax,
            })
        } else {

        }
    })
}



function removeData(t) {
    // var bs = $('#id_baseurl').val();
    var id = t.data('id');
    var num = t.data('num');
    var display = groom[num];

    var serial = display.display_serial;
    var form = {
        id: id
    }

    var text = `Are you sure you want delete this display ${serial}?`;
    Swal.fire({
        title: 'Confirmation',
        text: text,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Delete !',
        cancelButtonText: 'Cancel !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            // var bs = $('#id_baseurl').val();
            $.ajax({
                // url: bs + "display/post/delete",
                url: bs + ajax.url.post_delete_room_display,
                type: "POST",
                data: form,
                dataType: "json",
                beforeSend: function() {
                    loadingg('Please Wait', "Loading . . .");
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success: function(data) {
                    Swal.close();
                    $('#id_loader').html('');
                    if (data.status == "success") {
                        swalShowNotification('alert-success', "Succes deleted display " + serial, 'top', 'center')
                        init();
                    } else {
                        swalShowNotification('alert-danger', "Data not found", 'bottom', 'left')
                    }
                },
                error: errorAjax,
            })
        } else {

        }
    })

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

function swalShowNotification(icon, title, loc = "", loc2 = "") {
    var ic = "";
    if (icon == "alert-success") {
        ic = "success";
    } else if (icon == "alert-danger") {
        ic = "danger";
    } else if (icon == "alert-warning") {
        ic = "warning";
    } else if (icon == "alert-info") {
        ic = "info";
    }
    Swal.fire(
        title,
        '',
        ic
    )
}

function errorAjax(xhr, ajaxOptions, thrownError) {
    $('#id_loader').html('');
    Swal.close();
    if (ajaxOptions == "parsererror") {
        var msg = "Status Code 500, Error Server bad parsing";
        showNotification('alert-danger', msg, 'bottom', 'left')
    } else {
        var msg = "Status Code " + xhr.status + " Please check your connection !!!";
        showNotification('alert-danger', msg, 'bottom', 'left')
    }
}

function setButtonWavesEffect(event) {
    $(event.currentTarget).find('[role="menu"] li a').removeClass('waves-effect');
    $(event.currentTarget).find('[role="menu"] li:not(.disabled) a').addClass('waves-effect');
}

$('#id_mdl_create').on('hide.bs.modal', function (event) {
    $(':input','#frm_create')
    .not(':button, :submit, :reset, :hidden')
    .val('')
    .prop('checked', false)
    .prop('selected', false);

    $("#id_crt_color_occupied").val("#000000");
    $("#id_crt_color_available").val("#000000");
    // select_enable();
});