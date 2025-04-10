IF OBJECT_ID('smart_meeting_room.room_display_information', 'U') IS NOT NULL
    DROP TABLE smart_meeting_room.room_display_information;
CREATE TABLE smart_meeting_room.room_display_information (
    _generate INT NOT NULL IDENTITY(1,1),
    display_id BIGINT NULL,
    room_id NVARCHAR(255) NULL,
    icon NVARCHAR(255) NULL,
    distance INT NULL,
    PRIMARY KEY (_generate)
);