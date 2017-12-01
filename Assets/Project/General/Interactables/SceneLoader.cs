using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Interactables
{
    public class SceneLoader : MonoBehaviour, IInteractable
    {
        public string Scene;

        public void Interact()
        {
            SceneManager.LoadScene(Scene, LoadSceneMode.Additive);
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
        }

        private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == Scene)
            {
                SceneManager.SetActiveScene(scene);
                SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
            }
        }
    }
}
