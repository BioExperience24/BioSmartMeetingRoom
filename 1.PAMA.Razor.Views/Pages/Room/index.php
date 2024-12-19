<?php  
// date_default_timezone_set("Asia/Jakarta");
// echo date('w');
// print_r($modules);
// die();
?>
<!DOCTYPE html>
<html lang="en">

<head>
    <?php $this->load->view("_partials/head_css_dashboard.php", array('pagename'=>$pagename));?>
    <link href="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <link href="<?= base_url()?>assets/theme/plugins/jquery-datatable/select.dataTables.min.css" rel="stylesheet">
    <link href="<?= base_url()?>assets/theme/plugins/jquery-datatable/fixedColumns.dataTables.min.css" rel="stylesheet">
    <link href="<?= base_url()?>assets/theme/plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet" />
    <link href="<?= base_url()?>assets/external/filepond/filepond.min.css" rel="stylesheet">
    <link href="<?= base_url()?>assets/external/dropify/css/dropify.min.css" rel="stylesheet">
    <style>
        .modal-dialog {
          position:absolute;
          top:50% !important;
          transform: translate(0, -50%) !important;
          -ms-transform: translate(0, -50%) !important;
          -webkit-transform: translate(0, -50%) !important;
          margin:auto 5%;
          width:90%;
          height:80%;
        }
        .modal-content {
          min-height:100%;
          position:absolute;
          top:0;
          bottom:0;
          left:0;
          right:0; 
        }
        .modal-body {
          position:absolute;
          top:45px; /** height of header **/
          bottom:45px;  /** height of footer **/
          left:0;
          right:0;
          overflow-y:auto;
        }
        .modal-footer {
          position:absolute;
          bottom:0;
          left:0;
          right:0;
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
            <div class="fab-right">
            </div>
            <div class="block-header">
                <h2>
                    <?= strtoupper($pagename) ?>
                </h2>
            </div>
            <!-- DIV  -->
            <div class="row clearfix">
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                    <div class="info-box hover-expand-effect">
                        <div class="icon  bg-orange ">
                            <i class="material-icons">event</i>
                        </div>
                        <div class="content">
                            <div class="text">
                                <?= strtoupper(date("d M Y"))?>
                            </div>
                            <div class="number count-to" id="time1">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                    <div class="info-box  hover-expand-effect">
                        <div class="icon bg-light-green">
                            <i class="material-icons">toc</i>
                        </div>
                        <div class="content">
                            <div class="text">TOTAL ROOM</div>
                            <div class="number count-to" data-from="0" data-to="0" data-speed="1000" data-fresh-interval="20" id="id_count_total">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="card">
                        <div class="header">
                            <div class="row clearfix">
                                <div class="col-xs-12 col-sm-6">
                                    <h2>Room Management</h2>
                                </div>
                                <div class="col-xs-12 col-sm-6 align-right">
                                    <button onclick="createData($(this))" type="button" class="btn btn-primary waves-effect"><i class="material-icons">add</i></button>
                                </div>
                            </div>
                        </div>
                        <div class="body ">
                            <div class="row clearfix">
                                <div class="col-xs-4">
                                    <div class="form-line">
                                        <input id="id_search" type="input" class="form-control" placeholder="Search room name" />
                                    </div>
                                </div>
                                <div class="col-xs-4">
                                    <button onclick="removeAll()" type="button" class="btn btn-sm btn-danger waves-effect"><i class="material-icons">delete</i> </button>
                                    &nbsp;
                                    <!-- <button onclick="" type="button" class="btn btn-lg btn-info waves-effect"><b>Update</b> </button> -->
                                </div>
                            </div>
                            <div class="table-responsive responsive">
                                <table class="table table-hover" id="tbldata">
                                    <thead>
                                        <th>
                                            <input type="checkbox" id="ck_all" class="filled-in chk-col-red selectAll" />
                                            <label for="ck_all"></label>
                                        </th>
                                        <th></th>
                                        <th>Thumbnail</th>
                                        <th>Name</th>
                                        <th>Building</th>
                                        <th>Capacity</th>
                                        <th>Working Time</th>
                                        <!-- <th>Work Day</th> -->
                                        <?php if ($modules['automation']['is_enabled'] == 1): ?>
                                        <th>Automation Active</th>
                                        <th>Automation</th>
                                        <?php else: ?>
                                        <?php endif; ?>
                                        <?php if ($modules['price']['is_enabled'] == 1): ?>
                                        <th>Price</th>
                                        <?php else: ?>
                                        <?php endif; ?>
                                        <?php if ($modules['int_365']['is_enabled'] == 1 || $modules['int_google']['is_enabled'] == 1): ?>
                                        <th>Integration</th>
                                        <?php else: ?>
                                        <?php endif; ?>
                                        <th>Status</th>
                                        <th style="width:320px">
                                        </th>
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
    <!--start section modal create -->
    <?php $this->load->view("Admin/Room/form.create.php", array() ) ?>
    <!--end section modal create -->
    <!--start section modal update -->
    <?php $this->load->view("Admin/Room/form.update.php", array() ) ?>
    <!--end section modal update -->
    <!--start section modal integration -->
    <?php $this->load->view("Admin/Room/form.integration.php", array() ) ?>
    <!--end section modal integration -->
    
    <!-- # END MODAL CREATE  -->
    <input type="hidden" id="id_baseurl" value="<?= base_url()?>">
    <textarea id="id_room_for_usage" style="display: none;"><?= $room_for_usage?></textarea>
    <textarea id="id_modules" style="display: none;"> <?= json_encode($modules)?></textarea>
    <textarea id="id_building" style="display: none;"> <?= json_encode($building)?></textarea>
    <textarea id="id_user_approval" style="display: none;"> <?= json_encode($user_approval)?></textarea>
    <textarea id="id_user_permission" style="display: none;"> <?= json_encode($user_permission)?></textarea>
    <textarea id="id_room_user_checkin" style="display: none;"> <?= json_encode($room_user_checkin)?></textarea>
    <textarea id="id_floor" style="display: none;"> <?= json_encode($floor)?></textarea>
    <?php $this->load->view("_partials/js_dashboard.php");?>
    <!-- Datatables -->
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/dataTables.select.min.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/dataTables.fixedColumns.min.js"></script>
    <!-- end Datatables -->
    <script src="<?= base_url()?>assets/theme/plugins/sweatalert2/sweetalert2.all.min.js"></script>
    <!-- Moment Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment.js"></script>
    <!-- Bootstrap Material Datetime Picker Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js"></script>
    <script src="<?= base_url()?>assets/external/numeral/numeral.min.js"></script>
    <script src="<?= base_url()?>assets/external/numeral/locale.id.js"></script>
    <script src="<?= base_url()?>assets/external/dropify/js/dropify.min.js"></script>
    <?php if (APP_PRODUCTION): ?>
    <script src="<?= base_url()?>assets/process/room/footer.js"></script>
    <script src="<?= base_url()?>assets/process/room/index.js"></script>
    <script src="<?= base_url()?>assets/process/room/form.js"></script>
    <?php else: ?>
    <script src="<?= base_url()?>assets/process/room/footer.js"></script>
    <script src="<?= base_url()?>assets/process/room/index.js"></script>
    <script src="<?= base_url()?>assets/process/room/form.js"></script>
    <?php endif ?>
    <script>
    </script>
</body>

</html>