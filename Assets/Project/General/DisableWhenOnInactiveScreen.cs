using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.General
{
    public class DisableWhenOnInactiveScreen : MonoBehaviour
    {
        public void Start()
        {
            SceneManager.activeSceneChanged += (_, newScene) => gameObject.SetActive(gameObject.scene == newScene);
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
