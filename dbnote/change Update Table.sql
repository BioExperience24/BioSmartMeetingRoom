EXEC smart_meeting_room.sys.sp_rename N'smart_meeting_room.department.department_id' , N'id', 'COLUMN';

-- 
ALTER TABLE smart_meeting_room.[user] DROP CONSTRAINT [DF__user__is_deleted__390E6C01];
ALTER TABLE smart_meeting_room.[user] ALTER COLUMN is_deleted int NOT NULL;
ALTER TABLE smart_meeting_room.[user] ADD CONSTRAINT DF_user_is_deleted DEFAULT 0 FOR is_deleted;

-- 
ALTER TABLE smart_meeting_room.building ALTER COLUMN id bigint NOT NULL;
ALTER TABLE smart_meeting_room.building DROP CONSTRAINT [DF__building__is_del__37FA4C37];
ALTER TABLE smart_meeting_room.building ALTER COLUMN is_deleted int;
ALTER TABLE smart_meeting_room.[building] ADD CONSTRAINT DF_building_is_deleted DEFAULT 0 FOR is_deleted;
-- ALTER TABLE smart_meeting_room.building ALTER COLUMN is_deleted int COLLATE SQL_Latin1_General_CP1_CI_AS NULL;

-- 
-- Step 1: Drop Primary Key
ALTER TABLE smart_meeting_room.beacon_floor DROP CONSTRAINT PK_beacon_floor_id;
-- Step 2: Ubah tipe data
ALTER TABLE smart_meeting_room.beacon_floor ALTER COLUMN id bigint;
-- Step 3: Tambahkan kembali Primary Key
ALTER TABLE smart_meeting_room.beacon_floor ADD CONSTRAINT PK_beacon_floor_id PRIMARY KEY (id);
ALTER TABLE smart_meeting_room.beacon_floor ALTER COLUMN building_id bigint NOT NULL;
ALTER TABLE smart_meeting_room.beacon_floor DROP CONSTRAINT [DF__beacon_fl__build__14E61A24];

-- 
/* Identifikasi nama constraint default */
SELECT name AS DefaultConstraintName
FROM sys.default_constraints
WHERE parent_object_id = OBJECT_ID('smart_meeting_room.level') 
    AND parent_column_id = COLUMNPROPERTY(OBJECT_ID('smart_meeting_room.level'), 'is_deleted', 'ColumnId');
/* .Identifikasi nama constraint default */
-- drop constraint
ALTER TABLE smart_meeting_room.[level] DROP CONSTRAINT DF__level__is_delete__4EA8A765;
-- change data type to int from smallint
ALTER TABLE smart_meeting_room.[level] ALTER COLUMN is_deleted int NOT NULL;
-- add new constraint (optional)
ALTER TABLE smart_meeting_room.[level] ADD CONSTRAINT DF_level_is_deleted DEFAULT 0 FOR is_deleted;

-- 
ALTER TABLE smart_meeting_room.level_descriptiion DROP CONSTRAINT PK_level_descriptiion_id;
ALTER TABLE smart_meeting_room.level_descriptiion ALTER COLUMN id bigint;
ALTER TABLE smart_meeting_room.level_descriptiion ADD CONSTRAINT PK_level_descriptiion_id PRIMARY KEY (id);

--
ALTER TABLE smart_meeting_room.[building_floor] DROP CONSTRAINT [DF__building___build__3CBF0154];
ALTER TABLE smart_meeting_room.[building_floor] ALTER COLUMN building_id bigint NULL;
ALTER TABLE smart_meeting_room.[building_floor] ADD CONSTRAINT DF_building_floor_building_id DEFAULT NULL FOR building_id;

--
ALTER TABLE smart_meeting_room.[access_channel] DROP CONSTRAINT [DF__access_ch__is_de__65370702];
ALTER TABLE smart_meeting_room.[access_channel] ALTER COLUMN is_deleted INT NULL;
ALTER TABLE smart_meeting_room.[access_channel] ADD CONSTRAINT DF_access_channel_is_deleted DEFAULT NULL FOR is_deleted;

