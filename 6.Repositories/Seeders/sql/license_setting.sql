USE [smart_meeting_room]
GO
SET IDENTITY_INSERT [smart_meeting_room].[license_setting] ON 

INSERT INTO [smart_meeting_room].[license_setting] 
([id], [serial], [platform], [device_id], [checked_at], [status], [distributor_id], [customer_id], [ext], [webhost], [license_type], [pathdownload], [is_deleted], [created_at], [updated_at], [created_by], [updated_by]) 
VALUES 
(1, 'cf7473a5-6192-4688-9051-1051a874c0ad', 'SMART OFFICE', '', '2023-08-10 04:56:23', 1, 'distri_ivp', 'customer_ivp_001', '.license', 'http://localhost/fileEncCodeIgniter', 'local', 'encryptFile/20230810060521.license', 0, '2023-08-10 04:56:23', '2023-08-10 06:03:52', 'admin', 'admin');

SET IDENTITY_INSERT [smart_meeting_room].[license_setting] OFF
GO
