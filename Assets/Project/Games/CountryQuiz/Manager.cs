using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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
                country.GameObject.OnClick += () => CheckCountry(country); //Jedes Land bekommt ein Event welches CheckCountry ausführt
            }

            unusedCountries = new List<CountryData>(Countries);
            SelectNewCountry(); //Das erste Land wird ausgewählt
        }

        private bool coroutineRunning;

        private void CheckCountry(CountryData country)
        {
            if (!coroutineRunning)
                StartCoroutine(ShowStatus(country == currentCountry)); //Starte eine Coroutine, falls noch keine läuft, und zeige, ob das Land das richtige war
        }

        private IEnumerator ShowStatus(bool correct)
        {
            coroutineRunning = true;

            Color color = correct ? Color.green : Color.red; //Grün wenn richtig und rot wenn falsch

            float startTime = Time.time;
            while (startTime + 1 > Time.time) //Solange noch nicht eine Sekunde vergangen ist
            {
                Background.color = Color.Lerp(color, Color.white, Time.time - startTime); //Aktualisiere die Farbe (von rot/grün zu weiß)
                yield return new WaitForEndOfFrame(); //Und warte auf das Ende vom Spiel zeichnen
            }

            Background.color = Color.white; //Der Hintergrund wird am Ende zurück gesetzt auf Weiß

            if(correct) SelectNewCountry(); //Falls das Land richtig war wird ein neues ausgewählt

            coroutineRunning = false; //Die Coroutine ist zuende
        }

        private void SelectNewCountry()
        {
            if (unusedCountries.Any()) //Falls noch Länder zum auswählen übrig sind
            {
                var rnd = new Random();
                currentCountry = unusedCountries[rnd.Next(unusedCountries.Count)]; //Wähle ein zufälliges Land aus und speicher es ab
                unusedCountries.Remove(currentCountry); //Das Land kann nicht wmehr ausgewählt werden

                CountryText.text = currentCountry.Name; //Der Text wird aktualisiert
            }
            else
                SceneManager.UnloadSceneAsync(gameObject.scene); //Falls es keine Länder mehr gibt entlade die Szene und kehre zur Schulke zurück
        }
    }

    [Serializable]
    public class CountryData
    {
        [UsedImplicitly] public HoverCountry GameObject;
        [UsedImplicitly] public string Name;
    }
}
