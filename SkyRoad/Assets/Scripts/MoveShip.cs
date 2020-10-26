using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class StringExtension
{
    //static metod
    public static string ChangeScore(this int Score)
    {
        string ChangeScoreString = Score.ToString() + "'000";
        
        return ChangeScoreString;
    }
}

class GameParamert
{
    public void GameParametrsFloat(float Speed, float TimeGame)
    {
        Debug.Log(Speed+1);
        Debug.Log(TimeGame+1);
    }
}

class GameParametrs
{
    public void GameParamersDouble(double Speed, double TimeGame)
    {
        Debug.Log(Speed);
        Debug.Log(TimeGame);
    }
}
public class MoveShip : MonoBehaviour
{
    
    [SerializeField]public Text SpeedText, ScoreText, DataGameOver;
    public Rigidbody Ship;
    public float SpeedStart;
    public bool GameOver = false, Record = false;
    public int Score = 0;
    public GameObject GameOverPanel, RecordMessage, ExplosionObj;
    public float SpeedNow;
    private float SpeedRot = 10f, TimeGame = 0,  Cooldown = 0;
    private int CountTime = 1, CountAsteroid = 0, MaxScore = 0, MaxAsteroid = 0;
    private AudioSource AudioSource;
    private AudioClip[] AudioClips;
    private Animator Animator;
    private GameObject Shot;
    
    private void Awake()
    {
        var I = CountTime;
        GameParamert gameParamert = new GameParamert();
        gameParamert.GameParametrsFloat(TimeGame,Cooldown);
        //Loading maximum score
        if (PlayerPrefs.HasKey("SkyShip:Score"))
        {
            MaxScore = PlayerPrefs.GetInt("SkyShip:Score");
        }
        if (PlayerPrefs.HasKey("SkyShip:Asteroid"))
        {
            MaxAsteroid = PlayerPrefs.GetInt("SkyShip:Asteroid");
        }
        //Get Gaming Component
        Ship = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
        Animator = GetComponent<Animator>();
        GameOverPanel.SetActive(false);
        RecordMessage.SetActive(false);
        //Load Audio Clips
        AudioClips = Resources.LoadAll<AudioClip>("Sound");
        //disable menu
        ExplosionObj.SetActive(false);
    }
    void Start()
    {
        SpeedNow = SpeedStart;
        //Freeze the game at the start
        Time.timeScale = 0;
    }
    private void Movment()
    {
        //Start game if player down any Key
        if (Input.anyKey) 
        {
            Time.timeScale = 1;
        }
        //Constant movement of the ship
        Ship.transform.Translate(new Vector3(0, 0, 1) * SpeedNow * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        //Control
        if (Input.GetKey(KeyCode.D) && transform.position.x < 2.5f)
        {
            transform.Translate(Vector3.right * SpeedRot * Time.deltaTime);
            //Sound Rot
            if (!AudioSource.isPlaying)
            {
                AudioSource.PlayOneShot(AudioClips[5]);
            }  
            transform.rotation = Quaternion.Euler(0, 0,  -5);
        }
        //Control
        if (Input.GetKey(KeyCode.A) && transform.position.x > -2.5f)
        {
           transform.Translate(-Vector3.right * SpeedRot * Time.deltaTime);
            //Sound Rot
            if (!AudioSource.isPlaying)
            {
                AudioSource.PlayOneShot(AudioClips[5]);
            }
            transform.rotation = Quaternion.Euler(0, 0, 5);
        }
        //Acceleration
        if (Input.GetKey(KeyCode.Space))
        {
            SpeedNow = SpeedStart*1.3f;
            //Sound acceleration
            if (!AudioSource.isPlaying)
            {
                AudioSource.PlayOneShot(AudioClips[2]);
            }
            //Animation acceleration
            Animator.SetBool("2xSpeed", true);
        }
        else
        {
            SpeedNow = SpeedStart;
            Animator.SetBool("2xSpeed", false);
        }
        //Shot Blast
        if (!GameOver)
        {
          if (Input.GetKeyDown(KeyCode.Mouse0) && Cooldown <= 0)
                  {
                      //Shot Sound
                      AudioSource.PlayOneShot(AudioClips[4]);
                      //Shot Spawn
                      Shot = Instantiate(Resources.Load<GameObject>("Prefabs/Shot"));
                      //Shot transform
                      Shot.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3);
                      //Cooldown shot
                      Cooldown = 2;
                  }  
        }
        //Exit Game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Collision Asteroid
        if(other.tag == "Trap")
        {
            //ollision Asteroid Sound
            AudioSource.PlayOneShot(AudioClips[0]);
            SpeedStart = 0;
            SpeedRot = 0;
            GameOver = true;
            //Save Max Score
            if(Score > MaxScore)
            {
                Record = true;
                MaxScore = Score;
                PlayerPrefs.SetInt("SkyShip:Score", MaxScore);
            }
            //Save max Asteroid
            if (CountAsteroid > MaxAsteroid)
            {
                Record = true;
                MaxAsteroid = CountAsteroid;
                PlayerPrefs.SetInt("SkyShip:Asteroid", MaxAsteroid);
            }
            
        }
        //Count Score Asteroid
        if(other.tag == "Asteroid")
        {
            Score += 5;
            CountAsteroid += 1;
        }
    }
    /// <summary>
    /// Count Score standart
    /// </summary>
    private void ScoreCount()
    {
        if(TimeGame >= CountTime)
        {
            //Double Score
            if (Input.GetKey(KeyCode.Space))
            {
                Score += 2;
            }
            else
            {
                Score += 1;
            }
            CountTime += 1;
        }
    }
    void Update()
    {
        //Time Game
        TimeGame += Time.deltaTime;
        //Time Cooldown
        Cooldown -= Time.deltaTime;
        Movment();
        ScoreCount();
        //UI interface
        SpeedText.text = "Speed: " + SpeedNow.ToString();
        ScoreText.text = "Score: " + Score.ChangeScore();
        DataGameOver.text = "Score: " + Score.ToString() +"\n" + "Time Game: " + CountTime.ToString()
            + "\n"+ "Asteroid Count: " + CountAsteroid.ToString() + "\n"+ "Max Score Game: " + MaxScore.ToString() + "\n"+"Max Asteroid Count: "+ MaxAsteroid.ToString();
        
    }
    
}
