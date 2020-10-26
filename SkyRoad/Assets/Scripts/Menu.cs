using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioClip ButtomSound;
    [SerializeField] private Slider FonSlider;
    [SerializeField] private Slider MainSoundSlider;
    [SerializeField] private AudioSource MenuAudio;
    [SerializeField] private AudioSource OtherSound;
    private float Fon;
    private float SoundEvent;

    private void Update()
    {
        MenuAudio.volume = Fon;
        OtherSound.volume = SoundEvent;
    }

    public void FonSound()
    {
        Fon = FonSlider.value;
        Debug.Log(Fon);
    }
    public void MainSound()
    {
        SoundEvent = MainSoundSlider.value;
        Debug.Log(SoundEvent);
        
    }
    /// <summary>
    /// Restart Game
    /// </summary>
    public void RestartGame()
    {
        
        MenuAudio.PlayOneShot(ButtomSound);
        //Load Now Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Start Game
    /// </summary>
    public void StartGame()
    {
        MenuAudio.PlayOneShot(ButtomSound);
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Exit Game
    /// </summary>
    public void Exit()
    {
        MenuAudio.PlayOneShot(ButtomSound);
        Application.Quit();
    }
}
