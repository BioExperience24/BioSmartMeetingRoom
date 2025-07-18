ALTER TABLE smart_meeting_room.employee
ADD head_employee_id NVARCHAR(100) NULL DEFAULT NULL;

ALTER TABLE smart_meeting_room.employee
ADD is_protected INT NOT NULL DEFAULT 0;