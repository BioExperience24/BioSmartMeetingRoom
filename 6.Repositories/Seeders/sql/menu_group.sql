USE [smart_meeting_room]
GO

TRUNCATE TABLE [smart_meeting_room].[menu_group];
DBCC CHECKIDENT ('[smart_meeting_room].[menu_group]', RESEED, 1);

-- SET IDENTITY_INSERT [smart_meeting_room].[menu_group] ON 

INSERT INTO [smart_meeting_room].[menu_group] ([name], [icon]) 
VALUES 
    (N'Master/Base', N'storage'),
    (N'Meeting Room', N'input'),
    (N'Setting', N'lock'),
    (N'null', N''),
    (N'Report', N'description'),
    (N'Invoice', N'description'),
    (N'Tracking BLE', N'description'),
    (N'Desk Booking', N'event'),
    (N'Location', N'my_location'),
    (N'Snack & Pantry', N'local_cafe'),
    (N'User Management', N'contacts'),
    (N'Approval', N'done_all'),
    (N'Help', N'help_outline');

-- SET IDENTITY_INSERT [smart_meeting_room].[menu_group] OFF
GO
