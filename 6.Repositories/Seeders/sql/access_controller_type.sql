USE [smart_meeting_room]
GO
INSERT INTO [smart_meeting_room].[access_controller_type] ([id], [name], [is_deleted]) 
VALUES 
    (N'custid', N'Bio Access', 1),
    (N'entrypassid', N'Entrypass', 0),
    (N'falcoid', N'Falco Controller', 1);
GO
