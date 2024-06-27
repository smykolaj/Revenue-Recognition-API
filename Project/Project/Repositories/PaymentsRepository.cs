using Project.Context;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly ProjectContext _context;

        public PaymentsRepository(ProjectContext context)
        {
            _context = context;
        }

        public string PaymentStatusPartial => "Partial";
        public string PaymentStatusFull => "Full";
        public string PaymentStatusCancelled => "Cancelled";

        public async Task SetAllContractPaymentsToCancelled(long idContract)
        {
            foreach (var payment in _context.Payments.Where(p => p.IdContract == idContract))
            {
                payment.Status = PaymentStatusCancelled;
            }
            await _context.SaveChangesAsync(); // Save changes
        }

        public async Task<Payment> AddPayment(Payment newPayment)
        {
            await _context.Payments.AddAsync(newPayment);
            await _context.SaveChangesAsync(); // Save changes
            return newPayment;
        }

        public async Task<List<Payment>> GetPaymentsByContractId(long contractId)
        {
            throw new NotImplementedException();
        }
    }
}