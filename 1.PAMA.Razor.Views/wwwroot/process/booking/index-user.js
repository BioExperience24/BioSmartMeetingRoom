        var generalSettingval = $('#id_settinggeneral').val()
        var generalSetting = JSON.parse(generalSettingval);
        var gCheckedDate = false;
        var selectRoom = {};
        var gRoomAvailable = [];
        var gAutomation = [];
        var gInternalParticipant = [];
        var gExternalParticipant = [];
        var gDateBooking = "";
        var gTimeSelectBooking = [];
        var gTimeSelectBookingRes = [];
        var gEmployees = [];
        var gEmployeesSelected = [];
        var gEmployeesSelectedArray = []
        var gPartisipantManual = []
        var gAlocationPIC = []
        var gEmployeePIC = "";
        var gAlocation = [];
        var gPantryConfig = {};
        var gPantryDetail = [];

        var modules = JSON.parse($('#id_modules').val());

        var timeDurationArray = ["30 MIN", "60 MIN", "90 MIN", "120 MIN"];
        var path_room_image = "file/room/";
        var gBookingCrt = {}; 
        gBookingCrt['change'] = false;
        var isUserPIC = false;
        var intervalBookingCrt;
        var yourAlocationData;
        $(function(){
            moment.locale("en"); 
            initTable($('#tblPIC'));
            init();
            inituser();
            getAlocation();
            getEmployee();
            enable_datetimepicker();
            getCrtBookingHTML();
            checkIsPIC();
            getPantry();
        })

        function swalShort(title,desc,type="warning" ){
            Swal.fire({
              title: title,
              text: desc,
              type: type,
              showCloseButton: false,
              focusConfirm: false,
              confirmButtonText:
                'Close',
              confirmButtonAriaLabel: 'Close',
            });
        }
        
        function genegrateIntervalCrtBooking(status=""){
            var timestart = gBookingCrt.timestart['time']['time_array'];
                    var timeend = gBookingCrt.timeend['time']['time_array'];
                    var gdate = gBookingCrt.date == "today" ?  moment().format("YYYY-MM-DD") :  gBookingCrt.date  ;

                    var timeunixstart = moment(gdate + " " +timestart).format("X");
                    var timeunixend = moment(gdate + " " +timeend).format("X");
                    if(timeunixstart >  timeunixend){
                        $('#id_btn_crt_submit_booking').attr('disabled', true);
                        swalShort("Attention", "Finish time must to more thand start time")
                        return false;
                    }else{
                        if($('#id_frm_crt_title').val() == "" || $('#id_frm_crt_title').val() ==null ){
                            $('#id_frm_crt_title')[0].focus();
                            swalShort("Attention", "Subject cannot be empty")
                            return false;
                        }else if($('#id_frm_crt_pic').val() == null || $('#id_frm_crt_pic').val() == ""){
                            $('#id_frm_crt_title')[0].focus();
                            swalShort("Attention", "You must choose PIC ")
                            return false;
                        }else if($('#id_frm_crt_alocation').val() == "" || $('#id_frm_crt_alocation').val() == null){
                            
                            swalShort("Attention", "You must choose Department ")
                            return false;
                        }else if(gEmployeesSelected.length == 0 && gPartisipantManual.length == 0 ){
                            $('#id_frm_crt_title')[0].focus();
                            swalShort("Attention", "You must selected the participant ")
                            return false;
                        }else{
                            return true;
                        }
                    }
        }
        function openParticipant(){
            $('#id_mdl_in_participant').modal("show");  
        }
        function openPIC(){
            $('#id_mdl_pic').modal("show");  
        }
        function choosePIC(t){
            var name = t.data('name');
            var id = t.data('id');
            console.log(id, name)
            $('#id_book_pic_input').val(name);
            $('#id_book_pic_id_input').val(id);
             $('#id_mdl_pic').modal("hide");  
        }
        function incrementParticipant(){
            var val = $('#id_book_internal').val();
            if(val != ""){
                var arm = {
                    id : val,
                    name : $('#id_book_internal :selected').text()
                }
                var gdt = gInternalParticipant;
                var existData = 0;
                $.each(gdt, (index, item) =>{
                    if(item.id == val){
                        existData = 1;
                    }
                })
                if(existData == 0){
                    gInternalParticipant.push(arm);
                    var html = '<tr id="id_row_tbl_in_parti_'+val+'">';
                    html += '<td>'+$('#id_book_internal :selected').text()+'</td>';
                    html += '<td><button data-id="'+val+'" onclick="decrementParticipant($(this))" type="button" class="btn btn-danger waves-effect"><i class="material-icons">remove</i></button></td>';
                    html += '</tr>'
                    $('#tblInParti tbody').append(html)
                    $('#id_book_internal_input').val(gInternalParticipant.length + " Particaipants")
                }
            }
        }
        function checkIsPIC(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                url :  bs+"booking/check/user-pic",
                type : "GET",
                dataType: "json",
                beforeSend: function(){

                },
                success:function(data){
                    if(data.status == "success"){
                        if(data.collection.length > 0){
                            yourAlocationData=data.collection;
                            isUserPIC = true;

                        }
                    }
                },
                error: errorAjax
            })
        }
        function decrementParticipant(t){
            var id = t.data('id');
            var row = $('#id_row_tbl_in_parti_'+id);
            var gdt = gInternalParticipant;
            var num;
            $.each(gdt, (index, item) =>{
                if(item.id == id){
                    num = index;
                }
            })
            row.remove();
            gInternalParticipant.splice(num,1);
            $('#id_book_internal_input').val(gInternalParticipant.length + " Particaipants")

        }
        function decrementExParticipant(t){
            var num = t.data('num');
            var row = $('#id_row_tbl_ex_parti_'+num);
            row.remove();
            gExternalParticipant.splice(num,1);
        }

        
        function klikRoom(index, target){
            if(target.hasClass('selectedRoom')){
                $(target).removeClass('selectedRoom');
                selectRoom = {};
                // showNotification('alert-danger', "Unselected Room",'top','center')
            }else{
                $('*').removeClass('selectedRoom');
                $(target).addClass('selectedRoom');
                selectRoom = gRoomAvailable[index];
                // showNotification('alert-info', "Selected Room",'top','center')
            }
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
              // console.log(start.format('YYYY-MM-DD'),end.format('YYYY-MM-DD'))
              init(start.format('YYYY-MM-DD'),end.format('YYYY-MM-DD'))
              // console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
            });
            $('.input-group #daterangepicker_other').daterangepicker({
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
              inituser(start.format('YYYY-MM-DD'),end.format('YYYY-MM-DD'))
              // console.log('New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')');
            });
            
        }
        function getEmployee(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                url :  bs+"employee/get/data",
                type : "GET",
                dataType: "json",
                beforeSend: function(){

                },
                success:function(data){
                    if(data.status == "success"){
                        gEmployees = data.collection;
                    }
                },
                error: errorAjax
            })
        }
        function getAlocation(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                url :  bs+"alocation/get/data/alocation",
                type : "GET",
                dataType: "json",
                beforeSend: function(){

                },
                success:function(data){
                    if(data.status == "success"){
                        gAlocation = data.collection;
                    }
                    // gBookingCrt['change'] = ;
                    
                },
                error: errorAjax
            })
        }
        function getCrtBookingHTML(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"booking/get/booking-create/page1",
                type : "GET",
                beforeSend: function(){

                },
                success:function(data){
                    $('#booknowpage').html(data)
                    // gBookingCrt['change'] = ;
                    if(gBookingCrt['change'] == true){
                        var tm = gBookingCrt['date'];
                        var pick = tm == "today" ? "" : "pick"; 
                        chooseTimeCrt(tm, pick);
                    }else{
                        chooseTimeCrt();
                    }
                    
                },
                error: errorAjax
            })
        }

        function init(date1 = "", date2 = ""){
            var bs = $('#id_baseurl').val();
            if(date1 == "" || date2 == ""){
                date1 = moment().subtract(6, 'days').format('YYYY-MM-DD');
                date2 = moment().format('YYYY-MM-DD')
            }
            $.ajax({
                url : bs+"booking/get/data/start/"+date1+"/end/"+date2,
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
                        $.each(data.collection, function(index, item){
                            numm++;
                            var status = "";
                            var bookRun = false;
                            var bookExpr = false;
                            if(moment().unix() > moment(item.end).unix()){
                                status = "<b style='color:red;'>Expired</b>"
                            } else if(item.end_early_meeting == 1){
                                bookRun = true;
                                status = "<b style='color:light-green;'>Meeting End Early</b>"; 
                            }else if(
                                    moment().unix() <= moment(item.end).unix() &&  
                                    moment().unix() >= moment(item.start).unix() 
                                ){
                                bookRun = true
                                status = "<b style='color:light-green;'>Meeting in progress</b>"; // meeting dimulai
                            }else if(
                                moment().unix() <= moment(item.date+" "+item.start).unix() 
                            ){
                                status = "<b style='color:blue;'>Meeting queue</b>"; // antrian
                            }
                            if(item.is_rescheduled == 1){
                                status = "<b style='color:blue;'>Meeting queue</b>"; // antrian
                            }
                            if(item.is_canceled == 1){
                                status = "<b style='color:red;'>Meeting Canceled</b>"; // antrian
                            }
                            if(item.is_expired == 1){
                                bookExpr = true;
                                status = "<b style='color:red;'>Meeting Expired</b>"; // antrian
                            }
                            var $start = moment(item.start).format("hh:mm A");
                            var $end = moment(item.end).format("hh:mm A");
                            html += '<tr data-id="'+item.booking_id+'">'
                            html += '<td>'+numm+'</td>';
                            html += '<td>'+item.no_order+'</td>';
                            // html += '<td>'+item.booking_id+'</td>';
                            html += '<td data-field="name">'+item.title+'</td>';
                            html += '<td>'+item.room_name+'</td>';
                            html += '<td>'+moment(item.date).format("DD MMMM YYYY"); +'</td>';
                            html += '<td>'+$start +"-"+ $end  +'</td>';
                            html += '<td>'+status; +'</td>';
                            html += '<td>'; // 
                            html += '<button \
                                    onclick="openPartispant($(this))" \
                                    data-booking_id="'+item.booking_id+'" \
                                    data-title="'+item.title+'" \
                                    type="button" class="btn btn-info waves-effect ">Participant/Audience</button>';
                            html += '</td>'; // 
                            html += '<td><a onclick="clickPIC($(this))" data-id="'+item.booking_id+'" style="cursor:pointer;font-size:16px;font-weight:bold;" >'+item.pic+'</a></td>';
                            var url_download = bs+"api/" + 'schedule/report-meeting/'+item.booking_id;
                            html += '<td><a class="btn btn-info waves-effect " target="__blank" href="'+url_download+'"style="cursor:pointer;font-size:16px;font-weight:bold;" >Download</a></td>';
                            html += '<td>';
                            if(item.is_expired == 0 ){
                                if(item.is_canceled == 0 ){
                                    html += '<button class="btn btn-info waves-effect"  onclick="clickAttendance($(this))" data-title="'+item.title+'"  data-id="'+item.booking_id+'" style="cursor:pointer;font-weight:bold;" >Attendance Action</button>';
                                }

                            }else{

                            }
                            html += '</td>';
                            html += '<td>';
                            if(item.is_expired == 0 && moment().unix() < moment(item.end).unix()){
                                if(item.is_canceled == 0 ){
                                    if(bookRun){
                                        if(item.end_early_meeting == 0 && item.is_pic == "1"){
                                             html += '<button \
                                             onclick="extendMeeting($(this))" \
                                             data-id="'+item.id+'" \
                                             data-booking_id="'+item.booking_id+'" \
                                             data-name="'+item.title+'" \
                                             type="button" class="btn btn-info waves-effect ">Extend Meeting</button>';
                                             html += '<button \
                                             onclick="endMeeting($(this))" \
                                             data-id="'+item.id+'" \
                                             data-booking_id="'+item.booking_id+'" \
                                             data-name="'+item.title+'" \
                                             type="button" class="btn btn-danger waves-effect ">End Meeting</button>';
                                        }else{

                                        }
                                    }else{
                                        if(item.is_pic == "1"){
                                            html += '<button \
                                            onclick="rescheduleMeeting($(this))" \
                                            data-booking_id="'+item.booking_id+'" \
                                            data-date="'+item.date+'" \
                                            data-start="'+item.start+'" \
                                            data-end="'+item.end+'" \
                                            data-name="'+item.title+'" \
                                            data-room_name="'+item.room_name+'" \
                                            data-room_id="'+item.room_id+'" \
                                            type="button" class="btn btn-info waves-effect ">Reschedule</button>';
                                            html += '<button \
                                                 onclick="cancelMeeting($(this))" \
                                                 data-id="'+item.id+'" \
                                                 data-booking_id="'+item.booking_id+'" \
                                                 data-name="'+item.title+'" \
                                                 type="button" class="btn btn-warning waves-effect ">Cancel</button>';
                                        }
                                    
                                
                                    }
                                    
                                }else{
                              
                                }
                               
                            } // is expired
                            
                            
                            html += '</td>';
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
            });
        }
        function clickAttendance(t){
            var title = t.data('title');
            var booking_id = t.data('id');
            // console.log(title, booking_id)
            $('#id_att_booking_id').val(booking_id);
            $('#id_att_text_name').html(title)
            $('#id_mdl_attendance').modal('show')

        }
        function clickAttendanceStatus(data){
            var bs = $('#id_baseurl').val();
            var booking_id = $('#id_att_booking_id').val();
            var reason = $('#id_att_reason').val();
            // console.log(title, booking_id)
            var txt =( data == "attend") ? "attend" : "no attend";
            var form = {
                booking_id : booking_id,
                reason : reason,
                status : data
            }
            Swal.fire({
                title:'Are you sure to '+txt+' for this meeting?',
                text: "You will change status of attendance of meeting !",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Confirm!',
                cancelButtonText: 'Close !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                        $.ajax({
                            url : bs+"booking/attendance/meeting",
                            type : "POST",
                            dataType: "json",
                            data : form,
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                            },
                            success:function(res){
                                $('#id_loader').html('');
                                if(res.status == "success"){
                                   Swal.fire({
                                        title:'Message?',
                                        text: "You change the attendance status !",
                                        type: "warning",
                                        showCancelButton: false,
                                        allowOutsideClick: false,
                                        confirmButtonColor: '#3085d6',
                                        cancelButtonColor: '#d33',
                                        confirmButtonText: 'Ok',
                                        cancelButtonText: 'Close !',
                                        reverseButtons: true
                                        }).then((result) => {
                                            if (result.value) {
                                               init() 
                                                $('#id_mdl_attendance').modal('hide')
                                            }
                                        else{
                                            Swal.fire('Attention !!!','Try again to other time!','warning')
                                        }
                                    }) 
                                }else{

                                }
                                
                            },
                            error: errorAjax
                        });
                    }
                else{

                }
            })

        }
        function inituser(date1 = "", date2 = ""){
            var bs = $('#id_baseurl').val();
            if(date1 == "" || date2 == ""){
                date1 = moment().subtract(6, 'days').format('YYYY-MM-DD');
                date2 = moment().format('YYYY-MM-DD')
            }
            $.ajax({
                url : bs+"booking/get/data/other/start/"+date1+"/end/"+date2,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        clearTable($('#tbldata_other'));
                        var col = data.collection;
                        var html = "";
                        var numm = 0;
                        $.each(data.collection, function(index, item){
                            numm++;
                            var status = "";
                            var bookRun = false;
                            var bookExpr = false;
                            if(moment().unix() > moment(item.end).unix()){
                                status = "<b style='color:red;'>Expired</b>"
                            }else if(
                                    moment().unix() <= moment(item.end).unix() &&  
                                    moment().unix() >= moment(item.start).unix() 
                                ){
                                bookRun = true
                                status = "<b style='color:light-green;'>Meeting in progress</b>"; // meeting dimulai
                            }else if(
                                moment().unix() <= moment(item.date+" "+item.start).unix() 
                            ){
                                status = "<b style='color:blue;'>Meeting queue</b>"; // antrian
                            }
                            if(item.is_rescheduled == 1){
                                status = "<b style='color:blue;'>Meeting queue</b>"; // antrian
                            }
                            if(item.is_canceled == 1){
                                status = "<b style='color:red;'>Meeting Canceled</b>"; // antrian
                            }
                            if(item.is_expired == 1){
                                bookExpr = true;
                                status = "<b style='color:red;'>Meeting Expired</b>"; // antrian
                            }
                            var $start = moment(item.start).format("HH:mm");
                            var $end = moment(item.end).format("HH:mm");
                            html += '<tr data-id="'+item.booking_id+'">'
                            html += '<td>'+numm+'</td>';
                            html += '<td>'+item.booking_id+'</td>';
                            html += '<td data-field="name">'+item.title+'</td>';
                            html += '<td>'+item.room_name+'</td>';
                            html += '<td>'+moment(item.date).format("DD MMMM YYYY"); +'</td>';
                            html += '<td>'+$start +"-"+ $end  +'</td>';
                            html += '<td>'+status; +'</td>';
                            html += '<td>'; // 
                            html += '<button \
                                    onclick="openPartispant($(this))" \
                                    data-booking_id="'+item.booking_id+'" \
                                    data-title="'+item.title+'" \
                                    type="button" class="btn btn-info waves-effect ">Participant/Audience</button>';
                            html += '</td>'; // 
                            html += '<td><a onclick="clickPIC($(this))" data-id="'+item.booking_id+'" style="cursor:pointer;font-size:16px;font-weight:bold;" >'+item.pic+'</a></td>';
                            
                            html += '</tr>';
                        })
                        $('#tbldata_other tbody').html(html);
                        initTable($('#tbldata_other'));
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            });
        }
        function cancelMeeting(t){
            var id = t.data('id');
            var booking_id = t.data('booking_id');
            var name = t.data('name');
            var form = new FormData();
            form.append('id', id);
            form.append('booking_id', booking_id);
            form.append('name', name);
            Swal.fire({
                title:'Are you sure want cancel '+name+' of meeting?',
                text: "You will cancel the data booking "+name+" !",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Cancel Meeting !',
                cancelButtonText: 'Close !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                        var bs = $('#id_baseurl').val();
                        $.ajax({
                            url : bs+"booking/post/cancelbook",
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
                                    showNotification('alert-success', "Succes cancel "+name ,'top','center')
                                    init();
                                }else{
                                    showNotification('alert-danger', "Cancel "+name+" meeting is failed!!!",'bottom','left')
                                }
                            },
                            error: errorAjax,
                        })
                    }
                else{

                }
            })
        }

        // 
        function extendMeeting(t){
            var date = t.data('date');
            var booking_id = t.data('booking_id');
            var room_name = t.data('room_name');
            var room_id = t.data('room_id');
            var start = t.data('start');
            var end = t.data('end');
            var name = t.data('name');
            var bs = $('#id_baseurl').val();
            $.ajax({
                url :  bs+"booking/get/extend-meeting",
                type : "GET",
                data : {
                    booking_id : booking_id,
                    time:moment().format("HH:mm:ss"),
                    date:moment().format('YYYY-MM-DD'),
                },
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                    Swal.fire({
                         title: 'Please Wait !',
                         html: 'Process to cancel meeting',
                         allowOutsideClick: false,
                        onBeforeOpen: () => {
                            Swal.showLoading()
                        },
                    });

                },
                success:function(data){
                    Swal.close();
                    $('#id_loader').html('');
                    if(data.status == "success"){
                        var dt = data.collection;
                        var html             = "";
                        html +=  '<table class="table table-hover select" >';
                        html +=  '<tbody>';
                        $.each(dt, (index, item)=>{
                            if (item.book == "0" || item.book == 0) {
                                html +=  '<tr data-name="'+name+'" data-booking_id="'+booking_id+'" data-index="'+index+'"  data-duration="'+item.duration+'"   onclick="setExtendTimeBooking($(this))" style="cursor:pointer;" ><td>'+item.duration+' mins</td></tr>';
                            }
                        });
                        html +=  '</tbody>';
                        html +=  '</table>';
                        Swal.fire({
                          title: 'Extend Time of '+name,
                          // icon: 'info',
                          html: html,
                          showCloseButton: true,
                          focusConfirm: false,
                          confirmButtonText:
                            'Close',
                          confirmButtonAriaLabel: 'Close',
                        })
                        // gAlocation = data.collection;
                    }
                    // gBookingCrt['change'] = ;
                    
                },
                error: errorAjax
            })
        }
        function setExtendTimeBooking(t){
            var booking_id = t.data('booking_id');
            var duration = t.data('duration');
            var index = t.data('index');
            var name = t.data('name');
            var form = new FormData();
            form.append('booking_id', booking_id);
            form.append('index', index);
            form.append('extend', duration);
            form.append('name', name);
            Swal.fire({
                title:'Are you sure want extend '+name+' of meeting?',
                text: "You will extend the data booking "+name+" !",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Extend Meeting !',
                cancelButtonText: 'Close !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                        var bs = $('#id_baseurl').val();
                        $.ajax({
                            url : bs+"booking/post/extend-book",
                            type: "POST",
                            data : form,
                            processData: false,
                            contentType: false,
                            dataType :"json",
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                                Swal.fire({
                                    title: 'Please Wait !',
                                    html: 'Process to extend meeting',
                                    allowOutsideClick: false,
                                    onBeforeOpen: () => {
                                        Swal.showLoading()
                                    },
                                });
                            },
                            success:function(data){
                                Swal.close();
                                $('#id_loader').html('');
                                if (data.status == "success") {
                                    showNotification('alert-success', "Succes extend "+name ,'top','center')
                                    init();
                                }else{
                                    showNotification('alert-danger', "Extend "+name+" meeting is failed!!!",'bottom','left')
                                }
                            },
                            error: errorAjax,
                        })
                    }
                else{

                }
            })
        }


        function endMeeting(t){
            var id = t.data('id');
            var booking_id = t.data('booking_id');
            var name = t.data('name');
            var form = new FormData();
            form.append('id', id);
            form.append('booking_id', booking_id);
            form.append('name', name);
            form.append('user', true);
            Swal.fire({
                title:'Are you sure want end '+name+' of meeting?',
                text: "You will end the data booking "+name+" !",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'End Meeting !',
                cancelButtonText: 'Close !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                        var bs = $('#id_baseurl').val();
                        $.ajax({
                            url : bs+"booking/post/endbook",
                            type: "POST",
                            data : form,
                            processData: false,
                            contentType: false,
                            dataType :"json",
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                                Swal.fire({
                                    title: 'Please Wait !',
                                    html: 'Process to end meeting',
                                    allowOutsideClick: false,
                                    onBeforeOpen: () => {
                                        Swal.showLoading()
                                    },
                                });
                            },
                            success:function(data){
                                Swal.close();
                                $('#id_loader').html('');
                                if (data.status == "success") {
                                    showNotification('alert-success', "Succes end "+name ,'top','center')
                                    init();
                                }else{
                                    showNotification('alert-danger', "End "+name+" meeting is failed!!!",'bottom','left')
                                }
                            },
                            error: errorAjax,
                        })
                    }
                else{

                }
            })
        }
        // 
        function rescheduleMeeting(t){
            var date = t.data('date');
            var booking_id = t.data('booking_id');
            var room_name = t.data('room_name');
            var room_id = t.data('room_id');
            var start = t.data('start');
            var end = t.data('end');
            var name = t.data('name');
            $('#id_frm_res_timezone').val(moment.tz.guess(true));
            $('#id_frm_res_booking_id').val(booking_id);
            $('#id_frm_res_start_input').val(start)
            $('#id_frm_res_end_input').val(end)
            var display_button_start  = moment(start).format("hh:mm A");  
            var display_button_end  = moment(end).format("hh:mm A");  
            var startDate = new Date(date)
            $('#id_frm_res_date').val(date);
            $('#id_frm_res_date_dummy').val(moment(startDate).format(' D MMMM YYYY'));
            $('#id_res_text_name').html(name);
            $('#id_frm_res_start').html(display_button_start);
            $('#id_frm_res_finish').html(display_button_end);
            activeResDatepicker(booking_id,room_id)
            checkBookingDateRes(booking_id,date,room_id, "");
            $('#id_mdl_reschedule').modal('show');
        }
        function activeResDatepicker(booking_id,room_id){
            var endDate = moment().add(7, 'days');
            var startDate = moment().add(0, 'days');
            $('#id_frm_res_date_dummy').datepicker({ 
                 autoclose: true,
                todayHighlight: true,
                toggleActive: true,
                startDate   : new Date(startDate),
                endDate     : new Date(endDate),
                format      : "dd MM yyyy",
            }).on('changeDate', function (e) {
                var tm =  moment(e.date).format('YYYY-MM-DD');
                var dates =  moment(e.date).format(' D MMMM YYYY');
                var date = tm;
                $('#id_frm_res_date_dummy').val(dates);
                $('#id_frm_res_date').val(tm);
                checkBookingDateRes(booking_id,date,room_id, "changed");
            })
        }
        function checkBookingDateRes(bookid="", date="", radid="", status=""){
            var bs = $('#id_baseurl').val();
            var url = "booking/check/res-date/booking/"+bookid+"/"+date+"/"+radid;
            $.ajax({
                url :url,
                type : "GET",
                dataType: "json",
                beforeSend: function(){

                },
                success:function(data){
                    if(data.status == "success"){
                      var col = data.collection;
                      gTimeSelectBookingRes = col;
                      var html = "";

                      var datatime = reStructureSchedule(col);
                      $.each(col, (index, item) => {   
                        var today_name = moment().format("dddd");
                        var room_work_day = item.work_day;
                        today_name = today_name.toUpperCase();
                        var ifDayExist = room_work_day.indexOf("today_name");
                        var image_room = "";
                        var d1 = moment().format('YYYY-MM-DD'); //today
                        var d2 = moment(date).format('YYYY-MM-DD'); //pick date
                        var pick = date;
                        if(d1 == d2){
                            pick = "today";
                        }

                        var check_available =  checkNowTimeRoomRes(datatime, item.work_start, item.work_end, pick)
                        if(check_available == null){
                            return true;
                        } 
                        if(status == ""){ // no change date
                            var ssSTART =  $('#id_frm_res_start_input').val()
                            var ssEND = $('#id_frm_res_end_input').val()
                            var display_button_start  = moment(ssSTART).format("hh:mm A");  
                            var display_button_end  = moment(ssEND).format("hh:mm A");  
                            $('#id_frm_res_start').html(display_button_start);
                            $('#id_frm_res_finish').html(display_button_end);
                        }else{
                            var time_available_start  = item.datatime[check_available]['time_array'];
                            var time_available_end    = item.datatime[check_available+1]['time_array'];
                            // var date                  = moment().format('YYYY-MM-DD');
                            var display_button_start  = moment(date + " "+time_available_start).format("hh:mm A");  
                            var display_button_end    = moment(date + " "+time_available_end).format("hh:mm A"); 
                            // $('#id_frm_res_start_input').val(date + " "+time_available_start);
                            // $('#id_frm_res_end_input').val(date + " "+time_available_end);
                            // $('#id_frm_res_start').html(display_button_start);
                            // $('#id_frm_res_finish').html(display_button_end);
                            $('#id_frm_res_start_input').val("");
                            $('#id_frm_res_end_input').val("");
                            $('#id_frm_res_start').html(" - ");
                            $('#id_frm_res_finish').html(" - ");
                        }
                      })
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                },
                error: errorAjax
            })
        }
        function reStructureSchedule(colll){
            if(colll.length <= 0){
                return null;
            }
            var getDatatime = colll[0]['datatime'];
            for(var x in getDatatime){
                var numIndex = x;
                for(var rr in colll){
                    var tmp_data = colll[rr]['datatime'][numIndex];
                    console.log(tmp_data);
                    if(tmp_data.book == 1 || tmp_data.book == "1"){
                        getDatatime[numIndex]['book'] = "1";
                    }
                }

            }
            return getDatatime;
        }
        function checkNowTimeRoomRes(timerange, start, end, pick = ""){
            var date        = moment().format('YYYY-MM-DD');
            var now         = moment().format('HH:mm');
            var unixStart   = moment(date + " " +start).format('X');
            var unixEnd     = moment(date + " " +end).format('X');
            var timeactive  = null;
            var unixNow     =  moment(date + " "+ now).format('X');
            $.each(timerange, (index, item) =>{
                var completeTime = date + " "+item.time_array;
                var unixTime = moment(completeTime).format('X');
                if(item.book == 0){
                    // cek waktu belum terbooking
                    if(unixStart <= unixTime && unixEnd >  unixTime ){
                        // cek bahwa waktu diantara jam buka dan tutup
                        if(unixTime > unixNow || pick != "today"){
                            // cek bahwa waktu ini lewat
                            if(timerange[index+1]['book'] == 0  ){
                                // 30 or 60 MIN
                                timeactive = index;
                                return false;
                            }else{
                                
                            }
                        }

                    }
                }
            });
            return timeactive;
        }
        function clickPIC(t){
            var id = t.data('id');
            var bs = $('#id_baseurl').val();
            var html = '<div class="row clearfix">\
                                <div class="col-xs-12 align-left">\
                                    <button type="button" class="btn btn-primary waves-effect " >SAVE</button>\
                                </div>\
                            </div>';
             $.ajax({
                url : bs+"booking/get/data/pic/"+id,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                },
                success:function(data){
                    if(data['status'] == "success"){
                        var item = data.collection;
                        var html = '';
                        var npo= (item.no_phone == null || item.no_phone == undefined  || item.no_phone == "null" ) ? "-":item.no_phone;
                        var ext = (item.no_ext == null || item.no_ext == undefined || item.no_ext == "null") ? "-"  : item.no_ext;
                        html += '<div class="row clearfix">\
                                    <div class="col-xs-12 align-left">\
                                        <b style="font-size:18px;">Name</b>\
                                    </div>\
                                </div>';
                        html += '<div class="row clearfix">\
                                    <div class="col-xs-12 align-left">\
                                        <b  style="font-size:20px;">'+item.name+'</b>\
                                    </div>\
                                </div>';
                        html += '<br>';
                        html += '<div class="row clearfix">\
                                    <div class="col-xs-12 align-left">\
                                        <b  style="font-size:18px;">Email</b>\
                                    </div>\
                                </div>';
                        html += '<div class="row clearfix">\
                                    <div class="col-xs-12 align-left">\
                                        <b  style="font-size:20px;">'+item.email+'</b>\
                                    </div>\
                                </div>';
                        html += '<br>';
                        html += '<div class="row clearfix">\
                                    <div class="col-xs-12 align-left">\
                                        <b  style="font-size:18px;">No.Phone </b>\
                                    </div>\
                                </div>';
                        html += '<div class="row clearfix">\
                                    <div class="col-xs-12 align-left">\
                                        <b  style="font-size:20px;">'+npo+'</b>\
                                    </div>\
                                </div>';
                        html += '<br>';
                        html += '<div class="row clearfix">\
                                    <div class="col-xs-12 align-left">\
                                        <b  style="font-size:18px;">No.Extension </b>\
                                    </div>\
                                </div>'
                        html += '<div class="row clearfix">\
                                    <div class="col-xs-12 align-left">\
                                        <b  style="font-size:20px;">'+ext+'</b>\
                                    </div>\
                                </div>';
                        setTimeout(function(){
                            Swal.fire({
                              title: 'PIC information',
                              type: "info",
                              // 
                              html: html,
                              showCloseButton: true,
                              focusConfirm: false,
                              confirmButtonText:
                                'C L O S E',
                              confirmButtonAriaLabel: 'C L O S E',
                            })
                        },0)
                    }
                    

                }
            })
            
        }
        function getRoomByID(room_id){
            var data = null;
            for(var x in gTimeSelectBooking){
                if(gTimeSelectBooking[x].radid == room_id){
                    data = gTimeSelectBooking[x];
                    break;
                }
            }
            return data;
        }
        function insertTheMergeRoom(){
            for(var x in gTimeSelectBooking){
                var row = gTimeSelectBooking[x];
                if(gTimeSelectBooking[x].type_room == "merge"){
                    var mm = gTimeSelectBooking[x].merge_room;
                    if(mm != null){
                        for(var i in mm){
                            var gettimeArray = getRoomByID(mm[i].radid);
                            if(gettimeArray != null){
                                gTimeSelectBooking[x]['merge_room'][i]['datatime'] = gettimeArray['datatime']
                            }
                        }  
                    }
                }

            }
        }
        function checkBookingDate(text){
            var bs = $('#id_baseurl').val();
            gDateBooking = text;
            var url = text == "today" ? bs+"booking/check/today/booking" : bs+"booking/check/pick-date/booking/"+text ;
            gBookingCrt['date'] = text;
            $.ajax({
                url :url,
                type : "GET",
                dataType: "json",
                beforeSend: function(){

                },
                success:function(data){
                    if(data.status == "success"){
                      var col = data.collection;
                      var html = "";
                      gTimeSelectBooking = col;
                      insertTheMergeRoom();
                      $.each(col, (index, item) => {   
                        var today_name = moment().format("dddd");
                        var room_work_day = item.work_day;
                        today_name = today_name.toUpperCase();
                        var ifDayExist = room_work_day.indexOf("today_name");
                        var image_room = "";
                        var check_available =  checkNowTimeRoom(item.datatime, item.work_start, item.work_end, text)
                        // if(text != "today"){
                        //     // conosole.log(123)
                        // }else{
                        // }
                        if(item.image == "" || item.image == null ){
                            image_room = 'http://placehold.it/500x300';
                        }else{
                            image_room = bs + path_room_image + item.image;
                        }
                        if(check_available == null){
                            console.log("skip room");
                            return true;
                        }
                        var time_available = item.datatime[check_available]['time_array'];
                        var date            = moment().format('YYYY-MM-DD');
                        var display_button  = moment(date + " "+time_available).format("hh:mm A");  
                        var facility_room_ar = item.facility_room.split(",");
                        var htmlfac = '<ol>';
                        for(var x in facility_room_ar){
                             htmlfac += '<li>';
                             htmlfac += facility_room_ar[x];
                             htmlfac += '</li>';
                        }
                        htmlfac += '</ol>';
                        html += '<div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">\
                            <div class="thumbnail">\
                                <img style="height:300px;" class="img-booking" src="'+image_room+'" >\
                                <div class="caption">\
                                    <div class="row clearfix">\
                                        <div class="col-sm-6 col-md-6">\
                                            <h3>'+item.name+'</h3>\
                                        </div>\
                                        <div class="col-sm-6 col-md-6 align-right">\
                                            <h3>'+item.capacity+' Persons</h3>\
                                        </div>\
                                    </div>\
                                    <div class="row clearfix">\
                                        <div class="col-sm-12 col-md-12">\
                                            <p>\
                                                '+htmlfac+' \
                                            </p>\
                                        </div>\
                                    </div>\
                                    <div class="row clearfix">\
                                        <div class="col-xs-2"></div>\
                                        <div class="col-xs-8">\
                                            <button data-text="'+text+'"  data-roomnum="'+index+'" onclick="openAlertPilih($(this))" type="button" class="btn btn-lg btn-block bg-red waves-effect " aria-haspopup="true" aria-expanded="false">\
                                                <b id="id_display_button_'+index+'" > '+display_button+' </b> <span class="caret"> &nbsp;</span>\
                                            </button>\
                                        </div>\
                                        <div class="col-xs-2"></div>\
                                    </div>\
                                    <div class="row" id="id_area_duration_'+index+'">';
                        var _duration = generalSetting['duration']-0;
                        var _maxduration = generalSetting['max_display_duration']-0;
                        var textDur = 0;
                        var collectionBookShort = 0;
                        var stepMinute = 0;
                        if(_duration == 15){
                            stepMinute = 2;
                            _duration = 30;
                        }
                        for(var x = 1; x <= 4; x++){
                            var disabledItem = false;
                            // console.log(item.radid,item.datatime[check_available+x]['book'], item.datatime[check_available+x]['time_array'])
                            textDur+=_duration;
                            var itemStepMin = x;
                            if(stepMinute != 0){
                                itemStepMin = stepMinute * x;
                            }
                            try{
                                if(item.radid,item.datatime[check_available+itemStepMin]['book'] == 1){
                                    collectionBookShort++;
                                }
                                var disabled_status = item.datatime[check_available+itemStepMin]['book'] == 1 ? 'disabled="disabled"' : '';
                                if(collectionBookShort > 0){
                                    disabledItem = true;
                                    disabled_status = 'disabled="disabled"';
                                }
                            }catch(e){
                                var disabled_status = 'disabled="disabled"' ;
                            }
                            var completeTime = date + " "+item.datatime[check_available+itemStepMin].time_array;
                            var endTime = date + " "+item.work_end;
                            var unixTime = moment(completeTime).format('X');
                            var unixTime2 = moment(endTime).format('X');
                            if(unixTime > unixTime2){
                                html += '<div class="col-xs-6 ">\
                                <a href="javascript:void(0);" disabled class="btn btn-lg btn-block bg-pink waves-effect" role="button">'+textDur+"min"+'</a>\
                                </div>';
                            }else{
                                if(disabledItem == true){
                                    html += '<div class="col-xs-6 ">\
                                    <a href="javascript:void(0);" disabled class="btn btn-lg btn-block bg-pink waves-effect" role="button">'+textDur+"min"+'</a>\
                                    </div>';
                                }else{
                                    html += '<div class="col-xs-6 ">\
                                        <a href="javascript:void(0);"  \
                                        data-timenum="'+check_available+'"  \
                                        data-timenextnum="'+(check_available+itemStepMin)+'"  \
                                        data-timevalue="'+time_available+'"   data-text="'+text+'"  \
                                        data-roomnum="'+index+'" data-duration="'+timeDurationArray[x-1]+'" \
                                        onclick="setDurationCrtBooking($(this))" \
                                        class="btn btn-lg btn-block bg-pink waves-effect" role="button">'+textDur+"min"+'</a>\
                                    </div>';
                                }
                                
                            }
                           
                        }
                        html += '  </div>\
                                </div>\
                            </div>\
                        </div>';
                      })
                      if(html == ""){
                        html += '<img style="height:auto;width:100%" class="img-booking" src="'+bs+'assets/web/no-data.jpg" >'
                      }
                      $('#id_area_booking_ad_room').html(html);
                      // console.log(col)
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                },
                error: errorAjax
            })
        }
        function checkBookingDatePick(text){
            var bs = $('#id_baseurl').val();
            gDateBooking = text;
            var url =  bs+"booking/check/pick-date/booking/"+text ;
            gBookingCrt['date'] = text;
            $.ajax({
                url :url,
                type : "GET",
                dataType: "json",
                beforeSend: function(){

                },
                success:function(data){
                    if(data.status == "success"){
                      var col = data.collection;

                      var html = "";
                      gTimeSelectBooking = col;
                      insertTheMergeRoom();
                      var date            = moment(gDateBooking).format('YYYY-MM-DD');
                      $.each(col, (index, item) => {   
                        var today_name = moment().format("dddd");
                        var room_work_day = item.work_day;
                        today_name = today_name.toUpperCase();
                        var ifDayExist = room_work_day.indexOf("today_name");
                        var image_room = "";
                        var check_available =  checkNowTimeRoomPicker(item.datatime, item.work_start, item.work_end)
                        // var check_available =  checkNowTimeRoom(item.datatime, item.work_start, item.work_end, text)

                        if(item.image == "" || item.image == null ){
                            image_room = 'http://placehold.it/500x300';
                        }else{
                            image_room = bs + path_room_image + item.image;
                        }
                        if(check_available == null){
                            console.log("skip room");
                            return true;
                        }
                        var time_available = item.datatime[check_available]['time_array'];
                        var display_button  = moment(date + " "+time_available).format("hh:mm A");  
                        html += '<div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">\
                            <div class="thumbnail">\
                                <img style="height:300px;" class="img-booking" src="'+image_room+'" >\
                                <div class="caption">\
                                    <div class="row clearfix">\
                                        <div class="col-sm-6 col-md-6">\
                                            <h3>'+item.name+'</h3>\
                                        </div>\
                                        <div class="col-sm-6 col-md-6 align-right">\
                                            <h3>'+item.capacity+' Persons</h3>\
                                        </div>\
                                    </div>\
                                    <div class="row clearfix">\
                                        <div class="col-sm-12 col-md-12">\
                                            <p>\
                                                '+item.facility_room+' \
                                            </p>\
                                        </div>\
                                    </div>\
                                    <div class="row clearfix">\
                                        <div class="col-xs-2"></div>\
                                        <div class="col-xs-8">\
                                            <button data-text="'+text+'"  data-roomnum="'+index+'" onclick="openAlertPilih($(this))" type="button" class="btn btn-lg btn-block bg-red waves-effect " aria-haspopup="true" aria-expanded="false">\
                                                <b id="id_display_button_'+index+'" > '+display_button+' </b> <span class="caret"> &nbsp;</span>\
                                            </button>\
                                        </div>\
                                        <div class="col-xs-2"></div>\
                                    </div>\
                                    <div class="row" id="id_area_duration_'+index+'">';
                        var _duration = generalSetting['duration']-0;
                        var _maxduration = generalSetting['max_display_duration']-0;
                        var textDur = 0;
                        var collectionBookShort = 0;
                        // console.log(date)
                        for(var x = 1; x <= 4; x++){
                            var disabledItem = false;
                            // console.log(item.radid,item.datatime[check_available+x]['book'], item.datatime[check_available+x]['time_array'])
                            textDur+=_duration;
                            try{
                                if(item.radid,item.datatime[check_available+x]['book'] == 1){
                                    collectionBookShort++;
                                }
                                var disabled_status = item.datatime[check_available+x]['book'] == 1 ? 'disabled="disabled"' : '';
                                if(collectionBookShort > 0){
                                    disabledItem = true;
                                    disabled_status = 'disabled="disabled"';
                                }
                            }catch(e){
                                var disabled_status = 'disabled="disabled"' ;
                            }
                            var completeTime = date + " "+item.datatime[check_available+x].time_array;
                            var endTime = date + " "+item.work_end;
                            var unixTime = moment(completeTime).format('X');
                            var unixTime2 = moment(endTime).format('X');
                            if(unixTime > unixTime2){
                                html += '<div class="col-xs-6 ">\
                                <a href="javascript:void(0);" disabled class="btn btn-lg btn-block bg-pink waves-effect" role="button">'+textDur+"min"+'</a>\
                                </div>';
                            }else{
                                if(disabledItem == true){
                                    html += '<div class="col-xs-6 ">\
                                    <a href="javascript:void(0);" disabled class="btn btn-lg btn-block bg-pink waves-effect" role="button">'+textDur+"min"+'</a>\
                                    </div>';
                                }else{
                                    html += '<div class="col-xs-6 ">\
                                        <a href="javascript:void(0);"  \
                                        data-timenum="'+check_available+'"  \
                                        data-timenextnum="'+(check_available+x)+'"  \
                                        data-timevalue="'+time_available+'"   data-text="'+text+'"  \
                                        data-roomnum="'+index+'" data-duration="'+timeDurationArray[x-1]+'" \
                                        onclick="setDurationCrtBooking($(this))" \
                                        class="btn btn-lg btn-block bg-pink waves-effect" role="button">'+textDur+"min"+'</a>\
                                    </div>';
                                }
                                
                            }
                           
                        }
                        html += '  </div>\
                                </div>\
                            </div>\
                        </div>';
                      })
                      if(html == ""){
                        html += '<img style="height:auto;width:100%" class="img-booking" src="'+bs+'assets/web/no-data.jpg" >'
                      }
                      $('#id_area_booking_ad_room').html(html);
                      // console.log(col)
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                },
                error: errorAjax
            })
        }
        function choosePickerDateCrt(){
            var endDate = moment().add(7, 'days');
            var startDate = moment().add(1, 'days');
            Swal.fire({
              title: 'Picker Date',
              // icon: 'info',
              html:
                '<center><div id="id_pickdate_crt" style="width:100%;"></div></center>'+
                '',
              showCloseButton: true,
              focusConfirm: false,
              confirmButtonText:
                'Great!',
              confirmButtonAriaLabel: 'Thumbs up, great!',
            });
            $('#id_pickdate_crt').datepicker({ 
                todayHighlight: true,
                toggleActive: true,
                startDate   : new Date(startDate),
                endDate     : new Date(endDate),
                format      : "yyyy-mm-dd",
            }).on('changeDate', function (e) {
                var tm =  moment(e.date).format('YYYY-MM-DD');
                var dates =  moment(e.date).format(' D MMMM YYYY');
                $('#id_pick_date_crt').html(dates);
                chooseTimeCrt(tm, "pick");
                Swal.close();
            })
        }
        function chooseTimeCrt(text = "", p = ""){
            var datatext = text =="" ? "today" : text;
            if(p != "pick"){
                $('#id_pick_date_crt').html("");
                checkBookingDate(datatext);

            }else{
                checkBookingDatePick(datatext);

            }
        }
        function openAlertPilih(t){
            
            var num              = t.data('roomnum');
            var text             = t.data('text') == "today" ? "" : t.data('text');
            var timeArray        = gTimeSelectBooking[num]['datatime'];
            var start            = gTimeSelectBooking[num]['work_start'];
            var end              = gTimeSelectBooking[num]['work_end'];
            var html             = "";
            html +=  '<table class="table table-hover select" >';
            html +=  '<tbody>';
            $.each(timeArray, (index, item)=>{
                var checkData       = checkThatTimeRoom(timeArray, index,item, start, end, text);
                var date            = moment().format('YYYY-MM-DD');
                var display_text    = moment(date + " "+item.time_array).format("hh:mm A");  
                // var display_text    = item.time_array 
                if(checkData){
                    // console.log(1,index)
                    html +=  '<tr data-timenum="'+index+'"  data-value="'+item.time_array+'"   data-text="'+display_text+'"  data-roomnum="'+num+'" onclick="setTimeCrtBooking($(this))" style="cursor:pointer;" ><td>'+display_text+'</td></tr>';
                }else{
                    html +=  '<tr class="disabled" ><td>'+display_text+'</td></tr>';
                }
                // console.log(checkData, item.time_array)

            });
            html +=  '</tbody>';
            html +=  '</table>';
            setTimeout(function(){
                Swal.fire({
                  title: 'Choose Time',
                  // icon: 'info',
                  html: html,
                  showCloseButton: true,
                  focusConfirm: false,
                  confirmButtonText:
                    'Cancel!',
                  confirmButtonAriaLabel: 'Close!',
                })
            },1000)
            
        }
        function setTimeCrtBooking(t){
            var num = t.data('roomnum');
            var roomnum = t.data('roomnum');
            var text = t.data('text');
            var value = t.data('value');
            var timenum = t.data('timenum');
            var html = "";
            var date = moment().format("YYYY-MM-DD");
            var timeRoomEnd = gTimeSelectBooking[num]['work_end'];
            var _duration = generalSetting['duration']-0;
            var _maxduration = generalSetting['max_display_duration']-0;
            var textDur = 0;
            for(var x=1;x<=4;x++){
                textDur+=_duration;
                var disabled = "disabled";
                try{
                    var timenextnum = timenum+x
                    var timeArray =  gTimeSelectBooking[num]['datatime'][timenextnum];
                    if(timeArray.book == 0){
                        disabled = "";
                        var completeTime = date + " "+timeArray.time_array;
                        var endTime = date + " "+timeRoomEnd;
                        var unixTime = moment(completeTime).format('X');
                        var unixTime2 = moment(endTime).format('X');

                        if(unixTime > unixTime2){
                            html += '<div class="col-xs-6 ">\
                            <a href="javascript:void(0);" disabled '+disabled+' class="btn btn-lg btn-block bg-pink waves-effect" role="button">'+textDur+'min</a>\
                            </div>';
                        }else{
                            // console.log(3)

                            html += '<div class="col-xs-6 ">\
                            <a href="javascript:void(0);" \
                            data-timenum="'+timenum+'"  \
                            data-timenextnum="'+timenextnum+'"  \
                            data-timevalue="'+value+'"   data-text="'+text+'"  data-roomnum="'+roomnum+'" data-duration="'+timeDurationArray[x-1]+'" \
                            onclick="setDurationCrtBooking($(this))" class="btn btn-lg btn-block bg-pink waves-effect" role="button">'+textDur+'min</a>\
                            </div>';
                        }

                    }else{
                        // console.log(timeArray)
                        html += '<div class="col-xs-6 ">\
                        <a href="javascript:void(0);"  disabled '+disabled+' class="btn btn-lg btn-block bg-pink waves-effect" role="button">'+timeDurationArray[x-1]+'</a>\
                        </div>';
                    }
                }catch(e){
                    // console.log(e)
                    html += '<div class="col-xs-6 ">\
                        <a href="javascript:void(0);" disabled '+disabled+' class="btn btn-lg btn-block bg-pink waves-effect" role="button">'+timeDurationArray[x-1]+'</a>\
                        </div>';
                    disabled = "disabled";

                }
            }
            // console.log(html);
            $("#id_area_duration_"+roomnum).html(html)
            $('#id_display_button_'+roomnum).html(text)
            Swal.close();
        }
        function setDurationCrtBooking(t){
            var roomnum = t.data('roomnum');
            var text = t.data('text');
            var timevalue = t.data('timevalue');
            var timenextnum = t.data('timenextnum');
            var timenum = t.data('timenum');
            var duration = t.data('duration');
            if(isUserPIC == false){
                swalShort("Attention !!!","Your are not PIC(Person In Charge)",type="warning" )
                return false;
            }
            // swalShort("Attention !!!","Your are not PIC(Person In Charge)",type="warning" )
            gBookingCrt['room'] = {
                num : roomnum,
                id  : gTimeSelectBooking[roomnum]['radid']
            }
            gBookingCrt['timevalue'] = timevalue;
            gBookingCrt['timeend'] = {
                num     : timenextnum,
                time    : gTimeSelectBooking[roomnum]['datatime'][timenextnum]
            };
            gBookingCrt['timestart'] = {
                num     : timenextnum,
                time    : gTimeSelectBooking[roomnum]['datatime'][timenum]
            };
            gBookingCrt['durationText'] = duration;
            gBookingCrt['change'] = true;
            getCrtBookingHTML_page2()
        }
        function getCrtBookingHTML_page1(){
            
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"booking/get/booking-create/page1",
                type : "GET",
                beforeSend: function(){

                },
                success:function(data){
                    $('#booknowpage').html(data)
                    gBookingCrt['change'] = true;
                    if(gBookingCrt['date'] == "today"){
                        $('#id_choose_today').click();
                        $('#id_pick_date_crt').html("")
                    }else{
                        $('#id_choose_date').click()
                        Swal.close();
                        $('#id_pick_date_crt').html(gBookingCrt['date']);
                        chooseTimeCrt(gBookingCrt['date'], "pick");
                    }
                },
                error: errorAjax
            })
        }
        function onButtonBack(){
            getCrtBookingHTML_page1()
        }
        function getCrtBookingHTML_page2(){
            gEmployeesSelectedArray = [];
            gEmployeesSelected = [];
            gPartisipantManual = [];
            gEmployeePIC = "";
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"booking/get/booking-create/page2",
                type : "GET",
                beforeSend: function(){

                },
                success:function(data){
                    console.log(gBookingCrt);
                    $('#booknowpage').html(data)
                    var htmlEmp = '<option value="">Choose</option>';
                    var htmlPIC = '<option value=""></option>';
                    var dateString = gBookingCrt['date'] == "today" ? moment().format('DD MMM YYYY') : moment(gBookingCrt['date']).format('DD MMM YYYY')  ;
                    var tStart = gBookingCrt['timestart']['time']['time_array'];
                    var tEnd = gBookingCrt['timeend']['time']['time_array'];
                    var roomnum = gBookingCrt['room']['num'];
                    var room = gTimeSelectBooking[roomnum];
                    // console.log(room)
                    var timeVarStart = moment(dateString + " " +tStart).unix();
                    var timeVarEnd = moment(dateString + " " +tEnd).unix();
                    // console.log(timeVarEnd-timeVarStart);
                    var room_price = room.price;
                    var cost = numeral(room.price).format( '$ 0,0');
                    $('#id_frm_crt_date').html(dateString);
                    $('#id_frm_crt_time_start').html(moment(dateString + " " +tStart).format("hh:mm A"));
                    $('#id_frm_crt_time_end').html(moment(dateString + " " +tEnd).format("hh:mm A"));
                    $('#id_frm_crt_cost').html( " - " );
                    $('#id_frm_crt_name').html(room.name);
                    $('#id_frm_crt_location').html(room.location);
                    $('#id_frm_crt_cost_room').html(room.price);
                    $('#id_frm_crt_capacity').html(room.capacity);
                    var facility_room_ar = room.facility_room.split(",");
                    var htmlfac = '<ol>';
                    for(var x in facility_room_ar){
                             htmlfac += '<li>';
                             htmlfac += facility_room_ar[x];
                             htmlfac += '</li>';
                    }
                    htmlfac += '</ol>';
                    $('#id_frm_crt_facility').html(htmlfac);
                    $.each(gEmployees,(index, item)=>{
                        htmlEmp += '<option value="'+item.nik+'">'+item.name+' - '+item.department_name+'</option>';
                    });
                    $.each(gEmployees,(index, item)=>{
                        var dddPIC = yourAlocationData[0];
                        if(item.nik == dddPIC.nik){
                         // htmlPIC = '<option value="'+item.nik+'">'+item.name+'</option>';
                         htmlPIC += '<option value="'+item.nik+'">'+item.name+' - '+item.department_name+'</option>';
                        }
                    });
                    $('#id_frm_crt_type_room').val(room.type_room);
                    $('#id_area_merge_room').hide();
                    if(room.type_room == "merge"){
                        changeTimeRoom('id_frm_crt_merge_room');
                    }

                    $('#id_frm_crt_participant').html(htmlEmp);
                    $('#id_frm_crt_participant').attr('disabled',true);
                    $('#id_frm_crt_pic').html(htmlPIC);
                    changePIC($('#id_frm_crt_pic'));
                    var image = room.image;
                    var imageurl = image == null || image == "" ? "http://placehold.it/500x300" : bs+'file/room/'+room.image;
                    $('#id_frm_crt_image').attr('src', imageurl);
                   
                    if(gPantryConfig.status == 1 && modules['pantry']['is_enabled'] == 1){
                        $('#id_pantry_area').hide("fast");
                        // $('#id_pantry_area').show("fast");
                    }
                    select_enable();
                    getPantryPackage();
                    // genegrateIntervalCrtBooking("start");
                },
                error: errorAjax
            })
        }
        function calculate_cost(){
            var roomnum = gBookingCrt['room']['num'];
            var room = gTimeSelectBooking[roomnum];
            var price = room.price -0;
            var alocationData ;
            var value = $('#id_frm_crt_alocation').val();
            var config_duration=generalSetting['duration'];
            var invoicectext=$('#id_invoicedata').val();
            var pick = gBookingCrt['date'];
            if(pick == "today"){
                var date = moment().format('YYYY-MM-DD');
            }else{
                var date = moment(pick).format('YYYY-MM-DD');
            }
            var time1 = date + " " +gBookingCrt['timestart']['time']['time_array'];
            var time2 = date + " " +gBookingCrt['timeend']['time']['time_array'];
            var a = moment(time1);
            var b = moment(time2);
            var fdur = b.diff(a, 'minute');
            if(fdur < 0){
                // ngeative
                $('#id_frm_crt_cost').html( " - " );
                // console.log(122223);
                return false;
            }
            var duration = fdur/config_duration; // 1
            var invoiceName= JSON.parse(invoicectext);
            if(value == ""){
                $('#id_frm_crt_cost').html( " - " );
            }else{
                for(var x in gAlocationPIC){
                    if(gAlocationPIC[x]['alocation_id'] ==  value){
                        alocationData = gAlocationPIC[x];
                    }
                }
                if(alocationData['invoice_status'] == 1){

                    var total =  price * duration;
                    // console.log(total, duration, price);
                    var stringtotal = numeral(total).format('$0,0.00');
                    $('#id_frm_crt_cost').html( stringtotal );
                    // return invoiceName[3]['name']
                }else{
                    $('#id_frm_crt_cost').html( "-" );
                    // return invoiceName[3]['name']
                }
            }
        }
        function changeALOCATION(t){
            calculate_cost()
        }
            
        function changePIC(t){
            var value =t.val();
            var bs = $('#id_baseurl').val();
            if(value == ""){
                var html = '<option></option>';
                $('#id_frm_crt_alocation').html(html);
                select_enable();
                $('#id_frm_crt_participant').attr('disabled',true);
                return false;
            }
            $.ajax({
                url : bs+"booking/get/data/alocation/"+value,
                type : "GET",
                dataType: "json",
                beforeSend: function(){

                },
                success:function(data){
                   if(data.status == "success"){
                        gEmployeePIC = value;
                        gAlocationPIC = data.collection;
                        var html = '';
                        html += '<option value=""> C H O O S E </option>';
                         $.each(data.collection,(index, item)=>{
                            if(index == 0){
                                html += '<option selected value="'+item.alocation_id+'">'+item.name+'</option>';
                            }else{
                                html += '<option value="'+item.alocation_id+'">'+item.name+'</option>';

                            }
                        });
                        
                        $('#id_frm_crt_alocation').html(html);
                        $('#id_frm_crt_participant').attr('disabled',false);
                        select_enable();
                        reloadPartisipant()
                   }

                },
                error: errorAjax
            })
        }
        function changeMergeRoom(){
            calculate_cost()
        }
        function changeTimeRoom(id){
            var date        = moment().format('YYYY-MM-DD');
            // var value =$('#'+).val();
            var bs = $('#id_baseurl').val();
            var roomnum = gBookingCrt['room']['num'];
            var dataroom = gTimeSelectBooking[roomnum];

            if(dataroom.merge_room == null){
                return;
            }
            var dataMergeRoom = dataroom.merge_room;
            var html = '';

            console.log(dataMergeRoom)
            for(var x in dataMergeRoom){
                var r = dataMergeRoom[x];
                var item = r;
                var disabledItem = false;


                var numStart = gBookingCrt['timestart']['num'];
                var numEnd = gBookingCrt['timeend']['num'];
                if(item.datatime[numEnd]['book'] == 1 || item.datatime[numStart]['book'] == 1){
                    disabledItem = true;
                }
                var completeTime = date + " "+item.datatime[numEnd].time_array;
                var endTime = date + " "+item.work_end;
                var unixTime = moment(completeTime).format('X');
                var unixTime2 = moment(endTime).format('X');
                if(unixTime > unixTime2){
                }else if(disabledItem == true){
                }else{
                    html += '<option value="'+r.radid+'" >'+r.name+'</option>'
                }
            }
            // console.log(id)    

            $('#'+id).html(html);
            select_enable();
            $('#id_area_merge_room').show('fast');
        }
        function clickPartisipantAdd(t){
            // var value =t.val();
            var value = $('#id_frm_crt_participant').val()
            $.each(value, (index, item) => {
                if(item != ""){
                    gEmployeesSelected.push(item);
                    for(var x in gEmployees){
                        if(gEmployees[x].nik ==item ){
                            gEmployeesSelectedArray.push(gEmployees[x]);
                             break;
                        }
                    }
                }
                
            })
            reloadPartisipant();
            reloadPartisipantSelected();
        }
        function reloadPartisipant(){
            var htmlEmp = '<option value=""></option>';
            $.each(gEmployees,(index, item)=>{
                if(gEmployeesSelected.indexOf(item.nik) < 0 ){
                    if(gEmployeePIC != item.nik){
                        htmlEmp += '<option value="'+item.nik+'">'+item.name+' - '+item.department_name+'</option>';
                    }
                }
            });
            $('#id_frm_crt_participant').html(htmlEmp);
            select_enable();
        }
        function reloadPartisipantSelected(){
            var htmlEmp = '';
            $.each(gEmployeesSelected,(index, item)=>{
                htmlEmp += '<tr id="id_tr_id_'+item+'" data-id="'+item+'">';
                htmlEmp += '<td style="width: 90%;">'+gEmployeesSelectedArray[index].name+'</td>';
                htmlEmp += '<td>\
                    <button data-item="'+item+'" onclick="removeCrtParticipant($(this))" type="button" class="btn bg-red btn-sm waves-effect">\
                        <i class="material-icons" >delete</i> \
                    </button>\
                </td>';
                htmlEmp += '</tr>';
            });
            $('#id_list_tbl_participant tbody').html(htmlEmp);
        }
        function reloadPartisipantManual(){
            var htmlEmp = '';
            $.each(gPartisipantManual,(index, item)=>{
                var s = "update";
                htmlEmp += '<tr  id="id_tr_idmanual_'+index+'" onclick="clickPartisipantManualOpen(&quot;'+s+'&quot;,'+index+')" style="cursor:pointer;">';
                htmlEmp += '<td style="width: 90%;">'+item.name+'</td>';
                htmlEmp += '<td>\
                    <button data-item="'+index+'" onclick="removeCrtParticipantManual($(this))" type="button" class="btn bg-red btn-sm waves-effect">\
                        <i class="material-icons" >delete</i> \
                    </button>\
                </td>';
                htmlEmp += '</tr>';
            });
            $('#id_list_tbl_participant_manual tbody').html(htmlEmp);
        }
        function removeCrtParticipant(t){
            var item = t.data('item');
            var idtr = '#id_tr_id_'+item;
            var removeN = gEmployeesSelected.indexOf(item);
            gEmployeesSelected.splice(removeN,1);
            gEmployeesSelectedArray.splice(removeN,1);
            $(idtr).remove().delay(200);
            reloadPartisipant();
        }
        function removeCrtParticipantManual(t){
            var item = t.data('item');
            var idtr = '#id_tr_idmanual_'+item;
            var removeN = item;
            gPartisipantManual.splice(removeN,1);
            $(idtr).remove().delay(200);
            reloadPartisipantManual()
        }
        function clickPartisipantManualOpen(tp = "", dd = 0){
            if(tp== "update"){
                var name = gPartisipantManual[dd]['name'];
                var email = gPartisipantManual[dd]['email'];
                var company = gPartisipantManual[dd]['company'];
                var position = gPartisipantManual[dd]['position'];
            }else{
                var name = "";
                var email = "";
                var company = "";
                var position = "";
            }
            var html = '<form id="id_frm_crt_manual" >\
                                <label for="id_frm_crt_manual_name">NAME</label>\
                                <div class="form-group">\
                                    <div class="form-line">\
                                        <input autocomplete="off" value="'+name+'" required type="text" id="id_frm_crt_manual_name" class="form-control" placeholder="Enter the name">\
                                    </div>\
                                </div>\
                                <label for="id_frm_crt_manual_email">EMAIL</label>\
                                <div class="form-group">\
                                    <div class="form-line">\
                                        <input autocomplete="off"  value="'+email+'" type="email" id="id_frm_crt_manual_email" class="form-control" placeholder="Enter the email address">\
                                    </div>\
                                </div>\
                                <label for="id_frm_crt_manual_org">COMPANY/ORGANIZATION</label>\
                                <div class="form-group">\
                                    <div class="form-line">\
                                        <input autocomplete="off" value="'+company+'"  required type="text" id="id_frm_crt_manual_company" class="form-control" placeholder="Enter the company/organization">\
                                    </div>\
                                </div>\
                                <label for="id_frm_crt_manual_position">POSITION</label>\
                                <div class="form-group">\
                                    <div class="form-line">\
                                        <input autocomplete="off" value="'+position+'"  type="text" id="id_frm_crt_manual_position" class="form-control" placeholder="Enter the position">\
                                    </div>\
                                </div>\
                                <br>\
                                <button style="display:none;" id="id_btn_crt_manual" class="btn btn-primary m-t-15 waves-effect">LOGIN</button>\
                            </form>';
            Swal.fire({
              title: 'Add participation manually',
              html:html,
              focusConfirm: false,
              showConfirmButton: true,
              showCancelButton: true,
              confirmButtonColor: '#3085d6',
              cancelButtonColor: '#d33',
              reverseButtons: true,
              confirmButtonText:tp == "update" ? "Update !" : 
                'Add !',
              confirmButtonAriaLabel: 'manually!',
              preConfirm: function(r) {
                    if($('#id_frm_crt_manual').valid()){
                        return true;
                    }else{
                        return false;
                    }
                // console.log($('#id_frm_crt_manual').valid())  
                }
            }).then((result) => {
                if(result.value){
                    var data = {
                        name : $('#id_frm_crt_manual_name').val(),
                        email : $('#id_frm_crt_manual_email').val(),
                        company : $('#id_frm_crt_manual_company').val(),
                        position : $('#id_frm_crt_manual_position').val(),
                    }
                    if(tp== "update"){
                        gPartisipantManual[dd]= data;
                    }else{

                        gPartisipantManual.push(data);
                    }
                    reloadPartisipantManual();

                }
                return false;
            });
        }
        
        function checkNowTimeRoom(timerange, start, end, pick = ""){
            var date        = moment().format('YYYY-MM-DD');
            var now         = moment().format('HH:mm');
            var unixStart   = moment(date + " " +start).format('X');
            var unixEnd     = moment(date + " " +end).format('X');
            var timeactive  = null;
            var unixNow     =  moment(date + " "+ now).format('X');
            $.each(timerange, (index, item) =>{
                var completeTime = date + " "+item.time_array;
                var unixTime = moment(completeTime).format('X');
                if(item.book == 0){
                    // cek waktu belum terbooking
                    if(unixStart <= unixTime && unixEnd >  unixTime ){
                        // cek bahwa waktu diantara jam buka dan tutup
                        if(unixTime > unixNow || pick != "today"){
                            // cek bahwa waktu ini lewat
                            if(timerange[index+1]['book'] == 0  ){
                                // 30 or 60 MIN
                                timeactive = index;
                                return false;
                            }else{
                                
                            }
                        }

                    }
                }
            });
            return timeactive;
        }
        function checkNowTimeRoomPicker(timerange, start, end, pick = ""){
            var date        = moment().format('YYYY-MM-DD');
            var now         = moment().format('HH:mm');
            var unixStart   = moment(date + " " +start).format('X');
            var unixEnd     = moment(date + " " +end).format('X');
            var timeactive  = null;
            var unixNow     =  moment(date + " "+ now).format('X');
            $.each(timerange, (index, item) =>{
                var completeTime = date + " "+item.time_array;
                var unixTime = moment(completeTime).format('X');
                if(item.book == 0){
                    // cek waktu belum terbooking
                    if(unixStart <= unixTime && unixEnd >  unixTime ){
                        // cek bahwa waktu diantara jam buka dan tutup
                        if(unixTime > unixNow || pick != "today"){
                            // cek bahwa waktu ini lewat
                            if(timerange[index+1]['book'] == 0  ){
                                // 30 or 60 MIN
                                timeactive = index;
                                return false;
                            }else{
                                
                            }
                        }

                    }
                }
            });
            // console.log(timeactive);
            return timeactive;
        }

        function checkThatTimeRoom(timerange, index,thattime, start, end, pick = ""){
            var date        = moment().format('YYYY-MM-DD');
            var now         = moment().format('HH:mm');
            var unixStart   = moment(date + " " +start).format('X');
            var unixEnd     = moment(date + " " +end).format('X');
            var timeactive  = false;
            var unixNow     =  moment(date + " "+ now).format('X');

            var completeTime = date + " "+thattime.time_array;
            var unixTime = moment(completeTime).format('X');
            var indexGet = index +1;
            if(indexGet  >= timerange.length){

                return timeactive;
            }
            if(pick == "" ){
                if(timerange[index]['book'] == 0){
                    if(unixStart <= unixTime && unixEnd >=  unixTime ){
                        // cek bahwa waktu diantara jam buka dan tutup
                        if(unixTime > unixNow ){
                            // cek bahwa waktu ini lewat
                            if(timerange[index+1]['book'] == 0  ){
                                timeactive = true;
                            }else{
                                    
                            }
                        }
                    }
                }
            }
            if(pick != ""){
                if(timerange[index]['book'] == 0){
                    if(unixStart <= unixTime && unixEnd >=  unixTime ){
                       if(timerange[index+1]['book'] == 0  ){
                            timeactive = true;
                        }else{
                                    
                        }
                    }
                }
                
            }
            return timeactive;
        }
        function clickSubmit(ele){
            $('#'+ele).click();
        }
        function clickSubmitCrtBooking(){

            var checkform = genegrateIntervalCrtBooking();
            if(!checkform){
                return false;
            }
            var bs = $('#id_baseurl').val();
            var roomNum = gBookingCrt['room']['num']
            var selectRoom = gTimeSelectBooking[roomNum];
            var datetime = selectRoom['datatime'];
            var datebooking = gBookingCrt['date']=="today" ? moment().format("YYYY-MM-DD") :  gBookingCrt['date'];
            var endTime = moment(datebooking + " "+ gBookingCrt['timeend']['time']['time_array']);
            var startTime = moment(datebooking + " "+ gBookingCrt['timestart']['time']['time_array']);
            var durationFunc = moment.duration(endTime.diff(startTime));
            var duration = durationFunc.asMinutes();
            // console.log(duration);
            var form = {
                timezone : moment.tz.guess(true),
                room : gTimeSelectBooking[roomNum],
                date : datebooking,
                duration : duration,
                timestart : gBookingCrt['timestart']['time'],
                timeend : gBookingCrt['timeend']['time'],
                partisipant : gEmployeesSelected,
                partisipantManual : gPartisipantManual,
                title : $('#id_frm_crt_title').val(),
                alocation : $('#id_frm_crt_alocation').val(),
                pic : $('#id_frm_crt_pic').val(),
                pantry_detail : gPantryDetail,
                pantry_package : $('#id_frm_crt_pantry').val(),
                note : $('#id_frm_crt_note').val(),
                external_link : $('#id_frm_crt_external_link').val(),
                merge_room : $('#id_frm_crt_merge_room').val(),
                is_merge : $('#id_frm_crt_type_room').val() == "merge" ? true: false,
            }

            
            Swal.fire({
                title:'Confirmation?',
                text: "Are you sure to book this meeting? !",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Submit Meeting!',
                cancelButtonText: 'Close !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                        $.ajax({
                            url: bs+"booking/post/book",
                            type :"post",
                            dataType : "json",
                            data : form,
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                            },
                            success:function(data){
                                $('#id_loader').html('');
                                if(data.status == "success"){
                                    showNotification('alert-success', "Succes create a booking "+$('#id_frm_crt_title').val() ,'top','center')
                                    Swal.fire({
                                        title:'Messaage',
                                        text: "Your schedule of meeting success to save ",
                                        type: "warning",
                                        showCancelButton: false,
                                        allowOutsideClick: false,
                                        confirmButtonColor: '#3085d6',
                                        cancelButtonColor: '#d33',
                                        confirmButtonText: 'Close !',
                                        cancelButtonText: 'Close !',
                                        reverseButtons: true
                                        }).then((result) => {
                                            if (result.value) {
                                                window.location.reload();
                                            }
                                        else{

                                        }
                                    })
                                }else{
                                    var msg = "Your session is expired, login again !!!";
                                    Swal.fire('Warning!', data.msg,'warning')
                                }
                            },
                            error: errorAjax
                        })
                    }
                else{

                }
            })
        }
        $('#frm_reschedule').submit(function(e){

            e.preventDefault();
            var bs = $('#id_baseurl').val();
            var form = $('#frm_reschedule').serialize();
            var dtt = $('#id_frm_res_date').val();
            var unixTimeStart = moment($('#id_frm_res_start_input').val()).format('X');
            var unixTimeEnd = moment($('#id_frm_res_end_input').val()).format('X');
            // console.log($('#id_frm_res_start_input').val())
            if($('#id_frm_res_date').val() == ""){
                Swal.fire('Attention !!!','Date coloumn cannot be empty!','warning')
                // showNotification('alert-danger', "Date coloumn cannot be empty",'top','center')
                $('#id_frm_res_date_dummy').focus();
                return false;
            }else if($('#id_frm_res_start_input').val() == "" || $('#id_frm_res_end_input').val() == ""){
                Swal.fire('Attention !!!','Please selected the time!','warning')
                // showNotification('alert-danger', "Please selected the time",'top','center')
                return false;

            }else if(unixTimeStart > unixTimeEnd || unixTimeStart == unixTimeEnd ){
                Swal.fire('Attention !!!','Start time must to be more than Finish time, or start & finish time cannot be equal!','warning')
                // showNotification('alert-danger', "Start time must to be more than Finish time, or start & finish time cannot be equal.",'top','center')
                return false;
            }
            if($('#id_frm_res_date_dummy').val() == ""){
                Swal.fire('Attention !!!','Please select a date ','warning')
                return false;
            }
            // console.log(form)
            Swal.fire({
                title:'Confirmation?',
                text: "Are you sure to reschedule this meeting? ",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Reschedule Meeting !',
                cancelButtonText: 'Close !',
                reverseButtons: true
                }).then((result) => {
                    if (result.value) {
                        $.ajax({
                            url: bs+"booking/post/rebook",
                            type :"post",
                            dataType : "json",
                            data : form,
                            beforeSend: function(){
                                $('#id_loader').html('<div class="linePreloader"></div>');
                            },
                            success:function(data){
                                 $('#id_loader').html('');
                                if(data.status == "success"){
                                    showNotification('alert-success', "Succes reschedule a booking "+$('#id_res_text_name').html() ,'top','center')
                                    $('#id_mdl_reschedule').modal('hide')
                                    Swal.fire({
                                        title:'Messaage',
                                        text: "Your schedule of meeting success to save ",
                                        type: "warning",
                                        showCancelButton: false,
                                        allowOutsideClick: false,
                                        confirmButtonColor: '#3085d6',
                                        cancelButtonColor: '#d33',
                                        confirmButtonText: 'Close !',
                                        cancelButtonText: 'Close !',
                                        reverseButtons: true
                                        }).then((result) => {
                                            if (result.value) {
                                                window.location.reload();
                                            }
                                        else{

                                        }
                                    })

                                }else{
                                    var msg = "Your session is expired, login again !!!";
                                    showNotification('alert-danger', msg,'top','center')
                                }
                            },
                            error: errorAjax
                        })
                    }
                else{

                }
            })
            
        })
        function checkThatTimeRoomRes(timerange, index,thattime, start, end, pick = ""){
            // console.log(pick);
            var datenow     = moment().format('YYYY-MM-DD');
            var date        = moment().format('YYYY-MM-DD');
            var now         = moment().format('HH:mm');
            var unixStart   = moment(pick + " " +start).format('X');
            var unixEnd     = moment(pick + " " +end).format('X');
            var timeactive  = false;
            var unixNow     =  moment(pick + " "+ now).format('X');

            var completeTime = pick + " "+thattime.time_array;
            var unixTime = moment(completeTime).format('X');
            // console.log(pick,datenow)
            var indexGet = index +1;
            if(indexGet  >= timerange.length){

                return timeactive;
            }
            if(pick == datenow){
                // today
                // console.log(unixStart , unixTime, unixEnd);
                if(timerange[index]['book'] == 0){
                    if(unixStart <= unixTime && unixEnd >=  unixTime ){
                        // cek bahwa waktu diantara jam buka dan tutup
                        if(unixTime > unixNow ){
                            // cek bahwa waktu ini lewat
                            if(timerange[index]['book'] == 0  ){
                                timeactive = true;
                            }else{
                                    
                            }
                        }
                    }
                }
            }else{
                // other day
                if(timerange[index]['book'] == 0){
                    if(unixStart <= unixTime && unixEnd >=  unixTime ){
                        timeactive = true;
                        // cek bahwa waktu diantara jam buka dan tutup
                        // if(unixTime > unixNow ){
                        //     // cek bahwa waktu ini lewat
                        //     if(timerange[index]['book'] == 0  ){
                        //     }else{
                                    
                        //     }
                        // }
                    }
                }
            }
            
            return timeactive;
        }
        function openAlertPilihRes(t){
            var ele              = t.data('id');         
            var roomNum          = 0; // first
            var timeType         = t.data('type');      
            var selectRoom       = gTimeSelectBookingRes[0]; // first
            var timeArray        = selectRoom['datatime'];
            var start            = selectRoom['work_start'];
            var end              = selectRoom['work_end'];
            var text             = gBookingCrt['date'] == "today" ? "" : gBookingCrt['date'];
            var html             = '';
            var getdate         = $('#id_frm_res_date').val();
            html +=  '<table class="table table-hover select" >';
            html +=  '<tbody>';
            var bookqueue = false;
            console.log(timeArray);
            $.each(timeArray, (index, item)=>{
                var tnow            = moment().format('hh:mm') + ":00";
                var checkData       = checkThatTimeRoomRes(timeArray, index,item, start, end, getdate);
                var date            = moment().format('YYYY-MM-DD');
                var display_text    = moment(date + " "+item.time_array).format("hh:mm A");
                if(checkData){
                    // console.log( item )
                    if(item.book > 0){
                        bookqueue = true;
                    }
                    if(timeType == "end"){
                        if(bookqueue == true){
                             html +=  '<tr class="disabled" ><td>'+display_text+'</td></tr>';
                        }else{
                            // console.log(1234)
                            var strStart = $('#id_frm_res_start_input').val()
                            var timestart = moment(strStart).format('hh:mm') + ":00";
                            var unixTimeStart = moment(date+" "+timestart).format('X');
                            var unixTimeEnd = moment(date + " "+item.time_array).format('X');
                            // console.log(timestart, item.time_array)
                            if(unixTimeStart > unixTimeEnd || unixTimeStart == unixTimeEnd){
                                console.log(1234)
                                html +=  '<tr class="disabled" ><td>'+display_text+'</td></tr>';
                            }else{
                                html +=  '<tr data-timeType="'+timeType+'" data-ele="'+ele+'" \
                                class="" data-timenum="'+index+'"  \
                                data-value="'+item.time_array+'"   \
                                data-text="'+display_text+'" data-roomnum="'+roomNum+'" \
                                onclick="setTimeCrtBookingInsideRes($(this))" \
                                style="cursor:pointer;" ><td>'+display_text+'</td></tr>';
                            }
                        }
                    }else{
                        // start
                        html +=  '<tr data-timeType="'+timeType+'" data-ele="'+ele+'" \
                            class="" data-timenum="'+index+'"  \
                            data-value="'+item.time_array+'"   \
                            data-text="'+display_text+'" data-roomnum="'+roomNum+'" \
                            onclick="setTimeCrtBookingInsideRes($(this))" \
                            style="cursor:pointer;" ><td>'+display_text+'</td></tr>';
                    }
                    
                }else{
                    html +=  '<tr class="disabled" ><td>'+display_text+'</td></tr>';
                }
            });
            html +=  '</tbody>';
            html +=  '</table>';
            Swal.fire({
              title: 'Choose Time',
              icon: 'info',
              html: html,
              showCloseButton: true,
              focusConfirm: false,
              confirmButtonText:
                'Close!',
              confirmButtonAriaLabel: 'Close!',
            });
        }
        function openAlertPilihCrt(t){
            var ele              = t.data('id');         
            var roomNum          = gBookingCrt['room']['num'];
            var timeType         = t.data('type');      
            var selectRoom       = gTimeSelectBooking[roomNum];
            var timeArray        = selectRoom['datatime'];
            var start            = selectRoom['work_start'];
            var end              = selectRoom['work_end'];
            var text             = gBookingCrt['date'] == "today" ? "" : gBookingCrt['date'];
            var html             = '';
            html +=  '<table class="table table-hover select" >';
            html +=  '<tbody>';
            $.each(timeArray, (index, item)=>{
                var tnow            = moment().format('hh:mm') + ":00";
                var checkData       = checkThatTimeRoom(timeArray, index,item, start, end, text);
                var date            = moment().format('YYYY-MM-DD');
                var display_text    = moment(date + " "+item.time_array).format("hh:mm A");
                // console.log(ele)  
                if(checkData){
                    if(timeType == "end"){
                        var timestart = gBookingCrt['timestart']['time']['time_array'];
                        if(timestart == item.time_array){
                            html +=  '<tr class="disabled" ><td>'+display_text+'</td></tr>';
                        }else{
                            html +=  '<tr data-timeType="'+timeType+'" data-ele="'+ele+'" class="" data-timenum="'+index+'"  data-value="'+item.time_array+'"   data-text="'+display_text+'" data-roomnum="'+roomNum+'" onclick="setTimeCrtBookingInside($(this))" style="cursor:pointer;" ><td>'+display_text+'</td></tr>';
                        }
                    }else{
                        html +=  '<tr data-timeType="'+timeType+'" data-ele="'+ele+'" class="" data-timenum="'+index+'"  data-value="'+item.time_array+'"   data-text="'+display_text+'" data-roomnum="'+roomNum+'" onclick="setTimeCrtBookingInside($(this))" style="cursor:pointer;" ><td>'+display_text+'</td></tr>';
                        
                    }
                    
                }else{
                    html +=  '<tr class="disabled" ><td>'+display_text+'</td></tr>';
                }
            });
            html +=  '</tbody>';
            html +=  '</table>';
            Swal.fire({
              title: 'Choose Time',
              html: html,
              showCloseButton: true,
              focusConfirm: false,
              confirmButtonText:
                'Close!',
              confirmButtonAriaLabel: 'Close!',
            });
        }
        function setTimeCrtBookingInsideRes(t){
            var ele = t.data('ele');         
            var roomnum = t.data('roomnum');
            var text = t.data('text');
            var value = t.data('value');
            var timenum = t.data('timenum');
            var timeType = t.data('timetype');
            var ddd = moment().format("YYYY-MM-DD");
            var valueOri = "";
            var html = "";
            var selectRoom       = gTimeSelectBookingRes[0]; // first
            var timeArray        = selectRoom['datatime'];
            var dddd = [];
            if(timeType == "end"){
                var str = $('#id_frm_res_start_input').val() 
                var stsp  = str.split(" ");
                var stdate = stsp[0];
                var dtt = $('#id_frm_res_date').val();
                // $('#id_frm_res_end_input').val(dtt + " " +value);
                var endtime = timeArray[timenum];
                var sttmoment = moment(str).format("YYYY-MM-DD HH:mm");
                var endmoment = moment(stdate + " " + endtime.time_array).format("YYYY-MM-DD HH:mm");
                var nnn = 0;    
                $.each(timeArray, (index, item)=>{
                    var ttmm =  moment(stdate + " " + item.time_array).format("YYYY-MM-DD HH:mm");
                    if( ttmm > sttmoment && ttmm < endmoment){
                        if(item.book == "1" || item.book==1){
                            nnn++;
                            dddd.push(item)
                        }
                    }
                })
                if(nnn > 1){
                    Swal.fire('Attention !!!','Time is used up, change your time!','warning')
                }else{
                    if(sttmoment == endmoment){
                        Swal.fire('Attention !!!','Finish/End time must more than start time','warning')
                    }else{
                        var elemantHtml = moment(ddd + " "+ value).format("hh:mm A");
                        $('#'+ele).html(elemantHtml);
                        $('#id_frm_res_end_input').val(dtt + " " +value);
                        Swal.close();
                        // Swal.fire('Attention !!!','Time is used up, change your time!','warning')
                    }
                    
                }

            }else{
                var dtt = $('#id_frm_res_date').val();
                $('#id_frm_res_start_input').val(dtt + " " +value)
                var elemantHtml = moment(ddd + " "+ value).format("hh:mm A");
                $('#'+ele).html(elemantHtml);
                $('#id_frm_res_finish').html(" - ");
                $('#id_frm_res_end_input').val("");
                Swal.close();
            }
            
           
        }
        function setTimeCrtBookingInside(t){
            var ele = t.data('ele');         
            var roomnum = t.data('roomnum');
            var text = t.data('text');
            var value = t.data('value');
            var timenum = t.data('timenum');
            var timeType = t.data('timetype');
            var ddd = moment().format("YYYY-MM-DD");
            var valueOri = "";
            var html = "";
            if(timeType == "end"){
                
                var end = moment(ddd + " "+ value);
                var startTime = moment(ddd + " "+ gBookingCrt['timevalue']);
                var duration = moment.duration(end.diff(startTime));
                gBookingCrt['timeend'] = {
                    num : timenum,
                    time :  gTimeSelectBooking[roomnum]['datatime'][timenum],
                };
            }else{
                gBookingCrt['timevalue'] = value;
                gBookingCrt['timestart'] = {
                    num : timenum,
                    time :  gTimeSelectBooking[roomnum]['datatime'][timenum],
                };
            }
            var elemantHtml = moment(ddd + " "+ value).format("hh:mm A");
            $('#'+ele).html(elemantHtml);
            Swal.close();
            calculate_cost();
            changeTimeRoom('id_frm_crt_merge_room');
        }
        function openPartispant(t){
            var bs = $('#id_baseurl').val();
            var booking_id = t.data('booking_id');
            var title = t.data('title');
            var form = {
                booking_id : booking_id,
                title : title,
            }
            $.ajax({
                url: bs+"booking/get/partisipant",
                type :"post",
                dataType : "json",
                data : form,
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        clearTable($('#tbldataInternal'));
                        clearTable($('#tbldataEksternal'));
                        var colin = data.collection['internal'];
                        var colek = data.collection['eksternal'];
                        var htmlin = '';
                        var htmlek = '';
                        var nnn = 0;
                        var mmm = 0;
                        $('#id_partisipant_title').html(title)
                        $.each(colin, function(index, item){
                            nnn++;
                            var stt = item.attendance_status == 1 ? "Attend" : "No Attend";
                            stt = item.execute_attendance == 0 ? "" : stt ;
                            htmlin += '<tr>'
                            htmlin += '<td>'+nnn+'</td>';
                            htmlin += '<td>'+(item.emp_name == null ? "" : item.emp_name.toUpperCase())+'</td>';
                            htmlin += '<td >'+(item.emp_email == null ? "" : item.emp_email)+'</td>';
                            htmlin += '<td >'+(item.emp_phone == null ? "" : item.emp_phone)+'</td>';
                            htmlin += '<td >'+(item.emp_extnum == null ? "" : item.emp_extnum)+'</td>';
                            htmlin += '<td>'+stt+'</td>';
                            htmlin += '</tr>'
                        })
                        $.each(colek, function(index, item1){
                            mmm++;
                            var stt = item1.attendance_status == 1 ? "Attend" : "No Attend";
                            stt = item1.execute_attendance == 0 ? "" : stt;
                            htmlek += '<tr>'
                            htmlek += '<td>'+mmm+'</td>';
                            htmlek += '<td>'+(item1.name == null ? "" : item1.name.toUpperCase())+'</td>';
                            htmlek += '<td>'+(item1.email == null ? "" : item1.email)+'</td>';
                            htmlek += '<td>'+(item1.company == null ? "" : item1.company)+'</td>';
                            htmlek += '<td>'+(item1.position == null ? "" : item1.position)+'</td>';
                            htmlek += '<td>'+stt+'</td>';
                            htmlek += '</tr>'
                        console.log(htmlek)

                        })
                        $('#tbldataInternal tbody').html(htmlin)
                        $('#tbldataEksternal tbody').html(htmlek)
                        $('#id_mdl_partisipant').modal('show')
                        initTable($('#tbldataInternal'));
                        initTable($('#tbldataEksternal'));
                        $('#id_loader').html('');
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                },
                error: errorAjax
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
       function getPantry(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"setting/general/get/panty",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        gPantryConfig = data.collection;
                        console.log(gPantryConfig);
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function getPantryPackage(){
            var bs = $('#id_baseurl').val();
            $.ajax({
                url : bs+"pantry/get/package",
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        var c  = data.collection;
                        var html = '<option value="">C H O O S E</option> ';

                        $.each(c, (i, t) => {
                            html += '<option value="'+t.id+'">'+t.name+'</option> ';
                        })
                        $('#id_frm_crt_pantry').html(html);
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
        function oncPantry(){
            var package = $('#id_frm_crt_pantry').val();
            var bs = $('#id_baseurl').val();
            if(package == ""){
                gPantryDetail = [];
                $('#id_list_tbl_patry tbody').html('');
                return false;
            }
            $.ajax({
                url : bs+"pantry/get/package-update/"+package,
                type : "GET",
                dataType: "json",
                beforeSend: function(){
                    $('#id_loader').html('<div class="linePreloader"></div>');
                },
                success:function(data){
                    if(data.status == "success"){
                        var c  = data.collection;
                        var html = '';
                        gPantryDetail = [];
                        $.each(c.detail, (i, t) => {
                            var d = {
                                num : t._generate,
                                id : t.id,
                                name : t.name,
                                qty : 0,
                                note : "",
                                status : 1
                            }
                            gPantryDetail.push(d);
                        })
                        var nn = 0;
                        $.each(gPantryDetail, (i, t) => {
                            // var num = i;
                            var ch = (t.status == 1) ? 'checked' : '';
                            html += '<tr>'
                            html += '<td>'+nn+'</td>';
                            html += '<td>'+t.name+'</td>';
                            html += '<td><input type="number" onkeyup="oncInputPantry($(this))"  data-type="qty" data-id="'+t.num+'" id="id_frm_pantry_qty_'+t.num+'" value="'+t.qty+'" /></td>';
                            html += '<td><input type="text" onkeyup="oncInputPantry($(this))" data-type="note" data-id="'+t.num+'"  id="id_frm_pantry_note_'+t.num+'" value="'+t.note+'" /></td>';
                            html += '<td>';
                            html += '   <div class="switch"> \
                                            <label><input '+ch+' onchange="oncSwPantry($(this))"  type="checkbox" value="'+t.num+'" id="id_switch_pantry"><span class="lever switch-col-red"></span></label> \
                                        </div>';
                            html += '</td>';
                            html += '</tr>'
                        })
                        $('#id_list_tbl_patry tbody').html(html);
                    }else{
                        var msg = "Your session is expired, login again !!!";
                        showNotification('alert-danger', msg,'top','center')
                    }
                    $('#id_loader').html('');
                },
                error: errorAjax
            })
        }
        function oncSwPantry(t){
            var v = t.val();
            var x = ( t.prop('checked') ) ? 1 : 0;
            for(var i in gPantryDetail){
                var r = gPantryDetail[i];
                if(r.num == v){
                    gPantryDetail[i].status = x;
                    break;
                }
            }
        }
        function oncInputPantry(t){
            var num = t.data('id');
            var type = t.data('type');
            var value = t.val();
            // console.log(num, type, value)
            for(var i in gPantryDetail){
                var r = gPantryDetail[i];
                if(r.num == num){
                    // console.log(gPantryDetail[i][type], value)
                    if(type == 'qty'){
                        value = value -0;
                    }
                    gPantryDetail[i][type] = (value);
                    break;
                }
            }
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