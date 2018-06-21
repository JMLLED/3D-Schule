using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.General.Interactables
{
    public class SceneLoader : MonoBehaviour, IInteractable
    {
        public string Scene;

        public void Interact() //Beim Interagieren wird die angegebene Szene geöffnet
        {
            SceneManager.LoadScene(Scene, LoadSceneMode.Additive);
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
        }

        private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) //Wenn die Szene geladen wurde
        {
            if (scene.name == Scene)
            {
                StartCoroutine(activatingCoroutine(scene));
                SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
            }
        }

        private IEnumerator activatingCoroutine(Scene scene)
        {
            yield return new WaitForEndOfFrame();
            SceneManager.SetActiveScene(scene); //Wird sie "aktiv" gemacht, damit sie im Vordergrund ist
        }
    }
}
