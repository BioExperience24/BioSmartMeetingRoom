USE [smart_meeting_room]
GO
SET IDENTITY_INSERT [smart_meeting_room].[level_descriptiion] ON 

INSERT INTO [smart_meeting_room].[level_descriptiion] ([id], [level_id], [description], [is_deleted]) 
VALUES 
    (1, 1, N'{{"cms":{{"name":"Manage CMS","desc":"Allow user to sign in to your CMS using their username and access the following..","detail":["Manage company","Manage employee","Manage room","Manage booking","Manage report","Manage pantry","Manage user"]}}}}', 0),
    (2, 2, N'{{"cms":{{"name":"Meeting Webs","desc":"Allow user to sign in to your web using their username, nik and access the following.","detail":["Book a meeting","Invite a partisipant","Room Check","Order Snack"]}},"apps":{{"name":"Mobile Apps","desc":"Allow user to sign in to your web using their username, nik and access the following.","detail":["Book a meeting","Invite a partisipant","Get notification","Room check","Order snack"]}}}}', 0),
    (3, 3, N'{{"door_access":{{"name":"Door Access","desc":"Allows the user to open every door with access.","detail":["Open a door"]}}}}', 0),
    (4, 4, N'{{"pantry":{{"name":"Manage Pantry","desc":"Allow user to sign in to your CMS using their username and access the following..","detail":["Manage pantry","Manage pantry in screen"]}}}}', 0),
    (5, 6, N'{{"cms":{{"name":"Manage CMS","desc":"Allow user to sign in to your CMS using their username and access the following..","detail":["Manage meeting","Approval snack","Approval meeting"]}}}}', 0),
    (6, 7, N'{{"cms":{{"name":"Manage User Admin","desc":"Allow user to sign in to your CMS using their username and access the following..","detail":["Manage user admin"]}}}}', 0);

SET IDENTITY_INSERT [smart_meeting_room].[level_descriptiion] OFF
GO
