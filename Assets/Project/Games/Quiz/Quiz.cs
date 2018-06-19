using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Project.Games.Quiz
{
    public class Quiz : MonoBehaviour
    {
        private PolarQuestion current;
        public PolarQuestion[] Frage;
        private List<PolarQuestion> unanswered;
        public Text Text;
        public GameObject TrueButton;
        public GameObject FalseButton;


        private void RandomQuestion() //Wählt eine zufällige Frage aus
        {
            if (unanswered.Count != 0) //Falls es noch Fragen gibt
            {
                int temp = Random.Range(0, unanswered.Count); //Wähle eine zufällige aus und entferne sie von den möglichen
                current = unanswered[temp];
                Text.text = current.Question; //Aktualisiere den Text
                unanswered.RemoveAt(temp);
            }
            else //Falls nicht
            {
                current = null;
                SceneManager.UnloadSceneAsync(gameObject.scene); //Entlade die Szene
            }
        }

        public void Start()
        {
            if (unanswered == null || unanswered.Count == 0)
            {
                unanswered = Frage.ToList(); //Speichere alle Fragen ob um mögliche auszuwählen
            }
            RandomQuestion(); //Wähle eine zufällige erste Frage aus

        }

        [UsedImplicitly]
        public void True()
        {
            if (current == null) return;

            StartCoroutine(NextQuestion(current.Answer)); //Starte eine Coroutine und übergebe, ob der Spieler richtig war

        }

        [UsedImplicitly]
        public void False()
        {
            if (current == null) return;

            StartCoroutine(NextQuestion(!current.Answer)); //Starte eine Coroutine und übergebe, ob der Spieler richtig war
        }

        private IEnumerator NextQuestion(bool rightAnswer)
        {
            Vector2 center = Vector2.Lerp(TrueButton.transform.position, FalseButton.transform.position, 0.5f); //Rechne das Zentrum aus, um dort das Ergebnis zu platzieren
            Vector2 oldPos;

            current = null;

            if (rightAnswer)
            {
                FalseButton.SetActive(false); //Wenn die Frage richtig beantwortet wurde wird der "Falsch" Knopf ausgeblendet
                oldPos = TrueButton.transform.position; //Und der "Richtig" Knopf in die Mitte bewegt
                TrueButton.transform.position = center;
            }
            else
            {
                TrueButton.SetActive(false);
                oldPos = FalseButton.transform.position;
                FalseButton.transform.position = center;
            }

            yield return new WaitForSeconds(2); //Warte zwei Sekunden

            if (rightAnswer) //Setze die Animation zurück
            {
                FalseButton.SetActive(true);
                TrueButton.transform.position = oldPos;
            }
            else
            {
                TrueButton.SetActive(true);
                FalseButton.transform.position = oldPos;
            }

            RandomQuestion(); //Wähle am Schluss die neue Frage aus
        }
    }
}
