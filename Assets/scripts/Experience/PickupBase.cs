using UnityEngine;

public class PickupBase : MonoBehaviour
{
    public AudioClip pickupSound;

    // Start is called before the first frame update
    private void PlayPickUpSound()
    {
        GameObject audioObject = new("PickupSound");
        audioObject.transform.position = transform.position;

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = pickupSound;
        audioSource.Play();

        Destroy(audioObject, pickupSound.length);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayPickUpSound();
            Destroy(gameObject);
        }
    }
}
