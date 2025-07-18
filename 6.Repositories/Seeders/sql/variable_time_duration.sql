SET IDENTITY_INSERT [smart_meeting_room].[smart_meeting_room].[variable_time_duration] ON;

INSERT INTO [smart_meeting_room].[smart_meeting_room].[variable_time_duration] ([id], [time])
VALUES 
    (1, 30),
    (2, 60);

SET IDENTITY_INSERT [smart_meeting_room].[smart_meeting_room].[variable_time_duration] OFF;
