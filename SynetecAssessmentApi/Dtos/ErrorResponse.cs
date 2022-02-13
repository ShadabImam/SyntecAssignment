namespace SynetecAssessmentApi.Dtos
{
    /// <summary>
    /// Model Representing Error Response
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Error Message
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Error Code
        /// </summary>
        public string ErrorCode { get; set; }
    }
}
