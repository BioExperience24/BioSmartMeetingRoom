CREATE TABLE api_requests (
    id INT IDENTITY(1,1) PRIMARY KEY,
    url NVARCHAR(MAX) NULL,
    headers NVARCHAR(MAX) DEFAULT '[]',
    is_deleted SMALLINT DEFAULT 0,
    is_enabled SMALLINT DEFAULT 1
);
