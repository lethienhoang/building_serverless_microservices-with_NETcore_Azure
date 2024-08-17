using System.Data.SqlClient;
using Dapper;

namespace FunctionApp.DbMigrations;

public static class DbMigrationExecuting
{
    public static async Task RunAsync()
    {
        Console.WriteLine("Getting files....");
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Scripts");
        var pathFiles = Directory.GetFiles(path);
        Console.WriteLine("End getting files....");

        Console.WriteLine("Getting list schema has been executing script....");
        var executedList = await GetHistoryMigrationBeExecutedAsync();
        var executedNameFileList = executedList.Select(z => z.Name).ToArray();
        var pathFilesBeFilter = pathFiles.Where(file => executedNameFileList.Contains(file)).ToArray();
        Console.WriteLine("End Getting list schema has been executing script....");

        Console.WriteLine("Starting executing script....");
        await ExecuteSqlAsync(pathFilesBeFilter);
        Console.WriteLine("End executing script....");
    }
    
    private static async Task<IEnumerable<SchemaMigrationHistoryModel>> GetHistoryMigrationBeExecutedAsync()
    {
        using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("SConnectionString")))
        {
            await connection.OpenAsync();
        
            var schemaMigrationHistoryList = await connection.QueryAsync<SchemaMigrationHistoryModel>("SELECT * FROM SCHEMA_MIGRATION_HISTORY");

            return schemaMigrationHistoryList;
        }
    }
    
    private static async Task ExecuteSqlAsync(string[] pathFiles)
    {
        using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("SConnectionString")))
        {
            await connection.OpenAsync();
        
            using (var transaction = await connection.BeginTransactionAsync())
            {
                try
                {
                    foreach (var curPathFile in pathFiles)
                    {
                        // store version to migration history
                        string version = curPathFile.Split('_').Last();
                        if (!string.IsNullOrEmpty(version))
                        {
                            await connection.ExecuteAsync(
                                "INSERT INTO SCHEMA_MIGRATION_HISTORY (Name, version, CreatedDate) VALUES(@Name, @Version, @CreatedDate)",
                                new { Name = curPathFile, Version = version, CreatedDate = DateTime.UtcNow },
                                transaction: transaction);
                        }
                        
                        string sqlRaw = File.ReadAllText(curPathFile);
                        await connection.ExecuteAsync(
                            sqlRaw,
                            transaction: transaction);
                    }
                
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }

    public record SchemaMigrationHistoryModel
    {
        public string Name { get; init; }
        public string Version { get; init; }
    }
}