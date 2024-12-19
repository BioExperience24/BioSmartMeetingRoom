
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
        <img style="height: 350px;" src="<?php echo base_url("assets/email/reject.png") ?>"  alt="">
        <div class="error-code" style="font-size: 20px;font-weight: bold;"><?= $text_title?></div>
        <br>
        <br>
        <div class="error-message" style="font-size: 16px;font-weight: normal;"><?= $text_msg?></div>
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
        // popupalert();
       
        
    </script>
</body>

</html>