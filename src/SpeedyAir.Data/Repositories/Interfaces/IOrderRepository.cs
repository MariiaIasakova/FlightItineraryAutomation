using SpeedyAir.Data.DTO;

namespace SpeedyAir.Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Collection of orders.</returns>
        Task<ICollection<Order>> GetOrdersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Uploads orders.
        /// </summary>
        /// <param name="orders">Orders.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UploadOrdersAsync(ICollection<Order> orders, CancellationToken cancellationToken);
    }
}
