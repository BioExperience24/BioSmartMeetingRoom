EXEC smart_meeting_room.sys.sp_rename N'smart_meeting_room.smart_meeting_room.pantry_transaksi.paket_id' , N'package_id', 'COLUMN';
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_transaksi ALTER COLUMN package_id nvarchar(50) NULL;

ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_detail ALTER COLUMN pantry_id bigint NOT NULL;

ALTER TABLE smart_meeting_room.pantry_transaksi ALTER COLUMN approved_by nvarchar(100) NULL;

---
-- 'nama head atau atasan yang accept / reject'
ALTER TABLE smart_meeting_room.pantry_transaksi 
ADD approved_head_by varchar(255) NULL DEFAULT NULL;

ALTER TABLE smart_meeting_room.pantry_transaksi 
ADD approved_head_at datetime NULL DEFAULT NULL;

-- 'id atasan employee'
ALTER TABLE smart_meeting_room.pantry_transaksi 
ADD head_employee_id varchar(255) NULL DEFAULT NULL;

-- '0 pending, 1 accept, 2 reject'
ALTER TABLE smart_meeting_room.pantry_transaksi 
ADD approval_head int NULL DEFAULT 0;