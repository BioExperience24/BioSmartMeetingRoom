
<?php  
// date_default_timezone_set("Asia/Jakarta");
// echo date('w');
// die();
?>
<!DOCTYPE html>
<html lang="en">
<head>
   <?php $this->load->view("_partials/head_css_dashboard.php", array('pagename'=>$pagename));?>
    <link href="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <link href="<?= base_url()?>assets/theme/plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet" />
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
                    <!-- row -->
                    <div class="row clearfix">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="card">
                                <div class="header">

                                    <div class="row clearfix">
                                        <div class="col-xs-12 col-sm-6">
                                            <h2>Pantry Package</h2>
                                        </div>
                                        <div class="col-xs-12 col-sm-6 align-right">
                            
                                        </div>
                                    </div>
                                </div>
                                <div class="body table-responsive responsive">
                                    <table class="table table-hover" id="tbldataPackage">
                                        <thead>
                                                <th style="width: 30px;">#</th>
                                                <th >Package Name</th>
                                                <th >Pantry</th>
                                                <th>
                                                    <button 
                                                     onclick="createPackageData()" 
                                                     type="button" class="btn btn-primary waves-effect ">
                                                     <i class="material-icons">add_circle</i> CREATE</button>
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
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <!-- row -->
                    <div class="row clearfix">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="card">
                                <div class="header">

                                    <div class="row clearfix">
                                        <div class="col-xs-12 col-sm-6">
                                            <h2>Prefix Item</h2>
                                        </div>
                                        <div class="col-xs-12 col-sm-6 align-right">
                            
                                        </div>
                                    </div>
                                </div>
                                <div class="body table-responsive responsive">
                                    <table class="table table-hover" id="tbldataSatuan">
                                        <thead>
                                                <th style="width: 200px;">Name</th>
                                                <th>
                                                    <button 
                                                     onclick="createDataPrefix($(this))" 
                                                     type="button" class="btn btn-primary waves-effect ">
                                                     <i class="material-icons">add_circle</i> CREATE</button>
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
            <!-- row -->
            <div class="row clearfix">
                
            </div>
            <!-- row -->
        </div>
    </section>
    <div class="modal fade" id="id_mdl_create_prefix" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="idmdlcrLabel">Create Prefix</h4>
                        </div>
                        <div class="modal-body " id="id_mdl_create_prefix_body">
                            <form id="frm_create_prefix">
                                <label for="">Name</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="text" autocomplete="off" name="name" id="id_crt_pre_name" required=""  class="form-control" placeholder="Prefix Name">
                                    </div>
                                </div>
                                <button type="submit" id="id_crt_pre_btn" style="display: none;"  class="btn btn-primary m-t-15 waves-effect">Submit</button>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <div class="row clearfix">
                                <div class="col-xs-6 align-left">
                                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                                </div>
                                <div class="col-xs-6 align-right">
                                    <button onclick="clickSubmit('id_crt_pre_btn')" type="button" class="btn btn-primary waves-effect " >S A V E</button>
                                </div>
                            </div>
                            <!-- <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button> -->
                        </div>
                    </div>
                </div>
            </div>
    <div class="modal fade" id="id_mdl_create_package" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="idmdlcrLabel">Create Package</h4>
                        </div>
                        <div class="modal-body " id="id_mdl_create_prefix_body">
                            <form id="frm_create_package" >
                                <label for="">Name</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="text" autocomplete="off" name="name" id="id_crt_pack_name" required=""  class="form-control" placeholder="Package Name">
                                    </div>
                                </div>
                                <label for="">Pantry Name</label>
                                <div class="form-group">
                                    <select onchange="oncCrtPackagePantry()" class="form-control show-tick" id="id_crt_pack_pantry_id" name="pantry_id" data-live-search="true" >
                                        <option value="">C H O O S E  P A N T R Y </option>
                                    </select>
                                    
                                </div>
                                <label for="">Pantry Menu</label>
                                <div class="form-group">
                                    <div class="row clearfix">
                                        <div class="col-xs-6">
                                            <select class="form-control show-tick" id="id_crt_pack_menu" name="" data-live-search="true" >
                                                <option value="">C H O O S E  M E N U </option>
                                            </select>
                                        </div>
                                        <div class="col-xs-1">
                                            <button type="button" onclick="oncCrtAddPackagePantry()" class="btn btn-info waves-effect">ADD</button>
                                        </div>
                                    </div>
                                    
                                </div>
                                <label for="">List Menu</label>
                                <table class="table table-bordered" id="tblPackageAddMenu">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Name</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                                <br>
                                <button type="submit" id="id_crt_pack_btn" style="display: none;" class="btn btn-primary m-t-15 waves-effect">Submit</button>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <div class="row clearfix">
                                <div class="col-xs-6 align-left">
                                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                                </div>
                                <div class="col-xs-6 align-right">
                                    <button onclick="clickSubmit('id_crt_pack_btn')" type="button" class="btn btn-primary waves-effect " >S A V E</button>
                                </div>
                            </div>
                            <!-- <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button> -->
                        </div>
                    </div>
                </div>
            </div>
    <div class="modal fade" id="id_mdl_update_package" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="idmdlcrLabel">Update Package</h4>
                        </div>
                        <div class="modal-body " id="id_mdl_create_prefix_body">
                            <form id="frm_update_package" >
                                <label for="">Name</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="text" autocomplete="off" name="name" id="id_upd_pack_name" required=""  class="form-control" placeholder="Package Name">
                                        <input type="hidden" autocomplete="off" id="id_upd_pack_package_id" required=""  class="form-control" placeholder="Package ID">
                                    </div>
                                </div>
                                <label for="">Pantry Name</label>
                                <div class="form-group">
                                    <select onchange="oncUpdatePackagePantry()" class="form-control show-tick" id="id_upd_pack_pantry_id" name="pantry_id" data-live-search="true" >
                                        <option value="">C H O O S E  P A N T R Y </option>
                                    </select>
                                    
                                </div>
                                <label for="">Pantry Menu</label>
                                <div class="form-group">
                                    <div class="row clearfix">
                                        <div class="col-xs-6">
                                            <select class="form-control show-tick" id="id_upd_pack_menu" name="" data-live-search="true" >
                                                <option value="">C H O O S E  M E N U </option>
                                            </select>
                                        </div>
                                        <div class="col-xs-1">
                                            <button type="button" onclick="oncCrtUpdatePackagePantry()" class="btn btn-info waves-effect">ADD</button>
                                        </div>
                                    </div>
                                    
                                </div>
                                <label for="">List Menu</label>
                                <table class="table table-bordered" id="tblPackageUpdateMenu">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Name</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                                <br>
                                <button type="submit" id="id_upd_pack_btn" style="display: none;" class="btn btn-primary m-t-15 waves-effect">Submit</button>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <div class="row clearfix">
                                <div class="col-xs-6 align-left">
                                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                                </div>
                                <div class="col-xs-6 align-right">
                                    <button onclick="clickSubmit('id_upd_pack_btn')" type="button" class="btn btn-primary waves-effect " >S A V E</button>
                                </div>
                            </div>
                            <!-- <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button> -->
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
            initPaket();
        }) 

        var gAutomation = [];
        var gPrefixId = [];
        var gPrefixName = [];
        var gPantry = [];
        var gSatuan;
        var gMenuPackage = [];
        var gMenuPackagelist = [];

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
        }
        function showMenu(){
            $('#id_dataMenu_back').show();
            $('#id_dataMenu').show();
        }
        function createData(){
            $('#id_mdl_create').modal('show');
            enable_datetimepicker()
            select_enable()

        }
        function createDataPrefix(){
            $('#id_mdl_create_prefix').modal('show');
        }
        function createDataMenu(t){
            var name = $('#id_pantry_name').html()
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
        function createPackageData(){
            gMenuPackage = [];
            gMenuPackagelist = [];
            $('#id_crt_pack_name').val("");
            $('#id_crt_pack_pantry_id').html("");
            $('#id_crt_pack_menu').html("");
            $('#tblPackageAddMenu tbody').html("");

            var html = '<option value="">C H O O S E &nbsp;&nbsp;P A N T R Y </option>';
            $.each(gPantry, function(index, item){
                html += '<option value="'+item.id+'" >'+item.name+'</option>'
            })
            $('#id_crt_pack_pantry_id').html(html);
            $('#id_mdl_create_package').modal('show');
            select_enable()
        }
        function oncCrtPackagePantry(){
            var bs = $('#id_baseurl').val();
            var id = $('#id_crt_pack_pantry_id').val();
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
                        gMenuPackage = data.collection;
                        gMenuPackagelist = [];
                        generateTblPackageMenu()
                        var html = "";
                        var html = '<option value="">C H O O S E &nbsp;&nbsp; M E N U </option>';
                        $.each(gMenuPackage, function(index, item){
                            html += '<option value="'+item.id+'" >'+item.name+'</option>'
                        })
                        $('#id_crt_pack_menu').html(html)
                        select_enable()
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function oncCrtAddPackagePantry(){
            var menuid = $('#id_crt_pack_menu').val();
            if(menuid == ""){
                return false;
            }
            var data = {
                id : "",
                name : "",
                pantry_id : ""
            };
            for(var x in gMenuPackage){
                if(gMenuPackage[x]['id'] == menuid){
                    data = gMenuPackage[x];
                    break;
                }
            }
            gMenuPackagelist.push(data);
            generateTblPackageMenu()
        }
        function generateTblPackageMenu(){
            var html = '';
            var num = 0;
            for(var x in gMenuPackagelist){
                var r = gMenuPackagelist[x];
                num ++;
                html += '<tr>';
                html += '<td>'+num+'</td>';
                html += '<td>'+r.name+'</td>';
                html += '<td><button type="button" data-id="'+r.id+'" data-num="'+x+'" onclick="onclickRemoveData($(this))" class="btn btn-danger waves-effect">REMOVE</button></td>';
                html += '</tr>';
                
            }
            $('#tblPackageAddMenu tbody').html(html)
        }
        function onclickRemoveData(t){
            var num = t.data('num');
            var id = t.data('id');
            gMenuPackagelist.splice(num, 1);
            generateTblPackageMenu();
        }
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
                        var html = "";
                        gPantry = data.collection;
                        
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
        // #######################################################
        // UPDATE PACKAGE
        // #######################################################
        function updatePackageData(data, detail){
            gMenuPackage = [];
            gMenuPackagelist = [];
            $('#id_upd_pack_name').val(data.name);
            $('#id_upd_pack_package_id').val(data.id);
            $('#id_upd_pack_pantry_id').html("");
            $('#id_upd_pack_menu').html("");
            $('#tblPackageUpdateMenu tbody').html("");

            var html1 = '<option value="">C H O O S E &nbsp;&nbsp;P A N T R Y </option>';
            var html1 = '<option value="">C H O O S E &nbsp;&nbsp;M E N U </option>';
            $.each(gPantry, function(index, item){
                var ss = (item.id == data.pantry_id) ? "selected" : "";
                html1 += '<option '+ss+' value="'+item.id+'" >'+item.name+'</option>'
            })

            $('#id_upd_pack_pantry_id').html(html1);

            $('#id_mdl_update_package').modal('show');
            for(var x in detail){
                var val = {
                        id : detail[x]['id'],
                        name : detail[x]['name'],
                };
                gMenuPackagelist.push(val)
            }
            select_enable()
            oncUpdatePackagePantry('update');
            generateTblPackageUpdateMenu();

        }
        function oncUpdatePackagePantry(cb = ""){
            var bs = $('#id_baseurl').val();
            var id = $('#id_upd_pack_pantry_id').val();
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
                        gMenuPackage = data.collection;
                        if(cb == ""){
                            gMenuPackagelist = [];
                        }
                        gMenuPackagelist = [];
                        generateTblPackageMenu()
                        var html = "";
                        var html = '<option value="">C H O O S E &nbsp;&nbsp; M E N U </option>';
                        $.each(gMenuPackage, function(index, item){
                            html += '<option value="'+item.id+'" >'+item.name+'</option>'
                        })
                        $('#id_upd_pack_menu').html(html)
                        select_enable()
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function oncCrtUpdatePackagePantry(){
            var menuid = $('#id_upd_pack_menu').val();
            if(menuid == ""){
                return false;
            }
            var data = {
                id : "",
                name : "",
                pantry_id : ""
            };
            for(var x in gMenuPackage){
                if(gMenuPackage[x]['id'] == menuid){
                    data = gMenuPackage[x];
                    break;
                }
            }
            gMenuPackagelist.push(data);
            generateTblPackageUpdateMenu()
        }
        function generateTblPackageUpdateMenu(){
            var html = '';
            var num = 0;
            for(var x in gMenuPackagelist){
                var r = gMenuPackagelist[x];
                num ++;
                html += '<tr>';
                html += '<td>'+num+'</td>';
                html += '<td>'+r.name+'</td>';
                html += '<td><button type="button" data-id="'+r.id+'" data-num="'+x+'" onclick="onclickRemovePackageUpdateData($(this))" class="btn btn-danger waves-effect">REMOVE</button></td>';
                html += '</tr>';
                
            }
            $('#tblPackageUpdateMenu tbody').html(html)
        }
        function onclickRemovePackageUpdateData(t){
            var num = t.data('num');
            var id = t.data('id');
            gMenuPackagelist.splice(num, 1);
            generateTblPackageUpdateMenu();
        }

        // #######################################################
        // #######################################################
        $('#frm_create_prefix').submit(function(e){
            e.preventDefault();
            var form =  $('#frm_create_prefix').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"pantry/post/create-satuan",
                type : "POST",
                dataType: "json",
                data : form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                        if(data.status == "success"){
                          $('#frm_create_prefix')[0].reset();
                          pantrySatuan();
                          $('#id_mdl_create_prefix').modal('hide');
                          showNotification('alert-success', data.msg,'top','center')
                        }else{
                          showNotification('alert-danger', data.msg,'top','center')
                        }
                        $('#id_loader').html('');
                    },
                    error: errorAjax
            })
        }) 
        $('#frm_create_package').submit(function(e){
            e.preventDefault();
            Swal.fire({
                title:'Attention !',
                text: "Are you sure you want save it? ",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Save !',
                cancelButtonText: 'Cancel !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                        var bs = $('#id_baseurl').val();
                        if(gMenuPackagelist.length <= 0){
                            Swal.fire("Warning !", "Choose a menu item (min 1 item) ", 'warning')
                            return false;
                        }
                        var form = {
                            name : $('#id_crt_pack_name').val(),
                            pantry_id : $('#id_crt_pack_pantry_id').val(),
                            menu : gMenuPackagelist,
                        }
                        $.ajax({
                            url : bs+"pantry/post/create-package",
                            type: "POST",
                            data : form,
                            dataType :"json",
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                            },
                            success:function(data){
                                $('#id_loader').html('');
                                if (data.status == "success") {
                                    Swal.fire("Message !", data.msg, 'success')
                                    $('#frm_create_package')[0].reset();
                                    gMenuPackagelist = [];
                                    // showNotification('alert-success', "Succes deleted pantry "+name ,'top','center')
                                    $('#id_mdl_create_package').modal('hide');
                                    initPaket();
                                }else{
                                    Swal.fire("Warning !", data.msg, 'warning')
                                    // showNotification('alert-danger', "Data not found",'bottom','left')
                                }
                            },
                            error: errorAjax,
                        })
                    }
                else{

                }
            })
            
        }) 
        $('#frm_update_package').submit(function(e){
            e.preventDefault();
            Swal.fire({
                title:'Attention !',
                text: "Are you sure you want save it? ",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Save !',
                cancelButtonText: 'Cancel !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                        var bs = $('#id_baseurl').val();
                        if(gMenuPackagelist.length <= 0){
                            Swal.fire("Warning !", "Choose a menu item (min 1 item) ", 'warning')
                            return false;
                        }
                        var form = {
                            name : $('#id_upd_pack_name').val(),
                            pantry_id : $('#id_upd_pack_pantry_id').val(),
                            menu : gMenuPackagelist,
                        }
                        var upid =  $('#id_upd_pack_package_id').val();
                        $.ajax({
                            url : bs+"pantry/post/update-package/"+upid,
                            type: "POST",
                            data : form,
                            dataType :"json",
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                            },
                            success:function(data){
                                $('#id_loader').html('');
                                if (data.status == "success") {
                                    Swal.fire("Message !", data.msg, 'success')
                                    $('#frm_update_package')[0].reset();
                                    gMenuPackagelist = [];
                                    // showNotification('alert-success', "Succes deleted pantry "+name ,'top','center')
                                    $('#id_mdl_update_package').modal('hide');
                                    initPaket();
                                }else{
                                    Swal.fire("Warning !", data.msg, 'warning')
                                    // showNotification('alert-danger', "Data not found",'bottom','left')
                                }
                            },
                            error: errorAjax,
                        })
                    }
                else{

                }
            })
            
        }) 
        
        $('#frm_update').submit(function(e){
            e.preventDefault();
            var form = $('#frm_update').serialize();
            var id = $('#id_edt_id').val()
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"pantry/post/update/"+id,
                type : "POST",
                dataType: "json",
                data : form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                        if(data.status == "success"){
                          $('#frm_update')[0].reset();
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

        
        function initPaket(){
            closeMenu();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"pantry/get/package",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        clearTable($('#tbldataPackage'));
                        var html = "";
                        var nim = 0;
                        $.each(data.collection, function(index, item){
                            nim++;
                            html += '<tr  data-id="'+item.id+'"  >'
                            html += '<td >'+nim+'</td>';
                            html += '<td data-field="name">'+item.name+'</td>';
                            html += '<td data-field="name">'+item.pantry_name+'</td>';
                            html += '<td>\
                                <button \
                                 onclick="updateDataPackage($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-info btn-sm waves-effect btn-lg "><i class="material-icons">edit</i></button>\
                                <button \
                                 onclick="removeDataPackage($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-danger btn-sm waves-effect "><i class="material-icons">delete</i></button>\
                                 \
                                 </td>';
                            html += '</tr>';
                        })
                        $('#tbldataPackage tbody').html(html);
                        
                        initTable($('#tbldataPackage'));
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
        function updateDataPackage(t){
            var bs = $('#id_baseurl').val();
            var id = t.data('id');
            var name = t.data('id');
            $.ajax({
                url : bs+"pantry/get/package-update/"+id,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        var input = data.collection;
                        var html = "";
                        updatePackageData(input.data, input.detail)
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
                            html += '<tr  data-id="'+item.id+'" >'
                            html += '<td data-field="name">'+item.name+'</td>';
                            html += '<td>\
                                <a class="btn btn-info waves-effect edit" title="Edit">\
                                  <i class="material-icons">mode_edit</i>\
                                </a>\
                                <button \
                                 onclick="removeDataSatuan($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-danger waves-effect "><i class="material-icons">delete</i></button>\
                                 \
                                 </td>';
                            html += '</tr>';
                        })
                        $('#tbldataSatuan tbody').html(html)
                        $('#tbldataSatuan  tr').editable({
                            edit: function(values) {
                              $(".edit i", this)
                                .html('save')
                                .attr('title', 'Save');
                            },
                            save: function(values) {
                              $(".edit i", this)
                                .html('mode_edit')
                                .attr('title', 'Edit');
                                var id = $(this).data('id');
                                $.post(bs+'pantry/post/update-satuan/' + id, values);

                            },
                            cancel: function(values) {
                              $(".edit i", this)
                                .html('mode_edit')
                                .attr('title', 'Edit');

                            }
                          });
                        initTable($('#tbldataSatuan'));
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
        function removeDataPackage(t){
            var bs = $('#id_baseurl').val();
            var id = t.data('id');
            var name = t.data('name');
            var form = {
                id : id,
                name : name,
            }
            Swal.fire({
                title:'Attention !',
                text: "Are you sure you want delete package "+name+" ?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Delete !',
                cancelButtonText: 'Cancel !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                        $.ajax({
                            url : bs+"pantry/post/delete-package",
                            type: "POST",
                            data : form,
                            dataType :"json",
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                            },
                            success:function(data){
                                $('#id_loader').html('');
                                if (data.status == "success") {
                                    Swal.fire("Message !", data.msg, 'success');
                                    gMenuPackagelist = [];
                                    initPaket();
                                }else{
                                    Swal.fire("Warning !", data.msg, 'warning')
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