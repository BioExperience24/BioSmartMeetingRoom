USE [smart_meeting_room]
GO
INSERT INTO [smart_meeting_room].[user_access] ([access_id], [access_name], [is_active]) 
VALUES 
    (1, N'Booking Meeting', 1),
    (2, N'Order Pantry', 1),
    (3, N'Approval Order', 1),
    (4, N'Approval Meeting', 1);
GO
