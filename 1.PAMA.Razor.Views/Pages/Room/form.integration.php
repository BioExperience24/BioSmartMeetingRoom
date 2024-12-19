<div class="modal fade" id="id_mdl_integration" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="id_mdl_integrationLabel"></h4>
                </div>
                <div class="modal-body ">
                    <form id="frm_integration">
                        <label for="">Room Integration</label>
                        <div class="form-group">
                            <input type="hidden" name="id" id="id_int_id">
                            <input type="hidden" name="type" id="id_int_type">
                            <input type="hidden" name="roomid" id="id_int_room_meeting">
                            <select name="room_int_id" id="id_int_room" class="form-control show-tick"></select>
                        </div>
                        <div class="row">
                            <div class="col-xs-6">
                                <label for="">Remove Integration</label>
                            </div>
                            <div class="col-xs-6 align-right">
                                <button type="button" onclick="removeIntegrationRoom()" class="btn btn-danger waves-effect">Remove</button>
                            </div>
                        </div>
                        <br>
                        <button type="submit" style="display: none;" id="id_btn_int_submit" class="btn btn-primary m-t-15 waves-effect">Save</button>
                    </form>
                </div>
                <div class="modal-footer">
                    <div class="row clearfix">
                        <div class="col-xs-6 align-left">
                            <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                        </div>
                        <div class="col-xs-6">
                            <button onclick="clickSubmit('id_btn_int_submit')" type="button" class="btn btn-primary waves-effect ">SAVE</button>
                        </div>
                    </div>
                    <!-- <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button> -->
                </div>
            </div>
        </div>
    </div>