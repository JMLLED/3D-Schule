using System.Collections;
using UnityEngine;

namespace Project.General.UI
{
    public class HideTutorialScript : MonoBehaviour
    {
        public GameObject Tutorial;

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            Tutorial.transform.SetAsLastSibling(); //Das Tutorial ist das oberste und versteckt alles hinter sich
        }

        public void HideTutorial() => Tutorial.SetActive(false); //Das Tutorial wird deaktiviert und unsichtbar
    }
}
