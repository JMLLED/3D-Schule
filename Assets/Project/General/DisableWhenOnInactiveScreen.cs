using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.General
{
    public class DisableWhenOnInactiveScreen : MonoBehaviour
    {
        public void Start()
        {
            SceneManager.activeSceneChanged += (_, newScene) => gameObject.SetActive(gameObject.scene == newScene); //Wenn die aktive Scene verändert wurde wird überprüft, ob die jetzige Szene die Szene des GameObjects ist. Jenachdem wird das Objekt aktiviert oder deaktiviert
        }

        private void OnDisable() //Beim deaktivieren wird der Cursor "Freigegeben"
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
