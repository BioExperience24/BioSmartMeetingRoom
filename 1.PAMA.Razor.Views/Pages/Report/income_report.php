
<?php  
date_default_timezone_set("Asia/Jakarta");
$nbulan = array("", "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember");
$year = date("Y")-0;
// echo date('w');
// die();
?>
<!DOCTYPE html>
<html lang="en">
<head>
   <?php $this->load->view("_partials/head_css_dashboard.php", array('pagename'=>$pagename));?>
    <link href="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
   <link href="<?= base_url()?>assets/external/daterangepicker/daterangepicker.css" rel="stylesheet" />
    
    <!-- <link href="<?= base_url()?>assets/theme/plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet" /> -->
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
            <!-- DIV  -->
            <div class="row clearfix">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="card">
                        <div class="header">
                            <div class="row clearfix">
                                <div class="col-xs-12 col-sm-6">
                                    <h2>Report Of Rent Income</h2>
                                </div>
                                <div class="col-xs-12 col-sm-6 align-right">
                    
                                </div>
                            </div>
                        </div>
                        <div class="body">
                            <div class="row clearfix">
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                             <select data-live-search="true" show-tick name="" id="id_filter_year" class="form-control ">
                                                <option value="">CHOOSE YEAR</option>
                                                <?php for($n=$year;$n>2000;$n-- ) { ?>
                                                     <option  value="<?= $n?>"><?= $n?>
                                                    </option>
                                                <?php } ?>
                                            </select>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                             <select data-live-search="true" show-tick name="" id="id_filter_month" class="form-control ">
                                                <option value="">CHOOSE MONTH</option>
                                                <?php for($i=1;$i<=12;$i++ ) { ?>
                                                     <option  value="<?= $i?>"><?= $nbulan[$i]?>
                                                    </option>
                                                <?php } ?>
                                            </select>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                         <button  class="btn btn-block  btn-primary " onclick="filterReport()">Filter Report</button>
                                        </div>
                                   </div>
                            <div class="row clearfix">
                               <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                   Total Cost
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                   <b id="id_total">Rp0</b>
                                </div>
                            </div>  
                            <div class="row clearfix">
                               <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                   Periode
                                </div>
                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                   <b id="id_period_year"></b>
                                </div>  
                                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                   <b id="id_period_month"></b>
                                </div>
                                
                            </div>   
                             <div class="row clearfix">
                               <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                  <button id="id_btn_export"  class="btn btn-success " onclick="alertExport('excell')">Export to Excell</button>
                                </div>
                               
                            </div>        
                            <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                            <table id="id_tbl_room" class="table table-hover table-bordered">
                                                <thead>
                                                    <th></th>
                                                    <th>Booking No.</th>
                                                    <th>Alocation</th>
                                                    <th>Room</th>
                                                    <th>Usage Duration</th>
                                                    <th>Rent Cost</th>
                                                    <th>Status</th>
                                                </thead>    
                                                <tbody></tbody>  
                                            </table>
                                        </div>
                                        
                                   </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    
    <!-- # END MODAL CREATE  -->
    <?php $this->load->view("_partials/js_dashboard.php");?>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
    <script src="<?= base_url()?>assets/theme/plugins/sweatalert2/sweetalert2.all.min.js"></script>
    <!-- Moment Plugin Js -->
    <script src="<?= base_url()?>assets/theme/plugins/momentjs/moment.js"></script>
    <!-- Bootstrap Material Datetime Picker Plugin Js -->
    <!-- <script src="<?= base_url()?>assets/theme/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js"></script> -->
    <script src="<?= base_url()?>assets/external/daterangepicker/daterangepicker.js"></script>
    <script src="<?= base_url()?>assets/external/numeral/numeral.min.js"></script>
    <script src="<?= base_url()?>assets/external/numeral/locale.id.js"></script>

    <!-- base url -->
    <input type="hidden" id="id_baseurl" value="<?= base_url()?>">
    <script>
        numeral.locale('id');
        var globalStatusInvoice = [];
        $(function(){
            // init();
            // getAutomation();
            // getFacility();
            initStartDate();
            getStatus();
            $('#id_btn_export').hide();
            
        }) 
        function filterReport(){
            init();
        }
        function initStartDate(){
           
            $('.input-group #id_daterange_room').daterangepicker({
                "showDropdowns": true,
                "showWeekNumbers": true,
                "showISOWeekNumbers": true,
                "opens": "center",
                // "drops": "up",
                "startDate": moment().subtract(29, 'days').format('MM/DD/YYYY'),
                "endDate": moment().format('MM/DD/YYYY'),
                "autoApply": true,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
                    'Last Year': [moment().subtract(1, 'year').startOf('year'), moment().subtract(1, 'year').endOf('year')]
                },
                "alwaysShowCalendars": true,
            }, function(start, end, label) {
                // init();
              // getTableOngoing(start.format('YYYY-MM-DD'),end.format('YYYY-MM-DD'))
              // console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
            });
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
        function checkInvoiceStatus(enabled_stt, stt){
            if(enabled_stt == 0 && enabled_stt == "0"){
                console.log(globalStatusInvoice)
                return globalStatusInvoice[3].name;
            }else{
                var sttus = stt -0;
                var ret = "";
                $.each(globalStatusInvoice, (index, item) => {
                    if(item.id == sttus){
                        ret = item.name
                    }
                })
                return ret;
            }
            

        }
        function init(){
            var year = $("#id_filter_year").val();
            var month = $("#id_filter_month").val();
            var monthText = $("#id_filter_month option:selected").text();
            var bs = $('#id_baseurl').val();
            if(month == ""){
                var url =bs+"report/get/income-year/"+year;
            }else{
                var url =bs+"report/get/income-month/"+year+"/"+month;
            }
            $.ajax({
                url : url,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    // $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                       clearTable($('#id_tbl_room'));
                       var col = data.collection;
                       var html = '';
                       var num = 0;
                       var totalincome = 0;
                       $('#id_btn_export').show();
                       $.each(col, (index, item) => {
                            num ++;
                            var time1 = moment(item.start).format('hh:mm a');
                            var time2 = moment(item.end).format('hh:mm a');
                            var datetime = item.date + " " + time1 + " - " +time2;
                            var dur  = (item.total_duration-0) + (item.extended_duration-0);
                            var setHour = dur/item.duration_per_meeting;

                            totalincome += (item.invoice_rent_cost-0);
                            var rent_cost = numeral(item.invoice_rent_cost).format('$0,0.0');
                            var status_invoice = "";
                            if(item.alcoation_type_invoice_status == item.alocation_invoice_status){
                                status_invoice = checkInvoiceStatus(item.alcoation_type_invoice_status, item.invoice_status);
                            }else{
                                status_invoice = checkInvoiceStatus(item.alocation_invoice_status, item.invoice_status);
                            }
                            html += '<tr>';
                            html += '<td>'+num+'</td>';
                            html += '<td>'+item.booking_id+'</td>';
                            html += '<td>'+item.alocation_name+'</td>';
                            html += '<td>'+item.room_name+'</td>';
                            html += '<td>'+setHour+' hours</td>';
                            html += '<td>'+rent_cost+'</td>';
                            // if()
                            html += '<td><b style="color:red;">PAID</b></td>';
                          
                            html += '</tr>';
                        });
                        $('#id_total').html(numeral(totalincome).format('$0,0.0'));

                        if(month == ""){
                            $('#id_period_month').html(" - ");
                            $('#id_period_year').html(year);
                        }else{
                            $('#id_period_month ').html(monthText);
                            $('#id_period_year').html(year);
                        }
                       $('#id_tbl_room tbody').html(html);
                       initTable($('#id_tbl_room'));
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function getStatus(){
            globalStatusInvoice = [];
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"report/get/status-invoice",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                      globalStatusInvoice = data.collection;
                      // console.log(globalStatusInvoice);
                      // init();
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        
        function alertExport(type){
            var bs = $('#id_baseurl').val();
            var year = $("#id_filter_year").val();
            var month = $("#id_filter_month").val();
            if(year == ""){
                var msg = "Something wrong, please choose year";
                showNotification('alert-danger', msg,'top','center')
                return false;
            }else{

            }
            switch(type){
                case "excell" : 
                    if(month == ""){
                        var url =bs+"report/export-year/income/excell/"+year;
                    }else{
                        var url =bs+"report/export-month/income/excell/"+year+"/"+month;
                    }
                    exportExcell(url)
                    break;
                default:
                     var msg = "Something wrong to export format";
                    showNotification('alert-danger', msg,'top','center')
                    break;
               
            }
        }
        function exportExcell(url){
            Swal.fire({
                title:'Are you sure you want export it?',
                text: "",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Export it !',
                cancelButtonText: 'Close !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                        // window.location.href = url
                        window.open(url, '_blank');
                      
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
