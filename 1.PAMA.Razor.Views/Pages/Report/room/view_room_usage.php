<div class="row clearfix">
    <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
        <div class="input-group">
            <span class="input-group-addon">
                <i class="material-icons">date_range</i>
            </span>
            <div class="form-line">
                <input id="id_roomusage_daterange_search" type="text" class="form-control ">
            </div>
        </div>
    </div>
    <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2">
        <div class="input-group">
            <span class="input-group-addon">
                <i class="material-icons">my_location</i>
            </span>
            <select onchange="ocBuilding()" class="form-control" name="id_building" id="id_roomusage_building_search" data-live-search="true">
                <option value="">All Building</option>
            </select>
        </div>
    </div>
    <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2">
        <div class="input-group">
            <span class="input-group-addon">
                <i class="material-icons">map</i>
            </span>
            <select onchange="ocRoom()" class="form-control" name="id_room" id="id_roomusage_room_search" data-live-search="true">
                <option value="">All Room</option>
            </select>
        </div>
    </div>
    <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
        <select class="form-control" name="status_invoice" id="id_roomusage_department_search" data-live-search="true">
            <option value=""><b>All Department</b></option>
            <?php foreach ($alocation as $key => $value): ?>
            <option value="<?=$value['id']?>">
                <?=$value['name']?>
            </option>
            <?php endforeach?>
        </select>
    </div>
    <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2">
        <button class="btn btn-success btn-block waves-effect" onclick="initRoom()"><b>Filter</b></button>
    </div>
</div>

<div class="row table-responsive responsive">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
        <table id="id_tbl_room" class="table table-hover table-bordered">
            <thead>
                <th>#</th>
                <th><button class="btn btn-success " onclick="alertExportToAll('excell')">Export to Excell</button></th>
                <th>ID</th>
                <th>Title</th>
                <th>Meeting Time</th>
                <th>Room</th>
                <!-- <th>Room Adddress</th> -->
                <th>Department</th>
                <th>Attendees</th>
                <th>Duration</th>
                <?php if ($modules['price']['is_enabled'] == 1): ?>
                <th>Rent Cost</th>
                <th>Status Invoicing</th>
                <?php else: ?>
                <?php endif;?>
            </thead>
            <tbody></tbody>
        </table>
    </div>
</div>