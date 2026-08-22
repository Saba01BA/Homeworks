using LoanApi.Models;
using LoanApi.Models.Dto;

namespace LoanApi.Services
{
    public interface ILoanService
    {
        Task<List<Loan>> GetUserLoansAsync(int userId);
        Task<Loan?> GetUserLoanAsync(int loanId, int userId);
        Task<Loan> CreateAsync(int userId, CreateLoanDto dto);
        Task<bool> UpdateAsync(int loanId, int userId, UpdateLoanDto dto);
        Task<bool> DeleteAsync(int loanId, int userId);
        Task<List<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(int loanId);
        Task<bool> AccountantUpdateAsync(int loanId, UpdateLoanDto dto);
        Task<bool> ChangeStatusAsync(int loanId, LoanStatus status);
        Task<bool> AccountantDeleteAsync(int loanId);
    }
}
