using System;
using System.Collections.Generic;

namespace OrderApp.Service
{
    public class OperationReturn<T> where T : class
    {
        #region Constructors.

        /// <summary>
        /// Basic class constructor.
        /// </summary>
        public OperationReturn()
        {
            this.MessageList = new List<OperationInfo>();

            if (typeof(T).Equals(typeof(string)))
            {
                return;
            }

            this.Data = Activator.CreateInstance<T>();
        }

        #endregion

        #region Properties

        public T Data { get; set; }

        public bool Success { get; set; }

        public List<OperationInfo> MessageList { get; set; }

        #endregion
    }
}