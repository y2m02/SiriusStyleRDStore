using System.Collections.Generic;

namespace SiriusStyleRd.Entities.ViewModels
{
    public interface IViewModel
    {
    }

    public class Success<T> : IViewModel
    {
        public T Response { get; set; }
    }

    public class Error : IViewModel
    {
        public Error(string errorMessage)
        {
            ErrorMessage = $"Hubo un error durante el proceso: \n{errorMessage}";
        }

        public string ErrorMessage { get; }
    }

    public class Validation : IViewModel
    {
        public Validation(IEnumerable<string> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public IEnumerable<string> ValidationErrors { get; }
    }
}