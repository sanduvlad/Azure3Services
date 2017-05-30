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
        CloudTableClient tableClient;
        CloudTable table;

        private AzureConnectionSingleton()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
    CloudConfigurationManager.GetSetting("StorageConnectionString"));
            tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference("Generators");
            table.CreateIfNotExists();
        }

        public void StoreNumbers(List<IntNumber> numbers)
        {
            for(int i = 0; i < numbers.Count; i ++)
            {
                TableOperation to = TableOperation.Insert(numbers[i]);
                table.Execute(to);
            }
        }

        public List<IntNumber> GetNumbers(Guid userGuid)
        {

            var query = new TableQuery<IntNumber>().Where(
TableQuery.GenerateFilterCondition(
"PartitionKey",
QueryComparisons.GreaterThanOrEqual,
userGuid.ToString()
)
);
             List<IntNumber> list = table.ExecuteQuery(query).ToList();
            return list;
        }

        public static AzureConnectionSingleton GetInstance()
        {
            if (instance == null)
                instance = new AzureConnectionSingleton();
            return instance;
        }
    }
}