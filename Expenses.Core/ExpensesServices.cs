using Expenses.Core.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Expenses.Core
{
    public class ExpensesServices : IExpensesServices
    {
        private DB.AppDbContext _context;
        private readonly DB.User _user;
        public ExpensesServices(DB.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users
                .First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);
        }

        public Expense CreateExpense(DB.Expense expense) // створити новий запис про витрату
        {
            expense.User = _user;
            _context.Add(expense);
            _context.SaveChanges();

            return (Expense)expense;
        }

        public void DeleteExpense(Expense expense) // видалити запис про витрату
        {
            var dbExpense = _context.Expenses.First(e => e.User.Id == _user.Id && e.Id == expense.Id);
            _context.Expenses.Remove(dbExpense);
            _context.SaveChanges();
        }

        public Expense EditExpense(Expense expense) // редагувати запис про витрату
        {
            var dbExpense = _context.Expenses.First(e => e.User.Id == _user.Id && e.Id == expense.Id);
            dbExpense.Description = expense.Description;
            dbExpense.Amount = expense.Amount;
            _context.SaveChanges();

            return expense;
        }

        public Expense GetExpense(int id) =>
            _context.Expenses
            .Where(e => e.User.Id == _user.Id && e.Id == id)
            .Select(e => (Expense)e)
            .First();

        public List<Expense> GetExpenses() =>
            _context.Expenses
            .Where(e => e.User.Id == _user.Id)
            .Select(e => (Expense)e)
            .ToList();
    }
}
