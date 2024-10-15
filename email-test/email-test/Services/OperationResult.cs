
namespace email_test.Services;

public class OperationResult<T>
{
    public bool IsSuccessful { get; set; }
    
    public int Code { get; set;}
    public string Description { get; set; }

    public T ResultValue { get; set; }
}