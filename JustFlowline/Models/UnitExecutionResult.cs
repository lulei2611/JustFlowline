namespace JustFlowline.Models
{
    public class UnitExecutionResult
    {
        public bool Success { get; set; }

        public object Outcome {  get; set; }

        public static UnitExecutionResult Next()
        {
            return new UnitExecutionResult()
            {
                Success = true,
            };
        }
    }
}
