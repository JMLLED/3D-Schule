using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Games.Quiz
{
    public class Quiz : MonoBehaviour
    {
        private PolarQuestion Current;
        public PolarQuestion[] Frage;
        private  List<PolarQuestion> Unanswered;
        public Text Text;

        private void RandomQuestion()
        {
            if (Unanswered.Count != 0)
            {
                int temp = Random.Range(0, Unanswered.Count);
                Current = Unanswered[temp];
                Text.text = Current.Question;
                Unanswered.RemoveAt(temp);
            }
            else
            {
                Current = null;
            }
        }

        public void Start()
        {
            if (Unanswered == null || Unanswered.Count == 0)
            {
                Unanswered = Frage.ToList();
            }
            RandomQuestion();
        }

        public void True()
        {
            if (Current != null)
            {
                Debug.Log(Current.Answer ? "Richtig" : "Falsch");
                RandomQuestion();
            }
            else
            {
                Debug.Log("Keine Fragen mehr!");
            }
        }

        public void False()
        {
            if (Current != null)
            {
                Debug.Log(Current.Answer ? "Falsch" : "Richtig");
                RandomQuestion();
            }
            else
            {
                Debug.Log("Keine Fragen mehr!");
            }
        }


    }
}
