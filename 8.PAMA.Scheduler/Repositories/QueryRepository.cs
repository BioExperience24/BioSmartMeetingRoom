using _6.Repositories.DB;
using Microsoft.EntityFrameworkCore;
using _8.PAMA.Scheduler.ViewModel;
using Microsoft.Data.SqlClient;


namespace _8.PAMA.Scheduler.Repositories;

public class QueryRepository(MyDbContext _dbContext)
{
    public async Task<List<BookingInvitationModel>> SelectBookingInvitation(string bookingId)
    {
        var result = new List<BookingInvitationModel>();

        var sql = $@"
            SELECT 
                bi.id,
                bi.booking_id,
                bi.nik,
                bi.execute_door_access,
                e.card_number,
                e.name,
                e.nik_display AS staffNo
            FROM smart_meeting_room.[booking_invitation] bi
            INNER JOIN smart_meeting_room.[employee] e ON bi.nik = e.id
            WHERE 
                bi.internal = 1 AND 
                bi.booking_id = @bookingId AND 
                e.is_deleted = 0 AND 
                bi.execute_door_access = 0
        ";

        using var connection = _dbContext.Database.GetDbConnection();
        connection.ConnectionString = _dbContext.Database.GetConnectionString();
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.Add(new SqlParameter("@bookingId", bookingId));

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var model = new BookingInvitationModel
            {
                Id = reader["id"].ToString()!,
                BookingId = reader["booking_id"].ToString()!,
                Nik = reader["nik"].ToString()!,
                CardNumber = reader["card_number"].ToString()!,
                Name = reader["name"].ToString()!,
                StaffNo = reader["staffNo"].ToString()!,
                ExecuteDoorAccess = Convert.ToBoolean(reader["execute_door_access"])
            };

            result.Add(model);
        }


        return result;
    }

    public async Task InsertLogServicesAccessDoorAsync(string bookingId, string roomId, string pinRoom, string nik, string cardNumber, string msg, string nowLog)
    {
        var insertLogSql = $@"
                            INSERT INTO smart_meeting_room.[log_services_access_door] 
                                (booking_id, room_id, pin, nik, card, msg, datetime, status)
                            VALUES 
                                ('{bookingId}', '{roomId}', '{pinRoom}', '{nik}', '{cardNumber}', '{msg}', '{nowLog}', 1)
                        ";

        await RunActionQueryAsync(insertLogSql);
    }

    public async Task UpdateExecuteDoorAccess(string id)
    {
        var idLong = long.Parse(id);

        var sql = $@"
            UPDATE smart_meeting_room.[booking_invitation] 
            SET execute_door_access = 1 
            WHERE id = '{idLong}'
        ";

        await RunActionQueryAsync(sql);
    }

    public async Task UpdateExecuteDoorAccesssByBookingDate(DateOnly date)
    {
        var sql = $@"
            UPDATE bi
            SET bi.execute_door_access = 2
            FROM smart_meeting_room.booking_invitation bi
            INNER JOIN smart_meeting_room.booking b ON bi.booking_id = b.booking_id
            WHERE 
                b.date = {date} AND 
                b.is_alive > 1 AND 
                bi.execute_door_access <> 2
        ";

        await RunActionQueryAsync(sql);
    }

    public async Task RunActionQueryAsync(string sql)
    {
        using var connection = _dbContext.Database.GetDbConnection();
        connection.ConnectionString = _dbContext.Database.GetConnectionString();
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = sql;

        await command.ExecuteNonQueryAsync();
    }
}