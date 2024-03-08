using System;
using System.Collections.Generic;

namespace Database.AlphaBank.Models;

public partial class LoanApplication
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public DateOnly DateApplication { get; set; }

    public string Justification { get; set; } = null!;

    public string PathPatronalOrder { get; set; } = null!;

    public string PathProofSalary { get; set; } = null!;

    public int UserId { get; set; }

    public byte TypeCurrencyId { get; set; }

    public string AccountId { get; set; } = null!;

    public byte DeadlineId { get; set; }

    public byte TypeLoanId { get; set; }

    public byte ApplicationStatusId { get; set; }

    public byte InterestId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ApplicationStatus ApplicationStatus { get; set; } = null!;

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual Deadline Deadline { get; set; } = null!;

    public virtual Interest Interest { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual TypeCurrency TypeCurrency { get; set; } = null!;

    public virtual TypeLoan TypeLoan { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
