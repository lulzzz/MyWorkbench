using System.Data;
using System.Data.SqlClient;

/// <summary>
/// 	Extension methods for the IDBConnection
/// </summary>
public static class ConnectionExtensions {
    public static string Database(this string connectionString) {
        IDbConnection connection = new SqlConnection(connectionString);
        return connection.Database;
    }
}
