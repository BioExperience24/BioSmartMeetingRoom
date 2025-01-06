var typeRoom = [{ 'text': 'Single Room', 'value': 'single' }, { 'text': 'Merge Room', 'value': 'merge' }];
var bs = $('#id_baseurl').val();
var pathImage = bs + "assets/file/room/";
var defaultImage = "default.jpeg";
var uploadimageCrt = false;
var gAutomation = [];
var gFacility = [];
var assetsImageUrl = "";
var enabledRoom = ["Enabled", "Disabled"];
var building = $('#id_building').val();
var floor = $('#id_floor').val();
var room_for_usage = $('#id_room_for_usage').val();
var gBuilding = JSON.parse(building);
var gFloor = JSON.parse(floor);
var dataRoomIntegratonList = [];
var dataRoomIntegratonType = "";
var dataRoomIntegratonId = "";
var dataRoomIntegratonCurrent = {};
var gUserApproval = approvalUser();
var gUserPermission = permissionUser();
var gRoomUserCheckin = roomUserCheckin();
var count_room = 0;
var gInitRoom = [];
var gRoomForUsageGenerate = []; // [{room_id,room_usage_id, min_cap}]
var gUpdatedData = []; // [{room_id,room_usage_id, min_cap}]


var gMaximumDurationMeeting = [
    {value:60,name: "1 hrs"},
    {value:120,name: "2 hrs"},
    {value:180,name: "3 hrs"},
    {value:240,name: "4 hrs"},
    {value:300,name: "5 hrs"},
    {value:360,name: "6 hrs"},
    {value:420,name: "7 hrs"},
    {value:480,name: "8 hrs"},
    {value:540,name: "9 hrs"},
    {value:600,name: "10 hrs"},
    {value:660,name: "11 hrs"},
    {value:720,name: "12 hrs"},
    {value:0,name: " Unlimited"},
];

var gMinimumDurationMeeting = [
    {value:15,name: "15 mins"},
    {value:30,name: "30 mins"},
    {value:45,name: "45 mins"},
    {value:60,name: "1 hrs"},
];

var gReleaseTimeout = [
    {value:5,name: "5 mins"},
    {value:10,name: "10 mins"},
    {value:15,name: "15 mins"},
    {value:20,name: "20 mins"},
    {value:25,name: "25 mins"},
    {value:30,name: "30 mins"},
];

var gAdvanceMeeting = [
    {value:1,name: "1 days"},
    {value:3,name: "3 days"},
    {value:7,name: "7 days"},
    {value:14,name: "14 days"},
    {value:30,name: "30 days"},
    {value:60,name: "60 days"},
    {value:90,name: "90 days"},
    {value:120,name: "120 days"},
    {value:150,name: "150 days"},
    {value:180,name: "180 days"},
    {value:210,name: "210 days"},
    {value:240,name: "240 days"},
    {value:270,name: "270 days"},
    {value:300,name: "300 days"},
    {value:330,name: "330 days"},
    {value:365,name: "365 days"},
];

var dataTB;
$('[data-toggle="tooltip"]').tooltip({
        container: 'body'
});

function roomUserCheckin() {
    var c = [];
    var stringData = $('#id_room_user_checkin').val();
    try {
        var coldata = JSON.parse(stringData);
        c = coldata;
    } catch (error) {}
    return c;
}
function selectGlobal(list = [], key = "",value ){
    var html = ``;
    for(var x in list){
        var s = "";
        if(key != ""){
            s = list[x][key] == value? "selected" : '';
        }

        html += `<option ${s} value="${list[x].value}">${list[x].name}</option>`;
    }
    return html;
}

function approvalUser() {
    var c = [];
    var apUserString = $('#id_user_approval').val();
    try {
        var apUser = JSON.parse(apUserString);
        for (var x in apUser) {
            var acc = apUser[x].access_id;
            var spAcc = acc.split("#");
            if (spAcc.includes("4")) {
                c.push(apUser[x]);
            }
        }
    } catch (error) {}

    return c;
}
function permissionUser() {
    var c = [];
    var apUserString = $('#id_user_permission').val();
    try {
        var apUser = JSON.parse(apUserString);
        for (var x in apUser) {
            var acc = apUser[x].access_id;
            c.push(apUser[x]);
        }
    } catch (error) {}

    return c;

}

try {
    var gRoomForUsage = []
    var gRoomForUsage_raw = JSON.parse(room_for_usage);
    for (var xi in gRoomForUsage_raw) {
        gRoomForUsage.push(gRoomForUsage_raw[xi]);
    }

} catch (ee) {
    var gRoomForUsage = []

}
var gDatalistRoom = [];


$(function() {
    init();
    initSingle();
    getAutomation();
    getFacility();
    intrTime();
    initDrop();
})
function generateTableUsage(){
    var html =  ``;
    var n=0
    for(var x in gRoomForUsageGenerate){
        n++;
        var xxr = gRoomForUsageGenerate[x];
        var srUsage = getSinglegRoomForUsage(xxr.room_usage_id)
        if(srUsage == null){continue;}
        html += `<tr>`;
        html += `<td>${n}</td>`;
        html += `<td>${srUsage.name}</td>`;
        html += `<td><input type="number" name="room_usage_detail" data-num="${x}" data-id="${xxr.room_usage_id}" onkeyup="onTypeCapTableUsage($(this))" value="${xxr.min_cap}"></td>`;
        html += `</tr>`;
    }
    return html;
}
function onTypeCapTableUsage(t){
    var s = t.val();
    var num = t.data('num');
    var id = t.data('id');
    var r = getSinglegRoomForUsage(id);
    // if(r == null){continue;}
    gRoomForUsageGenerate[num].min_cap = s;


}
function ocCrtRoomForUsage() {
    var valuee = $('#id_crt_adv_room_for_usage').val();
    var coll = [];
    for (var i in valuee) {
        var find = false;
        //var dataSelected = valuee[i];
        //for (var x in gRoomForUsageGenerate) {
        //    if (gRoomForUsageGenerate[x].room_usage_id == dataSelected) {
        //        coll.push(gRoomForUsageGenerate[x]);
        //        find = true;
        //        break;
        //    } 
        //}

        //console.log(gRoomForUsageGenerate)
        //if (find == false) {
            coll.push({
                room_usage_id: valuee[i],
                min_cap: 0,
            })
        //}

    }
    gRoomForUsageGenerate = coll;
    var html = generateTableUsage();
    $('#id_crt_adv_tbl_room_for_usage tbody').html(html)
}
function ocEdtRoomForUsage() {
    var valuee = $('#id_edt_adv_room_for_usage').val();
    var coll = [];
    for (var i in valuee) {
        var find = false;
        var dataSelected = valuee[i];
        for (var x in gRoomForUsageGenerate) {
            if (gRoomForUsageGenerate[x].room_usage_id == dataSelected) {
                coll.push(gRoomForUsageGenerate[x]);
                find = true;
                break;
            } else {

            }
        }
        if (find == false) {
            coll.push({
                room_id: gUpdatedData.id,
                room_usage_id: valuee[i],
                min_cap: 0,
            })
        }

    }
    gRoomForUsageGenerate = coll;
    var html = generateTableUsage();
    $('#id_edt_adv_tbl_room_for_usage tbody').html(html)
}
function getSinglegRoomForUsage(value){
    var r = null;
    for (var xi in gRoomForUsage) {
        if(value == gRoomForUsage[xi].id){
            r = gRoomForUsage[xi];
            break;
        }
    }
    return r;
}


