USE [smart_meeting_room]
GO
SET IDENTITY_INSERT [smart_meeting_room].[employee] ON 

INSERT INTO [smart_meeting_room].[employee] 
([_generate], [id], [division_id], [company_id], [department_id], [name], [nik], [nik_display], [photo], [email], [no_phone], [no_ext], [birth_date], [gender], [address], [card_number], [card_number_real], [password_mobile], [gb_id], [fr_id], [priority], [created_at], [updated_at], [is_deleted], [is_vip], [vip_approve_bypass], [vip_limit_cap_bypass], [vip_lock_room], [is_protected]) 
VALUES 
(1, N'20241202071550', N'', N'1', N'10283', N'Admin', N'20241202071550', N'1234123412341111', N'', N'admin@mail.com', N'02123456789', N'111', CAST(N'2000-01-01' AS Date), N'male', N'Jalan Sesama', N'1234567890', N'1234567890', N'', N'', N'', 0, CAST(N'2024-12-02T07:15:51.0000000' AS DateTime2), CAST(N'2025-03-06T12:11:54.0000000' AS DateTime2), 0, 1, 1, 1, 1, 1),
(2, N'20241202071634', N'', N'2', N'10284', N'Helpdesk', N'20241202071634', N'1234123412342222', N'', N'so@mail.com', N'02123456789', N'222', CAST(N'2000-01-01' AS Date), N'male', N'Jalan Raya', N'1234567890', N'1234567890', N'', N'', N'', 0, CAST(N'2024-12-02T07:16:34.0000000' AS DateTime2), CAST(N'2025-02-24T12:01:07.0000000' AS DateTime2), 0, 0, 0, 0, 0, 0);

SET IDENTITY_INSERT [smart_meeting_room].[employee] OFF
GO
