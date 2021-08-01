using System;

namespace ChrisBeere.MMTDigital.WebApi.ExternalApiServices.CustomExceptions
{
    /// <summary>
    /// Defines a custom exception that is raised when a user is not found 
    /// or the customer id does not match the user.
    /// </summary>
    public class CustomerNotFoundException : Exception
    {
    }
}
