        var settingGeneralVal = $('#id_settinggeneral').val();
        var statusNameVal = $('#id_statusname').val();
        var settingGeneral = JSON.parse(settingGeneralVal);
        var statusName = JSON.parse(statusNameVal);
        var detailObj = {};
        var fhours = 60; // jam
        $(function(){
            initStartDate();
            // init();
            
        }) 
        $.validator.addMethod("valueNotEquals", function(value, element, arg){
          return arg !== value;
         }, "Select item .");
        function filterInvoice(){
            var bs = $('#id_baseurl').val();

            var filter_status_invoice = $('#id_filter_status_invoice').val();
            var filter_alocation = $('#id_filter_alocation').val();
            var filter_daterange = $('#id_daterange').val();
            init(filter_status_invoice, filter_alocation, filter_daterange)
          
        }
        function clickSubmit(btn){
            $('#'+btn)[0].click();
        }
        function initStartDate(){
            $('.input-group #id_daterange').daterangepicker({
                "showDropdowns": true,
                "showWeekNumbers": true,
                "showISOWeekNumbers": true,
                "opens": "center",
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
            });
        }
        function enable_datetimepicker(){
            var dateval = moment().startOf('month').format('MM/DD/YYYY') +"-"+ moment().endOf('month').format('MM/DD/YYYY');
        }
        var gAutomation = [];
        var gFacility= [];
        $('#id_formreport').submit(function(e){
           
        })  
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
        
        function sendPublishInvoice(t){
            var name = t.data("name");
            var format = t.data("format");
            var memo = t.data("memo");
            var referensi = t.data("referensi");
            $('#id_id_mdl_createTitle').html(format)
            $('#id_frm_crt_memo').val(memo)
            $('#id_frm_crt_ref').val(referensi)
            $('#id_frm_crt_id').val(name)
            $('#id_mdl_create').modal('show');
            
        }
        function confirmPaidInvoice(t){
            var name = t.data("name");
            var memo = t.data("memo");
            var format = t.data("format");
            var invoiceid = t.data("invoiceid");
            var referensi = t.data("referensi");
            $('#id_mdl_confirmTitle').html(format)
            $('#id_frm_conf_memo').val(memo)
            $('#id_frm_conf_ref').val(referensi)
            $('#id_frm_conf_id').val(invoiceid)
            $('#id_mdl_confirm').modal('show');
        }
        function onGenerateInvoice(){
            var bs = $('#id_baseurl').val();
            var year = $('#id_gen_years').val();
            var form = {
                year : year,
            }
            $.ajax({
                url : bs+"invoice/generate/invoice/years/"+year,
                type : "GET",
                dataType: "json",
                // data : form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                        if(data.status == "success"){
                          init();
                          showNotification('alert-success', data.msg,'top','center')
                        }else{
                          showNotification('alert-danger', data.msg,'top','center')
                        }
                        $('#id_loader').html('');
                    },
                    error: errorAjax
            })
        }
        function sendFinanceInvoice(t){
            var format = t.data("format");
            var bookid = t.data("id");
            var memo = t.data("memo");
            var referensi = t.data("referensi");
            if (memo == null || referensi == null ){
                Swal.fire({
                    title:'Attention!!!',
                    text: "Please publish your invoice before send to finance !",
                    type: "warning",
                })
                return false;
            }
            Swal.fire({
                title:'Attention!!!',
                text: "Are you sure to send this invoice? No. Invoice "+format,
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Send Invoice !',
                cancelButtonText: 'Close !',
                reverseButtons: true
                }).then((result) => {
                if (result.value) {
                    var form = {
                        booking_id:bookid
                    }
                    var bs = $('#id_baseurl').val();
                    $.ajax({
                        url : bs+"invoice/post/send-invoice",
                        type : "POST",
                        dataType: "json",
                        data : form,
                        beforeSend: function(){
                            $('#id_loader').html('<div class="linePreloader"></div>');
                        },
                        success:function(data){
                            if(data.status == "success"){
                                init();
                                showNotification('alert-success', data.msg,'top','center')
                            }else{
                                  showNotification('alert-danger', data.msg,'top','center')
                            }
                            $('#id_loader').html('');
                        },
                        error: errorAjax
                    })  
                }
                else{

                }
            })
        }

        $('#frm_generate').validate({
            highlight: function (input) {
                $(input).parents('.form-line').addClass('error');
            },
            unhighlight: function (input) {
                $(input).parents('.form-line').removeClass('error');
               
            },
            errorPlacement: function (error, element) {
                $(element).parents('.form-group').append(error);
            },
        });

        $('#frm_generate').submit(function(e){
            e.preventDefault();
            if($(this).valid() == false){
                Swal.fire("Please select empty field");
                return false;
            }
            var p1= $('#id_generate_alocation_period1').val();
            var p2= $('#id_generate_alocation_period2').val();
            if(p1>p2){
                Swal.fire("Must choose from the beginning of the year to the end of the year.<br> ex. January to December");
                return false;
            }
            Swal.fire({
                title:'Attention!!!',
                text: "Are you sure to generate this invoice?!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Publish Invoice !',
                cancelButtonText: 'Close !',
                reverseButtons: true
                }).then((result) => {
                if (result.value) {
                    var form = $('#frm_generate').serialize();
                    var bs = $('#id_baseurl').val();
                    $.ajax({
                        url : bs+"invoice/post/generate-invoice",
                        type : "POST",
                        dataType: "json",
                        data : form,
                        beforeSend: function(){
                            // Swal.showLoading();
                            Swal.fire({
                                title:'Generate Invoice !',
                                text: "Please wait until finish !",
                                type: "info",
                                // showCancelButton: false,
                                // showConfirmButton: false,
                                allowOutsideClick:false,
                                // timerProgressBar: true,
                                onBeforeOpen: () => {
                                    Swal.showLoading();
                                }
                            })
                            // $('#id_loader').html('<div class="linePreloader"></div>');
                        },
                        success:function(data){
                            if(data.status == "success"){
                                // $('#frm_generate')[0].reset();
                                Swal.fire({
                                    title:'Generate Info !',
                                    text: data.collection.msg ,
                                    type: "success",
                                    showCancelButton: false,
                                    showConfirmButton: false,
                                    allowOutsideClick:false,
                                    timer: 2000,
                                    timerProgressBar: true,
                                })
                                // showNotification('alert-success', data.msg,'top','center')
                                // $('#id_mdl_create').modal('hide');
                                init();
                            }else{
                                Swal.fire({
                                    title:'Oops !',
                                    text: data.msg,
                                    type: "warning",
                                    
                                })
                                // showNotification('alert-danger', data.msg,'top','center')
                            }
                            $('#id_loader').html('');
                        },
                        error: errorAjax22
                    })  
                }
                else{

                }
            })
            
        })
        $('#frm_publish').submit(function(e){
            e.preventDefault();
             Swal.fire({
                title:'Attention!!!',
                text: "Are you sure to send this invoice?!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Send Invoice !',
                cancelButtonText: 'Close !',
                reverseButtons: true
                }).then((result) => {
                if (result.value) {
                    var form = $('#frm_publish').serialize();
                    var bs = $('#id_baseurl').val();
                    $.ajax({
                        url : bs+"invoice/post/send-invoice",
                        type : "POST",
                        dataType: "json",
                        data : form,
                        beforeSend: function(){
                            $('#id_loader').html('<div class="linePreloader"></div>');
                        },
                        success:function(data){
                            if(data.status == "success"){
                                $('#frm_publish')[0].reset();
                                $('#id_mdl_create').modal('hide');
                                init();
                                showNotification('alert-success', data.msg,'top','center')
                            }else{
                                  showNotification('alert-danger', data.msg,'top','center')
                            }
                            $('#id_loader').html('');
                        },
                        error: errorAjax
                    })  
                }
                else{

                }
            })
            
        })
        $('#frm_confirm').submit(function(e){
            e.preventDefault();
             Swal.fire({
                title:'Attention!!!',
                text: "Are you sure to confirm paid this invoice?!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Confirm Paid Invoice !',
                cancelButtonText: 'Close !',
                reverseButtons: true
                }).then((result) => {
                if (result.value) {
                    var form = $('#frm_confirm').serialize();
                    var bs = $('#id_baseurl').val();
                    $.ajax({
                        url : bs+"invoice/post/confirm-invoice",
                        type : "POST",
                        dataType: "json",
                        data : form,
                        beforeSend: function(){
                            $('#id_loader').html('<div class="linePreloader"></div>');
                        },
                        success:function(data){
                            if(data.status == "success"){
                                $('#frm_confirm')[0].reset();
                                $('#id_mdl_confirm').modal('hide');
                                init();
                                showNotification('alert-success', data.msg,'top','center')
                            }else{
                                  showNotification('alert-danger', data.msg,'top','center')
                            }
                            $('#id_loader').html('');
                        },
                        error: errorAjax
                    })  
                }
                else{

                }
            })
            
        })
        function init(filter_status_invoice = "", filter_alocation = "", filter_daterange= ""){
            // init(filter_status_invoice, filter_alocation, filter_daterange)
            filter_daterange = filter_daterange == "" ? $('#id_daterange').val():filter_daterange;
            var daterangesp = filter_daterange.split("-");
            var date1 = moment(daterangesp[0]).format('YYYY-MM-DD');
            var date2 = moment(daterangesp[1]).format('YYYY-MM-DD');
            dataform = {
                statusInvoice : filter_status_invoice,
                alocation : filter_alocation,
                date1 : date1 ,
                date2 : date2 ,
            }
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"invoice/get/data/filter",
                type : "GET",
                data : dataform,
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                       clearTable($('#id_tbl_room'));
                       var col = data.collection;
                       var html = '';
                       var num = 0;
                       $.each(col, (index, item) => {
                        num ++;
                        var time1 = moment(item.start).format('hh:mm a');
                        var time2 = moment(item.end).format('hh:mm a');
                        var month1 = "2020-"+item.invoice_month1+"-01";
                        var month2 = "2020-"+item.invoice_month2+"-01";
                        var formatmonth1 = moment(month1).format('MMMM');
                        var formatmonth2 = moment(month2).format('MMMM');
                        var datetime = item.date + " " + time1 + " - " +time2;
                        var datetimeCreated = moment(item.created_at).format('DD MMMM YYYY hh:mm a');
                        var datetimeUpdated = moment(item.updated_at).format('DD MMMM YYYY hh:mm a');
                        var dur  = (item.total_duration-0) + (item.extended_duration-0);
                        var setHour = dur/item.duration_per_meeting;
                        var rent_cost = numeral(item.cost_total_booking).format('$0,0.0');
                        var status_invoice = "";

                        if(item.alcoation_type_invoice_status == item.alocation_invoice_status){
                            status_invoice = checkInvoiceStatus(item.alcoation_type_invoice_status, item.status);
                        }else{
                            status_invoice = checkInvoiceStatus(item.alocation_invoice_status, item.status);
                        }
                        if(item.status == "1" || item.status == "2" ){
                            var financeCheck = '<i style="font-weight:bold;font-size:22px;color:light-green;" class="material-icons">done</i>';
                        }else{
                            var financeCheck = '<i style="font-weight:bold;font-size:22px;color:red;" class="material-icons">close</i>';
                        }
                        html += '<tr>';
                        html += '<td>'+num+'</td>';
                        html += '<td>';
                        if (status_invoice != "N/A") {
                            // NON CORPORATE
                            var color_btn = (item.status == "0") 
                                            ? 'btn-primary' 
                                            : (item.status == "1") 
                                            ? 'btn-warning': 'btn-success'  ;
                            html += '<div class="btn-group">\
                                        <button type="button" class="btn '+color_btn+' dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">\
                                            ACTION <span class="caret"></span>\
                                        </button>';
                            html += '   <ul class="dropdown-menu">';

                            if(item.status=="0" ){
                                html += '      <li><a data-export="0" data-invoiceid="'+item.invoice_id+'"  href="javascript:void(0);"  onclick="exportToExcell($(this))" >Export to Excell</a></li>';   
                                html += '      <li><a data-format="'+item.invoice_format+'"  data-referensi="'+item.referensi_no+'"  data-memo="'+item.memo_no+'"  data-name="'+item.invoice_id+'" onclick="sendPublishInvoice($(this))" href="javascript:void(0);">Send to Finance</a></li>';   
                                // html += '      <li><a data-format="'+item.invoice_format+'" data-id="'+item.invoice_id+'" data-referensi="'+item.referensi_no+'"  data-memo="'+item.memo_no+'"  onclick="sendFinanceInvoice($(this))" href="javascript:void(0);">Send to Finance</a></li>';   
                                html += '      <li><a href="javascript:void(0);">Edit Invoice</a></li>';   
                            }else  if(item.status=="1" ){
                                html += '      <li><a data-export="1" data-invoiceid="'+item.invoice_id+'"  href="javascript:void(0);" onclick="exportToExcell($(this))" >Export to Excell</a></li>';   
                                html += '      <li><a data-format="'+item.invoice_format+'" data-invoiceid="'+item.invoice_id+'"  data-referensi="'+item.referensi_no+'"  data-memo="'+item.memo_no+'"  data-name="'+item.invoice_id+'" onclick="confirmPaidInvoice($(this))"  href="javascript:void(0);">Confirm Paid</a></li>';   
                                html += '      <li><a href="javascript:void(0);">Edit Invoice</a></li>';   
                            }else if(item.status=="2" ){
                                html += '      <li><a data-export="2" data-invoiceid="'+item.invoice_id+'"  href="javascript:void(0);" onclick="exportToExcell($(this))" >Export to Excell</a></li>';   
                                html += '      <li><a href="javascript:void(0);">Edit Invoice</a></li>';   
                            }
                            html += '   </ul>\
                                    </div>';
                        }else{
                            html += '<div class="btn-group">\
                                        <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">\
                                            ACTION <span class="caret"></span>\
                                        </button>';
                            html += '   <ul class="dropdown-menu">';
                            html += '      <li><a data-export="0" data-invoiceid="'+item.invoice_id+'"  href="javascript:void(0);"  onclick="exportToExcell($(this))" >Export to Excell</a></li>';   
                            html += '      <li><a href="javascript:void(0);">Edit Invoice</a></li>';   

                            html += '   </ul>\
                                    </div>';
                        }
                        html += '</td>';
                        html += '<td>'+item.invoice_id+'</td>';
                        html += '<td>'+formatmonth1+' - ' + formatmonth2+ ' </td>';
                        html += '<td>'+item.invoice_years+'</td>';
                        html += '<td>'+item.total_duration+' hour</td>';
                        html += '<td>'+item.total_cost+' hour</td>';
                        html += '<td>'+item.total_meeting+' hour</td>';
                        html += '<td>'+item.alocation_name+' hour</td>';
                        html += '<td>'+financeCheck+' </td>';
                        html += '<td>'+status_invoice+' </td>';
                        html += '</tr>';
                       });
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
        function exportToExcell(t){
            var export2 = t.data("export");
            var invoiceid = t.data("invoiceid");
            var bs = $('#id_baseurl').val();
            var url="";
            if (export2 == "0") {
                url = bs+"invoice/export/excell/before/"+invoiceid;
            }else if (export2 == "1"){
                url = bs+"invoice/export/excell/send/"+invoiceid;
            }else if (export2 == "2"){
                url = bs+"invoice/export/excell/paid/"+invoiceid;
            }
            if(url == ""){
                return false;
            }
            console.log(invoiceid)
            window.open(url, '_blank');

            // var form = {
            //     booking_id: bookid,
            // }
            // $.ajax({
            //     url : url,
            //     type : "GET",
            //     dataType: "json",
            //     data : form,
            //     beforeSend: function(){
            //         $('#id_loader').html('<div class="linePreloader"></div>');
            //     },
            //     success:function(data){
            //         if(data.status == "success"){
            //             init();
            //             showNotification('alert-success', data.msg,'top','center')
            //         }else{
            //               showNotification('alert-danger', data.msg,'top','center')
            //         }
            //         $('#id_loader').html('');
            //     },
            //     error: errorAjax
            // })

        }
        function checkInvoiceStatus(enabled_stt, stt){
            var statusInvoiceStr = $('#id_statusInvoice').val();
            var globalStatusInvoice;
            try{
                globalStatusInvoice = JSON.parse(statusInvoiceStr)
            }catch(e){
                globalStatusInvoice =[];
            }
            console.log(enabled_stt, stt)
            if(enabled_stt == 0 && enabled_stt == "0"){
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
        function closeDetail(){
            $('#id_row2').hide('fast');
            $('#id_row1').addClass('col-xs-12');
            $('#id_row1').addClass('col-sm-12');
            $('#id_row1').addClass('col-md-12');
            $('#id_row1').addClass('col-lg-12');
            $('#id_row1').removeClass('col-xs-6');
            $('#id_row1').removeClass('col-sm-6');
            $('#id_row1').removeClass('col-md-6');
            $('#id_row1').removeClass('col-lg-6');
        }
        function detail(t){
            var invid = t.data("id");
            var year = t.data("years");
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"invoice/get/detail-invoice/"+invid,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        $('#id_years_detail').html(year);

                        clearTable($('#tbldata'));
                        var html = "";
                        $('#id_row2').show('slow');
                        $('#id_row1').removeClass('col-xs-12');
                        $('#id_row1').removeClass('col-sm-12');
                        $('#id_row1').removeClass('col-md-12');
                        $('#id_row1').removeClass('col-lg-12');
                        $('#id_row1').addClass('col-xs-6');
                        $('#id_row1').addClass('col-sm-6');
                        $('#id_row1').addClass('col-md-6');
                        $('#id_row1').addClass('col-lg-6');
                        var num=0;
                        $.each(data.collection, function(index, item){
                            num++;
                            var invStatus = textStatus(item.invoice_status);
                            
                            html += '<tr>'
                            html += '<td>'+num+'</td>';
                            html += '<td> <button \
                            data-year="'+year+'" data-name="'+item.alocation_name+'" onclick="exportToExcell($(this))" \
                            data-id="'+item.invoice_id+'" data-alocation="'+item.alocation_id+'" type="button" \
                            class="btn btn-primary waves-effect">Export to Excell </button>\
                             </td>';
                            html += '<td>'+item.invoice_id+'</td>';
                            html += '<td>'+invStatus+' </td>';
                            html += '<td>'+item.no_urut+'</td>';
                            html += '<td>'+item.alocation_name+' </td>';
                            html += '<td>'+item.total_cost+' </td>';
                            html += '<td>'+item.total_duration / fhours+' Hour </td>';
                            html += '<td>'+item.total_meeting+' Meets </td>';
                            html += '<td> <button \
                            onclick="detailAlocation($(this))" \
                            data-alocation="'+item.alocation_id+'" data-year="'+year+'" data-name="'+item.alocation_name+'"  data-id="'+item.invoice_id+'" data-alocation="'+item.alocation_id+'"  type="button" \
                            class="btn btn-info waves-effect"> Detail </button>\
                             </td>';
                            html += '</tr>';

                        })
                        $('#tbldataRow2 tbody').html(html);
                        initTable($('#tbldataRow2'));
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function textStatus(txt){
            var invStatus = "";
            console.log(txt,statusName)
            for(var x in statusName){
                if(statusName[x]['id'] == txt){
                    invStatus = statusName[x]['name']; 
                    break;
                }
            }
            return invStatus;
        } 
        function initdETAIL(date1="", date2=""){
            if(date1 == "" || date2 == ""){
                date1 = moment().startOf('month').format('YYYY-MM-DD');
                date2 = moment().endOf('month').format('YYYY-MM-DD');
            }
            var years = $('#id_selected_years').val()
            // console.log(date1, date2);
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"invoice/get/data/from/"+date1+"/to/"+date2,
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
                            if(item['data']['total_duration'] == null ){
                                return false;
                            }
                            html += '<tr>'
                            html += '<td>'+item.id+'</td>';
                            html += '<td>'+item.name+'</td>';
                            if(item['data']['total_duration'] != null ){
                                html += '<td>'+((item.data.total_duration-0)/60)+' Hour </td>';
                                html += '<td>'+item.data.total_alocation+'</td>';
                                html += '<td>'+item.data.total_meeting+'</td>';
                            }else{
                                html += '<td></td>';
                                html += '<td></td>';
                                html += '<td></td>';
                            }
                            html += '<td> <button \
                            onclick="detailAlocation($(this))" \
                            data-id="'+item.id+'" type="button" \
                            data-name="'+item.name+'" data-from="'+date1+'" data-to="'+date2+'" class="btn btn-info waves-effect"> Detail </button>\
                             </td>';
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
        function exportToExcell222222(t){
            var inv = t.data("id");
            var name = t.data("name");
            var year = t.data("year");
            var alocation = t.data("alocation");
            window.location.href = "invoice/print-excell/alocation/"+inv+"/"+alocation;
        }

        function detailAlocation(t){
            var inv = t.data("id");
            var name = t.data("name");
            var year = t.data("year");
            var alocation = t.data("alocation");
            var bs = $('#id_baseurl').val();
            detailObj = {
                inv : inv,
                alocation : alocation,
                name : name,
                year : year,
            }
            $.ajax({
                url : bs+"invoice/get/detail/alocation/"+alocation+"/year/"+year,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    console.log(data)
                    if(data.status == "success"){
                        clearTable($('#tbldata_detail'));
                        $('#id_mdl_detail_alocation').html(name)
                        var html = "";
                        $('#id_mdl_detail').modal('show');

                        $.each(data.collection, function(index, item){
                            console.log(item)
                            var timearea = moment(item.start).format("hh:mm A") + " - " + moment(item.end).format("hh:mm A") 
                            var duration = (item.total_duration-0) + (item.extended_duration-0);
                            var jam = duration/60; // jam
                            html += '<tr>'
                            html += '<td>'+item.booking_id+'</td>';
                            html += '<td>'+item.title+'</td>';
                            html += '<td>'+item.pic+'</td>';
                            html += '<td>'+item.date+'</td>';
                            html += '<td>'+timearea+'</td>';
                            html += '<td>'+jam+' Hours</td>';
                            html += '<td>'+item.location+'</td>';
                            html += '<td>'+item.alocation_name+'</td>';
                           
                            // html += '<td>\
                            // <button \
                            // onclick="detail($(this))" \ 
                            // data-id="'+item.id+'" type="button" \
                            // data-from="'+date1+'" data-to="'+date2+'" \
                            // class="btn btn-info waves-effect"> Detail </button> \
                            // </td>';
                            html += '</tr>';

                        })
                        $('#tbldata_detail tbody').html(html);
                        initTable($('#tbldata_detail'));
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }

        function exportDetailAlocation(){
            window.location.href = "invoice/get/detail/alocation-excell/"+detailObj.alocation+"/year/"+detailObj.year;
           
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
        function errorAjax22(xhr, ajaxOptions, thrownError){
            $('#id_loader').html('');
            if(ajaxOptions == "parsererror"){
                var msg = "Status Code 500, Error Server bad parsing";
                Swal.fire({
                  type: 'error',
                  title: 'Oops...',
                  text: msg,
                  footer: '<a href="#">Please call the administrator</a>'
                })
                // showNotification('alert-danger', msg,'bottom','left')
            }else{
                var msg ="Status Code "+ xhr.status + " Please check your connection !!!";
                Swal.fire({
                  type: 'error',
                  title: 'Oops...',
                  text: msg,
                  footer: '<a href="#">Check your connection</a>'
                })
            }
        }