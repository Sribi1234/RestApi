namespace RestApi.Configuration
{
    using Microsoft.Data.SqlClient;
    /// <summary>
    /// Database configuration settings.
    /// Connection String: Data Source=SRIBINKK26\SQLEXPRESS;Initial Catalog=Credit;Integrated Security=True;Trust Server Certificate=True
    /// </summary>
    public class DatabaseConfiguration
    {
        public const string SectionName = "DatabaseConfiguration";

        public string DefaultConnection { get; set; } = string.Empty;
        public int CommandTimeout { get; set; } = 30;
        public bool EnableSensitiveDataLogging { get; set; } = false;
        public bool EnableDetailedErrors { get; set; } = true;

        /// <summary>Gets the server name from connection string.</summary>
        public string ServerName
        {
            get
            {
                try
                {
                    var builder = new SqlConnectionStringBuilder(DefaultConnection);
                    return builder.DataSource ?? "Unknown";
                }
                catch
                {
                    return "Unable to parse";
                }
            }
        }

        /// <summary>Gets the database name from connection string.</summary>
        public string DatabaseName
        {
            get
            {
                try
                {
                    var builder = new SqlConnectionStringBuilder(DefaultConnection);
                    return builder.InitialCatalog ?? "Unknown";
                }
                catch
                {
                    return "Unable to parse";
                }
            }
        }

        /// <summary>Gets authentication type (Windows or SQL Server).</summary>
        public string AuthType
        {
            get
            {
                try
                {
                    var builder = new SqlConnectionStringBuilder(DefaultConnection);
                    return string.IsNullOrEmpty(builder.UserID) ? "Windows (Integrated Security)" : "SQL Server Authentication";
                }
                catch
                {
                    return "Unknown";
                }
            }
        }
    }
}
