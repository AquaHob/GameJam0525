using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Won : MonoBehaviour
{
    public SoundFXManager SoundFXManager;

    public void ButtonSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(SoundFXManager.menuClickSound, transform, 1.0f);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
}