using System;
using System.Runtime.Serialization;

namespace BumpVersion
{
    [Serializable]
    public class MessageException : Exception
    {
        public MessageException()
        {
        }

        public MessageException(string message) : base(message)
        {
        }

        public MessageException(string message, Exception inner) : base(message, inner)
        {
        }

        protected MessageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
