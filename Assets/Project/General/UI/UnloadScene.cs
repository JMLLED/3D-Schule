using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.General.UI
{
    public class UnloadScene : MonoBehaviour
    {
        public void UnloadThisScene() => SceneManager.UnloadSceneAsync(gameObject.scene); //Die Szene wird entladen
    }
}