function clickSubmit(id) {
    $('#' + id).click();
}

function intrTime() {
    setInterval(
        function() {
            var tm = moment().format('HH:mm A');
            $('#time1').html(tm);
        }, 500
    );
}
function initDrop() {
    var drDestroy = $('.dropify').dropify();
    drDestroy = drDestroy.data('dropify')
    // drDestroy.isDropified()
    drDestroy.destroy();
    drDestroy.init();
}

function clickEnableActionAdvanceEdit() {

}
$('#id_crt_adv_is_config_setting_enable').change(function () {
    if (this.checked) {
        advanceCrtRoomEnabled()
    } else {
        advanceCrtRoomDisabled()
    }
    select_enable()

});
$('#id_edt_adv_is_config_setting_enable').change(function () {

    if (this.checked) {
        advanceEdtRoomEnabled()
    } else {
        advanceEdtRoomDisabled()
    }
    select_enable()

});
$('#id_edt_adv_is_enable_approval').change(function () {
    if (this.checked) {
        $('#id_edt_adv_config_approval_user').removeAttr("disabled");
    } else {
        $('#id_edt_adv_config_approval_user').attr('disabled', true);
    }
    select_enable()
});
$('#id_crt_adv_is_enable_approval').change(function () {
    if (this.checked) {
        $('#id_crt_adv_config_approval_user').removeAttr("disabled");
    } else {
        $('#id_crt_adv_config_approval_user').attr('disabled', true);
    }
    select_enable()
});
$('#id_crt_is_enable_permission').change(function () {
    if (this.checked) {
        $('#id_crt_adv_config_permission_user').removeAttr("disabled");
    } else {
        $('#id_crt_adv_config_permission_user').attr('disabled', true);
    }
    select_enable()
});
$('#id_crt_adv_is_enable_permission').change(function () {
    if (this.checked) {
        $('#id_crt_adv_config_permission_user').removeAttr("disabled");
    } else {
        $('#id_crt_adv_config_permission_user').attr('disabled', true);
    }
    select_enable()
});
$('#id_edt_adv_is_enable_permission').change(function () {
    if (this.checked) {
        $('#id_edt_adv_config_permission_user').removeAttr("disabled");
    } else {
        $('#id_edt_adv_config_permission_user').attr('disabled', true);
    }
    select_enable()
});

$('#id_crt_adv_is_enable_checkin').change(function () {
    if (this.checked) {
        $('#id_crt_adv_config_permission_checkin').removeAttr("disabled");
        $('#id_crt_adv_is_realease_checkin_timeout').removeAttr("disabled");
        if ($('#id_crt_adv_is_realease_checkin_timeout')[0].checked) {
            $('#id_crt_adv_config_release_room_checkin_timeout').removeAttr('disabled');
        }
    } else {
        $('#id_crt_adv_is_realease_checkin_timeout').attr('disabled', true);
        $('#id_crt_adv_config_permission_checkin').attr('disabled', true);
    }
    select_enable()
});

$('#id_edt_adv_is_enable_checkin').change(function () {
    if (this.checked) {
        $('#id_edt_adv_config_permission_checkin').removeAttr("disabled");
        $('#id_edt_adv_is_realease_checkin_timeout').removeAttr("disabled");
        if ($('#id_edt_adv_is_realease_checkin_timeout')[0].checked) {
            $('#id_edt_adv_config_release_room_checkin_timeout').removeAttr('disabled');
        }
    } else {
        $('#id_edt_adv_is_realease_checkin_timeout').attr('disabled', true);
        $('#id_edt_adv_config_permission_checkin').attr('disabled', true);
    }
    select_enable()
});
$('#id_crt_adv_is_realease_checkin_timeout').change(function () {
    if (this.checked) {
        $('#id_crt_adv_config_release_room_checkin_timeout').removeAttr("disabled");
    } else {
        $('#id_crt_adv_config_release_room_checkin_timeout').attr('disabled', true);
    }
    select_enable()
});
$('#id_edt_adv_is_realease_checkin_timeout').change(function () {
    if (this.checked) {
        $('#id_edt_adv_config_release_room_checkin_timeout').removeAttr("disabled");
    } else {
        $('#id_edt_adv_config_release_room_checkin_timeout').attr('disabled', true);
    }
    select_enable()
});







$("#ck_all").on( "click", function(e) {
    if ($(this).is( ":checked" )) {
         dataTB.rows(  ).select();        
    } else {
         dataTB.rows(  ).deselect(); 
    }
    // console.log(dataTB.rows({selected:true}).data().length)
});


function getModule() {
    var modules = $('#id_modules').val();
    return JSON.parse(modules)
}
function initTable(selector) {
    dataTB  = selector.DataTable({
    "scrollX":        true,
    "scrollCollapse": true,
    "fixedHeader":    true,
    paging:           false,
    // searching:        false,
    // bFilter :         false,
    info:             false,
    // scrollResize:     true,
    order: [[ 1, "asc" ]],
    // lengthMenu: [[5, 10, 20, 100,-1], [5, 10, 20,100, 'ALL']],
    fixedColumns: {
        leftColumns: 2,
        rightColumns: 1
    },
    columnDefs: [
        {
            orderable: false,
            className: 'select-checkbox',
            targets: 0,
            searchable: false,
        },
        {
            orderable: true,
           
        },
    ],
    select: {
        style:    'multi',
        selector: 'td:first-child'
    },
    }).on('deselect.dt', function(e, dt, type, indexes) {
        // console.log(dataTB.rows({selected:true}).data().length)
       
        if(dataTB.rows({selected:true}).data().length < count_room ){
            $('#ck_all').attr("checked", false);
        }
    }).on('select.dt', function(e, dt, type, indexes) {
        console.log(dataTB.rows({selected:true}).data().length, count_room)
        // $('#ck_all').find(".filled-in")
        if(dataTB.rows({selected:true}).data().length == count_room ){
            $('#ck_all').attr("checked", true);
        }
    });

    $('#id_search').on( 'keyup', function () {
        // console.log(this.value)
        dataTB
            .columns( 3 )
            .search( this.value )
            .draw();
    } );
    $('#tbldata_wrapper').find('#tbldata_filter').hide()

}

