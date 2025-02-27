/*
 Navicat Premium Data Transfer

 Source Server         : Localhost
 Source Server Type    : MySQL
 Source Server Version : 80029
 Source Host           : localhost:3306
 Source Schema         : demobooking_room

 Target Server Type    : MySQL
 Target Server Version : 80029
 File Encoding         : 65001

 Date: 04/02/2025 15:37:53
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for level
-- ----------------------------
DROP TABLE IF EXISTS `level`;
CREATE TABLE `level` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `default_menu` int NOT NULL,
  `sort_level` int DEFAULT NULL,
  `created_by` int DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  `is_deleted` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2020 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of level
-- ----------------------------
BEGIN;
INSERT INTO `level` (`id`, `name`, `default_menu`, `sort_level`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (1, 'Administrator', 7, 2, NULL, '2019-09-19 15:23:17', '2019-09-19 15:23:17', 0);
INSERT INTO `level` (`id`, `name`, `default_menu`, `sort_level`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (2, 'Employee', 7, 3, NULL, '2019-09-19 18:38:57', '2019-09-19 18:38:58', 0);
INSERT INTO `level` (`id`, `name`, `default_menu`, `sort_level`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (3, 'Employee Old', 7, NULL, NULL, '2019-09-19 18:38:57', '2019-09-19 18:38:57', 1);
INSERT INTO `level` (`id`, `name`, `default_menu`, `sort_level`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (4, 'Pantry Display', 7, 4, NULL, '2019-09-19 18:38:57', '2019-09-19 18:38:57', 1);
INSERT INTO `level` (`id`, `name`, `default_menu`, `sort_level`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (5, 'Pantry Operator', 7, 5, NULL, '2019-09-19 18:38:57', '2019-09-19 18:38:57', 1);
INSERT INTO `level` (`id`, `name`, `default_menu`, `sort_level`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (6, 'SO / Helpdesk', 7, 6, NULL, '2019-09-19 18:38:57', '2019-09-19 18:38:57', 0);
INSERT INTO `level` (`id`, `name`, `default_menu`, `sort_level`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (7, 'Super Admin', 7, 1, NULL, NULL, NULL, 0);
COMMIT;

-- ----------------------------
-- Table structure for level_descriptiion
-- ----------------------------
DROP TABLE IF EXISTS `level_descriptiion`;
CREATE TABLE `level_descriptiion` (
  `id` int NOT NULL AUTO_INCREMENT,
  `level_id` int NOT NULL,
  `description` text COLLATE utf8mb4_general_ci NOT NULL,
  `is_deleted` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of level_descriptiion
-- ----------------------------
BEGIN;
INSERT INTO `level_descriptiion` (`id`, `level_id`, `description`, `is_deleted`) VALUES (1, 1, '{\"cms\":{\"name\":\"Manage CMS\",\"desc\":\"Allow user to sign in to your CMS using their username and access the following..\",\"detail\":[\"Manage company\",\"Manage employee\",\"Manage room\",\"Manage booking\",\"Manage report\",\"Manage pantry\",\"Manage user\"]}}', 0);
INSERT INTO `level_descriptiion` (`id`, `level_id`, `description`, `is_deleted`) VALUES (2, 2, '{\"cms\":{\"name\":\"Meeting Webs\",\"desc\":\"Allow user to sign in to your web using their username, nik and access the following.\",\"detail\":[\"Book a meeting\",\"Invite a partisipant\",\"Room Check\",\"Order Snack\"]},\"apps\":{\"name\":\"Mobile Apps\",\"desc\":\"Allow user to sign in to your web using their username, nik and access the following.\",\"detail\":[\"Book a meeting\",\"Invite a partisipant\",\"Get notification\",\"Room check\",\"Order snack\"]}}', 0);
INSERT INTO `level_descriptiion` (`id`, `level_id`, `description`, `is_deleted`) VALUES (3, 3, '{\"door_access\":{\"name\":\"Door Access\",\"desc\":\"Allows the user to open every door with access.\",\"detail\":[\"Open a door\"]}}', 0);
INSERT INTO `level_descriptiion` (`id`, `level_id`, `description`, `is_deleted`) VALUES (4, 4, '{\"pantry\":{\"name\":\"Manage Pantry\",\"desc\":\"Allow user to sign in to your CMS using their username and access the following..\",\"detail\":[\"Manage pantry\",\"Manage pantry in screen\"]}}', 0);
INSERT INTO `level_descriptiion` (`id`, `level_id`, `description`, `is_deleted`) VALUES (6, 6, '{\"cms\":{\"name\":\"Manage CMS\",\"desc\":\"Allow user to sign in to your CMS using their username and access the following..\",\"detail\":[\"Manage meeting\",\"Approval snack\",\"Approval meeting\"]}}', 0);
INSERT INTO `level_descriptiion` (`id`, `level_id`, `description`, `is_deleted`) VALUES (7, 7, '{\"cms\":{\"name\":\"Manage User Admin\",\"desc\":\"Allow user to sign in to your CMS using their username and access the following..\",\"detail\":[\"Manage user admin\"]}}', 0);
COMMIT;

-- ----------------------------
-- Table structure for level_detail
-- ----------------------------
DROP TABLE IF EXISTS `level_detail`;
CREATE TABLE `level_detail` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `level_id` int NOT NULL,
  `menu_id` int NOT NULL,
  `coment` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=54 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of level_detail
-- ----------------------------
BEGIN;
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (1, 1, 1, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (2, 1, 2, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (3, 1, 3, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (4, 1, 4, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (5, 1, 5, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (6, 1, 6, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (8, 1, 7, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (9, 1, 8, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (10, 1, 9, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (11, 1, 10, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (12, 1, 11, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (13, 1, 12, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (14, 1, 13, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (15, 1, 14, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (16, 1, 15, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (17, 1, 16, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (18, 1, 17, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (19, 1, 18, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (20, 1, 19, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (21, 1, 20, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (22, 1, 21, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (23, 1, 22, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (24, 1, 23, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (25, 2, 12, 'user');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (26, 2, 7, 'user');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (27, 2, 2, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (28, 1, 24, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (29, 1, 25, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (30, 1, 26, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (31, 1, 27, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (32, 1, 28, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (33, 1, 29, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (34, 1, 30, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (35, 1, 32, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (36, 1, 33, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (37, 1, 34, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (38, 1, 35, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (39, 1, 36, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (40, 1, 37, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (41, 1, 38, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (42, 1, 39, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (43, 2, 34, 'USER');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (44, 7, 1, 'SUPER');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (45, 6, 40, 'SO');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (46, 6, 41, 'SO');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (47, 2, 24, 'USER');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (48, 6, 2, 'SO');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (49, 6, 12, 'SO');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (50, 1, 40, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (51, 1, 41, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (52, 1, 42, 'ADMIN');
INSERT INTO `level_detail` (`id`, `level_id`, `menu_id`, `coment`) VALUES (53, 1, 43, 'ADMIN');
COMMIT;

-- ----------------------------
-- Table structure for menu
-- ----------------------------
DROP TABLE IF EXISTS `menu`;
CREATE TABLE `menu` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `type_icon` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `icon` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `sort` int NOT NULL,
  `is_child` int NOT NULL,
  `menu_group_id` int NOT NULL,
  `module_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `created_by` int DEFAULT NULL,
  `created_at` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP,
  `updated_at` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP,
  `is_deleted` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=44 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of menu
-- ----------------------------
BEGIN;
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (1, 'User', '/user', 'awesome', 'person', 22, 1, 11, 'module_user', NULL, '2023-07-01 00:55:54', '2023-07-01 00:55:54', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (2, 'Room Schedule', '/booking', 'awesome', 'schedule', 5, 1, 2, 'module_meeting', NULL, '2024-12-24 10:42:56', '2024-12-24 10:42:56', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (3, 'Room Management', '/room', 'awesome', 'apps', 51, 1, 2, 'module_room', NULL, '2024-12-24 10:42:51', '2024-12-24 10:42:51', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (4, 'Master Snack & Pantry', '/pantry', 'awesome', 'room_service', 7, 1, 10, 'module_pantry', NULL, '2024-12-24 10:43:30', '2024-12-24 10:43:30', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (5, 'Facility', '/facility', 'awesome', 'home', 3, 1, 1, 'module_room', NULL, '2024-12-20 01:17:23', '2024-12-20 01:17:23', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (6, 'Attendance', '/attendance', 'awesome', 'people', 100, 1, 1, '', NULL, '2024-01-05 03:27:43', '2024-01-05 03:27:43', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (7, 'Dashboard', '/dashboard', 'awesome', 'home', 0, 0, 1, '', NULL, '2023-07-01 00:44:22', '2023-07-01 00:44:22', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (8, 'Automation', '/automation', 'awsome', 'home', 4, 1, 1, 'module_automation', NULL, '2023-02-02 04:45:31', '2023-02-02 04:45:31', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (9, 'Multimedia', '/Multimedia', 'awesome', 'tv', 9, 1, 1, '', NULL, '2023-02-02 04:45:31', '2023-02-02 04:45:31', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (10, 'Employee', '/employee', 'awesome', 'person', 21, 1, 11, 'module_user', NULL, '2023-07-01 00:55:49', '2023-07-01 00:55:49', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (11, 'Information', '/company', 'awesome', 'card_membership', 1, 0, 2, 'module_informasi', NULL, '2023-07-01 00:44:36', '2023-07-01 00:44:36', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (12, 'Room Usage', '/report-usage', 'awesome', 'description', 52, 1, 2, 'module_report', NULL, '2024-12-24 10:42:58', '2024-12-24 10:42:58', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (13, 'Invoice', '/invoice', 'awesome', 'description', 10, 0, 4, '', NULL, '2023-02-02 04:45:31', '2023-02-02 04:45:31', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (14, 'Department', '/department', 'awesome', 'apps', 2, 1, 11, '', NULL, '2023-07-01 00:48:13', '2023-07-01 00:48:13', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (15, 'Building', '/building', 'awesome', 'room_service', 4, 1, 9, 'module_building', NULL, '2023-07-01 01:00:39', '2023-07-01 01:00:39', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (16, 'Access Door', '/access', 'awsome', 'door', 3, 1, 1, 'module_access_door', NULL, '2024-12-20 01:13:03', '2024-12-20 01:13:03', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (17, 'Company/Department', '/alocation', 'awesome', 'apps', 2, 1, 11, 'module_alocation', NULL, '2023-10-09 13:24:27', '2023-10-09 13:24:27', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (18, 'General', '/setting/general', 'awesome', 'apps', 65, 1, 3, 'module_meeting', NULL, '2025-02-04 15:35:54', '2025-02-04 15:35:54', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (19, 'SMTP & Email', '/setting/smtp-email', 'awesome', 'apps', 66, 1, 3, 'module_meeting', NULL, '2025-02-04 15:35:57', '2025-02-04 15:35:57', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (20, 'Display Signage', '/display', 'awesome', 'apps', 31, 1, 1, 'module_display', NULL, '2025-01-14 16:11:09', '2025-01-14 16:11:09', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (21, 'Cancel Order', '/report-cancel-order', 'awesome', 'description', 10, 1, 5, '', NULL, '2023-10-09 12:24:10', '2023-10-09 12:24:10', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (22, 'Income Rent Room', '/report-income', 'awesome', 'description', 11, 1, 5, '', NULL, '2023-10-09 12:23:49', '2023-10-09 12:23:49', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (23, 'Outstanding Invoice', '/report-outstanding', 'awesome', 'description', 12, 1, 5, '', NULL, '2023-10-09 12:23:53', '2023-10-09 12:23:53', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (24, 'Order Management', '/pantry-transaction', 'awsome', 'schedule', 75, 1, 10, 'module_pantry', NULL, '2024-12-24 10:43:27', '2024-12-24 10:43:27', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (25, 'Menu Package', '/pantry-package', 'awesome', 'room_service', 74, 1, 10, 'module_pantry', NULL, '2024-12-24 10:43:17', '2024-12-24 10:43:17', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (26, 'Locker System', '/locker-system', 'awesome', 'lock', 7, 1, 1, 'module_loker', NULL, '2023-07-01 00:50:29', '2023-07-01 00:50:29', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (27, 'Display Kiosk', '/display-kiosk', 'awesome', 'apps', 32, 1, 1, 'module_kiosk', NULL, '2024-12-20 01:16:36', '2024-12-20 01:16:36', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (28, 'Floor Management', '/beacon-floor', 'awesome', 'apps', 41, 1, 9, 'module_floor', NULL, '2024-12-20 01:18:17', '2024-12-20 01:18:17', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (29, 'Floor Room', '/beacon-floor-room', 'awesome', 'apps', 5, 1, 7, 'module_beacon', NULL, '2023-06-11 02:18:11', '2023-06-11 02:18:11', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (30, 'Beacon Tag', '/beacon-tag', 'awesome', 'apps', 82, 1, 7, 'module_beacon', NULL, '2023-10-09 13:27:02', '2023-10-09 13:27:02', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (32, 'Desk Room', '/deskroom', 'awesome', 'apps', 6, 1, 8, 'module_desk', NULL, '2024-12-24 10:44:11', '2024-12-24 10:44:11', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (33, 'Desk Controller', '/deskcontroller', 'awesome', 'apps', 62, 1, 8, 'module_desk', NULL, '2024-12-24 10:44:09', '2024-12-24 10:44:09', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (34, 'Desk Transaction', '/desktrs', 'awesome', 'apps', 61, 1, 8, 'module_desk', NULL, '2024-12-24 10:44:13', '2024-12-24 10:44:13', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (35, 'Live Transaction', '/beacon-live-monitor', 'awesome', 'apps', 12, 1, 7, 'module_beacon', NULL, '2023-10-09 13:26:59', '2023-10-09 13:26:59', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (36, 'Beacon Gateway', '/beacon-gateway', 'awesome', 'apps', 81, 1, 7, 'module_beacon', NULL, '2023-10-09 13:27:04', '2023-10-09 13:27:04', 1);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (37, 'License', '/setting/license', 'awesome', 'description', 67, 1, 3, 'module_license', NULL, '2025-02-04 15:36:05', '2025-02-04 15:36:05', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (38, 'Integration', '/integration', 'awesome', 'open_with', 55, 0, 1, 'module_integration', NULL, '2025-02-04 15:36:25', '2025-02-04 15:36:25', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (39, 'Menu Order', '/pantry-menu', 'awesome', 'open_with', 71, 1, 10, 'module_pantry', NULL, '2024-12-24 10:43:12', '2024-12-24 10:43:12', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (40, 'Approval Meeting', '/approval-meeting', 'awesome', 'open_with', 41, 1, 12, 'module_meeting', NULL, '2025-02-04 15:33:21', '2025-02-04 15:33:21', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (41, 'Approval  Order', '/approval-order', 'awesome', 'open_with', 42, 1, 12, 'module_pantry', NULL, '2025-02-04 15:33:24', '2025-02-04 15:33:24', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (42, 'Help IT', '/help-id', 'awesome', 'apps', 53, 1, 13, 'module_meeting', NULL, '2025-02-04 15:34:48', '2025-02-04 15:34:48', 0);
INSERT INTO `menu` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (43, 'Help GS', '/help-gs', 'awesome', 'apps', 54, 1, 13, 'module_meeting', NULL, '2025-02-04 15:34:52', '2025-02-04 15:34:52', 0);
COMMIT;

-- ----------------------------
-- Table structure for menu_apps
-- ----------------------------
DROP TABLE IF EXISTS `menu_apps`;
CREATE TABLE `menu_apps` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `type_icon` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `icon` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `sort` int NOT NULL,
  `is_child` int NOT NULL,
  `menu_group_id` int NOT NULL,
  `module_text` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `created_by` int DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `is_deleted` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of menu_apps
-- ----------------------------
BEGIN;
INSERT INTO `menu_apps` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (1, 'Meeting', 'meeting', '', 'meeting.png', 1, 0, 0, 'module_meeting', NULL, NULL, NULL, 1);
INSERT INTO `menu_apps` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (2, 'Desk', 'desk', '', 'desk.png', 2, 0, 0, 'module_desk', NULL, NULL, NULL, 0);
INSERT INTO `menu_apps` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (3, 'Report', 'report', '', 'report.png', 4, 0, 0, 'module_report', NULL, NULL, NULL, 1);
INSERT INTO `menu_apps` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (4, 'Calender', 'calendar', '', 'calendar.png', 5, 0, 0, 'module_calender', NULL, NULL, NULL, 1);
INSERT INTO `menu_apps` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (5, 'Pantry', 'pantry', '', 'pantry.png', 6, 0, 0, 'module_pantry', NULL, NULL, NULL, 1);
INSERT INTO `menu_apps` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (6, 'Approval', 'approval', '', 'approval.png', 7, 0, 0, 'module_room_advance', NULL, NULL, NULL, 1);
INSERT INTO `menu_apps` (`id`, `name`, `url`, `type_icon`, `icon`, `sort`, `is_child`, `menu_group_id`, `module_text`, `created_by`, `created_at`, `updated_at`, `is_deleted`) VALUES (7, 'Website', 'website', '', 'website.png', 8, 0, 0, 'module_meeting', NULL, NULL, NULL, 1);
COMMIT;

-- ----------------------------
-- Table structure for menu_group
-- ----------------------------
DROP TABLE IF EXISTS `menu_group`;
CREATE TABLE `menu_group` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `icon` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of menu_group
-- ----------------------------
BEGIN;
INSERT INTO `menu_group` (`id`, `name`, `icon`) VALUES (1, 'Master/Base', 'storage');
INSERT INTO `menu_group` (`id`, `name`, `icon`) VALUES (2, 'Meeting Room', 'input');
INSERT INTO `menu_group` (`id`, `name`, `icon`) VALUES (3, 'Setting', 'lock');
INSERT INTO `menu_group` (`id`, `name`, `icon`) VALUES (4, 'null', '');
INSERT INTO `menu_group` (`id`, `name`, `icon`) VALUES (5, 'Report', 'description');
INSERT INTO `menu_group` (`id`, `name`, `icon`) VALUES (6, 'Invoice', 'description');
INSERT INTO `menu_group` (`id`, `name`, `icon`) VALUES (7, 'Tracking BLE', 'description');
INSERT INTO `menu_group` (`id`, `name`, `icon`) VALUES (8, 'Desk Booking', 'event');
INSERT INTO `menu_group` (`id`, `name`, `icon`) VALUES (9, 'Location', 'my_location');
INSERT INTO `menu_group` (`id`, `name`, `icon`) VALUES (10, 'Snack & Pantry', 'local_cafe');
INSERT INTO `menu_group` (`id`, `name`, `icon`) VALUES (11, 'User Management', 'contacts');
INSERT INTO `menu_group` (`id`, `name`, `icon`) VALUES (12, 'Approval', 'done_all');
INSERT INTO `menu_group` (`id`, `name`, `icon`) VALUES (13, 'Help', 'help_outline');
COMMIT;

SET FOREIGN_KEY_CHECKS = 1;
