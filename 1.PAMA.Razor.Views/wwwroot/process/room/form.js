$('#id_crt_building_id').on('change', function(){
    console.log('s')
    var v = $('#id_crt_building_id').val();
    var html_floor = "";
    html_floor += "";
    // console.log(html_floor);
    for (var i in gFloor) {
        if(v ==  gFloor[i].building_id ){
            html_floor += "<option value=" + gFloor[i].id + ">" + gFloor[i].name + "</option>";
        }
    }
    $('#id_crt_floor_id').html(html_floor);
    select_enable();
})

$('#frm_create').submit(function(e) {
    e.preventDefault();
    // gFacility = data.collection
    var form = new FormData(this);
    var vF = $('#id_crt_facility_room').val();
    var collFF = [];
    for (var x in vF) {
        var i = vF[x];

        for (var m in gFacility) {
            var gFF = gFacility[m];
            if (gFF['id'] == i) {
                form.append('facility_room_name[]', gFF['name']);
                // collFF.push(gFF['name'])
                break;
            }
        }

    }
    if ($('#id_crt_type_room').val() == "merge") {
        if ($('#id_crt_merge_room').val().length <= 1) {
            swalShowNotification('alert-warning', "Please choose a merge/combine room, minimum 2 rooms", 'top', 'center');
            return;
        }
    }
    var bs = $('#id_baseurl').val();
    $.ajax({
        url: bs + "room/post/create",
        type: "POST",
        dataType: "json",
        data: form,
        contentType: false,
        cache: false,
        processData: false,
        beforeSend: function() {
            $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            if (data.status == "success") {
                $('#frm_create')[0].reset();

                init();
                $('#id_mdl_create').modal('hide');
                swalShowNotification('alert-success', data.msg, 'top', 'center')
            } else {
                swalShowNotification('alert-danger', data.msg, 'top', 'center')
            }
            $('#id_loader').html('');
        },
        error: errorAjax
    });
})

async function submitFrmUpdate() {
    var ressUpdate = {status:'fail',collection:[],msg :""};
    var ressUpdateAdv = {status:'fail',collection:[],msg :""};
    var ressUpdateCheckin = {status:'fail',collection:[],msg :""};
    $('#id_loader').html('<div class="linePreloader"></div>');
    loadingg('Please wait ! ', 'Loading . . . ')
    try { ressUpdate = await submitFrmModuleUpdate();
    }catch(error){ console.log(error); }
    try { ressUpdateAdv = await submitFrmModuleUpdateAdv();
    }catch(error){ console.log(error); }
    try { ressUpdateCheckin = await submitFrmModuleUpdateAdvCheckin();
    }catch(error){ console.log(error); }
    setTimeout(function(){
        $('#id_loader').html('');
        $('#frm_update')[0].reset();
        $('#frm_adv_update')[0].reset();
        $('#id_mdl_update').modal('hide');
        Swal.fire({
            title: 'Success',
            text: ressUpdate.msg,
            type: "success",
            showCancelButton: false,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Close !',
        }).then((result) => {
            // window.location.reload();
        })

    }, 2000)
    // console.log(ressUpdateAdv, JSON.stringify(gRoomForUsageGenerate))
}

function submitFrmModuleUpdate() {
    $('#frm_update').submit();
    var id = $('#id_edt_id').val()
    var bs = $('#id_baseurl').val();
    var form = new FormData($('#frm_update')[0]);
    var vF = $('#id_edt_facility_room').val();
    var collFF = [];
    for (var x in vF) {
        var i = vF[x];
        for (var m in gFacility) {
            var gFF = gFacility[m];
            if (gFF['id'] == i) {
                form.append('facility_room_name[]', gFF['name']);
                // collFF.push(gFF['name'])
                break;
            }
        }
    }
    if ($('#id_edt_type_room').val() == "merge") {
        if ($('#id_edt_merge_room').val().length <= 1) {
            swalShowNotification('alert-warning', "Please choose a merge/combine room, minimum 2 rooms", 'top', 'center');
            return;
        }
    }
    return $.ajax({
        url: bs + "room/post/update/" + id,
        type: "POST",
        dataType: "json",
        contentType: false,
        cache: false,
        processData: false,
        // data : form,
        data: form,
        beforeSend: function() {
            // $('#id_loader').html('<div class="linePreloader"></div>');
        },
        success: function(data) {
            // if (data.status == "success") {
            //     $('#frm_update')[0].reset();
            //     $('#id_mdl_update').modal('hide');
            //     init();
            //     swalShowNotification('alert-success', data.msg, 'top', 'center')
            // } else {
            //     swalShowNotification('alert-danger', data.msg, 'top', 'center')
            // }
            // $('#id_loader').html('');
        },
        error: errorAjax
    })
}


function submitFrmModuleUpdateAdv() {
    $('#frm_adv_update').submit();
    var id = $('#id_edt_id').val()
    var bs = $('#id_baseurl').val();
    var form = new FormData($('#frm_adv_update')[0]);
    form.append("room_usage_detail", JSON.stringify(gRoomForUsageGenerate))
    
    return $.ajax({
        url: bs + "room/post/update-adv/" + id,
        type: "POST",
        dataType: "json",
        contentType: false,
        cache: false,
        processData: false,
        data: form,
        beforeSend: function() {},
        success: function(data) {},
        error: errorAjax
    })
}
function submitFrmModuleUpdateAdvCheckin() {
    $('#frm_adv_checkin_update').submit();
    var id = $('#id_edt_id').val()
    var bs = $('#id_baseurl').val();
    var form = new FormData($('#frm_adv_checkin_update')[0]);
    return $.ajax({
        url: bs + "room/post/update-adv-checkin/" + id,
        type: "POST",
        dataType: "json",
        contentType: false,
        cache: false,
        processData: false,
        data: form,
        beforeSend: function() {},
        success: function(data) {},
        error: errorAjax
    })
}
$('#frm_update').submit(function(e) {
    e.preventDefault();
});

$('#frm_adv_update').submit(function(e) {
    e.preventDefault();
});

$('#frm_adv_checkin_update').submit(function(e) {
    e.preventDefault();
});

$('#frm_integration').submit(function(e) {
    e.preventDefault();
    Swal.fire({
        title: 'Are you sure you want save it?',
        text: "",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Save !',
        cancelButtonText: 'Cancel !',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            submitIntegration();
        } else {

        }
    });

    // gFacility = data.collection
})
