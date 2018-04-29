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
            Tutorial.transform.SetAsLastSibling();
        }

        public void HideTutorial() => Tutorial.SetActive(false);
    }
}
