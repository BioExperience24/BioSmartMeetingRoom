function ckMaxMinDurationMeeting(room) {
    var rt = {
        status: false
    };
    try {

    } catch (error) {

    }
    var ckAdvEnable = room.is_config_setting_enable - 0;
    // NO ADV
    if (ckAdvEnable != 1) { return rt; }
    rt['max'] = room.config_max_duration
    rt['min'] = room.config_min_duration
    rt['status'] = true

    return rt;
}


function ckAdvanceBookingMeeting(room) {
    var rt = {
        status: false
    };
    try {

    } catch (error) {

    }
    var ckAdv = room.config_advance_booking - 0;

    rt['status'] = true
    rt['advance_booking'] = ckAdv;

    return rt;
}

function setSelectedTimeCrtBookingInside(t) {
    var val = t.val();
    var type = t.data('type');
    // console.log(type, val)
    var roomnum = gBookingCrt.room.num;
    var datetimeMeeting = gTimeSelectBooking[roomnum]['datatime'];
    var room = gTimeSelectBooking[roomnum];
    var timearray = {};
    var timenum = null;
    for (var xxx in datetimeMeeting) {
        if (datetimeMeeting[xxx].time_array == val) {
            timearray = datetimeMeeting[xxx];
            timenum = xxx;
            break;
        }
    }
    if (timenum == null) {
        return;
    }
    if (type == "end") {
        gBookingCrt['timeend'] = {
            num: timenum,
            time: datetimeMeeting[timenum],
        };
    } else {
        gBookingCrt['timevalue'] = val;
        gBookingCrt['timestart'] = {
            num: timenum,
            time: datetimeMeeting[timenum],
        };
        var endTimenum = timenum + 1;
        if ((modules['room_adv']['is_enabled'] - 0) == 1) {
            var minduration = room.config_min_duration - 0;
            var enddTime = minduration / 15;
            endTimenum = (timenum - 0) + enddTime
        }
        if (datetimeMeeting[endTimenum] != null) {
            gBookingCrt['timeend'] = {
                num: endTimenum,
                time: datetimeMeeting[endTimenum],
            };
            $('#id_frm_crt_timeend').selectpicker('val', datetimeMeeting[endTimenum].time_array);
            console.log(datetimeMeeting[endTimenum].time_array)
        }
    }
    calculate_cost();
    calculate_timeRoom(true);
    changeTimeRoom('id_frm_crt_merge_room');
}

function calculate_timeRoom(notif = false){
    var roomNum = gBookingCrt['room']['num'];
    var selectRoom = gTimeSelectBooking[roomNum];
    if ((modules['room_adv']['is_enabled'] - 0) == 0 || (selectRoom['is_config_setting_enable']-0) == 0 ) {
        // console.log("no")
        return true;
    }

    var date = moment().format("YYYY-MM-DD");
    var minduration = selectRoom.config_min_duration - 0;
    var maxduration = selectRoom.config_max_duration - 0;
    var strdate_start = `${date} ${gBookingCrt.timestart.time.time_array}`;
    var strdate_end = `${date} ${gBookingCrt.timeend.time.time_array}`;
    var dstart = moment(strdate_start);
    var dend = moment(strdate_end);
    var d = dend.diff(dstart, "minutes");

    // console.log(minduration, maxduration, d)

    if(d >= minduration  && d <= maxduration){
        return true;
    } else{
        if(notif){
            var msg = ""
            if(d <= minduration){
                msg = `The minimum duration of a meeting under the provisions is `;
                if( maxduration >= 60){
                    msg += `${minduration/60} hours`;
                }else{
                    msg += `${minduration} minutes`;
                }
                Swal.fire('Attention !!!', msg, 'warning')
            }else if( d >= maxduration && maxduration != 0){
                msg = `The maximum duration of a meeting exceeds the provisions is `
                if( maxduration >= 60){
                    msg += `${maxduration/60} hours`;
                }else{
                    msg += `${maxduration} minutes`;
                }
                Swal.fire('Attention !!!', msg, 'warning')
            }
            
        }
        return false;
        // error;
    }
}


function generateSelectTimeRoom(timeType = "", initial = "") {
    // var ele              = t.data('id');         
    var roomNum = gBookingCrt['room']['num'];
    // var timeType         = t.data('type');      
    var selectRoom = gTimeSelectBooking[roomNum];
    var timeArray = selectRoom['datatime'];
    var start = selectRoom['work_start'];
    var end = selectRoom['work_end'];
    var text = gBookingCrt['date'] == "today" ? "" : gBookingCrt['date'];
    var html = ``;
    $.each(timeArray, (index, item) => {
        var tnow = moment().format('hh:mm') + ":00";
        var checkData = checkThatTimeRoom(timeArray, index, item, start, end, text);
        var date = moment().format('YYYY-MM-DD');
        var display_text = moment(date + " " + item.time_array).format("HH:mm");
        if (checkData) {
            if (timeType == "end") {
                var timestart = gBookingCrt['timestart']['time']['time_array'];
                if (timestart == item.time_array) {
                    html += `<option class="timedisabled" disabled value="${item.time_array}" >${display_text}</option>`;
                    // html +=  '<tr class="disabled" ><td>'+display_text+'</td></tr>';
                } else {
                    var selected = initial == item.time_array ? "selected" : "";
                    html += `<option ${selected} value="${item.time_array}" >${display_text}</option>`;
                    // html +=  '<tr data-timeType="'+timeType+'" data-ele="'+ele+'" class="" data-timenum="'+index+'"  data-value="'+item.time_array+'"   data-text="'+display_text+'" data-roomnum="'+roomNum+'" onclick="setTimeCrtBookingInside($(this))" style="cursor:pointer;" ><td>'+display_text+'</td></tr>';
                }
            } else {
                var selected = initial == item.time_array ? "selected" : "";
                html += `<option ${selected} value="${item.time_array}" >${display_text}</option>`;
                // html +=  '<tr data-timeType="'+timeType+'" data-ele="'+ele+'" class="" data-timenum="'+index+'"  data-value="'+item.time_array+'"   data-text="'+display_text+'" data-roomnum="'+roomNum+'" onclick="setTimeCrtBookingInside($(this))" style="cursor:pointer;" ><td>'+display_text+'</td></tr>';

            }

        } else {
            html += `<option  class="timedisabled"  disabled value="${item.time_array}" >${display_text}</option>`;
            // html +=  '<tr class="disabled" ><td>'+display_text+'</td></tr>';
        }
    });

    return html;
}


