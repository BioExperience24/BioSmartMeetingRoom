USE [smart_meeting_room]
GO

TRUNCATE TABLE [smart_meeting_room].[menu_apps];
DBCC CHECKIDENT ('[smart_meeting_room].[menu_apps]', RESEED, 1);

-- SET IDENTITY_INSERT [smart_meeting_room].[menu_apps] ON 

INSERT INTO [smart_meeting_room].[menu_apps] ([name], [url], [type_icon], [icon], [sort], [is_child], [menu_group_id], [module_text], [created_by], [created_at], [updated_at], [is_deleted]) 
VALUES 
    (N'Meeting', N'meeting', N'', N'meeting.png', 1, 0, 0, N'module_meeting', NULL, NULL, NULL, 1),
    (N'Desk', N'desk', N'', N'desk.png', 2, 0, 0, N'module_desk', NULL, NULL, NULL, 0),
    (N'Report', N'report', N'', N'report.png', 4, 0, 0, N'module_report', NULL, NULL, NULL, 1),
    (N'Calender', N'calendar', N'', N'calendar.png', 5, 0, 0, N'module_calender', NULL, NULL, NULL, 1),
    (N'Pantry', N'pantry', N'', N'pantry.png', 6, 0, 0, N'module_pantry', NULL, NULL, NULL, 1),
    (N'Approval', N'approval', N'', N'approval.png', 7, 0, 0, N'module_room_advance', NULL, NULL, NULL, 1),
    (N'Website', N'website', N'', N'website.png', 8, 0, 0, N'module_meeting', NULL, NULL, NULL, 1);

-- SET IDENTITY_INSERT [smart_meeting_room].[menu_apps] OFF
GO
