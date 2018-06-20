namespace OrderApp.Service
{   
    public class OperationInfo
    {
        #region Constructors

        /// <summary>
        /// Basic class constructor.
        /// </summary>
        public OperationInfo() { }

        /// <summary>
        /// Alternative class constructor.
        /// </summary>
        /// <param name="code">Code of a operation info</param>
        /// <param name="message">A message of a operation info</param>
        public OperationInfo(string code, string message)
        {

            this.Code = code;
            this.Message = message;
        }

        #endregion

        #region Properties
        
        public string Code { get; set; }
        
        public string Message { get; set; }

        #endregion

    }
}