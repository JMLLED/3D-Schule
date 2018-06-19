using UnityEngine;
using UnityEngineInternal;

namespace Project.General.Player
{
    public class Interact : MonoBehaviour
    {
        public float MaxDistance = 3;

        private void Update()
        {
            if (Input.GetButtonDown("Interact")) //Wenn die "Interagieren"-Taste gedrückt wurde
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.forward, out hit, MaxDistance)) //Wird ein "Raycast" gemacht um zu schauen, mit was man interagiert hat
                {
                    (hit.transform.GetComponent<IInteractable>() ?? hit.transform.GetComponentInParent<IInteractable>())?.Interact(); //Sollte man mit einem GameObject interagiert haben, welches das IInteractable interface hat wird Interact() ausgeführt, soltte es das nicht haben wird in den Eltern gesucht
                }
            }
        }
    }
}
