var _URL = window.URL || window.webkitURL;
        var gBuilding = JSON.parse($('#id_building_json').val());
        $(function(){
            init();
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
            $('.selectpickerr').selectpicker("refresh");
            $('.selectpickerr').selectpicker("initialize");
        }
        function enable_datetimepicker(){
            $('.timepicker').bootstrapMaterialDatePicker({
                format: 'HH:mm',
                clearButton: true,
                date: false
            });
        }

        function calcPixel(){
            if($('#id_crt_pixel').val() == ""){
                Swal.fire({
                title:'Image File!!',
                text: "Image floor must be selected",
                type: "warning"})
            }else if($('#id_crt_floor_length').val() == ""){
                Swal.fire({
                title:'Floor Length!!',
                text: "Floor Length must be more than zero",
                type: "warning"})
            }else if($('#id_crt_floor_width').val() == ""){
                Swal.fire({
                title:'Floor Width!!',
                text: "Floor Width must be more than zero",
                type: "warning"})
            }
            var w = $('#id_crt_floor_width').val() -0;
            var l = $('#id_crt_floor_length').val() -0;
            var setRealSize = 0;
            if(w > l){
                setRealSize = w;
            }else{
                setRealSize = l;
            }
            var px = $('#id_crt_pixel').val();
            var sppx = px.split("x");
            var setSize = 0;
            if( (sppx[0]-0) > (sppx[1]-0)){
                setSize = sppx[0]-0;
            }else{
                setSize = sppx[1]-0;
            }
            var callc = setRealSize/setSize;
            // console.log(callc)

            var fixnum = callc.toFixed(2);
            $('#id_crt_meter_per_px').val(fixnum)
        }
        function calcPixelEdt(){
            if($('#id_edt_pixel').val() == ""){
                Swal.fire({
                title:'Image File!!',
                text: "Image floor must be selected",
                type: "warning"})
            }else if($('#id_edt_floor_length').val() == ""){
                Swal.fire({
                title:'Floor Length!!',
                text: "Floor Length must be more than zero",
                type: "warning"})
            }else if($('#id_edt_floor_width').val() == ""){
                Swal.fire({
                title:'Floor Width!!',
                text: "Floor Width must be more than zero",
                type: "warning"})
            }
            var w = $('#id_edt_floor_width').val() -0;
            var l = $('#id_edt_floor_length').val() -0;
            var setRealSize = 0;
            if(w > l){
                setRealSize = w;
            }else{
                setRealSize = l;
            }
            var px = $('#id_edt_pixel').val();
            var sppx = px.split("x");
            var setSize = 0;
            if( (sppx[0]-0) > (sppx[1]-0)){
                setSize = sppx[0]-0;
            }else{
                setSize = sppx[1]-0;
            }
            var callc = setRealSize/setSize;
            // console.log(callc)

            var fixnum = callc.toFixed(2);
            $('#id_edt_meter_per_px').val(fixnum)
        }
        function init(){
            var bs = $('#id_baseurl').val();
            var modules = getModule();
            $.ajax({
                // url : bs+"beacon-floor/get/data",
                url : ajax.url.get_beacon_floors,
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
                            var ename = "";
                            if(item.employee_id != null){
                                ename = item.employee_name;
                            }
                            // var pathfile = bs + "assets/file/beaconfloor/"+item.image;
                            var pathfile = item.image;
                            var imggg = ""
                            if(item.image == null || item.image == ""){
                                imggg = '';
                            }else{
                                imggg = "<img src='"+pathfile+"' style='height:100px;'> ";
                            }
                            html += '<tr>'
                            html += '<td>'+nn+'</td>';
                            html += '<td style="width:150px;">'+item.name+'</td>';
                            html += '<td>'+item.building_name+'</td>';
                            html += '<td>'+imggg+'</td>';
                            html += '<td>'+item.pixel+'</td>';
                            html += '<td>'+item.floor_length+'</td>';
                            html += '<td>'+item.floor_width+'</td>';
                            html += '<td>'+item.meter_per_px+'</td>';
                         
                            html += '<td>';
                            html += '<button \
                                 onclick="updateData($(this))" \
                                 data-id="'+item.id+'" \
                                 data-id="'+item.beacon_name+'" \
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

        
        $("#id_edt_image").change(function (e) {
            var file, img;
            if ((file = this.files[0])) {
                img = new Image();
                var objectUrl = _URL.createObjectURL(file);
                $('#id_edt_image_preview').attr('src', objectUrl);
                img.onload = function () {
                    var pixel = this.width+"x"+this.height;
                    $('#id_edt_pixel').val(pixel)
                    // alert(this.width + " " + this.height);
                    _URL.revokeObjectURL(objectUrl);

                    $('#id_edt_meter_per_px').val("");
                };
                img.src = objectUrl;
            }
        });
        $("#id_crt_image").change(function (e) {
            var file, img;
            if ((file = this.files[0])) {
                img = new Image();
                var objectUrl = _URL.createObjectURL(file);
                console.log(objectUrl)
                $('#id_crt_image_preview').attr('src', objectUrl);
                img.onload = function () {
                    var pixel = this.width+"x"+this.height;
                    $('#id_crt_pixel').val(pixel)
                    // alert(this.width + " " + this.height);
                    _URL.revokeObjectURL(objectUrl);
                };
                img.src = objectUrl;
            }
        });
        function createData(){
            // initEmpNoBeacon("",$('#id_crt_beacon_employee'));
            var html = "";
            for(var x in gBuilding){
                html += "<option value='"+gBuilding[x]['id']+"' >"+gBuilding[x]['name']+"</option>";
            }
            $('#id_crt_building_id').html(html);
            $('#id_mdl_create').modal('show');
            enable_datetimepicker()
            select_enable()
        }

        function updateData(t){
            var id = t.data('id');
            var bs = $('#id_baseurl').val();
            var modules = getModule();
            $.ajax({
                // url : bs+"beacon-floor/get/data",
                url : ajax.url.get_beacon_floor_byid,
                type : "POST",
                data : {
                    id : id
                },
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        var row = data.collection;

                        $('#id_edt_floor_id').val(row.id);
                        var html = "";
                        for(var x in gBuilding){
                            var s = gBuilding[x]['id'] == row.building_id ? "selected" : "";
                            html += "<option "+s+" value='"+gBuilding[x]['id']+"' >"+gBuilding[x]['name']+"</option>";
                        }
                        // var pathfile = bs + "assets/file/beaconfloor/"+row.image;
                        var pathfile = row.image;
                        if(row.image == null || row.image == ""){
                            $('#id_edt_image_old_preview').attr('src', "");
                        }else{
                            $('#id_edt_image_old_preview').attr('src', pathfile);
                        }

                        $('#id_edt_building_id').html(html);
                        $('#id_edt_name').val(row.name);
                        $('#id_edt_pixel').val(row.pixel);
                        $('#id_edt_floor_length').val(row.floor_length);
                        $('#id_edt_floor_width').val(row.floor_width);
                        $('#id_edt_meter_per_px').val(row.meter_per_px);
                        $('#id_mdl_update').modal('show');
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        swalShowNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
            enable_datetimepicker()
            select_enable()
        }
        function removeData(t){
            var id = t.data('id');
            var name = t.data('name');
             Swal.fire({
                title:'Are you sure you want delete '+name+'?',
                text: "",
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
                            // url : bs+"beacon-floor/post/delete",
                            url : ajax.url.post_delete_beacon_floors,
                            type : "POST",
                            data : {id:id},
                            dataType: "json",
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                            },
                            success:function(data){
                                if(data.status == "success"){
                                    Swal.fire({
                                    title:'Message',
                                    text: data.msg,
                                    type: "success"})
                                    init();
                                    
                                }else{
                                    var msg = "Your session is expired, login again !!!";
                                    swalShowNotification('alert-danger', msg,'top','center')
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
        $('#frm_create').submit(function(e){
            e.preventDefault();
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
                        saveCreate()
                    }
                else{

                }
            })
        })
        $('#frm_update').submit(function(e){
            e.preventDefault();
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
                        saveUpdate()
                    }
                else{

                }
            })
        })
       
        function saveCreate(){

            var form = $('#frm_create')[0];
            var formdata = new FormData(form)
            var bs = $('#id_baseurl').val();
            $.ajax({
                // url : bs+"beacon-floor/post/create",
                url : ajax.url.post_create_beacon_floors,
                type : "POST",
                data : formdata,
                dataType: "json",
                contentType: false,
                processData: false,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        Swal.fire({
                        title:'Message',
                        text: data.msg,
                        type: "success"})
                        init();
                        $('#id_mdl_create').modal('hide');
                        $('#frm_create')[0].reset()
                        
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        swalShowNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        
        function saveUpdate(){
            var form = $('#frm_update')[0];
            var formdata = new FormData(form)
            var bs = $('#id_baseurl').val();
            $.ajax({
                // url : bs+"beacon-floor/post/update",
                url : ajax.url.post_update_beacon_floors,
                type : "POST",
                data : formdata,
                dataType: "json",
                contentType: false,
                processData: false,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        Swal.fire({
                        title:'Message',
                        text: data.msg,
                        type: "success"})
                        init();
                        $('#id_mdl_update').modal('hide');
                        $('#frm_update')[0].reset()
                        
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        swalShowNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }