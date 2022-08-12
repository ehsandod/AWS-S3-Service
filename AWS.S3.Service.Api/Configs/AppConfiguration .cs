namespace AWS.S3.Service.Api.Configs
{
	public class AppConfiguration : IAppConfiguration
	{
		public AppConfiguration()
		{
			BucketName = "";
			Region = "";
			AwsAccessKey = "";
			AwsSecretAccessKey = "";
			AwsSessionToken = "";
		}

		public string BucketName { get; set; }
		public string Region { get; set; }
		public string AwsAccessKey { get; set; }
		public string AwsSecretAccessKey { get; set; }
		public string AwsSessionToken { get; set; }
	}
}
