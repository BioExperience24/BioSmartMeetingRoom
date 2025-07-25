USE [smart_meeting_room]
GO

TRUNCATE TABLE [smart_meeting_room].[module_backend];
DBCC CHECKIDENT ('[smart_meeting_room].[module_backend]', RESEED, 1);

-- SET IDENTITY_INSERT [smart_meeting_room].[module_backend] ON 

INSERT INTO [smart_meeting_room].[module_backend] ([module_text], [name], [module_serial], [is_enabled]) 
VALUES 
    (N'module_core', N'Module Core', NULL, 1),
    (N'module_company', N'Module Company', NULL, 1),
    (N'module_department', N'Module Department', NULL, 0),
    (N'module_division', N'Module Division', NULL, 0),
    (N'module_facility', N'Module Facility', NULL, 1),
    (N'module_pantry', N'Module Pantry', NULL, 1),
    (N'module_invoice', N'Module Invoice', NULL, 1),
    (N'module_automation', N'Module Automation', NULL, 0),
    (N'module_access_door', N'Module Access Door', NULL, 1),
    (N'module_booking', N'Module booking', NULL, 1),
    (N'module_web', N'Module Web', NULL, 1),
    (N'module_mobile_android', N'Module Mobile Android', NULL, 1),
    (N'module_mobile_ios', N'Module Mobile IOS', NULL, 1),
    (N'module_email', N'Module Email', NULL, 1),
    (N'module_price', N'Module Price', NULL, 1),
    (N'module_alocation', N'Module Alocation', NULL, 1),
    (N'module_display', N'Module Display', NULL, 1),
    (N'module_loker', N'Module Loker', NULL, 1),
    (N'module_beacon', N'Module Beacon', NULL, 1),
    (N'module_kiosk', N'Module KIOSK', NULL, 1),
    (N'module_meeting', N'Module Meeting', NULL, 1),
    (N'module_room', N'Module Room', NULL, 1),
    (N'module_report', N'Module Report', NULL, 1),
    (N'module_desk', N'Module Desk', NULL, 1),
    (N'module_calender', N'Module Calender', NULL, 1),
    (N'module_building', N'Module Building', NULL, 1),
    (N'module_floor', N'Module Building', NULL, 1),
    (N'module_int_alarm', N'Module Integration Alarm', NULL, 1),
    (N'module_int_google', N'Module Integration Google', NULL, 0),
    (N'module_int_365', N'Module Integration 365', NULL, 1),
    (N'module_user_vip', N'Module Employee/User VIP', NULL, 1),
    (N'module_room_advance', N'Module Room', NULL, 1);

-- SET IDENTITY_INSERT [smart_meeting_room].[module_backend] OFF
GO
