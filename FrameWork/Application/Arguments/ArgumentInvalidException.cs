using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Application.Arguments
{
    public class ArgumentInvalidException : Exception
    {
        public ArgumentInvalidException()
        {
        }

        public ArgumentInvalidException(string? message) : base(message)
        {
        }

        public ArgumentInvalidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ArgumentInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
