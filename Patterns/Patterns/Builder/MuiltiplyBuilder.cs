namespace Patterns.Builder;

public class Person
{
    // Address
    public string Street, House, Postcode;
    
    // Job
    public string JobName;
    public int Salary;

    public override string ToString() =>
        $"{nameof(Street)} -> {Street}\n" +
        $"{nameof(House)} -> {House}\n" +
        $"{nameof(Postcode)} -> {Postcode}\n\n" +
        $"{nameof(JobName)} -> {JobName}\n" +
        $"{nameof(Salary)} -> {Salary}\n";
}

public class PersonBuilder
{
    private Person _person;

    public PersonBuilder()
    {
        _person = new Person();
    }

    public PersonBuilder(Person person)
    {
        _person = person;
    }

    public AddressBuilder Lives() => new(_person);

    public JobBuilder Work() => new(_person);

    public Person Build() => _person;

    public override string ToString() => _person.ToString();
}

public class AddressBuilder : PersonBuilder
{
    private readonly Person _person;

    public AddressBuilder(Person person) : base(person)
    {
        _person = person;
    }

    public AddressBuilder InStreet(string street)
    {
        _person.Street = street;
        return this;
    }
    
    public AddressBuilder InHouse(string house)
    {
        _person.House = house;
        return this;
    }    
    
    public AddressBuilder WithPostCode(string postCode)
    {
        _person.Postcode = postCode;
        return this;
    }
}

public class JobBuilder : PersonBuilder
{
    private readonly Person _person;
    
    public JobBuilder(Person person) : base(person)
    {
        _person = person;
    }
    
    public JobBuilder At(string jobName)
    {
        _person.JobName = jobName;
        return this;
    }
    
    public JobBuilder HasSalary(int salary)
    {
        _person.Salary = salary;
        return this;
    }    
}
