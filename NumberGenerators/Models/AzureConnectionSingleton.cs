using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types

namespace NumberGenerators.Models
{
    public class AzureConnectionSingleton
    {
        private static AzureConnectionSingleton instance = null;

        private AzureConnectionSingleton()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
    CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Generators");
            table.CreateIfNotExists();
        }

        public void StoreNumbers()
        {

        }

        public static AzureConnectionSingleton GetInstance()
        {
            if (instance == null)
                instance = new AzureConnectionSingleton();
            return instance;
        }
    }
}