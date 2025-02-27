ALTER TABLE "smart_meeting_room"."level"
	ADD "sort_level" INT NULL;

-- jika data sebelumnya sudah ada
TRUNCATE TABLE smart_meeting_room."level";
DBCC CHECKIDENT ('smart_meeting_room.level', RESEED, 1);

INSERT INTO "smart_meeting_room"."level" ("name", "default_menu", "sort_level", "created_by", "created_at", "updated_at", "is_deleted")
VALUES ( 'Administrator', 7, 2, NULL, '2019-09-19 15:23:17', '2019-09-19 15:23:17', 0),
    ( 'Employee', 7, 3, NULL, '2019-09-19 18:38:57', '2019-09-19 18:38:58', 0),
    ( 'Employee Old', 7, NULL, NULL, '2019-09-19 18:38:57', '2019-09-19 18:38:57', 1),
    ( 'Pantry Display', 7, 4, NULL, '2019-09-19 18:38:57', '2019-09-19 18:38:57', 1),
    ( 'Pantry Operator', 7, 5, NULL, '2019-09-19 18:38:57', '2019-09-19 18:38:57', 1),
    ( 'SO / Helpdesk', 7, 6, NULL, '2019-09-19 18:38:57', '2019-09-19 18:38:57', 0),
    ( 'Super Admin', 7, 1, NULL, NULL, NULL, 0);


-- jika data sebelumnya sudah ada
TRUNCATE TABLE smart_meeting_room."level_descriptiion";
DBCC CHECKIDENT ('smart_meeting_room.level_descriptiion', RESEED, 1);

INSERT INTO [smart_meeting_room].[level_descriptiion] ([level_id], [description], [is_deleted]) VALUES (1, '{"cms":{"name":"Manage CMS","desc":"Allow user to sign in to your CMS using their username and access the following..","detail":["Manage company","Manage employee","Manage room","Manage booking","Manage report","Manage pantry","Manage user"]}}', 0);
INSERT INTO [smart_meeting_room].[level_descriptiion] ([level_id], [description], [is_deleted]) VALUES (2, '{"cms":{"name":"Meeting Webs","desc":"Allow user to sign in to your web using their username, nik and access the following.","detail":["Book a meeting","Invite a partisipant","Room Check","Order Snack"]},"apps":{"name":"Mobile Apps","desc":"Allow user to sign in to your web using their username, nik and access the following.","detail":["Book a meeting","Invite a partisipant","Get notification","Room check","Order snack"]}}', 0);
INSERT INTO [smart_meeting_room].[level_descriptiion] ([level_id], [description], [is_deleted]) VALUES (3, '{"door_access":{"name":"Door Access","desc":"Allows the user to open every door with access.","detail":["Open a door"]}}', 0);
INSERT INTO [smart_meeting_room].[level_descriptiion] ([level_id], [description], [is_deleted]) VALUES (4, '{"pantry":{"name":"Manage Pantry","desc":"Allow user to sign in to your CMS using their username and access the following..","detail":["Manage pantry","Manage pantry in screen"]}}', 0);
INSERT INTO [smart_meeting_room].[level_descriptiion] ([level_id], [description], [is_deleted]) VALUES (6, '{"cms":{"name":"Manage CMS","desc":"Allow user to sign in to your CMS using their username and access the following..","detail":["Manage meeting","Approval snack","Approval meeting"]}}', 0);
INSERT INTO [smart_meeting_room].[level_descriptiion] ([level_id], [description], [is_deleted]) VALUES (7, '{"cms":{"name":"Manage User Admin","desc":"Allow user to sign in to your CMS using their username and access the following..","detail":["Manage user admin"]}}', 0);

-- jika data sebelumnya sudah ada
TRUNCATE TABLE smart_meeting_room."level_detail";
DBCC CHECKIDENT ('smart_meeting_room.level_detail', RESEED, 1);

