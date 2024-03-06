namespace TasksManagement.Application.Models;

public sealed class Result<TData>
{
    public TData? Data { get; private set; }
    public bool WasSuccessful { get; private set; }
    public string? ErrorMessage { get; private set; }

    public Result(TData data)
    {
        Data = data;
        WasSuccessful = true;
    }

    public Result(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
