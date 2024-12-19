
<?php  
// date_default_timezone_set("Asia/Jakarta");
// echo date('w');
// print_r($modules);
// die();
$invoice_date_format = array("d/m/Y", "Y-m-d", "d-m-Y");
$invoice_date_format_text = array(date("d/m/Y"), date("Y-m-d"), date("d-m-Y"));
?>
<textarea id="id_modules" style="display: none;"> 
    <?= json_encode($modules)?>
</textarea> 
<!DOCTYPE html>
<html lang="en">
<head>
   <?php $this->load->view("_partials/head_css_dashboard.php", array('pagename'=>$pagename));?>
    <link href="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <link href="<?= base_url()?>assets/theme/plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet" />
    <link href="<?= base_url()?>assets/external/filepond/filepond.min.css" rel="stylesheet">
    <style>
        .demo-filepond-wrapper{
            height: 300px;
            padding-left: 20px;
            padding-top: 20px;
            padding-right: 20px;
            border: dashed 2px grey;
            background: white center no-repeat;
            background-size: cover;
            text-align: center;
            display: block;
            cursor: pointer;
        }
        .image-box {
            position: relative;
            height: 200px;
            width: 100%;
            position: relative;
            background-color: #eee;
            border: dotted 1px grey;
        }
        .img_overlay{
            opacity: 1;
            width: 100%;
            height: 100%;
            display: block;
        }
        .img_overlay:hover{
            opacity: 0.6;
            transition: 1s;
        }
        .image-box:hover .overlay{
            opacity: 1;
            /*width: 150px;*/
        }
    </style>
