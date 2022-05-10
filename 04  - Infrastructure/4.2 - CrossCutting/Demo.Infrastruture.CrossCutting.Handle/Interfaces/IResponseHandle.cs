using FluentValidation.Results;

namespace Demo.Infrastruture.CrossCutting.Handle.Interfaces
{
    public interface IResponseHandle
    {
        bool IsValid { get; }
        List<object> Errors { get; }
        void NewError(ValidationResult validationResult);
        void NewError(List<object> errors);
        void NewError(string errorMessage);
    }
}
