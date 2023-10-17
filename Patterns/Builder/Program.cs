using System;
using Builder.Builder;

var codeBuilder = new CodeBuilder()
    .AddClass()
        .SetName("User")
        .SetAccessModifier(AccessModifier.Public)
    .AddField()
        .AddName("Name")
        .AddAccessModifier(AccessModifier.Private)
        .AddType("string")
    .AddField()
        .AddName("Age")
        .AddAccessModifier(AccessModifier.Private)
        .AddType("int")
    .AddClass()
        .SetName("Player")
        .SetAccessModifier(AccessModifier.Private)
    .AddField()
        .AddName("Rating")
        .AddAccessModifier(AccessModifier.Internal)
        .AddType("int");


Console.WriteLine(codeBuilder);
