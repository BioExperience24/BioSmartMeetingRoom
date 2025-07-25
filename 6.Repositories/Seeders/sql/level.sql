USE [smart_meeting_room]
GO
SET IDENTITY_INSERT [smart_meeting_room].[level] ON 

INSERT INTO [smart_meeting_room].[level] ([id], [name], [default_menu], [created_by], [created_at], [updated_at], [is_deleted], [sort_level]) 
VALUES 
    (1, N'Administrator', 7, NULL, CAST(N'2019-09-19T15:23:17.000' AS DateTime), CAST(N'2019-09-19T15:23:17.000' AS DateTime), 0, 2),
    (2, N'Employee', 7, NULL, CAST(N'2019-09-19T18:38:57.000' AS DateTime), CAST(N'2019-09-19T18:38:58.000' AS DateTime), 0, 3),
    (3, N'Employee Old', 7, NULL, CAST(N'2019-09-19T18:38:57.000' AS DateTime), CAST(N'2019-09-19T18:38:57.000' AS DateTime), 1, NULL),
    (4, N'Pantry Display', 7, NULL, CAST(N'2019-09-19T18:38:57.000' AS DateTime), CAST(N'2019-09-19T18:38:57.000' AS DateTime), 1, 4),
    (5, N'Pantry Operator', 7, NULL, CAST(N'2019-09-19T18:38:57.000' AS DateTime), CAST(N'2019-09-19T18:38:57.000' AS DateTime), 1, 5),
    (6, N'SO / Helpdesk', 7, NULL, CAST(N'2019-09-19T18:38:57.000' AS DateTime), CAST(N'2019-09-19T18:38:57.000' AS DateTime), 0, 6),
    (7, N'Super Admin', 0, NULL, NULL, NULL, 0, 0);

SET IDENTITY_INSERT [smart_meeting_room].[level] OFF
GO
