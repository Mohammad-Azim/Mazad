using FluentValidation.Results;

namespace Application.Helper.Response
{
    public interface IBaseResponse<T>
    {
        bool Success { get; set; }
        string Message { get; set; }
        List<string> ValidationErrors { get; set; }
        T Data { get; set; }
        CodeStatusEnum StatusCode { get; set; }

        BaseResponse<T> ErrorsResponse(List<ValidationFailure> errors, string message = "There is an Error");
        BaseResponse<T> NotFoundResponse(string message = "Not found");
        BaseResponse<T> SuccessResponse(T data = default, string message = "Success");
    }
}