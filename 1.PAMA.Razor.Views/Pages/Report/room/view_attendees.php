<div class="row clearfix">
    <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
        <div class="input-group">
            <span class="input-group-addon">
                <i class="material-icons">date_range</i>
            </span>
            <div class="form-line">
                <input id="id_attendees_daterange_search" type="text" class="form-control ">
            </div>
        </div>
    </div>
    <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2">
        <div class="input-group">
            <span class="input-group-addon">
                <i class="material-icons">people</i>
            </span>
            <select class="form-control" id="id_attendees_employee_search" data-live-search="true">
                <option value="">All Employee</option>
            </select>
        </div>
    </div>
    <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2">
        <div class="input-group">
            <span class="input-group-addon">
                <i class="material-icons">my_location</i>
            </span>
            <select onchange="ocAttendeesBuilding()" class="form-control" id="id_attendees_building_search" data-live-search="true">
                <option value="">All Building</option>
            </select>
        </div>
    </div>
    <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2">
        <div class="input-group">
            <span class="input-group-addon">
                <i class="material-icons">map</i>
            </span>
            <select onchange="ocRoom()" class="form-control" id="id_attendees_room_search" data-live-search="true">
                <option value="">All Room</option>
            </select>
        </div>
    </div>
    <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2">
        <button class="btn btn-success waves-effect" onclick="filterAttendees()"><b>Filter</b></button>
        <button type="button" class="btn btn-info btn-sm waves-effect"  onclick="alertExportToAttendeesToExcell('excell')"> <i class="material-icons" style="font-size:16px;">print</i></button>
    </div>
    <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2">
        <!-- <button class="btn btn-success " onclick="alertExportToAll('excell')">Export to Excell</button> -->
    </div>
</div>

<div class="row table-responsive responsive">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
        <table id="id_tbl_attendees" class="table table-hover table-bordered">
            <thead>
                <th></th>
                <!-- <th></th> -->
                <th>Name</th>
                <th>Company&Department</th>
                <th>Meeting</th>
                <th>Present</th>
                <th>Absent</th>
                <th>Total Duration</th>
                <?php if ($modules['room_adv']['is_enabled'] == 1): ?>
                    <th>Attendees Check-in</th>
                <?php else: ?>
                <?php endif;?>
               
            </thead>
            <tbody></tbody>
        </table>
    </div>
</div>