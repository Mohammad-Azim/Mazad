using Application.Helper.ServiceExtensions;
using FluentValidation.Results;

namespace Application.Helper.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public T Data { get; set; }
        public CodeStatusEnum StatusCode { get; set; }
        public BaseResponse()
        {
            Success = true;
        }
        public BaseResponse(string message)
        {
            Success = true;
            Message = message;
        }

        public BaseResponse<T> NotFoundResponse(string message = "Not found")
        {
            Success = false;
            ValidationErrors.Add(message);
            StatusCode = CodeStatusEnum.NotFound;
            return this;
        }
        public BaseResponse<T> SuccessResponse(T data = default, string message = "Success")
        {
            Success = true;
            StatusCode = CodeStatusEnum.Ok;
            Message = message;
            Data = data;
            return this;
        }

        public BaseResponse<T> ErrorsResponse(List<ValidationFailure> errors, string message = "There is an Error")
        {
            Success = false;
            Message = message;
            StatusCode = CodeStatusEnum.UnprocessableEntity;
            foreach (var error in errors)
            {
                ValidationErrors.Add(error.ErrorMessage);
            }
            return this;
        }

    }
}