/**
 *
 * You can write your JS code here, DO NOT touch the default style file
 * because it will make it harder for you to update.
 *
 */

"use strict";

let times;

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

function generateTimes(startTime = moment().startOf('day'), intervalMinutes = 5) {
    let times = [];
    
    let endTime = moment().endOf('day'); // 23:59
    
    while (startTime.isBefore(endTime)) {
        times.push(startTime.format('HH:mm'));
        startTime.add(intervalMinutes, 'minutes');
    }
    
    return times;
}

function generateTimeOptions(id, selected = "", disableTimes = [], isDisablePastTime = false) {
    $(id).empty();

    let isSelectedCount = 0;

    $.each(times, function (_, item) { 
        var opt = document.createElement("option");
        var disabled = false;
        var isSelected = false;
        
        if (disableTimes.includes(item))
        {
            disabled = true;
        }

        // let m = selected != "" ? moment(selected, 'HH:mm') : moment();
        let m = moment();
        if (isDisablePastTime == true && moment(item, 'HH:mm').isBefore(m)) {
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
        // $(id).selectpicker("refresh");
    });

    $(id).select2();
}

function showNotification(message = "", type = "") {
    Swal.fire({
        title: message,
        type: type,
    });
}

function showErrorLoadNotification() {
    var msg = "Failed to load data, please try again !!!";
    showNotification(msg, "error");
}

function showLoading() {
    Swal.fire({
        title: 'Please Wait !',
        allowOutsideClick: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        },
    });
}

function objectifyForm(formArray) {
    //serialize data function
    var returnArray = {};
    for (var i = 0; i < formArray.length; i++){
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
}

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

$(document).ajaxError(function (event, jqXHR, ajaxSettings, thrownError) {
    
    if (jqXHR.status === 0 || jqXHR.status === 401) {
        // Redirect to the 401 error page
        window.location.href = `/webview/error/401`;
    } else {
        console.error('AJAX Error:', jqXHR.status, thrownError);
    }
});

$(document).on("ajaxSend", function(event, jqXHR, settings) {
    if (settings.type === "POST") {
        // console.log("URL:", settings.url);
        showLoading();
    }
});

$(document).on("ajaxComplete", function(event, jqXHR, settings) {
    if (settings.type === "POST") {
        Swal.close();
    }
});