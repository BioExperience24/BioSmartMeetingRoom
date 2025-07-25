USE [smart_meeting_room]
GO

TRUNCATE TABLE [smart_meeting_room].[user];
DBCC CHECKIDENT ('[smart_meeting_room].[user]', RESEED, 1);

-- SET IDENTITY_INSERT [smart_meeting_room].[user] ON 

INSERT INTO [smart_meeting_room].[user] ([secure_qr], [name], [username], [employee_id], [password], [real_password], [level_id], [access_id], [created_by], [created_at], [updated_at], [is_disactived], [is_deleted], [updated_by], [is_vip], [vip_approve_bypass], [vip_limit_cap_bypass], [vip_shifted_bypass], [is_approval], [is_protected]) 
VALUES 
(N'', N'Admin', N'admin', N'20241202071550', N'MTIzNDEyMzQ=', N'12341234', 1, N'1#2#3#4', NULL, CAST(N'2024-12-02T07:15:50.867' AS DateTime), CAST(N'2024-12-02T07:15:50.867' AS DateTime), 0, 0, NULL, 1, 1, 1, 1, 1, 1),
(N'', N'Helpdesk', N'so', N'20241202071634', N'MTIzNDEyMzQ=', N'12341234', 6, N'1#2#3#4', NULL, CAST(N'2024-12-02T07:16:34.060' AS DateTime), CAST(N'2024-12-08T16:23:34.690' AS DateTime), 0, 0, NULL, 0, 0, 0, 0, 0, 0);

-- SET IDENTITY_INSERT [smart_meeting_room].[user] OFF
GO