using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ShipmentManagement.Api.Configurations;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        var xmlFile = Path.Combine(AppContext.BaseDirectory, "ShipmentManagement.Api.xml");
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Title = "Shipment Management and Tracking API",
                Version = description.ApiVersion.ToString(),
                Description = "REST API for managing shipments and packages."
            });
        }

        options.IncludeXmlComments(xmlFile, includeControllerXmlComments: true);
    }
}