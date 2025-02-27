-- jika data sebelumnya sudah ada
TRUNCATE TABLE smart_meeting_room.module_backend;
-- DBCC CHECKIDENT ('smart_meeting_room.module_backend', RESEED, 1);

SET IDENTITY_INSERT smart_meeting_room.module_backend ON;

INSERT INTO smart_meeting_room.module_backend (module_id, module_text, name, module_serial, is_enabled) VALUES
(0, 'module_core', 'Module Core', NULL, 1),
(1, 'module_company', 'Module Company', NULL, 1),
(2, 'module_department', 'Module Department', NULL, 0),
(3, 'module_division', 'Module Division', NULL, 0),
(4, 'module_facility', 'Module Facility', NULL, 1),
(5, 'module_pantry', 'Module Pantry', NULL, 1),
(6, 'module_invoice', 'Module Invoice', NULL, 0),
(7, 'module_automation', 'Module Automation', NULL, 0),
(8, 'module_access_door', 'Module Access Door', NULL, 1),
(9, 'module_booking', 'Module booking', NULL, 1),
(10, 'module_web', 'Module Web', NULL, 1),
(11, 'module_mobile_android', 'Module Mobile Android', NULL, 1),
(12, 'module_mobile_ios', 'Module Mobile IOS', NULL, 1),
(13, 'module_email', 'Module Email', NULL, 1),
(14, 'module_price', 'Module Price', NULL, 1),
(15, 'module_alocation', 'Module Alocation', NULL, 1),
(16, 'module_display', 'Module Display', NULL, 1),
(17, 'module_loker', 'Module Loker', NULL, 0),
(18, 'module_beacon', 'Module Beacon', NULL, 1),
(19, 'module_kiosk', 'Module KIOSK', NULL, 1),
(20, 'module_meeting', 'Module Meeting', NULL, 1),
(21, 'module_room', 'Module Room', NULL, 1),
(22, 'module_report', 'Module Report', NULL, 1),
(23, 'module_desk', 'Module Desk', NULL, 1),
(24, 'module_calender', 'Module Calender', NULL, 1),
(26, 'module_building', 'Module Building', NULL, 1),
(27, 'module_floor', 'Module Building', NULL, 1),
(28, 'module_int_alarm', 'Module Integration Alarm', NULL, 1),
(29, 'module_int_google', 'Module Integration Google', NULL, 0),
(30, 'module_int_365', 'Module Integration 365', NULL, 1),
(31, 'module_user_vip', 'Module Employee/User VIP', NULL, 0),
(32, 'module_room_advance', 'Module Room', NULL, 1);

SET IDENTITY_INSERT smart_meeting_room.module_backend OFF;