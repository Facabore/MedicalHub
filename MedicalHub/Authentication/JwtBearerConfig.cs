namespace MedicalHub.Authentication;

public class JwtBearerConfig
{
    private readonly string SectionName = "Jwt";
    private readonly IConfiguration _configuration;

    public JwtBearerConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}