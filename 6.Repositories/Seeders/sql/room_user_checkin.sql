USE [smart_meeting_room]
GO
INSERT INTO [smart_meeting_room].[room_user_checkin] ([id], [key], [name], [is_deleted]) 
    VALUES 
    (1, N'pic', N'PIC/Host/Organize Only', 0),
    (2, N'all', N'PIC/Host/Organize  OR Attendee/Audience/Participant', 0);
GO
