using System.Collections;
using UnityEngine;

namespace Project.General.Interactables
{
    public class Door : MonoBehaviour, IInteractable
    {
        public float OpeningTime;
        private bool open;
        private Quaternion startingRotation;
        private Quaternion openedRotation;
        private bool opening;


        public void Interact() //Wird beim interagieren ausgeführt
        {
            if (opening) return; //Falls die Tür bereits geöffnet wird wird direkt aufgehört

            open = !open; //Die Tür ist jetzt offen/geschlossen
            StartCoroutine(UpdateDoor()); //Die Coroutine ausgeführt
        }

        private void Start()
        {
            startingRotation = transform.localRotation; //Am Anfang wird die Anfangsrotation gespeichert
            openedRotation = Quaternion.Euler(startingRotation.eulerAngles + new Vector3(0, 90, 0)); //Und die geöffnete Rotation ausgerechnet
        }

        private IEnumerator UpdateDoor() //Aktualisiert die Rotation der Tür
        {
            opening = true; //Die Tür wird jetzt rotiert

            float startTime = Time.time; //Die Anfangszeit wird ausgerechnet
            while (startTime + OpeningTime > Time.time) //Solange die Tür sich rotieren soll
            {
                float currentOffset = Time.time - startTime; //Wird die vergangene Zeit ausgerechnet
                transform.localRotation =
                    open //Und jenachdem, ob geöffnet oder geschlossen werden soll die Rotation interpoliert
                        ? Quaternion.Slerp(startingRotation, openedRotation, currentOffset / OpeningTime)
                        : Quaternion.Slerp(openedRotation, startingRotation, currentOffset / OpeningTime);
                yield return null; //Danach wird auf den nächsten "Frame" gewartet
            }

            transform.localRotation = open ? openedRotation : startingRotation; //Als letztes wird die Rotation "Komplett" gemacht, falls die While schleife nicht schnell genug war

            opening = false; //Die Tür ist fertig rotiert
        }
    }
}
