
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
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div class="row clearfix">
                        <div class="col-xs-12">
                            <div class="card">
                                <div class="header">

                                    <div class="row clearfix">
                                        <div class="col-xs-12 col-sm-6">
                                            <h2>General Setting</h2>
                                        </div>
                                        <div class="col-xs-12 col-sm-6 align-right">
                            
                                        </div>
                                    </div>
                                </div>
                                <div class="body ">
                                    <form id="frm_save_general">
                                        <!-- <label for="">Automatically end of meeting time</label> -->
                                        <!-- <div class="form-group">
                                            <div class="form-line">
                                                <input maxlength="6"  type="number" name="max_end_meeting" id="id_max_end_meeting" class=" form-control" required="" placeholder="Max duration per minutes ...">
                                            </div>
                                        </div> -->
                                        <label for="">Room fines are not used</label>
                                        <div class="form-group">
                                            <select required="" name="unuse_cancel_fee" id="id_unuse_cancel_fee" class="form-control show-tick">
                                            </select>
                                            <small>Fines based on the calculation of usage time</small>
                                        </div>
                                        <label for="">Time of unused meeting OR Reminder before meeting </label>
                                        <div class="form-group">
                                            <div class="input-group" style="width: 100px">
                                                <div class="form-line" style="width: 100px">
                                                    <input maxlength="6"  type="number" name="notif_unused_meeting" id="id_notif_unused_meeting" class=" form-control"  required="" placeholder="Duration per minutes ...">
                                                </div>
                                                <span class="input-group-addon">mins</span>
                                            </div>
                                            <small>Cancel a meeting when no participant is present</small>
                                        </div>
                                        
                                        
                                        <label for="">Time access </label>
                                        <div class="form-group">
                                            <div class="input-group" style="width: 100px">
                                                <div class="form-line" style="width: 100px">
                                                    <input maxlength="6"  type="number" name="notif_unuse_before_meeting" id="id_notif_unuse_before_meeting" class=" form-control"  required="" placeholder="Duration per minutes ...">
                                                </div>
                                                <span class="input-group-addon">mins</span>
                                            </div>
                                            <small>Time access the room, notification a unsed meeting room, and notification meeting </small>
                                        </div>
                                        <hr>
                                        <label for="">Duration of meeting time</label>
                                        <div class="form-group">
                                            <select required="" disabled="" name="duration" id="id_booking_duration" class="form-control show-tick">
                                            </select>
                                        </div>
                                        <label for="">Max Duration of of display</label>
                                        <div class="form-group">
                                            <select required="" disabled="" name="max_display_duration" id="id_booking_max_dur" class="form-control show-tick">
                                            </select>
                                        </div>
                                        <?php if ($modules['access_door']['is_enabled'] == 1): ?>
                                        <label for="">Room pin activated</label>
                                        <div class="form-group">
                                            <select name="room_pin" id="id_booking_room_pin_activated" class="form-control show-tick">
                                               
                                            </select>
                                        </div>
                                        <label for="">Room pin number</label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <input maxlength="6"  type="number" name="room_pin_number" id="id_booking_pin_number" class=" form-control" required="" placeholder="Pin Number ...">
                                            </div>
                                        </div>
                                        <label for="">Extend meeting activated</label>
                                        <div class="form-group">
                                             <select name="extend_meeting" id="id_booking_extend_activated" class="form-control show-tick">
                                              
                                            </select>
                                            
                                        </div>
                                        <label for="">Extend time maximum</label>
                                        <div class="form-group">
                                            <select name="extend_meeting_max" disabled="" id="id_booking_extend_max" class="form-control show-tick">
                                            </select>
                                        </div>
                                        <label for="">Extend notification time</label>
                                        <!-- <div class="form-group">
                                            <div class="form-line">
                                                <input  type="number" name="extend_meeting_notification" id="id_booking_extend_notification"  class=" form-control" required="" placeholder=" Time Notification ...">
                                            </div>
                                        </div> -->
                                        <div class="input-group" style="width: 80px">
                                            <div class="form-line" style="width: 80px">
                                                <input  type="number" name="extend_meeting_notification" id="id_booking_extend_notification"  class=" form-control" required="" placeholder=" Time Notification ...">
                                            </div>
                                            <span class="input-group-addon">mins</span>

                                        </div>
                                        <?php endif; ?>
                                        <div class="row clearfix">
                                        <div class="col-xs-12 align-right">
                                            <button type="submit" class="btn btn-lg btn-block btn-primary waves-effect " >SAVE</button>
                                        </div>
                                       
                                    </div>
                                     </form>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end GENERAL SETTING -->
                    </div>
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                        <div class="row clearfix">
                            <div class="col-xs-12">
                                <div class="card">
                                    <div class="header">

                                        <div class="row clearfix">
                                            <div class="col-xs-12 col-sm-6">
                                                <h2>Pantry Setting</h2>
                                            </div>
                                            <div class="col-xs-12 col-sm-6 align-right">
                                                <div class="switch">
                                                    <label><input type="checkbox" onchange="oncPantryStatus()"  id="id_switch_pantry"><span class="lever switch-col-red"></span></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="body ">
                                        <form id="frm_save_pantry"> 
                                            <label for="">Expired Time </label>
                                            <div class="form-group">
                                                <div class="input-group" >
                                                    <div class="form-line" >
                                                        <input maxlength="4" type="number" name="pantry_expired" id="id_pantry_expired" class=" form-control"  required="" placeholder="">
                                                    </div>
                                                    <span class="input-group-addon">mins</span>
                                                </div>
                                                <small>Orders expired when the order is not taken within a certain time period</small>
                                            </div>
                                            <!--  -->
                                            <label for="">Max Order Qty Time </label>
                                            <div class="form-group">
                                                <div class="input-group" style="">
                                                    <div class="form-line" style="">
                                                        <input maxlength="2"  type="number" name="max_order_qty" id="id_pantry_max_order_qty" class=" form-control"  required="" placeholder="">
                                                    </div>
                                                    <span class="input-group-addon">mins</span>
                                                </div>
                                                <small>Maximum qty that can be ordered on each item. <b class="font-italic font-bold col-red">*note : entry 0 for infinite order qty</b></small>
                                            </div>
                                            <!--  -->
                                            <div class="row clearfix">
                                                <div class="col-xs-12 align-right">
                                                    <button type="submit" class="btn btn-lg btn-block btn-primary waves-effect " >SAVE</button>
                                                </div>
                                            </div>
                                        </form>
                                        <!-- FORM -->
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end PANTRY SETTING -->
                     <!--
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                        <div class="row clearfix">
                            <div class="col-xs-12">
                                <div class="card">
                                    <div class="header">

                                        <div class="row clearfix">
                                            <div class="col-xs-12 col-sm-6">
                                                <h2>Invoice Setting</h2>
                                            </div>
                                            <div class="col-xs-12 col-sm-6 align-right">
                                
                                            </div>
                                        </div>
                                    </div>
                                    <div class="body ">
                                        <form id="frm_save_invoice">
                                            <label for="">Date Format</label>
                                            <div class="form-group">
                                                <select required="" name="date_format" id="id_inv_date_format" class="form-control show-tick">
                                                    <?php foreach ($invoice_date_format as $key => $value): ?>
                                                        <?php if($value == $invConfig['date_format']) {?> 
                                                        <option selected="" value="<?= $value?>"><?= $invoice_date_format_text[$key]?></option>  
                                                        <?php }else{ ?>
                                                        <option value="<?= $value?>"><?= $invoice_date_format_text[$key]?></option>  
                                                        <?php } ?>
                                                    <?php endforeach ?>
                                                </select>
                                            </div>
                                            <label for="">Date Text</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input value="<?= @$invConfig['date_text']?>" type="text" name="date_text" id="id_inv_date_text" class=" form-control"  placeholder="">
                                                </div>
                                                
                                            </div>
                                            <label for="">To Text</label>
                                            <div class="form-group">
                                                 <div class="form-line">
                                                    <input  value="<?= @$invConfig['to_text']?>" type="text" name="to_text" id="id_inv_to_text" class=" form-control"  placeholder="">
                                                </div>
                                            </div>
                                            <label for="">Up Text</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input  value="<?= @$invConfig['up_text']?>" t type="text" name="up_text" id="id_inv_up_text" class=" form-control"  placeholder="">
                                                </div>
                                            </div>
                                            <label for="">Profit Text</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input value="<?= @$invConfig['no_profit_text']?>"  type="text" name="no_profit_text" id="id_inv_no_profit_text" class=" form-control" placeholder="">
                                                </div>
                                            </div>
                                            
                                            <label for="">Description Text</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input  value="<?= @$invConfig['description_text']?>" type="text" name="description_text" id="id_inv_description_text " class=" form-control" placeholder="">
                                                </div>
                                            </div>
                                            <label for="">Amount Text</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input value="<?= @$invConfig['amount_text']?>"  type="text" name="amount_text" id="id_inv_amount_text" class=" form-control" placeholder="">
                                                </div>
                                            </div>
                                            <label for="">Content Text</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <textarea name="content_text" id="id_inv_content_text"><?= @$invConfig['content_text']?></textarea>
                                                </div>
                                            </div>
                                            <label for="">Amount Text</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input value="<?= @$invConfig['amount_bill_text']?>"  type="text" name="amount_bill_text" id="id_inv_amount_bill_text" class=" form-control" placeholder="">
                                                </div>
                                            </div>
                                            <label for="">Tax Text</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input  value="<?= @$invConfig['tax_text']?>" type="text" name="tax_text" id="id_inv_tax_text" class=" form-control" placeholder="">
                                                </div>
                                            </div>
                                            <label for="">Tax Amount</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input  value="<?= @$invConfig['tax_amount']?>"  type="number" min="0" max="100" maxlength="3" name="tax_amount" id="id_inv_tax_amount" class=" form-control" placeholder="">
                                                </div>
                                            </div>
                                            <label for="">Amount Text</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input  value="<?= @$invConfig['total_text']?>" type="text" name="total_text" id="id_inv_total_text" class=" form-control" placeholder="">
                                                </div>
                                            </div>
                                            <label for="">Footer1 Text</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <textarea  class="form-control" name="footer_text" id="id_inv_footer_text" rows="10"><?= @$invConfig['footer_text']?></textarea>
                                                </div>
                                            </div>
                                            <label for="">Footer2 Text</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input  value="<?= @$invConfig['footer2_text']?>"  type="text" name="footer2_text" id="id_inv_footer2_text" class=" form-control" placeholder="">
                                                </div>
                                            </div>
                                            <label for="">Footer3 Text</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input value="<?= @$invConfig['footer3_text']?>" type="text" name="footer3_text" id="id_inv_footer3_text" class=" form-control" placeholder="">
                                                </div>
                                            </div>
                                            <div class="row clearfix">
                                            <div class="col-xs-12 align-right">
                                                <button type="submit" class="btn btn-lg btn-block btn-primary waves-effect " >SAVE</button>
                                            </div>
                                           
                                        </div>
                                         </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                
                </div>
                -- >
                <!-- </div> -->
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
    <!-- <script src="<?= base_url()?>assets/external/numeral/numeral.min.js"></script> -->
    <!-- <script src="<?= base_url()?>assets/external/numeral/locale.id.js"></script> -->
    <!-- Ckeditor -->
    <script src="<?= base_url()?>assets/theme/plugins/ckeditor/ckeditor.js"></script>
    <script src="<?= base_url()?>assets/process/setting/general.js"></script>
    <script>
        
    </script>
    </body>
</html>