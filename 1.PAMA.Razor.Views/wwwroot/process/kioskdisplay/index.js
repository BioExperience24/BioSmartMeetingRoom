        
        var intervalBookingCrt;
        var enabledVar = [1, 0];
        var enabledString = ["Disabled", "Enabled"];
        var booleanString = ["False", "True"];
        var path = "assets/file/display/";
        var bgpath = path+"background/";
        var signagepath = path+"signage/";
        var groom = [];
        var gData = [];
        $(function(){
            moment.locale("en"); 
            init();
        })

        var typekiosk = [
            // {id:"display_signage", value:"Display Signage" },
            {id:"display_deskbooking", value:"Display Deskbooking" },
        ];
        
        
        function clickSubmit(id){
            $('#'+id).click();

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
        function colorpicker(){
            $('.colorpicker').colorpicker({
                format: 'hex'
            });
        }

        function enable_datetimepicker(){
            $('.input-group #daterangepicker').daterangepicker({
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
              console.log(start.format('YYYY-MM-DD'),end.format('YYYY-MM-DD'))
              init(start.format('YYYY-MM-DD'),end.format('YYYY-MM-DD'))
              // console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
            });
            
        }
        
        function init(){
            var bs = $('#id_baseurl').val();
            gData = [];
            $.ajax({
                url : bs+"display-kiosk/get/data",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        clearTable($('#tbldata'));
                        var col = data.collection;
                        var html = "";
                        var numm = 0;
                        groom =col;
                        gData = col;
                        $.each(col, function(index, item){
                            numm++;
                            var display_uuid = item.display_uuid == null ? "" : item.display_uuid;
                            var display_last = item.last_logged == null ? "" : item.last_logged;
                            // var status = item.enable_signage == 1 ? "Enabled" : "Disabled";
                            html += '<tr data-id="'+item.id+'">'
                            html += '<td>'+numm+'</td>';
                            html += '<td>'+item.display_serial+'</td>';
                            html += '<td>'+item.display_name+'</td>';
                            html += '<td>'+display_uuid+'</td>';
                            html += '<td>'+display_last+'</td>';
                            html += '<td>'; // 
                            html += '<button \
                                    onclick="openUpdate($(this))" \
                                    data-id="'+item.id+'" \
                                    data-num="'+index+'" \
                                    type="button" class="btn btn-info waves-effect "><i class="material-icons">mode_edit</i></button>';
                            html += '</td>'; // 
                            html += '<td>'; // 
                            if(item.is_logged == 1){
                                 html += '<button \
                                    onclick="logoutDeviceData($(this))" \
                                    data-id="'+item.id+'" \
                                    data-num="'+index+'" \
                                    type="button" class="btn btn-warning waves-effect "><i class="material-icons">input</i></button>';
                            }
                            html += '&nbsp;&nbsp;'; // 
                           
                            html += '</td>'; // 
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
            });
        }
        function openSignage(t){
            var bs = $('#id_baseurl').val();
            var id = t.data('room_id');
            var num = t.data('num');
            var display = groom[num];
            var pathfile = bs+signagepath+display.signage_media
            $('#id_si_old').attr('src', pathfile)
            $('#id_mdl_sigange').modal('show');

        }

        $('#frm_create').submit(function(e){
            e.preventDefault();
            Swal.fire({
                title:'Are you sure you want create it?',
                text: "You will add the kiosk/display !",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Save/Create !',
                cancelButtonText: 'Cancel !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                       createSave()
                    }
                    else{

                    }
            })
        })  
        function createSave(){
            var form =  $('#frm_create').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"display-kiosk/post/create",
                type : "POST",
                dataType: "json",
                data: form,
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
        }
        $('#frm_update').submit(function(e){
            e.preventDefault();
            e.preventDefault();
            Swal.fire({
                title:'Are you sure you want save it?',
                text: "You will save the kiosk/display  !",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Save !',
                cancelButtonText: 'Cancel !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                       updateSave()
                    }
                    else{

                    }
            })
            
        })  
        function updateSave(){
            var form =  $('#frm_update').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"display-kiosk/post/update",
                type : "POST",
                dataType: "json",
                data: form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        $('#frm_update')[0].reset();
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
        }
        $('#frm_signage').submit(function(e){
            e.preventDefault();
            var form =  $('#frm_signage').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"display/post/signage",
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
                        $('#frm_signage')[0].reset();
                        init();
                        $('#id_mdl_sigange').modal('hide');
                        $('#id_si_old')[0].pause();
                        showNotification('alert-success', data.msg,'top','center')
                    }else{
                        $('#id_si_old')[0].pause();
                        Swal.fire(
                              'Upload failed !!!',
                              data.msg,
                              'question'
                        )
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                    },
                error: errorAjax
            })
        })  
        function createData(){
            var html  = '';
            for(var x in typekiosk){
                html+= '<option value="'+typekiosk[x]['id']+'">'+typekiosk[x]['value']+'</option>';
            }
            $('#id_mdl_create').modal('show');
            $('#id_crt_display_type').html(html);
            $('#id_crt_display_name').val("");
            select_enable();
            
        }
        function openUpdate(t){
            var id = t.data('id');
            var num = t.data('num');
            var obj = gData[num];

             var html  = '';

            for(var x in typekiosk){
                var s = typekiosk[x]['id'] == obj['display_type'] ? "selected" : "";
                html+= '<option '+s+' value="'+typekiosk[x]['id']+'">'+typekiosk[x]['value']+'</option>';
            }
            $('#id_mdl_update').modal('show');
            $('#id_edt_display_name').val(obj.display_name);
            $('#id_edt_display_id').val(obj.id);
            select_enable();
            
        }
        function logoutDeviceData(t){
            var id = t.data('id');
            var form = new FormData();
            form.append('id', id);
            Swal.fire({
                title:'Are you sure you want to exit?',
                text: "Kiosk/Display will display the login page again !",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Logout !',
                cancelButtonText: 'Cancel !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                        var bs = $('#id_baseurl').val();
                        $.ajax({
                            url : bs+"display-kiosk/post/logout",
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
                                    showNotification('alert-success', "Succes logout kiosk "+name ,'top','center')
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

        function removeData(t){
            var id = t.data('id');
            var booking_id = t.data('booking_id');
            var name = t.data('name');
            var form = new FormData();
            form.append('id', id);
            form.append('booking_id', booking_id);
            form.append('name', name);
            Swal.fire({
                title:'Are you sure you want delete it?',
                text: "You will lose the data booking "+name+" !",
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
                            url : bs+"booking/post/delete",
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
                                    showNotification('alert-success', "Succes deleted booking "+name ,'top','center')
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
        function setButtonWavesEffect(event) {
            $(event.currentTarget).find('[role="menu"] li a').removeClass('waves-effect');
            $(event.currentTarget).find('[role="menu"] li:not(.disabled) a').addClass('waves-effect');
        }