using SpeedyAir.Data.DTO;
using SpeedyAir.Data.Repositories.Interfaces;

namespace SpeedyAir.Data.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private static ICollection<Order> _orders;

        public Task<ICollection<Order>> GetOrdersAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_orders);
        }

        public Task UploadOrdersAsync(ICollection<Order> orders, CancellationToken cancellationToken)
        {
            _orders = orders;
            return Task.CompletedTask;
        }
    }
}
