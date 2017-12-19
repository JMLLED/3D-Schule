

namespace Project.Games.Quiz
{
    public class Frage
    {

        private string frage;
        private bool antwort;

        public Frage(string pFrage, bool pAntwort)
        {
            frage = pFrage;
            antwort = pAntwort;
        }

        public string GetFrage()
        {
            return frage;
        }

        public bool GetAntwort()
        {
            return antwort;
        }
    }
}
