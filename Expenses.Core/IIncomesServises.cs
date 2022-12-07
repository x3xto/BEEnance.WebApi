using Expenses.Core.DTO;
using System.Collections.Generic;

namespace Expenses.Core
{
    public interface IIncomesServises
    {
        List<Income> GetIncomes();
        Income GetIncome(int id);
        Income CreateIncome(DB.Income income);
        void DeleteIncome(Income income);
        Income EditIncome(Income income);
    }
}
