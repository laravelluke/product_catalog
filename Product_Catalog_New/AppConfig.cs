namespace Product_Catalog_New
{
    public class AppConfig : IAppConfig
    {
        public readonly string _suffix = string.Empty;

        public IConfiguration Configuration { get; }
        public AppConfig(IConfiguration configuration)
        {
            Configuration = configuration;
            _suffix = Configuration["Suffix"];
        }

        public string GetSuffix()
        {
            return _suffix;
        }
    }
    public interface IAppConfig
    {
        string GetSuffix();
    }
}
