using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using S3IntegrationService.Application.FileStorage;
using S3IntegrationService.Infrastructure.FileStorage;

namespace S3IntegrationService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var s3Options = configuration.GetS3FileStorageOptions();
            s3Options.Validate();
            services.AddSingleton(s3Options);
            services.AddSingleton<IAmazonS3>(new AmazonS3Client(
                new BasicAWSCredentials(
                    s3Options.AccessKeyId,
                    s3Options.AccessSecretKey
                ),
                new AmazonS3Config
                {
                    ForcePathStyle = true,
                    ServiceURL = s3Options.Address
                }
            ));
            services.AddSingleton<IFileStorageService, S3FileStorageService>();


            return services;
        }

        private static S3FileStorageOptions GetS3FileStorageOptions(this IConfiguration configuration)
        {
            return configuration
                .GetSection("S3")
                .Get<S3FileStorageOptions>()
                ?? throw new InvalidOperationException($"Failed to bind {nameof(S3FileStorageOptions)} from configuration.");
        }
    }
}
