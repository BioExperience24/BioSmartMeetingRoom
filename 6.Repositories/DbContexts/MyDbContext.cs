using _6.Repositories.Repository;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.DB;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<IdOnly> IdOnly { get; set; }
    public virtual DbSet<AccessChannel> AccessChannels { get; set; }
    public virtual DbSet<AccessControl> AccessControls { get; set; }
    public virtual DbSet<AccessControllerFalco> AccessControllerFalcos { get; set; }
    public virtual DbSet<AccessControllerType> AccessControllerTypes { get; set; }
    public virtual DbSet<AccessIntegrated> AccessIntegrateds { get; set; }
    public virtual DbSet<AlarmIntegration> AlarmIntegrations { get; set; }
    public virtual DbSet<Alocation> Alocations { get; set; }
    public virtual DbSet<AlocationMatrix> AlocationMatrices { get; set; }
    public virtual DbSet<AlocationType> AlocationTypes { get; set; }
    public virtual DbSet<AuthSerial> AuthSerials { get; set; }
    public virtual DbSet<BatchUpload> BatchUploads { get; set; }
    public virtual DbSet<BeaconFloor> BeaconFloors { get; set; }
    public virtual DbSet<Booking> Bookings { get; set; }
    public virtual DbSet<BookingAlive> BookingAlives { get; set; }
    public virtual DbSet<BookingInvitation> BookingInvitations { get; set; }
    public virtual DbSet<BookingInvoice> BookingInvoices { get; set; }
    public virtual DbSet<BookingInvoiceDetail> BookingInvoiceDetails { get; set; }
    public virtual DbSet<BookingInvoiceGenerate> BookingInvoiceGenerates { get; set; }
    public virtual DbSet<BookingRoomTr> BookingRoomTrs { get; set; }
    public virtual DbSet<Building> Buildings { get; set; }
    public virtual DbSet<BuildingFloor> BuildingFloors { get; set; }
    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<DeskBooking> DeskBookings { get; set; }
    public virtual DbSet<DeskBookingInvitation> DeskBookingInvitations { get; set; }
    public virtual DbSet<DeskController> DeskControllers { get; set; }
    public virtual DbSet<DeskControllerInitial> DeskControllerInitials { get; set; }
    public virtual DbSet<DeskInvitation> DeskInvitations { get; set; }
    public virtual DbSet<DeskRoom> DeskRooms { get; set; }
    public virtual DbSet<DeskRoomTable> DeskRoomTables { get; set; }
    public virtual DbSet<DeskRoomZone> DeskRoomZones { get; set; }
    public virtual DbSet<DevicePlayerIntegration> DevicePlayerIntegrations { get; set; }
    public virtual DbSet<Divisi> Divisis { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Facility> Facilities { get; set; }
    public virtual DbSet<HelpdeskMonitor> HelpdeskMonitors { get; set; }
    public virtual DbSet<Integration365> Integration365s { get; set; }
    public virtual DbSet<KioskDisplay> KioskDisplays { get; set; }
    public virtual DbSet<Level> Levels { get; set; }
    public virtual DbSet<LevelDescriptiion> LevelDescriptiions { get; set; }
    public virtual DbSet<LevelDetail> LevelDetails { get; set; }
    public virtual DbSet<LevelHeaderDetail> LevelHeaderDetails { get; set; }
    public virtual DbSet<LicenseList> LicenseLists { get; set; }
    public virtual DbSet<LicenseSetting> LicenseSettings { get; set; }
    public virtual DbSet<Locker> Lockers { get; set; }
    public virtual DbSet<Log> Logs { get; set; }
    public virtual DbSet<LogAccessRoom> LogAccessRooms { get; set; }
    public virtual DbSet<LogActivity> LogActivities { get; set; }
    public virtual DbSet<LogServicesAccessDoor> LogServicesAccessDoors { get; set; }
    public virtual DbSet<Menu> Menus { get; set; }
    public virtual DbSet<MenuApp> MenuApps { get; set; }
    public virtual DbSet<MenuGroup> MenuGroups { get; set; }
    public virtual DbSet<MenuHeader> MenuHeaders { get; set; }
    public virtual DbSet<ModuleBackend> ModuleBackends { get; set; }
    public virtual DbSet<NotifBooking> NotifBookings { get; set; }
    public virtual DbSet<NotificationAdmin> NotificationAdmins { get; set; }
    public virtual DbSet<NotificationConfig> NotificationConfigs { get; set; }
    public virtual DbSet<NotificationData> NotificationData { get; set; }
    public virtual DbSet<NotificationType> NotificationTypes { get; set; }
    public virtual DbSet<NotificationTypeAdmin> NotificationTypeAdmins { get; set; }
    public virtual DbSet<Pantry> Pantries { get; set; }
    public virtual DbSet<PantryDetail> PantryDetails { get; set; }
    public virtual DbSet<PantryDetailMenuVariant> PantryDetailMenuVariants { get; set; }
    public virtual DbSet<PantryDetailMenuVariantDetail> PantryDetailMenuVariantDetails { get; set; }
    public virtual DbSet<PantryDisplay> PantryDisplays { get; set; }
    public virtual DbSet<PantryMenuPaket> PantryMenuPakets { get; set; }
    public virtual DbSet<PantryMenuPaketD> PantryMenuPaketDs { get; set; }
    public virtual DbSet<PantryNotif> PantryNotifs { get; set; }
    public virtual DbSet<PantrySatuan> PantrySatuans { get; set; }
    public virtual DbSet<PantryTransaksi> PantryTransaksis { get; set; }
    public virtual DbSet<PantryTransaksiD> PantryTransaksiDs { get; set; }
    public virtual DbSet<PantryTransaksiStatus> PantryTransaksiStatuses { get; set; }
    public virtual DbSet<Room> Rooms { get; set; }
    public virtual DbSet<RoomData> RoomDatas { get; set; }
    public virtual DbSet<Room365> Room365s { get; set; }
    public virtual DbSet<RoomAutomation> RoomAutomations { get; set; }
    public virtual DbSet<RoomDetail> RoomDetails { get; set; }
    public virtual DbSet<RoomDisplay> RoomDisplays { get; set; }
    public virtual DbSet<RoomForUsage> RoomForUsages { get; set; }
    public virtual DbSet<RoomForUsageDetail> RoomForUsageDetails { get; set; }
    public virtual DbSet<RoomGoogle> RoomGoogles { get; set; }
    public virtual DbSet<RoomMergeDetail> RoomMergeDetails { get; set; }
    public virtual DbSet<RoomUserCheckin> RoomUserCheckins { get; set; }
    public virtual DbSet<SendingEmail> SendingEmails { get; set; }
    public virtual DbSet<SendingNotif> SendingNotifs { get; set; }
    public virtual DbSet<SendingTextStatus> SendingTextStatuses { get; set; }
    public virtual DbSet<SettingEmailTemplate> SettingEmailTemplates { get; set; }
    public virtual DbSet<SettingInvoiceConfig> SettingInvoiceConfigs { get; set; }
    public virtual DbSet<SettingInvoiceText> SettingInvoiceTexts { get; set; }
    public virtual DbSet<SettingLogConfig> SettingLogConfigs { get; set; }
    public virtual DbSet<SettingPantryConfig> SettingPantryConfigs { get; set; }
    public virtual DbSet<SettingRuleBooking> SettingRuleBookings { get; set; }
    public virtual DbSet<SettingRuleDeskbooking> SettingRuleDeskbookings { get; set; }
    public virtual DbSet<SettingSmtp> SettingSmtps { get; set; }
    public virtual DbSet<TimeAmMeeting> TimeAmMeetings { get; set; }
    public virtual DbSet<TimeSchedule15> TimeSchedule15s { get; set; }
    public virtual DbSet<TimeSchedule30> TimeSchedule30s { get; set; }
    public virtual DbSet<TimeSchedule60> TimeSchedule60s { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserAccess> UserAccesses { get; set; }
    public virtual DbSet<UserConfig> UserConfigs { get; set; }
    public virtual DbSet<VariableTimeDuration> VariableTimeDurations { get; set; }
    public virtual DbSet<VariableTimeExtend> VariableTimeExtends { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccessChannelConfiguration());
        modelBuilder.ApplyConfiguration(new AccessControlConfiguration());
        modelBuilder.ApplyConfiguration(new PantryMenuPaketConfiguration());
        modelBuilder.ApplyConfiguration(new PackageDConfiguration());
        modelBuilder.ApplyConfiguration(new PantryTransaksiConfiguration());

        modelBuilder.Entity<AccessControllerFalco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_access_controller_falco_id");

            entity.ToTable("access_controller_falco", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccessId)
                .HasMaxLength(32)
                .HasColumnName("access_id");
            entity.Property(e => e.FalcoIp)
                .HasMaxLength(32)
                .HasColumnName("falco_ip");
            entity.Property(e => e.GroupAccess)
                .HasMaxLength(10)
                .HasColumnName("group_access");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.UnitNo)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("unit_no");
        });

        modelBuilder.Entity<AccessControllerType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_access_controller_type_id");

            entity.ToTable("access_controller_type", "smart_meeting_room");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AccessIntegrated>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_access_integrated_id");

            entity.ToTable("access_integrated", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccessId)
                .HasMaxLength(15)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("access_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.RoomId)
                .HasMaxLength(15)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("room_id");
        });

        modelBuilder.Entity<AlarmIntegration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_alarm_integration_id");

            entity.ToTable("alarm_integration", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(0)
                .HasColumnName("active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(0)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ParamAuth)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("param_auth");
            entity.Property(e => e.ParamFeed)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("param_feed");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("password");
            entity.Property(e => e.StatusIntegration)
                .HasDefaultValue(0)
                .HasColumnName("status_integration");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("updated_by");
            entity.Property(e => e.UrlAuth)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("url_auth");
            entity.Property(e => e.UrlFeedback)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("url_feedback");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("username");
        });

        modelBuilder.Entity<Alocation>(entity =>
        {
            entity.HasKey(e => e.Generate).HasName("PK_alocation__generate");

            entity.ToTable("alocation", "smart_meeting_room");

            entity.Property(e => e.Generate).HasColumnName("_generate");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("created_by");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(43)
                .HasDefaultValue("")
                .HasColumnName("department_code");
            entity.Property(e => e.Id)
                .HasMaxLength(40)
                .HasDefaultValue("")
                .HasColumnName("id");
            entity.Property(e => e.InvoiceStatus)
                .HasDefaultValue(0)
                .HasColumnName("invoice_status");
            entity.Property(e => e.InvoiceType)
                .HasDefaultValue(0)
                .HasColumnName("invoice_type");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsPermanent).HasColumnName("is_permanent");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.ShowInInvitation)
                .HasDefaultValue(1)
                .HasColumnName("show_in_invitation");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<AlocationMatrix>(entity =>
        {
            entity.HasKey(e => e.Generate).HasName("PK_alocation_matrix__generate");

            entity
                // .HasNoKey()
                .ToTable("alocation_matrix", "smart_meeting_room");

            entity.HasIndex(e => e.Generate, "alocation_matrix$_generate")
                .IsUnique()
                .IsClustered();

            entity.Property(e => e.AlocationId)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("alocation_id");
            entity.Property(e => e.Generate)
                .ValueGeneratedOnAdd()
                .HasColumnName("_generate");
            entity.Property(e => e.Nik)
                .HasMaxLength(40)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("nik");
        });

        modelBuilder.Entity<AlocationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_alocation_type_id");

            entity.ToTable("alocation_type", "smart_meeting_room");

            entity.HasIndex(e => e.Generate, "alocation_type$_generate").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Generate)
                .ValueGeneratedOnAdd()
                .HasColumnName("_generate");
            entity.Property(e => e.InvoiceStatus)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("invoice_status");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsPermanent).HasColumnName("is_permanent");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<AuthSerial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_auth_serial_id");

            entity.ToTable("auth_serial", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Serial).HasColumnName("serial");
        });

        modelBuilder.Entity<BatchUpload>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_batch_upload_id");

            entity.ToTable("batch_upload", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Time)
                .HasPrecision(0)
                .HasColumnName("time");
            entity.Property(e => e.TotalRow).HasColumnName("total_row");
            entity.Property(e => e.TotalSize)
                .HasMaxLength(255)
                .HasColumnName("total_size");
        });

        modelBuilder.Entity<BeaconFloor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_beacon_floor_id");

            entity.ToTable("beacon_floor", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BuildingId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("building_id");
            entity.Property(e => e.CenterX)
                .HasDefaultValue(0)
                .HasColumnName("center_x");
            entity.Property(e => e.CenterY)
                .HasDefaultValue(0)
                .HasColumnName("center_y");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.FloorLength)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("floor_length");
            entity.Property(e => e.FloorWidth)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("floor_width");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("image");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.MeterPerPx)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("meter_per_px");
            entity.Property(e => e.MeterPerPx2)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("meter_per_px2");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name");
            entity.Property(e => e.Pixel)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("pixel");
            entity.Property(e => e.PlusHeight)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("plus_height");
            entity.Property(e => e.PlusWidth)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("plus_width");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_booking_id");

            entity.ToTable("booking", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlocationId)
                .HasMaxLength(20)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("alocation_id");
            entity.Property(e => e.AlocationName).HasColumnName("alocation_name");
            entity.Property(e => e.BookingDevices)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("booking_devices");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .HasColumnName("booking_id");
            entity.Property(e => e.BookingId365).HasColumnName("booking_id_365");
            entity.Property(e => e.BookingIdGoogle).HasColumnName("booking_id_google");
            entity.Property(e => e.CanceledAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("canceled_at");
            entity.Property(e => e.CanceledBy)
                .HasMaxLength(100)
                .HasColumnName("canceled_by");
            entity.Property(e => e.CanceledNote)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("canceled_note");
            entity.Property(e => e.Category)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("category");
            entity.Property(e => e.CheckinCount)
                .HasDefaultValue(1)
                .HasColumnName("checkin_count");
            entity.Property(e => e.CleaningEnd)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("cleaning_end");
            entity.Property(e => e.CleaningStart)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("cleaning_start");
            entity.Property(e => e.CleaningTime)
                .HasDefaultValue(0)
                .HasColumnName("cleaning_time");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CostTotalBooking)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("cost_total_booking");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DurationPerMeeting)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("duration_per_meeting");
            entity.Property(e => e.DurationSavedRelease)
                .HasDefaultValue(0)
                .HasColumnName("duration_saved_release");
            entity.Property(e => e.EarlyEndedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("early_ended_at");
            entity.Property(e => e.EarlyEndedBy)
                .HasMaxLength(100)
                .HasColumnName("early_ended_by");
            entity.Property(e => e.End)
                .HasPrecision(0)
                .HasColumnName("end");
            entity.Property(e => e.EndEarlyMeeting).HasColumnName("end_early_meeting");
            entity.Property(e => e.ExpiredAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("expired_at");
            entity.Property(e => e.ExpiredBy)
                .HasMaxLength(100)
                .HasColumnName("expired_by");
            entity.Property(e => e.ExtendedDuration)
                .HasDefaultValue(0)
                .HasColumnName("extended_duration");
            entity.Property(e => e.ExternalLink)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("external_link");
            entity.Property(e => e.ExternalLink365).HasColumnName("external_link_365");
            entity.Property(e => e.ExternalLinkGoogle).HasColumnName("external_link_google");
            entity.Property(e => e.IsAccessTrigger)
                .HasDefaultValue(0)
                .HasColumnName("is_access_trigger");
            entity.Property(e => e.IsAlive)
                .HasDefaultValue(1)
                .HasColumnName("is_alive");
            entity.Property(e => e.IsApprove)
                .HasDefaultValue(0)
                .HasColumnName("is_approve");
            entity.Property(e => e.IsCanceled).HasColumnName("is_canceled");
            entity.Property(e => e.IsCleaningNeed)
                .HasDefaultValue(0)
                .HasColumnName("is_cleaning_need");
            entity.Property(e => e.IsConfigSettingEnable)
                .HasDefaultValue(0)
                .HasColumnName("is_config_setting_enable");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsDevice)
                .HasDefaultValue(0)
                .HasColumnName("is_device");
            entity.Property(e => e.IsEar).HasColumnName("is_ear");
            entity.Property(e => e.IsEnableApproval)
                .HasDefaultValue(0)
                .HasColumnName("is_enable_approval");
            entity.Property(e => e.IsEnableCheckin)
                .HasDefaultValue(0)
                .HasColumnName("is_enable_checkin");
            entity.Property(e => e.IsEnableCheckinCount)
                .HasDefaultValue(1)
                .HasColumnName("is_enable_checkin_count");
            entity.Property(e => e.IsEnablePermission)
                .HasDefaultValue(240)
                .HasColumnName("is_enable_permission");
            entity.Property(e => e.IsEnableRecurring)
                .HasDefaultValue(0)
                .HasColumnName("is_enable_recurring");
            entity.Property(e => e.IsExpired).HasColumnName("is_expired");
            entity.Property(e => e.IsMeal).HasColumnName("is_meal");
            entity.Property(e => e.IsMerge)
                .HasDefaultValue((short)0)
                .HasColumnName("is_merge");
            entity.Property(e => e.IsMoved)
                .HasDefaultValue(0)
                .HasColumnName("is_moved");
            entity.Property(e => e.IsMovedAgree)
                .HasDefaultValue(0)
                .HasColumnName("is_moved_agree");
            entity.Property(e => e.IsNotifBeforeEndMeeting).HasColumnName("is_notif_before_end_meeting");
            entity.Property(e => e.IsNotifEndMeeting).HasColumnName("is_notif_end_meeting");
            entity.Property(e => e.IsRealeaseCheckinTimeout)
                .HasDefaultValue(1)
                .HasColumnName("is_realease_checkin_timeout");
            entity.Property(e => e.IsReleased)
                .HasDefaultValue(0)
                .HasColumnName("is_released");
            entity.Property(e => e.IsRescheduled).HasColumnName("is_rescheduled");
            entity.Property(e => e.IsVip)
                .HasDefaultValue(0)
                .HasColumnName("is_vip");
            entity.Property(e => e.LastModifiedDateTime365)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("lastModifiedDateTime_365");
            entity.Property(e => e.MeetingEndNote)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("meeting_end_note");
            entity.Property(e => e.MergeRoom).HasColumnName("merge_room");
            entity.Property(e => e.MergeRoomId)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("merge_room_id");
            entity.Property(e => e.MergeRoomName)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("merge_room_name");
            entity.Property(e => e.MovedDuration)
                .HasDefaultValue(5)
                .HasColumnName("moved_duration");
            entity.Property(e => e.NoOrder).HasColumnName("no_order");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Participants).HasColumnName("participants");
            entity.Property(e => e.PermissionCheckin)
                .HasMaxLength(255)
                .HasDefaultValue("pic")
                .HasColumnName("permission_checkin");
            entity.Property(e => e.PermissionEnd)
                .HasMaxLength(255)
                .HasDefaultValue("pic")
                .HasColumnName("permission_end");
            entity.Property(e => e.Pic)
                .HasMaxLength(255)
                .HasColumnName("pic");
            entity.Property(e => e.ReleaseRoomCheckinTime)
                .HasDefaultValue(10)
                .HasColumnName("release_room_checkin_time");
            entity.Property(e => e.RescheduledAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("rescheduled_at");
            entity.Property(e => e.RescheduledBy)
                .HasMaxLength(100)
                .HasColumnName("rescheduled_by");
            entity.Property(e => e.RoomId)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("room_id");
            entity.Property(e => e.RoomMeetingMove)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("room_meeting_move");
            entity.Property(e => e.RoomMeetingOld)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("room_meeting_old");
            entity.Property(e => e.RoomName)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("room_name");
            entity.Property(e => e.ServerDate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("server_date");
            entity.Property(e => e.ServerEnd)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("server_end");
            entity.Property(e => e.ServerStart)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("server_start");
            entity.Property(e => e.Start)
                .HasPrecision(0)
                .HasColumnName("start");
            entity.Property(e => e.TextEarly)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("text_early");
            entity.Property(e => e.Timezone)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("timezone");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.TotalDuration)
                .HasDefaultValue(0)
                .HasColumnName("total_duration");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
            entity.Property(e => e.UserApproval)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("user_approval");
            entity.Property(e => e.UserApprovalDatetime)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("user_approval_datetime");
            entity.Property(e => e.UserCheckin)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("user_checkin");
            entity.Property(e => e.UserCleaning)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("user_cleaning");
            entity.Property(e => e.UserEndMeeting)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("user_end_meeting");
            entity.Property(e => e.VipApproveBypass)
                .HasDefaultValue(0)
                .HasColumnName("vip_approve_bypass");
            entity.Property(e => e.VipForceMoved)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("vip_force_moved");
            entity.Property(e => e.VipLimitCapBypass)
                .HasDefaultValue(0)
                .HasColumnName("vip_limit_cap_bypass");
            entity.Property(e => e.VipLockRoom)
                .HasDefaultValue(0)
                .HasColumnName("vip_lock_room");
            entity.Property(e => e.VipUser)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("vip_user");
            entity.Property(e => e.BookingType)
                .HasMaxLength(20)
                .HasDefaultValue("general")
                .HasColumnName("booking_type");
            entity.Property(e => e.IsPrivate)
                .HasDefaultValue(0)
                .HasColumnName("is_private");
        });

        modelBuilder.Entity<BookingAlive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_booking_alive_id");

            entity.ToTable("booking_alive", "smart_meeting_room");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<BookingInvitation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_booking_invitation_id");

            entity.ToTable("booking_invitation", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttendanceReason)
                .HasMaxLength(300)
                .HasDefaultValue("")
                .HasColumnName("attendance_reason");
            entity.Property(e => e.AttendanceStatus).HasColumnName("attendance_status");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .HasColumnName("booking_id");
            entity.Property(e => e.Checkin)
                .HasDefaultValue(0)
                .HasColumnName("checkin");
            entity.Property(e => e.Company)
                .HasMaxLength(200)
                .HasDefaultValue("")
                .HasColumnName("company");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasDefaultValue("")
                .HasColumnName("email");
            entity.Property(e => e.EndMeeting)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("end_meeting");
            entity.Property(e => e.ExecuteAttendance)
                .HasDefaultValue(0)
                .HasColumnName("execute_attendance");
            entity.Property(e => e.ExecuteDoorAccess).HasColumnName("execute_door_access");
            entity.Property(e => e.Internal)
                .HasDefaultValue(1)
                .HasColumnName("internal");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsPic).HasColumnName("is_pic");
            entity.Property(e => e.IsVip)
                .HasDefaultValue(0)
                .HasColumnName("is_vip");
            entity.Property(e => e.LastUpdate365)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("lastUpdate_365");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.Nik)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("nik");
            entity.Property(e => e.PinRoom)
                .HasMaxLength(11)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("pin_room");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("position");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<BookingInvoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_booking_invoice_id");

            entity.ToTable("booking_invoice", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alocation)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("alocation");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("booking_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.InvoiceFormat)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("invoice_format");
            entity.Property(e => e.InvoiceGenerateNo)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("invoice_generate_no");
            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("invoice_no");
            entity.Property(e => e.InvoiceStatus)
                .HasMaxLength(11)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("invoice_status");
            entity.Property(e => e.MemoNo)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("memo_no");
            entity.Property(e => e.ReferensiNo)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("referensi_no");
            entity.Property(e => e.RentCost)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("rent_cost");
            entity.Property(e => e.TimeBefore)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("time_before");
            entity.Property(e => e.TimePaid)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("time_paid");
            entity.Property(e => e.TimeSend)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("time_send");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(0)
                .HasColumnName("is_deleted");
        });

        modelBuilder.Entity<BookingInvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_booking_invoice_detail_id");

            entity.ToTable("booking_invoice_detail", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlocationId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("alocation_id");
            entity.Property(e => e.AlocationName).HasColumnName("alocation_name");
            entity.Property(e => e.AlocationType)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("alocation_type");
            entity.Property(e => e.CostCode)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("cost_code");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("created_by");
            entity.Property(e => e.InvoiceId)
                .HasMaxLength(20)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("invoice_id");
            entity.Property(e => e.InvoiceStatus)
                .HasMaxLength(5)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("invoice_status");
            entity.Property(e => e.NoInvoice).HasColumnName("no_invoice");
            entity.Property(e => e.NoUrut)
                .HasMaxLength(6)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("no_urut");
            entity.Property(e => e.OutstandingStatus)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("outstanding_status");
            entity.Property(e => e.PaidAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("paid_at");
            entity.Property(e => e.PaidBy)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("paid_by");
            entity.Property(e => e.SentAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("sent_at");
            entity.Property(e => e.SentBy)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("sent_by");
            entity.Property(e => e.TotalCost)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("total_cost");
            entity.Property(e => e.TotalDuration)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("total_duration");
            entity.Property(e => e.TotalMeeting)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("total_meeting");
        });

        modelBuilder.Entity<BookingInvoiceGenerate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_booking_invoice_generate_id");

            entity.ToTable("booking_invoice_generate", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlocationId)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("alocation_id");
            entity.Property(e => e.ConfirmBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("confirm_by");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.DateConfirm)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("date_confirm");
            entity.Property(e => e.DateGenerate)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("date_generate");
            entity.Property(e => e.DateSending)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("date_sending");
            entity.Property(e => e.GenerateBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("generate_by");
            entity.Property(e => e.InvoiceFormat)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("invoice_format");
            entity.Property(e => e.InvoiceId)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("invoice_id");
            entity.Property(e => e.InvoiceMonth1)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("invoice_month1");
            entity.Property(e => e.InvoiceMonth2)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("invoice_month2");
            entity.Property(e => e.InvoiceYears)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("invoice_years");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(0)
                .HasColumnName("is_deleted");
            entity.Property(e => e.MemoNo)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("memo_no");
            entity.Property(e => e.ReferensiNo)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("referensi_no");
            entity.Property(e => e.SendingBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("sending_by");
            entity.Property(e => e.Status)
                .HasMaxLength(5)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("status");
            entity.Property(e => e.TotalCost)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("total_cost");
            entity.Property(e => e.TotalDuration)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("total_duration");
            entity.Property(e => e.TotalMeeting)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("total_meeting");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<BookingRoomTr>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("booking_room_trs", "smart_meeting_room");

            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("booking_id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("date");
            entity.Property(e => e.RoomId)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("room_id");
        });

        modelBuilder.Entity<Building>(entity =>
        {
             entity.HasKey(e => e.Generate).HasName("PK_building_generate");

            entity.ToTable("building", "smart_meeting_room");

             entity.Property(e => e.Generate).HasColumnName("generate");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DetailAddress).HasColumnName("detail_address");
            entity.Property(e => e.GoogleMap)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("google_map");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasDefaultValue("default.jpeg")
                .HasColumnName("image");
            entity.Property(e => e.IsDeleted)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Koordinate)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("koordinate");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name");
            entity.Property(e => e.Timezone)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("timezone");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<BuildingFloor>(entity =>
        {
             entity.HasKey(e => e.Generate).HasName("PK_building_floor__generate");

            entity.ToTable("building_floor", "smart_meeting_room");

             entity.Property(e => e.Generate).HasColumnName("_generate");
            entity.Property(e => e.BuildingId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("building_id");
            entity.Property(e => e.CenterX)
                .HasDefaultValue(0)
                .HasColumnName("center_x");
            entity.Property(e => e.CenterY)
                .HasDefaultValue(0)
                .HasColumnName("center_y");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.FloorLength)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("floor_length");
            entity.Property(e => e.FloorWidth)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("floor_width");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("image");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.MeterPerPx)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("meter_per_px");
            entity.Property(e => e.MeterPerPx2)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("meter_per_px2");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name");
            entity.Property(e => e.Pixel)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("pixel");
            entity.Property(e => e.PlusHeight)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("plus_height");
            entity.Property(e => e.PlusWidth)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("plus_width");
            entity.Property(e => e.Position)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("position");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_company_id");

            entity.ToTable("company", "smart_meeting_room");

            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Icon)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("icon");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("logo");
            entity.Property(e => e.MenuBar)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("menu_bar");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Picture)
                .HasMaxLength(200)
                .HasColumnName("picture");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
            entity.Property(e => e.UpdateAt).HasColumnName("update_at");
            entity.Property(e => e.UrlAddress).HasColumnName("url_address");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_department_id_department");

            entity.ToTable("department", "smart_meeting_room");

            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(200)
                .HasColumnName("department_name");
            entity.Property(e => e.Foto)
                .HasMaxLength(200)
                .HasColumnName("foto");
            entity.Property(e => e.IdPerusahaan)
                .HasMaxLength(20)
                .HasColumnName("id_perusahaan");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.UpdateAt).HasColumnName("update_at");

            // Relasi ke Company
            entity.HasOne(d => d.Company) // Navigation Property
                .WithMany() // Tidak ada koleksi Department di Company
                .HasForeignKey(d => d.IdPerusahaan) // Foreign key adalah id_perusahaan
                .HasConstraintName("FK_department_company");
        });

        modelBuilder.Entity<DeskBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_desk_booking_id");

            entity.ToTable("desk_booking", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlocationId)
                .HasMaxLength(20)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("alocation_id");
            entity.Property(e => e.AlocationName).HasColumnName("alocation_name");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .HasColumnName("booking_id");
            entity.Property(e => e.CanceledAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("canceled_at");
            entity.Property(e => e.CanceledBy)
                .HasMaxLength(100)
                .HasColumnName("canceled_by");
            entity.Property(e => e.CanceledNote)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("canceled_note");
            entity.Property(e => e.CostTotalBooking)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("cost_total_booking");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DeskId)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("desk_id");
            entity.Property(e => e.DeskName)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("desk_name");
            entity.Property(e => e.DurationPerMeeting)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("duration_per_meeting");
            entity.Property(e => e.EarlyEndedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("early_ended_at");
            entity.Property(e => e.EarlyEndedBy)
                .HasMaxLength(100)
                .HasColumnName("early_ended_by");
            entity.Property(e => e.End)
                .HasPrecision(0)
                .HasColumnName("end");
            entity.Property(e => e.EndEarlyMeeting)
                .HasDefaultValue(0)
                .HasColumnName("end_early_meeting");
            entity.Property(e => e.ExpiredAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("expired_at");
            entity.Property(e => e.ExpiredBy)
                .HasMaxLength(100)
                .HasColumnName("expired_by");
            entity.Property(e => e.ExtendedDuration)
                .HasDefaultValue(0)
                .HasColumnName("extended_duration");
            entity.Property(e => e.IsAccessTrigger)
                .HasDefaultValue(0)
                .HasColumnName("is_access_trigger");
            entity.Property(e => e.IsAlive)
                .HasDefaultValue(1)
                .HasColumnName("is_alive");
            entity.Property(e => e.IsCanceled).HasColumnName("is_canceled");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsDevice)
                .HasDefaultValue(0)
                .HasColumnName("is_device");
            entity.Property(e => e.IsEar)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_ear");
            entity.Property(e => e.IsExpired).HasColumnName("is_expired");
            entity.Property(e => e.IsMeal).HasColumnName("is_meal");
            entity.Property(e => e.IsNotifBeforeEndMeeting).HasColumnName("is_notif_before_end_meeting");
            entity.Property(e => e.IsNotifEndMeeting).HasColumnName("is_notif_end_meeting");
            entity.Property(e => e.IsRescheduled).HasColumnName("is_rescheduled");
            entity.Property(e => e.NoOrder).HasColumnName("no_order");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Participants).HasColumnName("participants");
            entity.Property(e => e.Pic)
                .HasMaxLength(255)
                .HasColumnName("pic");
            entity.Property(e => e.RescheduledAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("rescheduled_at");
            entity.Property(e => e.RescheduledBy)
                .HasMaxLength(100)
                .HasColumnName("rescheduled_by");
            entity.Property(e => e.RoomId)
                .HasMaxLength(32)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("room_id");
            entity.Property(e => e.RoomName)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("room_name");
            entity.Property(e => e.Start)
                .HasPrecision(0)
                .HasColumnName("start");
            entity.Property(e => e.TextEarly)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("text_early");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.TotalDuration)
                .HasDefaultValue(0)
                .HasColumnName("total_duration");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<DeskBookingInvitation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_desk_booking_invitation_id");

            entity.ToTable("desk_booking_invitation", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttendanceReason)
                .HasMaxLength(300)
                .HasDefaultValue("")
                .HasColumnName("attendance_reason");
            entity.Property(e => e.AttendanceStatus).HasColumnName("attendance_status");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .HasColumnName("booking_id");
            entity.Property(e => e.Company)
                .HasMaxLength(200)
                .HasDefaultValue("")
                .HasColumnName("company");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasDefaultValue("")
                .HasColumnName("email");
            entity.Property(e => e.ExecuteAttendance)
                .HasDefaultValue(0)
                .HasColumnName("execute_attendance");
            entity.Property(e => e.ExecuteDoorAccess).HasColumnName("execute_door_access");
            entity.Property(e => e.Internal)
                .HasDefaultValue(1)
                .HasColumnName("internal");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsPic).HasColumnName("is_pic");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.Nik)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("nik");
            entity.Property(e => e.PinRoom)
                .HasMaxLength(11)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("pin_room");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("position");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<DeskController>(entity =>
        {
            entity.HasKey(e => e.Generate).HasName("PK_desk_controller__generate");

            entity.ToTable("desk_controller", "smart_meeting_room");

            entity.Property(e => e.Generate).HasColumnName("_generate");
            entity.Property(e => e.Capacity)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("capacity");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("id");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("ip_address");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<DeskControllerInitial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_desk_controller_initial_id");

            entity.ToTable("desk_controller_initial", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ControllerId)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("controller_id");
            entity.Property(e => e.DeskId)
                .HasMaxLength(32)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("desk_id");
            entity.Property(e => e.DeskRoomId)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("desk_room_id");
            entity.Property(e => e.Socket)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("socket");
        });

        modelBuilder.Entity<DeskInvitation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_desk_invitation_id");

            entity.ToTable("desk_invitation", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttendanceReason)
                .HasMaxLength(300)
                .HasDefaultValue("")
                .HasColumnName("attendance_reason");
            entity.Property(e => e.AttendanceStatus).HasColumnName("attendance_status");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .HasColumnName("booking_id");
            entity.Property(e => e.Company)
                .HasMaxLength(200)
                .HasDefaultValue("")
                .HasColumnName("company");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasDefaultValue("")
                .HasColumnName("email");
            entity.Property(e => e.ExecuteAttendance)
                .HasDefaultValue(0)
                .HasColumnName("execute_attendance");
            entity.Property(e => e.ExecuteDoorAccess).HasColumnName("execute_door_access");
            entity.Property(e => e.Internal)
                .HasDefaultValue(1)
                .HasColumnName("internal");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsPic).HasColumnName("is_pic");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.Nik)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("nik");
            entity.Property(e => e.PinRoom)
                .HasMaxLength(11)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("pin_room");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("position");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<DeskRoom>(entity =>
        {
            entity.HasKey(e => e.Generate).HasName("PK_desk_room__generate");

            entity.ToTable("desk_room", "smart_meeting_room");

            entity.Property(e => e.Generate).HasColumnName("_generate");
            entity.Property(e => e.AutomationId).HasColumnName("automation_id");
            entity.Property(e => e.BuildingId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("building_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FacilityRoom).HasColumnName("facility_room");
            entity.Property(e => e.GoogleMap)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("google_map");
            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Image2).HasColumnName("image2");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsDisabled)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_disabled");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RoomMap).HasColumnName("room_map");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkDay)
                .HasMaxLength(255)
                .HasColumnName("work_day");
            entity.Property(e => e.WorkEnd).HasColumnName("work_end");
            entity.Property(e => e.WorkStart).HasColumnName("work_start");
            entity.Property(e => e.WorkTime)
                .HasMaxLength(30)
                .HasColumnName("work_time");
        });

        modelBuilder.Entity<DeskRoomTable>(entity =>
        {
            entity.HasKey(e => e.Generate).HasName("PK_desk_room_table__generate");

            entity.ToTable("desk_room_table", "smart_meeting_room");

            entity.Property(e => e.Generate).HasColumnName("_generate");
            entity.Property(e => e.BlockNumber)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("block_number");
            entity.Property(e => e.Datetime)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("datetime");
            entity.Property(e => e.DeskId)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("desk_id");
            entity.Property(e => e.DeskRoomId)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("desk_room_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.PointerDeskX)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("pointer_desk_x");
            entity.Property(e => e.PointerDeskY)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("pointer_desk_y");
            entity.Property(e => e.ZoneId)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("zone_id");
        });

        modelBuilder.Entity<DeskRoomZone>(entity =>
        {
            entity.HasKey(e => e.Generate).HasName("PK_desk_room_zone__generate");

            entity.ToTable("desk_room_zone", "smart_meeting_room");

            entity.Property(e => e.Generate).HasColumnName("_generate");
            entity.Property(e => e.Color)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("color");
            entity.Property(e => e.DeskRoomId)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("desk_room_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name");
            entity.Property(e => e.Pointer)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("pointer");
            entity.Property(e => e.Size)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("size");
            entity.Property(e => e.ZoneId)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("zone_id");
        });

        modelBuilder.Entity<DevicePlayerIntegration>(entity =>
        {
            entity.HasKey(e => new { e.Generate, e.Id }).HasName("PK_device_player_integration__generate");

            entity.ToTable("device_player_integration", "smart_meeting_room");

            entity.Property(e => e.Generate)
                .ValueGeneratedOnAdd()
                .HasColumnName("_generate");
            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.HardwareId)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("hardware_id");
            entity.Property(e => e.Info)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("info");
            entity.Property(e => e.IsActived)
                .HasDefaultValue(0)
                .HasColumnName("is_actived");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Mac)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("mac");
            entity.Property(e => e.Os)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("os");
            entity.Property(e => e.Serial)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("serial");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("type");
            entity.Property(e => e.Uuid)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("uuid");
            entity.Property(e => e.Version)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("version");
        });

        modelBuilder.Entity<Divisi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_divisi_id_divisi");

            entity.ToTable("divisi", "smart_meeting_room");

            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .HasColumnName("id_divisi");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DivisiName)
                .HasMaxLength(200)
                .HasColumnName("divisi_name");
            entity.Property(e => e.Foto)
                .HasMaxLength(200)
                .HasColumnName("foto");
            entity.Property(e => e.IdDepartment)
                .HasMaxLength(20)
                .HasColumnName("id_department");
            entity.Property(e => e.IdPerusahaan)
                .HasMaxLength(20)
                .HasColumnName("id_perusahaan");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.UpdateAt).HasColumnName("update_at");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_employee_id");

            entity.ToTable("employee", "smart_meeting_room");

            entity.HasIndex(e => e.Generate, "employee$_generate").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("birth_date");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(100)
                .HasColumnName("card_number");
            entity.Property(e => e.CardNumberReal)
                .HasMaxLength(100)
                .HasColumnName("card_number_real");
            entity.Property(e => e.CompanyId)
                .HasMaxLength(20)
                .HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(200)
                .HasColumnName("department_id");
            entity.Property(e => e.DivisionId)
                .HasMaxLength(50)
                .HasColumnName("division_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FrId)
                .HasMaxLength(10)
                .HasColumnName("fr_id");
            entity.Property(e => e.GbId)
                .HasMaxLength(100)
                .HasColumnName("gb_id");
            entity.Property(e => e.Gender)
                .HasMaxLength(25)
                .HasColumnName("gender");
            entity.Property(e => e.Generate)
                .ValueGeneratedOnAdd()
                .HasColumnName("_generate");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsVip)
                .HasDefaultValue(0)
                .HasColumnName("is_vip");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Nik)
                .HasMaxLength(50)
                .HasColumnName("nik");
            entity.Property(e => e.NikDisplay)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("nik_display");
            entity.Property(e => e.NoExt)
                .HasMaxLength(20)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("no_ext");
            entity.Property(e => e.NoPhone)
                .HasMaxLength(20)
                .HasDefaultValue(" ")
                .HasColumnName("no_phone");
            entity.Property(e => e.PasswordMobile)
                .HasMaxLength(200)
                .HasDefaultValue("")
                .HasColumnName("password_mobile");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
            entity.Property(e => e.VipApproveBypass)
                .HasDefaultValue(0)
                .HasColumnName("vip_approve_bypass");
            entity.Property(e => e.VipLimitCapBypass)
                .HasDefaultValue(0)
                .HasColumnName("vip_limit_cap_bypass");
            entity.Property(e => e.VipLockRoom)
                .HasDefaultValue(0)
                .HasColumnName("vip_lock_room");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_facility_id");

            entity.ToTable("facility", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.GoogleIcon).HasColumnName("google_icon");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<HelpdeskMonitor>(entity =>
        {
            entity.HasKey(e => e.Generate).HasName("PK_helpdesk_monitor__generate");

            entity.ToTable("helpdesk_monitor", "smart_meeting_room");

            entity.Property(e => e.Generate).HasColumnName("_generate");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Datetime)
                .HasPrecision(0)
                .HasColumnName("datetime");
            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ReasonResponse).HasColumnName("reason_response");
            entity.Property(e => e.Response).HasColumnName("response");
            entity.Property(e => e.RoomId)
                .HasMaxLength(100)
                .HasColumnName("room_id");
        });

        modelBuilder.Entity<Integration365>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_integration_365_id");

            entity.ToTable("integration_365", "smart_meeting_room");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccessToken).HasColumnName("access_token");
            entity.Property(e => e.AccountId)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("account_id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("display_name");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("email");
            entity.Property(e => e.RefreshAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("refresh_at");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserPrincipalName)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("userPrincipalName");
        });

        modelBuilder.Entity<KioskDisplay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_kiosk_display_id");

            entity.ToTable("kiosk_display", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Background)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("background");
            entity.Property(e => e.DisplayHwSerial)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("display_hw_serial");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("display_name");
            entity.Property(e => e.DisplaySerial)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("display_serial");
            entity.Property(e => e.DisplayType)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("display_type");
            entity.Property(e => e.DisplayUuid)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("display_uuid");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsLogged)
                .HasDefaultValue((short)0)
                .HasColumnName("is_logged");
            entity.Property(e => e.Koordinate)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("koordinate");
            entity.Property(e => e.LastLogged)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("last_logged");
            entity.Property(e => e.RunningText)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("running_text");
            entity.Property(e => e.TitleKiosk)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("title_kiosk");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_level_id");

            entity.ToTable("level", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.DefaultMenu).HasColumnName("default_menu");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.SortLevel)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("sort_level");
        });

        modelBuilder.Entity<LevelDescriptiion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_level_descriptiion_id");

            entity.ToTable("level_descriptiion", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.LevelId).HasColumnName("level_id");
        });

        modelBuilder.Entity<LevelDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_level_detail_id");

            entity.ToTable("level_detail", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Coment)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("coment");
            entity.Property(e => e.LevelId).HasColumnName("level_id");
            entity.Property(e => e.MenuId).HasColumnName("menu_id");
        });

        modelBuilder.Entity<LevelHeaderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_level_header_detail_id");

            entity.ToTable("level_header_detail", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Coment)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("coment");
            entity.Property(e => e.LevelId).HasColumnName("level_id");
            entity.Property(e => e.MenuId)
                .HasMaxLength(25)
                .HasColumnName("menu_id");
        });

        modelBuilder.Entity<LicenseList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_license_list_id");

            entity.ToTable("license_list", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpiredAt)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("expired_at");
            entity.Property(e => e.IsLifetime)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_lifetime");
            entity.Property(e => e.Module)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("module");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name");
            entity.Property(e => e.PlatformSerial)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("platform_serial");
            entity.Property(e => e.Qty)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("qty");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("type");
            entity.Property(e => e.IsDeleted)
               .HasDefaultValue(0)
               .HasColumnName("is_deleted");
        });

        modelBuilder.Entity<LicenseSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_license_setting_id");

            entity.ToTable("license_setting", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CheckedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("checked_at");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("customer_id");
            entity.Property(e => e.DeviceId)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("device_id");
            entity.Property(e => e.DistributorId)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("distributor_id");
            entity.Property(e => e.Ext)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("ext");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(0)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LicenseType)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("license_type");
            entity.Property(e => e.Pathdownload)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("pathdownload");
            entity.Property(e => e.Platform)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("platform");
            entity.Property(e => e.Serial)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("serial");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
            entity.Property(e => e.Webhost)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("webhost");
        });

        modelBuilder.Entity<Locker>(entity =>
        {
            entity.HasKey(e => e.Generate).HasName("PK_locker__generate");

            entity.ToTable("locker", "smart_meeting_room");

            entity.Property(e => e.Generate).HasColumnName("_generate");
            entity.Property(e => e.AutoReserve)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("auto_reserve");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("id");
            entity.Property(e => e.IpLocker)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("ip_locker");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_log_id");

            entity.ToTable("log", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activity)
                .HasMaxLength(255)
                .HasColumnName("activity");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Datetime)
                .HasPrecision(0)
                .HasColumnName("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<LogAccessRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_log_access_room_id");

            entity.ToTable("log_access_room", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("booking_id");
            entity.Property(e => e.Datetime)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("datetime");
            entity.Property(e => e.IsDefault)
                .HasDefaultValue(0)
                .HasColumnName("is_default");
            entity.Property(e => e.Msg).HasColumnName("msg");
            entity.Property(e => e.Nik)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("nik");
            entity.Property(e => e.Pin)
                .HasMaxLength(11)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("pin");
            entity.Property(e => e.RoomId)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("room_id");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("status");
        });

        modelBuilder.Entity<LogActivity>(entity =>
        {
            entity.HasKey(e => e.Generate).HasName("PK_log_activity__generate");

            entity.ToTable("log_activity", "smart_meeting_room");

            entity.Property(e => e.Generate).HasColumnName("_generate");
            entity.Property(e => e.AccessAction)
                .HasMaxLength(20)
                .HasColumnName("access_action");
            entity.Property(e => e.AccessDescription).HasColumnName("access_description");
            entity.Property(e => e.AccessIp)
                .HasMaxLength(100)
                .HasColumnName("access_ip");
            entity.Property(e => e.AccessQuery).HasColumnName("access_query");
            entity.Property(e => e.AccessTime)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("access_time");
            entity.Property(e => e.AccessUrl).HasColumnName("access_url");
            entity.Property(e => e.Nik)
                .HasMaxLength(50)
                .HasColumnName("nik");
        });

        modelBuilder.Entity<LogServicesAccessDoor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_log_services_access_door_id");

            entity.ToTable("log_services_access_door", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("booking_id");
            entity.Property(e => e.Card)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("card");
            entity.Property(e => e.Datetime)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("datetime");
            entity.Property(e => e.Msg).HasColumnName("msg");
            entity.Property(e => e.Nik)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("nik");
            entity.Property(e => e.Pin)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("pin");
            entity.Property(e => e.RoomId)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("room_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(0)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_menu_id");

            entity.ToTable("menu", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Icon)
                .HasMaxLength(255)
                .HasColumnName("icon");
            entity.Property(e => e.IsChild).HasColumnName("is_child");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.MenuGroupId).HasColumnName("menu_group_id");
            entity.Property(e => e.ModuleText)
                .HasMaxLength(100)
                .HasColumnName("module_text");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Sort).HasColumnName("sort");
            entity.Property(e => e.TypeIcon)
                .HasMaxLength(255)
                .HasColumnName("type_icon");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");
        });

        modelBuilder.Entity<MenuApp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_menu_apps_id");

            entity.ToTable("menu_apps", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Icon)
                .HasMaxLength(255)
                .HasColumnName("icon");
            entity.Property(e => e.IsChild).HasColumnName("is_child");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.MenuGroupId).HasColumnName("menu_group_id");
            entity.Property(e => e.ModuleText)
                .HasMaxLength(100)
                .HasColumnName("module_text");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Sort).HasColumnName("sort");
            entity.Property(e => e.TypeIcon)
                .HasMaxLength(255)
                .HasColumnName("type_icon");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");
        });

        modelBuilder.Entity<MenuGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_menu_group_id");

            entity.ToTable("menu_group", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .HasColumnName("icon");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<MenuHeader>(entity =>
        {
            entity.HasKey(e => e.Generate).HasName("PK_menu_headers__generate");

            entity.ToTable("menu_headers", "smart_meeting_room");

            entity.Property(e => e.Generate).HasColumnName("_generate");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Icon)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("icon");
            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModuleText)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("module_text");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name");
            entity.Property(e => e.Sort)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("sort");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("url");
        });

        modelBuilder.Entity<ModuleBackend>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("PK_module_backend_module_id");

            entity.ToTable("module_backend", "smart_meeting_room");

            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.IsEnabled).HasColumnName("is_enabled");
            entity.Property(e => e.ModuleSerial)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("module_serial");
            entity.Property(e => e.ModuleText)
                .HasMaxLength(200)
                .HasColumnName("module_text");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<NotifBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_notif_booking_id");

            entity.ToTable("notif_booking", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .HasColumnName("booking_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.IsInvited).HasColumnName("is_invited");
            entity.Property(e => e.IsNotifSend).HasColumnName("is_notifSend");
            entity.Property(e => e.IsNotifhandler).HasColumnName("is_notifhandler");
            entity.Property(e => e.IsReschedule).HasColumnName("is_reschedule");
            entity.Property(e => e.NotifId)
                .HasMaxLength(100)
                .HasColumnName("notif_id");
        });

        modelBuilder.Entity<NotificationAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_notification_admin_id");

            entity.ToTable("notification_admin", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.Datetime)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("datetime");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(0)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(0)
                .HasColumnName("is_read");
            entity.Property(e => e.IsSending)
                .HasDefaultValue(0)
                .HasColumnName("is_sending");
            entity.Property(e => e.Nik)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("nik");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<NotificationConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_notification_config_id");

            entity.ToTable("notification_config", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Authorization).HasColumnName("authorization");
            entity.Property(e => e.Topics)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("topics");
            entity.Property(e => e.Url).HasColumnName("url");
        });

        modelBuilder.Entity<NotificationData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_notification_data_id");

            entity.ToTable("notification_data", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.Datetime)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("datetime");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(0)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsSending)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_sending");
            entity.Property(e => e.Nik)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("nik");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("value");
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_notification_type_id");

            entity.ToTable("notification_type", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cololr)
                .HasMaxLength(10)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("cololr");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name");
            entity.Property(e => e.Route)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("route");
            entity.Property(e => e.Table)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("table");
            entity.Property(e => e.Topics)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("topics");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("type");
            entity.Property(e => e.Where)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("where");
        });

        modelBuilder.Entity<NotificationTypeAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_notification_type_admin_id");

            entity.ToTable("notification_type_admin", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Element)
                .HasMaxLength(255)
                .HasColumnName("element");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name");
            entity.Property(e => e.Route)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("route");
        });

        modelBuilder.Entity<Pantry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_pantry_id");

            entity.ToTable("pantry", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BuildingId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("building_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Days).HasColumnName("days");
            entity.Property(e => e.Detail).HasColumnName("detail");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(100)
                .HasColumnName("employee_id");
            entity.Property(e => e.IsApproval)
                .HasDefaultValue(0)
                .HasColumnName("is_approval");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsInternal)
                .HasDefaultValue(1)
                .HasColumnName("is_internal");
            entity.Property(e => e.IsShowPrice)
                .HasDefaultValue(0)
                .HasColumnName("is_show_price");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OpeningHoursEnd).HasColumnName("opening_hours_end");
            entity.Property(e => e.OpeningHoursStart).HasColumnName("opening_hours_start");
            entity.Property(e => e.OwnerPantry)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("owner_pantry");
            entity.Property(e => e.Pic).HasColumnName("pic");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<PantryDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_pantry_detail_id");

            entity.ToTable("pantry_detail", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.PantryId).HasColumnName("pantry_id");
            entity.Property(e => e.Pic).HasColumnName("pic");
            entity.Property(e => e.PrefixId).HasColumnName("prefix_id");
            entity.Property(e => e.Price)
                .HasDefaultValue(0)
                .HasColumnName("price");
            entity.Property(e => e.Rasio).HasColumnName("rasio");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<PantryDetailMenuVariant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_pantry_detail_menu_variant_id");

            entity.ToTable("pantry_detail_menu_variant", "smart_meeting_room");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Max).HasColumnName("max");
            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.Min).HasColumnName("min");
            entity.Property(e => e.Multiple).HasColumnName("multiple");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PantryDetailMenuVariantDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_pantry_detail_menu_variant_detail_id");

            entity.ToTable("pantry_detail_menu_variant_detail", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasDefaultValue(0)
                .HasColumnName("price");
            entity.Property(e => e.VariantId)
                .HasMaxLength(33)
                .HasColumnName("variant_id");
        });

        modelBuilder.Entity<PantryDisplay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_pantry_display_id");

            entity.ToTable("pantry_display", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Background)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("background");
            entity.Property(e => e.BackgroundUpdate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("background_update");
            entity.Property(e => e.ColorAvailable)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("color_available");
            entity.Property(e => e.ColorOccupied)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("color_occupied");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("created_by");
            entity.Property(e => e.DisableMsg)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("disable_msg");
            entity.Property(e => e.DisplaySerial)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("display_serial");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(1)
                .HasColumnName("enabled");
            entity.Property(e => e.HardwareInfo)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("hardware_info");
            entity.Property(e => e.HardwareLastsync)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("hardware_lastsync");
            entity.Property(e => e.HardwareUuid)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("hardware_uuid");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(0)
                .HasColumnName("is_deleted");
            entity.Property(e => e.RoomSelect).HasColumnName("room_select");
            entity.Property(e => e.StatusSync)
                .HasDefaultValue(0)
                .HasColumnName("status_sync");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasDefaultValue("general")
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("updated_by");
        });



        modelBuilder.Entity<PantryNotif>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_pantry_notif_id");

            entity.ToTable("pantry_notif", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .HasColumnName("booking_id");
            entity.Property(e => e.IsNotifhandler).HasColumnName("is_notifhandler");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.NotifId)
                .HasMaxLength(100)
                .HasColumnName("notif_id");
            entity.Property(e => e.PantryTrsId).HasColumnName("pantry_trs_id");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<PantrySatuan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_pantry_satuan_id");

            entity.ToTable("pantry_satuan", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PantryTransaksiD>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_pantry_transaksi_d_id");

            entity.ToTable("pantry_transaksi_d", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Detailorder).HasColumnName("detailorder");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsRejected).HasColumnName("is_rejected");
            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.NoteOrder).HasColumnName("note_order");
            entity.Property(e => e.NoteReject).HasColumnName("note_reject");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.RejectedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("rejected_at");
            entity.Property(e => e.RejectedBy)
                .HasMaxLength(100)
                .HasColumnName("rejected_by");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TransaksiId)
                .HasMaxLength(50)
                .HasColumnName("transaksi_id");
        });

        modelBuilder.Entity<PantryTransaksiStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_pantry_transaksi_status_id");

            entity.ToTable("pantry_transaksi_status", "smart_meeting_room");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_room_id");

            entity.ToTable("room", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AutomationId).HasColumnName("automation_id");
            entity.Property(e => e.BuildingId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("building_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.ConfigAdvanceBooking)
                .HasDefaultValue(7)
                .HasColumnName("config_advance_booking");
            entity.Property(e => e.ConfigAdvanceCheckin)
                .HasDefaultValue(5)
                .HasColumnName("config_advance_checkin");
            entity.Property(e => e.ConfigApprovalUser).HasColumnName("config_approval_user");
            entity.Property(e => e.ConfigGoogle)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("config_google");
            entity.Property(e => e.ConfigMaxDuration)
                .HasDefaultValue(240)
                .HasColumnName("config_max_duration");
            entity.Property(e => e.ConfigMicrosoft)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("config_microsoft");
            entity.Property(e => e.ConfigMinDuration)
                .HasDefaultValue(0)
                .HasColumnName("config_min_duration");
            entity.Property(e => e.ConfigParticipantCheckinCount)
                .HasDefaultValue(1)
                .HasColumnName("config_participant_checkin_count");
            entity.Property(e => e.ConfigPermissionCheckin)
                .HasMaxLength(255)
                .HasDefaultValue("pic")
                .HasColumnName("config_permission_checkin");
            entity.Property(e => e.ConfigPermissionEnd)
                .HasMaxLength(20)
                .HasDefaultValue("pic")
                .HasColumnName("config_permission_end");
            entity.Property(e => e.ConfigPermissionUser).HasColumnName("config_permission_user");
            entity.Property(e => e.ConfigReleaseRoomCheckinTimeout)
                .HasDefaultValue(10)
                .HasColumnName("config_release_room_checkin_timeout");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FacilityRoom).HasColumnName("facility_room");
            entity.Property(e => e.FloorId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("floor_id");
            entity.Property(e => e.GoogleMap).HasColumnName("google_map");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasDefaultValue("default.jpeg")
                .HasColumnName("image");
            entity.Property(e => e.Image2).HasColumnName("image2");
            entity.Property(e => e.IsAutomation).HasColumnName("is_automation");
            /* entity.Property(e => e.IsBeacon)
                .HasDefaultValue(0)
                .HasColumnName("is_beacon"); */
            entity.Property(e => e.IsBeacon)
                .HasColumnName("is_beacon")
                .HasColumnType("SMALLINT")
                .HasDefaultValue((short)0);
            entity.Property(e => e.IsConfigSettingEnable)
                .HasDefaultValue(0)
                .HasColumnName("is_config_setting_enable");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsDisabled)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_disabled");
            entity.Property(e => e.IsEnableApproval)
                .HasDefaultValue(0)
                .HasColumnName("is_enable_approval");
            entity.Property(e => e.IsEnableCheckin)
                .HasDefaultValue(0)
                .HasColumnName("is_enable_checkin");
            entity.Property(e => e.IsEnableCheckinCount)
                .HasDefaultValue(0)
                .HasColumnName("is_enable_checkin_count");
            entity.Property(e => e.IsEnablePermission)
                .HasDefaultValue(0)
                .HasColumnName("is_enable_permission");
            entity.Property(e => e.IsEnableRecurring)
                .HasDefaultValue(0)
                .HasColumnName("is_enable_recurring");
            entity.Property(e => e.IsRealeaseCheckinTimeout)
                .HasDefaultValue(0)
                .HasColumnName("is_realease_checkin_timeout");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.MultipleImage).HasColumnName("multiple_image");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Radid)
                .HasMaxLength(100)
                .HasColumnName("radid");
            entity.Property(e => e.TypeRoom)
                .HasMaxLength(30)
                .HasDefaultValue("single")
                .HasColumnName("type_room");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.WorkDay)
                .HasMaxLength(255)
                .HasColumnName("work_day");
            entity.Property(e => e.WorkEnd).HasColumnName("work_end");
            entity.Property(e => e.WorkStart).HasColumnName("work_start");
            entity.Property(e => e.WorkTime)
                .HasMaxLength(30)
                .HasColumnName("work_time");
            entity.Property(e => e.ConfigRoomForUsage)
                .HasMaxLength(30)
                .HasColumnName("config_room_for_usage");
            entity.Property(e => e.KindRoom)
                .HasMaxLength(20)
                .HasDefaultValue("room")
                .HasColumnName("kind_room");


        });

        modelBuilder.Entity<Room365>(entity =>
        {
            entity.HasKey(e => e.Generate).HasName("PK_room_365__generate");

            entity.ToTable("room_365", "smart_meeting_room");

            entity.Property(e => e.Generate).HasColumnName("_generate");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("address");
            entity.Property(e => e.AudioDeviceName)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("audioDeviceName");
            entity.Property(e => e.BookingType)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("bookingType");
            entity.Property(e => e.Building)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("building");
            entity.Property(e => e.Capacity)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("capacity");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.DisplayDeviceName)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("displayDeviceName");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("displayName");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("emailAddress");
            entity.Property(e => e.FloorLabel)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("floorLabel");
            entity.Property(e => e.FloorNumber)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("floorNumber");
            entity.Property(e => e.GeoCoordinates)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("geoCoordinates");
            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("id");
            entity.Property(e => e.Initial)
                .HasDefaultValue(0)
                .HasColumnName("initial");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(0)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsWheelChairAccessible)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("isWheelChairAccessible");
            entity.Property(e => e.Label)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("label");
            entity.Property(e => e.Nickname)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("nickname");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("phone");
            entity.Property(e => e.Tags)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("tags");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.VideoDeviceName)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("videoDeviceName");
        });

        modelBuilder.Entity<RoomAutomation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_room_automation_id");

            entity.ToTable("room_automation", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.Devices).HasColumnName("devices");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(100)
                .HasColumnName("ip_address");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Room).HasColumnName("room");
            entity.Property(e => e.Serial)
                .HasMaxLength(100)
                .HasColumnName("serial");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<RoomDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_room_detail_id");

            entity.ToTable("room_detail", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datetime)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("datetime");
            entity.Property(e => e.FacilityId)
                .HasMaxLength(255)
                .HasColumnName("facility_id");
            entity.Property(e => e.RoomId)
                .HasMaxLength(255)
                .HasColumnName("room_id");
        });

        modelBuilder.Entity<RoomDisplay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_room_display_id");

            entity.ToTable("room_display", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Background)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("background");
            entity.Property(e => e.BackgroundUpdate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("background_update");
            entity.Property(e => e.ColorAvailable)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("color_available");
            entity.Property(e => e.ColorOccupied)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("color_occupied");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("created_by");
            entity.Property(e => e.DisableMsg)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("disable_msg");
            entity.Property(e => e.DisplaySerial)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("display_serial");
            entity.Property(e => e.EnableSignage)
                .HasDefaultValue(0)
                .HasColumnName("enable_signage");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(1)
                .HasColumnName("enabled");
            entity.Property(e => e.HardwareInfo)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("hardware_info");
            entity.Property(e => e.HardwareLastsync)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("hardware_lastsync");
            entity.Property(e => e.HardwareUuid)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("hardware_uuid");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(0)
                .HasColumnName("is_deleted");
            entity.Property(e => e.RoomId)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("room_id");
            entity.Property(e => e.RoomSelect).HasColumnName("room_select");
            entity.Property(e => e.SignageMedia)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("signage_media");
            entity.Property(e => e.SignageType)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("signage_type");
            entity.Property(e => e.SignageUpdate)
                .HasDefaultValue(0)
                .HasColumnName("signage_update");
            entity.Property(e => e.StatusSync)
                .HasDefaultValue(0)
                .HasColumnName("status_sync");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasDefaultValue("general")
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("updated_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Description)
                .HasColumnName("description");
        });

        modelBuilder.Entity<RoomForUsage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_room_for_usage_id");

            entity.ToTable("room_for_usage", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("name");
        });

        modelBuilder.Entity<RoomForUsageDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("room_for_usage_detail", "smart_meeting_room");

            entity.Property(e => e.External)
                .HasDefaultValue(0)
                .HasColumnName("external");
            entity.Property(e => e.Internal)
                .HasDefaultValue(0)
                .HasColumnName("internal");
            entity.Property(e => e.MinCap)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("min_cap");
            entity.Property(e => e.RoomId)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("room_id");
            entity.Property(e => e.RoomUsageId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("room_usage_id");
        });

        modelBuilder.Entity<RoomGoogle>(entity =>
        {
            entity.HasKey(e => e.Generate).HasName("PK_room_google__generate");

            entity.ToTable("room_google", "smart_meeting_room");

            entity.Property(e => e.Generate).HasColumnName("_generate");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("address");
            entity.Property(e => e.AudioDeviceName)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("audioDeviceName");
            entity.Property(e => e.BookingType)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("bookingType");
            entity.Property(e => e.Building)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("building");
            entity.Property(e => e.Capacity)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("capacity");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.DisplayDeviceName)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("displayDeviceName");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("displayName");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("emailAddress");
            entity.Property(e => e.FloorLabel)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("floorLabel");
            entity.Property(e => e.FloorNumber)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("floorNumber");
            entity.Property(e => e.GeoCoordinates)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("geoCoordinates");
            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("id");
            entity.Property(e => e.Initial)
                .HasDefaultValue(0)
                .HasColumnName("initial");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(0)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsWheelChairAccessible)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("isWheelChairAccessible");
            entity.Property(e => e.Label)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("label");
            entity.Property(e => e.Nickname)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("nickname");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("phone");
            entity.Property(e => e.Tags)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("tags");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.VideoDeviceName)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("videoDeviceName");
        });

        modelBuilder.Entity<RoomMergeDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_room_merge_detail_id");

            entity.ToTable("room_merge_detail", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MergeRoomId)
                .HasMaxLength(255)
                .HasColumnName("merge_room_id");
            entity.Property(e => e.RoomId)
                .HasMaxLength(255)
                .HasColumnName("room_id");
        });

        modelBuilder.Entity<RoomUserCheckin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_room_user_checkin_id");

            entity.ToTable("room_user_checkin", "smart_meeting_room");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(0)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Key)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("key");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("name");
        });

        modelBuilder.Entity<SendingEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_sending_email_id");

            entity.ToTable("sending_email", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Batch).HasColumnName("batch");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .HasColumnName("booking_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.ErrorSending).HasColumnName("error_sending");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsStatus)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_status");
            entity.Property(e => e.Pending).HasColumnName("pending");
            entity.Property(e => e.Success).HasColumnName("success");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<SendingNotif>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_sending_notif_id");

            entity.ToTable("sending_notif", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Batch).HasColumnName("batch");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .HasColumnName("booking_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.ErrorSending).HasColumnName("error_sending");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsStatus)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_status");
            entity.Property(e => e.Pending).HasColumnName("pending");
            entity.Property(e => e.Success).HasColumnName("success");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<SendingTextStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_sending_text_status_id");

            entity.ToTable("sending_text_status", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Text)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("text");
        });

        modelBuilder.Entity<SettingEmailTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_setting_email_template_id");

            entity.ToTable("setting_email_template", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttendanceNoText)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("attendance_no_text");
            entity.Property(e => e.AttendanceText)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("attendance_text");
            entity.Property(e => e.CloseText)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("close_text");
            entity.Property(e => e.ContentText).HasColumnName("content_text");
            entity.Property(e => e.DateText)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("date_text");
            entity.Property(e => e.DetailLocation)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("detail_location");
            entity.Property(e => e.FootText).HasColumnName("foot_text");
            entity.Property(e => e.GreetingText)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("greeting_text");
            entity.Property(e => e.IsEnabled)
                .HasDefaultValue(0)
                .HasColumnName("is_enabled");
            entity.Property(e => e.Link).HasColumnName("link");
            entity.Property(e => e.MapLinkText)
                .HasMaxLength(255)
                .HasDefaultValue("Direction map")
                .HasColumnName("map_link_text");
            entity.Property(e => e.Room)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("room");
            entity.Property(e => e.SupportText)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("support_text");
            entity.Property(e => e.TitleAgendaText)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("title_agenda_text");
            entity.Property(e => e.TitleOfText)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("title_of_text");
            entity.Property(e => e.ToText)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("to_text");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("type");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
        });

        modelBuilder.Entity<SettingInvoiceConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_setting_invoice_config_id");

            entity.ToTable("setting_invoice_config", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AmountBillText)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("amount_bill_text");
            entity.Property(e => e.AmountText)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("amount_text");
            entity.Property(e => e.ContentText).HasColumnName("content_text");
            entity.Property(e => e.DateFormat)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("date_format");
            entity.Property(e => e.DateText)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("date_text");
            entity.Property(e => e.DescriptionText)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("description_text");
            entity.Property(e => e.Footer2Text)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("footer2_text");
            entity.Property(e => e.Footer3Text)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("footer3_text");
            entity.Property(e => e.FooterText).HasColumnName("footer_text");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.NoInvText)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("no_inv_text");
            entity.Property(e => e.NoProfitText)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("no_profit_text");
            entity.Property(e => e.TaxAmount)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("tax_amount");
            entity.Property(e => e.TaxText)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("tax_text");
            entity.Property(e => e.ToText)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("to_text");
            entity.Property(e => e.TotalText)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("total_text");
            entity.Property(e => e.UpText)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("up_text");
        });

        modelBuilder.Entity<SettingInvoiceText>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_setting_invoice_text_id");

            entity.ToTable("setting_invoice_text", "smart_meeting_room");

            entity.Property(e => e.Id)
                .HasMaxLength(11)
                .HasDefaultValue("")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValue("s")
                .HasColumnName("created_by");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasDefaultValue(" s")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<SettingLogConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_setting_log_config_id");

            entity.ToTable("setting_log_config", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Text)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("text");
        });

        modelBuilder.Entity<SettingPantryConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_setting_pantry_config_id");

            entity.ToTable("setting_pantry_config", "smart_meeting_room");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BeforeOrderMeeting).HasColumnName("before_order_meeting");
            entity.Property(e => e.MaxOrderQty).HasColumnName("max_order_qty");
            entity.Property(e => e.PantryExpired).HasColumnName("pantry_expired");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<SettingRuleBooking>(entity =>
        {
            //entity.HasKey(e => e.Id).HasName("PK_setting_rule_booking_id");

            entity.ToTable("setting_rule_booking", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConfigAdvanceBooking)
                .HasDefaultValue(7)
                .HasColumnName("config_advance_booking");
            entity.Property(e => e.ConfigAdvanceCheckin)
                .HasDefaultValue(5)
                .HasColumnName("config_advance_checkin");
            entity.Property(e => e.ConfigApprovalUser).HasColumnName("config_approval_user");
            entity.Property(e => e.ConfigMaxDuration)
                .HasDefaultValue(240)
                .HasColumnName("config_max_duration");
            entity.Property(e => e.ConfigMinDuration)
                .HasDefaultValue(15)
                .HasColumnName("config_min_duration");
            entity.Property(e => e.ConfigParticipantCheckinCount)
                .HasDefaultValue(0)
                .HasColumnName("config_participant_checkin_count");
            entity.Property(e => e.ConfigPermissionCheckin)
                .HasMaxLength(255)
                .HasDefaultValue("pic")
                .HasColumnName("config_permission_checkin");
            entity.Property(e => e.ConfigPermissionEnd)
                .HasMaxLength(255)
                .HasDefaultValue("pic")
                .HasColumnName("config_permission_end");
            entity.Property(e => e.ConfigPermissionUser).HasColumnName("config_permission_user");
            entity.Property(e => e.ConfigReleaseRoomCheckinTimeout)
                .HasDefaultValue(10)
                .HasColumnName("config_release_room_checkin_timeout");
            entity.Property(e => e.ConfigRoomForUsage)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("config_room_for_usage");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Duration)
                .HasDefaultValue(0)
                .HasColumnName("duration");
            entity.Property(e => e.EndEarlyMeeting)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("end_early_meeting");
            entity.Property(e => e.ExtendCountTime)
                .HasDefaultValue(0)
                .HasColumnName("extend_count_time");
            entity.Property(e => e.ExtendMeeting)
                .HasDefaultValue(30)
                .HasColumnName("extend_meeting");
            entity.Property(e => e.ExtendMeetingMax)
                .HasDefaultValue(60)
                .HasColumnName("extend_meeting_max");
            entity.Property(e => e.ExtendMeetingNotification)
                .HasDefaultValue(1)
                .HasColumnName("extend_meeting_notification");
            entity.Property(e => e.IfUnusedRoom).HasColumnName("if_unused_room");
            entity.Property(e => e.IsConfigSettingEnable)
                .HasDefaultValue(0)
                .HasColumnName("is_config_setting_enable");
            entity.Property(e => e.IsEnableApproval)
                .HasDefaultValue(0)
                .HasColumnName("is_enable_approval");
            entity.Property(e => e.IsEnableCheckin)
                .HasDefaultValue(0)
                .HasColumnName("is_enable_checkin");
            entity.Property(e => e.IsEnableCheckinCount)
                .HasDefaultValue(0)
                .HasColumnName("is_enable_checkin_count");
            entity.Property(e => e.IsEnablePermission)
                .HasDefaultValue(0)
                .HasColumnName("is_enable_permission");
            entity.Property(e => e.IsEnableRecurring)
                .HasDefaultValue(0)
                .HasColumnName("is_enable_recurring");
            entity.Property(e => e.IsRealeaseCheckinTimeout)
                .HasDefaultValue(0)
                .HasColumnName("is_realease_checkin_timeout");
            entity.Property(e => e.LimitTimeBooking)
                .HasDefaultValue(0)
                .HasColumnName("limit_time_booking");
            entity.Property(e => e.MaxDisplayDuration)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("max_display_duration");
            entity.Property(e => e.MaxEndMeeting)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("max_end_meeting");
            entity.Property(e => e.NotifUnuseBeforeMeeting).HasColumnName("notif_unuse_before_meeting");
            entity.Property(e => e.NotifUnusedMeeting)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("notif_unused_meeting");
            entity.Property(e => e.RoomPin)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("room_pin");
            entity.Property(e => e.RoomPinNumber)
                .HasMaxLength(11)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("room_pin_number");
            entity.Property(e => e.RoomPinRefresh)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("room_pin_refresh");
            entity.Property(e => e.UnuseCancelFee).HasColumnName("unuse_cancel_fee");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<SettingRuleDeskbooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_setting_rule_deskbooking_id");

            entity.ToTable("setting_rule_deskbooking", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConfigAdvanceBooking)
                .HasDefaultValue(7)
                .HasColumnName("config_advance_booking");
            entity.Property(e => e.ConfigAdvanceCheckin)
                .HasDefaultValue(7)
                .HasColumnName("config_advance_checkin");
            entity.Property(e => e.ConfigEnableCheckin)
                .HasDefaultValue(1)
                .HasColumnName("config_enable_checkin");
            entity.Property(e => e.ConfigMaxDuration)
                .HasDefaultValue(240)
                .HasColumnName("config_max_duration");
            entity.Property(e => e.ConfigMinDuration)
                .HasDefaultValue(15)
                .HasColumnName("config_min_duration");
            entity.Property(e => e.ConfigParticipantCheckinCount)
                .HasDefaultValue(0)
                .HasColumnName("config_participant_checkin_count");
            entity.Property(e => e.ConfigPermissionCheckin)
                .HasDefaultValue(1)
                .HasColumnName("config_permission_checkin");
            entity.Property(e => e.ConfigPermissionEnd)
                .HasDefaultValue(1)
                .HasColumnName("config_permission_end");
            entity.Property(e => e.ConfigReleaseRoomCheckinEnable)
                .HasDefaultValue(1)
                .HasColumnName("config_release_room_checkin_enable");
            entity.Property(e => e.ConfigReleaseRoomCheckinTime)
                .HasDefaultValue(10)
                .HasColumnName("config_release_room_checkin_time");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.Duration)
                .HasDefaultValue(0)
                .HasColumnName("duration");
            entity.Property(e => e.EndEarlyMeeting)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("end_early_meeting");
            entity.Property(e => e.ExtendCountTime)
                .HasDefaultValue(0)
                .HasColumnName("extend_count_time");
            entity.Property(e => e.ExtendMeeting)
                .HasDefaultValue(30)
                .HasColumnName("extend_meeting");
            entity.Property(e => e.ExtendMeetingMax)
                .HasDefaultValue(60)
                .HasColumnName("extend_meeting_max");
            entity.Property(e => e.ExtendMeetingNotification)
                .HasDefaultValue(1)
                .HasColumnName("extend_meeting_notification");
            entity.Property(e => e.IfUnusedRoom)
                .HasDefaultValue(0)
                .HasColumnName("if_unused_room");
            entity.Property(e => e.IsConfigCheckinEnable)
                .HasDefaultValue(0)
                .HasColumnName("is_config_checkin_enable");
            entity.Property(e => e.IsConfigSettingEnable)
                .HasDefaultValue(0)
                .HasColumnName("is_config_setting_enable");
            entity.Property(e => e.LimitTimeBooking)
                .HasDefaultValue(0)
                .HasColumnName("limit_time_booking");
            entity.Property(e => e.MaxDisplayDuration)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("max_display_duration");
            entity.Property(e => e.MaxEndMeeting)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("max_end_meeting");
            entity.Property(e => e.NotifUnuseBeforeMeeting)
                .HasDefaultValue(0)
                .HasColumnName("notif_unuse_before_meeting");
            entity.Property(e => e.NotifUnusedMeeting)
                .HasDefaultValue(0)
                .HasColumnName("notif_unused_meeting");
            entity.Property(e => e.UnuseCancelFee)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("unuse_cancel_fee");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<SettingSmtp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_setting_smtp_id");

            entity.ToTable("setting_smtp", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.Host)
                .HasMaxLength(100)
                .HasColumnName("host");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsEnabled)
                .HasDefaultValue(0)
                .HasColumnName("is_enabled");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Port).HasColumnName("port");
            entity.Property(e => e.Secure).HasColumnName("secure");
            entity.Property(e => e.SelectedEmail)
                .HasDefaultValue(0)
                .HasColumnName("selected_email");
            entity.Property(e => e.TitleEmail)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("title_email");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
            entity.Property(e => e.User)
                .HasMaxLength(100)
                .HasColumnName("user");
        });

        modelBuilder.Entity<TimeAmMeeting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_time_am_meeting_id");

            entity.ToTable("time_am_meeting", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Day)
                .HasDefaultValue(0)
                .HasColumnName("day");
            entity.Property(e => e.Desc).HasColumnName("desc");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(0)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsDisactivated)
                .HasDefaultValue(0)
                .HasColumnName("is_disactivated");
            entity.Property(e => e.Time)
                .HasDefaultValue(0)
                .HasColumnName("time");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<TimeSchedule15>(entity =>
        {
            entity.HasKey(e => e.Timeid).HasName("PK_time_schedule_15_timeid");

            entity.ToTable("time_schedule_15", "smart_meeting_room");

            entity.Property(e => e.Timeid).HasColumnName("timeid");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Time)
                .HasMaxLength(10)
                .HasColumnName("time");
        });

        modelBuilder.Entity<TimeSchedule30>(entity =>
        {
            entity.HasKey(e => e.Timeid).HasName("PK_time_schedule_30_timeid");

            entity.ToTable("time_schedule_30", "smart_meeting_room");

            entity.Property(e => e.Timeid).HasColumnName("timeid");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Time)
                .HasMaxLength(10)
                .HasColumnName("time");
        });

        modelBuilder.Entity<TimeSchedule60>(entity =>
        {
            entity.HasKey(e => e.Timeid).HasName("PK_time_schedule_60_timeid");

            entity.ToTable("time_schedule_60", "smart_meeting_room");

            entity.Property(e => e.Timeid).HasColumnName("timeid");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Time)
                .HasMaxLength(10)
                .HasColumnName("time");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_user_id");

            entity.ToTable("user", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccessId)
                .HasMaxLength(255)
                .HasDefaultValue("1")
                .HasColumnName("access_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("created_by");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("employee_id");
            entity.Property(e => e.IsApproval)
                .HasDefaultValue(0)
                .HasColumnName("is_approval");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsDisactived).HasColumnName("is_disactived");
            entity.Property(e => e.IsVip)
                .HasDefaultValue(0)
                .HasColumnName("is_vip");
            entity.Property(e => e.LevelId).HasColumnName("level_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .HasColumnName("password");
            entity.Property(e => e.RealPassword)
                .HasMaxLength(100)
                .HasColumnName("real_password");
            entity.Property(e => e.SecureQr)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("secure_qr");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("updated_by");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
            entity.Property(e => e.VipApproveBypass)
                .HasDefaultValue(0)
                .HasColumnName("vip_approve_bypass");
            entity.Property(e => e.VipLimitCapBypass)
                .HasDefaultValue(0)
                .HasColumnName("vip_limit_cap_bypass");
            entity.Property(e => e.VipShiftedBypass)
                .HasDefaultValue(0)
                .HasColumnName("vip_shifted_bypass");
        });

        modelBuilder.Entity<UserAccess>(entity =>
        {
            entity.HasKey(e => e.AccessId).HasName("PK_user_access_access_id");

            entity.ToTable("user_access", "smart_meeting_room");

            entity.Property(e => e.AccessId)
                .ValueGeneratedNever()
                .HasColumnName("access_id");
            entity.Property(e => e.AccessName)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("access_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("is_active");
        });

        modelBuilder.Entity<UserConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_user_config_id");

            entity.ToTable("user_config", "smart_meeting_room");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DefaultPassword)
                .HasMaxLength(100)
                .HasColumnName("default_password");
            entity.Property(e => e.PasswordLength).HasColumnName("password_length");
        });

        modelBuilder.Entity<VariableTimeDuration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_variable_time_duration_id");

            entity.ToTable("variable_time_duration", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Time)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("time");
        });

        modelBuilder.Entity<VariableTimeExtend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_variable_time_extend_id");

            entity.ToTable("variable_time_extend", "smart_meeting_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Time)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("time");
        });




        //modelBuilder.Entity<RoomData>().HasNoKey();

        OnModelCreatingPartial(modelBuilder);
    }

    internal IEnumerable<ModuleBackend> Where(Func<object, bool> value)
    {
        throw new NotImplementedException();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
