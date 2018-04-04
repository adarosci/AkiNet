using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AkiNet.Entities
{
    public class CQuestion : BaseParameters
    {
        internal class CAnswer
        {
            [JsonProperty("answer")]
            public string Answer { get; set; }
        }
        public class Answer
        {
            private Answer() {}
            public string Yes { get; set; }
            public string No { get; set; }
            public string DontKnow { get; set; }
            public string Probably { get; set; }
            public string ProbablyNot { get; set; }

            internal static Answer ParseAnswersAkiList(List<CAnswer> list)
            {
                Answer a = new Answer();
                a.Yes = list[0].Answer;
                a.No = list[1].Answer;
                a.DontKnow = list[2].Answer;
                a.Probably = list[3].Answer;
                a.ProbablyNot = list[4].Answer;
                return a;
            }
        }

        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("answers")]
        private List<CAnswer> _Answers { get; set; }
        public Answer Answers
            => Answer.ParseAnswersAkiList(_Answers);

        [JsonProperty("step")]
        public int Step { get; set; }

        [JsonProperty("progression")]
        public double Progression { get; set; }

        [JsonProperty("questionid")]
        public int QuestionId { get; set; }

        [JsonIgnore]
        private ulong Infogain { get; set; }

        public override string ToString()
         => Question;
    }
}
