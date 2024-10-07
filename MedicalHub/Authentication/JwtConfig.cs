using Microsoft.Extensions.Options;

namespace MedicalHub.Authentication;

public class JwtConfig : IConfigureOptions<JwtOptions>
{
    
    private readonly string SectionName = "Jwt";
    private readonly IConfiguration _configuration;

    public JwtConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}