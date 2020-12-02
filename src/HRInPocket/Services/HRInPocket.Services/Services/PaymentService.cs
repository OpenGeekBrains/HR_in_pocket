using System;
using System.Threading.Tasks;

using HRInPocket.Interfaces.Services;

namespace HRInPocket.Services.Services
{
    public class PaymentService : IPaymentService
    {
        public PaymentService() { }

        public Task CalculationDiscountsAsync() => throw new NotImplementedException();

        public Task PaymentAsync() => throw new NotImplementedException();
    }
}
