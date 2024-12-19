var uploadimageCrt = false;
        var enabledVar = [1, 0];
        var enabledGeneral = ["Disabled", "Enabled"];
        var booleanSelect = ["False", "True"];
        var dataTemplateId = {
            invitation : "id_temp_inv_",
            reschedule : "id_temp_res_",
            cancel : "id_temp_cancel_",
        }
        var variableSetting = {};
        $(function(){
            initVariable();
            initPantry();
            // initEmailSMTP();
            // initEmailTemplate()
            // CKEDITOR.editorConfig = function( config ) {
            //     config.toolbarGroups = [
            //         { name: 'document', groups: [ 'mode', 'document', 'doctools' ] },
            //         { name: 'editing', groups: [ 'find', 'selection', 'spellchecker', 'editing' ] },
            //         { name: 'forms', groups: [ 'forms' ] },
            //         '/',
            //         { name: 'clipboard', groups: [ 'clipboard', 'undo' ] },
            //         { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
            //         { name: 'paragraph', groups: [ 'list', 'indent', 'blocks', 'align', 'bidi', 'paragraph' ] },
            //         { name: 'links', groups: [ 'links' ] },
            //         { name: 'insert', groups: [ 'insert' ] },
            //         { name: 'styles', groups: [ 'styles' ] },
            //         { name: 'colors', groups: [ 'colors' ] },
            //         { name: 'tools', groups: [ 'tools' ] },
            //         { name: 'others', groups: [ 'others' ] },
            //         { name: 'about', groups: [ 'about' ] },
            //         '/'
            //     ];
            //     config.height = 150;
            //     config.removeButtons = 'Image,Flash,Table,HorizontalRule,Smiley,SpecialChar,PageBreak,Iframe,Styles,Format,Maximize,ShowBlocks,About,BidiLtr,BidiRtl,Language,Outdent,Indent,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Scayt,Find,Replace,Save,NewPage,Preview,Print,Templates,Source,SelectAll,Cut,Copy,Paste,PasteText,PasteFromWord,NumberedList,BulletedList,CreateDiv,Blockquote';
            // };
            // CKEDITOR.replace('id_inv_content_text');
            // CKEDITOR.replace('id_inv_footer_text');
            // CKEDITOR.replace('id_temp_inv_content');
            // CKEDITOR.replace('id_temp_res_content');
            // CKEDITOR.replace('id_temp_cancel_content');

            // CKEDITOR.config.height = 200;
        }) 
        
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
       
        $('#frm_save_general').submit(function(e){
            e.preventDefault();
            var form =  $('#frm_save_general').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url: location.origin + "/api/SettingRuleBooking/Update",
                type : "POST",
                dataType: "json",
                data:  form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        showNotification('alert-success', data.msg,'top','center')
                    }else{
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }) 
        $('#frm_save_invoice').submit(function(e){
            e.preventDefault();
            $('#id_inv_content_text').val(CKEDITOR.instances['id_inv_content_text'].getData());
            $('#id_inv_footer_text').val(CKEDITOR.instances['id_inv_footer_text'].getData());
            // console.log($('#id_inv_content_text').val())
            // console.log($('#id_inv_footer_text').val())
            var form =  $('#frm_save_invoice').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"setting/post/invoice-config",
                type : "POST",
                dataType: "json",
                data:  form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        showNotification('alert-success', data.msg,'top','center')
                    }else{
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        })   
        // $('#id_frm_setting_smtp').submit(function(e){
        //     e.preventDefault();
        //     var form =  $('#id_frm_setting_smtp').serialize();
        //     var bs = $('#id_baseurl').val();
        //     $.ajax({
        //         url : bs+"setting/post/email-smtp",
        //         type : "POST",
        //         dataType: "json",
        //         data:  form,
        //         beforeSend: function(){
        //             $('#id_loader').html('<div class="linePreloader"></div>');
        //         },
        //         success:function(data){
        //             if(data.status == "success"){
        //                 showNotification('alert-success', data.msg,'top','center')
        //             }else{
        //                 showNotification('alert-danger', data.msg,'top','center')
        //             }
        //             $('#id_loader').html('');
        //         },
        //         error: errorAjax
        //     })
        // })  
        // $('#id_frm_setting_temp_inv').submit(function(e){
        //     e.preventDefault();
        //     var form =  $('#id_frm_setting_temp_inv').serialize();
        //     var bs = $('#id_baseurl').val();
        //     $.ajax({
        //         url : bs+"setting/post/email-template/invitation",
        //         type : "POST",
        //         dataType: "json",
        //         data:  form,
        //         beforeSend: function(){
        //             $('#id_loader').html('<div class="linePreloader"></div>');
        //         },
        //         success:function(data){
        //             if(data.status == "success"){
        //                 showNotification('alert-success', data.msg,'top','center')
        //             }else{
        //                 showNotification('alert-danger', data.msg,'top','center')
        //             }
        //             $('#id_loader').html('');
        //         },
        //         error: errorAjax
        //     })
        // })  
        // $('#id_frm_setting_temp_res').submit(function(e){
        //     e.preventDefault();
        //     var form =  $('#id_frm_setting_temp_res').serialize();
        //     var bs = $('#id_baseurl').val();
        //     $.ajax({
        //         url : bs+"setting/post/email-template/reschedule",
        //         type : "POST",
        //         dataType: "json",
        //         data:  form,
        //         beforeSend: function(){
        //             $('#id_loader').html('<div class="linePreloader"></div>');
        //         },
        //         success:function(data){
        //             if(data.status == "success"){
        //                 showNotification('alert-success', data.msg,'top','center')
        //             }else{
        //                 showNotification('alert-danger', data.msg,'top','center')
        //             }
        //             $('#id_loader').html('');
        //         },
        //         error: errorAjax
        //     })
        // })  
        // $('#id_frm_setting_temp_cancel').submit(function(e){
        //     e.preventDefault();
        //     var form =  $('#id_frm_setting_temp_cancel').serialize();
        //     var bs = $('#id_baseurl').val();
        //     $.ajax({
        //         url : bs+"setting/post/email-template/cancel",
        //         type : "POST",
        //         dataType: "json",
        //         data:  form,
        //         beforeSend: function(){
        //             $('#id_loader').html('<div class="linePreloader"></div>');
        //         },
        //         success:function(data){
        //             if(data.status == "success"){
        //                 showNotification('alert-success', data.msg,'top','center')
        //             }else{
        //                 showNotification('alert-danger', data.msg,'top','center')
        //             }
        //             $('#id_loader').html('');
        //         },
        //         error: errorAjax
        //     })
        // })  
        function initVariable(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                url: location.origin + "/api/VariableTimeDuration/GetAllVariables",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                      variableSetting = data['collection'];
                      initBooking();
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function initBooking(){
            var bs = $('#id_baseurl').val();
            var modules = getModule();
            $.ajax({
                url: location.origin + "/api/SettingRuleBooking/GetAllSettingRuleBookings",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    console.log(data)
                    if(data.status == "success"){
                        var html = "";
                        var nn = 0;
                        var input = data.collection;
                        var html_dur_cancel = '';
                        var html_dur = '';
                        var html_maxdur = '';
                        for(var x in variableSetting['duration']){
                            console.log(input[0].unuse_cancel_fee, variableSetting['duration'][x]['time'])
                            var sel = variableSetting['duration'][x]['time'] == input[0].unuse_cancel_fee? "selected" : "";
                            html_dur_cancel += '<option '+sel+' value="'+variableSetting['duration'][x]['time']+'">'+variableSetting['duration'][x]['time']+'min</option>';
                        }
                        for(var x in variableSetting['duration']){
                            var sel = variableSetting['duration'][x]['time'] == input[0].duration? "selected" : "";
                            html_dur += '<option '+sel+' value="'+variableSetting['duration'][x]['time']+'">'+variableSetting['duration'][x]['time']+'min</option>';
                        }
                        var maxdur = 4;
                        var _duration = input[0].duration-0;
                        for(var x = _duration; x <= (_duration*4) ; x+=_duration){
                            var sel = x == input[0].max_display_duration? "selected" : "";
                            html_maxdur += '<option '+sel+' value="'+x+'">'+x+'min</option>';
                        }
                        $('#id_max_end_meeting').val(input[0].max_end_meeting);
                        $('#id_notif_unused_meeting').val(input[0].notif_unused_meeting);
                        $('#id_notif_unuse_before_meeting').val(input[0].notif_unuse_before_meeting);
                        $('#id_unuse_cancel_fee').html(html_dur_cancel);
                        $('#id_booking_duration').html(html_dur);
                        // $('#id_booking_duration').html(html_dur);
                        $('#id_booking_max_dur').html(html_maxdur);
                        var html_ext_max = '';
                        for(var x in variableSetting['time_extend']){
                            html_ext_max += '<option value="'+variableSetting['time_extend'][x]['time']+'">'+variableSetting['time_extend'][x]['time']+'min</option>';
                        }
                        $('#id_booking_extend_max').html(html_ext_max);

                        var html_pin_room = '';
                        for(var x in enabledVar){
                            var mn = enabledVar[x];
                            html_pin_room += '<option value="'+mn+'">'+enabledGeneral[mn]+'</option>';
                        }
                        $('#id_booking_room_pin_activated').html(html_pin_room);
                        var html_extend = '';
                        for(var x in enabledVar){
                            var mn = enabledVar[x];
                            html_extend += '<option value="'+mn+'">'+enabledGeneral[mn]+'</option>';
                        }
                        $('#id_booking_extend_activated').html(html_extend);
                        $('#id_booking_duration').val(input[0].duration);
                        $('#id_booking_pin_number').val(input[0].room_pin_number);
                        // $('#id_booking_pin_refresh').val(input['room_pin_refresh']);
                        $('#id_booking_extend_notification').val(input[0].extend_meeting_notification);
                        // 
                        // $.each(data.collection, function(index, item){
                            
                        // $('#tbldata tbody').html(html);

                        // initTable($('#tbldata'));
                        datepickerActivate($('#id_booking_pin_refresh'));
                        select_enable();
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }

        function _swTimeRefresh(e){
            var tm =  moment(e.date).format('YYYY-MM-DD');
            var dates =  moment(e.date).format(' D MMMM YYYY');
        }
        function datepickerActivate(selectorfrom, _function = null){
            selectorfrom.bootstrapMaterialDatePicker({ date: false, format : 'HH:mm:ss' });
           
        }
        function initPantry(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                url: location.origin + "/api/SettingPantryConfig/GetAllSettingPantryConfigs",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                      var c =  data.collection;
                      if(c[0].status == 1){
                        $('#id_switch_pantry').prop('checked', true);
                      }else{
                        $('#id_switch_pantry').prop('checked', false);
                      }
                      $('#id_pantry_expired').val(c[0].pantry_expired)
                      $('#id_pantry_max_order_qty').val(c[0].max_order_qty)
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function oncPantryStatus(){
            var bs = $('#id_baseurl').val();
            var x = $('#id_switch_pantry').prop('checked');
            var status =1;
            if(x){
                status =1;
            }else{
                status = 0;
            }
            var data = {status : status}
            $.post(location.origin + "/api/SettingPantryConfig/Update", data);
            console.log(x);
        }
        $('#frm_save_pantry').submit(function(e){
            e.preventDefault();
            var form = $('#frm_save_pantry').serialize();
            var x = $('#id_switch_pantry').prop('checked');
            if (x) 
                form = form + "&status=1";
            else
                form = form + "&status=0";
            var bs = $('#id_baseurl').val();
            $.ajax({
                url: location.origin + "/api/SettingPantryConfig/UpdateOrCreatePantryConfig",
                type : "POST",
                dataType: "json",
                data:  form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        showNotification('alert-success', data.msg,'top','center')
                    }else{
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
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