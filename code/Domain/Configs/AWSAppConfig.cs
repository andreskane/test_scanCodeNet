namespace Domain.Configs
{
    public class AWSAppConfig
    {
        public string AppName { get; set; }//: "drx-cos-api-customermanager",
        public string Region { get; set; }//: "us-east-2",
        public string KubernetesEnv { get; set; }//: "dev"



        public AWSAppConfig() { }
        public AWSAppConfig(string appName, string region, string kubernetesEnv)
        {
            AppName = appName;
            Region = region;
            KubernetesEnv = kubernetesEnv;

        }
    }
}
