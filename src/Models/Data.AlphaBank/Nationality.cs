﻿namespace Data.AlphaBank;

public partial class Nationality {
    public byte Id { get; set; }
    public string Description { get; set; } = null!;
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}