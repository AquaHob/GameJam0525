using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public SoundFXManager SoundFXManager;
    public void ButtonSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.menuClickSound, transform, 1.0f);
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player has quit the game");
    }
}
  
