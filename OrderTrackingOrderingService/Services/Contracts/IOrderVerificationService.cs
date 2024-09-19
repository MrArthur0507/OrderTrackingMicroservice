using OrderTrackingOrdereringService.Models.DbModels;

namespace OrderTrackingOrderingService.Services.Contracts
{
    public interface IOrderVerificationService
    {
        Task SendOrderForVerification(Order order);
    }
}