</head>
<body class="theme-red">
    <!-- Page Loader -->
    <div class="page-loader-wrapper">
        <div class="loader">
            <div class="preloader">
                <div class="spinner-layer pl-red">
                    <div class="circle-clipper left">
                        <div class="circle"></div>
                    </div>
                    <div class="circle-clipper right">
                        <div class="circle"></div>
                    </div>
                </div>
            </div>
            <p>Please wait...</p>
        </div>
    </div>
    <!-- #END# Page Loader -->
    <!-- Overlay For Sidebars -->
    <div class="overlay"></div>
    <!-- Top Bar -->
        <?php $this->load->view("_partials/navbar.php", array("pagename"=>$pagename));?>
    <!-- #Top Bar -->
    <section>
        <?php $this->load->view("_partials/sidebar.php", array("menumaster"=>$menumaster));?>
    </section>
    <section class="content">

         <div class="container-fluid">

            <div class="block-header">
                <h2><?= strtoupper($pagename) ?></h2>
            </div>
            <div class="row clearfix">
                <!-- END BOOKING SETTING -->
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="row clearfix">
                         <div class="col-xs-12">
                            <div class="card">
                                <div class="header">
                                    <div class="row clearfix">
                                        <div class="col-xs-12 col-sm-6">
                                            <h2>SMTP Setting</h2>
                                        </div>
                                        <div class="col-xs-12 col-sm-6 align-right">
                            
                                        </div>
                                    </div>
                                </div>
                                <div class="body ">
                                    <label for="">SMTP Account</label>
                                    <div class="form-group">
                                        <select onchange="onSelectedAccount()" name="name" id="id_smtp_name" class="form-control show-tick"></select>
                                    </div>
                                    <form id="id_frm_setting_smtp" style="display:none;">
                                        <label for="">Enabled Send Email</label>
                                        <div class="form-group">
                                            <select name="is_enabled" id="id_smtp_enable" class="form-control show-tick">
                                            </select>
                                        </div>
                                        <label for="">Host/ Email Smtp Server</label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <input type="text" placeholder="Name" autocomplete="off" name="name" required readonly id="id_smtp_name_val" class="form-control">
                                                <input type="text" placeholder="Host/ Email Smtp Server " autocomplete="off" name="host" id="id_smtp_host" class="form-control">

                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-xs-12 col-sm-6">
                                                <label for="">Username/Email SMTP</label>
                                                <div class="form-group">
                                                    <div class="form-line">
                                                        <input type="text" placeholder="Username/Email " autocomplete="off" name="user" id="id_smtp_usernam" class="form-control">

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <label for="">Password SMTP</label>
                                                <div class="form-group">
                                                    <div class="form-line">
                                                        <input type="text" placeholder="Password " autocomplete="off" name="password" id="id_smtp_password" class="form-control">

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-xs-12 col-sm-6">
                                                <label for="">Port SMTP</label>
                                                <div class="form-group">
                                                    <div class="form-line">
                                                        <input type="text" placeholder="Port " autocomplete="off" name="port" id="id_smtp_port" class="form-control">

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-6">
                                                <label for="">Secure SMTP</label>
                                                <div class="form-group">
                                                    <div class="form-line">
                                                        <select name="secure" id="id_smtp_secure" class="form-control show-tick"></select>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <!-- <div class="col-xs-12 align-right">
                                                <button type="submit" class="btn btn-lg btn-block btn-primary waves-effect " >SAVE</button>
                                            </div> -->
                                        </div>
                                    </form>
                                    <div class="row clearfix">
                                        <div class="col-xs-12 align-right">
                                            <button type="button" onclick="applySMTP()" class="btn btn-lg btn-block btn-primary waves-effect " >SAVE & APPLY</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--END CARD SMTP -->
                         </div>
                    </div>
                    <div class="row clearfix">
                         <div class="col-xs-12">
                            <div class="card">
                                <div class="header">
                                    <div class="row clearfix">
                                        <div class="col-xs-12 col-sm-6">
                                            <h2>Email Template Setting</h2>
                                        </div>
                                        <div class="col-xs-12 col-sm-6 align-right">
                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="body ">
                                    <div class="panel-group" id="accordion_17" role="tablist" aria-multiselectable="true">
                                        <div class="panel panel-col-pink">
                                            <div class="panel-heading" role="tab" id="headingOne_17">
                                                <h4 class="panel-title">
                                                    <a role="button" data-toggle="collapse" data-parent="#accordion_17" href="#collapseOne_17" aria-expanded="true" aria-controls="collapseOne_17">
                                                        <i class="material-icons">inbox</i> Template of invitation 
                                                    </a>
                                                </h4>
                                            </div>
                                            <div id="collapseOne_17" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne_17">
                                                <div class="panel-body">
                                                   <form id="id_frm_setting_temp_inv">
                                                        <label for="">Enabled Email Invitation</label>
                                                        <div class="form-group">
                                                            <select name="is_enabled" id="id_temp_inv_enable" class="form-control show-tick">
                                                            </select>
                                                        </div>
                                                        <label for="">Title Text</label>
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                                <input type="text" placeholder=" Title Text" autocomplete="off" name="title_of_text" id="id_temp_inv_title" class="form-control">

                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">To Text </label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="To Text" autocomplete="off" name="to_text" id="id_temp_inv_to" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Agenda Text</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Agenda Text " autocomplete="off" name="title_agenda_text" id="id_temp_inv_agenda" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Date Text </label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Date Text " autocomplete="off" name="date_text" id="id_temp_inv_date" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Room Text </label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Room Text " autocomplete="off" name="room" id="id_temp_inv_room" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Detail Location Text </label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Open Text Text " autocomplete="off" name="detail_location" id="id_temp_inv_detail" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Greeting Text</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Greeting Text" autocomplete="off" name="greeting_text" id="id_temp_inv_greeting" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <label for="">Content Text</label>
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                                <textarea id="id_temp_inv_content" name="content_text" rows="4" class="form-control no-resize" placeholder="Please type what you want..."></textarea>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Attendance Text Button</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Attendance Text Button" autocomplete="off" name="attendance_text" id="id_temp_inv_attendance" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">No Attendance Text Button</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="No Attendance Text Button" autocomplete="off" name="attendance_no_text" id="id_temp_inv_no_attendance" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Close Text</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Close Text" autocomplete="off" name="close_text" id="id_temp_inv_close" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Support Text</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Support Text" autocomplete="off" name="support_text" id="id_temp_inv_support" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <label for="">Footer Text</label>
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                              <input type="text" placeholder="Footer Text" autocomplete="off" name="foot_text" id="id_temp_inv_foot" class="form-control">
                                                            </div>
                                                        </div>
                                                        <label for="">Footer Url</label>
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                              <input type="text" placeholder="Footer Url" autocomplete="off" name="link" id="id_temp_inv_link" class="form-control">
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-6 align-right">
                                                                <button onclick="previewEmailFormat('id_frm_setting_temp_inv')" type="button" class="btn btn-lg btn-block btn-info waves-effect " >PREVIEW</button>
                                                            </div>
                                                            <div class="col-xs-6 align-right">
                                                                <button type="submit" class="btn btn-lg btn-block btn-primary waves-effect " >SAVE</button>
                                                            </div>
                                                        </div>
                                                    </form>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-col-cyan">
                                            <div class="panel-heading" role="tab" id="headingTwo_17">
                                                <h4 class="panel-title">
                                                    <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion_17" href="#collapseTwo_17" aria-expanded="false"
                                                       aria-controls="collapseTwo_17">
                                                        <i class="material-icons">inbox</i>  Template of Reschedule 
                                                    </a>
                                                </h4>
                                            </div>
                                            <div id="collapseTwo_17" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo_17">
                                                <div class="panel-body">
                                                    <form id="id_frm_setting_temp_res">
                                                        <label for="">Enabled Email Reschedule</label>
                                                        <div class="form-group">
                                                            <select name="is_enabled" id="id_temp_res_enable" class="form-control show-tick">
                                                            </select>
                                                        </div>
                                                        <label for="">Title Text</label>
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                                <input type="text" placeholder=" Title Text" autocomplete="off" name="title_of_text" id="id_temp_res_title" class="form-control">

                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">To Text </label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="To Text" autocomplete="off" name="to_text" id="id_temp_res_to" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Agenda Text</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Agenda Text " autocomplete="off" name="title_agenda_text" id="id_temp_res_agenda" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Date Text </label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Date Text " autocomplete="off" name="date_text" id="id_temp_res_date" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Room Text </label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Room Text " autocomplete="off" name="room" id="id_temp_res_room" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Detail Location Text </label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Open Text Text " autocomplete="off" name="detail_location" id="id_temp_res_detail" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Greeting Text</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Greeting Text" autocomplete="off" name="greeting_text" id="id_temp_res_greeting" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <label for="">Content Text</label>
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                                <textarea id="id_temp_res_content" name="content_text" rows="4" class="form-control no-resize" placeholder="Please type what you want..."></textarea>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Attendance Text Button</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Attendance Text Button" autocomplete="off" name="attendance_text" id="id_temp_res_attendance" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">No Attendance Text Button</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="No Attendance Text Button" autocomplete="off" name="attendance_no_text" id="id_temp_res_no_attendance" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Close Text</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Close Text" autocomplete="off" name="close_text" id="id_temp_res_close" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Support Text</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Support Text" autocomplete="off" name="support_text" id="id_temp_res_support" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <label for="">Footer Text</label>
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                              <input type="text" placeholder="Footer Text" autocomplete="off" name="foot_text" id="id_temp_res_foot" class="form-control">
                                                            </div>
                                                        </div>
                                                        <label for="">Footer Url</label>
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                              <input type="text" placeholder="Footer Url" autocomplete="off" name="link" id="id_temp_res_link" class="form-control">
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-6 align-right">
                                                                <button onclick="previewEmailFormat('id_frm_setting_temp_res')" type="button" class="btn btn-lg btn-block btn-info waves-effect " >PREVIEW</button>
                                                            </div>
                                                            <div class="col-xs-6 align-right">
                                                                <button type="submit" class="btn btn-lg btn-block btn-primary waves-effect " >SAVE</button>
                                                            </div>
                                                        </div>
                                                    </form>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-col-teal">
                                            <div class="panel-heading" role="tab" id="headingThree_17">
                                                <h4 class="panel-title">
                                                    <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion_17" href="#collapseThree_17" aria-expanded="false"
                                                       aria-controls="collapseThree_17">
                                                        <i class="material-icons">inbox</i> Template of Cancel
                                                    </a>
                                                </h4>
                                            </div>
                                            <div id="collapseThree_17" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree_17">
                                                <div class="panel-body">
                                                    <form id="id_frm_setting_temp_cancel">
                                                        <label for="">Enabled Email Canceled</label>
                                                        <div class="form-group">
                                                            <select name="is_enabled" id="id_temp_cancel_enable" class="form-control show-tick">
                                                            </select>
                                                        </div>
                                                        <label for="">Title Text</label>
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                                <input type="text" placeholder=" Title Text" autocomplete="off" name="title_of_text" id="id_temp_cancel_title" class="form-control">

                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">To Text </label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="To Text" autocomplete="off" name="to_text" id="id_temp_cancel_to" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Agenda Text</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Agenda Text " autocomplete="off" name="title_agenda_text" id="id_temp_cancel_agenda" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Date Text </label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Date Text " autocomplete="off" name="date_text" id="id_temp_cancel_date" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Room Text </label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Room Text " autocomplete="off" name="room" id="id_temp_cancel_room" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Detail Location Text </label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Open Text Text " autocomplete="off" name="detail_location" id="id_temp_cancel_detail" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Greeting Text</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Greeting Text" autocomplete="off" name="greeting_text" id="id_temp_cancel_greeting" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <label for="">Content Text</label>
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                                <textarea id="id_temp_cancel_content" name="content_text" rows="4" class="form-control no-resize" placeholder="Please type what you want..."></textarea>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Attendance Text Button</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Attendance Text Button" autocomplete="off" name="attendance_text" id="id_temp_cancel_attendance" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">No Attendance Text Button</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="No Attendance Text Button" autocomplete="off" name="attendance_no_text" id="id_temp_cancel_no_attendance" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Close Text</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Close Text" autocomplete="off" name="close_text" id="id_temp_cancel_close" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <label for="">Support Text</label>
                                                                <div class="form-group">
                                                                    <div class="form-line">
                                                                        <input type="text" placeholder="Support Text" autocomplete="off" name="support_text" id="id_temp_cancel_support" class="form-control">

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <label for="">Footer Text</label>
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                              <input type="text" placeholder="Footer Text" autocomplete="off" name="foot_text" id="id_temp_cancel_foot" class="form-control">
                                                            </div>
                                                        </div>
                                                        <label for="">Footer Url</label>
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                              <input type="text" placeholder="Footer Url" autocomplete="off" name="link" id="id_temp_cancel_link" class="form-control">
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-xs-6 align-right">
                                                                <button onclick="previewEmailFormat('id_frm_setting_temp_cancel')" type="button" class="btn btn-lg btn-block btn-info waves-effect " >PREVIEW</button>
                                                            </div>
                                                            <div class="col-xs-6 align-right">
                                                                <button type="submit" class="btn btn-lg btn-block btn-primary waves-effect " >SAVE</button>
                                                            </div>
                                                        </div>
                                                    </form>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--END CARD EMAIL TEMPLATE -->
                         </div>
                    </div>
                    <!-- ROW EMAIL TEMPLATE -->
                </div>
            </div>
        </div>
    </section>
    <!-- # END MODAL CREATE  -->
    <?php $this->load->view("_partials/js_dashboard.php");?>
    <input type="hidden" id="id_baseurl" value="<?= base_url()?>">
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/sweatalert2/sweetalert2.all.min.js"></script>
    <!-- Moment Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment.js"></script>
    <!-- Bootstrap Material Datetime Picker Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js"></script>
    <script src="<?= base_url()?>assets/external/numeral/numeral.min.js"></script>
    <script src="<?= base_url()?>assets/external/numeral/locale.id.js"></script>
    <!-- Ckeditor -->
    <script src="<?= base_url()?>assets/theme/plugins/ckeditor/ckeditor.js"></script>
    <script src="<?= base_url()?>assets/process/setting/smtp_email.js"></script>
    <script>
        
    </script>
    </body>
</html>
