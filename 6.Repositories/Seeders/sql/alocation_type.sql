USE [smart_meeting_room]
GO
SET IDENTITY_INSERT [smart_meeting_room].[alocation_type] ON 

INSERT INTO [smart_meeting_room].[alocation_type] 
([_generate], [id], [name], [invoice_status], [created_by], [updated_by], [created_at], [updated_at], [is_permanent], [is_deleted]) 
VALUES 
(1, N'1', N'Bio Smart Meeting', 1, NULL, NULL, CAST(N'2024-12-18T16:15:40.0000000' AS DateTime2), CAST(N'2024-12-18T16:15:40.0000000' AS DateTime2), 0, 0),
(2, N'2', N'Bio Smart Meeting 2', 1, NULL, NULL, CAST(N'2024-11-25T11:04:25.0000000' AS DateTime2), CAST(N'2025-03-17T08:10:19.0000000' AS DateTime2), 0, 0);

SET IDENTITY_INSERT [smart_meeting_room].[alocation_type] OFF
GO
