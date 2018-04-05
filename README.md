# AkiNet
A free open-source client for playing Akinator using C#

How to use AkiNet?
First of all, connect the namespace: `using AkiNet;`
After it, initilize a new AkiNet Client `Client c = Client.StartGame();`, You can also provide a language: `Client c = Client.StartGame(Client.Language.Arabic);`
After it, you should to display the question, it stored in the Client.Question, just convert it to String: `c.Question.ToString();`
You can answer the question using `c.Answer(Client.AnswerOptions.Yes);`, this will also return the next question.
Request guesses using `c.GetGuesses();` and filter them, you can take a look on the Test application.

**Enjoy!**
