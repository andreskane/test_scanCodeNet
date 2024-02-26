using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace Infrastructure.AWSSecretManager
{
    public static class GetSecretAWS
    {


        /*
 *	Use this code snippet in your app.
 *	If you need more information about configurations or implementing the sample code, visit the AWS docs:
 *	https://aws.amazon.com/developer/language/net/getting-started
 */


        public static async Task<String> GetSecret(string secretName, string region)
        {




            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT"

            };

            GetSecretValueResponse response;

            try
            {
                response = await client.GetSecretValueAsync(request);
            }
            catch (Exception e)
            {
                // For a list of the exceptions thrown, see
                // https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
                throw e;
            }
            string secret = response.SecretString;

            return secret;
        }

    }
}
