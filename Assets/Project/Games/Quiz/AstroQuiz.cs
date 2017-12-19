using UnityEngine;

namespace Project.Games.Quiz
{
    public class AstroQuiz : MonoBehaviour
    {

        Frage[] fragen = new Frage[5];
        public int Reihenfolge;
        public bool Eingabe;

        public void Start ()
        {
            fragen[0] = new Frage("Frage1", true);
            fragen[1] = new Frage("Frage2", false);
            fragen[2] = new Frage("Frage3", false);
            fragen[3] = new Frage("Frage4", false);
            fragen[4] = new Frage("Frage5", true);
        }

        public bool Pruefen(int f)
        {
            return fragen[f].GetAntwort() == Eingabe;
        }

        public void Weiter()
        {

        }


    }
}
