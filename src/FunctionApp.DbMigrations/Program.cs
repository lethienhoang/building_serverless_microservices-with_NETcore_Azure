// See https://aka.ms/new-console-template for more information

using FunctionApp.DbMigrations;

Console.WriteLine("Starting scripts migration to database!");
await DbMigrationExecuting.RunAsync();
Console.WriteLine("Finished scripts migration to database!");