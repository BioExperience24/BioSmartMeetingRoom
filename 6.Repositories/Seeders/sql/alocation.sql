USE [smart_meeting_room]
GO
SET IDENTITY_INSERT [smart_meeting_room].[alocation] ON 

INSERT INTO [smart_meeting_room].[alocation] 
([_generate], [id], [department_code], [name], [type], [invoice_type], [invoice_status], [created_by], [updated_by], [created_at], [updated_at], [is_permanent], [is_deleted], [show_in_invitation]) 
VALUES 
(1, N'10283', N'10283', N'Demo Department', N'1', 0, 0, N'', N'', CAST(N'2020-07-15T22:18:00.0000000' AS DateTime2), CAST(N'2024-12-24T02:15:52.0000000' AS DateTime2), 0, 0, 1),
(2, N'10284', N'10284', N'Department Demo 2', N'2', 0, 0, N'', N'', CAST(N'2024-11-25T11:06:21.0000000' AS DateTime2), CAST(N'2024-11-25T11:06:54.0000000' AS DateTime2), 0, 0, 1);

SET IDENTITY_INSERT [smart_meeting_room].[alocation] OFF
GO
