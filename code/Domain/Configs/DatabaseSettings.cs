namespace Domain.Configs
{
    public class DatabaseSettings
    {
        public const string SectionName = "DatabaseSettings";

        public string ConnectionString { get; set; } = default!;
    }
}
