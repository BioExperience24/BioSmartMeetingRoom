ALTER TABLE smart_meeting_room.booking
ADD recurring_id NVARCHAR(100) DEFAULT NULL;

ALTER TABLE smart_meeting_room.booking
ADD is_recurring SMALLINT DEFAULT 0;