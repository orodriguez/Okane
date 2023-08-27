namespace Okane.Contracts;

public class Errors : Dictionary<string, string[]>
{
    public Errors(string property, string errorMessage) => 
        this[property] = new[] { errorMessage };

    public Errors(Dictionary<string,string[]> errorsDictionary) : base(errorsDictionary)
    {
    }
}