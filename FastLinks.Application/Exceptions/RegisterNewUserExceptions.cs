namespace FastLinks.Application.Exceptions;

//public sealed class RegisterNewUserExceptions(string message) : Exception(message) { }



public sealed class RegisterNewUserExceptions : Exception
{
    public IDictionary<string, string> Errors;
    public RegisterNewUserExceptions(IDictionary<string, string> Errors)
    {
        this.Errors = Errors;
    }

}
