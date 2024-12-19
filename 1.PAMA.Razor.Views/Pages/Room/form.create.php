<div class="modal fade" id="id_mdl_create" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="idmdlcrLabel">Create Room </h4>
            </div>
            <div class="modal-body " id="id_mdl_create_body">
                <ul class="nav nav-tabs tab-nav-right" role="tablist">
                    <li role="presentation" class="active"><a href="#room-general-create" data-toggle="tab" onclick="actionBtnEdit('id_btn_crt_submit')">General</a></li>
                    <?php if ($modules['room_adv']['is_enabled'] == 1): ?>
                    <li role="presentation"><a href="#room-advanced-create" data-toggle="tab">Advanced Setting</a></li>
                    <li role="presentation" id="id_panel_checkin" style="display:none;"><a href="#room-checkin-create" data-toggle="tab">Check In Setting</a></li>
                    <?php endif ?>
                </ul>
                <br>
                <div class="tab-content">
                    <div role="tabpanel" class="tab-pane fade in active" id="room-general-create">
                        <form id="frm_create">
                            <div class="row clearfix">
                                <div class="col-xs-7">
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label for="">Name <b style="color:red;">*</b></label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input type="text" autocomplete="off" name="name" id="id_crt_name" required="" class="form-control" placeholder="Name">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <label for="">Description</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <textarea rows="1" name="description" class="form-control no-resize" name="description" id="id_crt_description" placeholder="Please type the room description..."></textarea>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label for="">Building <b style="color:red;">*</b></label>
                                            <div class="form-group">
                                                <select title="Choose the building..." control name="building_id" id="id_crt_building_id" class="form-control show-tick"></select>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <label for="">Floor <b style="color:red;">*</b></label>
                                            <div class="form-group">
                                                <select title="Choose the floor..." required name="floor_id" id="id_crt_floor_id" class="form-control show-tick"></select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label for="">Type Room</label>
                                            <div class="form-group">
                                                <select title="Choose the type..." onchange="initTypeRoom('','id_crt_type_room','id_crt_merge_room','')" name="type_room" id="id_crt_type_room" class="form-control show-tick">
                                                    <option value="single">Single Room</option>
                                                    <option value="merge">Merge Room</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-xs-6" id="id_area_add_merge">
                                            <label for="">Merger Room</label>
                                            <div class="form-group">
                                                <select title="Choose the any room..." name="merge_room[]" id="id_crt_merge_room" class="form-control show-tick" multiple data-actions-box="true">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-2">
                                            <label for="">Capacity <b style="color:red;">*</b></label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input type="number" name="capacity" id="id_crt_capacity" required="" class="form-control" placeholder="Capacity">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-4" id="id_area_add_merge">
                                            <label for="">Working Day</label>
                                            <div class="form-group">
                                                <select title="Choose the day..." multiple name="work_day[]" data-dropup-auto="false" data-selected-text-format="count > 2" data-actions-box="true" id="id_crt_workday" data-size="7" class="form-control show-tick">
                                                    <option value="SUNDAY">SUNDAY</option>
                                                    <option value="MONDAY">MONDAY</option>
                                                    <option value="TUESDAY">TUESDAY</option>
                                                    <option value="WEDNESDAY">WEDNESDAY</option>
                                                    <option value="THURSDAY">THURSDAY</option>
                                                    <option value="FRIDAY">FRIDAY</option>
                                                    <option value="SATURDAY">SATURDAY</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <label for="">Working Time <b style="color:red;">*</b></label>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6 col-xs-6 col-lg-6 col-sm-6">
                                                        <div class="form-line">
                                                            <input type="text" name="work_start" id="id_crt_work_start" class="timepicker form-control" required="" placeholder="Please choose a start time...">
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 col-xs-6 col-lg-6 col-sm-6">
                                                        <div class="form-line">
                                                            <input type="text" name="work_end" id="id_crt_work_end" class="timepicker form-control" required="" placeholder="Please choose a finish time...">
                                                        </div>
                                                    </div>
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
                                                <select title="Choose the facility..." data-size="5" data-live-search="true" multiple name="facility_room[]" id="id_crt_facility_room" class="form-control show-tick" data-actions-box="true"></select>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <label for="">Status</label>
                                            <div class="form-group">
                                                <select title="Choose the status..." name="is_disabled" id="id_crt_is_disabled" class="form-control show-tick">
                                                    <option value="0">Enabled</option>
                                                    <option value="1">Disabled/Maintenance</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <label for="">Detail Location</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <textarea rows="4" name="location" class="form-control no-resize" id="id_crt_location" placeholder="Please type the room location..."></textarea>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <label for="">Link Map</label>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <input type="text" autocomplete="off" name="google_map" id="id_edt_google_map" class="form-control" placeholder="Google Map">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <label for="">Thumbnail <b style="color:red;">*</b></label>
                                            <div class="form-group">
                                                <input type="file" name="image" id="input-file-to-destroy" class="dropify" data-height="120" data-max-file-size="4M" accept="image/*" />
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <label for="">Image 1 </label>
                                            <div class="form-group">
                                                <input type="file" name="image2[]" class="dropify" data-height="120" data-max-file-size="4M" accept="image/*" />
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <label for="">Image 2 </label>
                                            <div class="form-group">
                                                <input type="file" name="image2[]" class="dropify" data-height="120" data-max-file-size="4M" accept="image/*" />
                                            </div>
                                        </div>
                                        <div class="col-xs-3">
                                            <label for="">Image 3 </label>
                                            <div class="form-group">
                                                <input type="file" name="image2[]" class="dropify" data-height="120" data-max-file-size="4M" accept="image/*" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <?php if ($modules['automation']['is_enabled'] == 1): ?>
                            <label for="">Automation Active</label>
                            <div class="form-group">
                                <select title="Enable the Automation..." name="is_automation" id="id_crt_automation_active" class="form-control show-tick">
                                    <option value="0">Off</option>
                                    <option value="1">On</option>
                                </select>
                            </div>
                            <label for="">Automation List</label>
                            <div class="form-group">
                                <select title="Choose the Automations List..." name="automation_id" id="id_crt_automation" class="form-control show-tick"></select>
                            </div>
                            <?php else: ?>
                            <?php endif; ?>
                            <?php if ($modules['price']['is_enabled'] == 1): ?>
                            <!-- <label for="">Price of the room per room<b style="color:red;">*</b></label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input  type="number" name="price" id="id_crt_price"  class=" form-control" required="" placeholder="Price Room ...">
                                    </div>
                                    
                                </div> -->
                            <?php else: ?>
                            <?php endif; ?>
                            <!--  <label for="">Room Access Controll</label>
                                <div class="form-group">
                                    <div class="form-line">
                                        <input autocomplete="off" type="text" name="access_id" id="id_crt_access_id" required=""  class="form-control" placeholder="Access Number Ex. 1 ">
                                    </div>
                                </div> -->
                            <br>
                            <button type="submit" style="display: none;" id="id_btn_crt_submit" class="btn btn-primary m-t-15 waves-effect">Save</button>
                        </form>
                    </div>
                    <div role="tabpanel" class="tab-pane fade in active" id="room-advanced-create">
                    </div>
                    <div role="tabpanel" class="tab-pane fade in active" id="room-checkin-create">
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row clearfix">
                    <div class="col-xs-6 align-left">
                        <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                    </div>
                    <div class="col-xs-6">
                        <button onclick="clickSubmit('id_btn_crt_submit')" type="button" class="btn btn-primary waves-effect ">SAVE</button>
                    </div>
                </div>
                <!-- <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button> -->
            </div>
        </div>
    </div>
</div>