using System;

namespace ExampleProjectsCommomResources.Domain.Models.ApplicationModels
{
    public class Error
    {
        public string Description { get; private set; }

        public string Detail { get; private set; }

        public string Stack { get; private set; }

        public Error(string description)
        {
            Description = description;
        }

        public Error(string description, string detail)
        {
            Description = description;
            Detail = detail;
        }

        public Error(Exception exception)
        {
            Description = exception.Message;
            Detail = exception.InnerException != null ? exception.InnerException.Message : null;
            Stack = exception.StackTrace;
        }
    }
}