INSERT INTO [smart_meeting_room].[level_detail] ([level_id], [menu_id], [coment]) 
VALUES
    (1, 1, 'ADMIN'),
    (1, 2, 'ADMIN'),
    (1, 3, 'ADMIN'),
    (1, 4, 'ADMIN'),
    (1, 5, 'ADMIN'),
    (1, 6, 'ADMIN'),
    (1, 7, 'ADMIN'),
    (1, 8, 'ADMIN'),
    (1, 9, 'ADMIN'),
    (1, 10, 'ADMIN'),
    (1, 11, 'ADMIN'),
    (1, 12, 'ADMIN'),
    (1, 13, 'ADMIN'),
    (1, 14, 'ADMIN'),
    (1, 15, 'ADMIN'),
    (1, 16, 'ADMIN'),
    (1, 17, 'ADMIN'),
    (1, 18, 'ADMIN'),
    (1, 19, 'ADMIN'),
    (1, 20, 'ADMIN'),
    (1, 21, 'ADMIN'),
    (1, 22, 'ADMIN'),
    (1, 23, 'ADMIN'),
    (2, 12, 'user'),
    (2, 7, 'user'),
    (2, 2, 'ADMIN'),
    (1, 24, 'ADMIN'),
    (1, 25, 'ADMIN'),
    (1, 26, 'ADMIN'),
    (1, 27, 'ADMIN'),
    (1, 28, 'ADMIN'),
    (1, 29, 'ADMIN'),
    (1, 30, 'ADMIN'),
    (1, 32, 'ADMIN'),
    (1, 33, 'ADMIN'),
    (1, 34, 'ADMIN'),
    (1, 35, 'ADMIN'),
    (1, 36, 'ADMIN'),
    (1, 37, 'ADMIN'),
    (1, 38, 'ADMIN'),
    (1, 39, 'ADMIN'),
    (2, 34, 'USER'),
    (7, 1, 'SUPER'),
    (6, 40, 'SO'),
    (6, 41, 'SO'),
    (2, 24, 'USER'),
    (6, 2, 'SO'),
    (6, 12, 'SO'),
    (1, 40, 'ADMIN'),
    (1, 41, 'ADMIN'),
    (1, 42, 'ADMIN'),
    (1, 43, 'ADMIN');


-- jika data sebelumnya sudah ada
TRUNCATE TABLE smart_meeting_room."menu";
DBCC CHECKIDENT ('smart_meeting_room.menu', RESEED, 1);

