using cOS.Customer.Manage.apllication.Infrastructure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cOS.Customer.Manage.apllication.Helper
{

    public class BlobStorageHelper
    {
        /// <summary>
        /// This gets all the files in a given context
        /// </summary>
        //public static IEnumerable<ICloudFileModel> GetFiles(string containerName)
        //{
        //    IEnumerable<ICloudFileModel> files = new List<ICloudFileModel>();
        //    CloudStorageAccount storageAccount = null;
        //    CloudBlobContainer cloudBlobContainer = null;

        //    string storageConnectionString = AppSettingsHelper.GetAppSettingValue<string>(AzureConstants.StorageConnectionStringName, null, true);

        //    if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
        //    {
        //        try
        //        {
        //            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        //            cloudBlobContainer = cloudBlobClient.GetContainerReference(AppSettingsHelper.GetAppSettingValue<string>(AzureConstants.ContainerBlobStorageName, null, true));
        //            IEnumerable<IListBlobItem> blobs = BlobStorageHelper.ListBlobsAsync(null, cloudBlobContainer).Result;

        //            files = blobs.OfType<CloudBlockBlob>().Select(b => new AzureFileDto()
        //            {
        //                FileName = b.Name,
        //                Uri = b.Uri + BuildSAS(b),
        //                FileType = b.Name.Split('.').ToList().Last(),
        //                CreateDate = b.Properties.Created.HasValue ? b.Properties.Created.Value.UtcDateTime : (DateTime?)null,
        //                ModificationDate = b.Properties.LastModified.HasValue ? b.Properties.LastModified.Value.UtcDateTime : (DateTime?)null,
        //            }).ToList();
        //        }
        //        catch (StorageException ex)
        //        {
        //            throw new Exception("ERROR_COMMON_01", ex);
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("ERROR_COMMON_02");
        //    }
        //    return files;
        //}

        /// <summary>
        /// This gets all the files in recursive folder
        /// </summary>
        public static IList<IListBlobItem> GetRecursiveFiles(string containerName)
        {
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;
            BlobContinuationToken blobContinuationToken = null;

            IList<IListBlobItem> blobs = new List<IListBlobItem>();
            string storageConnectionString = AppSettingsHelper.GetAppSettingValue<string>(AzureConstants.StorageConnectionStringName, null, true);

            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {

                try
                {
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                    cloudBlobContainer = cloudBlobClient.GetContainerReference(AppSettingsHelper.GetAppSettingValue<string>(AzureConstants.ContainerBlobStorageName, null, true));
                    do
                    {
                        BlobResultSegment results = cloudBlobContainer.ListBlobsSegmentedAsync(containerName, blobContinuationToken).GetAwaiter().GetResult();

                        foreach (IListBlobItem item in results.Results)
                        {
                            blobs.Add(item);
                        }
                    } while (blobContinuationToken != null);
                }
                catch (StorageException ex)
                {
                    throw new Exception("ERROR_COMMON_01", ex);
                }
            }
            else
            {
                throw new Exception("ERROR_COMMON_02");
            }
            return blobs;
        }

        /// <summary>
        /// This gets the azure file model for a blob reference
        /// </summary>
        //public static AzureFileDto GetFileModel(string absoluteUri, bool buildSAS = true)
        //{
        //    AzureFileDto file = new AzureFileDto();
        //    CloudStorageAccount storageAccount = GetAccount();

        //    try
        //    {
        //        Uri reportUriWithoutSAS = new Uri(absoluteUri);
        //        CloudBlob reportBlob = new CloudBlob(reportUriWithoutSAS, storageAccount.Credentials);
        //        String sharedAccessSignature = String.Empty;
        //        if (buildSAS)
        //            sharedAccessSignature = BuildSAS(reportBlob);

        //        file.FileName = reportBlob.Name;
        //        file.FileType = reportBlob.Name.Split('.').ToList().Last();
        //        file.Uri = $"{absoluteUri}{sharedAccessSignature}";
        //    }
        //    catch (StorageException ex)
        //    {
        //        throw new Exception("ERROR_COMMON_01", ex);
        //    }
        //    return file;
        //}

        /// <summary>
        /// This gets the raw text from a file
        /// </summary>
        public static string GetTextFile(string path)
        {
            string text = null;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    CloudBlob blob = GetBlobFile(path);
                    blob.DownloadToStreamAsync(ms).Wait();
                    text = StreamToText(ms);
                }
            }
            catch
            {
                text = null;
            }
            return text;
        }

        public static String StreamToText(Stream ms)
        {
            String text = String.Empty;

            ms.Position = 0;
            using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }

        /// <summary>
        /// Gets a blob file by it storage path
        /// </summary>
        public static CloudBlob GetBlobFile(string path)
        {
            CloudBlob blobRef = null;
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;
            string storageConnectionString = AppSettingsHelper.GetAppSettingValue<string>(AzureConstants.StorageConnectionStringName, null, true);

            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    cloudBlobContainer = cloudBlobClient.GetContainerReference(AppSettingsHelper.GetAppSettingValue<string>(AzureConstants.ContainerBlobStorageName, null, true));

                    blobRef = cloudBlobContainer.GetBlobReference(path);
                }
                catch (StorageException ex)
                {
                    throw new Exception("ERROR_COMMON_01", ex);
                }
            }
            else
            {
                throw new Exception("ERROR_COMMON_02");
            }
            return blobRef;
        }

        /// <summary>
        /// Sets a blob storage by given container a bite array, returning a string if the upload was successful
        /// </summary>
        public static String SetFile(string containerName, String fileName, byte[] file, string contentType = null)
        {
            string path = containerName + fileName;

            string storageConnectionString = AppSettingsHelper.GetAppSettingValue<String>
                (AzureConstants.StorageConnectionStringName, throwExecption: true);

            if (CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
            {
                try
                {
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(AppSettingsHelper.GetAppSettingValue<string>(AzureConstants.ContainerBlobStorageName, null, true));
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(path);

                    if (contentType != null)
                        cloudBlockBlob.Properties.ContentType = contentType;

                    cloudBlockBlob.UploadFromByteArrayAsync(file, 0, file.Length).Wait();

                    BlobContinuationToken blobContinuationToken = null;
                    do
                    {
                        var results = cloudBlobContainer.ListBlobsSegmentedAsync(containerName, blobContinuationToken).GetAwaiter().GetResult();

                        blobContinuationToken = results.ContinuationToken;
                    } while (blobContinuationToken != null);
                }
                catch (StorageException ex)
                {
                    throw new Exception("ERROR_COMMON_01", ex);
                }
            }
            else
            {
                throw new Exception("ERROR_COMMON_02");
            }
            return AzureConstants.OK_response;
        }

        /// <summary>
        /// Given a string path, it deletes a blob and returns a string telling if the operation was successful
        /// </summary>
        public static Boolean DeleteBlobFile(String path)
        {
            Boolean deleteResult = true;

            try
            {
                CloudBlob blob = GetBlobFile(path);
                deleteResult = blob.DeleteIfExistsAsync().Result;
            }
            catch (StorageException ex)
            {
                throw new Exception("ERROR_COMMON_01", ex);
            }

            return deleteResult;
        }

        /// <summary>
        /// Given a string path, it deletes a container and returns a string telling if the operation was successful
        /// </summary>
        public static void DeleteDirectoryBlobFiles(String directoryPath)
        {
            try
            {
                CloudBlobContainer cloudBlobContainer = GetContainer();
                IEnumerable<IListBlobItem> blobs = cloudBlobContainer.GetDirectoryReference(directoryPath).ListBlobsSegmentedAsync(null).GetAwaiter().GetResult() as IEnumerable<IListBlobItem>;

                foreach (IListBlobItem blob in blobs)
                {
                    if (blob.GetType() == typeof(CloudBlob) || blob.GetType().BaseType == typeof(CloudBlob))
                    {
                        ((CloudBlob)blob).DeleteIfExistsAsync();
                    }
                }
            }
            catch (StorageException ex)
            {
                throw new Exception("ERROR_COMMON_01", ex);
            }
        }

        /// <summary>
        /// Given the path of a clod blob, it returns its storage Uri
        /// </summary>
        public static string GetUrifromPath(string path)
        {
            CloudBlob blob = GetBlobFile(path);
            return blob.Uri.AbsoluteUri;
        }

        #region GeneralAzureUtilities
        /// <summary>
        /// Returns a temporarily publicly accesible URI using Shared Access Credentials (SAS).
        /// </summary>
        /// <param name="strReportUriWithoutSAS">The private report URI used to generate a public URI with SAS</param>
        public static String GetSASUriFromPrivate(String strReportUriWithoutSAS)
        {
            String responseUri = null;
            CloudStorageAccount storageAccount = GetAccount();

            try
            {
                Uri reportUriWithoutSAS = new Uri(strReportUriWithoutSAS);
                CloudBlob reportBlob = new CloudBlob(reportUriWithoutSAS, storageAccount.Credentials);

                String sharedAccessSignature = BuildSAS(reportBlob);
                responseUri = $"{strReportUriWithoutSAS}{sharedAccessSignature}";
            }
            catch (StorageException ex)
            {
                throw new Exception("ERROR_COMMON_01", ex);
            }

            return responseUri;
        }

        /// <summary>
        /// Builds the shared access signatures for the blob storage
        /// </summary>
        public static String BuildSAS(CloudBlob blob)
        {
            Int32 minutesToLive = ParametrosAppSetting.AzureMinutesToLiveSAS;
            TimeSpan timeToLive = TimeSpan.FromMinutes(minutesToLive);
            SharedAccessBlobPolicy sharedAccessBlobPolicy = new SharedAccessBlobPolicy()
            {
                SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-5),
                SharedAccessExpiryTime = DateTime.UtcNow.Add(timeToLive),
                Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.List
            };

            String sharedAccessSignature = blob.GetSharedAccessSignature(sharedAccessBlobPolicy);
            return sharedAccessSignature;
        }

        /// <summary>
        /// Returns the account root blob container object
        /// </summary>
        public static CloudBlobContainer GetContainer()
        {
            CloudStorageAccount storageAccount = GetAccount();

            String k_ContainerAccount = AppSettingsHelper.GetAppSettingValue<string>(AzureConstants.ContainerBlobStorageName, null, true);
            CloudBlobContainer cloudBlobContainer = null;

            try
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                cloudBlobContainer = cloudBlobClient.GetContainerReference(k_ContainerAccount);
                cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Off
                }).Wait();
            }
            catch (StorageException ex)
            {
                throw new Exception("ERROR_COMMON_01", ex);
            }
            return cloudBlobContainer;
        }

        /// <summary>
        /// Gets the storage account object
        /// </summary>
        public static CloudStorageAccount GetAccount()
        {
            String k_Connstring = AppSettingsHelper.GetAppSettingValue<string>(AzureConstants.StorageConnectionStringName, null, true);
            CloudStorageAccount storageAccount = null;
            String storageConnectionString = k_Connstring;

            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
                return storageAccount;
            else
            {
                throw new Exception("ERROR_COMMON_02");
            }
        }

        /// <summary>
        /// Given a container name, it returns the blobs stored within
        /// </summary>
        public static List<CloudBlob> GetBlobFiles(String containerName)
        {
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;
            List<CloudBlob> blobs = new List<CloudBlob>();
            string storageConnectionString = AppSettingsHelper.GetAppSettingValue<string>(AzureConstants.StorageConnectionStringName, null, true);

            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    cloudBlobContainer = GetContainer();
                    IEnumerable<IListBlobItem> blobsItems = cloudBlobContainer.GetDirectoryReference(containerName).ListBlobsSegmentedAsync(null).GetAwaiter().GetResult() as IEnumerable<IListBlobItem>;
                    blobs = blobsItems.Select(c => (CloudBlob)c).ToList();
                }
                catch (StorageException ex)
                {
                    throw new Exception("ERROR_COMMON_01", ex);
                }
            }
            else
            {
                throw new Exception("ERROR_COMMON_02");
            }
            return blobs;
        }

        public static byte[] TransformTextToByteArray(string toMemory)
        {
            byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                TextWriter tw = new StreamWriter(ms);
                tw.Write(toMemory);
                tw.Flush();
                ms.Position = 0;
                bytes = ms.ToArray();
            }
            return bytes;
        }
        #endregion

        //private static async Task<List<IListBlobItem>> ListBlobsAsync(BlobContinuationToken currentToken, CloudBlobContainer container)
        //{
        //    BlobContinuationToken continuationToken = null;
        //    List<IListBlobItem> results = new List<IListBlobItem>();

        //    do
        //    {
        //        var response =    container.ListBlobsSegmentedAsync(continuationToken).Result;
        //        continuationToken = response.ContinuationToken;
        //        results.AddRange(response.Results);
        //    }
        //    while (continuationToken != null);
        //    return results;
        //}
    }
}
