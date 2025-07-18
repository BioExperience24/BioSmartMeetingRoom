USE [smart_meeting_room]
GO

TRUNCATE TABLE [smart_meeting_room].[pantry_transaksi_status];

INSERT INTO [smart_meeting_room].[pantry_transaksi_status] ([id], [name]) 
VALUES 
(0, 'Order not yet processed'),
(1, 'Order processed'),
(2, 'Order Delivered'),
(3, 'Order Completed'),
(4, 'Order canceled'),
(5, 'Order rejected');
