using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.UI
{
    /// <summary>
    /// Unloads its scene when interacted
    /// </summary>
    public class UnloadScene : MonoBehaviour
    {
        public void UnloadThisScene() => SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
