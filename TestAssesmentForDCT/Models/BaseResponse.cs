namespace TestAssesmentForDCT.Models
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; } = default;
    }
}
