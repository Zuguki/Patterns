using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    static class Program
    {
        static void Main(string[] args)
        {
            //var builder = new HtmlBuilder("ul");
            //builder.AddChild("li", "Hello!")
            //    .AddChild("li", "How r u?")
            //    .AddChild("li", "...");
            //Console.WriteLine(builder.ToString());

            //var pb = new PersonBuilder();
            //Person person = pb
            //    .Lives
            //        .At("145 London Road")
            //        .In("London")
            //        .WithPostcode("SW12BC")
            //    .Works
            //        .At("Google")
            //        .AsA("Engineer")
            //        .Earning(1229990);
//
            //Console.WriteLine(person);

            var cb = new CodeBuilder("Person")
                .AddField("Name", "string")
                .AddField("Age", "int");

            Console.WriteLine(cb);
        }
    }

    public class Field
    {
        public string Name, Type;

        public Field(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            return $"public {Type} {Name}";
        }
    }
    
    public class Code
    {
        public string ClassName;

        public List<Field> Fields = new List<Field>();
        
        private const int IndentSize = 4;

        public Code(string className)
        {
            ClassName = className;
        }

        public override string ToString()
        {
            return ToStringImpl(IndentSize);
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"public class {ClassName}")
                .AppendLine("{");
            foreach (var field in Fields)
                sb.AppendLine(new string(' ', indent) + field);

            sb.AppendLine("}");
            return sb.ToString();
        }
    }

    public class CodeBuilder
    {
        private Code _root;

        public CodeBuilder(string className)
        {
            _root = new Code(className);
        }

        public CodeBuilder AddField(string fieldName, string type)
        {
            var field = new Field(fieldName, type);
            _root.Fields.Add(field);
            return this;
        }

        public override string ToString() => _root.ToString();
    }
    
    public class Person
    {
        public string StreetAddress, Postcode, City;
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"StreetAddress: {StreetAddress}\nPostcode: {Postcode}\nCity: {City}\n\n" +
                   $"CompanyName: {CompanyName}\nPosition: {Position}\nAnnualIncome: {AnnualIncome}";
        }
    }

    public class PersonBuilder
    {
        protected Person Person;

        public PersonBuilder()
        {
            Person = new Person();
        }
        
        public PersonBuilder(Person person)
        {
            Person = person;
        }

        public PersonAddressBuilder Lives => new PersonAddressBuilder(Person);
        
        public PersonJobBuilder Works => new PersonJobBuilder(Person);

        public static implicit operator Person(PersonBuilder pb) => pb.Person;
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person) : base(person) { }

        public PersonAddressBuilder At(string streetAddress)
        {
            Person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            Person.City = city;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postCode)
        {
            Person.Postcode = postCode;
            return this;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person) : base(person) { }

        public PersonJobBuilder At(string companyName)
        {
            Person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            Person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int annulIncome)
        {
            Person.AnnualIncome = annulIncome;
            return this;
        }
    }

    public class HtmlElement
    {
        public string Name;
        public string Text;

        public List<HtmlElement> Elements = new List<HtmlElement>();

        private const int IndentSize = 2;

        public HtmlElement()
        { }

        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', IndentSize * indent);
            
            sb.Append($"{i}<{Name}>\n");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', IndentSize * (indent + 1)));
                sb.Append(Text);
                sb.Append('\n');
            }

            foreach (var element in Elements)
                sb.Append(element.ToStringImpl(indent + 1));

            sb.Append($"{i}</{Name}>\n");
            return sb.ToString();
        }
    }

    public class HtmlBuilder
    {
        protected HtmlElement Root = new HtmlElement();
        private readonly string _rootName;

        public HtmlBuilder(string rootName)
        {
            _rootName = rootName;
            Root.Name = rootName;
        }

        public HtmlBuilder AddChild(string childName, string childText)
        {
            var element = new HtmlElement(childName, childText);
            Root.Elements.Add(element);
            return this;
        }

        public override string ToString() => Root.ToString();

        public void Clear() => Root = new HtmlElement{Name = _rootName};

        public HtmlElement Build() => Root;
    }
}