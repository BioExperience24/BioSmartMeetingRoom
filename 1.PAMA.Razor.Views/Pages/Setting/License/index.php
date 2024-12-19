
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
    <style type="text/css">
        .img-circle {
            border-radius: 50%;
            height: 80px;
            width: 80px;
            border: 1px solid #fafafa;
        }
        .img-icon{
            width: 120px !important;
        }
        .vcenter {
            display: inline-block;
            position: relative;
        }
        .connected-area {
            height: 93px;
            display: table;
            width: 100%;
        }

    .wrapper-div {
        display: table-cell;
      vertical-align: middle
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
                                    <h2>License</h2>
                                </div>
                                <div class="col-xs-12 col-sm-6 align-right">
                    				
                                </div>
                            </div>
                        </div>
                        <div class="body">
                            <div class="row">
                                <div class="col-xs-12">
                                	<?php if ($license['status'] == 0): ?>
                                		<form id="frm_set_license">
	                                		<label for="">Upload License </label>
		                                    <div class="form-group">
		                                        <div class="form-line">
		                                            <input type="file" name="photo" id="" class="form-control">
		                                        </div>
		                                     </div>

		                                     <button onclick="clickSubmit('id_btn_type_crt_submit')" type="button" class="btn btn-primary waves-effect " >UPLOAD NEW LICENSE</button>
		                                    
	                                	</form>
                                	<?php elseif($license['status'] == 1): ?>
                                		<label for="">Serial </label>
		                                    <div class="form-group">
		                                        <div class="form-line">
		                                            <input readonly class="form-control" value="<?= $license['serial']?>">
		                                     </div>
		                                </div>
                                	<?php endif ?>
                                </div>
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
                                    <h2></h2>
                                </div>
                                <div class="col-xs-12 col-sm-6 align-right">
                    				
                                </div>
                            </div>
                        </div>
                        <div class="body">
                            <table id="tbl" class="table table-hover table-bordered">
                                <thead>
                                    <th>Name</th>
                                    <th>Type</th>
                                    <th>Expired</th>
                                    <th>QTY</th>
                                    <th>Status</th>
                                </thead>    
                                <tbody>
                                	<?php foreach ($license_list as $key => $value): ?>
                                		<?php 
                                			$xp = $value['is_lifetime'] == 1 ? "Perpetual" : $value['expired_at'];
                                			$stt = $value['status'] == 1 ? "Enabled" : 'Disabled';
                                		?>
                                		<tr>
                                			<td><?= $value['name']?></td>
                                			<td><?= $value['type']?></td>
                                			<td><?= $xp?></td>
                                			<td><?= $value['qty']?></td>
                                			<td><?= $stt?></td>
                                		</tr>
                                	<?php endforeach ?>
                                </tbody>  
                            </table>
                        </div>
                    </div>
                </div>
               
            </div>

        </div>
    </section>
    <div class="modal fade" id="id_mdl_set_alarm" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="idmdlcrLabel">Set Alarm Configuration </h4>
                        </div>
                        <div class="modal-body " id="id_mdl_create_body">
                            <form id="frm_set_alarm">
                                <label for="">Alarm Server Auth</label>
                                <div class="form-group">
                                    <div class="col-xs-3">
                                        <select name="auth_http" class="form-control" id="id_alarm_server_auth_http"></select>
                                    </div>
                                    <div class="col-xs-9">
                                        <div class="form-line">
                                            <input type="text" autocomplete="off" name="url_auth" id="id_alarm_server_auth" required=""  class="form-control" placeholder="ex server.local.alarm">
                                        </div>
                                    </div>
                                </div>
                                <label for=""> Server Auth</label>
                                <div class="form-group">
                                    <div class="col-xs-3">
                                        <select name="feed_http" class="form-control"  id="id_alarm_server_feedback_http"></select>
                                    </div>
                                    <div class="col-xs-9">
                                        <div class="form-line">
                                            <input type="text" autocomplete="off"  name="url_feedback"  id="id_alarm_server_feedback" required="" value="<?= ROOT_URL?>" class="form-control" placeholder="ex this.domain.local">
                                        </div>
                                    </div>
                                </div>
                                
                                <br>
                                <button type="submit" style="display: none;" id="id_btn_type_crt_submit" class="btn btn-primary m-t-15 waves-effect">Submit</button>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <div class="row clearfix">
                                <div class="col-xs-6 align-left">
                                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                                </div>
                                <div class="col-xs-6 align-right">
                                    <button onclick="clickSubmit('id_btn_type_crt_submit')" type="button" class="btn btn-primary waves-effect " >SAVE</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    
    <!-- # END MODAL CREATE  -->

 
    <!-- # END MODAL CREATE  -->
    <?php $this->load->view("_partials/js_dashboard.php");?>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/sweatalert2/sweetalert2.all.min.js"></script>
    <!-- Moment Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment.js"></script>
    <!-- Bootstrap Material Datetime Picker Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/jquery-editable/src/table-edits.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js"></script>
    <input type="hidden" id="id_baseurl" value="<?= base_url()?>">
    <textarea style="display:none;" id="id_alarm"><?= $alarm_integration?></textarea>
    <textarea style="display:none;" id="id_module_alarm"><?= $alarm_integration?></textarea>
    <textarea style="display:none;" id="id_feedback_collection"></textarea>
    <script>
        var alarm_integration = JSON.parse($('#id_alarm').val());
        var iconData = {};
        var arHttp = ["http://", "https://"];
        var cIAalarm ;

        function btnConfigIntegrationAlarm(){
            var auth = alarm_integration.url_auth;
            var feedback = alarm_integration.url_feedback;
            var s_auth = auth.split("//");
            var s_feed = feedback.split("//");

            var htmlAuth = "";
            var htmlFeedback = "";


            for(var i in arHttp){

                var xselA = s_auth[0]+"//" == arHttp[i] ? "selected" : "";
                var xselF = s_feed[0]+"//" == arHttp[i] ? "selected" : "";
                htmlAuth+= "<option "+xselA+" value='"+arHttp[i]+"'>"+arHttp[i]+"</option>";
                htmlFeedback+= "<option "+xselF+" value='"+arHttp[i]+"'>"+arHttp[i]+"</option>";
            }
           

            $('#id_alarm_server_feedback').val(s_feed[1] == null ? "" :s_feed[1])
            $('#id_alarm_server_feedback_http').html(htmlFeedback);
            $('#id_alarm_server_auth_http').html(htmlAuth);
            $('#id_alarm_server_auth').val(s_auth[1] == null ?"" :s_auth[1] );
            $('#id_mdl_set_alarm').modal('show');
            // enable_datetimepicker()
            select_enable()
        }
        function btnIntegrationAlarm(){
            if(alarm_integration.status_integration == 1){
                return;

            }
            var auth = alarm_integration.url_auth;
            var feedback = alarm_integration.url_feedback;
            var p_auth = alarm_integration.param_auth;
            var p_feed = alarm_integration.param_feed;
            feedback = feedback+p_feed;
            var qq = "?feedback="+feedback
            var url = auth+p_auth+qq;


            window.open(url,'targetWindow',
                                   `toolbar=no,
                                    location=no,
                                    status=no,
                                    menubar=no,
                                    scrollbars=yes,
                                    resizable=yes,
                                    width=500,
                                    height=500`);

            cIAalarm = setInterval(function(){
                if($('#id_feedback_collection').val() == "ok"){
                    clearInterval(cIAalarm);
                    window.location.reload();
                }
            }, 1000)
            
        }

        // id_btn_connect_alarm

        $(function(){
            if(alarm_integration.status_integration == 1){
                $('#id_btn_connect_alarm').hide();
                $('#id_btn_connect_alarm_disconnected').show();
                

            }
            init();
        }) 
        function clickSubmit(id){
            $('#'+id).click();
        }
        var gAutomation = [];
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
        function oncSearchIcon(selectText, selectShow){
            iconData['text'] = selectText
            iconData['show'] = selectShow
            iconData['value'] = ""
            $('#id_mdl_icon_list').modal('show');

        }
        function oncBtnIcon(t){
            var tx =t.find("span.icon-name").text();
            iconData['text'].val(tx)
            iconData['show'].html('<i class="material-icons">'+tx+'</i>');
            iconData['value'] = tx;
            $('#id_mdl_icon_list').modal('hide');
        }
        function createData(){
            $('#id_mdl_create').modal('show');
            enable_datetimepicker()
            select_enable()

        }
        function updateData(t){
            var id = t.data('id')
            // enable_datetimepicker()
            // select_enable()
            // e.preventDefault();
            // var form =  $('#frm_create').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"facility/get/data/detail/"+id,
                type : "POST",
                dataType: "json",
                // data : form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                        if(data.status == "success"){
                            var col = data.collection;
                            $('#id_mdl_update').modal('show');
                            $('#id_edt_name').val(col.name)
                            $('#id_edt_google_icon').val(col.google_icon)
                            $('#id_edt_id').val(col.id)
                            $('#id_edt_icon_google_icon').html(col.google_icon)

                        }else{
                            $('#id_mdl_update').modal('hide');
                            showNotification('alert-danger', data.msg,'top','center')
                        }
                        $('#id_loader').html('');
                    },
                    error: errorAjax
            })

        }
        $('#frm_set_alarm').submit(function(e){
            e.preventDefault();
            var form =  $('#frm_set_alarm').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"integration/save/alarm-config",
                type : "POST",
                dataType: "json",
                data : form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                        if(data.status == "success"){
                          alarm_integration.url_auth = data.collection.url_auth;
                          alarm_integration.url_feedback = data.collection.url_feedback;
                          $('#frm_set_alarm')[0].reset();
                          // $('#id_crt_icon_google_icon').html("");
                          init();
                          $('#id_mdl_set_alarm').modal('hide');
                          showNotification('alert-success', data.msg,'top','center')
                        }else{
                          showNotification('alert-danger', data.msg,'top','center')
                        }
                        $('#id_loader').html('');
                    },
                    error: errorAjax
            })
        })  
        $('#frm_create').submit(function(e){
            e.preventDefault();
            var form =  $('#frm_create').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"facility/post/create",
                type : "POST",
                dataType: "json",
                data : form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                        if(data.status == "success"){
                          $('#frm_create')[0].reset();
                          $('#id_crt_icon_google_icon').html("");
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
            var form =  $('#frm_update').serialize();
            var id = $('#id_edt_id').val()
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"facility/post/update/"+id,
                type : "POST",
                dataType: "json",
                data : form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                        if(data.status == "success"){
                          $('#frm_update')[0].reset();
                          $('#id_edt_icon_google_icon').html("");
                          init();
                          $('#id_mdl_update').modal('hide');
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
                        clearTable($('#tbldata'));
                        var html = "";
                        $.each(data.collection, function(index, item){
                            html += '<tr data-id="'+item.id+'">'
                            html += '<td data-field="name">'+item.name+'</td>';
                            html += '<td>';
                            // html += '<a class="btn btn-info waves-effect edit" title="Edit">\
                            //           <i class="material-icons">mode_edit</i>\
                            //         </a>\
                            //        ';
                             html += '&nbsp;&nbsp;&nbsp;';
                            html += '<button \
                                 onclick="updateData($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-default waves-effect "><i class="material-icons">edit</i></button>';
                            html += '<button \
                                 onclick="removeData($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-danger waves-effect "><i class="material-icons">delete</i></button>';
                            html += '</td>';
                            html += '</tr>';
                        })
                        $('#tbldata tbody').html(html);
                        $('#tbldata  tr').editable({
                            dropdowns: {},
                            dblclick: true,
                            button: true, // enable edit buttons
                            buttonSelector: ".edit", // CSS selector for edit buttons
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
                                $.post(bs+'facility/post/update/' + id, values);
                            },
                            cancel: function(values) {
                              $(".edit i", this)
                                .html('mode_edit')
                                .attr('title', 'Edit');
                            }
                          });
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


        function removeData(t){
            var id = t.data('id');
            var name = t.data('name');
            var form = new FormData();
            form.append('id', id);
            form.append('name', name);
            Swal.fire({
                title:'Are you sure you want delete it?',
                text: "You will lose the data facility "+name+" !",
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
                            url : bs+"facility/post/delete",
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
                                    showNotification('alert-success', "Succes deleted facility "+name ,'top','center')
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
