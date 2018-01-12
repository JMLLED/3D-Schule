using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Games.Quiz
{
    public class Quiz : MonoBehaviour
    {
        private PolarQuestion current;
        public PolarQuestion[] Frage;
        private  List<PolarQuestion> unanswered;
        public Text Text;

        private void RandomQuestion()
        {
            if (unanswered.Count != 0)
            {
                int temp = Random.Range(0, unanswered.Count);
                current = unanswered[temp];
                Text.text = current.Question;
                unanswered.RemoveAt(temp);
            }
            else
            {
                current = null;
            }
        }

        public void Start()
        {
            if (unanswered == null || unanswered.Count == 0)
            {
                unanswered = Frage.ToList();
            }
            RandomQuestion();
        }

        public void True()
        {
            if (current != null)
            {
                Debug.Log(current.Answer ? "Richtig" : "Falsch");
                RandomQuestion();
            }
            else
            {
                Debug.Log("Keine Fragen mehr!");
            }
        }

        public void False()
        {
            if (current != null)
            {
                Debug.Log(current.Answer ? "Falsch" : "Richtig");
                RandomQuestion();
            }
            else
            {
                Debug.Log("Keine Fragen mehr!");
            }
        }


    }
}
