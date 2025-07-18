using Asp.Versioning.ApiExplorer;
using Common.Application.Configurations;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace DotNetTraining.Application.Settings
{
    public class ConfigureSwaggerOptions : BaseConfigureSwaggerOptions
    {
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : base(provider)
        {
        }

        protected override OpenApiInfo GetApiInfo(ApiVersionDescription description)
        {
            return new OpenApiInfo
            {
                Title = $"Your API - {description.GroupName.ToUpperInvariant()}",
                Version = description.ApiVersion.ToString()
            };
        }

        public override void Configure(SwaggerGenOptions options)
        {
            var seen = new HashSet<string>();

            foreach (var description in _provider.ApiVersionDescriptions)
            {
                if (seen.Contains(description.GroupName)) continue;

                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                seen.Add(description.GroupName);
            }
        }
    }
}
