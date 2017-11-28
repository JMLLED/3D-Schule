using UnityEngine;

namespace Project.Player
{
    public class Interact : MonoBehaviour
    {
        public float MaxDistance = 3;

        // Update is called once per frame
        public void Update()
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
