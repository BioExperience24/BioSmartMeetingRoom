using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _6.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "smart_meeting_room");

            migrationBuilder.CreateTable(
                name: "access_channel",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    channel = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_channel_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "access_control",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, defaultValue: ""),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    ip_controller = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "0.0.0.0"),
                    access_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    channel = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    controller_list = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    room_controller_falco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    delay = table.Column<int>(type: "int", nullable: true, defaultValue: 3),
                    model_controller = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "reader"),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_control_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "access_controller_falco",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    access_id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    group_access = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    unit_no = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    falco_ip = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_controller_falco_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "access_controller_type",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_controller_type_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "access_integrated",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    access_id = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true, defaultValueSql: "(NULL)"),
                    room_id = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_integrated_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "alarm_integration",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status_integration = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    active = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    url_auth = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    url_feedback = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    param_auth = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    param_feed = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alarm_integration_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "alocation",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department_code = table.Column<string>(type: "nvarchar(43)", maxLength: 43, nullable: false, defaultValue: ""),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    invoice_type = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    invoice_status = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: ""),
                    updated_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: ""),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_permanent = table.Column<int>(type: "int", nullable: false),
                    show_in_invitation = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    id = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, defaultValue: ""),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alocation__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "alocation_matrix",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    alocation_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    nik = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alocation_matrix__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "alocation_type",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    invoice_status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_permanent = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alocation_type_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "auth_serial",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auth_serial_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "batch_upload",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    total_row = table.Column<int>(type: "int", nullable: false),
                    total_size = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_batch_upload_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "beacon_floor",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    building_id = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "(NULL)"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    pixel = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    floor_length = table.Column<double>(type: "float", nullable: true, defaultValueSql: "(NULL)"),
                    floor_width = table.Column<double>(type: "float", nullable: true, defaultValueSql: "(NULL)"),
                    meter_per_px = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    meter_per_px2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    plus_width = table.Column<double>(type: "float", nullable: true, defaultValueSql: "(NULL)"),
                    plus_height = table.Column<double>(type: "float", nullable: true, defaultValueSql: "(NULL)"),
                    center_x = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    center_y = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_beacon_floor_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "booking",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    booking_id_365 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    booking_id_google = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    booking_devices = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    no_order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    room_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    room_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    is_merge = table.Column<short>(type: "smallint", nullable: true, defaultValue: (short)0),
                    merge_room = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    merge_room_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    merge_room_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    start = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    end = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    cost_total_booking = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "(NULL)"),
                    duration_per_meeting = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    total_duration = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    extended_duration = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    pic = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    alocation_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValueSql: "(NULL)"),
                    alocation_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    canceled_note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    participants = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    external_link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    external_link_365 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    external_link_google = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    end_early_meeting = table.Column<int>(type: "int", nullable: false),
                    text_early = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    is_device = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_meal = table.Column<short>(type: "smallint", nullable: false),
                    is_ear = table.Column<int>(type: "int", nullable: false),
                    is_rescheduled = table.Column<int>(type: "int", nullable: false),
                    is_canceled = table.Column<int>(type: "int", nullable: false),
                    is_expired = table.Column<int>(type: "int", nullable: false),
                    canceled_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    canceled_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    expired_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    expired_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    rescheduled_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    rescheduled_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    early_ended_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    early_ended_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    is_alive = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    timezone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    is_notif_end_meeting = table.Column<int>(type: "int", nullable: false),
                    is_notif_before_end_meeting = table.Column<int>(type: "int", nullable: false),
                    is_access_trigger = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_config_setting_enable = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_enable_approval = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_enable_permission = table.Column<int>(type: "int", nullable: true, defaultValue: 240),
                    is_enable_recurring = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_enable_checkin = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_realease_checkin_timeout = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    is_released = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_enable_checkin_count = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    category = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    lastModifiedDateTime_365 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    permission_end = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "pic"),
                    permission_checkin = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "pic"),
                    release_room_checkin_time = table.Column<int>(type: "int", nullable: true, defaultValue: 10),
                    checkin_count = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    is_vip = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_approve = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    vip_user = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    user_end_meeting = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    user_checkin = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    user_approval = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    user_approval_datetime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    room_meeting_move = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    room_meeting_old = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    is_moved = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_moved_agree = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    moved_duration = table.Column<int>(type: "int", nullable: true, defaultValue: 5),
                    meeting_end_note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    vip_approve_bypass = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    vip_limit_cap_bypass = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    vip_lock_room = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    vip_force_moved = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    duration_saved_release = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_cleaning_need = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    cleaning_time = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    cleaning_start = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    cleaning_end = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    user_cleaning = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    server_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    server_start = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    server_end = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    booking_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "general"),
                    is_private = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    recurring_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    is_recurring = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "booking_alive",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_alive_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "booking_invitation",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    nik = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    @internal = table.Column<int>(name: "internal", type: "int", nullable: false),
                    attendance_status = table.Column<int>(type: "int", nullable: false),
                    attendance_reason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true, defaultValue: ""),
                    execute_attendance = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    execute_door_access = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    company = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    is_pic = table.Column<short>(type: "smallint", nullable: false),
                    is_vip = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    pin_room = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    lastUpdate_365 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    checkin = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    end_meeting = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_invitation_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "booking_invoice",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    invoice_generate_no = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    invoice_no = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    invoice_format = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    rent_cost = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "(NULL)"),
                    alocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    memo_no = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    referensi_no = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    time_before = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    time_send = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    time_paid = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    invoice_status = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_invoice_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "booking_invoice_detail",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    invoice_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValueSql: "(NULL)"),
                    no_urut = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true, defaultValueSql: "(NULL)"),
                    no_invoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    alocation_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    alocation_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    total_cost = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "(NULL)"),
                    total_duration = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    total_meeting = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    outstanding_status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    invoice_status = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValueSql: "(NULL)"),
                    alocation_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    cost_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: ""),
                    created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: ""),
                    sent_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: ""),
                    paid_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: ""),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    sent_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    paid_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_invoice_detail_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "booking_invoice_generate",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    invoice_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: ""),
                    invoice_format = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    invoice_month1 = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    invoice_month2 = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    invoice_years = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "(NULL)"),
                    memo_no = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    referensi_no = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    alocation_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    total_cost = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    total_meeting = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    total_duration = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "(NULL)"),
                    status = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValueSql: "(NULL)"),
                    date_generate = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    date_sending = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    date_confirm = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    generate_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    sending_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    confirm_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_invoice_generate_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "booking_room_trs",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    room_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    date = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "building",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "default.jpeg"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timezone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    detail_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    google_map = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    koordinate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    created_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    id = table.Column<long>(type: "bigint", nullable: true),
                    is_deleted = table.Column<int>(type: "int", maxLength: 255, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_building_generate", x => x.generate);
                });

            migrationBuilder.CreateTable(
                name: "building_floor",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    building_id = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "(NULL)"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    position = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    pixel = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    floor_length = table.Column<double>(type: "float", nullable: true, defaultValueSql: "(NULL)"),
                    floor_width = table.Column<double>(type: "float", nullable: true, defaultValueSql: "(NULL)"),
                    meter_per_px = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    meter_per_px2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    plus_width = table.Column<double>(type: "float", nullable: true, defaultValueSql: "(NULL)"),
                    plus_height = table.Column<double>(type: "float", nullable: true, defaultValueSql: "(NULL)"),
                    center_x = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    center_y = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    created_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    id = table.Column<long>(type: "bigint", nullable: true),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_building_floor__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "company",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    state = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    picture = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    icon = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    logo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    menu_bar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    url_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    update_at = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "desk_booking",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    no_order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    room_id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, defaultValueSql: "(NULL)"),
                    desk_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    desk_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    room_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    start = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    end = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    cost_total_booking = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "(NULL)"),
                    duration_per_meeting = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    total_duration = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    extended_duration = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    pic = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    alocation_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValueSql: "(NULL)"),
                    alocation_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    participants = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    end_early_meeting = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    text_early = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    is_meal = table.Column<short>(type: "smallint", nullable: false),
                    is_ear = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    is_rescheduled = table.Column<int>(type: "int", nullable: false),
                    is_canceled = table.Column<int>(type: "int", nullable: false),
                    is_expired = table.Column<int>(type: "int", nullable: false),
                    canceled_note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    canceled_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    canceled_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    expired_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    expired_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    rescheduled_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    rescheduled_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    early_ended_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    early_ended_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    is_alive = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_notif_before_end_meeting = table.Column<int>(type: "int", nullable: false),
                    is_notif_end_meeting = table.Column<int>(type: "int", nullable: false),
                    is_device = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_access_trigger = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desk_booking_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "desk_booking_invitation",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    nik = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    @internal = table.Column<int>(name: "internal", type: "int", nullable: false, defaultValue: 1),
                    attendance_status = table.Column<int>(type: "int", nullable: false),
                    attendance_reason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true, defaultValue: ""),
                    execute_attendance = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    execute_door_access = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    company = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    is_pic = table.Column<short>(type: "smallint", nullable: false),
                    pin_room = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desk_booking_invitation_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "desk_controller",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    ip_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    capacity = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    created_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desk_controller__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "desk_controller_initial",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    socket = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    controller_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    desk_room_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    desk_id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desk_controller_initial_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "desk_invitation",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    nik = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    @internal = table.Column<int>(name: "internal", type: "int", nullable: false, defaultValue: 1),
                    attendance_status = table.Column<int>(type: "int", nullable: false),
                    attendance_reason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true, defaultValue: ""),
                    execute_attendance = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    execute_door_access = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    company = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    is_pic = table.Column<short>(type: "smallint", nullable: false),
                    pin_room = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desk_invitation_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "desk_room",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    building_id = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    automation_id = table.Column<int>(type: "int", nullable: false),
                    facility_room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    work_day = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    work_time = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    work_start = table.Column<TimeOnly>(type: "time", nullable: false),
                    work_end = table.Column<TimeOnly>(type: "time", nullable: false),
                    google_map = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    room_map = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<long>(type: "bigint", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_disabled = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "(NULL)"),
                    created_by = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desk_room__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "desk_room_table",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    desk_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    desk_room_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    zone_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    block_number = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    pointer_desk_x = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    pointer_desk_y = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    datetime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desk_room_table__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "desk_room_zone",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    desk_room_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    zone_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    pointer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    size = table.Column<double>(type: "float", nullable: true, defaultValueSql: "(NULL)"),
                    color = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desk_room_zone__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "device_player_integration",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    serial = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    hardware_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    uuid = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    mac = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    os = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    info = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    version = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    is_actived = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_device_player_integration__generate", x => new { x._generate, x.id });
                });

            migrationBuilder.CreateTable(
                name: "divisi",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id_divisi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    id_perusahaan = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    id_department = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    divisi_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    foto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    update_at = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_divisi_id_divisi", x => x.id_divisi);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    division_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    company_id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    department_id = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    nik = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nik_display = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    no_phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: " "),
                    no_ext = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValueSql: "(NULL)"),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(NULL)"),
                    gender = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    card_number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    card_number_real = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password_mobile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    gb_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fr_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    priority = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    is_vip = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    vip_approve_bypass = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    vip_limit_cap_bypass = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    vip_lock_room = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    head_employee_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    is_protected = table.Column<int>(type: "int", nullable: false, defaultValueSql: "(0)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "facility",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValue: ""),
                    google_icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facility_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "help_it_ga",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    booking_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    room_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    subject = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    problem_facility = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    problem_reason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    process_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    done_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    reject_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    response_done = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    response_reject = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    time_until_done_at = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    process_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    done_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    reject_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    created_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(0)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__help_it___3213E83F112681DD", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "helpdesk_monitor",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    room_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    datetime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    action = table.Column<int>(type: "int", nullable: false),
                    response = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reason_response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_helpdesk_monitor__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "http_url",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    headers = table.Column<string>(type: "TEXT", nullable: false),
                    is_deleted = table.Column<short>(type: "smallint", nullable: false),
                    is_enabled = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_http_url", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IdOnly",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdOnly", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "integration_365",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    refresh_token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    access_token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    display_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    userPrincipalName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    account_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    scope = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    refresh_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integration_365_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "kiosk_display",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    display_serial = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    display_type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    display_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    background = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    running_text = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    title_kiosk = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    display_uuid = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    display_hw_serial = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    koordinate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    is_logged = table.Column<short>(type: "smallint", nullable: true, defaultValue: (short)0),
                    last_logged = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kiosk_display_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "level",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    default_menu = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    sort_level = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_level_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "level_descriptiion",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    level_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_level_descriptiion_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "level_detail",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    level_id = table.Column<int>(type: "int", nullable: false),
                    menu_id = table.Column<int>(type: "int", nullable: false),
                    coment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_level_detail_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "level_header_detail",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    level_id = table.Column<int>(type: "int", nullable: false),
                    menu_id = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    coment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_level_header_detail_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "license_list",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    module = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    expired_at = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    is_lifetime = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    qty = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    platform_serial = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_license_list_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "license_setting",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serial = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    platform = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    device_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    checked_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    distributor_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    customer_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    ext = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    webhost = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    license_type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    pathdownload = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    created_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_license_setting_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "locker",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    ip_locker = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    auto_reserve = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    created_by = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locker__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "log",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    datetime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    activity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "log_access_room",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    room_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    is_default = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    pin = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true, defaultValueSql: "(NULL)"),
                    nik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    datetime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    msg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_access_room_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "log_activity",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nik = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    access_ip = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    access_action = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    access_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    access_time = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    access_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    access_query = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_activity__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "log_services_access_door",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    room_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    pin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    nik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    card = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    datetime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    msg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_services_access_door_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    type_icon = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    icon = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    sort = table.Column<int>(type: "int", nullable: false),
                    is_child = table.Column<int>(type: "int", nullable: false),
                    menu_group_id = table.Column<int>(type: "int", nullable: false),
                    module_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu_apps",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    type_icon = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    icon = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    sort = table.Column<int>(type: "int", nullable: false),
                    is_child = table.Column<int>(type: "int", nullable: false),
                    menu_group_id = table.Column<int>(type: "int", nullable: false),
                    module_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_apps_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu_group",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_group_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu_headers",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    sort = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    icon = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    module_text = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    created_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_headers__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "module_backend",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    module_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    module_text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    module_serial = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    is_enabled = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module_backend_module_id", x => x.module_id);
                });

            migrationBuilder.CreateTable(
                name: "notif_booking",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    notif_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    employee_id = table.Column<long>(type: "bigint", nullable: false),
                    is_reschedule = table.Column<int>(type: "int", nullable: false),
                    is_invited = table.Column<int>(type: "int", nullable: false),
                    is_notifhandler = table.Column<int>(type: "int", nullable: false),
                    is_notifSend = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notif_booking_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notification_admin",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    type = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    datetime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_read = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_sending = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_admin_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notification_config",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    authorization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    topics = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    active = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_config_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notification_data",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    type = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    datetime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    is_sending = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_data_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notification_type",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    cololr = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, defaultValueSql: "(NULL)"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    route = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    table = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "(NULL)"),
                    where = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    topics = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_type_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notification_type_admin",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    element = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    route = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_type_admin_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pantry",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    building_id = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    days = table.Column<int>(type: "int", nullable: false),
                    opening_hours_start = table.Column<TimeOnly>(type: "time", nullable: false),
                    opening_hours_end = table.Column<TimeOnly>(type: "time", nullable: false),
                    is_show_price = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    pic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employee_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_approval = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_internal = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    owner_pantry = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pantry_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pantry_detail",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pantry_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    pic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prefix_id = table.Column<int>(type: "int", nullable: false),
                    rasio = table.Column<int>(type: "int", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pantry_detail_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pantry_detail_menu_variant",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    menu_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    multiple = table.Column<int>(type: "int", nullable: false),
                    min = table.Column<int>(type: "int", nullable: false),
                    max = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pantry_detail_menu_variant_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pantry_detail_menu_variant_detail",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    variant_id = table.Column<string>(type: "nvarchar(33)", maxLength: 33, nullable: false),
                    price = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pantry_detail_menu_variant_detail_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pantry_display",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    display_serial = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "general"),
                    background = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    background_update = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    color_occupied = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true, defaultValue: ""),
                    color_available = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true, defaultValue: ""),
                    created_by = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: ""),
                    updated_by = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: ""),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    status_sync = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    enabled = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    hardware_uuid = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    hardware_info = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    hardware_lastsync = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    room_select = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    disable_msg = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pantry_display_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pantry_notif",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    notif_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    pantry_trs_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_notifhandler = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pantry_notif_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pantry_satuan",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pantry_satuan_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pantry_transaksi",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    pantry_id = table.Column<long>(type: "bigint", nullable: false),
                    order_no = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    employee_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_blive = table.Column<int>(type: "int", nullable: false),
                    room_id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    via = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    datetime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    order_datetime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    order_datetime_before = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    order_st = table.Column<int>(type: "int", nullable: false),
                    order_st_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    process = table.Column<int>(type: "int", nullable: false),
                    complete = table.Column<int>(type: "int", nullable: false),
                    failed = table.Column<int>(type: "int", nullable: false),
                    done = table.Column<int>(type: "int", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    note_reject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    note_canceled = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    is_rejected_pantry = table.Column<int>(type: "int", nullable: false),
                    rejected_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    rejected_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    is_trashpantry = table.Column<int>(type: "int", nullable: false),
                    is_canceled = table.Column<int>(type: "int", nullable: false),
                    is_expired = table.Column<int>(type: "int", nullable: false),
                    expired_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    canceled_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    canceled_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    completed_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    completed_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    process_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    process_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    canceled_pantry_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    rejected_pantry_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    completed_pantry_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    process_pantry_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    timezone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    from_pantry = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    to_pantry = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    pending = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    pending_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    package_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    approved_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    approved_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(getdate())"),
                    approved_head_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "(NULL)"),
                    approved_head_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    head_employee_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "(NULL)"),
                    approval_head = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pantry_transaksi_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pantry_transaksi_d",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transaksi_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    menu_id = table.Column<long>(type: "bigint", nullable: true),
                    qty = table.Column<int>(type: "int", nullable: false),
                    note_order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    note_reject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    detailorder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    is_rejected = table.Column<int>(type: "int", nullable: false),
                    rejected_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    rejected_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pantry_transaksi_d_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pantry_transaksi_status",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pantry_transaksi_status_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    radid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    building_id = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "(NULL)"),
                    floor_id = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "(NULL)"),
                    type_room = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, defaultValue: "single"),
                    kind_room = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "room"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    google_map = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_automation = table.Column<short>(type: "smallint", nullable: false),
                    automation_id = table.Column<int>(type: "int", nullable: false),
                    facility_room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    work_day = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    work_time = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    work_start = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    work_end = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "default.jpeg"),
                    image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    multiple_image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<long>(type: "bigint", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_disabled = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "(NULL)"),
                    is_beacon = table.Column<short>(type: "SMALLINT", nullable: true, defaultValue: (short)0),
                    created_by = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_config_setting_enable = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_room_for_usage = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    is_enable_approval = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_approval_user = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_enable_permission = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_permission_user = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    config_permission_checkin = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "pic"),
                    config_permission_end = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "pic"),
                    config_min_duration = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_max_duration = table.Column<int>(type: "int", nullable: true, defaultValue: 240),
                    config_advance_booking = table.Column<int>(type: "int", nullable: true, defaultValue: 7),
                    is_enable_recurring = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_enable_checkin = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_advance_checkin = table.Column<int>(type: "int", nullable: true, defaultValue: 5),
                    is_realease_checkin_timeout = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_release_room_checkin_timeout = table.Column<int>(type: "int", nullable: true, defaultValue: 10),
                    config_participant_checkin_count = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    is_enable_checkin_count = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_google = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    config_microsoft = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_365",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    emailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    displayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    geoCoordinates = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    nickname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    building = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    floorNumber = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    floorLabel = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    label = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    capacity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    bookingType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    audioDeviceName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    videoDeviceName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    displayDeviceName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    isWheelChairAccessible = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "(NULL)"),
                    tags = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    initial = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_365__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "room_automation",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ip_address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    serial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    devices = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_automation_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_detail",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    room_id = table.Column<long>(type: "bigint", maxLength: 255, nullable: true),
                    facility_id = table.Column<long>(type: "bigint", maxLength: 255, nullable: true),
                    datetime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_detail_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_display",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    room_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    display_serial = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "general"),
                    background = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    background_update = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    color_occupied = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true, defaultValue: ""),
                    color_available = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true, defaultValue: ""),
                    enable_signage = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    signage_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: ""),
                    signage_media = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    signage_update = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_by = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: ""),
                    updated_by = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: ""),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    status_sync = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    enabled = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    hardware_uuid = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    hardware_info = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    hardware_lastsync = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    room_select = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    disable_msg = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    building_id = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "(NULL)"),
                    floor_id = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_display_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_display_information",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    display_id = table.Column<long>(type: "bigint", nullable: true),
                    room_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    icon = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    distance = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_display_information__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "room_for_usage",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_for_usage_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_for_usage_detail",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    room_id = table.Column<long>(type: "bigint", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    room_usage_id = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    min_cap = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    @internal = table.Column<int>(name: "internal", type: "int", nullable: true, defaultValue: 0),
                    external = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "room_google",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    _generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    emailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    displayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    geoCoordinates = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    nickname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    building = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    floorNumber = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    floorLabel = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    label = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    capacity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    bookingType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    audioDeviceName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    videoDeviceName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    displayDeviceName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    isWheelChairAccessible = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "(NULL)"),
                    tags = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    initial = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_google__generate", x => x._generate);
                });

            migrationBuilder.CreateTable(
                name: "room_merge_detail",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    merge_room_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    room_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_merge_detail_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room_user_checkin",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    key = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_user_checkin_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sending_email",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    batch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    pending = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    error_sending = table.Column<int>(type: "int", nullable: false),
                    success = table.Column<int>(type: "int", nullable: false),
                    is_status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    is_deleted = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sending_email_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sending_notif",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    batch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    booking_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    pending = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    error_sending = table.Column<int>(type: "int", nullable: false),
                    success = table.Column<int>(type: "int", nullable: false),
                    is_status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    is_deleted = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sending_notif_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sending_text_status",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sending_text_status_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sessions",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(449)", maxLength: 449, nullable: false),
                    Value = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ExpiresAtTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SlidingExpirationInSeconds = table.Column<long>(type: "bigint", nullable: true),
                    AbsoluteExpiration = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sessions___3213E83F112681DD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "setting_email_template",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_enabled = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: ""),
                    title_of_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    to_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    title_agenda_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    date_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    room = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    detail_location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    greeting_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    content_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    attendance_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    attendance_no_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    close_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: ""),
                    support_text = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: ""),
                    foot_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    map_link_text = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "Direction map"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setting_email_template_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "setting_invoice_config",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_format = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    date_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    to_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    up_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    no_inv_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    no_profit_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    description_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    amount_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    content_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount_bill_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    tax_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    tax_amount = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    total_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    footer_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    footer2_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    footer3_text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setting_invoice_config_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "setting_invoice_text",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false, defaultValue: ""),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "s"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(getdate())"),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: " s"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(getdate())"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setting_invoice_text_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "setting_log_config",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setting_log_config_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "setting_pantry_config",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    pantry_expired = table.Column<int>(type: "int", nullable: false),
                    max_order_qty = table.Column<int>(type: "int", nullable: false),
                    before_order_meeting = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setting_pantry_config_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "setting_rule_booking",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    duration = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    if_unused_room = table.Column<int>(type: "int", nullable: false),
                    max_end_meeting = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    notif_unused_meeting = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    notif_unuse_before_meeting = table.Column<int>(type: "int", nullable: false),
                    unuse_cancel_fee = table.Column<int>(type: "int", nullable: false),
                    max_display_duration = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    room_pin = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "(NULL)"),
                    room_pin_number = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true, defaultValueSql: "(NULL)"),
                    room_pin_refresh = table.Column<TimeOnly>(type: "time", nullable: true, defaultValueSql: "(NULL)"),
                    extend_meeting = table.Column<int>(type: "int", nullable: true, defaultValue: 30),
                    extend_meeting_max = table.Column<int>(type: "int", nullable: true, defaultValue: 60),
                    extend_count_time = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    extend_meeting_notification = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    end_early_meeting = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    limit_time_booking = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_config_setting_enable = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_room_for_usage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    is_enable_approval = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_approval_user = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_enable_permission = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_permission_user = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    config_permission_checkin = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "pic"),
                    config_permission_end = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "pic"),
                    config_min_duration = table.Column<int>(type: "int", nullable: true, defaultValue: 15),
                    config_max_duration = table.Column<int>(type: "int", nullable: true, defaultValue: 240),
                    config_advance_booking = table.Column<int>(type: "int", nullable: true, defaultValue: 7),
                    is_enable_recurring = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_enable_checkin = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_advance_checkin = table.Column<int>(type: "int", nullable: true, defaultValue: 5),
                    is_realease_checkin_timeout = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_release_room_checkin_timeout = table.Column<int>(type: "int", nullable: true, defaultValue: 10),
                    config_participant_checkin_count = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_enable_checkin_count = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setting_rule_booking", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "setting_rule_deskbooking",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    duration = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    if_unused_room = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    max_end_meeting = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    notif_unused_meeting = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    notif_unuse_before_meeting = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    unuse_cancel_fee = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    max_display_duration = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    extend_meeting = table.Column<int>(type: "int", nullable: true, defaultValue: 30),
                    extend_meeting_max = table.Column<int>(type: "int", nullable: true, defaultValue: 60),
                    extend_count_time = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    extend_meeting_notification = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    end_early_meeting = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    limit_time_booking = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_config_setting_enable = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_min_duration = table.Column<int>(type: "int", nullable: true, defaultValue: 15),
                    config_max_duration = table.Column<int>(type: "int", nullable: true, defaultValue: 240),
                    config_advance_booking = table.Column<int>(type: "int", nullable: true, defaultValue: 7),
                    is_config_checkin_enable = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    config_enable_checkin = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    config_permission_checkin = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    config_permission_end = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    config_advance_checkin = table.Column<int>(type: "int", nullable: true, defaultValue: 7),
                    config_release_room_checkin_enable = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    config_release_room_checkin_time = table.Column<int>(type: "int", nullable: true, defaultValue: 10),
                    config_participant_checkin_count = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setting_rule_deskbooking_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "setting_smtp",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    selected_email = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_enabled = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    title_email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    host = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    user = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    port = table.Column<int>(type: "int", nullable: false),
                    secure = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    is_deleted = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setting_smtp_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "time_am_meeting",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    day = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValueSql: "(NULL)"),
                    is_disactivated = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_deleted = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_time_am_meeting_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "time_schedule_15",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    timeid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_time_schedule_15_timeid", x => x.timeid);
                });

            migrationBuilder.CreateTable(
                name: "time_schedule_30",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    timeid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_time_schedule_30_timeid", x => x.timeid);
                });

            migrationBuilder.CreateTable(
                name: "time_schedule_60",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    timeid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_time_schedule_60_timeid", x => x.timeid);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    secure_qr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    employee_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    real_password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    level_id = table.Column<int>(type: "int", nullable: false),
                    access_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "1"),
                    created_by = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(NULL)"),
                    is_disactived = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    is_vip = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    vip_approve_bypass = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    vip_limit_cap_bypass = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    vip_shifted_bypass = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_approval = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_protected = table.Column<int>(type: "int", nullable: false, defaultValueSql: "(0)"),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_access",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    access_id = table.Column<int>(type: "int", nullable: false),
                    access_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "(NULL)"),
                    is_active = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_access_access_id", x => x.access_id);
                });

            migrationBuilder.CreateTable(
                name: "user_config",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    default_password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password_length = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_config_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "variable_time_duration",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_variable_time_duration_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "variable_time_extend",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_variable_time_extend_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "department",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    id_perusahaan = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    department_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    foto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    update_at = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department_id_department", x => x.id);
                    table.ForeignKey(
                        name: "FK_department_company",
                        column: x => x.id_perusahaan,
                        principalSchema: "smart_meeting_room",
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pantry_menu_paket",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    pantry_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pantry_menu_paket_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_pantry_package",
                        column: x => x.pantry_id,
                        principalSchema: "smart_meeting_room",
                        principalTable: "pantry",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pantry_menu_paket_d",
                schema: "smart_meeting_room",
                columns: table => new
                {
                    menu_id = table.Column<long>(type: "bigint", nullable: false),
                    package_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    is_deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pantry_menu_paket_d", x => new { x.menu_id, x.package_id });
                    table.ForeignKey(
                        name: "FK_pantry_menu_detail",
                        column: x => x.menu_id,
                        principalSchema: "smart_meeting_room",
                        principalTable: "pantry_detail",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pantry_packageD",
                        column: x => x.package_id,
                        principalSchema: "smart_meeting_room",
                        principalTable: "pantry_menu_paket",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "alocation_matrix$_generate",
                schema: "smart_meeting_room",
                table: "alocation_matrix",
                column: "_generate",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "alocation_type$_generate",
                schema: "smart_meeting_room",
                table: "alocation_type",
                column: "_generate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_department_id_perusahaan",
                schema: "smart_meeting_room",
                table: "department",
                column: "id_perusahaan");

            migrationBuilder.CreateIndex(
                name: "employee$_generate",
                schema: "smart_meeting_room",
                table: "employee",
                column: "_generate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pantry_menu_paket_pantry_id",
                schema: "smart_meeting_room",
                table: "pantry_menu_paket",
                column: "pantry_id");

            migrationBuilder.CreateIndex(
                name: "IX_pantry_menu_paket_d_package_id",
                schema: "smart_meeting_room",
                table: "pantry_menu_paket_d",
                column: "package_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_channel",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "access_control",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "access_controller_falco",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "access_controller_type",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "access_integrated",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "alarm_integration",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "alocation",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "alocation_matrix",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "alocation_type",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "auth_serial",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "batch_upload",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "beacon_floor",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "booking",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "booking_alive",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "booking_invitation",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "booking_invoice",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "booking_invoice_detail",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "booking_invoice_generate",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "booking_room_trs",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "building",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "building_floor",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "department",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "desk_booking",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "desk_booking_invitation",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "desk_controller",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "desk_controller_initial",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "desk_invitation",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "desk_room",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "desk_room_table",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "desk_room_zone",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "device_player_integration",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "divisi",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "employee",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "facility",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "help_it_ga",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "helpdesk_monitor",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "http_url",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "IdOnly");

            migrationBuilder.DropTable(
                name: "integration_365",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "kiosk_display",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "level",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "level_descriptiion",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "level_detail",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "level_header_detail",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "license_list",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "license_setting",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "locker",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "log",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "log_access_room",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "log_activity",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "log_services_access_door",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "menu",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "menu_apps",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "menu_group",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "menu_headers",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "module_backend",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "notif_booking",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "notification_admin",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "notification_config",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "notification_data",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "notification_type",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "notification_type_admin",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "pantry_detail_menu_variant",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "pantry_detail_menu_variant_detail",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "pantry_display",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "pantry_menu_paket_d",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "pantry_notif",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "pantry_satuan",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "pantry_transaksi",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "pantry_transaksi_d",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "pantry_transaksi_status",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "room",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "room_365",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "room_automation",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "room_detail",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "room_display",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "room_display_information",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "room_for_usage",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "room_for_usage_detail",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "room_google",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "room_merge_detail",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "room_user_checkin",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "sending_email",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "sending_notif",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "sending_text_status",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "sessions",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "setting_email_template",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "setting_invoice_config",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "setting_invoice_text",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "setting_log_config",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "setting_pantry_config",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "setting_rule_booking",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "setting_rule_deskbooking",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "setting_smtp",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "time_am_meeting",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "time_schedule_15",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "time_schedule_30",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "time_schedule_60",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "user",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "user_access",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "user_config",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "variable_time_duration",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "variable_time_extend",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "company",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "pantry_detail",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "pantry_menu_paket",
                schema: "smart_meeting_room");

            migrationBuilder.DropTable(
                name: "pantry",
                schema: "smart_meeting_room");
        }
    }
}
