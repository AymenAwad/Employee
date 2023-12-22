namespace Shared.Constants
{
    using Microsoft.Extensions.Configuration;

    public static class CommonsConfigurations
    {
        public static IConfiguration Configuration { get; set; }

        public static string ConnectionStringName => Configuration.GetSection("Commons").GetSection("DbConnectionStringName").Value;

        public static string ConnectionStringValue => Configuration.GetConnectionString(ConnectionStringName);

        public static string DefaultCulture { get; internal set; } = "ar-SA";

        public static string PageNumber = "pageNumber";
        public static string PageSize = "pageSize";

        public static int DefaultPageSize = 10;
        public static int DefaultPageNumber = 1;
    }
}