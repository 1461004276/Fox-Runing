using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class buttonControl : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public void pauseGame()
    {
        transform.Find("PauseMenu").gameObject.SetActive(true);
        GameObject.Find("fox").GetComponent<AudioSource>().Pause();
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        GameObject.Find("Canvas/PauseMenu").SetActive(false);
        GameObject.Find("fox").GetComponent<AudioSource>().Play();
        Time.timeScale = 1f;
    }
    
    public void returnMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    public void controlAudio(float volume)
    {
        AudioMixer.SetFloat("mainAudio",volume);
    }

    public void gameOver()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
}
