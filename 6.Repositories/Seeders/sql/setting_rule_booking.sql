USE [smart_meeting_room]
GO

TRUNCATE TABLE [smart_meeting_room].[setting_rule_booking];
DBCC CHECKIDENT ('[smart_meeting_room].[setting_rule_booking]', RESEED, 1);

-- SET IDENTITY_INSERT [smart_meeting_room].[setting_rule_booking] ON 

INSERT INTO [smart_meeting_room].[setting_rule_booking] ([duration], [if_unused_room], [max_end_meeting], [notif_unused_meeting], [notif_unuse_before_meeting], [unuse_cancel_fee], [max_display_duration], [room_pin], [room_pin_number], [room_pin_refresh], [extend_meeting], [extend_meeting_max], [extend_count_time], [extend_meeting_notification], [end_early_meeting], [limit_time_booking], [created_by], [updated_by], [updated_at], [is_config_setting_enable], [config_room_for_usage], [is_enable_approval], [config_approval_user], [is_enable_permission], [config_permission_user], [config_permission_checkin], [config_permission_end], [config_min_duration], [config_max_duration], [config_advance_booking], [is_enable_recurring], [is_enable_checkin], [config_advance_checkin], [is_realease_checkin_timeout], [config_release_room_checkin_timeout], [config_participant_checkin_count], [is_enable_checkin_count], [is_deleted]) VALUES (30, 0, NULL, 30, 5, 30, 20, 1, N'123456', NULL, 1, 30, 30, 10, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

-- SET IDENTITY_INSERT [smart_meeting_room].[setting_rule_booking] OFF
GO
