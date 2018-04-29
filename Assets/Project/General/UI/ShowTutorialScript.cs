using UnityEngine;

namespace Project.General.UI
{
    public class ShowTutorialScript : MonoBehaviour
    {
        public GameObject Tutorial;
        
        public void ShowTutorial() => Tutorial.SetActive(true);
    }
}
