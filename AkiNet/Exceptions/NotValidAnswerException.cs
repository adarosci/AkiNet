using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkiNet.Exceptions
{
    public class NotValidAnswerException : Exception
    {
        public NotValidAnswerException(string Answer) : base($"Answer {Answer} is not valid.") { }
        public NotValidAnswerException(string Answer, string message) : base($"Answer {Answer} is not valid.\n{message}") { }
        public NotValidAnswerException(string Answer, string message, Exception inner) : base($"Answer {Answer} is not valid.\n{message}", inner) { }
        protected NotValidAnswerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
