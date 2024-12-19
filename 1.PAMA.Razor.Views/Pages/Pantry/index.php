<!DOCTYPE html>
<html lang="en">
<head>
   <?php $this->load->view("_partials/head_css_dashboard.php", array('pagename'=>$pagename));?>
    <link href="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <link href="<?= base_url()?>assets/theme/plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet" />
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
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="card">
                                <div class="header">

                                    <div class="row clearfix">
                                        <div class="col-xs-12 col-sm-6">
                                            <h2>Tenant List</h2>
                                        </div>
                                        <div class="col-xs-12 col-sm-6 align-right">
                            
                                        </div>
                                    </div>
                                </div>
                                <div class="body table-responsive responsive">
                                    <table class="table table-hover" id="tbldata">
                                        <thead>
                                                <th style="width: 200px;">No</th>
                                                <th style="width: 200px;">Name</th>
                                                <th style="width: 200px;">Image</th>
                                                <th>
                                                    <button 
                                                     onclick="createData($(this))" 
                                                     type="button" class="btn btn-primary waves-effect ">
                                                     <i class="material-icons">add_circle</i> CREATE</button>
                                                </th>
                                                <th></th>
                                        </thead>
                                        <tbody>
                                                   
                                        </tbody>
                                        </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="card">
                                <div class="header">
                                    <div class="row clearfix">
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <h2>Menu Kategori Variant <b id="id_variant_name"></b></h2>
                                        </div>
                                        <div class="col-xs-6 col-sm-6  col-md-6 col-lg-6 align-right" id="id_dataVariant_back" >

                                        </div>
                                    </div>
                                </div>
                                <div class="body" id="id_dataVariant" >
                                    <div class="row clearfix">
                                         <div class="col-xs-12 col-md-12 col-md-4 col-lg-4">
                                            <select class="form-control" id="id_select_variant">
                                                <option value=""></option>
                                            </select>
                                        </div>
                                    </div>
                                    <br>
                                    <div class="row clearfix ">
                                        <div class="col-xs-12 table-responsive responsive">
                                            <table class="table table-hover" id="tbldataVariant">
                                            <thead>
                                                    <th style="width: 80;" >#</th>
                                                    <th style="width: 200px;" >Name</th>
                                                    <th style="width: 200px;" >Multiple</th>
                                                    <th>
                                                        <button 
                                                         onclick="createDataVariant($(this))" 
                                                         type="button" class="btn btn-primary waves-effect ">
                                                         <i class="material-icons">add_circle</i> CREATE</button>
                                                    </th>
                                            </thead>
                                            <tbody></tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <!-- row -->
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- row -->
                </div>
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div class="row clearfix">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="card">
                                <div class="header">
                                    <div class="row clearfix">
                                        <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                            <h2>Item Menu <b id="id_pantry_name"></b></h2>
                                        </div>
                                        <div class="col-xs-6 col-sm-6  col-md-6 col-lg-6 align-right" id="id_dataMenu_back" >
                                            <button 
                                                     onclick="closeMenu($(this))" 
                                                     type="button" class="btn btn-default waves-effect ">
                                                    CLOSE</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="body table-responsive responsive" id="id_dataMenu" >
                                    <table class="table table-hover" id="tbldataMenu">
                                        <thead>
                                                <th style="width: 80px;">#</th>
                                                <th>Image</th>
                                                <th style="width: 200px;">Name</th>
                                                <th style="width: 200px;">Prefix</th>
                                                <th>
                                                    <button 
                                                     onclick="createDataMenu($(this))" 

                                                     type="button" class="btn btn-primary waves-effect ">
                                                     <i class="material-icons">add_circle</i> CREATE</button>
                                                </th>
                                        </thead>
                                        <tbody></tbody>
                                        </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <!-- row -->
                </div>
                <!-- Row -->
                
            </div>
            <!-- row -->
        </div>
    </section>
   
    <div class="modal fade" id="id_mdl_create" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="idmdlcrLabel">Create Tenant</h4>
                        </div>
                        <div class="modal-body " id="id_mdl_create_body">
                            <form id="frm_create" method="POST"  enctype="multipart/form-data" >
                                <label for="">Name</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="text" autocomplete="off" name="name" id="id_crt_name" required=""  class="form-control" placeholder="Name Tenant">
                                    </div>
                                </div>
                                <label for="">
                                    Order Days 
                                    <a data-toggle="tooltip" data-placement="bottom" title="" data-original-title="If this is filled in more than 0, then automatically ordering a pantry/tenant/kitchen requires a minimum preparation time that has been entered, for example 1 day before ordering.">(?)</a>
                                </label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="text" autocomplete="off" name="days" id="id_crt_days" required=""  class="form-control" placeholder="Order Days">
                                    </div>
                                </div>
                                <label for="">Image</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input  type="file" accept="image/png, image/jpeg, image/jpg" autocomplete="off" name="pic" id="id_crt_pic" class="form-control" placeholder="Name Pantry">
                                    </div>
                                </div>
                                <button type="submit" id="id_crt_btn" style="display: none;" class="btn btn-primary m-t-15 waves-effect">Submit</button>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <div class="row clearfix">
                                <div class="col-xs-6 align-left">
                                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                                </div>
                                <div class="col-xs-6 align-right">
                                    <button onclick="clickSubmit('id_crt_btn')" type="button" class="btn btn-primary waves-effect " >S A V E</button>
                                </div>
                            </div>
                            <!-- <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button> -->
                        </div>
                    </div>
                </div>
            </div>
    <div class="modal fade" id="id_mdl_update" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="idmdlcrLabel">Update Pantry</h4>
                        </div>
                        <div class="modal-body " id="id_mdl_create_body">
                            <form id="frm_update_pantry" method="POST"  enctype="multipart/form-data" >
                                <label for="">Name</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="text" autocomplete="off" name="name" id="id_edt_name" required=""  class="form-control" placeholder="Name Pantry">
                                        <input type="hidden" autocomplete="off" name="id" id="id_edt_id" required="" >
                                    </div>
                                </div>
                                <label for="">
                                    Order Days 
                                    <a data-toggle="tooltip" data-placement="bottom" title="" data-original-title="If this is filled in more than 0, then automatically ordering a pantry/tenant/kitchen requires a minimum preparation time that has been entered, for example 1 day before ordering.">(?)</a>
                                </label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="text" autocomplete="off" name="days" id="id_edt_days" required=""  class="form-control" placeholder="Order Days">
                                    </div>
                                </div>
                                
                                <label for="">Image</label>
                                <div class="row clearfix">
                                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 align-right">
                                        <img src="" id="id_edt_pic_preview" style="height: 80px;width: 120px;">
                                    </div>
                                    <div class="col-xs-8 col-sm-8 col-md-8 col-lg-8 ">
                                        <div class="form-group">
                                            <div class="form-line">
                                                <input  type="file" accept="image/png, image/jpeg, image/jpg" autocomplete="off" name="pic" id="id_edt_pic" class="form-control" placeholder="Name Pantry">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <button type="submit" id="id_edt_btn" style="display: none;" class="btn btn-primary m-t-15 waves-effect">Submit</button>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <div class="row clearfix">
                                <div class="col-xs-6 align-left">
                                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                                </div>
                                <div class="col-xs-6 align-right">
                                    <button onclick="clickSubmit('id_edt_btn')" type="button" class="btn btn-primary waves-effect " >S A V E</button>
                                </div>
                            </div>
                            <!-- <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button> -->
                        </div>
                    </div>
                </div>
            </div>
    <div class="modal fade" id="id_mdl_create_menu" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="id_mdl_create_menu_lbl">Create Menu</h4>
                        </div>
                        <div class="modal-body " id="id_mdl_create_menu_body">
                            <form id="frm_create_menu" method="POST"  enctype="multipart/form-data">
                                <label for="">Name</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="hidden" autocomplete="off" name="pantry_id" id="id_crt_menu_pantry_id" required=""  >
                                        <input type="text" autocomplete="off" name="name" id="id_crt_menu_name" required=""  class="form-control" placeholder="Menu Name">
                                    </div>
                                </div>
                                <label for="">Prefix</label>
                                <div class="form-group">
                                   <select name="prefix_id" id="id_crt_menu_prefix" class="form-control show-tick"></select>
                                </div>
                                <label for="">Description</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <textarea id="id_crt_menu_description" name="description" class="form-control"></textarea>
                                    </div>
                                </div>
                                <label for="">Image</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input  type="file" accept="image/png, image/jpeg, image/jpg" autocomplete="off" name="pic" id="id_crt_menu_pic" class="form-control" >
                                    </div>
                                </div>
                                <br>
                                <button type="submit" id="id_crt_menu_btn" style="display: none;" class="btn btn-primary m-t-15 waves-effect">Submit</button>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <div class="row clearfix">
                                <div class="col-xs-6 align-left">
                                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                                </div>
                                <div class="col-xs-6 align-right">
                                    <button onclick="clickSubmit('id_crt_menu_btn')" type="button" class="btn btn-primary waves-effect " >S A V E</button>
                                </div>
                            </div>
                            <!-- <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button> -->
                        </div>
                    </div>
                </div>
            </div>

    <div class="modal fade" id="id_mdl_update_menu" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="id_mdl_update_menu_lbl">Update Menu</h4>
                        </div>
                        <div class="modal-body " id="id_mdl_create_menu_body">
                            <form id="frm_update_menu" method="POST"  enctype="multipart/form-data">
                                <label for="">Name</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="hidden" autocomplete="off" name="pantry_id" id="id_edt_menu_pantry_id" required=""  >
                                        <input type="hidden" autocomplete="off" name="id" id="id_edt_menu_id" required=""  >
                                        <input type="text" autocomplete="off" name="name" id="id_edt_menu_name" required=""  class="form-control" placeholder="Menu Name">
                                    </div>
                                </div>
                                <label for="">Prefix</label>
                                <div class="form-group">
                                   <select name="prefix_id" id="id_edt_menu_prefix" class="form-control show-tick"></select>
                                </div>
                                <label for="">Description</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <textarea id="id_edt_menu_description" name="description" class="form-control"></textarea>
                                    </div>
                                </div>
                                <!--  -->
                                <label for="">Image</label>
                                <div class="row clearfix">
                                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 align-right">
                                       <img src="" id="id_edt_menu_pic_preview" style="height: 80px;width: 120px;">
                                    </div>
                                    <div class="col-xs-8 col-sm-8 col-md-8 col-lg-8 ">
                                        <div class="form-group">
                                            <div class="form-line">
                                                <input  type="file" accept="image/png, image/jpeg, image/jpg" autocomplete="off" name="pic" id="id_edt_menu_pic" class="form-control" >
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--  -->
                                <br>
                                <div id="id_edt_menu_btn_html"></div>
                                <button type="submit" id="id_edt_menu_btn" style="display: none;" class="btn btn-primary m-t-15 waves-effect">Submit</button>
                            </form>
                        </div>
                         <div class="modal-footer">
                            <div class="row clearfix">
                                <div class="col-xs-6 align-left">
                                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                                </div>
                                <div class="col-xs-6 align-right">
                                    <button onclick="clickSubmit('id_edt_menu_btn')" type="button" class="btn btn-primary waves-effect " >S A V E</button>
                                </div>
                            </div>
                            <!-- <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button> -->
                        </div>
                    </div>
                </div>
            </div>
    <div class="modal fade" id="id_mdl_create_variant" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" >Create Variant <font id="id_mdl_create_variant_lbl"></font></h4>
                        </div>
                        <div class="modal-body " id="id_mdl_create_menu_body">
                            <form id="frm_create_variant" >
                                <label for="">Name</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="hidden" autocomplete="off" name="menu_id" id="id_crt_variant_menu_id" required=""  >
                                        <input type="text" autocomplete="off" name="name" id="id_crt_variant_name" required=""  class="form-control" placeholder="Variant Name">
                                    </div>
                                </div>
                                <label for="">Select Rule</label>
                                <div class="form-group">
                                    <select required="" class="form-control show-tick" id="id_crt_variant_rule" name="" data-live-search="true" onchange="oncCrtVariantMenuRule()">
                                        <option value="1">1 Option </option>
                                        <option value="2">Multiple Option </option>
                                    </select>
                                </div>
                                <label for="">Variant Menu</label>
                                <div class="form-group">
                                    <div class="row clearfix">
                                        <div class="col-xs-6">
                                            <div class="form-line">
                                                <input type="text" autocomplete="off" name="name" id="id_crt_variant_menu" class="form-control" placeholder="Variant Name">
                                            </div>
                                        </div>
                                        <div class="col-xs-1">
                                            <button type="button" onclick="oncCrtAddVariantMenu()" class="btn btn-info waves-effect">ADD</button>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <table class="table table-bordered" id="tblVariantMenu">
                                        <thead>
                                            <tr>
                                                <th style="width: 50px;">#</th>
                                                <th>Name</th>
                                                <th style="width: 150px;">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                                <label for="">Minimal/Maximal Option </label>
                                <div class="form-group">
                                    <div class="row clearfix">
                                        <div class="col-xs-6">
                                            <div class="form-line">
                                                <input disabled type="number"  autocomplete="off" name="min" id="id_crt_variant_min" class="form-control" placeholder="Variant Min">
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-line">
                                                <input disabled type="number" autocomplete="off" name="max" id="id_crt_variant_max" class="form-control" placeholder="Variant Max">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <br>
                                <br>
                                <button type="submit" id="id_crt_variant_btn" style="display: none;" class="btn btn-primary m-t-15 waves-effect">Submit</button>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <div class="row clearfix">
                                <div class="col-xs-6 align-left">
                                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                                </div>
                                <div class="col-xs-6 align-right">
                                    <button onclick="clickSubmit('id_crt_variant_btn')" type="button" class="btn btn-primary waves-effect " >S A V E</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    <div class="modal fade" id="id_mdl_update_variant" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" >Update Variant <font id="id_mdl_update_variant_lbl"></font></h4>
                        </div>
                        <div class="modal-body " id="id_mdl_update_menu_body">
                            <form id="frm_update_variant" >
                                <label for="">Name</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="hidden" autocomplete="off" name="menu_id" id="id_edt_variant_menu_id" required=""  >
                                        <input type="hidden" autocomplete="off" name="menu_id" id="id_edt_variant_id" required=""  >
                                        <input type="text" autocomplete="off" name="name" id="id_edt_variant_name" required=""  class="form-control" placeholder="Variant Name">
                                    </div>
                                </div>
                                <label for="">Select Rule</label>
                                <div class="form-group">
                                    <select required="" class="form-control show-tick" id="id_edt_variant_rule" name="" data-live-search="true" onchange="oncCrtUpdateVariantMenuRule()">
                                        <option value=""></option>
                                    </select>
                                </div>
                                <label for="">Variant Menu</label>
                                <div class="form-group">
                                    <div class="row clearfix">
                                        <div class="col-xs-6">
                                            <div class="form-line">
                                                <input type="text" autocomplete="off" name="name" id="id_edt_variant_menu" class="form-control" placeholder="Variant Name">
                                            </div>
                                        </div>
                                        <div class="col-xs-1">
                                            <button type="button" onclick="oncCrtUpdateVariantMenu()" class="btn btn-info waves-effect">ADD</button>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <table class="table table-bordered" id="tblVariantUpdateMenu">
                                        <thead>
                                            <tr>
                                                <th style="width: 50px;">#</th>
                                                <th>Name</th>
                                                <th style="width: 150px;">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                                <label for="">Minimal/Maximal Option </label>
                                <div class="form-group">
                                    <div class="row clearfix">
                                        <div class="col-xs-6">
                                            <div class="form-line">
                                                <input disabled type="number"  autocomplete="off" name="min" id="id_edt_variant_min" class="form-control" placeholder="Variant Min">
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-line">
                                                <input disabled type="number" autocomplete="off" name="max" id="id_edt_variant_max" class="form-control" placeholder="Variant Max">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <br>
                                <br>
                                <button type="submit" id="id_edt_variant_btn" style="display: none;" class="btn btn-primary m-t-15 waves-effect">Submit</button>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <div class="row clearfix">
                                <div class="col-xs-6 align-left">
                                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                                </div>
                                <div class="col-xs-6 align-right">
                                    <button onclick="clickSubmit('id_edt_variant_btn')" type="button" class="btn btn-primary waves-effect " >S A V E</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    <!-- # END MODAL CREATE  -->
    <?php $this->load->view("_partials/js_dashboard.php");?>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/sweatalert2/sweetalert2.all.min.js"></script>
    <!-- MODAL CREATE -->
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-editable/src/table-edits.js"></script>
    <input type="hidden" id="id_baseurl" value="<?= base_url()?>">
    <script>
        $(function(){
            init();
            pantrySatuan();
            // initPaket();
        }) 
        var gBeforImage = "";
        var gAutomation = [];
        var gPrefixId = [];
        var gPrefixName = [];
        var gPantry = [];
        var gSatuan;
        var gMenuVariant = [];
        var gMenuPackagelist = [];

        var gMultipleVariant = {
            1 : "1 Option",
            2 : "Multiple Option",
        }
        $('[data-toggle="tooltip"]').tooltip({
            container: 'body'
        });

        function clickSubmit(btn){
            $('#'+btn)[0].click();
        }
        function initTable(selector){
            selector.DataTable();
        }
        function clearTable(selector){
            selector.DataTable().destroy();
        }
        function select_enable(){
            $('select').selectpicker("refresh");
            $('select').selectpicker("initialize");
            $.AdminBSB.input.activate();
        }
        function enable_datetimepicker(){
            $('.timepicker').bootstrapMaterialDatePicker({
                format: 'HH:mm',
                clearButton: true,
                date: false
            });
        }
        function closeMenu(){
            $('#id_dataMenu_back').hide();
            $('#id_dataMenu').hide();
            $('#id_pantry_name').html("");
            $('#id_dataVariant_back').hide();
            $('#id_variant_name').html("");

            $('#id_dataVariantMenu_back').hide();
            $('#id_menu_variant_name').html("");

            $('#id_dataVariant').hide();
            $('#tbldataMenuVariant').hide();
            

        }
        function showMenu(){
            $('#id_dataMenu_back').show();
            $('#id_dataMenu').show();
        }
        function showVariant(){
            $('#id_dataVariant').show();
            $('#id_dataVariant_back').show();
            $('#tbldataVariant').show();
        }
        function createData(){
            $('#id_mdl_create').modal({backdrop: 'static', keyboard: false});
            $('#id_mdl_create').modal('show');
            enable_datetimepicker()
            select_enable()
        }
        
        function createDataMenu(t){
            var name = $('#id_pantry_name').html()
            $('#id_mdl_create_menu').modal({backdrop: 'static', keyboard: false});
            $('#id_mdl_create_menu').modal('show');
            var html = ""
            $.each(gPrefixName, function(index, item){
                html += '<option value="'+(index+1)+'" >'+item+'</option>'
            })
            $('#id_crt_menu_prefix').html(html);
            $('#id_mdl_create_menu_lbl').html("Create menu for " + name)
            var pantry_id = gSatuan.data('id')
            $('#id_crt_menu_pantry_id').val(pantry_id);

            select_enable()
        }

        // 
        
     
        // #######################################################
        // UPDATE MENU/DETAIL
        // #######################################################
        function updateDataMenu(t){
            var bs = $('#id_baseurl').val();
            var id = t.data("id");
            $.ajax({
                url : bs+"pantry/get/menu-update/"+id,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    $('#id_loader').html('');
                    if(data.status == "success"){
                        var col = data.collection;
                        $('#id_mdl_update_menu').modal('show');
                        var html = "";
                        $.each(gPrefixName, function(index, item){
                            var s = (index+1) == col['prefix_id'] ? "selected":"";
                            html += '<option '+s+' value="'+(index+1)+'" >'+item+'</option>'
                        })
                        var img = col.pic;
                        var imghtml = bs+"assets/pantry-detail.jpg";
                        if(img != ""){
                            imghtml = bs+"assets/pantry/"+img;
                        }
                        $('#id_edt_menu_prefix').html(html);
                        $('#id_edt_menu_description').val(col.description);
                        // $('#id_mdl_create_menu_lbl').html("Create menu for " + name)
                        var pantry_id = col['pantry_id'];
                        gBeforImage = imghtml;
                        $('#id_edt_menu_pic_preview').prop("src", imghtml)
                        // var id_h = col['pantry_id'];
                        $('#id_edt_menu_pantry_id').val(pantry_id);
                        $('#id_edt_menu_name').val(col.name);
                        $('#id_edt_menu_id').val(id);
                        var htmlbtn = '<button style="display:none;" \
                        id="id_edt_menu_btn_html_v" \
                        onclick="pantryMenu($(this))" \
                        data-id="'+col.id+'" \
                        data-name="'+col.name+'" \
                        type="button" class="btn btn-danger waves-effect btn-lg ">S</button>';
                        // console.log(htmlbtn);
                        $('#id_edt_menu_btn_html').html(htmlbtn)
                        select_enable()
                    }else{
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                },
                error: errorAjax
            })
        }
        $("#id_edt_menu_pic").change(function() {
          readURL(this, $('#id_edt_menu_pic_preview'));
        });
        // #######################################################
        // INIT MENU KATEGORI VARIANT
        // #######################################################
        $('#id_select_variant').change(function(){
            initVariant();
        })

        function createDataVariant(){
            var text = $('#id_select_variant').find("option:selected").text();
            var value = $('#id_select_variant').val()

            $('#id_mdl_create_variant_lbl').html(" - "+text);
            $('#id_mdl_create_variant').modal({backdrop: 'static', keyboard: false});
            $('#id_mdl_create_variant').modal('show');
            $('#id_crt_variant_menu_id').val(value)
            enable_datetimepicker()
            select_enable()
            gMenuVariant = [];
        }

        function updateDataVariant(t){
            var bs = $('#id_baseurl').val();
            var id = t.data("id");
            var name = t.data("name");
            $.ajax({
                url : bs+"pantry/get/variant-update/"+id,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    $('#id_loader').html('');
                    if(data.status == "success"){
                        var col = data.collection;
                        var detail = col['detail'];
                        var datav = col['data'];
                        gMenuVariant = [];
                        // // console.log(col);
                        $('#id_mdl_update_variant').modal({backdrop: 'static', keyboard: false});
                        $('#id_mdl_update_variant').modal('show');
                        // // // // // // // // 
                        var htmloptionVariant = "";
                        for(var x in gMultipleVariant){
                            var r = gMultipleVariant[x];
                            var s = (x == datav.multiple )? "selected" : ""  ;

                            htmloptionVariant += "<option "+s+" value='"+x+"' >"+r+"</option>"
                        }
                        for(var i in detail){
                            detail[i]["update"] = 1
                            gMenuVariant.push(detail[i]);
                        }
                        // gMenuVariant = detail;
                        // // // // // // // //
                        $('#id_edt_variant_id').val(id);
                        $('#id_edt_variant_rule').html(htmloptionVariant);
                        // // // // // // // //
                        $('#id_edt_variant_name').val(datav.name);
                        $('#id_edt_variant_min').val(datav.min);
                        $('#id_edt_variant_max').val(datav.max);
                        select_enable()
                        generateTblVariantUpdateMenu()
                        oncCrtUpdateVariantMenu();
                    }else{
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                },
                error: errorAjax
            })
        }
        function generateTblVariantUpdateMenu(){
            var html = '';
            var num = 0;
            console.log()
            for(var x in gMenuVariant){
                var row = gMenuVariant[x];

                if(row.is_deleted == 0){
                    num ++;
                    html += '<tr>';
                    html += '<td>'+num+'</td>';
                    html += '<td><input data-num="'+num+'" onkeyup="oncKVariant($(this))" id="variant_data_'+num+'" value="'+row.name+'" /></td>';
                    html += '<td><button type="button" data-id="'+row.id+'" data-num="'+x+'" onclick="onclickRemoveVariantUpdateMenuData($(this))" class="btn btn-danger waves-effect">REMOVE</button></td>';
                    html += '</tr>';
                }
            }
            $('#tblVariantUpdateMenu tbody').html(html)
        }
        function oncKVariant(t){
            var val = t.val();
            var dat_num = t.data("num");
            var index = dat_num-1;
            gMenuVariant[index]["name"] =val;
            // console.log(123)
        }
        function oncCrtVariantMenuRule(){
            var rule = $('#id_crt_variant_rule').val(); 
            if(rule == "1" || rule == 1){
                $('#id_crt_variant_min').prop("disabled", "true");
                $('#id_crt_variant_max').prop("disabled", "true");
            }else if(rule == "2" || rule == 2){
                $('#id_crt_variant_min').removeAttr("disabled");
                $('#id_crt_variant_max').removeAttr("disabled");
            }
            select_enable();
            // console.log(123)
        }
        function oncCrtUpdateVariantMenuRule(){
            var rule = $('#id_edt_variant_rule').val(); 
            if(rule == "1" || rule == 1){
                $('#id_edt_variant_min').prop("disabled", "true");
                $('#id_edt_variant_max').prop("disabled", "true");
            }else if(rule == "2" || rule == 2){
                $('#id_edt_variant_min').removeAttr("disabled");
                $('#id_edt_variant_max').removeAttr("disabled");
            }
            select_enable();
            // console.log(123)
        }

        function oncCrtAddVariantMenu(){
            var menu = $('#id_crt_variant_menu').val();
            if(menu == ""){
                showNotification('alert-danger', "Variant menu cannot be empty",'top','center')
                return false;
            }
            var data = {
                name : menu,
            };
            gMenuVariant.push(data);
            generateTblVariantMenu()
        }
        function oncCrtUpdateVariantMenu(){
            var menu = $('#id_edt_variant_menu').val();
            if(menu == ""){
                showNotification('alert-danger', "Variant menu cannot be empty",'top','center')
                return false;
            }
            var data = {
                id:"null",
                name : menu,
                update : 0,
                is_deleted : 0,
            };
            gMenuVariant.push(data);
            generateTblVariantUpdateMenu()
        }
        function generateTblVariantMenu(){
            var html = '';
            var num = 0;
            for(var x in gMenuVariant){
                var r = gMenuVariant[x];
                num ++;
                html += '<tr>';
                html += '<td>'+num+'</td>';
                html += '<td>'+r.name+'</td>';
                html += '<td><button type="button" data-id="'+r.id+'" data-num="'+x+'" onclick="onclickRemoveVariantMenuData($(this))" class="btn btn-danger waves-effect">REMOVE</button></td>';
                html += '</tr>';
            }
            $('#tblVariantMenu tbody').html(html)
        }
        function onclickRemoveVariantMenuData(t){
            var num = t.data('num');
            gMenuVariant.splice(num, 1);
            generateTblVariantMenu();
        }
        function onclickRemoveVariantUpdateMenuData(t){
            var num = t.data('num');
            gMenuVariant[num]['is_deleted'] = 1;
            generateTblVariantUpdateMenu();
        }


        function initVariant(){
            var bs = $('#id_baseurl').val();
            var idmenu = $('#id_select_variant').val();
            $.ajax({
                url : bs+"pantry/get/variant/"+idmenu,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        var input = data.collection;
                        var html = "";
                        var n = 0;
                        clearTable($('#tbldataVariant'));
                        $.each(data.collection, function(index, item){
                            n++;
                            var mt = "";
                            if(item.multiple == "2" || item.multiple == 2){
                                mt = "Yes";
                            }else  if(item.multiple == "1" || item.multiple == 1){
                                mt = "No";
                            }else{
                                mt = "";
                            }
                            html += '<tr >'
                            html += '<td>'+n+'</td>';
                            html += '<td>'+item.name+'</td>';
                            html += '<td>'+mt+'</td>';
                            
                            html += '<td>\
                                <button \
                                 title="Edit" \
                                 onclick="updateDataVariant($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-info waves-effect "><i class="material-icons">mode_edit</i></button>\
                                <button \
                                 onclick="removeDataVariant($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-danger waves-effect "><i class="material-icons">delete</i></button>\
                                 </td>';
                            html += '</tr>';
                        })
                        $('#tbldataVariant tbody').html(html)
                        initTable($('#tbldataVariant'));
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function removeDataVariant(t){
            var bs = $('#id_baseurl').val();
            var id = t.data("id");
            var name = t.data("name");
            Swal.fire({
                title:'Are you sure you want delete it?',
                text: "You will lose the data variant "+name+" !",
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
                            url : bs+"pantry/post/delete-variant/"+id,
                            type: "POST",
                            data : {
                                id:id,
                                name:name,
                            },
                            dataType :"json",
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                            },
                            success:function(data){
                                $('#id_loader').html('');
                                if (data.status == "success") {
                                    showNotification('alert-success', "Succes deleted pantry variant "+name ,'top','center')
                                    initVariant();
                                }else{
                                    showNotification('alert-danger', "Data not found",'bottom','left')
                                }
                            },
                            error: errorAjax,
                        })
                    }
                else{

                }
            })
        }
        $('#frm_create_variant').submit(function(e){
            e.preventDefault();
            var id = $('#id_select_variant').val();
            // $('#frm_create_variant').serialize();
            if(gMenuVariant.length <=0 ){
                $('#id_mdl_create_variant').modal('hide');
                // showNotification('alert-danger', "Variant cannot be empty",'top','center')
                Swal.fire(
                      'Attention!',
                      'Variant cannot be empty!',
                      'warning'
                    )
                return false;
            }

            if($('#id_crt_variant_rule').val() == 2){
                if($('#id_crt_variant_min').val() == "" || $('#id_crt_variant_max').val() == ""){
                    Swal.fire(
                      'Attention!',
                      'Variant min and max cannot be empty!',
                      'warning'
                    )
                    // showNotification('alert-danger', "Variant min and max cannot be empty",'top','center')
                    return false;

                }
            }
            if(gMenuVariant.length <= 0){
                // console.log("Variant cannot be empty");
                Swal.fire(
                      'Attention!',
                      'Variant cannot be empty!',
                      'warning'
                    )
                // showNotification('alert-danger', "Variant cannot be empty",'top','center')
                return false;

            }
            var form =  {
                name : $('#id_crt_variant_name').val(),
                idmenu : $('#id_crt_variant_menu_id').val(),
                rule : $('#id_crt_variant_rule').val(),
                min : $('#id_crt_variant_min').val(),
                max :$('#id_crt_variant_max').val(),
                variant : gMenuVariant
            }
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"pantry/post/create-variant/"+id,
                dataType: "json",
                data : form,
                type: 'POST',
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        $('#frm_create_variant')[0].reset();
                        gMenuVariant = [];
                        initVariant();
                        $('#id_mdl_create_variant').modal('hide');
                        showNotification('alert-success', data.msg,'top','center')
                    }else{
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            });
        });
        $('#frm_update_variant').submit(function(e){
            e.preventDefault();
            var id = $('#id_edt_variant_id').val();
            var len = 0;
            for(var ln in gMenuVariant){
                if(gMenuVariant[ln].is_deleted == 0){
                    len++;
                }
            }
            // $('#frm_create_variant').serialize();
            if(len <= 0 ){
                $('#id_mdl_update_menu').modal('hide');
                Swal.fire(
                  'Attention!',
                  'Variant cannot be empty!',
                  'warning'
                )
                // showNotification('alert-danger', "Variant cannot be empty",'top','center')
                return false;
            }
            if($('#id_edt_variant_rule').val() == 2){
                if($('#id_edt_variant_min').val() == "" || $('#id_edt_variant_max').val() == ""){
                    Swal.fire(
                      'Attention!',
                      'Variant min and max cannot be empty!',
                      'warning'
                    )
                    // showNotification('alert-danger', "Variant min and max cannot be empty",'top','center')
                    return false;
                }
            }
            // if()
            var form =  {
                name : $('#id_edt_variant_name').val(),
                idmenu : $('#id_edt_variant_menu_id').val(),
                rule : $('#id_edt_variant_rule').val(),
                min : $('#id_edt_variant_min').val(),
                max :$('#id_edt_variant_max').val(),
                variant : gMenuVariant
            }
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"pantry/post/update-variant/"+id,
                dataType: "json",
                data : form,
                type: 'POST',
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        $('#frm_update_variant')[0].reset();
                        gMenuVariant = [];
                        initVariant();
                        $('#id_mdl_update_variant').modal('hide');
                        showNotification('alert-success', data.msg,'top','center')
                    }else{
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            });
        });  
        
        // #######################################################
        // #######################################################
        $('#frm_create_menu').submit(function(e){
            e.preventDefault();
            var form = $('#frm_create_menu')[0]; // You need to use standard javascript object here
            var formData = new FormData(form);
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"pantry/post/create-menu",
                type : "POST",
                dataType: "json",
                data : formData,
                contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
                processData: false, // NEEDED, DON'T OMIT THIS
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                        if(data.status == "success"){
                          $('#frm_create_menu')[0].reset();
                          pantryMenu(gSatuan);
                          $('#id_mdl_create_menu').modal('hide');
                          showNotification('alert-success', data.msg,'top','center')
                        }else{
                          showNotification('alert-danger', data.msg,'top','center')
                        }
                        $('#id_loader').html('');
                    },
                    error: errorAjax
            })
        }) 
        $('#frm_update_menu').submit(function(e){
            e.preventDefault();
            var form = $('#frm_update_menu')[0]; // You need to use standard javascript object here
            var formData = new FormData(form);
            var id = $('#id_edt_menu_id').val()
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"pantry/post/update-menu/"+id,
                type : "POST",
                dataType: "json",
                data : formData,
                contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
                processData: false,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        $('#frm_update_menu')[0].reset();
                        $('#id_mdl_update_menu').modal('hide');
                        $('#id_edt_menu_btn_html_v').click();
                        // init();
                        showNotification('alert-success', data.msg,'top','center')
                    }else{
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            });
        })
        $('#frm_create').submit(function(e){
            e.preventDefault();
            var form = $('#frm_create')[0]; // You need to use standard javascript object here
            var formData = new FormData(form);
            // var form =  $('#frm_create').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"pantry/post/create",
                dataType: "json",
                data : formData,
                type: 'POST',
                contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
                processData: false, // NEEDED, DON'T OMIT THIS
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                        if(data.status == "success"){
                          $('#frm_create')[0].reset();
                          init();
                          $('#id_mdl_create').modal('hide');
                          showNotification('alert-success', data.msg,'top','center')
                        }else{
                          showNotification('alert-danger', data.msg,'top','center')
                        }
                        $('#id_loader').html('');
                    },
                    error: errorAjax
            })
        })  
        $('#frm_update_pantry').submit(function(e){
            e.preventDefault();
            var form = $('#frm_update_pantry')[0]; // You need to use standard javascript object here
            var formData = new FormData(form);
            var id = $('#id_edt_id').val()
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"pantry/post/update/"+id,
                type : "POST",
                dataType: "json",
                data : formData,
                contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
                processData: false,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        $('#frm_update_pantry')[0].reset();
                        $('#id_mdl_update').modal('hide');
                        init();
                        showNotification('alert-success', data.msg,'top','center')
                    }else{
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        })

        function init(){
            closeMenu();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"pantry/get/data",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        clearTable($('#tbldata'));
                        var html = "";
                        gPantry = data.collection;
                        var n = 0;
                        $.each(data.collection, function(index, item){
                            n++;
                            var img = item.pic ;
                            var imghtml = "<img src='"+bs+"assets/pantry.jpeg' style='height:80px; width:120px'>";
                            if(img != ""){
                                imghtml = "<img src='"+bs+"assets/pantry/"+img+"' style='height:80px; width:120px'>";
                            }
                            html += '<tr  data-id="'+item.id+'" data-name="'+item.name+'" >'
                            html += '<td >'+n+'</td>';
                            html += '<td >'+item.name+'</td>';
                            html += '<td >'+imghtml+'</td>';
                            html += '<td>\
                                <button \
                                 onclick="oncUpdatePantryData($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-info waves-effect "><i class="material-icons">mode_edit</i></button>\
                                 \
                                 <button \
                                 onclick="removeData($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-danger waves-effect "><i class="material-icons">delete</i></button>\
                                 \
                                 </td>';
                            html += '<td><button \
                                 onclick="pantryMenu($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-danger waves-effect btn-lg ">MENU</button>\
                                 </td>';
                            html += '</tr>';
                        })
                        $('#tbldata tbody').html(html);
                        
                        initTable($('#tbldata'));
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    // 
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        
        
        function pantryMenu(t){

            var bs = $('#id_baseurl').val();
            var id = t.data('id');
            var name = t.data('name');
            gSatuan = t;
            $('#id_pantry_name').html(" - " + name)
            $.ajax({
                url : bs+"pantry/get/menu/"+id,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        var input = data.collection;
                        var html = "";
                        var n = 0;
                        var htmlsw = "";
                        clearTable($('#tbldataMenu'));
                        $.each(data.collection, function(index, item){
                            n++;
                            var prefix = item.prefix == null ? "" : item.prefix;
                            var img = item.pic;
                            var imghtml = "<img src='"+bs+"assets/pantry-detail.jpg' style='height:80px; width:120px'>";
                            if(img != ""){
                                imghtml = "<img src='"+bs+"assets/pantry/"+img+"' style='height:80px; width:120px'>";
                            }
                            if(n == 1){
                                htmlsw += "<option selected value='"+item.id+"' >"+item.name+"</option>";
                            }else{
                                htmlsw += "<option value='"+item.id+"' >"+item.name+"</option>";
                            }

                            html += '<tr  data-id="'+item.id+'" >'
                            html += '<td>'+n+'</td>';
                            html += '<td>'+imghtml+'</td>';
                            html += '<td data-field="name">'+item.name+'</td>';
                            html += '<td data-field="prefix">'+prefix+'</td>';
                            html += '<td>\
                                <button \
                                 title="Edit" \
                                 onclick="updateDataMenu($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-info waves-effect "><i class="material-icons">mode_edit</i></button>\
                                <button \
                                 onclick="removeDataMenu($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-danger waves-effect "><i class="material-icons">delete</i></button>\
                                 \
                                 </td>';
                            html += '</tr>';
                        })
                        $('#tbldataMenu tbody').html(html)
                        $('#id_select_variant').html(htmlsw)
                        select_enable();
                        initTable($('#tbldataMenu'));
                        initVariant()
                        showMenu()
                        showVariant();

                        // $('#id_mdl_update').modal('show');
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        
       
        function pantrySatuan(){
            closeMenu();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"pantry/get/satuan",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        var input = data.collection;
                        var html = "";
                        gPrefixName = [];
                        $.each(data.collection, function(index, item){
                            gPrefixName.push(item.name);
                        })
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function oncUpdatePantryData(t){
            var bs = $('#id_baseurl').val();
            var id = t.data('id');
            $.ajax({
                url : bs+"pantry/get/edit/"+id,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    gBeforImage = "";
                    if(data.status == "success"){
                        // var input = data.collection;
                        var col = data.collection;
                        $('#id_edt_name').val(col.name)
                        $('#id_edt_id').val(col.id)
                        $('#id_edt_days').val(col.days)

                        var pic = col.pic;
                        var imghtml = bs+"assets/pantry.jpeg";
                        if(pic != ""){
                            imghtml = bs+"assets/pantry/"+pic;
                        }
                        gBeforImage = imghtml;
                        $('#id_edt_pic_preview').prop("src", imghtml)
                        select_enable()
                        // $('#id_mdl_update').modal({backdrop: 'static', keyboard: false});
                        $('#id_mdl_update').modal('show');
                        enable_datetimepicker()
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function readURL(input, selected) {
          if (input.files && input.files[0] && input.files.length != 0) {
            var reader = new FileReader();
            
            reader.onload = function(e) {
              selected.attr('src', e.target.result);
            }
            
            reader.readAsDataURL(input.files[0]); // convert to base64 string
          }else{
            var imghtml = gBeforImage;
            selected.prop("src", imghtml);
          }
        }

        $("#id_edt_pic").change(function() {
          readURL(this, $('#id_edt_pic_preview'));
        });

        function removeData(t){
            var id = t.data('id');
            var name = t.data('name');
            var form = new FormData();
            form.append('id', id);
            form.append('name', name);
            Swal.fire({
                title:'Are you sure you want delete it?',
                text: "You will lose the data pantry "+name+" !",
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
                            url : bs+"pantry/post/delete",
                            type: "POST",
                            data : form,
                            processData: false,
                            contentType: false,
                            dataType :"json",
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                            },
                            success:function(data){
                                $('#id_loader').html('');
                                if (data.status == "success") {
                                    showNotification('alert-success', "Succes deleted pantry "+name ,'top','center')
                                    init();
                                }else{
                                    showNotification('alert-danger', "Data not found",'bottom','left')
                                }
                            },
                            error: errorAjax,
                        })
                    }
                else{

                }
            })
            
        }
        function removeDataSatuan(t){
            var id = t.data('id');
            var name = t.data('name');
            var form = new FormData();
            form.append('id', id);
            form.append('name', name);
            Swal.fire({
                title:'Are you sure you want delete it?',
                text: "You will lose the data prefix "+name+" !",
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
                            url : bs+"pantry/post/delete-satuan",
                            type: "POST",
                            data : form,
                            processData: false,
                            contentType: false,
                            dataType :"json",
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                            },
                            success:function(data){
                                $('#id_loader').html('');
                                if (data.status == "success") {
                                    showNotification('alert-success', "Succes deleted prefix "+name ,'top','center')
                                    pantrySatuan()
                                }else{
                                    showNotification('alert-danger', "Data not found",'bottom','left')
                                }
                            },
                            error: errorAjax,
                        })
                    }
                else{

                }
            })
            
        }
        function removeDataMenu(t){
            var id = t.data('id');
            var name = t.data('name');
            var indexT = gSatuan;
            var form = new FormData();
            form.append('id', id);
            form.append('name', name);
            Swal.fire({
                title:'Are you sure you want delete it?',
                text: "You will lose the data menu "+name+" !",
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
                            url : bs+"pantry/post/delete-menu",
                            type: "POST",
                            data : form,
                            processData: false,
                            contentType: false,
                            dataType :"json",
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                            },
                            success:function(data){
                                $('#id_loader').html('');
                                if (data.status == "success") {
                                    showNotification('alert-success', "Succes deleted menu "+name ,'top','center')
                                    pantryMenu(indexT)
                                }else{
                                    showNotification('alert-danger', "Data not found",'bottom','left')
                                }
                            },
                            error: errorAjax,
                        })
                    }
                else{

                }
            })
            
        }
        
        function errorAjax(xhr, ajaxOptions, thrownError){
            $('#id_loader').html('');
            if(ajaxOptions == "parsererror"){
                var msg = "Status Code 500, Error Server bad parsing";
                showNotification('alert-danger', msg,'bottom','left')
            }else{
                var msg ="Status Code "+ xhr.status + " Please check your connection !!!";
                showNotification('alert-danger', msg,'bottom','left')
            }
        }
    </script>
    </body>
</html>