INSERT INTO [smart_meeting_room].[menu] ([name], [url], [type_icon], [icon], [sort], [is_child], [menu_group_id], [module_text], [created_by], [created_at], [updated_at], [is_deleted]) VALUES 
('User', '/user', 'awesome', 'person', 22, 1, 11, 'module_user', NULL, '2023-07-01 00:55:54', '2023-07-01 00:55:54', 0),
('Room Schedule', '/booking', 'awesome', 'schedule', 5, 1, 2, 'module_meeting', NULL, '2024-12-24 10:42:56', '2024-12-24 10:42:56', 0),
('Room Management', '/room', 'awesome', 'apps', 51, 1, 2, 'module_room', NULL, '2024-12-24 10:42:51', '2024-12-24 10:42:51', 0),
('Master Snack & Pantry', '/pantry', 'awesome', 'room_service', 7, 1, 10, 'module_pantry', NULL, '2024-12-24 10:43:30', '2024-12-24 10:43:30', 0),
('Facility', '/facility', 'awesome', 'home', 3, 1, 1, 'module_room', NULL, '2024-12-20 01:17:23', '2024-12-20 01:17:23', 0),
('Attendance', '/attendance', 'awesome', 'people', 100, 1, 1, '', NULL, '2024-01-05 03:27:43', '2024-01-05 03:27:43', 1),
('Dashboard', '/dashboard', 'awesome', 'home', 0, 0, 1, '', NULL, '2023-07-01 00:44:22', '2023-07-01 00:44:22', 0),
('Automation', '/automation', 'awsome', 'home', 4, 1, 1, 'module_automation', NULL, '2023-02-02 04:45:31', '2023-02-02 04:45:31', 1),
('Multimedia', '/Multimedia', 'awesome', 'tv', 9, 1, 1, '', NULL, '2023-02-02 04:45:31', '2023-02-02 04:45:31', 1),
('Employee', '/employee', 'awesome', 'person', 21, 1, 11, 'module_user', NULL, '2023-07-01 00:55:49', '2023-07-01 00:55:49', 0),
('Information', '/company', 'awesome', 'card_membership', 1, 0, 2, 'module_informasi', NULL, '2023-07-01 00:44:36', '2023-07-01 00:44:36', 0),
('Room Usage', '/report-usage', 'awesome', 'description', 52, 1, 2, 'module_report', NULL, '2024-12-24 10:42:58', '2024-12-24 10:42:58', 0),
('Invoice', '/invoice', 'awesome', 'description', 10, 0, 4, '', NULL, '2023-02-02 04:45:31', '2023-02-02 04:45:31', 1),
('Department', '/department', 'awesome', 'apps', 2, 1, 11, '', NULL, '2023-07-01 00:48:13', '2023-07-01 00:48:13', 1),
('Building', '/building', 'awesome', 'room_service', 4, 1, 9, 'module_building', NULL, '2023-07-01 01:00:39', '2023-07-01 01:00:39', 0),
('Access Door', '/access', 'awsome', 'door', 3, 1, 1, 'module_access_door', NULL, '2024-12-20 01:13:03', '2024-12-20 01:13:03', 1),
('Company/Department', '/alocation', 'awesome', 'apps', 2, 1, 11, 'module_alocation', NULL, '2023-10-09 13:24:27', '2023-10-09 13:24:27', 0),
('General', '/setting/general', 'awesome', 'apps', 65, 1, 3, 'module_meeting', NULL, '2025-02-04 15:35:54', '2025-02-04 15:35:54', 0),
('SMTP & Email', '/setting/smtp-email', 'awesome', 'apps', 66, 1, 3, 'module_meeting', NULL, '2025-02-04 15:35:57', '2025-02-04 15:35:57', 0),
('Display Signage', '/display', 'awesome', 'apps', 31, 1, 1, 'module_display', NULL, '2025-01-14 16:11:09', '2025-01-14 16:11:09', 0),
('Cancel Order', '/report-cancel-order', 'awesome', 'description', 10, 1, 5, '', NULL, '2023-10-09 12:24:10', '2023-10-09 12:24:10', 1),
('Income Rent Room', '/report-income', 'awesome', 'description', 11, 1, 5, '', NULL, '2023-10-09 12:23:49', '2023-10-09 12:23:49', 1),
('Outstanding Invoice', '/report-outstanding', 'awesome', 'description', 12, 1, 5, '', NULL, '2023-10-09 12:23:53', '2023-10-09 12:23:53', 1),
('Order Management', '/pantry-transaction', 'awsome', 'schedule', 75, 1, 10, 'module_pantry', NULL, '2024-12-24 10:43:27', '2024-12-24 10:43:27', 0),
('Menu Package', '/pantry-package', 'awesome', 'room_service', 74, 1, 10, 'module_pantry', NULL, '2024-12-24 10:43:17', '2024-12-24 10:43:17', 0),
('Locker System', '/locker-system', 'awesome', 'lock', 7, 1, 1, 'module_loker', NULL, '2023-07-01 00:50:29', '2023-07-01 00:50:29', 1),
('Display Kiosk', '/display-kiosk', 'awesome', 'apps', 32, 1, 1, 'module_kiosk', NULL, '2024-12-20 01:16:36', '2024-12-20 01:16:36', 1),
('Floor Management', '/beacon-floor', 'awesome', 'apps', 41, 1, 9, 'module_floor', NULL, '2024-12-20 01:18:17', '2024-12-20 01:18:17', 1),
('Floor Room', '/beacon-floor-room', 'awesome', 'apps', 5, 1, 7, 'module_beacon', NULL, '2023-06-11 02:18:11', '2023-06-11 02:18:11', 1),
('Beacon Tag', '/beacon-tag', 'awesome', 'apps', 82, 1, 7, 'module_beacon', NULL, '2023-10-09 13:27:02', '2023-10-09 13:27:02', 1),
('Desk Room', '/deskroom', 'awesome', 'apps', 6, 1, 8, 'module_desk', NULL, '2024-12-24 10:44:11', '2024-12-24 10:44:11', 1),
('Desk Controller', '/deskcontroller', 'awesome', 'apps', 62, 1, 8, 'module_desk', NULL, '2024-12-24 10:44:09', '2024-12-24 10:44:09', 1),
('Desk Transaction', '/desktrs', 'awesome', 'apps', 61, 1, 8, 'module_desk', NULL, '2024-12-24 10:44:13', '2024-12-24 10:44:13', 1),
('Live Transaction', '/beacon-live-monitor', 'awesome', 'apps', 12, 1, 7, 'module_beacon', NULL, '2023-10-09 13:26:59', '2023-10-09 13:26:59', 1),
('Beacon Gateway', '/beacon-gateway', 'awesome', 'apps', 81, 1, 7, 'module_beacon', NULL, '2023-10-09 13:27:04', '2023-10-09 13:27:04', 1),
('License', '/setting/license', 'awesome', 'description', 67, 1, 3, 'module_license', NULL, '2025-02-04 15:36:05', '2025-02-04 15:36:05', 0),
('Integration', '/integration', 'awesome', 'open_with', 55, 0, 1, 'module_integration', NULL, '2025-02-04 15:36:25', '2025-02-04 15:36:25', 0),
('Menu Order', '/pantry-menu', 'awesome', 'open_with', 71, 1, 10, 'module_pantry', NULL, '2024-12-24 10:43:12', '2024-12-24 10:43:12', 0),
('Approval Meeting', '/approval-meeting', 'awesome', 'open_with', 41, 1, 12, 'module_meeting', NULL, '2025-02-04 15:33:21', '2025-02-04 15:33:21', 0),
('Approval  Order', '/approval-order', 'awesome', 'open_with', 42, 1, 12, 'module_pantry', NULL, '2025-02-04 15:33:24', '2025-02-04 15:33:24', 0),
('Help IT', '/help-id', 'awesome', 'apps', 53, 1, 13, 'module_meeting', NULL, '2025-02-04 15:34:48', '2025-02-04 15:34:48', 0),
('Help GS', '/help-gs', 'awesome', 'apps', 54, 1, 13, 'module_meeting', NULL, '2025-02-04 15:34:52', '2025-02-04 15:34:52', 0);


