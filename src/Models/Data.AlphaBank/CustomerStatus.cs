﻿namespace Data.AlphaBank;

public partial class CustomerStatus {
    public byte Id { get; set; }
    public string Description { get; set; } = null!;
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}