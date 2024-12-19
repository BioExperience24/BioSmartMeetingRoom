
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

    <link href="<?= base_url()?>assets/external/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

   <link href="<?= base_url()?>assets/external/daterangepicker/daterangepicker.css" rel="stylesheet" />

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
                       
                        <div class="body">
                            <ul class="nav nav-tabs tab-nav-right" role="tablist">
                                <li onclick="changeTabs('roomusage')" role="presentation" class="active" ><a href="#roomusage" data-toggle="tab" >TRANSACTION</a></li>
                                <li  onclick="changeTabs('organizer')"  role="presentation"><a href="#organizer" data-toggle="tab">ORDER USER</a></li>
                               
                            </ul>
                            <div class="tab-content">
                                <div role="tabpanel" class="tab-pane fade  in active" id="roomusage">
                                    <?php $this->load->view("Admin/PantryTransaction/pantry/view_transaction.php", array('pagename'=>$pagename));?>
                                </div>
                                <div role="tabpanel" class="tab-pane fade " id="organizer">
                                    <?php $this->load->view("Admin/Report/room/view_organizer.php", array('pagename'=>$pagename));?>
                                </div>
                                <div role="tabpanel" class="tab-pane fade" id="attendees">
                                     <?php $this->load->view("Admin/Report/room/view_attendees.php", array('pagename'=>$pagename));?>
                                </div>
                                <div role="tabpanel" class="tab-pane fade" id="settings">
                                </div>
                            </div>

                            <br>    
                            <br>    
                            <br>    
                            <div class="row clearfix">
                                
                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <i class="material-icons">date_range</i>
                                        </span>
                                        <div class="form-line">
                                            <input id="daterangepicker" type="text" class="form-control " >
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                       <select name="prefix_id" id="id_pantry_data" class="form-control show-tick"></select>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <button class="btn btn-primary" onclick="onFilter()" > Search </button>
                                </div>
                            </div>
                            <!-- row clearfix -->
                            <div class="row clearfix">
                                <div class="table-responsive responsive">
                                    <table class="table table-hover" id="tbldata">
                                        <thead>
                                                <th style="width: 20px">#</th>
                                                <th style="">Order ID</th>
                                                <th style="">Order Time</th>
                                                <th style="">Order By</th>
                                                <th style="">Booking Title</th>
                                                <th style="">Room</th>
                                                <th style="">Order Status</th>
                                                <th style="">Expired Time</th>
                                                
                                        </thead>
                                        <tbody></tbody>
                                    </table>  
                                </div>
                            </div>
                            <!-- row clearfix -->
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
                            <h4 class="modal-title" id="idmdlcrLabel">Create Facility</h4>
                        </div>
                        <div class="modal-body " id="id_mdl_create_body">
                            <form id="frm_create">
                                <label for="">Name</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input type="text" autocomplete="off" name="name" id="id_crt_name" required=""  class="form-control" placeholder="Facility Name">
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
    <?php $this->load->view("_partials/js_dashboard.php");?>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/sweatalert2/sweetalert2.all.min.js"></script>
    <!-- Moment Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment.js"></script>
    <!-- Bootstrap Material Datetime Picker Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/jquery-editable/src/table-edits.js"></script>

    <script src="<?= base_url()?>assets/external/daterangepicker/daterangepicker.js"></script>
    <script src="<?= base_url()?>assets/external/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <textarea  id="id_pantry" style="display: none;"><?= $pantry?></textarea>
    <input type="hidden" id="id_baseurl" value="<?= base_url()?>">
    <script>
        var localtimezone = moment.tz.guess();
        var gPantry = JSON.parse($('#id_pantry').val());
        var gPantry = [
            {id:0,value:"Order not yet processed"},
            {id:1,value:"Order processed"},
            {id:2,value:"Order Delivered"},
            {id:3,value:"Order Completed"},
            {id:4,value:"Order canceled"},
            {id:4,value:"Order rejected"},
        ];
        var gPantry = [];

    </script>
    <script>
        $(function(){
            initPantry();
            enable_datetimepicker()
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
            $('.input-group #daterangepicker').daterangepicker({
                "locale" : {
                    format:"YYYY-MM-DD"
                },
                "showWeekNumbers": true,
                "showISOWeekNumbers": true,
                "opens": "center",
                "drops": "down",
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                },
                "alwaysShowCalendars": true,
            }, function(start, end, label) {
              // console.log(start.format('YYYY-MM-DD'),end.format('YYYY-MM-DD'))
              // init(start.format('YYYY-MM-DD'),end.format('YYYY-MM-DD'))
              
              // console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
            });
            
        }
        function onFilter(){
            var date = $('#daterangepicker').val();
            var sp =  date.split(" - ");
            var st = sp[0].trim();
            var en = sp[1].trim();
            // console.log(st,en)
            init(st,en)
        }
        function createData(){
            $('#id_mdl_create').modal('show');
           
            enable_datetimepicker()
            select_enable()

        }
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
 
        function init(start = "",end = ""){
            var bs = $('#id_baseurl').val();
            var pantry = $('#id_pantry_data').val();
            $.ajax({
                url : bs+"pantry-transaction/get/data",
                type : "GET",
                data : {
                    start : start,
                    end : end,
                    pantry : pantry,
                },
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        clearTable($('#tbldata'));
                        var html = "";
                        var num = 0;
                        $.each(data.collection, function(index, item){
                            num ++;
                            var status_order = item.order_status;
                            var expired = "";
                            if(item.order_st == 0){
                                var dateexpired = moment(item.expired_at).unix();
                                var nowtime = moment().unix();
                                if(nowtime > dateexpired ){
                                    status_order = "Order expired";
                                    expired = moment(item.expired_at).format("DD MMM YYYY hh:mm A");
                                }
                            }
                            var orderTime = moment(item.order_datetime).format("DD MMM YYYY hh:mm A");
                            html += '<tr data-id="'+item.id+'">';
                            html += '<td>'+num+'</td>';
                            html += '<td>'+item.id+'</td>';
                            html += '<td>'+orderTime+'</td>';
                            html += '<td>'+(( item.emp_name==null ) ? "" : item.emp_name )+ '</td>';
                            html += '<td>'+(( item.title==null ) ? "" : item.title)+'</td>';
                            html += '<td>'+(( item.room_name==null ) ? "" : item.room_name)+'</td>';
                            html += '<td>'+status_order+'</td>';
                            html += '<td>'+expired+'</td>';
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
        function initPantry(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"pantry-transaction/get/data-pantry",
                type : "GET",

                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        // clearTable($('#tbldata'));
                        var html = "";
                        var num = 0;
                        gPantry = data.collection;
                        html += '<option value="" >All Pantry</option>';
                        $.each(data.collection, function(index, item){
                            html += '<option value="'+item.id+'" >'+item.name+'</option>';
                        });
                        $('#id_pantry_data').html(html);
                        select_enable()
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
