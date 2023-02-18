using System;
using Patterns.Builder;

var pb = new PersonBuilder()
    .Lives()
        .InStreet("Luchi")
        .InHouse("32")
        .WithPostCode("33223322")
    .Work()
        .At("Engineer")
        .HasSalary(999999)
    .Build();

Console.WriteLine(pb.ToString());
