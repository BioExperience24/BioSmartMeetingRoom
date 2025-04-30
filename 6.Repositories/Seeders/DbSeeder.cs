using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace _6.Repositories.Seeders
{
    public class DbSeeder
    {
        public static void Seed(MyDbContext db, IConfiguration configuration)
        {
            db.Database.EnsureCreated();
            RunSqlSeedersIfTableEmpty(db, configuration);
        }

        private static void RunSqlSeedersIfTableEmpty(MyDbContext db, IConfiguration configuration)
        {
            var sqlDir = Path.Combine(AppContext.BaseDirectory, "Seeders", "sql");

            if (!Directory.Exists(sqlDir))
            {
                Console.WriteLine($"Folder not found: {sqlDir}");
                return;
            }

            var files = Directory.GetFiles(sqlDir, "*.sql");

            if (files.Length == 0)
            {
                Console.WriteLine("No SQL files to execute.");
                return;
            }

            // Get database name from connection string
            var defaultConnection = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(defaultConnection))
            {
                throw new ArgumentException("DefaultConnection is null or empty in the configuration.");
            }
            var databaseName = ExtractDatabaseName(defaultConnection);

            foreach (var file in files)
            {
                string tableName = Path.GetFileNameWithoutExtension(file).Replace("zz.", "");

                if (IsTableEmpty(db, tableName, databaseName))
                {
                    Console.WriteLine($"Seeding table [{databaseName}].[{tableName}] from file {Path.GetFileName(file)}");
                    try
                    {
                        string sql = File.ReadAllText(file);

                        // 1. Remove "USE [smart_meeting_room]" and the following "GO"
                        sql = Regex.Replace(sql, @"USE\s+\[.*?\]\s*GO", "", RegexOptions.IgnoreCase);

                        // 2. Replace prefix "[smart_meeting_room]." with the database name
                        sql = sql.Replace("[smart_meeting_room].", $"[{databaseName}].");

                        // 3. Optional: Remove standalone "GO" lines
                        sql = Regex.Replace(sql, @"^\s*GO\s*$", "", RegexOptions.Multiline);

                        // 4. Optionally: Add "INSERT INTO" if needed
                        // sql = Regex.Replace(sql, @"INSERT\s+\[", "INSERT INTO [", RegexOptions.IgnoreCase);

                        // Log SQL before execution
                        // Console.WriteLine($"SQL to be executed:\n{sql}");

                        db.Database.ExecuteSqlRaw(sql);
                        Console.WriteLine($"Done seeding [{databaseName}].[{tableName}].");
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to seed table [{databaseName}].[{tableName}]: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Skipping table [{databaseName}].[{tableName}] because it already contains data.");
                }
            }
        }

        private static string ExtractDatabaseName(string connectionString)
        {
            var match = Regex.Match(connectionString, @"Database=(?<dbName>[^;]+);", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups["dbName"].Value : throw new Exception("Database name not found in the connection string.");
        }

        private static bool IsTableEmpty(MyDbContext db, string tableName, string databaseName)
        {
            try
            {
                var conn = db.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT TOP 1 * FROM [{databaseName}].[{tableName}]";
                using var reader = cmd.ExecuteReader();
                return !reader.HasRows;
            }
            catch (DbException ex)
            {
                Console.WriteLine($"Unable to check table [{databaseName}].[{tableName}]: {ex.Message}");
                return false; // Do not proceed with seeding if the table check fails
            }
        }
    }
}