-- 
ALTER TABLE smart_meeting_room.access_controller_falco DROP CONSTRAINT PK_access_controller_falco_id;
ALTER TABLE smart_meeting_room.access_controller_falco ALTER COLUMN id bigint;
ALTER TABLE smart_meeting_room.access_controller_falco ADD CONSTRAINT PK_access_controller_falco_id PRIMARY KEY (id);

-- Step 1: Alter the column type (without default)
ALTER TABLE smart_meeting_room.smart_meeting_room.room 
ALTER COLUMN is_beacon INT;

-- Step 2: Add a default constraint
ALTER TABLE smart_meeting_room.smart_meeting_room.room 
ADD CONSTRAINT DF_is_beacon DEFAULT 0 FOR is_beacon;

-- smart_meeting_room.smart_meeting_room.room_detail definition

-- Drop table

-- DROP TABLE smart_meeting_room.smart_meeting_room.room_detail;

-- CREATE TABLE smart_meeting_room.smart_meeting_room.room_detail (
-- 	id bigint IDENTITY(1062,1) NOT NULL,
-- 	room_id bigint NOT NULL,
-- 	facility_id bigint NOT NULL,
-- 	[datetime] datetime2(0) DEFAULT NULL NULL,
-- 	CONSTRAINT PK_room_detail_id_2 PRIMARY KEY (id)
-- );

-- 1. Drop the Primary Key Constraint
SELECT name
FROM sys.key_constraints
WHERE type = 'PK' AND parent_object_id = OBJECT_ID('smart_meeting_room.smart_meeting_room.room_automation');
-- Replace PK_room_automation_id
ALTER TABLE smart_meeting_room.smart_meeting_room.room_automation
DROP CONSTRAINT PK_room_automation_id;
-- Alter the Column
ALTER TABLE smart_meeting_room.smart_meeting_room.room_automation
ALTER COLUMN id BIGINT NOT NULL;
-- Recreate the Primary Key Constraint
ALTER TABLE smart_meeting_room.smart_meeting_room.room_automation
ADD CONSTRAINT PK_room_automation_id PRIMARY KEY (id);

ALTER TABLE smart_meeting_room.smart_meeting_room.room
DROP CONSTRAINT DF__room__floor_id__6482D9EB;

ALTER TABLE smart_meeting_room.smart_meeting_room.room ALTER COLUMN floor_id bigint NULL;


ALTER TABLE smart_meeting_room.smart_meeting_room.room ALTER COLUMN image2 NVARCHAR(MAX) NULL;


ALTER TABLE smart_meeting_room.smart_meeting_room.room_for_usage_detail
DROP CONSTRAINT DF__room_for___room___2CDD9F46; -- Replace with the actual name if different


ALTER TABLE smart_meeting_room.smart_meeting_room.room_for_usage_detail
ALTER COLUMN room_id BIGINT NULL;

ALTER TABLE smart_meeting_room.smart_meeting_room.room_merge_detail ALTER COLUMN room_id bigint NOT NULL;

ALTER TABLE smart_meeting_room.smart_meeting_room.room ALTER COLUMN work_end nvarchar(5) NOT NULL;
ALTER TABLE smart_meeting_room.smart_meeting_room.room ALTER COLUMN work_start nvarchar(5) NOT NULL;

ALTER TABLE smart_meeting_room.smart_meeting_room.room ALTER COLUMN work_end nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL;
ALTER TABLE smart_meeting_room.smart_meeting_room.room ALTER COLUMN work_start nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL;


-- 
ALTER TABLE smart_meeting_room.kiosk_display DROP CONSTRAINT PK_kiosk_display_id;
ALTER TABLE smart_meeting_room.kiosk_display ALTER COLUMN id bigint;
ALTER TABLE smart_meeting_room.kiosk_display ADD CONSTRAINT PK_kiosk_display_id PRIMARY KEY (id);