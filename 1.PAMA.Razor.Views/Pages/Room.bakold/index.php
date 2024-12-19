
<?php  
// date_default_timezone_set("Asia/Jakarta");
// echo date('w');
// print_r($modules);
// die();
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
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="card">
                        <div class="header">

                            <div class="row clearfix">
                                <div class="col-xs-12 col-sm-6">
                                    <h2>Room List</h2>
                                </div>
                                <div class="col-xs-12 col-sm-6 align-right">
                    
                                </div>
                            </div>
                        </div>
                        <div class="body table-responsive responsive">
                            <table class="table table-hover" id="tbldata">
                                <thead>
                                        <th>#</th>
                                        <th>ID / Serial</th>
                                        <th>Name</th>
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
                                        <th>Status</th>
                                        <th>
                                            <button 
                                             onclick="createData($(this))" 
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
    </section>
    <div class="modal fade" id="id_mdl_create" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="idmdlcrLabel">Create Room </h4>
                        </div>
                        <div class="modal-body " id="id_mdl_create_body">
                            <form id="frm_create">
                                <label for="">Room Image </label>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <div class="demo-filepond-wrapper" id="uploaded_area">
                                                    <input name="image" accept="image/*" id="id_crt_image" type="file" 
                                                        max="1" style="display: none";>
                                                    <p style="font-weight: bold;font-size: 16px;" id="">Choose a image (Recommend Size 1280x720 in pixel) </p>
                                                    <div id="id_img_crt_area">
                                                        <div class="image-box"> 
                                                            <img class="img_overlay" id="blah" src="" alt="Image" />
                                                            <!-- <div class="overlay">sasa</div> -->
                                                        </div>
                                                    </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                </div>
                                <label for="">Status</label>
                                <div class="form-group">
                                    <select name="is_disabled" id="id_crt_is_disabled" class="form-control show-tick">
                                        <option value="0">Enabled</option>
                                        <option value="1">Disabled</option>
                                    </select>
                                </div>
                                <label for="">Name <b style="color:red;">*</b></label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="text" autocomplete="off" name="name" id="id_crt_name" required=""  class="form-control" placeholder="Name">
                                    </div>
                                </div>
                                <label for="">Location</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <textarea rows="4"  name="location" class="form-control no-resize"  id="id_crt_location" placeholder="Please type the room location..."></textarea>
                                    </div>    
                                    
                                </div>
                                <label for="">Room Capacity <b style="color:red;">*</b></label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="number" name="capacity" id="id_crt_capacity" required=""  class="form-control" placeholder="Capacity">
                                    </div>
                                </div>
                            <?php if ($modules['automation']['is_enabled'] == 1): ?>
                                <label for="">Automation Active</label>
                                <div class="form-group">
                                    <select name="is_automation" id="id_crt_automation_active" class="form-control show-tick">
                                        <option value="0">Off</option>
                                        <option value="1">On</option>
                                    </select>
                                </div>
                                <label for="">Automation List</label>
                                <div class="form-group">
                                    <select name="automation_id" id="id_crt_automation" class="form-control show-tick"></select>
                                </div>
                            <?php else: ?>

                            <?php endif; ?>
                                
                                <label for="">Room Facility</label>
                                <div class="form-group">
                                    <select  data-live-search="true" multiple name="facility_room[]" id="id_crt_facility_room" class="form-control show-tick"></select>
                                </div>
                                <label for="">Working Day</label>
                                <div class="form-group">
                                    <select data-actions-box="true" multiple name="work_day[]" id="id_crt_workday" class="form-control show-tick">
                                        <option value="SUNDAY">SUNDAY</option>
                                        <option value="MONDAY">MONDAY</option>
                                        <option value="TUESDAY">TUESDAY</option>
                                        <option value="WEDNESDAY">WEDNESDAY</option>
                                        <option value="WEDNESDAY">THURSDAY</option>
                                        <option value="FRIDAY">FRIDAY</option>
                                        <option value="SATURDAY">SATURDAY</option>
                                    </select>
                                </div>
                                <label for="">Working Time <b style="color:red;">*</b></label>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6 col-xs-6 col-lg-6 col-sm-6">
                                            <div class="form-line">
                                                <input  type="text" name="work_start" id="id_crt_work_start"  class="timepicker form-control" required="" placeholder="Please choose a start time...">
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-xs-6 col-lg-6 col-sm-6">
                                            <div class="form-line">
                                                <input  type="text" name="work_end" id="id_crt_work_end"  class="timepicker form-control" required="" placeholder="Please choose a finish time...">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            <?php if ($modules['price']['is_enabled'] == 1): ?>
                                <label for="">Price of the room per room<b style="color:red;">*</b></label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input  type="number" name="price" id="id_crt_price"  class=" form-control" required="" placeholder="Price Room ...">
                                    </div>
                                    
                                </div>
                            <?php else: ?>

                            <?php endif; ?>
                               <!--  <label for="">Room Access Controll</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input autocomplete="off" type="text" name="access_id" id="id_crt_access_id" required=""  class="form-control" placeholder="Access Number Ex. 1 ">
                                    </div>
                                </div> -->
                            
                                <br>
                                <button type="submit" class="btn btn-primary m-t-15 waves-effect">Save</button>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                        </div>
                    </div>
                </div>
            </div>
    <div class="modal fade" id="id_mdl_update" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="idmdlcrLabel">Update Room</h4>
                        </div>
                        <div class="modal-body " id="id_mdl_update">
                            <form id="frm_update">
                                <label for="">Room Image </label>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <div class="demo-filepond-wrapper" id="uploaded_area2">
                                                    <input name="image" accept="image/*" id="id_edt_image" type="file" 
                                                        max="1" style="display: none";>
                                                    <p style="font-weight: bold;font-size: 16px;" id="">Choose a image (Recommend Size 1280x720 in pixel) </p>
                                                    <div id="id_img_crt_area2">
                                                        <div class="image-box"> 
                                                            <img class="img_overlay" id="blah2" src="" alt="Image" />
                                                        </div>
                                                    </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                </div>
                                <label for="">Status</label>
                                <div class="form-group">
                                    <select name="is_disabled" id="id_edt_is_disabled" class="form-control show-tick">
                                        
                                    </select>
                                </div>
                                <label for="">Name <b style="color:red;">*</b></label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="text" autocomplete="off" name="name" id="id_edt_name" required=""  class="form-control" placeholder="Name">
                                        <input type="hidden" autocomplete="off" name="id" id="id_edt_id" readonly="" required=""  >
                                        <input type="hidden" autocomplete="off" name="radid" id="id_edt_radid" readonly="" required=""  >
                                    </div>
                                </div>
                                <label for="">Location</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <textarea rows="4"  name="location" class="form-control no-resize"  id="id_edt_location" placeholder="Please type the room location..."></textarea>
                                    </div>    
                                    
                                </div>
                                <label for="">Room Capacity  <b style="color:red;">*</b></label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input required="" type="number" name="capacity" id="id_edt_capacity" required=""  class="form-control" placeholder="Capacity">
                                    </div>
                                </div>
                           
                            <?php if ($modules['automation']['is_enabled'] == 1): ?>
                                <label for="">Automation Active</label>
                                <div class="form-group">
                                    <select name="is_automation" id="id_edt_automation_active" class="form-control show-tick">
                                        <option value="0">Off</option>
                                        <option value="1">On</option>
                                    </select>
                                </div>
                                <label for="">Automation List</label>
                                <div class="form-group">
                                    <select name="automation_id" id="id_edt_automation" class="form-control show-tick"></select>
                                </div>
                            <?php else: ?>

                            <?php endif; ?>

                                <label for="">Room Facility</label>
                                <div class="form-group">
                                    <select data-actions-box="true" multiple name="facility_room[]" id="id_edt_facility_room" class="form-control show-tick"></select>
                                </div>
                                <label for="">Working Day <b style="color:red;">*</b></label>
                                <div class="form-group">
                                    <select data-actions-box="true" required="" multiple name="work_day[]" id="id_edt_workday" class="form-control show-tick">
                                     
                                    </select>
                                </div>
                                <label for="">Working Time <b style="color:red;">*</b></label>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6 col-xs-6 col-lg-6 col-sm-6">
                                            <div class="form-line">
                                                <input required="" type="text" name="work_start" id="id_edt_work_start"  class="timepicker form-control" placeholder="Please choose a start time...">
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-xs-6 col-lg-6 col-sm-6">
                                            <div class="form-line">
                                                <input required="" type="text" name="work_end" id="id_edt_work_end"  class="timepicker form-control" placeholder="Please choose a finish time...">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            <?php if ($modules['price']['is_enabled'] == 1): ?>
                                <label for="">Price of the room per room<b style="color:red;">*</b></label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input  type="number" name="price" id="id_edt_price"  class=" form-control" required="" placeholder="Price Room ...">
                                    </div>
                                    
                                </div>
                            <?php else: ?>

                            <?php endif; ?>
                               <!--  <label for="">Room Access Controll</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input autocomplete="off" type="text" name="access_id" id="id_edt_access_id" required=""  class="form-control" placeholder="Access Number Ex. 1 ">
                                    </div>
                                </div> -->
                                <br>
                                <button type="submit" class="btn btn-primary m-t-15 waves-effect">Update</button>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                        </div>
                    </div>
                </div>
            </div>
    <!-- # END MODAL CREATE  -->
    <?php $this->load->view("_partials/js_dashboard.php");?>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/sweatalert2/sweetalert2.all.min.js"></script>
    <!-- Moment Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment.js"></script>
    <!-- Bootstrap Material Datetime Picker Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js"></script>
    <script src="<?= base_url()?>assets/external/numeral/numeral.min.js"></script>
    <script src="<?= base_url()?>assets/external/numeral/locale.id.js"></script>
    <input type="hidden" id="id_baseurl" value="<?= base_url()?>">
    <script>
        var uploadimageCrt = false;
        var gAutomation = [];
        var gFacility= [];
        var assetsImageUrl = "";
        var enabledRoom = ["Enabled", "Disabled"];
        $(function(){
            init();
            getAutomation();
            getFacility();
            inputFile()
        }) 
        function readURL(inputthis, selector, idx) {
          if (inputthis.target.files.length > 0) {
            var reader = new FileReader();
            reader.onload = function(e) {
              // selector.css('background-img', e.target.result);
              idx.attr('src', e.target.result);
            }
            reader.readAsDataURL(inputthis.target.files[0]);
          }
        }
        function inputFile(){
            var datainput = $('#id_crt_image');
            $('#uploaded_area').click(function(){
                datainput[0].click();
            });
            datainput.on('change', function(e){
                // $('#id_img_crt_area').html("");
                if(e.target.files.length > 0){
                    readURL(e, $('#uploaded_area'), $('#blah'));
                    uploadimageCrt = true;
                }else{
                    $('#blah').attr('src', "#");

                    uploadimageCrt = false;
                    // $('#uploaded_area p').html("Choose a file") ;
                }
            })
        }
        function inputFile2(){
            var bs = $('#id_baseurl').val();
            var datainput = $('#id_edt_image');
            $('#uploaded_area2').click(function(){
                datainput[0].click();
            });
            datainput.on('change', function(e){
                if(e.target.files.length > 0){
                    readURL(e, $('#uploaded_area2'), $('#blah2'));
                    uploadimageCrt = true;
                }else{
                    $('#blah2').attr('src',bs+"file/room/"+assetsImageUrl );
                    uploadimageCrt = false;
                    // $('#uploaded_area p').html("Choose a file") ;
                }
            })
        }
        function getModule(){
            var modules = $('#id_modules').val();
            return JSON.parse(modules)
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
        function createData(){
            $('#id_mdl_create').modal('show');
            $('#id_crt_room').html('');
            $('#id_crt_devices').html('');
            var html_automation = "";
            html_automation += "<option value='' disabled>Please choose a automation ...</option>";
            for (var i in gAutomation){
                html_automation += "<option value="+gAutomation[i].id+">"+gAutomation[i].name+"</option>";
            }

            var html_facility = "";
            html_facility += "<option value='' disabled>Please choose a facility ...</option>";
            for (var i in gFacility){
                html_facility += "<option value='"+gFacility[i]+"'>"+gFacility[i]+"</option>";
            }
            $('#id_crt_automation').html(html_automation);
            $('#id_crt_facility_room').html(html_facility);
            enable_datetimepicker()
            select_enable()
        }
        $('#frm_create').submit(function(e){
            e.preventDefault();
            var form =  $('#frm_create').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"room/post/create",
                type : "POST",
                dataType: "json",
                data:  new FormData(this),
                contentType: false,
                cache: false,
                processData:false,
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
        $('#frm_update').submit(function(e){
            e.preventDefault();
            var form = $('#frm_update').serialize();
            var id = $('#id_edt_id').val()
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"room/post/update/"+id,
                type : "POST",
                dataType: "json",
                contentType: false,
                cache: false,
                processData:false,
                // data : form,
                data:  new FormData(this),
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

        function getAutomation(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"automation/get/data",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                       gAutomation = data.collection;
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function getFacility(){
            gFacility = [];
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"facility/get/data",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                       gFacility = [];
                       $.each(data.collection, function(index, item){
                        gFacility.push(item.name);
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
        function init(){
            var bs = $('#id_baseurl').val();
            var modules = getModule();
            $.ajax({
                url : bs+"room/get/data",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        clearTable($('#tbldata'));
                        var html = "";
                        var nn = 0;
                        $.each(data.collection, function(index, item){
                            nn++;
                            var automation = item.is_automation == 0 ? "Unactive" : "Active";
                            var price = item.price == null || item.price == "" ? 0 : item.price ;
                            var ra_name = item.ra_name == null  ? "" : item.ra_name
                            html += '<tr>'
                            html += '<td>'+nn+'</td>';
                            html += '<td style="width:150px;">'+item.radid+'</td>';
                            html += '<td style="width:150px;">'+item.name+'</td>';
                            html += '<td>'+item.capacity+'</td>';
                            html += '<td>'+item.work_time+'</td>';
                            // html += '<td>'+item.work_day+'</td>';
                            // 
                            if(modules['automation']['is_enabled'] ==  1){
                                html += '<td>'+automation+'</td>';
                                html += '<td>'+ra_name+'</td>';
                            }
                            if(modules['price']['is_enabled'] ==  1){
                                html += '<td>'+numeral(price).format('$ 0,0.00');+'</td>';
                            }
                            console.log(item)
                            html += '<td>'+enabledRoom[item.is_disabled]+'</td>';

                            html += '<td>';
                            html += '<button \
                                 onclick="editData($(this))" \
                                 data-id="'+item.id+'" \
                                 data-id="'+item.name+'" \
                                 data-ra_id="'+item.ra_id+'" \
                                 type="button" class="btn btn-info waves-effect">Detail</button>';
                            html += ' <button \
                                 onclick="removeData($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-danger waves-effect"><i class="material-icons">delete</i> </button> ';
                            html += '</td>';
                            html += '</tr>';
                        })
                        $('#tbldata tbody').html(html);

                        initTable($('#tbldata'));
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }

        function editData(t){
            var id = t.data('id');
            var bs = $('#id_baseurl').val();
            var modules = getModule();
            $('#frm_update')[0].reset();
            assetsImageUrl = "";
            $.ajax({
                url : bs+"room/get/edit/"+id,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        var input = data.collection;
                        var pathimage = bs+"file/room/";
                        assetsImageUrl = input['image'];
                        var dataR = "";
                        for(var x in enabledRoom){
                            var sel = x == input['is_disabled'] ? "selected" : "";
                            dataR += '<option '+sel+' value="'+x+'">'+enabledRoom[x]+'</option>'
                        }
                        $('#id_edt_is_disabled').html(dataR)
                        // console.log(pathimage+input['image'] )
                        // $('#blah2').attr('src',bs+"file/room/"+assetsImageUrl );
                        $('#blah2').attr("src",pathimage+input['image']);
                        $('#id_edt_radid').val(input['radid']);
                        $('#id_edt_name').val(input['name']);
                        $('#id_edt_capacity').val(input['capacity']);
                        $('#id_edt_automation_active').val(input['serial']);
                        $('#id_edt_location').val(input['location'])
                        $('#id_edt_id').val(id);
                        var timest = input['work_time'];
                        var timesp = timest.split('-');
                        var html_room = "";
                        var html_devices="";
                        $('#id_edt_work_start').val(timesp[0]);
                        $('#id_edt_work_end').val(timesp[1]);
                        $('#id_edt_price').val(input['price']);
                        $('#id_edt_access_id').val(input['access_id']);
                        var wrkdaydb = input['work_day'];
                        var wrkdaydb_array = wrkdaydb.split(",");

                        var facilitydb = input['facility_room'];
                        var facilitydb_array = facilitydb.split(",");

                        var html_aut = "";
                        var html_wkd = "";
                        var html_active = "";

                        if(modules['automation']['is_enabled'] ==  1){
                            var active = ["Off", "On"];
                            $.each(active,function(index, item) {
                                var sl = "";
                               if (input['is_automation'] == index) { sl = "selected" }
                                html_active += "<option "+sl+" value="+index+">"+item+"</option>";
                            });
                            html_aut += "<option value='' disabled>Please choose a automation ...</option>";
                            $.each(gAutomation,function(index, item) {
                                var sl = "";
                                if (input['automation_id'] == item.id) {
                                    sl = "selected";
                                }
                                html_aut += "<option "+sl+" value="+item.id+">"+item.name+"</option>";
                            });
                        }
                        var workday = ["SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY"];
                        $.each(workday,function(index, item) {
                            var sl = "";
                            if (wrkdaydb_array.indexOf(item.toUpperCase()) >= 0) {
                                sl = "selected";
                            }
                            html_wkd += "<option "+sl+" value="+item+">"+item+"</option>";
                        });
                        var html_facility = "";
                        html_facility += "<option value='' disabled>Please choose a facility ...</option>";
                        for (var i in gFacility){
                            var selcted = "";
                            if(facilitydb_array.indexOf(gFacility[i]) >= 0) { selcted = "selected"}
                            html_facility += "<option "+selcted+" value='"+gFacility[i]+"'>"+gFacility[i]+"</option>";
                        }
                        $('#id_edt_automation').html(html_aut);
                        $('#id_edt_workday').html(html_wkd);
                        $('#id_edt_automation_active').html(html_active);
                        $('#id_edt_facility_room').html(html_facility);
                        enable_datetimepicker()
                        select_enable()
                        $('#id_mdl_update').modal('show');
                        inputFile2();
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
                text: "You will lose the data room "+name+" !",
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
                            url : bs+"room/post/delete",
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
                                    showNotification('alert-success', "Succes deleted room "+name ,'top','center')
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
