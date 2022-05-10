using Demo.Infrastruture.CrossCutting.Handle.Interfaces;
using Demo.Infrastruture.CrossCutting.Handle.Model;
using FluentValidation.Results;

namespace Demo.Infrastruture.CrossCutting.Handle.Handler
{
    public class ResponseHandle : IResponseHandle
    {
        public bool IsValid { get; set; } = true;
        public List<object> Errors { get; private set; }

        public ResponseHandle()
        {
        }

        public ResponseHandle(string errorMessage)
        {
            NewError(errorMessage);
        }

        public void NewError(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                NewError(($"{error.PropertyName}: {error.ErrorMessage}"));
            }
            IsValid = false;
        }

        public void NewError(string errorMessage)
        {
            if (Errors is null)
            {
                Errors = new List<object>();
                IsValid = false;
            }

            Errors.Add(new Error(errorMessage));
        }

        public void NewError(List<object> errors)
        {
            Errors = errors;
            IsValid = false;
        }

    }
}
