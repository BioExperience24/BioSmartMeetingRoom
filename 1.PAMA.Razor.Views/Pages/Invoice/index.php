
<?php  
date_default_timezone_set("Asia/Jakarta");
// echo date('w');
// echo $statusInvoiceJson;
// die();
$nbulan = array("", "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember");
$year = date("Y") -0;
$yearbefore = 2019;
?>
<!DOCTYPE html>
<html lang="en">
<head>
   <?php $this->load->view("_partials/head_css_dashboard.php", array('pagename'=>$pagename));?>
    <link href="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <link href="<?= base_url()?>assets/theme/plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet" />
   <link href="<?= base_url()?>assets/external/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <style>
        .change{
            -webkit-transition: all 0.5s ease;
            -moz-transition: all 0.5s ease;
            -o-transition: all 0.5s ease;
            transition: all 0.5s ease;
        }
    </style>
</head>
<body class="theme-red">
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
                <div class="change col-xs-12 col-sm-12 col-md-12 col-lg-12" id="id_row1">
                    <div class="card">
                        <div class="header">
                           <h2>GENERATE INVOICE</h2>
                        </div>
                        <div class="body">
                            <form class="form-horizontal" id="frm_generate">
                            <div class="row clearfix">
                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                    <div class="form-group form-float">
                                        <div class="form-line">
                                             <select required="" show-tick name="invoice_month1" id="id_generate_alocation_period1" class="form-control ">
                                                <option value="">PERIOD 1</option>
                                                <?php for($ii = 1;$ii< count($nbulan);$ii++) { ?>
                                                <option value="<?= $ii ?>"><?= $nbulan[$ii] ?>
                                                </option>
                                                <?php } ?>
                                            </select>
                                        </div>
                                    </div>
                                 
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                    <div class="form-group form-float">
                                        <div class="form-line">
                                             <select required="" show-tick name="invoice_month2" id="id_generate_alocation_period2" class="form-control ">
                                                <option value="">PERIOD 2</option>
                                                <?php for($nn = 1;$nn< count($nbulan);$nn++) { ?>
                                                <option value="<?= $nn ?>"><?= $nbulan[$nn] ?>
                                                </option>
                                                <?php } ?>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                    <div class="form-group form-float">
                                        <div class="form-line">
                                             <select required="" data-live-search="true"  show-tick name="invoice_years" id="id_generate_alocation_year" class="form-control ">
                                                <option value="">YEAR</option>
                                                <?php for($yy = $year ;$yy >= $yearbefore ;$yy--) { ?>
                                                <option value="<?= $yy ?>"><?= $yy ?>
                                                </option>
                                                <?php } ?>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <button class="btn btn-block btn-primary  waves-effect" >Generate Invoice</button>
                                </div>
                            </div>
                            <div class="row clearfix">
                               
                            </div>
                            <!-- row1 -->
                            </form>
                            <!-- form -->
                        </div>
                        <!-- body -->
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="change col-xs-12 col-sm-12 col-md-12 col-lg-12" id="id_row1">
                    <div class="card">
                        <div class="header">
                            <div class="row clearfix">
                                <div class="col-xs-12 col-sm-6">
                                    <h2>Invoice Meeting Order</h2>
                                </div>
                                <div class="col-xs-12 col-sm-6 align-right">
                        
                                </div>
                            </div>
                        </div>
                        <div class="body">
                            <div class="row clearfix">
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <select data-live-search="true" show-tick name="" id="id_filter_alocation" class="form-control ">
                                                <option value="">CHOOSE ALOCATION</option>
                                                <?php foreach ($alocation as $akey => $aval): ?>
                                                    <option data-subtext="<?= $aval['name_type']?>" value="<?= $aval['id']?>"><?= $aval['name']?>
                                                    </option>
                                                <?php endforeach ?>
                                            </select>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <select data-live-search="true" show-tick name="" id="id_filter_status_invoice" class="form-control ">
                                                <option value="">STATUS INVOICE</option>
                                                <?php foreach ($statusInvoice as $skey => $sval): ?>
                                                    <option  value="<?= $sval['id']?>"><?= $sval['name']?>
                                                    </option>
                                                <?php endforeach ?>
                                            </select>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="material-icons">date_range</i>
                                                </span>
                                                <div class="form-line">
                                                    <input autocomplete="off" id="id_daterange" type="text" class="form-control ">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                         <button  class="btn btn-block  btn-primary waves-effect" onclick="filterInvoice()">Filter Invoice</button>
                                        </div>
                            </div>
                            <div class="row clearfix">
                                <div class="col-xs-4">
                                    <button  class="btn btn-success " onclick="alertExportToAll('excell')">Export to Excell</button>
                                </div>
                            </div>
                            <table class="table table-hover" id="tbldata">
                                <thead>
                                    <th>#</th>
                                    <th>Action</th>
                                    <th>INVOICE No.</th>
                                    <th>Period</th>
                                    <th>Year</th>
                                    <th>Total Duration</th>
                                    <th>Total Cost</th>
                                    <th>Total Meeting</th>
                                    <th>Alocation</th>
                                    <th>Corporate Finance Check</th>
                                    <th>Status Invoicing</th>
                                </thead>    
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <!-- row 2 -->
                <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6" id="id_row2" style="display: none;">
                    <div class="card">
                        <div class="header">
                            <div class="row clearfix">
                                <div class="col-xs-3 col-sm-3">
                                     <button onclick="closeDetail()" type="button" class="btn btn-default  waves-effect">CLOSE</button>

                                </div>
                                <div class="col-xs-9 col-sm-9 align-right">
                                    <h2>Invoice Alocation at <b id="id_years_detail"></b></h2>
                                </div>
                            </div>
                        </div>
                        <div class="body table-responsive" >
                            <br>
                            <div class="table-responsive">
                                <table class="table table-hover" id="tbldataRow2">
                                    <thead>
                                        <th>#</th> 
                                        <th>ID</th> 
                                        <th></th> 
                                        <th>Status</th>
                                        <th>Invoice</th>
                                        <th>Alocation</th>
                                        <th>Total Cost</th>
                                        <th>Total Duration</th>
                                        <th>Total Meeting</th>
                                        <th></th>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                            
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </section>
    <!-- <div class="modal fade" id="id_mdl_detail" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="idmdlcrLabel">Detail of Alocation <b id="id_mdl_detail_alocation"></b> <div class="col-xs-6 align-left">
                                    <button onclick="exportDetailAlocation()" type="button" class="btn btn-primary waves-effect" >EXPORT XLS</button>
                                </div></h4>
                        </div>
                        <div class="modal-body table table-responsive " id="id_mdl_detail_body">
                           <table class="table table-hover" id="tbldata_detail">
                                        <thead>
                                                <th>ID</th>
                                                <th>Title</th>
                                                <th>PIC</th>
                                                <th>Date</th>
                                                <th>Time</th>
                                                <th>Duration</th>
                                                <th>Location</th>
                                                <th>Alocation</th>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                </table>
                        </div>
                        <div class="modal-footer">
                            <div class="row clearfix">
                                <div class="col-xs-6 align-left">
                                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                                </div>
                                <div class="col-xs-6 align-right">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div> -->
        <div class="modal fade" id="id_mdl_create" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="id_mdl_createLabel">Send Invoice to Finance  #<b id="id_id_mdl_createTitle"></b></h4>
                        </div>
                        <div class="modal-body table table-responsive " id="id_mdl_create_body">
                           <form class="form-horizontal" id="frm_publish">
                                <div class="row clearfix">
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-5 form-control-label">
                                        <label for="id_frm_crt_memo">Memo No</label>
                                    </div>
                                    <div class="col-lg-10 col-md-10 col-sm-8 col-xs-7">
                                        <div class="form-group">
                                            <div class="form-line">
                                                <input name="invoice_id" type="hidden" id="id_frm_crt_id" class="form-control" placeholder="Enter Memo">
                                                <input autocomplete="off" name="memo" type="text" id="id_frm_crt_memo" class="form-control" placeholder="Enter Memo">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-5 form-control-label">
                                        <label for="id_frm_crt_ref">Refrensi No</label>
                                    </div>
                                    <div class="col-lg-10 col-md-10 col-sm-8 col-xs-7">
                                        <div class="form-group">
                                            <div class="form-line">
                                                <input autocomplete="off" name="refrensi" type="text" id="id_frm_crt_ref" class="form-control" placeholder="Enter Refrensi No">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                </div>
                                <div class="row clearfix">
                                    <div class="col-lg-offset-2 col-md-offset-2 col-sm-offset-4 col-xs-offset-5">
                                        <button type="submit" id="id_frm_crt_btn" style="display: none;" class="btn btn-primary m-t-15 waves-effect">Send</button>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <div class="row clearfix">
                                <div class="col-xs-6 align-left">
                                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                                </div>
                                <div class="col-xs-6 align-right">
                                    <button onclick="clickSubmit('id_frm_crt_btn')" type="button" class="btn btn-primary waves-effect " >S E N D</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    <!-- # END MODAL CREATE  -->
        <div class="modal fade" id="id_mdl_confirm" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="id_mdl_confirmLabel">Confirm Paid Invoice from Finance #<b id="id_id_mdl_confirmTitle"></b></h4>
                        </div>
                        <div class="modal-body table table-responsive " id="id_mdl_confirm_body">
                           <form class="form-horizontal" id="frm_confirm">
                                <div class="row clearfix">
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-5 form-control-label">
                                        <label for="id_frm_conf_memo">Memo No</label>
                                    </div>
                                    <div class="col-lg-10 col-md-10 col-sm-8 col-xs-7">
                                        <div class="form-group">
                                            <div class="form-line">
                                                <input autocomplete="off" name="invoice_id" type="hidden" id="id_frm_conf_id" class="form-control" placeholder="Enter Memo">
                                                <input autocomplete="off" name="memo" type="text" id="id_frm_conf_memo" class="form-control" placeholder="Enter Memo">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-5 form-control-label">
                                        <label for="id_frm_conf_ref">Refrensi No</label>
                                    </div>
                                    <div class="col-lg-10 col-md-10 col-sm-8 col-xs-7">
                                        <div class="form-group">
                                            <div class="form-line">
                                                <input name="refrensi" type="text" id="id_frm_conf_ref" class="form-control" placeholder="Enter Refrensi No">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                </div>
                                <div class="row clearfix">
                                    <div class="col-lg-offset-2 col-md-offset-2 col-sm-offset-4 col-xs-offset-5">
                                        <button type="submit" id="id_frm_conf_btn" style="display: none;" class="btn btn-primary m-t-15 waves-effect">CONFIRM</button>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <div class="row clearfix">
                                <div class="col-xs-6 align-left">
                                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                                </div>
                                <div class="col-xs-6 align-right">
                                    <button onclick="clickSubmit('id_frm_conf_btn')" type="button" class="btn btn-success waves-effect " >C O N F I R M &nbsp;&nbsp;P A I D</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    <!-- # END MODAL CREATE  -->
    <input type="hidden" id="id_baseurl" value="<?= base_url()?>">
    <textarea  id="id_settinggeneral" style="display: none;"><?= $settinggeneral?></textarea>
    <textarea  id="id_statusname" style="display: none;"><?= $statusname?></textarea>
    <textarea  id="id_statusInvoice" style="display: none;"><?= $statusInvoiceJson?></textarea>
    <?php $this->load->view("_partials/js_dashboard.php");?>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/sweatalert2/sweetalert2.all.min.js"></script>
    <!-- Moment Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/jquery-validation/jquery.validate.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment.js"></script>
    <script src="<?= base_url()?>assets/external/daterangepicker/daterangepicker.js"></script>
    <script src="<?= base_url()?>assets/external/numeral/numeral.min.js"></script>
    <script src="<?= base_url()?>assets/external/numeral/locale.id.js"></script>
    <script src="<?= base_url()?>assets/process/report/invoice.js"></script>
    <!-- base url -->
    <script>
       
    </script>
    </body>
</html>
