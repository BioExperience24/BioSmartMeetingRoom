USE [smart_meeting_room]
GO

TRUNCATE TABLE [smart_meeting_room].[time_schedule_60];
DBCC CHECKIDENT ('[smart_meeting_room].[time_schedule_60]', RESEED, 1);

-- SET IDENTITY_INSERT [smart_meeting_room].[time_schedule_60] ON 

INSERT INTO [smart_meeting_room].[time_schedule_60] ([time], [is_deleted]) 
VALUES 
    (N'00:00', 0),
    (N'01:00', 0),
    (N'02:00', 0),
    (N'03:00', 0),
    (N'04:00', 0),
    (N'05:00', 0),
    (N'06:00', 0),
    (N'07:00', 0),
    (N'08:00', 0),
    (N'09:00', 0),
    (N'10:00', 0),
    (N'11:00', 0),
    (N'12:00', 0),
    (N'13:00', 0),
    (N'14:00', 0),
    (N'15:00', 0),
    (N'16:00', 0),
    (N'17:00', 0),
    (N'18:00', 0),
    (N'19:00', 0),
    (N'20:00', 0),
    (N'21:00', 0),
    (N'22:00', 0),
    (N'23:00', 0);

-- SET IDENTITY_INSERT [smart_meeting_room].[time_schedule_60] OFF
GO
