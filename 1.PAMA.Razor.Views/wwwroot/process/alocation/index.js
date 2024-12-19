 var uploadimageCrt = false;
        var assetsImageUrl = "";
        var gAlocation = [];
        var gType = [];
        var gChecked = {};
        var gEmployee = {};
        var nload = 0;
        $(function(){
            initType();
            setTimeout(function(){

            }, 2000)
            getEmployee()
            // getRoom();
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
        function enable_datetimepicker(){
            $('.timepicker').bootstrapMaterialDatePicker({
                format: 'HH:mm',
                clearButton: true,
                date: false
            });
        }
        function createData_type(){
            
            $('#id_mdl_create_type').modal('show');
            select_enable()
            // $('.ip').inputmask('099.099.099.099', { placeholder: '___.___.___.___' });
        }
        function createData_alocation(){
            
            $('#id_mdl_create_alocation').modal('show');
            var html1 = "";
            $.each(gType, (index, item) => {
                html1 += '<option value="'+item.id+'">'+item.name+'</option>';
            });
            $('#id_crt_type').html(html1)
            // enable_datetimepicker()
            select_enable()
        }
        function openData(id,data, name = ""){
            var html = '';
            var dataloop = [];
            gChecked = {};
            $.each(data, (index, item) => {
                // console.log(item)
                dataloop.push(item.nik);
            });
            // console.log(dataloop)
            $.each(gEmployee, (index, item)=>{
                console.log(item)
                if(dataloop.indexOf(item.nik) > -1){
                    gChecked[item.nik] = {
                        nik : item.nik,
                        status : 1,
                    }
                    html += '<tr data-check="check-'+item.nik+'" onclick="clickListAssign($(this))">';
                    html += '<td  data-check="check-'+item.nik+'" onclick="clickListAssign($(this))" style="width:50px;"><input onchange="onCheckRoom($(this))" type="checkbox" name="employee-'+item.nik+'" class="filled-in" id="check-'+item.nik+'" checked value="'+item.nik+'">\
                    <label for="check-'+item.nik+'"></label></td>'
                    html += '<td>'+item.name+'</td>'
                    html += '</tr>';
                    
                }else{
                    gChecked[item.nik] = {
                        nik : item.nik,
                        status : 0,
                    }
                    html += '<tr data-check="check-'+item.nik+'" onclick="clickListAssign($(this))">';
                    html += '<td  data-check="check-'+item.nik+'" onclick="clickListAssign($(this))" style="width:50px;"><input onchange="onCheckRoom($(this))" type="checkbox" name="employee-'+item.nik+'" class="filled-in" id="check-'+item.nik+'" value="'+item.nik+'">\
                    <label for="check-'+item.nik+'"></label></td>'
                    html += '<td>'+item.name+'</td>'
                    html += '</tr>';
                    
                }
            });
            $('#id_access_assign_id').val(id)
            $('#id_list_assign tbody').html(html)
            $('#id_mdl_assign').modal('show');
            $('#id_assign_title').html('<u>'+name+'</br>');
            initTable($('#id_list_assign'));
            select_enable()
            // $('.ip').inputmask('099.099.099.099', { placeholder: '___.___.___.___' });
        }
        function clickListAssign(t){
            var idstr = t.data('check');
            $('#'+idstr).click();
        }
        function onCheckRoom(t){
            var ck = t.is(":checked");
            gChecked[t.val()]['nik'] = t.val();
            gChecked[t.val()]['status'] = ck == true ? 1 : 0;
        }
        function getEmployee(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"employee/get/data",
                type : "POST",
                dataType: "json",
                beforeSend: function(){
                },
                success:function(data){
                    if(data.status == "success"){
                        gEmployee= data.collection
                    }else{
                        showNotification('alert-danger', data.msg,'top','center')
                    }
                },
                error: errorAjax
            })
        }
        function assignData(t){
            var html = '';
            var bs = $('#id_baseurl').val();
            var id = t.data('id');
            var name = t.data('name');
            $.ajax({
                url : bs+"alocation/get/data/assign/"+id,
                type : "POST",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        openData(id,data.collection, name);
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
            formData.append("alocation", id);
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
                        gChecked = {};
                        // var bs = $('#id_baseurl').val();
                        $.ajax({
                            url : bs+"alocation/post/assign",
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
                                    initAlocation();
                                    $('#id_mdl_assign').modal('hide');
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
        $('#frm_create_type').submit(function(e){
            e.preventDefault();
            var form =  $('#frm_create_type').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"alocation/post/create/type",
                type : "POST",
                dataType: "json",
                data:  form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        initType()
                        $('#frm_create_type')[0].reset();
                        $('#id_mdl_create_type').modal('hide');
                        showNotification('alert-success', data.msg,'top','center')
                    }else{
                          showNotification('alert-danger', data.msg,'top','center')
                    }
                    $('#id_loader').html('');
                    },
                error: errorAjax
            })
        }) 
        $('#frm_create_alocation').submit(function(e){
            e.preventDefault();
            var form =  $('#frm_create_alocation').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"alocation/post/create/alocation",
                type : "POST",
                dataType: "json",
                data:  form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        initAlocation()
                        $('#frm_create_alocation')[0].reset();
                        $('#id_mdl_create_alocation').modal('hide');
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
                url : bs+"access/post/update/"+id,
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
                        $('#id_edt_room').html("");
                        $('#id_edt_channel').html("")
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

        function initAlocation(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"alocation/get/data/alocation",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                },
                success:function(data){
                    var arInvoice = ["Disabled", "Enabled"]; 
                    if(data.status == "success"){
                       gAlocation = data.collection;
                       clearTable($('#tbldataAlocation'));
                        var html = "";
                        var nn = 0;
                        $.each(data.collection, function(index, item){
                            nn++;
                            var invoice = item.invoice_status == 0 || item.invoice_status == null ? "Disabled":"Enabled";
                            html += '<tr data-id="'+item.id+'">'
                            html += '<td>';
                            html += '<a class="btn btn-info waves-effect edit" title="Edit">\
                                      <i class="material-icons">mode_edit</i>\
                                    </a>\
                                   ';
                            html += ' <button \
                                 onclick="removeDataAlocation($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-danger waves-effect"><i class="material-icons">delete</i> </button> ';
                            html += '</td>';
                            html += '<td data-field="department_code">'+item.department_code+'</td>';
                            html += '<td data-field="name">'+item.name+'</td>';
                            html += '<td data-field="type">'+item.type_name+'</td>';
                            html += '<td data-field="invoice_status">'+invoice+'</td>';
                            // html += '<td>';
                            // html += '<button \
                            //      onclick="assignData($(this))" \
                            //      data-id="'+item.id+'" \
                            //      data-name="'+item.name+'" \
                            //      type="button" class="btn btn-info waves-effect">Assign Employee</button>';
                            // html += '</td>';
                            
                            
                            html += '</tr>';
                        })
                        $('#tbldataAlocation tbody').html(html);
                        var ntype = [];
                        $.each(gType, (index, item) => {
                            ntype.push(
                                {
                                    id:item.id,
                                    name:item.name,
                                }
                            )
                            // ntype.push(item.name)
                        })
                        // console.log(ntype)
                        $('#tbldataAlocation  tr').editable({
                            dropdowns: {
                              type: ntype,
                              invoice_status: arInvoice,
                            },
                            dblclick: true,
                            button: true, // enable edit buttons
                            buttonSelector: ".edit", // CSS selector for edit buttons
                            edit: function(values) {
                              $(".edit i", this)
                                .html('save')
                                .attr('title', 'Save');
                                // $("<span>Hello world ASA!</span>").insertAfter(".asd");
                              // $(".edit i", this).parent().append('Some text');
                            },
                            save: function(values) {
                              var xxx = "Are you sure to change this data?";
                              // var xxx = "if you change the invoice to enable, then the department will be charged at the meeting. <br>";
                              // xxx += "if you change the invoice to disable, then the department will not be charged at the meeting. ";
                              Swal.fire({
                              title: 'Attention !',
                              html: xxx,
                              icon: 'warning',
                              showCancelButton: true,
                              confirmButtonColor: '#3085d6',
                              reverseButtons: true,
                              cancelButtonColor: '#d33',
                              confirmButtonText: 'Yes, change it!'

                            }).then((result) => {
                              if (result.value) {
                                $(".edit i", this)
                                    .html('mode_edit')
                                    .attr('title', 'Edit');
                                    var id = $(this).data('id');
                                    values['id'] = id;
                                    values['invoice_status'] = values['invoice_status'] == "Disabled" ? 0 : 1;
                                    // console.log(values)
                                    $.post(bs+'alocation/post/update/alocation', values);
                                    Swal.fire(
                                      'Change alocation list!',
                                      'changes made successfully',
                                      'success'
                                    )
                              }else{
                                 $(".edit i", this)
                                    .html('mode_edit')
                                    .attr('title', 'Edit');
                              }
                            })
                              
                                // showNotification('alert-success', "Succes deleted access control "+name ,'top','center')
                            },
                            cancel: function(values) {
                              $(".edit i", this)
                                .html('mode_edit')
                                .attr('title', 'Edit');
                            }
                          });
                        initTable($('#tbldataAlocation'));
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function initType(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"alocation/get/data/type",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        var arInvoice = ["Disabled", "Enabled"]; 
                       gType = data.collection;
                       if(nload==0){
                            initAlocation();
                       }
                       nload++;
                       clearTable($('#tbldataType'));
                        var html = "";
                        var nn = 0;
                        $.each(data.collection, function(index, item){
                            nn++;
                            var invoice = item.invoice_status == 0 || item.invoice_status == null ? "Disabled":"Enabled";
                            html += '<tr data-id="'+item.id+'">'
                            html += '<td>';
                            html += '<a class="btn btn-info waves-effect edit" title="Edit">\
                                      <i class="material-icons">mode_edit</i>\
                                    </a>\
                                   ';
                            html += ' <button \
                                 onclick="removeDataType($(this))" \
                                 data-id="'+item.id+'" \
                                 data-name="'+item.name+'" \
                                 type="button" class="btn btn-danger waves-effect"><i class="material-icons">delete</i> </button> ';
                            html += '</td>';
                            html += '<td>'+item.id+'</td>';
                            html += '<td data-field="name">'+item.name+'</td>';
                            html += '<td  data-field="invoice_status">'+invoice+'</td>';
                            html += '<td>';
                           
                            
                            html += '</tr>';
                        })
                        $('#tbldataType tbody').html(html);

                        $('#tbldataType  tr').editable({
                            dropdowns: {
                                invoice_status: arInvoice,
                            },
                            dblclick: true,
                            button: true, // enable edit buttons
                            buttonSelector: ".edit", // CSS selector for edit buttons
                            edit: function(values) {
                              $(".edit i", this)
                                .html('save')
                                .attr('title', 'Save');
                                // select_enable();
                                // $("<span>Hello world ASA!</span>").insertAfter(".asd");
                              // $(".edit i", this).parent().append('Some text');
                            },
                            save: function(values) {
                              
                                var xxx = "Are you sure to change this data?";
                                // var xxx = "if you change the invoice to enable, then the allocation will be charged at the meeting. <br>";
                                  // xxx += "if you change the invoice to disable, then the allocation will not be charged at the meeting. ";
                                  Swal.fire({
                                  title: 'Are you sure?',
                                  html: xxx,
                                  icon: 'warning',
                                  showCancelButton: true,
                                  confirmButtonColor: '#3085d6',
                                  cancelButtonColor: '#d33',
                                  confirmButtonText: 'Yes, change it!'
                                }).then((result) => {
                                  if (result.value) {
                                    $(".edit i", this)
                                    .html('mode_edit')
                                    .attr('title', 'Edit');
                                    var id = $(this).data('id');
                                    values['id'] = id;
                                    values['invoice_status'] = values['invoice_status'] == "Disabled" ? 0 : 1;
                                    $.post(bs+'alocation/post/update/type', values);
                                        Swal.fire(
                                          'Change alocation type!',
                                          'changes made successfully',
                                          'success'
                                        )
                                  }else{
                                     $(".edit i", this)
                                        .html('mode_edit')
                                        .attr('title', 'Edit');
                                  }
                                })
                                // showNotification('alert-success', "Succes deleted access control "+name ,'top','center')
                            },
                            cancel: function(values) {
                              $(".edit i", this)
                                .html('mode_edit')
                                .attr('title', 'Edit');
                            }
                          });
                        initTable($('#tbldataType'));
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
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
        function removeDataType(t){
            var id = t.data('id');
            var name = t.data('name');
            var form = new FormData();
            form.append('id', id);
            form.append('name', name);
            Swal.fire({
                title:'Are you sure you want delete it?',
                text: "You will lose the data division "+name+" !",
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
                            url : bs+"alocation/post/delete/type",
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
                                    showNotification('alert-success', "Succes deleted alocation type "+name ,'top','center')
                                    initType();
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
        function removeDataAlocation(t){
            var id = t.data('id');
            var name = t.data('name');
            var form = new FormData();
            form.append('id', id);
            form.append('name', name);
            Swal.fire({
                title:'Are you sure you want delete it?',
                text: "You will lose the data departement "+name+" !",
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
                            url : bs+"alocation/post/delete/alocation",
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
                                    showNotification('alert-success', "Succes deleted alocation type "+name ,'top','center')
                                    initAlocation();
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