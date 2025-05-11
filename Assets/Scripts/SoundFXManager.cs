using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;
    public AudioClip babyCrySound;
    public AudioClip destroySound;
    public AudioClip babyHappySound;
    public AudioClip gameOverSound;
    public AudioClip correctItemDeliveredSound;
    public AudioClip wonSound;
    public AudioClip combineSuccessSound;
    public AudioClip combineFailureSound;
    public AudioClip dropSound;
    public AudioClip pickUpSound;
    public AudioClip menuClickSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {

        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);

    }

    public void PlayPickUpSound()
    {

    }

}