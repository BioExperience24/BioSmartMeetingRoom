USE [smart_meeting_room]
GO
SET IDENTITY_INSERT [smart_meeting_room].[level_header_detail] ON 

INSERT INTO [smart_meeting_room].[level_header_detail] ([id], [level_id], [menu_id], [coment]) 
        VALUES 
            (43, 1, 'MH0001', 'ADMIN'),
            (44, 1, 'MH0002', 'ADMIN'),
            (45, 1, 'MH0003', 'ADMIN'),
            (46, 6, 'MH0001', 'FRONT'),
            (47, 6, 'MH0002', 'FRONT'),
            (48, 6, 'MH0003', 'FRONT'),
            (49, 2, 'MH0003', 'USER');
            
SET IDENTITY_INSERT [smart_meeting_room].[alocation] OFF
GO
