SET IDENTITY_INSERT [smart_meeting_room].[smart_meeting_room].[variable_time_extend] ON;

INSERT INTO [smart_meeting_room].[smart_meeting_room].[variable_time_extend] ([id], [time])
VALUES 
    (1, 30),
    (2, 60),
    (3, 90),
    (4, 120);

SET IDENTITY_INSERT [smart_meeting_room].[smart_meeting_room].[variable_time_extend] OFF;
