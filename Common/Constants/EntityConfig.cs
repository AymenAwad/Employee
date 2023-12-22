namespace Shared.Constants
{
    public class EntityConfig
    {
        public const string DatabaseConnectionStringName = "DefaultConnection";
        public const string LogsDatabaseConnectionStringName = "logsConnection";
        public const string SqlDecimalType = "decimal(18,2)";

        public const string SqlDateType = "datetime2";
        public const string SqlDateDefaultValue = "getDate()";
        public const string SqlRowDeletedDefaultValue = "0";
        public const string SqlGuidDefaultValue = "newsequentialid()";
    }
}
