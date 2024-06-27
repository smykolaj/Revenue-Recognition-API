using Project.Models;

namespace Project.Repositories.Interfaces;

public interface IPaymentsRepository
{
    string PaymentStatusPartial { get; }
    string PaymentStatusFull { get; }
    string PaymentStatusCancelled { get; }
    Task SetAllContractPaymentsToCancelled(long idContract);
    Task<Payment> AddPayment(Payment newPayment);
    Task<List<Payment>> GetPaymentsByContractId(long contractId);
}