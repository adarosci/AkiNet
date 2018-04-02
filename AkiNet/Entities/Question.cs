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
            public bool Yes { get; set; } = false;
            public bool No { get; set; } = false;
            public bool DontKnow { get; set; } = false;
            public bool Probably { get; set; } = false;
            public bool ProbablyNot { get; set; } = false;

            internal static Answer ParseAnswersAkiList(List<CAnswer> list)
            {
                Answer a = new Answer();
                foreach (CAnswer answer in list)
                {
                    switch (answer.Answer.ToLower())
                    {
                        case "yes":
                            a.Yes = true;
                            break;
                        case "no":
                            a.No = true;
                            break;
                        case "don't know":
                            a.DontKnow = true;
                            break;
                        case "probably":
                            a.Probably = true;
                            break;
                        case "probably not":
                            a.ProbablyNot = true;
                            break;
                        default:
                            throw new Exceptions.NotValidAnswerException(answer.Answer);
                    }
                }
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
