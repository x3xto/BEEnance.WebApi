using Expenses.DB;
using System.Collections.Generic;

namespace Expenses.Core
{
    public interface IIncomesServises
    {
        List<Income> GetIncomes();
    }
}