-- jika data sebelumnya sudah ada
TRUNCATE TABLE smart_meeting_room."menu_apps";
DBCC CHECKIDENT ('smart_meeting_room.menu_apps', RESEED, 1);

INSERT INTO [smart_meeting_room].[menu_apps] ([name], [url], [type_icon], [icon], [sort], [is_child], [menu_group_id], [module_text], [created_by], [created_at], [updated_at], [is_deleted]) VALUES 
('Meeting', 'meeting', '', 'meeting.png', 1, 0, 0, 'module_meeting', NULL, NULL, NULL, 1),
('Desk', 'desk', '', 'desk.png', 2, 0, 0, 'module_desk', NULL, NULL, NULL, 0),
('Report', 'report', '', 'report.png', 4, 0, 0, 'module_report', NULL, NULL, NULL, 1),
('Calender', 'calendar', '', 'calendar.png', 5, 0, 0, 'module_calender', NULL, NULL, NULL, 1),
('Pantry', 'pantry', '', 'pantry.png', 6, 0, 0, 'module_pantry', NULL, NULL, NULL, 1),
('Approval', 'approval', '', 'approval.png', 7, 0, 0, 'module_room_advance', NULL, NULL, NULL, 1),
('Website', 'website', '', 'website.png', 8, 0, 0, 'module_meeting', NULL, NULL, NULL, 1);


-- jika data sebelumnya sudah ada
TRUNCATE TABLE smart_meeting_room."menu_group";
DBCC CHECKIDENT ('smart_meeting_room.menu_group', RESEED, 1);

INSERT INTO [smart_meeting_room].[menu_group] ([name], [icon]) VALUES 
('Master/Base', 'storage'),
('Meeting Room', 'input'),
('Setting', 'lock'),
('null', ''),
('Report', 'description'),
('Invoice', 'description'),
('Tracking BLE', 'description'),
('Desk Booking', 'event'),
('Location', 'my_location'),
('Snack & Pantry', 'local_cafe'),
('User Management', 'contacts'),
('Approval', 'done_all'),
('Help', 'help_outline');