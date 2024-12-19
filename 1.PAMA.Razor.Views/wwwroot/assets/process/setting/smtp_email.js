	var uploadimageCrt = false;
	var enabledVar = [1, 0];
    var enabledGeneral = ["Disabled", "Enabled"];
    var booleanSelect = ["False", "True"];
    var dataTemplateId = {
            invitation : "id_temp_inv_",
            reschedule : "id_temp_res_",
            cancel : "id_temp_cancel_",
    }
    var gSmtpEmail = [];
    var gSmtpEmailCurrent = [];
	var variableSetting = {};
        $(function(){
            // initVariable();
            initEmailSMTP();
            initEmailTemplate()
            CKEDITOR.editorConfig = function( config ) {
                config.toolbarGroups = [
                    { name: 'document', groups: [ 'mode', 'document', 'doctools' ] },
                    { name: 'editing', groups: [ 'find', 'selection', 'spellchecker', 'editing' ] },
                    { name: 'forms', groups: [ 'forms' ] },
                    '/',
                    { name: 'clipboard', groups: [ 'clipboard', 'undo' ] },
                    { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
                    { name: 'paragraph', groups: [ 'list', 'indent', 'blocks', 'align', 'bidi', 'paragraph' ] },
                    { name: 'links', groups: [ 'links' ] },
                    { name: 'insert', groups: [ 'insert' ] },
                    { name: 'styles', groups: [ 'styles' ] },
                    { name: 'colors', groups: [ 'colors' ] },
                    { name: 'tools', groups: [ 'tools' ] },
                    { name: 'others', groups: [ 'others' ] },
                    { name: 'about', groups: [ 'about' ] },
                    '/'
                ];
                config.height = 150;
                config.removeButtons = 'Image,Flash,Table,HorizontalRule,Smiley,SpecialChar,PageBreak,Iframe,Styles,Format,Maximize,ShowBlocks,About,BidiLtr,BidiRtl,Language,Outdent,Indent,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Scayt,Find,Replace,Save,NewPage,Preview,Print,Templates,Source,SelectAll,Cut,Copy,Paste,PasteText,PasteFromWord,NumberedList,BulletedList,CreateDiv,Blockquote';
            };
            // CKEDITOR.replace('id_inv_content_text');
            // CKEDITOR.replace('id_inv_footer_text');
            CKEDITOR.replace('id_temp_inv_content');
            CKEDITOR.replace('id_temp_res_content');
            CKEDITOR.replace('id_temp_cancel_content');
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


        $('#id_frm_setting_temp_inv').submit(function(e){
            e.preventDefault();
            var form =  $('#id_frm_setting_temp_inv').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : location.origin + "/api/SettingEmailTemplate/Update",
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
        $('#id_frm_setting_temp_res').submit(function(e){
            e.preventDefault();
            var form =  $('#id_frm_setting_temp_res').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url: location.origin + "/api/SettingEmailTemplate/Update",
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
        $('#id_frm_setting_temp_cancel').submit(function(e){
            e.preventDefault();
            var form =  $('#id_frm_setting_temp_cancel').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url: location.origin + "/api/SettingEmailTemplate/Update",
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
       	function initEmailSMTP(){
            var bs = $('#id_baseurl').val();
            var modules = getModule();
            $.ajax({
                url: location.origin + "/api/SettingSmtp/GetAllSettingSmtps",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    // $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        var html = "";
                        var nn = 0;
                        var input = data.collection;
                        var html_dur = '';
                        var html_enabled = '';
                        var html_secure = '';
                        gSmtpEmail = data.collection;
                        var html_selected_smtp =``;
                        for(var x in gSmtpEmail){
                            if(gSmtpEmail[x].selected_email == 1 || gSmtpEmail[x].selected_email == "1"){
                                gSmtpEmailCurrent = gSmtpEmail[x];
                                html_selected_smtp +=`<option selected value='${gSmtpEmail[x].name}' >${gSmtpEmail[x].name}</option>`;
                                // break;
                            }else{
                                html_selected_smtp +=`<option value='${gSmtpEmail[x].name}' >${gSmtpEmail[x].name}</option>`;

                            }
                        }
                        // console.log(html_selected_smtp)
                        $('#id_smtp_name').html(html_selected_smtp);
                        select_enable();

                        onSelectedAccountInit();
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function applySMTP(){
            var namee = $('#id_smtp_name').val();
            Swal.fire({
                title:'Are you sure you want apply it?',
                text: "",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Save !',
                cancelButtonText: 'Cancel !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                        processApply();
                    }
                else{

                }
            })
            
        }
        function processApply(){
            var form =  $('#id_frm_setting_smtp').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : location.origin + "/api/settingsmtp/update",
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
        }
        $('#id_frm_setting_smtp').submit(function(e){
            e.preventDefault();
            applySMTP();
        })  
        function onSelectedAccount(){
            var namee = $('#id_smtp_name').val();
            for(var x in gSmtpEmail){
                if(gSmtpEmail[x].name == namee ){
                    gSmtpEmailCurrent = gSmtpEmail[x];
                    break;
                }
            }
            onSelectedAccountInit();
        }
        function onSelectedAccountInit(){
            if(gSmtpEmailCurrent.name == "BIO SMTP Server"){
                $('#id_frm_setting_smtp').hide("fast");
            }else if(gSmtpEmailCurrent.name == "Custom SMTP Server"){
                $('#id_frm_setting_smtp').show("fast");
            }else if(gSmtpEmailCurrent.name == "Disabled"){
                $('#id_frm_setting_smtp').hide("fast");
            }



            var html_dur = '';
            var html_enabled = '';
            var html_secure = '';
            var input = gSmtpEmailCurrent;

            for(var x in enabledVar){
                var mn = enabledVar[x];
                var seltext = input.is_enabled == mn ? "selected" : "";
                html_enabled += '<option '+seltext+' value="'+mn+'">'+enabledGeneral[mn]+'</option>';
            }
            for(var x in enabledVar){
                var mn = enabledVar[x];
                var seltext = input.secure == mn ? "selected" : "";
                html_secure += '<option '+seltext+'  value="'+mn+'">'+booleanSelect[mn]+'</option>';
            }

            $('#id_smtp_name_val').val(input.name);
            $('#id_smtp_host').val(input.host);
            $('#id_smtp_usernam').val(input.user);
            $('#id_smtp_password').val(input.password);
            $('#id_smtp_port').val(input.port);
            // console.log(html_enabled)
            $('#id_smtp_enable').html(html_enabled);
            $('#id_smtp_secure').html(html_secure);
            select_enable();
        }

        function initEmailTemplate(){
            var bs = $('#id_baseurl').val();
            var modules = getModule();
            $.ajax({
                url: location.origin + "/api/settingEmailTemplate/GetAllEmailTemplates",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    // $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        var html = "";
                        var nn = 0;
                        var input = data.collection;
                        var html_dur = '';
                        var html_secure = '';
                        $.each(input, (index, item) => {
                           var idTemp = dataTemplateId[item.type];
                            var html_enabled = '';

                           for(var x in enabledVar){
                                var mn = enabledVar[x];
                                var seltext = item.is_enabled == mn ? "selected" : "";
                                html_enabled += '<option '+seltext+' value="'+mn+'">'+enabledGeneral[mn]+'</option>';
                            }
                            $('#'+idTemp+'enable').html(html_enabled);
                            $('#'+idTemp+'title').val(item.title_of_text);
                            $('#'+idTemp+'to').val(item.to_text);
                            $('#'+idTemp+'agenda').val(item.title_agenda_text);
                            $('#'+idTemp+'date').val(item.date_text);
                            $('#'+idTemp+'room').val(item.room);
                            $('#'+idTemp+'detail').val(item.detail_location);
                            $('#'+idTemp+'greeting').val(item.greeting_text);
                            $('#'+idTemp+'content').val(item.content_text);
                            $('#'+idTemp+'attendance').val(item.attendance_text);
                            $('#'+idTemp+'no_attendance').val(item.attendance_no_text);
                            $('#'+idTemp+'close').val(item.close_text);
                            $('#'+idTemp+'support').val(item.support_text);
                            $('#'+idTemp+'foot').val(item.foot_text);
                            $('#'+idTemp+'link').val(item.link);

                        })
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
        function previewEmailFormat(formid){
            var form =  $('#'+formid).serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"setting/email-template/preview",
                type : "POST",
                // dataType: "json",
                data:  form,
                beforeSend: function(){
                    // $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    var myWindow = window.open("", "MsgWindow",  "width=1000,height=1000");
                    myWindow.document.write(data);
                    // window.open(url,windowName,'height=200,width=150');
                    // if(data.status == "success"){
                    //     showNotification('alert-success', data.msg,'top','center')
                    // }else{
                    //     showNotification('alert-danger', data.msg,'top','center')
                    // }
                    // $('#id_loader').html('');
                },
                error: errorAjax
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