function checkAttendeesIFAttendeesWithCategory() {
    var roomnum = gBookingCrt['room']['num'];
    var categorylist = gTimeSelectBooking[roomnum]['category'];
    var room = gTimeSelectBooking[roomnum];
    var category = $('#id_frm_crt_category').val();

    if ((modules['room_adv']['is_enabled'] - 0) == 0) {
        // console.log("adv not found");
        return true; // allow
    }

    if( (room['is_config_setting_enable'] -0) == 0 ){
        return true; // allow
    }



    var isVip = $('#id_frm_crt_vip_meeting').val() == "" ? 0 :  ($('#id_frm_crt_vip_meeting').val() -0); // is vip enable
    var ct = getSelectedCategory(category);
    var vipUser = getSelectedVIP(gVipId);

    if(ct == null ){
        console.log("cat not found");
        return true;// allow
    }
    var minCap = ct.min_cap - 0;
    var isInternal = ct.internal - 0;
    var isExternal = ct.external - 0;
    var internalAttendees = gEmployeesSelected.length;
    var externalAttendees = gPartisipantManual.length;
    var allAttendees = 0;

    if (isInternal == 0 && isExternal == 0) { return true; }
    if(vipUser != null ){
       if( (vipUser.vip_limit_cap_bypass -0 ) == 1){
            return true;// allow
       }
    }
    // 
    if (isInternal == 1) { allAttendees += internalAttendees; }
    if (isExternal == 1) { allAttendees += externalAttendees; }
    if (isInternal == 1 && isExternal ==1  && minCap > 0 && allAttendees < minCap) {
        gMsgCategory = `The minimum number of invited internal & external attendees must be ${minCap} people`;
        return false;
    }
    if (isInternal == 1 && minCap > 0 && allAttendees < minCap) {
         gMsgCategory = `The minimum number of invited internal attendees must be ${minCap} people`;
         return false;
    }
    if (isExternal == 1 && minCap > 0 && allAttendees < minCap) {
         gMsgCategory = `The minimum number of invited external attendees must be ${minCap} people`;
         return false;
    }
    return true;
}
function getSelectedCategory(value){
    var data = null;
    var roomnum = gBookingCrt['room']['num'];
    var categorylist = gTimeSelectBooking[roomnum]['category'];
    for (var x in categorylist) {
        if(value == categorylist[x].room_usage_id){
            data = categorylist[x];
            break
        }
    }
    return data;
}

function getSelectedVIP(value){
    var data = null;
    for (var x in gEmployees) {
        if(value == gEmployees[x].id){
            data = gEmployees[x];
            break
        }
    }
    return data;
}

function openVipAccessInit(dataPegawai) {
    if (dataPegawai.is_vip == null) {
        $('#id_area_vip').hide("fast");
        $('#id_frm_crt_vip_meeting').html("");
        $('#id_frm_crt_vip_meeting').attr('disabled', true);
        select_enable();
        return;
    }
    var isVip = dataPegawai.is_vip - 0;
    if (isVip == 0) {
        $('#id_area_vip').hide("fast");
        $('#id_frm_crt_vip_meeting').html("");
        $('#id_frm_crt_vip_meeting').attr('disabled', true);
        select_enable();
        return;
    }
    $('#id_area_vip').show("fast");
    var html = ``;
    for (var x in gVip) {
        html += `<option value="${gVip[x].value}" >${gVip[x].name}</option>`;
    }
    $('#id_frm_crt_vip_meeting').html(html);
    $('#id_frm_crt_vip_meeting').removeAttr('disabled');
    gVipId = dataPegawai.id;
    select_enable();
}

function openCategoryAccessInit() {

    var roomnum = gBookingCrt['room']['num'];
    var room = gTimeSelectBooking[roomnum];


    if( (room['is_config_setting_enable'] -0) == 0 ){
        $('#id_area_category').hide('fast');
        $('#id_frm_crt_category').html("");
        $('#id_frm_crt_category').attr('disabled', true);
        select_enable();
        return;
    }
    if (room['category'] == null) {
        $('#id_area_category').hide('fast');
        $('#id_frm_crt_category').html("");
        $('#id_frm_crt_category').attr('disabled', true);
        select_enable();
        return;
    }
    var category = gTimeSelectBooking[roomnum]['category'];
    $('#id_area_category').show("fast");
    var html = ``;
    var n = 0;
    for (var x in category) {
        var ss = n == 0 ?'selected' : '';
        html += `<option ${ss} value="${category[x].room_usage_id}" >${category[x].name}</option>`;
    }
    $('#id_frm_crt_category').html(html);
    $('#id_frm_crt_category').removeAttr('disabled');
    select_enable();
    return;

}

function ocCategory() {
    var val = $('#id_frm_crt_category').val();
    var t = false;
    for (var x in gRoomCategory) {
        if (val == gRoomCategory[x].id) {
            gRoomCategorySelected = val;
            t = true;
            break;
        }
    }
}

function calculateAttendees() {

}