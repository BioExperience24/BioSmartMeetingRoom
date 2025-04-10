--change is_deleted to INT
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry 
DROP CONSTRAINT DF__pantry__is_delet__31D75E8D;

ALTER TABLE smart_meeting_room.smart_meeting_room.pantry 
ALTER COLUMN is_deleted INT NOT NULL;

ALTER TABLE smart_meeting_room.smart_meeting_room.pantry 
ADD CONSTRAINT DF_pantry_is_deleted 
DEFAULT 0 FOR is_deleted;

--change long of id to 36 so it can saving uuid
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_detail_menu_variant ALTER COLUMN id nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL;

ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_detail_menu_variant_detail ALTER COLUMN variant_id nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL;

--change PK pantry_detail_menu_variant_detail to LONG
-- 1. Hapus constraint primary key
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_detail_menu_variant_detail
DROP CONSTRAINT PK_pantry_detail_menu_variant_detail_id;

-- 2. Ubah tipe data kolom id menjadi BIGINT
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_detail_menu_variant_detail
ALTER COLUMN id BIGINT NOT NULL;

-- 3. Buat ulang constraint primary key
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_detail_menu_variant_detail
ADD CONSTRAINT PK_pantry_detail_menu_variant_detail_id PRIMARY KEY (id);


--change is deleted
ALTER TABLE smart_meeting_room.smart_meeting_room.facility 
DROP CONSTRAINT DF__facility__is_del__36D11DD4;

ALTER TABLE smart_meeting_room.smart_meeting_room.facility ALTER COLUMN is_deleted int NOT NULL;

ALTER TABLE smart_meeting_room.smart_meeting_room.facility 
ADD CONSTRAINT DF_facility_is_deleted 
DEFAULT 0 FOR is_deleted;

--pantry ID di table package dan transaksi
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_menu_paket ALTER COLUMN pantry_id bigint NOT NULL;
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_transaksi ALTER COLUMN pantry_id bigint NOT NULL;

--change PK pantrysatuan
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_satuan DROP CONSTRAINT PK_pantry_satuan_id;
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_satuan ALTER COLUMN id bigint NOT NULL;
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_satuan ADD CONSTRAINT PK_pantry_satuan_id PRIMARY KEY (id);

--ganti foreign key dari INT ke STRING
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_menu_paket_d ALTER COLUMN package_id nvarchar(50) NOT NULL;
--ganti lg dr int ke long
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_menu_paket_d ALTER COLUMN menu_id bigint NOT NULL;

--nambah kolom di pantry transaksi
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_transaksi ADD paket_id bigint NULL;
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_transaksi ADD approved_by text NULL;
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_transaksi ADD approved_at datetime NULL;

INSERT INTO smart_meeting_room.smart_meeting_room.pantry_transaksi_status (id, name) 
VALUES (5, 'Reject');

--ganti dari int ke long
ALTER TABLE smart_meeting_room.smart_meeting_room.pantry_transaksi_d ALTER COLUMN menu_id bigint NOT NULL;
