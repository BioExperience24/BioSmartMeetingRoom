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
            <!-- START WIDGET -->
            <div class="row clearfix">
                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                    <div class="info-box hover-expand-effect">
                        <div class="icon  bg-orange ">
                            <i class="material-icons">event</i>
                        </div>
                        <div class="content">
                            <div class="text"><?= strtoupper(date("d M Y"))?></div>
                            <div class="number count-to" id="time1">
                                
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                    <div class="info-box  hover-expand-effect">
                        <div class="icon bg-light-green">
                            <i class="material-icons">toc</i>
                        </div>
                        <div class="content">
                            <div class="text">TOTAL PANTRY</div>
                            <div class="number count-to" data-from="0" data-to="0" data-speed="1000" data-fresh-interval="20" id="id_count_total_pantry">
                                
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                    <div class="info-box  hover-expand-effect">
                        <div class="icon bg-light-green">
                            <i class="material-icons">toc</i>
                        </div>
                        <div class="content">
                            <div class="text">TOTAL MENU</div>
                            <div class="number count-to" data-from="0" data-to="0" data-speed="1000" data-fresh-interval="20" id="id_count_total_menu">
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END WIDGET -->
            <div class="row clearfix">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="card">
                        <div class="body">
                            <ul class="nav nav-tabs tab-nav-right" role="tablist">
                                <li onclick="changeTabs('tabmenu')" role="presentation" class="active" ><a href="#tabmenu" data-toggle="tab" >MENU</a></li>
                                <li  onclick="changeTabs('tabvariant')"  role="presentation"><a href="#tabvariant" data-toggle="tab">MENU VARIANT</a></li>
                            </ul>
                            <div class="tab-content">
                                <div role="tabpanel" class="tab-pane fade  in active" id="tabmenu">
                                    <?php $this->load->view("Admin/Pantry/menu/view_menu.php", array('pagename'=>$pagename));?>
                                </div>
                                <div role="tabpanel" class="tab-pane fade " id="tabvariant">
                                    <?php $this->load->view("Admin/Pantry/menu/view_menu.php", array('pagename'=>$pagename));?>
                                </div>
                                <div role="tabpanel" class="tab-pane fade" id="settings">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           
        </div>
    </section>

    <!-- START JS -->
    <?php $this->load->view("_partials/js_dashboard.php");?>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/sweatalert2/sweetalert2.all.min.js"></script>
    <!-- Moment Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment-timezone.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment-timezone-data.min.js"></script>
    <script src="<?= base_url()?>assets/external/numeral/numeral.min.js"></script>
    <script src="<?= base_url()?>assets/external/numeral/locale.id.js"></script>

    <textarea  id="id_modules" style="display: none;"> <?= json_encode($modules)?></textarea> 
    <textarea  id="id_pantry" style="display: none;"><?= $pantry?></textarea>
    <input type="hidden" id="id_baseurl" value="<?= base_url()?>">
    <script type="text/javascript">
        var localtimezone = moment.tz.guess();
        var gPantry = JSON.parse($('#id_pantry').val());
        // var gRoom = JSON.parse($('#id_room').val());
        var gPantry = JSON.parse($('#id_pantry').val());
        var initialTab =  "tabmenu";
        var gGlobalTabs = "tabmenu";
        
    </script>
    <script type="text/javascript">
        $(function(){
            intrTime()
            // initGlobalData()
            // initialLoad();
            initPantry();
        }) 
        function changeTabs(argument) {
            if(gGlobalTabs != argument){
                gGlobalTabs = argument;
            }
        }
        function intrTime(){
            setInterval(
                function(){
                var tm = moment().format('hh:mm A');
                $('#time1').html(tm);
                },500
            );
        }
        function getModule(){
            var modules = $('#id_modules').val();
            return JSON.parse(modules)
        }
        function initPantry(){
            var html = '<option selected value="">All Pantry</option>';
            var htmlRoom = '<option selected value="">All Room</option>';
            for(var x in gPantry){
                html += '<option value="'+gPantry[x].id+'">'+gPantry[x].name+'</option>';
            }
            $(`#id_${gGlobalTabs}_pantry_search`).html(html);
            select_enable();
        }

         function initTable(selector) {
            selector.DataTable();
        }

        function clearTable(selector) {
            if(selector != null){
                selector.DataTable().destroy();
            }
        }

        function select_enable() {
            $('select').selectpicker("refresh");
            $('select').selectpicker("initialize");
        }

        function enable_datetimepicker() {
            $('.timepicker').bootstrapMaterialDatePicker({
                format: 'HH:mm',
                clearButton: true,
                date: false
            });
        }

        function loadingg(title = "", body = "") {
            Swal.fire({
                title: title,
                html: body,
                allowOutsideClick: false,
                onBeforeOpen: () => {
                    Swal.showLoading()
                },
            });
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
    <!-- END JS -->
</body>
</html>