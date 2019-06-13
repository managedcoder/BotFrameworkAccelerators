using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QnA.MultiturnQnAMaker
{
    /// <summary>
    /// This is passed as the context in the body of the call to generateanswer API in the QnA service
    /// </summary>
    public class MultiturnQnAState
    {
        public int PreviousQnaId { get; set; }

        public string PreviousUserQuery { get; set; }
    }
}
