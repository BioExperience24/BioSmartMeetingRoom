-- jika data sebelumnya sudah ada
TRUNCATE TABLE smart_meeting_room."pantry_transaksi_status";

-- Menambahkan data baru dengan id dimulai dari 0
INSERT INTO smart_meeting_room.pantry_transaksi_status (id, name) 
VALUES 
(0, 'Order not yet processed'),
(1, 'Order processed'),
(2, 'Order Delivered'),
(3, 'Order Completed'),
(4, 'Order canceled'),
(5, 'Order rejected');

-- 
ALTER TABLE smart_meeting_room.pantry_transaksi_status DROP CONSTRAINT PK_pantry_transaksi_status_id;
ALTER TABLE smart_meeting_room.pantry_transaksi_status ALTER COLUMN id bigint NOT NULL;
ALTER TABLE smart_meeting_room.pantry_transaksi_status ADD CONSTRAINT PK_pantry_transaksi_status_id PRIMARY KEY (id);
