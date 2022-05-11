using Azure.Storage.Queues;

namespace Product_Catalog_New.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration _configuration;
        public QueueService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMessage(string queueName, string message)
        {
            string connectionString = _configuration["StorageConnectionString"];
           
            QueueClient queueClient = new QueueClient(connectionString, queueName, new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });
            queueClient.CreateIfNotExists();
            if (queueClient.Exists())
            {
                queueClient.SendMessage(message);
            }

        }
    }
}
