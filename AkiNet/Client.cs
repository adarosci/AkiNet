﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using AkiNet;
using System.Net.Http;

namespace AkiNet
{
    public enum AnswerOptions
    {
        Unknown = -1,
        Yes = 0,
        No = 1,
        DontKnow = 2,
        Probably = 3,
        ProbablyNot = 4
    }
    [Serializable]
    public class Client
    {
        private Client()
        {
            webClient = new HttpClient();
        }
        // Base Variables
        private HttpClient webClient;
        private Language UsedLanguage;

        // Aki Base Variables
        private string session;
        private string signature;
        private int step;

        // Aki Question Information
        public Entities.CQuestion Question;

        // Read Only Variables
        public static readonly string BASE_NEW_SESSION_URI = "https://{0}/ws/new_session?partner=1&player=website-desktop&constraint=ETAT<>'AV'";
        public static readonly string BASE_ANSWER_QUESTION_URI = "https://{0}/ws/answer?session={{0}}&signature={{1}}&step={{2}}&answer={{3}}";
        public static readonly string BASE_LIST_GUESS_URI = "https://{0}/ws/list?session={{0}}&signature={{1}}&mode_question=0&step={{2}}";
        public static IReadOnlyDictionary<Language, string> AkiLanguagesHosts = new Dictionary<Language, string>()
            .AddRangeEx(new Language[]
            {
                Language.English,
                Language.Arabic,
                Language.Chinese,
                Language.German,
                Language.Spanish,
                Language.Frence,
                Language.Hebrew,
                Language.Italian,
                Language.Japanese,
                Language.Korean,
                Language.Nederlands,
                Language.Polish,
                Language.Portuguese,
                Language.Russian,
                Language.Turkish,
            }, new string[]
            {
                "srv2.akinator.com:9157",
                "srv2.akinator.com:9155",
                "srv5.akinator.com:9125",
                "srv7.akinator.com:9145",
                "srv6.akinator.com:9127",
                "api-fr3.akinator.com/ws",
                "srv9.akinator.com:9131",
                "srv9.akinator.com:9131",
                "srv10.akinator.com:9120",
                "srv2.akinator.com:9156",
                "srv9.akinator.com:9133",
                "srv7.akinator.com:9143",
                "srv3.akinator.com:9166",
                "srv5.akinator.com:9124",
                "srv3.akinator.com:9164"
            });
        public static string NEW_SESSION_URI(Language Language) =>
            String.Format(BASE_NEW_SESSION_URI, AkiLanguagesHosts[Language]);
        public string ANSWER_QUESTION_URI =>
            String.Format(BASE_ANSWER_QUESTION_URI, AkiLanguagesHosts[UsedLanguage]);
        public string LIST_GUESS_URI =>
            String.Format(BASE_LIST_GUESS_URI, AkiLanguagesHosts[UsedLanguage]);


        public enum AnswerReturnType
        {
            Question,
            Guess,
            Unknown
        }
        public enum Language
        {
            English,
            Arabic,
            Chinese,
            German,
            Spanish,
            Frence,
            Hebrew,
            Italian,
            Japanese,
            Korean,
            Nederlands,
            Polish,
            Portuguese,
            Russian,
            Turkish
        }

        public static Client StartGame(Language Language = Language.English)
        {
            Client c = new Client();
            string JSONNewGame;
            using (var downloadStringTask = c.webClient.GetStringAsync(NEW_SESSION_URI(Language)))
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
            c.UsedLanguage = Language;
            return c;
        }

        public Entities.CQuestion Answer(AnswerOptions answer)
        {
            string AnswerUri = String.Format(ANSWER_QUESTION_URI, session, signature, step, (int)answer);
            string JSONNewQuestion;
            using (var downloadStringTask = webClient.GetStringAsync(AnswerUri))
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
            using (var downloadStringTask = webClient.GetStringAsync(ListUri))
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

        public AnswerOptions AnswerTextToAnswerOption(string Answer)
        {
            if (Answer.ToLower() == Question.Answers.Yes.ToLower()) return AnswerOptions.Yes;
            else if (Answer.ToLower() == Question.Answers.No.ToLower()) return AnswerOptions.No;
            else if (Answer.ToLower() == Question.Answers.DontKnow.ToLower()) return AnswerOptions.DontKnow;
            else if (Answer.ToLower() == Question.Answers.Probably.ToLower()) return AnswerOptions.Probably;
            else if (Answer.ToLower() == Question.Answers.ProbablyNot.ToLower()) return AnswerOptions.ProbablyNot;
            else return AnswerOptions.Unknown;
        }
    }
}
