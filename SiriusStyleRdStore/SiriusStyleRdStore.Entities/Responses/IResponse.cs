using System.Collections.Generic;

namespace SiriusStyleRdStore.Entities.Responses
{
    public interface IResponse
    {
    }

    public class Success<T> : IResponse
    {
        public T Response { get; set; }
    }

    public class Error : IResponse
    {
        public Error(string errorMessage)
        {
            ErrorMessage = $"Hubo un error durante el proceso: \n{errorMessage}";
        }

        public string ErrorMessage { get; }
    }

    public class Validation : IResponse
    {
        public Validation(IEnumerable<string> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public IEnumerable<string> ValidationErrors { get; }
    }
}