using System;

namespace SynetecAssessmentApi.Infrastructure
{
    /// <summary>
    /// SyntechExceptionn that contains the main exception
    /// </summary>
    [Serializable]
    public class SyntechException : Exception
    {
        private readonly SyntechExceptionType _exceptionType;


        /// <summary>
        /// Gets the SyntechExceptionType
        /// Can be used to map the message shown on the client based on standardized exception type   
        /// </summary>
        public SyntechExceptionType ExceptionType => _exceptionType;

        public SyntechException(Exception innerException, SyntechExceptionType exceptionType = SyntechExceptionType.Default) : base("", innerException)
        {
            _exceptionType = exceptionType;
        }
    }
}
