using UnityEngine;

namespace Project.Interactables
{
    public class Door : MonoBehaviour, IInteractable
    {
        public float OpeningTime;
        private bool open;
        private Quaternion startingRotation;
        private Quaternion openedRotation;
        private float openingTime;


        public void Interact()
        {
            if (openingTime > 0) return;

            open = !open;
            openingTime = OpeningTime;
        }

        private void Start()
        {
            startingRotation = transform.localRotation;
            openedRotation = Quaternion.Euler(startingRotation.eulerAngles + new Vector3(0, 90, 0));
        }

        private void Update()
        {
            if (openingTime > 0)
            {
                openingTime -= Time.deltaTime;
                transform.localRotation =
                    open
                        ? Quaternion.Slerp(openedRotation, startingRotation, openingTime / OpeningTime)
                        : Quaternion.Slerp(startingRotation, openedRotation, openingTime / OpeningTime);
            }
        }
    }
}