function clearTable(selector) {
    if(dataTB == null){return;}
    dataTB.destroy();
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

function createData() {
    initDrop();
    $('#id_area_add_merge').hide();
    $('#id_mdl_create').modal('show');
    $('#id_crt_room').html('');
    $('#id_crt_devices').html('');

    var html_automation = "";
    html_automation += "<option value=''>Please choose a automation ...</option>";
    for (var i in gAutomation) {
        html_automation += "<option value=" + gAutomation[i].id + ">" + gAutomation[i].name + "</option>";
    }

    var html_building = "";
    html_building += "";
    for (var i in gBuilding) {
        html_building += "<option value=" + gBuilding[i].id + ">" + gBuilding[i].name + "</option>";
    }

    var html_floor = "";
    html_floor += "";
    for (var i in gFloor) {
        html_floor += "<option value=" + gFloor[i].id + ">" + gFloor[i].name + "</option>";
    }

    var html_facility = "";
    html_facility += "<option value=''>Please choose a facility ...</option>";
    for (var i in gFacility) {
        html_facility += "<option value='" + gFacility[i]['id'] + "'>" + gFacility[i]['name'] + "</option>";
    }

    var html_typeroom = "";
    html_typeroom += "";
    for (var i in typeRoom) {
        html_typeroom += "<option value=" + typeRoom[i].value + ">" + typeRoom[i].text + "</option>";
    }
    $('#id_crt_automation').html(html_automation);
    $('#id_crt_facility_room').html(html_facility);
    $('#id_crt_building_id').html(html_building);
    $('#id_crt_type_room').html(html_typeroom);
    $('#id_crt_floor_id').html(html_floor);

    //
    //
    //
    // ADVANCED
    //
    //var is_config_enable = input["is_config_setting_enable"] - 0;
    //var is_config_adv_approval = input["is_enable_approval"] - 0;
    //var is_config_adv_permission = input["is_enable_permission"] - 0;
    //var is_config_adv_checkin = input["is_enable_checkin"] - 0;
    //var is_config_adv_recurring = input["is_enable_recurring"] - 0;

    //var editRoomForUsage = input["room_data_usage"];

    //// console.log(input['is_config_setting_enable'] )
    //if (is_config_enable != 1) {
    //    advanceCrtRoomDisabled();
    //} else {
        //advanceCrtRoomEnabled();
    //}
    //var config_room_for_usage = input["config_room_for_usage"];
    //var sp_config_room_for_usage = config_room_for_usage.split(",");

    var html_adv_room_for_usage = "";
    for (var i in gRoomForUsage) {
        var rrrr_g = gRoomForUsage[i].name;
        var rrrr_g_id = gRoomForUsage[i].id;
        var s = "";
        //if (sp_config_room_for_usage.indexOf(rrrr_g_id) >= 0) {
        //    s = "selected";
        //}
        html_adv_room_for_usage +=
            "<option " + s + " value='" + rrrr_g_id + "'>" + rrrr_g + "</option>";
    }
    $("#id_crt_adv_room_for_usage").html(html_adv_room_for_usage);
    //

    //var html_adv_tbl_room_for_usage = "";
    //gRoomForUsageGenerate = editRoomForUsage;
    //if (is_config_enable == 1) {
        var html_adv_tbl_room_for_usage = generateTableUsage();
    //}
    $("#id_crt_adv_tbl_room_for_usage tbody").html(html_adv_tbl_room_for_usage);

    // Recurreing
    //if (is_config_adv_recurring == 1 && is_config_enable == 1) {
    //    $("#id_crt_adv_is_enable_recurring").attr("checked", true);
    //} else {
        $("#id_crt_adv_is_enable_recurring").attr("checked", true);
    //}

    // APPROVAL
    //
    //if (is_config_adv_approval == 1 && is_config_enable == 1) {
    //    $("#id_crt_adv_config_approval_user").removeAttr("disabled");
    //    $("#id_crt_adv_is_enable_approval").attr("checked", true);
    //} else {

    //if ($("#id_crt_adv_is_enable_checkin")[0].checked) {
    //    $("#id_crt_adv_config_permission_checkin").removeAttr("disabled");
    //}
    //if ($("#id_crt_adv_is_enable_permission")[0].checked) {
    //    $("#id_edt_adv_config_permission_user").removeAttr("disabled");
    //}
    //}
    var config_approval_user = ""
        //input["config_approval_user"] == null ? "" : input["config_approval_user"];
    var html_adv_approval_user = "";
    //var sp_config_approval_user = config_approval_user.split(",");
    for (var iu in gUserApproval) {
        var rrrr_g = gUserApproval[iu].employee_id;
        var rrrr_g_name = gUserApproval[iu].name;
        var s = "";
        //if (sp_config_approval_user.indexOf(rrrr_g) >= 0) {
        //    s = "selected";
        //}
        html_adv_approval_user +=
            "<option " + s + " value='" + rrrr_g + "'>" + rrrr_g_name + "</option>";
    }
    $("#id_crt_adv_config_approval_user").html(html_adv_approval_user);

    // PERMISSION
    //
    //if (is_config_adv_permission == 1 && is_config_enable == 1) {
    //    $("#id_crt_adv_config_permission_user").removeAttr("disabled");
    //    $("#id_crt_adv_is_enable_permission").attr("checked", true);
    //} else {
        //$("#id_crt_adv_config_permission_user").attr("disabled", true);
        //$("#id_crt_adv_is_enable_permission").attr("checked", false);
    //}

    var config_permission_user = ""
        //input["config_permission_user"] == null
        //    ? ""
        //    : input["config_permission_user"];
    var html_adv_permission_user = "";
    //var sp_config_permission_user = config_permission_user.split(",");
    for (var iu in gUserPermission) {
        var rrrr_g = gUserPermission[iu].id;
        var rrrr_g_name = gUserPermission[iu].name;
        var s = "";
        //if (sp_config_permission_user.indexOf(rrrr_g) >= 0) {
        //    s = "selected";
        //}
        html_adv_permission_user +=
            "<option " + s + " value='" + rrrr_g + "'>" + rrrr_g_name + "</option>";
    }
    $("#id_crt_adv_config_permission_user").html(html_adv_permission_user);
    //
    // CHECKIN PERMISSION
    //
    // $('#id_crt_adv_config_permission_end').removeAttr();
    var config_permission_checkin = ""
        //input["config_permission_checkin"] == null
        //    ? 0
        //    : input["config_permission_checkin"];
    //var config_permission_end = input["config_permission_end"];
    //var is_realease_checkin_timeout = input["is_realease_checkin_timeout"] - 0;
    // console.log(is_config_adv_checkin,is_config_enable, is_realease_checkin_timeout )
    //if (is_config_adv_checkin == 1 && is_config_enable == 1) {
    //    $("#id_crt_adv_config_permission_checkin").removeAttr("disabled");
    //    $("#id_crt_adv_is_enable_checkin").attr("checked", true);
    //    $("#id_crt_adv_is_realease_checkin_timeout").removeAttr("disabled");
    //    if (is_realease_checkin_timeout == 1) {
    //        $("#id_crt_adv_is_realease_checkin_timeout").attr("checked", true);
    //        $("#id_crt_adv_config_release_room_checkin_timeout").removeAttr(
    //            "disabled"
    //        );
    //    } else {
    //        $("#id_crt_adv_is_realease_checkin_timeout").attr("checked", false);
    //        $("#id_crt_adv_config_release_room_checkin_timeout").attr(
    //            "disabled",
    //            true
    //        );
    //    }
    //}
    //else {
        $("#id_crt_adv_is_enable_checkin").attr("checked", false);
        $("#id_crt_adv_config_permission_checkin").attr("disabled", true);
        $("#id_crt_adv_config_permission_end").removeAttr("disabled");
        $("#id_crt_adv_is_realease_checkin_timeout").attr("disabled", true);
    //}
    var html_adv_permission_checkin = "";
    var html_adv_permission_end_permission = "";
    // var sp_config_permission_user = config_permission_checkin.split(",");
    for (var iu in gRoomUserCheckin) {
        var rrrr_g = gRoomUserCheckin[iu];
        var s = ""
            //config_permission_checkin == rrrr_g.key ? "selected" : "";
        html_adv_permission_checkin +=
            "<option " +
            s +
            " value='" +
            rrrr_g.key +
            "'>" +
            rrrr_g.name +
            "</option>";
    }

    for (var iuen in gRoomUserCheckin) {
        var rrrr_g = gRoomUserCheckin[iuen];
        var s = ""
            //config_permission_end == rrrr_g.key ? "selected" : "";
        html_adv_permission_end_permission +=
            "<option " +
            s +
            " value='" +
            rrrr_g.key +
            "'>" +
            rrrr_g.name +
            "</option>";
    }
    $("#id_crt_adv_config_permission_checkin").html(html_adv_permission_checkin);
    $("#id_crt_adv_config_permission_end").html(
        html_adv_permission_end_permission
    );
    var html_adv_config_min_duration = selectGlobal(
        gMinimumDurationMeeting,
        "value"
        //,
        //input["config_min_duration"] - 0
    );
    var html_adv_config_max_duration = selectGlobal(
        gMaximumDurationMeeting,
        "value"
        //,
        //input["config_max_duration"] - 0
    );
    var html_adv_config_advance_booking = selectGlobal(
        gAdvanceMeeting,
        "value"
        //,
        //input["config_advance_booking"] - 0
    );
    var html_adv_config_release_room_checkin_timeout = selectGlobal(
        gReleaseTimeout,
        "value"
        //,
        //input["config_release_room_checkin_timeout"] - 0
    );

    $("#id_crt_adv_config_min_duration").html(html_adv_config_min_duration);
    $("#id_crt_adv_config_max_duration").html(html_adv_config_max_duration);
    $("#id_crt_adv_config_advance_booking").html(html_adv_config_advance_booking);
    $("#id_crt_adv_config_release_room_checkin_timeout").html(
        html_adv_config_release_room_checkin_timeout
    );


    initTypeRoom('add', 'id_crt_type_room', 'id_crt_merge_room');

    enable_datetimepicker();
    select_enable();
}

function openIntData(roomid = "", type = "") {

    $.ajax({
        url: bs + "room/get/integration",
        type: "POST",
        dataType: "json",
        data: {
            roomid: roomid,
            type: type,
        },
        beforeSend: function() {
            $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            if (data.status == "success") {
                var col = data.collection;
                $('#id_mdl_integration').modal('show');
                $('#id_mdl_integrationLabel').html("")
                if (col.type == "365") {
                    var html = `<option value="">Choose Microsoft 365 Room</option>`;

                    $('#id_mdl_integrationLabel').html("Microsoft 365 Room Integration")
                } else if (col.type == "google") {
                    var html = `<option value="">Choose Google Room</option>`;
                    $('#id_mdl_integrationLabel').html("Google Room Integration")
                }
                dataRoomIntegratonId = roomid;
                dataRoomIntegratonList = col.listRoom;
                dataRoomIntegratonCurrent = col.data;
                dataRoomIntegratonType = col.type;
                $('#id_int_type').val(dataRoomIntegratonType);
                if (dataRoomIntegratonCurrent != null) {
                    $('#id_int_id').val(dataRoomIntegratonCurrent.id);
                }
                $('#id_int_room_meeting').val(dataRoomIntegratonId);
                for (var x in dataRoomIntegratonList) {
                    var sel = "";
                    if (dataRoomIntegratonCurrent != null) {
                        if (dataRoomIntegratonCurrent.id == dataRoomIntegratonList[x].id) {
                            sel = "selected";
                        }
                    }
                    html += `<option ${sel} value="${dataRoomIntegratonList[x].id}">${dataRoomIntegratonList[x].displayName} - ${dataRoomIntegratonList[x].emailAddress}</option>`
                }
                $('#id_int_room').html(html)
                enable_datetimepicker()
                select_enable()
                // swalShowNotification('alert-success', data.msg,'top','center')
            } else {
                // swalShowNotification('alert-danger', data.msg,'top','center')
            }
            $('#id_loader').html('');
        },
        error: errorAjax
    })


    enable_datetimepicker()
    select_enable()
}

function submitIntegration() {
    var f = $('#frm_integration').serialize()
    var bs = $('#id_baseurl').val();
    $.ajax({
        url: bs + "room/post/integration",
        type: "POST",
        dataType: "json",
        data: f,
        beforeSend: function() {
            $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            if (data.status == "success") {
                $('#frm_integration')[0].reset();
                $('#id_mdl_integration').modal('hide');
                $('#id_mdl_integrationLabel').html("");
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


function getAutomation() {
    var bs = $('#id_baseurl').val();
    $.ajax({
        url: "api/Automation/GetAll",
        type: "GET",
        dataType: "json",
        beforeSend: function() {
            $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            if (data.status == "success") {
                gAutomation = data.collection;
            } else {
                var msg = "Your session is expired, login again !!!";
                showNotification('alert-danger', msg, 'top', 'center')
            }
            $('#id_loader').html('');
        },
        error: errorAjax
    })
}

function getFacility() {
    gFacility = [];
    var bs = $('#id_baseurl').val();
    $.ajax({
        url: "api/Facility/GetAll",
        type: "GET",
        dataType: "json",
        beforeSend: function() {
            $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            if (data.status == "success") {
                gFacility = [];
                // $.each(data.collection, function(index, item){
                //  gFacility.push(item.name);
                // })
                gFacility = data.collection
            } else {
                var msg = "Your session is expired, login again !!!";
                showNotification('alert-danger', msg, 'top', 'center')
            }
            $('#id_loader').html('');
        },
        error: errorAjax
    })
}

function initKindRoom(id_edt_kind_room, value) {

    var html = "";

    var listKindRoom = ["room", "noroom", "trainingroom", "specialroom"]


    $.each(listKindRoom, function (index, item) {
        var sel = item == value ? "selected" : "";
        html += '<option ' + sel + '  value="' + item + '" >' + item + '</option>'
    })

    $('#' + id_edt_kind_room).html(html);
    select_enable()
}

function initTypeRoom(action, id_type_room, id_merge_room, values) {
    // console.log($('#'+id_type_room).val());
    if ($('#' + id_type_room).val() == "") {
        if (action == "edit") {
            $('#id_area_edt_merge').hide('slow');
        } else {
            $('#id_area_add_merge').hide('slow');
        }

        return;
    }
    if ($('#' + id_type_room).val() == "single") {
        if (action == "edit") {
            $('#id_area_edt_merge').hide('slow');
        } else {
            $('#id_area_add_merge').hide('slow');
        }
        return;
    }
    if (values == "") {
        values = "null"
    }
    if (action == "edit") {
        $('#id_area_edt_merge').show('slow');
    } else {
        $('#id_area_add_merge').show('slow');
    }
    var bs = $('#id_baseurl').val();

    if (action == "edit") {

        $.ajax({
            url: "api/Room/GetRoomMerge/" + values,
            type: "GET",
            //data: {
            //    room_id: values
            //},
            //dataType: "json",
            beforeSend: function () { },
            success: function (data) {
                if (data.status == "success") {
                    var list = [];
                    for (var x in data.collection) {
                        list.push(data.collection[x].merge_room_id);
                    }

                    var html = "";
                    var nn = 0;
                    $.each(gDatalistRoom, function (index, item) {
                        var ft = list.includes(item.radid);
                        var sel = ft == true ? "selected" : "";
                        html += '<option ' + sel + '  value="' + item.radid + '" >' + item.name + '</option>'
                    })
                    $('#' + id_merge_room).html(html);
                    // enable_datetimepicker()
                    select_enable()

                } else {
                    var msg = "Your session is expired, login again !!!";
                    swalShowNotification('alert-danger', msg, 'top', 'center')
                }
            },
            error: errorAjax
        })
    } else {
        $.ajax({
            url: "api/Room/GetAllRoomMerge",
            type: "GET",
            dataType: "json",
            beforeSend: function () { },
            success: function (data) {
                if (data.status == "success") {
                    var list = [];
                    for (var x in data.collection) {
                        list.push(data.collection[x].merge_room_id);
                    }

                    var html = "";
                    var nn = 0;
                    $.each(gDatalistRoom, function (index, item) {
                        var ft = list.includes(item.radid);
                        var sel = ft == true ? "selected" : "";
                        html += '<option ' + sel + '  value="' + item.radid + '" >' + item.name + '</option>'
                    })
                    $('#' + id_merge_room).html(html);
                    // enable_datetimepicker()
                    select_enable()

                } else {
                    var msg = "Your session is expired, login again !!!";
                    swalShowNotification('alert-danger', msg, 'top', 'center')
                }
            },
            error: errorAjax
        })
    }
}

function init() {
    var bs = $('#id_baseurl').val();
    var modules = getModule();
    $.ajax({
        url:"api/Room/GetRoomData",
        type: "GET",
        dataType: "json",
        beforeSend: function() {
            $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            if (data.status == "success") {
                clearTable($('#tbldata'));
                var html = "";
                var nn = 0;
                $('#id_count_total').html(data.collection.length);
                count_room  = data.collection.length;
                gInitRoom = data.collection;
                $.each(data.collection, function(index, item) {
                    nn++;
                    var automation = item.is_automation == 0 ? "Unactive" : "Active";
                    var price = item.price == null || item.price == "" ? 0 : item.price;
                    var ra_name = item.ra_name == null ? "" : item.ra_name
                    html += '<tr data-id="'+item.radid+'">'
                    html += '<td data-id="'+item.radid+'"></td>';
                    html += '<td>' + nn + '</td>';
                    var imgp = item.image == "" || item.image == null ? defaultImage : item.image;
                    html += '<td> <a href="javascript:void(0);" class="thumbnail"><img src="' + bs + 'api/Room/GetRoomDetailView/' + imgp + '?h=80&noCache=false" style="height:64px;" class="img-responsive"></a></td>';
                    html += '<td style="width:150px;">' + item.name + '</td>';
                    html += '<td style="width:150px;">' + item.building_name + '</td>';
                    html += '<td>' + item.capacity + '</td>';
                    html += '<td>' + item.work_time + '</td>';
                    // html += '<td>'+item.work_day+'</td>';
                    // 
                    if (modules['price']['is_enabled'] == 1) {
                        html += '<td>' + numeral(price).format('$ 0,0.00'); + '</td>';
                    }
                    //if (modules['int_365']['is_enabled'] == 1 || modules['int_google']['is_enabled'] == 1) {
                    //    html += '<td>';
                    //    if (modules['int_365']['is_enabled'] == 1) {
                    //        var ms365 = item.config_microsoft == null ? "" : item.config_microsoft;
                    //        if (ms365 != "") {
                    //            html += `<button type="button" class="btn btn-default waves-effect">Microsoft 365</button>`;
                    //        }

                    //    }
                    //    if (modules['int_google']['is_enabled'] == 1) {
                    //        var msGoogle = item.config_google == null ? "" : item.config_google;
                    //        if (msGoogle != "") {
                    //            html += `<button type="button" class="btn btn-default waves-effect">Google</button>`;
                    //        }

                    //    }
                    //    html += '</td>';

                    //}
                    html += '<td>' + enabledRoom[item.is_disabled] + '</td>';
                    html += '<td>';
                     html += `<button 
                                 onclick="editData($(this))"   data-id="${item.id}"   data-name="${item.name}" 
                                 data-ra_id="${item.ra_id}" 
                                 type="button" class="btn btn-info waves-effect"><i class="material-icons">edit</i></button>&nbsp;`
                     html += `<button 
                                 onclick="removeData($(this))"   data-id="${item.id}" data-name="${item.name}" 
                                 data-ra_id="${item.ra_id}" 
                                 type="button" class="btn btn-danger waves-effect"><i class="material-icons">delete</i></button>&nbsp;`
                    
                    //if (modules['int_365']['is_enabled'] == 1 || modules['int_google']['is_enabled'] == 1) {
                    //    html += `&nbsp; <div class="btn-group">
                    //                <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                    //                   <i class="material-icons">link</i> <span class="caret"></span>
                    //                </button>
                    //            `;
                    //    html += `<ul class="dropdown-menu">`;
                    //    if (modules['int_365']['is_enabled'] == 1) {
                    //        html += `<li><a href="javascript:openIntData('${item.radid}','365');" class=" waves-effect waves-block">Connected to 365</a></li>`;
                    //    }
                    //    if (modules['int_google']['is_enabled'] == 1) {
                    //        html += `<li><a href="javascript:openIntData('${item.radid}','365');" class=" waves-effect waves-block">Connected to Google</a></li>`;
                    //    }

                    //    html += `</ul">`;
                    //    html += `</div">`;

                    //}

                    html += '</td>';
                    html += '</tr>';
                })
                $('#tbldata tbody').html(html);

                initTable($('#tbldata'));
            } else {

                var msg = "Your session is expired, login again !!!";
                swalShowNotification('alert-danger', msg, 'top', 'center')
            }
            $('#id_loader').html('');
        },
        error: errorAjax
    })
}

function initSingle() {
    var bs = $('#id_baseurl').val();
    var modules = getModule();
    $.ajax({
        url: "api/Room/GetSingle",
        type: "GET",
        dataType: "json",
        beforeSend: function() {},
        success: function(data) {
            gDatalistRoom = [];
            if (data.status == "success") {
                gDatalistRoom = data.collection;
            } else {

            }
        },
        error: errorAjax
    });
}


async function getRoomForUsageDetail(id) {
    try {
        const data = await $.ajax({
            url: "api/Room/GetConfigRoomForUsageByIdRoom/" + id,
            type: "GET",
            dataType: "json",
        });

        if (data.status === "success") {
            return data.collection;
        } else {
            console.error("Error: Unexpected response status", data.status);
            return [];
        }
    } catch (error) {
        console.error("Error in AJAX request:", error);
        return [];
    }
}
    


function editData(t) {
    var id = t.data('id');
    var bs = $('#id_baseurl').val();
    var modules = getModule();
    $('#frm_update')[0].reset();
    assetsImageUrl = "";
    $.ajax({
        url: "api/Room/GetRoomById/" + id,
        type: "GET",
        dataType: "json",
        beforeSend: function() {
            $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            // $('#id_edt_image2_old_div').html("")
            if (data.status == "success") {

                $('#id_panel_checkin_edt').hide();
                var input = data.collection;
                gUpdatedData = input;
                var pathimage = bs + "assets/file/room/";
                assetsImageUrl = input['image'];
                var dataR = "";
                for (var x in enabledRoom) {
                    var sel = x == input['is_disabled'] ? "selected" : "";
                    dataR += '<option ' + sel + ' value="' + x + '">' + enabledRoom[x] + '</option>'
                }
                var imgp = input.image == "" || input.image == null ? defaultImage : input.image;
                var imap = bs + 'api/Room/GetRoomDetailView/' + imgp + '?h=80&noCache=false';
                $('#id_edt_image_old').attr("src", imap);


                if (input['image2'] != null) {
                    var im2 = input['image2'].split("##");
                    var ht = "";
                    var n0 = 0;
                    for (var x in im2) {
                        n0++;
                        var imgp2 = im2[x] == "" || im2[x] == null ? defaultImage : im2[x];
                        var imap2 = bs + 'api/Room/GetRoomDetailView/' + imgp2 + '?h=80&noCache=false';
                        $('#id_edt_image2_' + n0 + '_old').attr("src", imap2);

                    }
                }
                $('#id_edt_is_disabled').html(dataR)
               
                $('#id_edt_image_old').attr("src", imap);
                $('#id_edt_radid').val(input['radid']);
                $('#id_edt_name').val(input['name']);
                $('#id_edt_capacity').val(input['capacity']);
                $('#id_edt_automation_active').val(input['serial']);
                $('#id_edt_location').val(input['location'])
                $('#id_edt_description').val(input['description'])
                $('#id_edt_id').val(id);
                var timest = input['work_time'];
                var timesp = timest.split('-');
                var html_room = "";
                var html_devices = "";
                $('#id_edt_work_start').val(timesp[0]);
                $('#id_edt_work_end').val(timesp[1]);
                $('#id_edt_price').val(input['price']);
                $('#id_edt_access_id').val(input['access_id']);
                var wrkdaydb = input['work_day'];
                var wrkdaydb_array = wrkdaydb

                var google_map = input['google_map'];

                $("#id_edt_google_map").val(google_map);

                var facilitydb = input['facility_room'];
                var facilitydb_array = [];
                for (var xxx in input['facility_room2']) {
                    facilitydb_array.push(input['facility_room2'][xxx]['facility_id'])
                }
                var html_aut = "";
                var html_wkd = "";
                var html_active = "";
                var html_typeroom = "";
                var html_building = "";



                if (modules['automation']['is_enabled'] == 1) {
                    var active = ["Off", "On"];
                    $.each(active, function(index, item) {
                        var sl = "";
                        if (input['is_automation'] == index) { sl = "selected" }
                        html_active += "<option " + sl + " value=" + index + ">" + item + "</option>";
                    });
                    html_aut += "<option value=''>Please choose a automation ...</option>";
                    $.each(gAutomation, function(index, item) {
                        var sl = "";
                        if (input['automation_id'] == item.id) {
                            sl = "selected";
                        }
                        html_aut += "<option " + sl + " value=" + item.id + ">" + item.name + "</option>";
                    });
                }

                $.each(typeRoom, function(index, item) {
                    var sl = item.value == input['type_room'] ? "selected" : "";

                    html_typeroom += "<option " + sl + " value=" + item.value + ">" + item.text + "</option>";
                });




                var workday = ["SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY"];
                $.each(workday, function(index, item) {
                    var sl = "";
                    if (wrkdaydb_array.indexOf(item.toUpperCase()) >= 0) {
                        sl = "selected";
                    }
                    html_wkd += "<option " + sl + " value=" + item + ">" + item + "</option>";
                })
                var html_facility = "";
                for (var i in gFacility) {
                    var selcted = "";
                    if (facilitydb_array.indexOf(gFacility[i]['id']) >= 0) { selcted = "selected" }
                    html_facility += "<option " + selcted + " value='" + gFacility[i]['id'] + "'>" + gFacility[i]['name'] + "</option>";
                }

                // html_building += "<option value=''>Please choose a building ...</option>";
                for (var i in gBuilding) {

                    var selcted = gBuilding[i]['id'] == input['building_id'] ? "selected" : "";
                    html_building += "<option " + selcted + " value='" + gBuilding[i]['id'] + "'>" + gBuilding[i]['name'] + "</option>";
                }


                $('#id_edt_building_id').html(html_building)
                $('#id_edt_automation').html(html_aut);
                $('#id_edt_workday').html(html_wkd);
                $('#id_edt_automation_active').html(html_active);
                $('#id_edt_facility_room').html(html_facility);
                $('#id_edt_type_room').html(html_typeroom);
                // 
                // 
                // 
                // ADVANCED
                // 
                var is_config_enable = input['is_config_setting_enable']-0;
                var is_config_adv_approval = input['is_enable_approval'] -0;
                var is_config_adv_permission = input['is_enable_permission'] -0;
                var is_config_adv_checkin = input['is_enable_checkin'] -0;
                var is_config_adv_recurring = input['is_enable_recurring'] -0;





                // console.log(input['is_config_setting_enable'] )
                if (is_config_enable != 1) {
                    advanceEdtRoomDisabled();
                } else {
                    advanceEdtRoomEnabled();
                }

                if (data.collection?.config_room_for_usage) {

                    let html_adv_room_for_usage = "";

                    var room_for_usage = data.collection?.config_room_for_usage

                    for (const room of gRoomForUsage) {
                        const roomName = room.name;
                        const roomId = room.id;


                        const isSelected = room_for_usage.some(item => item === roomId.toString());

                        html_adv_room_for_usage += `<option ${isSelected ? "selected" : ""} value='${roomId}'>${roomName}</option>`;
                    }

                    $('#id_edt_adv_room_for_usage').html(html_adv_room_for_usage);


                    // for trigger the select element
                    var old_data = $('#id_edt_adv_room_for_usage').val()
                    $('#id_edt_adv_room_for_usage').val(null)
                    $('#id_edt_adv_room_for_usage').val(old_data)

                }

                fetchRoomForUsageDetail(id, is_config_enable)

                //// Recurreing
                if (is_config_adv_recurring == 1 && is_config_enable == 1) {
                    $('#id_edt_adv_is_enable_recurring').attr("checked", true);
                } else {
                    $('#id_edt_adv_is_enable_recurring').attr("checked", true);
                }

                // APPROVAL
                // 
                if (is_config_adv_approval == 1 && is_config_enable == 1) {
                    $('#id_edt_adv_config_approval_user').removeAttr('disabled');
                    $('#id_edt_adv_is_enable_approval').attr("checked", true);
                } else {
                    $('#id_edt_adv_config_approval_user').attr("disabled", true);
                    $('#id_edt_adv_is_enable_approval').attr("checked", false);
                }
                var config_approval_user = input['config_approval_user'] == null ? "" :  input['config_approval_user'];
                var html_adv_approval_user = "";
                var sp_config_approval_user = config_approval_user //.split(",");
                for (var iu in gUserApproval) {
                    var rrrr_g = gUserApproval[iu].employee_id;
                    var rrrr_g_name = gUserApproval[iu].name;
                    var s = "";
                    if (sp_config_approval_user.indexOf(rrrr_g) >= 0) {
                        s = "selected"
                    }
                    html_adv_approval_user += "<option " + s + " value='" + rrrr_g + "'>" + rrrr_g_name + "</option>";
                }
                $('#id_edt_adv_config_approval_user').html(html_adv_approval_user);

                // PERMISSION
                // 
                if (is_config_adv_permission == 1 && is_config_enable == 1) {
                    $('#id_edt_adv_config_permission_user').removeAttr('disabled');
                    $('#id_edt_adv_is_enable_permission').attr("checked", true);
                } else {
                    $('#id_edt_adv_config_permission_user').attr("disabled", true);
                    $('#id_edt_adv_is_enable_permission').attr("checked", false);
                }
                var config_permission_user = input['config_permission_user'] == null ? "":input['config_permission_user']  ;
                var html_adv_permission_user = "";
                var sp_config_permission_user = config_permission_user //.split(",");
                for (var iu in gUserPermission) {
                    var rrrr_g = gUserPermission[iu].id;
                    var rrrr_g_name = gUserPermission[iu].name;
                    var s = "";
                    if (sp_config_permission_user.indexOf(rrrr_g) >= 0) {
                        s = "selected"
                    }
                    html_adv_permission_user += "<option " + s + " value='" + rrrr_g + "'>" + rrrr_g_name + "</option>";
                }
                $('#id_edt_adv_config_permission_user').html(html_adv_permission_user);
                // 
                // CHECKIN PERMISSION
                // 
                // $('#id_edt_adv_config_permission_end').removeAttr();
                var config_permission_checkin = input['config_permission_checkin'] == null ? 0 : input['config_permission_checkin'];
                var config_permission_end = input['config_permission_end'];
                var is_realease_checkin_timeout = input['is_realease_checkin_timeout'] -0;
                // console.log(is_config_adv_checkin,is_config_enable, is_realease_checkin_timeout )
                if (is_config_adv_checkin == 1 && is_config_enable == 1) {
                    $('#id_edt_adv_config_permission_checkin').removeAttr('disabled');
                    $('#id_edt_adv_is_enable_checkin').attr("checked", true);
                    $('#id_edt_adv_is_realease_checkin_timeout').removeAttr('disabled');
                    if(is_realease_checkin_timeout == 1){
                        $('#id_edt_adv_is_realease_checkin_timeout').attr("checked", true);
                        $('#id_edt_adv_config_release_room_checkin_timeout').removeAttr('disabled');
                    }else{
                        $('#id_edt_adv_is_realease_checkin_timeout').attr("checked", false);
                        $('#id_edt_adv_config_release_room_checkin_timeout').attr("disabled", true);
                    }
                } else {
                    $('#id_edt_adv_is_enable_checkin').attr("checked", false);
                    $('#id_edt_adv_config_permission_checkin').attr("disabled", true);
                    $('#id_edt_adv_config_permission_end').removeAttr("disabled");
                    $('#id_edt_adv_is_realease_checkin_timeout').attr("disabled", true);
                }
                var html_adv_permission_checkin = "";
                var html_adv_permission_end_permission = "";
                // var sp_config_permission_user = config_permission_checkin.split(",");
                for (var iu in gRoomUserCheckin) {
                    var rrrr_g = gRoomUserCheckin[iu];
                    var s = config_permission_checkin ==  rrrr_g.key ? "selected":"";
                    html_adv_permission_checkin += "<option " + s + " value='" + rrrr_g.key + "'>" + rrrr_g.name + "</option>";
                }

                for (var iuen in gRoomUserCheckin) {
                    var rrrr_g = gRoomUserCheckin[iuen];
                    var s = config_permission_end ==  rrrr_g.key ? "selected":"";
                    html_adv_permission_end_permission += "<option " + s + " value='" + rrrr_g.key + "'>" + rrrr_g.name + "</option>";
                }
                $('#id_edt_adv_config_permission_checkin').html(html_adv_permission_checkin);
                $('#id_edt_adv_config_permission_end').html(html_adv_permission_end_permission);
                var html_adv_config_min_duration = selectGlobal(gMinimumDurationMeeting,"value", input['config_min_duration']-0);
                var html_adv_config_max_duration = selectGlobal(gMaximumDurationMeeting,"value", input['config_max_duration']-0);
                var html_adv_config_advance_booking = selectGlobal(gAdvanceMeeting,"value", input['config_advance_booking']-0);
                var html_adv_config_release_room_checkin_timeout = selectGlobal(gReleaseTimeout,"value", input['config_release_room_checkin_timeout']-0);

                $('#id_edt_adv_config_min_duration').html(html_adv_config_min_duration);
                $('#id_edt_adv_config_max_duration').html(html_adv_config_max_duration);
                $('#id_edt_adv_config_advance_booking').html(html_adv_config_advance_booking);
                $('#id_edt_adv_config_release_room_checkin_timeout').html(html_adv_config_release_room_checkin_timeout);



                enable_datetimepicker()
                select_enable()
                initTypeRoom('edit', 'id_edt_type_room', 'id_edt_merge_room', input.id);
                initKindRoom('id_edt_kind_room', data.collection.kind_room);
                ocEdtRoomForUsage()

                $('#id_mdl_update').modal('show');
            } else {
                var msg = "Your session is expired, login again !!!";
                swalShowNotification('alert-danger', msg, 'top', 'center')
            }
            $('#id_loader').html('');

        },
        error: errorAjax
    })
}

function fetchRoomForUsageDetail(id, is_config_enable) {
    assetsImageUrl = "";
    $.ajax({
        url: "api/Room/GetConfigRoomForUsageByIdRoom/" + id,
        type: "GET",
        dataType: "json",
        beforeSend: function () {
            $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function (data) {
            gRoomForUsageGenerate = data.collection;

            var html_adv_tbl_room_for_usage = "";
            if (is_config_enable == 1) {
                html_adv_tbl_room_for_usage = generateTableUsage();
            }
            $('#id_edt_adv_tbl_room_for_usage tbody').html(html_adv_tbl_room_for_usage);

        }
    })
}
function advanceCrtRoomDisabled() {
    // checked
    $('#id_crt_adv_is_config_setting_enable').attr("checked", false);
    // disable
    $('#id_crt_adv_room_for_usage').attr('disabled', true);
    $('#id_crt_adv_is_enable_approval').attr('disabled', true);
    $('#id_crt_adv_config_approval_user').attr("disabled", true);
    $('#id_crt_adv_is_enable_checkin').attr("disabled", true);
    $('#id_crt_adv_config_permission_checkin').attr("disabled", true);

    $('#id_crt_adv_is_enable_permission').attr("disabled", true);
    $('#id_crt_adv_config_permission_user').attr("disabled", true);
    $('#id_crt_adv_config_min_duration').attr("disabled", true);
    $('#id_crt_adv_config_max_duration').attr("disabled", true);
    $('#id_crt_adv_config_advance_booking').attr("disabled", true);
    $('#id_crt_adv_is_enable_recurring').attr("disabled", true);
    $('#id_crt_adv_tbl_room_for_usage').hide();



    $('#id_panel_checkin').hide();
}


function advanceEdtRoomDisabled() {
    // checked
    $('#id_edt_adv_is_config_setting_enable').attr("checked", false);
    // disable
    $('#id_edt_adv_room_for_usage').attr('disabled', true);
    $('#id_edt_adv_is_enable_approval').attr('disabled', true);
    $('#id_edt_adv_config_approval_user').attr("disabled", true);
    $('#id_edt_adv_is_enable_checkin').attr("disabled", true);
    $('#id_edt_adv_config_permission_checkin').attr("disabled", true);

    $('#id_edt_adv_is_enable_permission').attr("disabled", true);
    $('#id_edt_adv_config_permission_user').attr("disabled", true);
    $('#id_edt_adv_config_min_duration').attr("disabled", true);
    $('#id_edt_adv_config_max_duration').attr("disabled", true);
    $('#id_edt_adv_config_advance_booking').attr("disabled", true);
    $('#id_edt_adv_is_enable_recurring').attr("disabled", true);
     $('#id_edt_adv_tbl_room_for_usage').hide();



    $('#id_panel_checkin_edt').hide();
}

function advanceCrtRoomEnabled() {
    // checked
    $("#id_crt_adv_is_config_setting_enable").attr("checked", true);

    // disable
    $("#id_crt_adv_is_enable_approval").removeAttr("disabled");
    $("#id_crt_adv_room_for_usage").removeAttr("disabled");
    $("#id_crt_adv_is_enable_checkin").removeAttr("disabled");
    $("#id_crt_adv_is_enable_permission").removeAttr("disabled");

    $("#id_crt_adv_config_min_duration").removeAttr("disabled");
    $("#id_crt_adv_config_max_duration").removeAttr("disabled");
    $("#id_crt_adv_config_advance_booking").removeAttr("disabled");
    $("#id_crt_adv_is_enable_recurring").removeAttr("disabled");
    $("#id_crt_adv_tbl_room_for_usage").show();


    

    //if ($("#id_crt_adv_is_enable_approval")[0].checked) {
    //    $("#id_crt_adv_config_approval_user").removeAttr("disabled");
    //}
    //if ($("#id_crt_adv_is_enable_checkin")[0].checked) {
    //    $("#id_crt_adv_config_permission_checkin").removeAttr("disabled");
    //}
    //if ($("#id_crt_adv_is_enable_permission")[0].checked) {
    //    $("#id_edt_adv_config_permission_user").removeAttr("disabled");
    //}

    $("#id_panel_checkin").show();
}

function advanceEdtRoomEnabled() {
    // checked
    $('#id_edt_adv_is_config_setting_enable').attr("checked", true);

    // disable
    $('#id_edt_adv_is_enable_approval').removeAttr('disabled');
    $('#id_edt_adv_room_for_usage').removeAttr('disabled');
    $('#id_edt_adv_is_enable_checkin').removeAttr('disabled');
    $('#id_edt_adv_is_enable_permission').removeAttr('disabled');

    $('#id_edt_adv_config_min_duration').removeAttr('disabled');
    $('#id_edt_adv_config_max_duration').removeAttr('disabled');
    $('#id_edt_adv_config_advance_booking').removeAttr('disabled');
    $('#id_edt_adv_is_enable_recurring').removeAttr('disabled');
    $('#id_edt_adv_tbl_room_for_usage').show();

    if($('#id_edt_adv_is_enable_approval')[0].checked){
        $('#id_edt_adv_config_approval_user').removeAttr('disabled');
    }
    if($('#id_edt_adv_is_enable_checkin')[0].checked){
        $('#id_edt_adv_config_permission_checkin').removeAttr('disabled');
    }
    if($('#id_edt_adv_is_enable_permission')[0].checked){
        $('#id_edt_adv_config_permission_user').removeAttr('disabled');
    }

    $('#id_panel_checkin_edt').show();
}

function removeAll(){
    var co = dataTB.rows({selected:true}).data().length;
    var dataRoll = dataTB.rows('.selected').indexes();
    var colRadid = [];
    var colMs365 = [];
    var colGoogle = [];
    var num = 0;
    var idsToDelete = [];
    for(var i = 0; i < dataRoll.length;i++){
         var indexx = dataRoll[i];
        var row = gInitRoom[indexx];

        idsToDelete.push(row.id);

        colRadid.push(row.radid);
        colMs365.push(row.config_microsoft);
        colGoogle.push(row.config_google);
    }
    var strcolRadid     = colRadid.join(",");
    var strcolMs365     = colMs365.join(",");
    var strcolGoogle    = colGoogle.join(",");
    Swal.fire({
        title: 'Are you sure you want remove this data?',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: `Remove ${co} items`,
        cancelButtonText: 'Cancel !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            var bs = $('#id_baseurl').val();
            const queryIds = idsToDelete.join(",");
            $.ajax({
                url: "api/Room/DeleteItemsByIds?ids=" + queryIds,
                type: "DELETE",
                data: {
                    data : strcolRadid,
                    ms365 : strcolMs365,
                    google : strcolGoogle,
                },
                dataType: "json",
                beforeSend: function() {
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success: function(data) {
                    $('#id_loader').html('');
                    if (data.status == "success") {
                        
                        swalShowNotification('alert-success', "Succes delete room selected ", 'top', 'center')
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


function removeData(t) {
    var id = t.data('id');
    var name = t.data('name');
    Swal.fire({
        title: 'Are you sure you want delete it?',
        text: "You will lose the data room " + name + " !",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Delete !',
        cancelButtonText: 'Cancel !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            var bs = $('#id_baseurl').val();
            $.ajax({
                url: "api/Room/Delete?id=" + id,
                type: "DELETE",
                processData: false,
                contentType: false,
                dataType: "json",
                beforeSend: function() {
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success: function(data) {
                    $('#id_loader').html('');
                    if (data.status == "success") {
                        swalShowNotification('alert-success', "Succes deleted room " + name, 'top', 'center')
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

function removeIntegrationRoom() {
    Swal.fire({
        title: 'Are you sure you want remove it?',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Delete !',
        cancelButtonText: 'Cancel !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            var bs = $('#id_baseurl').val();
            var f = {
                roomid: dataRoomIntegratonId,
                type: dataRoomIntegratonType,
                id: dataRoomIntegratonCurrent.id,
            }
            $.ajax({
                url: bs + "room/remove/integration",
                type: "POST",
                data: f,
                dataType: "json",
                beforeSend: function() {
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success: function(data) {
                    $('#id_loader').html('');
                    if (data.status == "success") {
                        $('#frm_integration')[0].reset();
                        $('#id_mdl_integration').modal('hide');
                        $('#id_mdl_integrationLabel').html("");
                        swalShowNotification('alert-success', "Succes deleted room integration ", 'top', 'center')
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
