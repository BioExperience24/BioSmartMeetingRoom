        var uploadimageCrt = false;
        var assetsImageUrl = "";
        var gChannel = [];
        var gRoom = [];
        var gChecked = {};
        var gModelCtrl = [
            {'name':"Reader",'value':'reader'},
            {'name':"Face Reader",'value':'face_reader'},
        ];
        $(function(){
            init();
            getChannel();
            getRoom();
        }) 



        function clickSubmit(id){
            $('#'+id).click();
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
        function enableControllerType(data, type){
            console.log(data, type)
            switch(data){
                case "custid":
                    if(type == "edit"){
                        $('#id_edt_controller_area_custid').show('slow');
                        $('#id_edt_controller_area_falco').hide('slow');

                    }else if(type == "add"){
                        $('#id_crt_controller_area_custid').show('slow');
                        $('#id_crt_controller_area_falco').hide('slow');
                    }
                    break;
                case "custom":
                    if(type == "edit"){
                        $('#id_edt_controller_area_custid').show('slow');
                        $('#id_edt_controller_area_falco').hide('slow');

                    }else if(type == "add"){
                        $('#id_crt_controller_area_custid').show('slow');
                        $('#id_crt_controller_area_falco').hide('slow');
                    }
                    break;
                case "falcoid":
                    if(type == "edit"){
                        $('#id_edt_controller_area_falco').show('slow');
                        $('#id_edt_controller_area_custid').hide('slow');

                    }else if(type == "add"){
                        $('#id_crt_controller_area_custid').hide('slow');
                        $('#id_crt_controller_area_falco').show('slow');
                    }
                    break;
                default : 
                    if(type == "edit"){
                        $('#id_edt_controller_area_falco').hide('slow');
                    }else if(type == "add"){
                        $('#id_crt_controller_area_falco').hide('slow');
                    }
                    break;
            }
        }
        function enable_datetimepicker(){
            $('.timepicker').bootstrapMaterialDatePicker({
                format: 'HH:mm',
                clearButton: true,
                date: false
            });
        }
        function createData(){
            var ctrl_type = $('#id_controller_type').val()
            var jsctrl_type = JSON.parse(ctrl_type);
            var htmlctrl = '<option value="">List Controller</option>';
            var htmlmodelctrl = '<option value="">List Model</option>';
            var html = '';
            html += '<option value=""></option>';
            $.each(gChannel, (index, item)=>{
                html += '<option value="'+item.channel+'">Ch '+item.channel+'</option>';
            })
            var htmlx = '<option value=""></option>';
            $.each(gRoom, (index, item)=>{
                htmlx += '<option value="'+item.radid+'">'+item.name+'</option>';
            })

            $.each(jsctrl_type, (index, item)=>{
                htmlctrl += '<option value="'+item.id+'">'+item.name+'</option>';
            })

            $.each(gModelCtrl, (index, item)=>{
                htmlmodelctrl += '<option value="'+item.value+'">'+item.name+'</option>';
            })

            $('#id_crt_channel').html(html)
            $('#id_crt_room').html(htmlx)
            $('#id_crt_controller_type').html(htmlctrl)
            $('#id_crt_model_controller').html(htmlmodelctrl)
            console.log(htmlmodelctrl)
            enableControllerType("", "add");
            $('#id_mdl_create').modal('show');
            enable_datetimepicker()
            select_enable()
        }
        function oncCRTControllerType(){
            var ct = $('#id_crt_controller_type').val()
            // console.log(ct)
            enableControllerType(ct, "add")
        }
        function oncEDTControllerType(){
            var ct = $('#id_edt_controller_type').val()
            enableControllerType(ct, "edit")
        }
        function openData(id,data){
            var html = '';
            var dataloop = [];
            $.each(data, (index, item) => {
                dataloop.push(item.room_id);
            });
            gChecked = {};
            $.each(gRoom, (index, item)=>{
                if(dataloop.indexOf(item.radid) > -1){
                    
                    gChecked[item.radid] = {
                        room : item.radid,
                        status : 1,
                    }
                    html += '<li class="list-group-item">\
                            <input onchange="onCheckRoom($(this))" type="checkbox" name="room-'+item.radid+'" class="filled-in" id="check-'+item.radid+'" checked value="'+item.radid+'">\
                            <label for="check-'+item.radid+'">'+item.name+'</label>\
                        </li>';
                }else{
                    gChecked[item.radid] = {
                        room : item.radid,
                        status : 0,
                    }
                    html += '<li class="list-group-item">\
                            <input onchange="onCheckRoom($(this))" class="filled-in" type="checkbox"  name="room-'+item.radid+'"  id="check-'+item.radid+'" value="'+item.radid+'">\
                            <label for="check-'+item.radid+'">'+item.name+'</label>\
                        </li>';
                }
            });
            $('#id_access_assign_id').val(id)
            $('#id_list_assign').html(html)
            $('#id_mdl_assign').modal('show');
            enable_datetimepicker()
            select_enable()
            // $('.ip').inputmask('099.099.099.099', { placeholder: '___.___.___.___' });
        }
        function onCheckRoom(t){
            var ck = t.is(":checked");
            gChecked[t.val()]['room'] = t.val();
            gChecked[t.val()]['status'] = ck == true ? 1 : 0;
        }
        function assignData(t){
            var html = '';
            var bs = $('#id_baseurl').val();
            var id = t.data('id');
            $.ajax({
                // url : bs+"access/get/data/integrated/"+id,
                url : bs + ajax.url.get_access_integrateds + `/${id}`,
                type : "POST",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        openData(id,data.collection);
                        // showNotification('alert-success', data.msg,'top','center')
                    }else{
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        $('#frm_assign').submit(function(e){
            e.preventDefault();
            var form =  $('#frm_assign').serialize();
            var formData = new FormData();
            var strChecked = JSON.stringify(gChecked);
            var id = $('#id_access_assign_id').val();
            formData.append("strdata", strChecked);
            formData.append("access", id);
            var bs = $('#id_baseurl').val();
            Swal.fire({
                title:'Are you sure you want save it?',
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
                        // var bs = $('#id_baseurl').val();
                        $.ajax({
                            // url : bs+"access/post/assign",
                            url : bs + ajax.url.post_assign_access_integrated,
                            type : "POST",
                            dataType: "json",
                            data:  formData,
                            processData: false,
                            contentType: false,
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                            },
                            success:function(data){
                                if(data.status == "success"){
                                    init();
                                    $('#id_mdl_assign').modal('hide');
                                    swalShowNotification('alert-success', data.msg,'top','center')
                                }else{
                                    swalShowNotification('alert-danger', data.msg,'top','center')
                                }
                                $('#id_loader').html('');
                            },
                            error: errorAjax
                        });
                    }
                else{

                }
            })
            
        }) 
        function saveCreate(){
            var form =  $('#frm_create')[0];
            var bs = $('#id_baseurl').val();
            $.ajax({
                // url : bs+"access/post/create",
                url : bs + ajax.url.post_create_access_controls,
                type : "POST",
                dataType: "json",
                data:  new FormData(form),
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
                          swalShowNotification('alert-success', data.msg,'top','center')
                    }else{
                          swalShowNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                    },
                error: errorAjax
            })
        }
        $('#frm_create').submit(function(e){
            e.preventDefault();
            if($('#id_crt_controller_type').val() == "falcoid"){
                if($('#id_crt_controller_falco_group_access').val() == ""){
                    Swal.fire(
                      'Attention!',
                      'Falco unit no cannot be empty!',
                      'warning'
                    )
                    $('#id_crt_controller_falco_group_access').focus()
                    return false;
                }
            }
           
            Swal.fire({
                title:'Are you sure you want save it?',
                text: "",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Save !',
                cancelButtonText: 'Cancel !',
                reverseButtons: true
            }).then((result) => {
                // console.log(result.value);
                if (result.value == true) {
                    saveCreate()
                }else{
                    return false
                }
            });
            
        })  
        function saveUpdate(){
            var form = $('#frm_update')[0];
            var id = $('#id_edt_id').val()
            var bs = $('#id_baseurl').val();
            $.ajax({
                // url : bs+"access/post/update/"+id,
                url : bs + ajax.url.post_update_access_controls + `/${id}`,
                type : "POST",
                dataType: "json",
                contentType: false,
                cache: false,
                processData:false,
                // data : form,
                data:  new FormData(form),
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        $('#frm_update')[0].reset();
                        $('#id_edt_room').html("");
                        $('#id_edt_channel').html("")
                        $('#id_mdl_update').modal('hide');
                        init();
                        swalShowNotification('alert-success', data.msg,'top','center')
                    }else{
                        swalShowNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        $('#frm_update').submit(function(e){
            e.preventDefault();

            if($('#id_edt_controller_type').val() == "falcoid"){
                if($('#id_edt_controller_falco_group_access').val() == ""){
                    Swal.fire(
                      'Attention!',
                      'Falco unit no cannot be empty!',
                      'warning'
                    )
                    $('#id_edt_controller_falco_group_access').focus()
                    return false;
                }
            }
           
            Swal.fire({
                title:'Are you sure you want save it?',
                text: "",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Save !',
                cancelButtonText: 'Cancel !',
                reverseButtons: true
            }).then((result) => {
                // console.log(result.value);
                if (result.value == true) {
                    saveUpdate()
                }else{
                    return false
                }
            });
            
        })

        function getChannel(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                // url : bs+"access/get/data/channel",
                url : bs + ajax.url.get_access_channels,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                },
                success:function(data){
                    if(data.status == "success"){
                        gChannel = data.collection;
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function getRoom(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                // url : bs+"room/get/data",
                url : bs + ajax.url.get_rooms,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                },
                success:function(data){
                    if(data.status == "success"){
                       gRoom = data.collection;
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
                // url : bs+"access/get/data",
                url : bs + ajax.url.get_access_controls,
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
                            html += '<tr>'
                            html += '<td>'+nn+'</td>';
                            html += '<td style="width:150px;">'+item.name+'</td>';
                            html += '<td>'+item.ip_controller+'</td>';
                            html += '<td>CH '+item.channel+'</td>';
                            html += '<td>'+item.room+' room</td>';
                            html += '<td>';
                            html += '<button \
                                 onclick="assignData($(this))" \
                                 data-id="'+item.id+'" \
                                 data-id="'+item.name+'" \
                                 type="button" class="btn btn-info waves-effect">Assign Room</button>';
                            html += '</td>';
                            html += '<td>';
                            html += '<button \
                                 onclick="editData($(this))" \
                                 data-id="'+item.id+'" \
                                 data-id="'+item.name+'" \
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
                        swalShowNotification('alert-danger', msg,'top','center')
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
            $('#id_edt_room').html("");
            $('#id_edt_channel').html("")
            assetsImageUrl = "";
            $.ajax({
                // url : bs+"access/get/edit/"+id,
                url : bs + ajax.url.get_access_control + `/${id}`,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        var input = data.collection.access;
                        var integrate = data.collection.integrate;
                        $('#id_edt_id').val(id);
                        $('#id_edt_name').val(input['name']);
                        $('#id_edt_controller').val(input['ip_controller']);
                        var htmlc = "";
                        $.each(gChannel,function(index, item) {
                            var sl = item.channel == input['channel'] ? "selected" : "";
                            htmlc += "<option "+sl+" value='"+item.channel+"' >"+item.channel+"</option>";
                        });
                        $('#id_edt_channel').html(htmlc)
                        var htmlx = '<option value=""></option>';
                        $.each(gRoom, (index, item)=>{
                            var checks = checkInArrayLoop(item.radid , integrate, 'room_id');
                            var sld = checks ? "selected" : "";
                            htmlx += '<option '+sld+' value="'+item.radid+'">'+item.name+'</option>';
                        })
                        $('#id_edt_room').html(htmlx)
                        var ctrl_type = $('#id_controller_type').val()
                        var jsctrl_type = JSON.parse(ctrl_type);
                        var htmlctrl = '<option value="">List Controller</option>';
                        var htmlmodelctrl = '<option value="">List Model</option>';

                        var controller_type = input['type'];
                        if(controller_type == "falcoid"){
                            $('#id_edt_controller_falco_group_access').val(input.falco_group_access)
                            $('#id_edt_controller_falco_unit_no').val(input.falco_unit_no)
                        }else  if(controller_type == "custid"){
                            $('#id_edt_controller_access_id').val(input.access_id)

                        }
                        $.each(jsctrl_type, (index, item)=>{
                            var sldct = item.id==input['type'] ? "selected" : "";
                            htmlctrl += '<option '+sldct+' value="'+item.id+'">'+item.name+'</option>';
                        });

                        $.each(gModelCtrl, (index, item)=>{
                            var sldmc = item.value==input['model_controller'] ? "selected" : "";
                            htmlmodelctrl += '<option '+sldmc+' value="'+item.value+'">'+item.name+'</option>';
                        })
                        $('#id_edt_controller_type').html(htmlctrl)
                        $('#id_edt_model_controller').html(htmlmodelctrl)
                        enableControllerType(controller_type, "edit");
                        enable_datetimepicker()
                        select_enable()
                        $('#id_mdl_update').modal('show');
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        swalShowNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                    
                },
                error: errorAjax
            })
        }
        function checkInArrayLoop(radid, myArray, key =""){
            var ret = false;
            for(var x in myArray){
                if(radid == myArray[x][key] ){
                    ret = true;
                }
            }
            return ret;
        }
        function removeData(t){
            var id = t.data('id');
            var name = t.data('name');
            var form = new FormData();
            form.append('id', id);
            form.append('name', name);
            Swal.fire({
                title:'Are you sure you want delete it?',
                text: "You will lose the data access control "+name+" !",
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
                            // url : bs+"access/post/delete",
                            url : bs + ajax.url.post_delete_access_controls,
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
                                    swalShowNotification('alert-success', "Succes deleted access control "+name ,'top','center')
                                    init();
                                }else{
                                    swalShowNotification('alert-danger', "Data not found",'bottom','left')
                                }
                            },
                            error: errorAjax,
                        })
                    }
                else{

                }
            })
            
        }
        function swalShowNotification(icon,title,loc = "", loc2="" ){
            var ic = "";
            if(icon == "alert-success"){
                ic = "success";
            }else if(icon == "alert-danger"){
                ic = "danger";
            }else if(icon == "alert-warning"){
                ic = "warning";
            }else if(icon == "alert-info"){
                ic = "info";
            }
            Swal.fire(
              title,
              '',
              ic
            )
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