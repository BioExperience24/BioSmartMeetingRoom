CREATE TABLE smart_meeting_room.help_it_ga (
    id VARCHAR(255) NOT NULL PRIMARY KEY,
    datetime DATETIME NOT NULL,
    booking_id VARCHAR(255) NOT NULL,
    room_id VARCHAR(255) NOT NULL,
    type NVARCHAR(255) CHECK (type IN (N'it', N'ga')) NOT NULL,
    status NVARCHAR(255) CHECK (status IN (N'pending', N'process', N'done', N'reject')) NOT NULL,
    subject VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    problem_facility TEXT NOT NULL,
    problem_reason NVARCHAR(255) CHECK (problem_reason IN (N'facility', N'connection', N'comfort')) NOT NULL,
    process_at DATETIME DEFAULT NULL,
    done_at DATETIME DEFAULT NULL,
    reject_at DATETIME DEFAULT NULL,
    response_done TEXT DEFAULT NULL,
    response_reject TEXT DEFAULT NULL,
    time_until_done_at INT DEFAULT NULL,
    process_by VARCHAR(255) DEFAULT NULL,
    done_by VARCHAR(255) DEFAULT NULL,
    reject_by VARCHAR(255) DEFAULT NULL,
    is_deleted INT DEFAULT 0,
    created_at DATETIME DEFAULT NULL,
    created_by VARCHAR(255) DEFAULT NULL,
    updated_at DATETIME DEFAULT NULL,
    updated_by VARCHAR(255) DEFAULT NULL
);

-- DUMMY DATA
INSERT INTO smart_meeting_room.help_it_ga (id, datetime, booking_id, room_id, type, status, subject, description, problem_facility, problem_reason, process_at, done_at, reject_at, response_done, response_reject, time_until_done_at, process_by, done_by, reject_by, is_deleted, created_at, created_by, updated_at, updated_by)
    VALUES
    ('59785803-93a4-4336-a73c-d3658862515d', '2023-10-01 10:00:00', '3234293641', '405284', 'it', 'pending', 'Subject 1', 'Description 1', 'Facility 1', 'facility', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, '2023-10-01 10:00:00', '20241208170622', NULL, NULL),
    ('123ae4f7-9522-4e14-9ad1-48f965bb84ef', '2023-10-02 11:00:00', '3234293641', '405284', 'ga', 'pending', 'Subject 2', 'Description 2', 'Facility 2', 'connection', '2023-10-02 12:00:00', NULL, NULL, NULL, NULL, NULL, 'User2', NULL, NULL, 0, '2023-10-02 11:00:00', '20241208170622', NULL, NULL),
    ('e1ce94d1-5ef1-4c30-a5e4-3d33b55fec81', '2023-10-03 12:00:00', '3234293641', '405284', 'it', 'pending', 'Subject 3', 'Description 3', 'Facility 3', 'comfort', '2023-10-03 13:00:00', '2023-10-03 14:00:00', NULL, 'Response Done 3', NULL, 60, 'User3', 'User3', NULL, 0, '2023-10-03 12:00:00', '20241208170622', NULL, NULL),
    ('029a8059-d897-4a6c-8760-9e07b5fbaa45', '2023-10-04 13:00:00', '3234293641', '405284', 'ga', 'pending', 'Subject 4', 'Description 4', 'Facility 4', 'facility', '2023-10-04 14:00:00', NULL, '2023-10-04 15:00:00', NULL, 'Response Reject 4', NULL, 'User4', NULL, 'User4', 0, '2023-10-04 13:00:00', '20241208170622', NULL, NULL),
    ('d29be949-9c9f-4bb9-a9b5-94c932b0849c', '2023-10-05 14:00:00', '3234293641', '405284', 'it', 'pending', 'Subject 5', 'Description 5', 'Facility 5', 'connection', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, '2023-10-05 14:00:00', '20241208170622', NULL, NULL);