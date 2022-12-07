namespace Expenses.Core.DTO
{
    public class Income
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }

        public static explicit operator Income(DB.Income e) => new Income
        {
            Id = e.Id,
            Amount = e.Amount,
            Description = e.Description
        };
    }
}

