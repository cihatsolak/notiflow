namespace Puzzle.Lib.Database.Infrastructure.Extensions;

internal static class DatabaseDetectorExtensions
{
    private const string MSSQL = "Microsoft.EntityFrameworkCore.SqlServer";
    private const string PostgreSQL = "Npgsql.EntityFrameworkCore.PostgreSQL";

    internal static string Shema(this DatabaseFacade database)
    {
        return database.ProviderName switch
        {
            MSSQL => "dbo",
            PostgreSQL => "public",
            _ => throw new NotImplementedException("Unidentified database provider"),
        };
    }

    internal static string CurrentDate(this DatabaseFacade database)
    {
        return database.ProviderName switch
        {
            MSSQL => "getdate()",
            PostgreSQL => "now()",
            _ => throw new NotImplementedException("Unidentified database provider"),
        };
    }
}
