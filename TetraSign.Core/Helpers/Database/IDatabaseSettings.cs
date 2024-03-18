namespace TetraSign.Core.Helpers.Database;

public interface IDatabaseSettings
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
    string CollectionName { get; set; }
}