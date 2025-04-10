CREATE TABLE smart_meeting_room.sessions
(
    Id NVARCHAR(449) NOT NULL PRIMARY KEY,
    Value VARBINARY(MAX) NOT NULL,
    ExpiresAtTime DATETIMEOFFSET NOT NULL,
    SlidingExpirationInSeconds BIGINT NULL,
    AbsoluteExpiration DATETIMEOFFSET NULL
);