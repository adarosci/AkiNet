using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace AkiNet
{
    public class Client
    {
        private Client()
        {
            webClient = new WebClient();
        }
        // Base WebClient
        private WebClient webClient;

        // Aki Base Variables
        private string session;
        private string signature;
        private int step;

        // Aki Question Information
        public Entities.CQuestion Question;

        // Read Only Variables
        public static readonly string NEW_SESSION_URI = "http://api-en4.akinator.com/ws/new_session?partner=1&player=1";
        public static readonly string ANSWER_QUESTION_URI = "http://api-en4.akinator.com/ws/answer?session={0}&signature={1}&step={2}&answer={3}";
        public static readonly string LIST_GUESS_URI = "http://api-en4.akinator.com/ws/list?session={0}&signature={1}&mode_question=0&step={2}";

        public enum AnswerOptions
        {
            Yes,
            No,
            DontKnow,
            Probably,
            ProbablyNot
        }
        public enum AnswerReturnType
        {
            Question,
            Guess,
            Unknown
        }

        public static Client StartGame()
        {
            Client c = new Client();
            string JSONNewGame;
            using (var downloadStringTask = c.webClient.DownloadStringTaskAsync(NEW_SESSION_URI))
            {
                downloadStringTask.Wait();
                JSONNewGame = downloadStringTask.Result;
            }
            Entities.BaseResponse<Entities.NewGameParameters> Response = JsonConvert.DeserializeObject<Entities.BaseResponse<Entities.NewGameParameters>>(JSONNewGame, new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
            c.session = Response.Parameters.Identification.Session;
            c.signature = Response.Parameters.Identification.Signature;
            c.step = Response.Parameters.StepInformation.Step;
            c.Question = Response.Parameters.StepInformation;
            return c;
        }

        public Entities.CQuestion Answer(AnswerOptions answer)
        {
            string AnswerUri = String.Format(ANSWER_QUESTION_URI, session, signature, step, (int)answer);
            string JSONNewQuestion;
            using (var downloadStringTask = webClient.DownloadStringTaskAsync(AnswerUri))
            {
                downloadStringTask.Wait();
                JSONNewQuestion = downloadStringTask.Result;
            }
            Entities.BaseResponse<Entities.CQuestion> Response = JsonConvert.DeserializeObject<Entities.BaseResponse<Entities.CQuestion>>(JSONNewQuestion, new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
            Question = Response.Parameters;
            step = Response.Parameters.Step;
            return Response.Parameters;
        }

        public Entities.CGuess GetGuess()
        {
            string ListUri = String.Format(LIST_GUESS_URI, session, signature, step);
            string JSONGuessList;
            using (var downloadStringTask = webClient.DownloadStringTaskAsync(ListUri))
            {
                downloadStringTask.Wait();
                JSONGuessList = downloadStringTask.Result;
            }
            Entities.BaseResponse<Entities.CGuess> Response = JsonConvert.DeserializeObject<Entities.BaseResponse<Entities.CGuess>>(JSONGuessList, new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
            return Response.Parameters;
        }
    }
}
