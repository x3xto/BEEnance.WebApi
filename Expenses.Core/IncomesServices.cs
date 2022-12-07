using Expenses.Core.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Expenses.Core
{
    public class IncomesServices : IIncomesServises
    {
        private DB.AppDbContext _context;
        private readonly DB.User _user;
        public IncomesServices(DB.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users
                .First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);
        }

        public Income CreateIncome(DB.Income income)
        {
            income.User = _user;
            _context.Add(income);
            _context.SaveChanges();

            return (Income)income;
        }

        public void DeleteIncome(Income income)
        {
            var dbIncome = _context.Incomes.First(e => e.User.Id == _user.Id && e.Id == income.Id);
            _context.Incomes.Remove(dbIncome);
            _context.SaveChanges();
        }

        public Income EditIncome(Income income)
        {
            var dbIncome = _context.Incomes.First(e => e.User.Id == _user.Id && e.Id == income.Id);
            dbIncome.Description = income.Description;
            dbIncome.Amount = income.Amount;
            _context.SaveChanges();

            return income;
        }

        public Income GetIncome(int id) =>
            _context.Incomes
            .Where(e => e.User.Id == _user.Id && e.Id == id)
            .Select(e => (Income)e)
            .First();


        public List<Income> GetIncomes() =>
            _context.Incomes
            .Where(e => e.User.Id == _user.Id)
            .Select(e => (Income)e)
            .ToList();
    }
}
