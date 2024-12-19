
<?php  
// date_default_timezone_set("Asia/Jakarta");
// echo date('w');
// die();
$gender = array( 'male', 'female');
?>
<!DOCTYPE html>
<html lang="en">
<head>
   <?php $this->load->view("_partials/head_css_dashboard.php", array('pagename'=>$pagename));?>
    <link href="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <link href="<?= base_url()?>assets/external/daterangepicker/daterangepicker.css" rel="stylesheet" />
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
                <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                    <div class="card">
                        <div class="header">
                            <div class="row clearfix">
                                <div class="col-xs-12 col-sm-6">
                                    <h2>User Profile </h2>
                                </div>
                                <div class="col-xs-12 col-sm-6 align-right">
                                   </div> 
                            </div>
                        </div>
                        <div class="body">
                          <form id="frm_create">
                                        <label for="">Name*</label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <input type="text" autocomplete="off" name="name" required=""  class="form-control" placeholder="Name" value="<?= @$profile['name']?>" >
                                            </div>
                                        </div>
                                        <label for="">NPK*</label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <input type="text" autocomplete="off" name="nik_display" required=""  class="form-control" placeholder="NPK" value="<?= @$profile['nik_display']?>" >
                                            </div>
                                        </div>
                                        <label for="">Email</label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                 <input type="text" autocomplete="off" name="email"  class="form-control" placeholder="Email" value="<?= @$profile['email']?>" >
                                            </div>
                                        </div>
                                        <label for="">No Phone</label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                 <input type="text" autocomplete="off" name="no_phone"  class="form-control" placeholder="No Phone" value="<?= @$profile['no_phone']?>" >
                                            </div>
                                        </div>
                                        <label for="">No Extension</label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                 <input type="text" autocomplete="off" name="no_ext"   class="form-control" placeholder="No Extension" value="<?= @$profile['no_ext']?>" >
                                            </div>
                                        </div>
                                        <label for="">Gender</label>
                                        <div class="form-group">
                                        	<select class="form-control"  name="gender" >
                                        		<?php foreach ($gender as $key => $value): ?>
                                        			<?php 
                                        			$selected = ""; 
                                        			if ($value == @$profile['gender']) {
                                        				$selected = "selected";
                                        			}
                                        			?>
                                        			<option <?= $selected?> value="<?= $value?>"><?=  strtoupper($value) ?></option>
                                        		<?php endforeach ?>	
                                        	</select>
                                            
                                        </div>
                                        <label for="">Birth Date</label>
                                        <div class="form-group">
                                            <div class="form-line">
                                                 <input id="id_birth_date" type="text" autocomplete="off" name="birth_date"  class="form-control" placeholder="Birth Date" value="<?= @$profile['birth_date']?>" >
                                            </div>
                                        </div>
                                        <label for="">Card  Number</label>
                                        <div class="form-group">
                                            <div class="form-line">
                                            	 <input type="text" autocomplete="off" name="card_number" class="form-control" placeholder="Card number" value="<?= @$profile['card_number']?>" >
                                               
                                            </div>
                                        </div>
                                        <label for="">Address</label>
                                        <div class="form-group">
                                            <div class="form-line">
                                            	<textarea name="address" rows="4" class="form-control">
                                            		<?= @$profile['address']?>
                                            	</textarea>
                                               
                                            </div>
                                        </div>
                                        <br>
                                        <button type="submit" class="btn btn-primary m-t-15 waves-effect">S A V E</button>
                                    </form>
                                </div>
                        </div>
                    </div>
                    <!-- end profile -->
                    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
	                    <div class="card">
	                        <div class="header">
	                            <div class="row clearfix">
	                                <div class="col-xs-12 col-sm-6">
	                                    <h2>User Form </h2>
	                                </div>
	                            </div>
	                        </div>
	                        <div class="body">
	                        	 <form id="frm_username">
                                    <label for="">Username*</label>
                                    <div class="form-group">
                                    	<div class="input-group">
                                            <div class="form-line">
                                                <input id="id_username" type="text" autocomplete="off" name="username" required=""  class="form-control" placeholder="Username" value="<?= $usernamenya?>"  >
                                            </div>
                                            <span class="input-group-addon">
                                            	
                                            </span>
                                        </div>
                                    </div>
                                    <label for="">Grup Access </label>
                                    <h4 class="font-bold col-red"><?= $group_user?></h4>
                                   
                                    <button type="submit" class="btn btn-primary m-t-15 waves-effect">S A V E</button>
                                </form>
	                        </div>
	                    </div>
	                    <!-- card -->
	                    <div class="card">
	                        <div class="header">
	                            <div class="row clearfix">
	                                <div class="col-xs-12 col-sm-6">
	                                    <h2>Change Password </h2>
	                                </div>
	                            </div>
	                        </div>
	                        <div class="body">
	                        	 <form id="frm_password">
                                    <label for="">Old Password*</label>
                                    <div class="form-group">
                                    	<div class="input-group">
                                            <div class="form-line">
                                                <input id="id_old_pass" type="password" autocomplete="off" name="old_pass" required=""  class="form-control" placeholder="Old Password"  >
                                            </div>
                                            <span class="input-group-addon">
                                            	<button  onclick="onLock('id_old_pass', $(this))"  type="button" class="btn btn-sm btn-default waves-effect">
				                                    <i class="material-icons">lock_outline</i>
				                                </button>
                                            </span>
                                        </div>
                                    </div>
                                    <label for="">New Password*</label>
                                    <div class="form-group">
                                    	<div class="input-group">
                                            <div class="form-line">
                                                <input id="id_new_pass" type="password" autocomplete="off" name="new_pass" required=""  class="form-control" placeholder="New Password"  >
                                            </div>
                                            <span class="input-group-addon">
                                            	<button  onclick="onLock('id_new_pass', $(this))"  type="button" class="btn btn-sm btn-default waves-effect">
				                                    <i class="material-icons">lock_outline</i>
				                                </button>
                                            </span>
                                        </div>
                                    </div>
                                    <label for="">Confirm Password*</label>
                                    <div class="form-group">
                                    	<div class="input-group">
                                            <div class="form-line">
                                                <input id="id_con_pass" type="password" autocomplete="off" name="con_pass" required=""  class="form-control" placeholder="Confirm Password"  >
                                            </div>
                                            <span class="input-group-addon">
                                            	<button onclick="onLock('id_con_pass', $(this))" type="button" class="btn btn-sm btn-default waves-effect">
				                                    <i class="material-icons">lock_outline</i>
				                                </button>
                                            </span>
                                        </div>
                                    </div>
                                    <button type="submit" class="btn btn-primary m-t-15 waves-effect">S A V E</button>
                                </form>
	                        </div>
	                    </div>
	                </div>
                </div>
            </div>

        </div>
    </section>
   
    <?php $this->load->view("_partials/js_dashboard.php");?>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/sweatalert2/sweetalert2.all.min.js"></script>
    <!-- Moment Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment.js"></script>
    <!-- Bootstrap Material Datetime Picker Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/jquery-editable/src/table-edits.js"></script>
	
	<script src="<?= base_url()?>assets/external/daterangepicker/daterangepicker.js"></script>
    <input type="hidden" id="id_baseurl" value="<?= base_url()?>">
    <script>
        $(function(){
        	initStartDate()
            // $('.datepicker').datepicker();
        }) 
        function onLock(id, t){
        	// t.find("i .material-icons");
        	var selector = $('#'+id);
        	console.log(selector.attr('type'))

        	if(selector.attr('type') == "password"){
        		selector.attr('type', "text");
        		t.find(".material-icons").html("lock_open")
        	}else{
        		t.find(".material-icons").html("lock_outline")
        		selector.attr('type', "password");

        	}
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
        function initStartDate(){
            $('#id_birth_date').daterangepicker({
            	 "singleDatePicker": true,
                "showDropdowns": true,
                "showWeekNumbers": true,
                "showISOWeekNumbers": true,
                "opens": "center",
                "startDate": moment().subtract(29, 'days').format('MM/DD/YYYY'),
                "endDate": moment().format('MM/DD/YYYY'),
                "autoApply": true,
                "alwaysShowCalendars": true,
            }, function(start, end, label) {
                // init();
            });
        }
        
        $('#frm_create').submit(function(e){
            e.preventDefault();
            var form =  $('#frm_create').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"profile/post/update",
                type : "POST",
                dataType: "json",
                data : form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                        if(data.status == "success"){
                          Swal("Update Info", data.msg, 'success');

                          showNotification('alert-success', data.msg,'top','center')
                        }else{
                          Swal("Update Info", data.msg, 'warning');
                          showNotification('alert-danger', data.msg,'top','center')
                        }
                        $('#id_loader').html('');
                    },
                    error: errorAjax
            })
        })  
        $('#frm_username').submit(function(e){
            e.preventDefault();
            var form =  $('#frm_username').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"profile/post/username",
                type : "POST",
                dataType: "json",
                data : form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                        if(data.status == "success"){

                          Swal("Update Info", data.msg, 'success');
                          showNotification('alert-success', data.msg,'top','center')
                        }else{
                          Swal("Update Info", data.msg, 'warning');

                          showNotification('alert-danger', data.msg,'top','center')
                        }
                        $('#id_loader').html('');
                    },
                    error: errorAjax
            })
        })  
        $('#frm_password').submit(function(e){
            e.preventDefault();
            var form =  $('#frm_password').serialize();
            var bs = $('#id_baseurl').val();
            var op = $('#id_old_pass').val();
            var np = $('#id_new_pass').val();
            var cp = $('#id_con_pass').val();
            var check = true;
            if(op == np){
            	Swal.fire("Attention!!!", "The new and old passwords are the same!", "warning")
                check = false;
                return false;
            } 
            if (np != cp) {
                check = false;
            	Swal.fire("Attention!!!", "Confirm passwords are not the same!", "warning")
                return false;
            }
            if(check == true){
                $.ajax({
                    url : bs+"profile/post/password",
                    type : "POST",
                    dataType: "json",
                    data : form,
                    beforeSend: function(){
                        $('#id_loader').html('<div class="linePreloader"></div>');
                    },
                    success:function(data){
                            if(data.status == "success"){
                              Swal("Update Info", data.msg, 'success');
                              showNotification('alert-success', data.msg,'top','center')
                              window.location.reload()
                            }else{
                              Swal("Update Info", data.msg, 'warning');
                              showNotification('alert-danger', data.msg,'top','center')
                            }

                            $('#id_loader').html('');
                        },
                        error: errorAjax
                })
            }
            
        })  
       

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
