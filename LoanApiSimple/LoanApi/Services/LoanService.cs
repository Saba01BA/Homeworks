using LoanApi.Data;
using LoanApi.Models;
using LoanApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace LoanApi.Services
{
    public class LoanService : ILoanService
    {
        private readonly AppDbContext _context;

        public LoanService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Loan>> GetUserLoansAsync(int userId)
        {
            return await _context.Loans.Where(loan => loan.UserId == userId).ToListAsync();
        }

        public async Task<Loan?> GetUserLoanAsync(int loanId, int userId)
        {
            return await _context.Loans.FirstOrDefaultAsync(
                loan => loan.Id == loanId && loan.UserId == userId);
        }

        public async Task<Loan> CreateAsync(int userId, CreateLoanDto dto)
        {
            var user = await _context.Users.FindAsync(userId)
                ?? throw new InvalidOperationException("User was not found.");

            if (user.IsBlocked && user.BlockedUntil > DateTime.UtcNow)
                throw new InvalidOperationException("This user is blocked and cannot apply for a loan.");

            if (user.IsBlocked && user.BlockedUntil <= DateTime.UtcNow)
            {
                user.IsBlocked = false;
                user.BlockedUntil = null;
            }

            var loan = new Loan
            {
                LoanType = dto.LoanType,
                Amount = dto.Amount,
                Currency = dto.Currency,
                PeriodInMonths = dto.PeriodInMonths,
                Status = LoanStatus.Pending,
                UserId = userId
            };

            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<bool> UpdateAsync(int loanId, int userId, UpdateLoanDto dto)
        {
            var loan = await GetUserLoanAsync(loanId, userId);
            if (loan == null)
                return false;
            if (loan.Status != LoanStatus.Pending)
                throw new InvalidOperationException("Only pending loans can be updated.");

            UpdateFields(loan, dto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int loanId, int userId)
        {
            var loan = await GetUserLoanAsync(loanId, userId);
            if (loan == null)
                return false;
            if (loan.Status != LoanStatus.Pending)
                throw new InvalidOperationException("Only pending loans can be deleted.");

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _context.Loans.ToListAsync();
        }

        public async Task<Loan?> GetByIdAsync(int loanId)
        {
            return await _context.Loans.FindAsync(loanId);
        }

        public async Task<bool> AccountantUpdateAsync(int loanId, UpdateLoanDto dto)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
                return false;
            UpdateFields(loan, dto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeStatusAsync(int loanId, LoanStatus status)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
                return false;
            loan.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AccountantDeleteAsync(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
                return false;
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return true;
        }

        private void UpdateFields(Loan loan, UpdateLoanDto dto)
        {
            loan.LoanType = dto.LoanType;
            loan.Amount = dto.Amount;
            loan.Currency = dto.Currency;
            loan.PeriodInMonths = dto.PeriodInMonths;
        }
    }
}
