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
                SceneManager.UnloadSceneAsync(gameObject.scene);
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

        [UsedImplicitly]
        public void True()
        {
            if (current == null) return;

            StartCoroutine(NextQuestion(current.Answer));

        }

        [UsedImplicitly]
        public void False()
        {
            if (current == null) return;

            StartCoroutine(NextQuestion(!current.Answer));
        }

        private IEnumerator NextQuestion(bool rightAnswer)
        {
            Vector2 center = Vector2.Lerp(TrueButton.transform.position, FalseButton.transform.position, 0.5f);
            Vector2 oldPos;

            current = null;

            if (rightAnswer)
            {
                FalseButton.SetActive(false);
                oldPos = TrueButton.transform.position;
                TrueButton.transform.position = center;
            }
            else
            {
                TrueButton.SetActive(false);
                oldPos = FalseButton.transform.position;
                FalseButton.transform.position = center;
            }

            yield return new WaitForSeconds(2);

            if (rightAnswer)
            {
                FalseButton.SetActive(true);
                TrueButton.transform.position = oldPos;
            }
            else
            {
                TrueButton.SetActive(true);
                FalseButton.transform.position = oldPos;
            }

            RandomQuestion();
        }
    }
}
