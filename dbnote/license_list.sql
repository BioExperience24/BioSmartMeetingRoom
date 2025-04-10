-- jika data sebelumnya sudah ada
TRUNCATE TABLE smart_meeting_room.license_list;
DBCC CHECKIDENT ('smart_meeting_room.license_list', RESEED, 1);

SET IDENTITY_INSERT smart_meeting_room.license_list ON;

INSERT INTO smart_meeting_room.license_list (id, name, type, module, expired_at, is_lifetime, status, qty, platform_serial) VALUES 
(9, 'Smart Meeting Room', 'core', 'module_meeting', '9999-12-30', 1, '1', 1, 'cf7473a5-6192-4688-9051-1051a874c0ad'),
(10, 'Information', 'feature', 'module_informasi', '9999-12-30', 1, '1', 1, 'cf7473a5-6192-4688-9051-1051a874c0ad'),
(11, 'Room', 'feature', 'module_room', '9999-12-30', 1, '1', 3, 'cf7473a5-6192-4688-9051-1051a874c0ad'),
(12, 'User', 'feature', 'module_user', '9999-12-30', 1, '1', 1, 'cf7473a5-6192-4688-9051-1051a874c0ad'),
(13, 'Department & Division', 'feature', 'module_alocation', '9999-12-30', 1, '1', 1, 'cf7473a5-6192-4688-9051-1051a874c0ad'),
(14, 'Display Signage', 'feature', 'module_display', '9999-12-30', 1, '1', 15, 'cf7473a5-6192-4688-9051-1051a874c0ad'),
(15, 'Meeting Report', 'feature', 'module_report', '9999-12-30', 1, '1', 1, 'cf7473a5-6192-4688-9051-1051a874c0ad'),
(16, 'Meeting SMPT Notification', 'feature', 'module_smtp', '9999-12-30', 1, '1', 1, 'cf7473a5-6192-4688-9051-1051a874c0ad'),
(17, 'Integration', 'feature', 'module_integration', '9999-12-30', 1, '1', 1, 'cf7473a5-6192-4688-9051-1051a874c0ad'),
(18, 'Integration Google', 'addons', 'module_int_google', '9999-12-30', 1, '1', 1, 'cf7473a5-6192-4688-9051-1051a874c0ad'),
(19, 'Integration 365 Outlook', 'addons', 'module_int_365', '9999-12-30', 1, '1', 1, 'cf7473a5-6192-4688-9051-1051a874c0ad'),
(20, 'Integration Alarm ', 'addons', 'module_int_alarm', '9999-12-30', 1, '1', 1, 'cf7473a5-6192-4688-9051-1051a874c0ad');

SET IDENTITY_INSERT smart_meeting_room.license_list OFF;