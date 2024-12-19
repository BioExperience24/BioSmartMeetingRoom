<div class="modal fade" id="id_mdl_update" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="idmdlcrLabel">Update Room</h4>
            </div>
            <div class="modal-body">
                <ul class="nav nav-tabs tab-nav-right" role="tablist">
                    <li role="presentation" class="active"><a href="#room-general-edit" data-toggle="tab" onclick="actionBtnEdit('id_btn_edt_submit')">General</a></li>
                    <?php if ($modules['room_adv']['is_enabled'] == 1): ?>
                    <li role="presentation"><a href="#room-advanced-edit" data-toggle="tab">Advanced Setting</a></li>
                    <li role="presentation" id="id_panel_checkin" style="display:none;"><a href="#room-checkin-edit" data-toggle="tab">Check In Setting</a></li>
                    <?php endif ?>
                </ul>
                <br>
                <div class="tab-content">
                    <div role="tabpanel" class="tab-pane fade in active" id="room-general-edit">
                        <form id="frm_update">
                            <div class="row clearfix">
                                <div class="col-xs-7">
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label for="">Name <b style="color:red;">*</b></label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input type="text" autocomplete="off" name="name" id="id_edt_name" required="" class="form-control" placeholder="Name">
                                                    <input type="hidden" autocomplete="off" name="id" id="id_edt_id" readonly="" required="">
                                                    <input type="hidden" autocomplete="off" name="radid" id="id_edt_radid" readonly="" required="">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <label for="">Building</label>
                                            <div class="form-group">
                                                <select title="Choose the building..." name="building_id" id="id_edt_building_id" class="form-control show-tick">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label for="">Type Room</label>
                                            <div class="form-group">
                                                <select title="Choose the type..." onchange="initTypeRoom('edit','id_edt_type_room','id_edt_merge_room','')" name="type_room" id="id_edt_type_room" class="form-control show-tick">
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-xs-6" id="id_area_edt_merge">
                                            <label for="">Merger Room</label>
                                            <div class="form-group">
                                                <select title="Choose the any room..." name="merge_room[]" id="id_edt_merge_room" class="form-control show-tick" multiple data-actions-box="true">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <label for="">Capacity <b style="color:red;">*</b></label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input required="" type="number" name="capacity" id="id_edt_capacity" required="" class="form-control" placeholder="Capacity">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <label for="">Working Day <b style="color:red;">*</b></label>
                                            <div class="form-group">
                                                <select data-dropup-auto="false" title="Choose the day..." data-selected-text-format="count > 2" data-actions-box="true" required="" multiple name="work_day[]" id="id_edt_workday" class="form-control show-tick dropdown">
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <label for="">Working Time <b style="color:red;">*</b></label>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6 col-xs-6 col-lg-6 col-sm-6">
                                                        <div class="form-line">
                                                            <input required="" type="text" name="work_start" id="id_edt_work_start" class="timepicker form-control" placeholder="Please choose a start time...">
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 col-xs-6 col-lg-6 col-sm-6">
                                                        <div class="form-line">
                                                            <input required="" type="text" name="work_end" id="id_edt_work_end" class="timepicker form-control" placeholder="Please choose a finish time...">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label for="">Description</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <textarea rows="1" name="description" class="form-control no-resize" id="id_edt_description" placeholder="Please type the room description..."></textarea>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <label for="">Detail Location</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <textarea rows="1" name="location" class="form-control no-resize" id="id_edt_location" placeholder="Please type the location..."></textarea>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-5">
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label for="">Facility</label>
                                            <div class="form-group">
                                                <select title="Choose the facility..." data-actions-box="true" data-selected-text-format="count > 3" multiple name="facility_room[]" id="id_edt_facility_room" class="form-control show-tick dropup"></select>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <label for="">Status</label>
                                            <div class="form-group">
                                                <select title="Choose the status..." name="is_disabled" id="id_edt_is_disabled" class="form-control show-tick">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label for="">Link Map</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input type="text" autocomplete="off" name="google_map" id="id_edt_google_map" class="form-control" placeholder="Link Map">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <?php if ($modules['price']['is_enabled'] == 1): ?>
                                            <!-- <label for="">Price of the room per room<b style="color:red;">*</b></label>
                                                            <div class="form-group">
                                                                <div class="form-line">
                                                                    <input  type="number" name="price" id="id_edt_price"  class=" form-control" required="" placeholder="Price Room ...">
                                                                </div>
                                                                
                                                            </div> -->
                                            <?php else: ?>
                                            <?php endif; ?>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label for="">Old Thumbnail </label>
                                            <a href="javascript:void(0);" class="thumbnail">
                                                <img id="id_edt_image_old" src="" style="height:100px" class="img-responsive">
                                            </a>
                                        </div>
                                        <div class="col-xs-6">
                                            <label for="">New Thumbnail </label>
                                            <div class="form-group">
                                                <input type="file" name="image" id="id_edt_image" class="dropify" data-height="100" data-max-file-size="2M" accept="image/*" />
                                            </div>
                                        </div>
                                    </div>
                                    <!-- <div class="row">
                                                    <div class="col-xs-4">
                                                        <label for="">Old Image 1 </label>
                                                        <a href="javascript:void(0);" class="thumbnail">
                                                            <img id="id_edt_image2_1_old" src="" style="height:100px" class="img-responsive">
                                                        </a>
                                                    </div>
                                                    <div class="col-xs-4">
                                                        <label for="">Old Image 2 </label>
                                                        <a href="javascript:void(0);" class="thumbnail">
                                                            <img id="id_edt_image2_2_old" src="" style="height:100px" class="img-responsive">
                                                        </a>
                                                    </div>
                                                    <div class="col-xs-4">
                                                        <label for="">Old Image 3 </label>
                                                        <a href="javascript:void(0);" class="thumbnail">
                                                            <img id="id_edt_image2_3_old" src="" style="height:100px" class="img-responsive">
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-4">
                                                        <label for="">New Image 1 </label>
                                                        <div class="form-group">
                                                            <input type="file" name="image2_1"  class="dropify" data-height="100" data-max-file-size="2M" accept="image/*"   
                                                            />
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-4">
                                                        <label for="">New Image 2 </label>
                                                        <div class="form-group">
                                                            <input type="file" name="image2_2"  class="dropify" data-height="100" data-max-file-size="2M" accept="image/*"   
                                                            />
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-4">
                                                        <label for="">New Image 3 </label>
                                                        <div class="form-group">
                                                            <input type="file" name="image2_3"  class="dropify" data-height="100" data-max-file-size="2M" accept="image/*"   
                                                            />
                                                        </div>
                                                    </div>
                                                </div> -->
                                </div>
                                <!-- Split -->
                            </div>
                            <?php if ($modules['automation']['is_enabled'] == 1): ?>
                            <label for="">Automation Active</label>
                            <div class="form-group">
                                <select title="Enable the automation..." name="is_automation" id="id_edt_automation_active" class="form-control show-tick">
                                    <option value="0">Off</option>
                                    <option value="1">On</option>
                                </select>
                            </div>
                            <label for="">Automation List</label>
                            <div class="form-group">
                                <select title="Enable the automation list..." name="automation_id" id="id_edt_automation" class="form-control show-tick"></select>
                            </div>
                            <?php else: ?>
                            <?php endif; ?>
                            <button type="button" style="display: none;" id="id_btn_edt_submit" class="btn btn-primary m-t-15 waves-effect" onclick="submitFrmUpdate()">Update</button>
                        </form>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="room-advanced-edit">
                        <form id="frm_adv_update">
                            <div class="row clearfix">
                                <div class="col-xs-6">
                                    <div class="row clearfix">
                                        <div class="col-xs-6">
                                            <div class="row">
                                                <div class="col-xs-8 align-left"><label for="id_edt_adv_is_config_setting_enable">Enable Advanced Setting <a data-toggle="tooltip" data-placement="bottom" title=" This feature will activate several more features in a room
                                                            "> (?) </a></label></div>
                                                <div class="col-xs-4 align-right">
                                                    <div class="switch">
                                                        <label><input id="id_edt_adv_is_config_setting_enable" name="is_config_setting_enable" type="checkbox"><span class="lever switch-col-red"></span></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <label for="">Room For Usage <a data-toggle="tooltip" data-placement="bottom" title=" 
                                                            "> (?) </a></label>
                                            <div class="form-group">
                                                <select data-size="5" onchange="ocEdtRoomForUsage()" title="Choose the usage..." data-selected-text-format="count > 3" data-actions-box="true" disabled name="config_room_for_usage[]" id="id_edt_adv_room_for_usage" class="form-control show-tick" multiple>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-xs-12">
                                            <label for="">List Room for Usage <a data-toggle="tooltip" data-placement="bottom" title=" This room can be limited to a minimum booking time
                                                            "> (?) </a>
                                        </div>
                                        <div class="col-xs-12" style="max-height: 200px; overflow-y: scroll;">
                                            <table class="table table-striped" id="id_edt_adv_tbl_room_for_usage">
                                                <thead>
                                                    <th>No.</th>
                                                    <th>Usage</th>
                                                    <th>Minimum Capacity</th>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-xs-6">
                                            <label for="">Minimum duration of a meeting <a data-toggle="tooltip" data-placement="bottom" title=" This room can be limited to a minimum booking time
                                                            "> (?) </a>
                                            </label>
                                            <div class="form-group">
                                                <select title="Choose the time..." disabled data-size="4" name="config_min_duration" id="id_edt_adv_config_min_duration" class="form-control show-tick">
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <label for="">Maximum duration of a meeting <a data-toggle="tooltip" data-placement="bottom" title="This room can be limited to a maximum booking time
                                                            "> (?) </a>
                                            </label>
                                            <div class="form-group">
                                                <select title="Choose the time..." disabled data-size="4" name="config_max_duration" id="id_edt_adv_config_max_duration" class="form-control show-tick">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="row clearfix">
                                        <div class="col-xs-6">
                                            <label for="">Advance Booknig Date <a data-toggle="tooltip" data-placement="bottom" title="This feature can open the date selection to several days in the future
                                                            "> (?) </a>
                                            </label>
                                            <div class="form-group">
                                                <select data-size="4" title="Choose the days..." disabled name="config_advance_booking" id="id_edt_adv_config_advance_booking" class="form-control show-tick">
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="row">
                                                <div class="col-xs-8 align-left"><label for="id_edt_adv_is_enable_recurring">Allow Recurring</label></div>
                                                <div class="col-xs-4 align-right">
                                                    <div class="switch">
                                                        <label><input disabled id="id_edt_adv_is_enable_recurring" name="is_enable_recurring" type="checkbox"><span class="lever switch-col-red"></span></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-xs-12">
                                            <div class="row">
                                                <div class="col-xs-8 align-left"><label for="id_edt_adv_is_enable_approval">Needs Room Approval <a data-toggle="tooltip" data-placement="bottom" title=" If this feature is activated then the room will need approval to book the room
                                                            "> (?) </a></label></div>
                                                <div class="col-xs-4 align-right">
                                                    <div class="switch">
                                                        <label><input id="id_edt_adv_is_enable_approval" name="is_enable_approval" type="checkbox"><span class="lever switch-col-red"></span></label>
                                                    </div>
                                                </div>
                                                <br>
                                                <select title="Choose the approval user..." data-selected-text-format="count > 3" data-actions-box="true" data-live-search="true" data-size="4" disabled name="config_approval_user[]" id="id_edt_adv_config_approval_user" class="form-control show-tick" multiple>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <br>
                                    <div class="row clearfix">
                                        <div class="col-xs-12">
                                            <div class="row">
                                                <div class="col-xs-8 align-left"><label for="id_edt_adv_is_enable_permission">Private Meeting Room <a data-toggle="tooltip" data-placement="bottom" title=" If this feature is activated, the room will become private and only a few people will be given the right to book the room
                                                            "> (?) </a></label></div>
                                                <div class="col-xs-4 align-right">
                                                    <div class="switch">
                                                        <label><input id="id_edt_adv_is_enable_permission" name="is_enable_permission" type="checkbox"><span class="lever switch-col-red"></span></label>
                                                    </div>
                                                </div>
                                                <br>
                                                <div class="form-group">
                                                    <select title="Choose the permission user..." data-selected-text-format="count > 3" data-size="4" data-actions-box="true" data-live-search="true" disabled name="config_permission_user[]" id="id_edt_adv_config_permission_user" class="form-control show-tick" multiple>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br>
                            <br>
                            <div class="row">
                            </div>
                            <div class="row">
                            </div>
                            <!-- <button type="submit" style="display: none;" id="id_btn_edt_adv_submit" class="btn btn-primary m-t-15 waves-effect">Update</button> -->
                        </form>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="room-checkin-edit">
                        <form id="frm_adv_checkin_update">
                            <div class="row clearfix">
                                <div class="col-xs-6">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="row">
                                                <div class="col-xs-8 align-left"><label for="id_edt_adv_is_enable_checkin">Needs Room Check IN <a data-toggle="tooltip" data-placement="bottom" title=" If this feature is activated, the room that has been booked will need to be checked in after the room is active
                                                            "> (?) </a></label></div>
                                                <div class="col-xs-4 align-right">
                                                    <div class="switch">
                                                        <label><input id="id_edt_adv_is_enable_checkin" name="is_enable_checkin" type="checkbox"><span class="lever switch-col-red"></span></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <label for="">Permission to Check in</label>
                                            <div class="form-group">
                                                <select title="Choose the permission..." disabled name="config_permission_checkin" id="id_edt_adv_config_permission_checkin" class="form-control show-tick">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <label for="">Permission to End Meeting</label>
                                            <div class="form-group">
                                                <select title="Choose the permission..." name="config_permission_end" id="id_edt_adv_config_permission_end" class="form-control show-tick">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="row">
                                                <div class="col-xs-4 align-left"><label for="id_edt_adv_is_realease_checkin_timeout">Release a Meeting Room <a data-toggle="tooltip" data-placement="bottom" title=" If this feature is activated, rooms that have the check-in feature activated will be able to be released automatically if the room is not checked in within the specified time
                                                            "> (?) </a></label></div>
                                                <div class="col-xs-2 align-right">
                                                    <div class="switch">
                                                        <label><input id="id_edt_adv_is_realease_checkin_timeout" name="is_realease_checkin_timeout" type="checkbox"><span class="lever switch-col-red"></span></label>
                                                    </div>
                                                </div>
                                                <div class="col-xs-6 align-right">
                                                    <div class="form-group">
                                                        <select data-size="5" title="Choose the time..." disabled name="config_release_room_checkin_timeout" id="id_edt_adv_config_release_room_checkin_timeout" class="form-control show-tick">
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row clearfix">
                    <div class="col-xs-6 align-left">
                        <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                    </div>
                    <div class="col-xs-6">
                        <button id="id_btn_mdl_submit" onclick="submitFrmUpdate()" type="button" class="btn btn-primary waves-effect ">SAVE</button>
                    </div>
                </div>
                <!-- <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button> -->
            </div>
        </div>
    </div>
</div>