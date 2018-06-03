using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

namespace Project.Games.CountryQuiz
{
    public class Manager : MonoBehaviour
    {
        public Text CountryText;
        public Image Background;

        public CountryData[] Countries;
        private List<CountryData> unusedCountries;
        private CountryData currentCountry;

        private void Start()
        {
            foreach (var country in Countries)
            {
                country.GameObject.OnClick += () => CheckCountry(country);
            }

            unusedCountries = new List<CountryData>(Countries);
            SelectNewCountry();
        }

        private bool coroutineRunning;

        private void CheckCountry(CountryData country)
        {
            if (!coroutineRunning)
                StartCoroutine(ShowStatus(country == currentCountry));
        }

        private IEnumerator ShowStatus(bool correct)
        {
            coroutineRunning = true;

            Color color = correct ? Color.green : Color.red;

            float startTime = Time.time;
            while (startTime + 1 > Time.time)
            {
                Background.color = Color.Lerp(color, Color.white, Time.time - startTime);
                yield return new WaitForEndOfFrame();
            }

            Background.color = Color.white;

            if(correct) SelectNewCountry();

            coroutineRunning = false;
        }

        private void SelectNewCountry()
        {
            if (unusedCountries.Any())
            {
                var rnd = new Random();
                currentCountry = unusedCountries[rnd.Next(unusedCountries.Count)];
                unusedCountries.Remove(currentCountry);

                CountryText.text = currentCountry.Name;
            }
            else
                SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }

    [Serializable]
    public class CountryData
    {
        public HoverCountry GameObject;
        public string Name;
    }
}
