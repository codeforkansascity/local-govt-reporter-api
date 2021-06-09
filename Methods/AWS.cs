using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime.CredentialManagement;

namespace LocalGovtReporterAPI.Methods
{
    public class AWS
    {
        public static AmazonDynamoDBClient GetDynamoDBClient()
        {
            AmazonDynamoDBClient client = null;

            #if DEBUG
                var sharedFile = new SharedCredentialsFile();
                sharedFile.TryGetProfile("localgovt", out var profile);
                AWSCredentialsFactory.TryGetAWSCredentials(profile, sharedFile, out var credentials);
                client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast2);
            #else
                client = new AmazonDynamoDBClient(RegionEndpoint.USEast2);
            #endif

            return client;
        }
    }
}