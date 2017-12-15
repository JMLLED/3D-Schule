using UnityEngine;

namespace Project.General.Player
{
    public class Interact : MonoBehaviour
    {
        public float MaxDistance = 3;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetButtonDown("Interact"))
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.forward, out hit, MaxDistance))
                {
                    hit.transform.GetComponent<IInteractable>()?.Interact();
                }
            }
        }
    }
}
