        var intervalBookingCrt;
        var enabledVar = [1, 0];
        var enabledString = ["Disabled", "Enabled"];
        var booleanString = ["False", "True"];
        var gDisplayType = [
            { 'value': 'general', 'name': "General", 'description': '' },
            { 'value': 'allroom', 'name': "Display Information", 'description': '' },
            { 'value': 'receptionist', 'name': "Receptionist", 'description': '' },
        ];
        var gRoom = [];

        var path = "assets/file/display/";
        var bgpath = path + "background/";
        var signagepath = path + "signage/";
        var groom = [];
        $(function() {
            moment.locale("en");
            init();
            getRoom();
            intrTime()
        })
        function intrTime() {
            setInterval(
                function() {
                    var tm = moment().format('HH:mm');
                    $('#time1').html(tm);
                }, 500
            );
        }

        function tootipEnable(){
            $('[data-toggle="tooltip"]').tooltip({
                container: 'body'
            });

            //Popover
            $('[data-toggle="popover"]').popover();
        }
        function clickSubmit(id) {
            $('#' + id).click();

        }

        function initTable(selector) {
            selector.DataTable();
        }

        function clearTable(selector) {
            selector.DataTable().destroy();
        }

        function select_enable() {
            $('select').selectpicker("refresh");
            $('select').selectpicker("initialize");
        }

        function colorpicker() {
            $('.colorpicker').colorpicker({
                format: 'hex'
            });
        }

        function generateRoom(value = "") {
            var gehtml = "";
            $.each(gRoom, (index, item) => {
                gehtml += `<option data-subtext="${item.building_name}" value="${item.radid}" >${ item.name}</option>`;
                // if (item.background == null) {
                // }
            })
            return gehtml;
        }

        function generateRoomSelect(value = []) {

            var gehtml = "";
            $.each(gRoom, (index, item) => {
                var s = value.indexOf(item.radid) >= 0 ? 'selected' : '';
                gehtml += `<option ${s} data-subtext="${item.building_name}" value="${item.radid}" >${item.name}</option>`;
            })
            return gehtml;
        }

        function generateDisplayType(value = "") {
            var html = ``;
            for (var x in gDisplayType) {
                var s = value == gDisplayType[x].value ? 'selected' : '';
                html += `<option ${s} value='${gDisplayType[x].value}'>${gDisplayType[x].name}</option>`;
            }
            return html;
        }

        function enable_datetimepicker() {
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
                // console.log(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'))
                init(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'))
                // console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
            });

        }

        function ocDisplayType(action = '') {
            if (action == "crt") {
                var v = $('#id_crt_type').val();
                if (v == "general") {
                    $('#id_crt_single_room_area').show('fast');
                    $('#id_crt_multiple_room_area').hide('fast');
                } else if (v == "allroom" || v == "receptionist") {
                    $('#id_crt_single_room_area').hide('fast');
                    $('#id_crt_multiple_room_area').show('fast');

                }

            } else if (action == "upd") {
                var v = $('#id_upd_type').val();
                if (v == "general") {
                    $('#id_upd_single_room_area').show('fast');
                    $('#id_upd_multiple_room_area').hide('fast');
                } else if (v == "allroom" || v == "receptionist") {
                    $('#id_upd_single_room_area').hide('fast');
                    $('#id_upd_multiple_room_area').show('fast');
                }

            }
        }

        function init() {
            var bs = $('#id_baseurl').val();
            $.ajax({
                // url: bs + "display/get/data",
                url: bs + ajax.url.get_room_displays,
                type: "GET",
                dataType: "json",
                beforeSend: function() {
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success: function(data) {
                    if (data.status == "success") {
                        groom = [];
                        clearTable($('#tbldata'));
                        var col = data.collection;
                        var len = data.collection.length;
                        var html = "";
                        var numm = 0;
                        groom = col;
                        $.each(col, function(index, item) {
                            numm++;
                            var status = item.enable_signage == 1 ? "Enabled" : "Disabled";
                            var display_status = ckLastTime(item.hardware_lastsync);
                            var status_sync = statusSycn(item.status_sync - 0);
                            var enabled = item.enabled - 0;
                            var bg = "";
                            if(enabled == 0){
                                bg = "background-color:lightgrey";
                            }
                            var roomname = "";
                            var roomnametooltip = "";
                            if(item.type == "general"){
                                roomname = item.room_name;
                                roomnametooltip =  item.room_name;
                            }else{
                                var arrRoomSelect = [];
                                var arrRoomSelectToolTip = [];
                                if(item.room_select_data != null){
                                    for(var ri in item.room_select_data ){
                                        if(ri < 2){

                                            arrRoomSelect.push(`${item.room_select_data [ri].name}`);
                                        }
                                        arrRoomSelectToolTip.push(`${item.room_select_data [ri].name}`);
                                    }
                                    if(arrRoomSelectToolTip.length < 2){
                                        roomname = arrRoomSelect.join(',');
                                        roomnametooltip = arrRoomSelectToolTip.join(',');
                                    }else{
                                        roomname = arrRoomSelect.join(',') + "..." ;
                                        roomnametooltip = arrRoomSelectToolTip.join(',');
                                    }
                                }
                            }

                            html += `<tr style="${bg}" >`
                            html += '<td>' + numm + '</td>';
                            html += '<td>' + item.display_serial + '</td>';
                            html += '<td data-toggle="tooltip" data-placement="top" title="" data-original-title="'+roomnametooltip+'" >' + roomname + '</td>';
                            // html += '<td><img src="' + bs + bgpath + item.background + '"  style="width:80px;height:auto;" ></td>';
                            html += '<td><img src="' + item.background + '"  style="width:80px;height:auto;" ></td>';
                            html += '<td><div style="width:25px;height:25px;border:2px solid #000;background:' + item.color_occupied + ' "></div></td>';
                            html += '<td><div style="width:25px;height:25px;border:2px solid #000;background:' + item.color_available + ' "></div></td>';
                            // html += '<td>'+status; +'</td>';
                            html += `<td>${status_sync}</td>`;
                            html += `<td>${display_status}</td>`;

                            var enabled = item.enabled -0;
                            var iconEnabled = 'close';
                            var colorEnabled = 'btn-warning';
                            var actionEnabled = 0;
                            if(enabled == 0){
                                iconEnabled = 'done';
                                colorEnabled = 'btn-success';
                                actionEnabled = 1;
                            }

                            html += '<td>'; // 

                            html += `<button  onclick="openUpdate($(this))" 
                            data-id ="${item.id}" 
                            data-num ="${index}" 
                            type="button" 
                            class="btn btn-info waves-effect"><i class="material-icons">edit</i>
                            </button>`;
                            html += '&nbsp;'; // 
                            html += `<button  onclick="openEnabled($(this))" 
                            title="Click this for enable/disable display"
                            data-toggle="tooltip" data-placement="top"
                            data-action ="${actionEnabled}" 
                            data-id ="${item.id}" 
                            data-num ="${index}" 
                            type="button" 
                            class="btn ${colorEnabled} waves-effect"><i class="material-icons">${iconEnabled}</i>
                            </button>`;
                            html += '&nbsp;'; // 
                            html += `<button onclick="removeData($(this))" 
                            data-id ="${item.id}" 
                            data-num ="${index}" 
                            type="button" 
                            class="btn btn-danger waves-effect"><i class="material-icons">delete</i> 
                            </button>`;
                            html += '</td>'; // 

                            // html += '<td>'; // 
                            // html += '<button \
                            //         onclick="openSignage($(this))" \
                            //         data-room_id="'+item.room_id+'" \
                            //         data-num="'+index+'" \
                            //         type="button" class="btn btn-info waves-effect ">Sigange</button>';
                            // html += '</td>'; // 
                        })
                        
                        $('#id_count_total').html(len)
                        $('#tbldata tbody').html(html);
                        tootipEnable();
                        // initTable($('#tbldata'));
                    } else {
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg, 'top', 'center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            });
        }
        function statusSycn(status){
            if(status == 0){
                return "Not Synchronized";
            }else  if(status == 1){
                return "Synchronized";
            }else if(status == 2){
                 return "Update Synchronized";
            }
        }
        function ckLastTime(datetime){
            var now = moment();
            var ck = moment(datetime);
            // var diff =ck.diff(now);
            var diff =now.diff(ck);
            var durr = moment.duration(diff).asMinutes();
            if(durr > 3 ){
                return "Disconnected"
            }else{
                return "Connected"
            }
            // console.log(durr)
        }

        function openSignage(t) {
            var bs = $('#id_baseurl').val();
            var id = t.data('room_id');
            var num = t.data('num');
            var display = groom[num];
            var pathfile = bs + signagepath + display.signage_media
            $('#id_si_old').attr('src', pathfile)
            $('#id_mdl_sigange').modal('show');

        }

        
        $('#frm_create').submit(function(e) {
            e.preventDefault();
            var form = $('#frm_create').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                // url: bs + "display/post/room",
                url: bs + ajax.url.post_create_room_display,
                type: "POST",
                dataType: "json",
                data: new FormData(this),
                contentType: false,
                cache: false,
                processData: false,
                beforeSend: function() {
                    loadingg('Please wait ! ', 'Loading . . . ')
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success: function(data) {
                    Swal.close();
                    if (data.status == "success") {
                        $('#frm_create')[0].reset();
                        init();
                        $('#id_mdl_create').modal('hide');
                        swalShowNotification('alert-success', data.msg, 'top', 'center')
                    } else {
                        swalShowNotification('alert-danger', data.msg, 'top', 'center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        })
        $('#frm_update').submit(function(e) {
            e.preventDefault();
            var form = $('#frm_create').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                // url: bs + "display/post/room/update",
                url: bs + ajax.url.post_update_room_display,
                type: "POST",
                dataType: "json",
                data: new FormData(this),
                contentType: false,
                cache: false,
                processData: false,
                beforeSend: function() {
                    loadingg("Please Wait","Loading . . . ");
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success: function(data) {
                    $('#id_loader').html('');
                    Swal.close();
                    if (data.status == "success") {
                        $('#frm_update')[0].reset();
                        init();
                        $('#id_mdl_update').modal('hide');
                        swalShowNotification('alert-success', data.msg, 'top', 'center')
                    } else {
                        swalShowNotification('alert-danger', data.msg, 'top', 'center')
                    }
                },
                error: errorAjax
            })
        })
        $('#frm_signage').submit(function(e) {
            e.preventDefault();
            // var form = $('#frm_signage').serialize();
            var bs = $('#id_baseurl').val();
            $.ajax({
                url: bs + "display/post/signage",
                type: "POST",
                dataType: "json",
                data: new FormData(this),
                contentType: false,
                cache: false,
                processData: false,
                beforeSend: function() {
                    loadingg("Please Wait","Loading . . . ");
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success: function(data) {
                    $('#id_loader').html('');
                    Swal.close();
                    if (data.status == "success") {

                        $('#frm_signage')[0].reset();
                        init();
                        $('#id_mdl_sigange').modal('hide');
                        $('#id_si_old')[0].pause();
                        showNotification('alert-success', data.msg, 'top', 'center')
                    } else {
                        $('#id_si_old')[0].pause();
                        Swal.fire(
                            'Upload failed !!!',
                            data.msg,
                            'question'
                        )
                        showNotification('alert-danger', data.msg, 'top', 'center')
                    }
                   
                },
                error: errorAjax
            })
        })

        function getRoom() {
            var bs = $('#id_baseurl').val();
            $.ajax({
                // url: bs + "display/get/data-room",
                url: bs + ajax.url.get_room_room_displays,
                type: "GET",
                dataType: "json",
                beforeSend: function() {
                    // $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success: function(data) {
                    Swal.close()
                    if (data.status == "success") {
                        gRoom = data.collection;

                    } else {
                        $('#id_loader').html('');

                    }
                },
                error: errorAjax
            });
        }
        function openUpdate(t) {
            var bs = $('#id_baseurl').val();
            var id = t.data('id');
            var num = t.data('num');
            var display = groom[num];
            // console.log(display);
            $('#id_upd_room_id').val(id);
            $('#id_upd_color_occupied').val(display.color_occupied)
            $('#id_upd_color_available').val(display.color_available)
            $('#id_upd_display_serial').val(display.display_serial)
            var room_select = display.room_select == null ? "":  display.room_select;
            var sp_room_select = room_select.split(",");
            var gehtml =generateRoom(display.room_id);
            var htmlSelectRoom =generateRoomSelect(sp_room_select);
            var htmlDisplayType = generateDisplayType(display.type);
            var en = '';
            for (var x in enabledVar) {
                var vdd = enabledVar[x]
                var selc = vdd == display.enable_signage ? "selected" : "";
                en += '<option ' + selc + ' value="' + vdd + '" >' + enabledString[vdd] + '</option>';
            }
            $('#id_upd_enable_signage').html(en);
            $('#id_upd_room').html(gehtml);
            $('#id_upd_type').html(htmlDisplayType);
            $('#id_upd_select_room').html(htmlSelectRoom);
            ocDisplayType("upd");
            colorpicker();
            select_enable();
            $('#id_mdl_update').modal('show');


        }
        function openEnabled(t) {
            var bs = $('#id_baseurl').val();
            var id = t.data('id');
            var num = t.data('num');
            var action = t.data('action') - 0;
            var display = groom[num];
            var serial = display.display_serial;
            var text = "Are you sure you want disabled this display?";
            var confText = "Disabled";
            var html = ``;
            if(action == 1){
                confText = "Enabled";
                html = "<p>Are you sure you want enabled this display?<p>";
                html += `<input type="hidden" id="id_enable_text" class="swal2-input" placeholder="Disable Messages">`;
            }else if(action == 0){
                confText = "Disabled";
                html = "<p>Are you sure you want disabled this display?<p>";
                html += `<input type="text" id="id_enable_text" class="swal2-input" placeholder="Disable Messages">`;
            }
            Swal.fire({
                title: 'Confirmation',
                // text: text,
                type: "warning",
                html: html,
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: confText,
                cancelButtonText: 'Cancel !',
                reverseButtons: true
            }).then((result) => {
                var enable_text = $('#id_enable_text').val();
                if (result.value) {
                    var bs = $('#id_baseurl').val();
                    $.ajax({
                        // url: bs + "display/post/enable",
                        url: bs + ajax.url.post_change_status_room_display,
                        type: "POST",
                        data: {
                            id : id,
                            action : action,
                            disable_msg : enable_text
                        },
                        dataType: "json",
                        beforeSend: function() {
                            loadingg('Please Wait', "Loading . . .");
                            $('#id_loader').html('<div class="linePreloader"></div>');
                        },
                        success: function(data) {
                            Swal.close();
                            $('#id_loader').html('');
                            if (data.status == "success") {
                                init();
                                swalShowNotification('alert-success',`Success change display status ${serial}`, 'top', 'center')
                            } else {
                                swalShowNotification('alert-danger', "Data not found", 'bottom', 'left')
                            }
                        },
                        error: errorAjax,
                    })
                } else {

                }
            })
        }

        function createData() {
            var gehtml =generateRoom("");
            var htmlSelectRoom =generateRoom("");
            var htmlDisplayType = generateDisplayType("");
            var en = '';
            for (var x in enabledVar) {
                var vdd = enabledVar[x]
                en += '<option value="' + vdd + '" >' + enabledString[vdd] + '</option>';
            }
            $('#id_crt_enable_signage').html(en);
            $('#id_crt_room').html(gehtml);
            $('#id_crt_type').html(htmlDisplayType);
            $('#id_crt_select_room').html(htmlSelectRoom);

            select_enable();
            colorpicker();
            $('#id_mdl_create').modal('show');
        }

        function removeData(t) {
            var bs = $('#id_baseurl').val();
            var id = t.data('id');
            var num = t.data('num');
            var display = groom[num];

            var serial = display.display_serial;
            var form = {
                id:id
            }

            var text = `Are you sure you want delete this display ${serial}?`;
            Swal.fire({
                title: 'Confirmation',
                text: text,
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
                        // url: bs + "display/post/delete",
                        url: bs + ajax.url.post_delete_room_display,
                        type: "POST",
                        data: form,
                        dataType: "json",
                        beforeSend: function() {
                            loadingg('Please Wait', "Loading . . .");
                            $('#id_loader').html('<div class="linePreloader"></div>');
                        },
                        success: function(data) {
                            Swal.close();
                            $('#id_loader').html('');
                            if (data.status == "success") {
                                swalShowNotification('alert-success', "Succes deleted display " + serial, 'top', 'center')
                                init();
                            } else {
                                swalShowNotification('alert-danger', "Data not found", 'bottom', 'left')
                            }
                        },
                        error: errorAjax,
                    })
                } else {

                }
            })

        }

        function loadingg(title = "", body = "") {
            Swal.fire({
                title: title,
                html: body,
                allowOutsideClick: false,
                onBeforeOpen: () => {
                    Swal.showLoading()
                },
            });
        }

        function swalShowNotification(icon, title, loc = "", loc2 = "") {
            var ic = "";
            if (icon == "alert-success") {
                ic = "success";
            } else if (icon == "alert-danger") {
                ic = "danger";
            } else if (icon == "alert-warning") {
                ic = "warning";
            } else if (icon == "alert-info") {
                ic = "info";
            }
            Swal.fire(
                title,
                '',
                ic
            )
        }

        function errorAjax(xhr, ajaxOptions, thrownError) {
            $('#id_loader').html('');
            Swal.close();
            if (ajaxOptions == "parsererror") {
                var msg = "Status Code 500, Error Server bad parsing";
                showNotification('alert-danger', msg, 'bottom', 'left')
            } else {
                var msg = "Status Code " + xhr.status + " Please check your connection !!!";
                showNotification('alert-danger', msg, 'bottom', 'left')
            }
        }

        function setButtonWavesEffect(event) {
            $(event.currentTarget).find('[role="menu"] li a').removeClass('waves-effect');
            $(event.currentTarget).find('[role="menu"] li:not(.disabled) a').addClass('waves-effect');
        }