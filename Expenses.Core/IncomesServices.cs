using Expenses.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expenses.Core
{
    class IncomesServices : IIncomesServises
    {
        private AppDbContext _context;
        public IncomesServices(AppDbContext context)
        {
            _context = context;
        }
        public List<Income> GetIncomes()
        {
            return _context.Incomes.ToList();
        }
    }
}
