
<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>Internal Attendance</title>
    <link href="<?php echo base_url("assets/theme/plugins/bootstrap/css/bootstrap.css") ?>" rel="stylesheet">
    <link href="<?php echo base_url("assets/theme/plugins/iconfont/material-icons.css");?>" rel="stylesheet">
    <link href="<?php echo base_url("assets/theme/plugins/node-waves/waves.css") ?>" rel="stylesheet" />
    <!-- Animation Css -->
    <link href="<?php echo base_url("assets/theme/plugins/animate-css/animate.css") ?>" rel="stylesheet" />
    <!-- Custom Css -->
    <link href="<?php echo base_url("assets/theme/css/style.css") ?>" rel="stylesheet">
    <style>
        .swal2-popup{
            width:50em !important;
        }
        b{
            padding:2px; 
            font-size: 20px;
        }
        p{
            font-size: 16px;
        }
    </style>
</head>

<body class="five-zero-zero">
    <div class="five-zero-zero-container">
        <div class="error-code" style="font-size: 70px;"><?= $text_title?></div>
        <img src="<?php echo base_url("assets/image_people.png") ?>"  alt="">
        <div class="error-message"><?= $text_msg?></div>
        <div class="button-place">
            <!-- <a href="../../index.html" class="btn btn-default btn-lg waves-effect">GO TO HOMEPAGE</a> -->
        </div>
    </div>
    <textarea style="display: none;"  id="id_data" cols="30" rows="10"><?= $data?></textarea>    
    <input id="id_attend" type="hidden" value="<?= $attend?>" />
    <input id="id_baseurl" type="hidden" value="<?= base_url()?>" />
    <script src="<?php echo base_url("assets/theme/plugins/jquery/jquery.min.js");?>"></script> 
    <script src="<?php echo base_url("assets/theme/plugins/bootstrap/js/bootstrap.js");?>"></script>
    <script src="<?php echo base_url("assets/theme/plugins/node-waves/waves.js");?>"></script>
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/sweatalert2/sweetalert2.all.min.js"></script>
    <script>
        popupalert();
        function popupalert(){
            var bs = $('#id_baseurl').val();
            var data = $('#id_data').val();
            var attend = $('#id_attend').val();
            if(data != "" || data != null){
                var jsondata = JSON.parse(data);
                // console.log(jsondata)
                var ttle = "Meeting of " + jsondata['title'];
                var html = '';
                if(attend != "Attend"){
                    html += '<img style="width:8em" src="'+bs+'assets/close.png"  alt="">';
                }else{
                    html += '<img style="width:8em" src="'+bs+'assets/check.png"  alt="">';
                }
                html += '<br>';
                html += '<br>';
                html += '<br>';
                html += '<div class="row">';
                    html += '<div class="col-xs-6 align-left" >';
                    html += '<b>Name</b>';
                    html += '</div>';
                    html += '<div class="col-xs-6 align-left">';
                    html += '<b>'+jsondata['name']+'</b>';
                    html += '</div>';
                html += '</div>';
                html += '<div class="row">';
                    html += '<div class="col-xs-6 align-left" >';
                    html += '<b>Date</b>';
                    html += '</div>';
                    html += '<div class="col-xs-6 align-left">';
                    html += '<b>'+moment(jsondata['date']).format('DD MMMM YYYY')+'</b>';
                    html += '</div>';
                html += '</div>';
                html += '<div class="row">';
                    html += '<div class="col-xs-6 align-left" >';
                    html += '<b>Time</b>';
                    html += '</div>';
                    html += '<div class="col-xs-6 align-left">';
                    html += '<b>'+moment(jsondata['start']).format("hh:mm A")+' - '+moment(jsondata['end']).format("hh:mm A")+' </b>';

                    html += '</div>';
                html += '</div>';
                html += '<div class="row">';
                    html += '<div class="col-xs-6 align-left" >';
                    html += '<b>Room</b>';
                    html += '</div>';
                    html += '<div class="col-xs-6 align-left">';
                    html += '<b>'+jsondata['room_name']+' </b><br/>';
                    html += '<p>'+jsondata['location']+' </p>';
                    
                    html += '</div>';
                html += '</div>';
                html += '<div class="row">';
                    html += '<div class="col-xs-6 align-left" >';
                    html += '<b>Attend Status</b>';
                    html += '</div>';
                    html += '<div class="col-xs-6 align-left">';
                    html += '<b>'+attend+'</b>';
                    html += '</div>';
                html += '</div>';
                if(attend != "Attend"){
                    html += '<div class="row">';
                        html += '<div class="col-xs-6 align-left" >';
                        html += '<b>Reason </b>';
                        html += '</div>';
                        html += '<div class="col-xs-6 align-left">';
                        html += '<b>'+jsondata['attendance_reason']+'</b>';
                        html += '</div>';
                    html += '</div>';
                    if(jsondata['attendance_reason'] == null || jsondata['attendance_reason'] == ""){
                        openAlertReason(ttle,jsondata['booking_id'], jsondata['nik'], html)
                    }else{
                        OnlyAlert(ttle,html)
                    }
                }else{
                    OnlyAlert(ttle,html)
                    
                }
            }
            
        }
        function OnlyAlert(ttle,html){
            
            Swal.fire({
                        title: ttle.toUpperCase(),
                        html: html,
                        showCloseButton: true,
                        focusConfirm: false,
                        confirmButtonText:
                            'Close!',
                        confirmButtonAriaLabel: 'Close!',
                    })
        }
        function openAlertReason(ttle, bookID, nik, htmlsemua=""){
            var bs = $('#id_baseurl').val();
            var html = '<div class="row">';
                    html += '<div class="col-xs-12 align-left" >';
                    html += '<input class="form-control" id="id_reason" placeholder="Reason for not attend">';
                    html += '</div>';
                html += '</div>';
            Swal.fire({
              title: 'Your reason for not attend on '+ttle,
              html: html,
              confirmButtonText: 'Submit',
              // showLoaderOnConfirm: true,
              
            }).then((result) => {
              if (result.value) {
                var rs = $('#id_reason').val();
                if(rs == ""){
                    Swal.fire({
                      title: "Reason must not be empty",
                    });
                    setTimeout(function(){
                        window.location.reload()
                    }, 3000)
                }else{
                    var form = {
                        reason : rs,
                        booking_id:bookID,
                        nik:nik,
                    }
                    var url = bs + 'participant/internal/set/reason';
                    $.ajax({
                        url :url,
                        type : "post",
                        data : form,
                        dataType: "json",
                        beforeSend: function(){

                        },
                        success:function(data){
                            // OnlyAlert(ttle,htmlsemua);
                            window.location.reload()
                        },
                        error: errorAjax
                    })
                }
                

                // Swal.fire({
                //   title: `${result.value.login}'s avatar`,
                //   imageUrl: result.value.avatar_url
                // })
              }
            })
        }
        function errorAjax(xhr, ajaxOptions, thrownError){
            // $('#id_loader').html('');
            if(ajaxOptions == "parsererror"){
                // var msg = "Status Code 500, Error Server bad parsing";
                // showNotification('alert-danger', msg,'bottom','left')
            }else{
                // var msg ="Status Code "+ xhr.status + " Please check your connection !!!";
                // showNotification('alert-danger', msg,'bottom','left')
            }
        }
        
    </script>
</body>

</html>