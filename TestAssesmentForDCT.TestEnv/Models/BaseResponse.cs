namespace TestAssesmentForDCT.TestEnv.Models
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; } = default;
    }
}
