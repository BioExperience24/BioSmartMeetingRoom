USE [smart_meeting_room]
GO
SET IDENTITY_INSERT [smart_meeting_room].[http_url] ON 


INSERT INTO [smart_meeting_room].[http_url] ([id], [url], [headers], [is_deleted], [is_enabled]) VALUES (1, N'https://servicemail.piranticerdasindonesia.com/api/v1/email/send', N'[{{"X-Key":"THRmZFFYMk53c0N0blltNkhFT2NnQT09"}}]', 0, 1);

SET IDENTITY_INSERT [smart_meeting_room].[http_url] OFF
GO
