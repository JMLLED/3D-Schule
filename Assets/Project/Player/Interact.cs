using UnityEngine;

namespace Project.Player
{
    public class Interact : MonoBehaviour {

        public Camera Camera1;

        // Update is called once per frame
        public void Update () {
            if (Input.GetButtonDown("Interact"))
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, Camera1.transform.forward, out hit)) {
                    if(hit.distance <= 3)
                    {
                        hit.transform.GetComponent<IInteractable>().Interact();
                    }
                }

            }
        }
    }
}
