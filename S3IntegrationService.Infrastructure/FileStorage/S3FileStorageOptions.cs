using Microsoft.Extensions.Options;

namespace S3IntegrationService.Infrastructure.FileStorage
{
    public class S3FileStorageOptions
    {
        public string Address { get; set; } = null!;
        public string AccessKeyId { get; set; } = null!;
        public string AccessSecretKey { get; set; } = null!;
        public string BucketName { get; set; } = null!;
        public TimeSpan DefaultLinkExpiration { get; set; } = TimeSpan.FromMinutes(30);

        internal void Validate()
        {
            var failures = new List<string>();

            if(string.IsNullOrWhiteSpace(Address))
                failures.Add($"{nameof(Address)} must not be empty");

            if (!Uri.IsWellFormedUriString(Address, UriKind.Absolute))
                failures.Add($"{nameof(Address)} should be correct url");

            if(string.IsNullOrEmpty(AccessKeyId))
                failures.Add($"{nameof(AccessKeyId)} must not be empty");

            if(string.IsNullOrEmpty(AccessSecretKey))
                failures.Add($"{nameof(AccessSecretKey)} must not be empty");

            if(string.IsNullOrEmpty(BucketName))
                failures.Add($"{nameof(BucketName)} must not be empty");

            if (DefaultLinkExpiration.TotalSeconds < 5)
                failures.Add($"{nameof(DefaultLinkExpiration)} should not be less than 5 seconds");

            if (failures.Any())
                throw new OptionsValidationException(Options.DefaultName, typeof(S3FileStorageOptions), failures);
        }
    }
}
