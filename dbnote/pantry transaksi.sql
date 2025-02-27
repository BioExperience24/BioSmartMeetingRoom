EXEC smart_meeting_room.sys.sp_rename N'smart_meeting_room.smart_meeting_room.pantry_transaksi.paket_id' , N'package_id', 'COLUMN';
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_transaksi ALTER COLUMN package_id nvarchar(50) NULL;

ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_detail ALTER COLUMN pantry_id bigint NOT NULL;
