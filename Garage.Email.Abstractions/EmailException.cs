using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Garage.Email.Abstractions
{
    /// <summary>
    /// Represents exceptions related to messaging services.
    /// </summary>
    [Serializable]
    public class EmailException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailException"/> class.
        /// </summary>
        public EmailException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailException"/> class.
        /// </summary>
        /// <param name="message">The exception message</param>
        public EmailException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailException"/> class.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="inner">The inner exception</param>
        public EmailException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailException"/> class.
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected EmailException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
