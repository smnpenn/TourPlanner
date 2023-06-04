namespace TourPlanner.DAL.Configuration
{
    public interface IConfigManager
    {
        public string? GetDBConfig();
        public string? GetAPIKey();

        public string? GetESUser();
        public string? GetESPassword();
        public string? GetESIndex();
        public string? GetESFingerprint();
    }
}
