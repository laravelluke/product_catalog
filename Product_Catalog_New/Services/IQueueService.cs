namespace Product_Catalog_New.Services
{
    public interface IQueueService
    {

        void SendMessage(string queueName, string message);

    }
}
