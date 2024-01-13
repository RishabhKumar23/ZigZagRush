using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // variable 
    public bool isGameStarted;
    public GameObject platformSpawner;

    [Header("GameOver")]
    public GameObject gameOverPanel;
    public GameObject newHighScoreImage;
    public Text lastScoreText;

    // 
    [Header("Score")]
    public Text scoreText;
    public Text bestText;
    public Text diamondText;
    public Text starText;

    //
    [Header("For Player")]
    public GameObject[] player;
    Vector3 playerStartPosition = new Vector3(0, 2, 0);
    int selectedCar = 0;

    int score;
    int bestScore, totalStar, totalDiamond;
    bool countScore;
    bool soundPlayed = false; // New flag to track if GameEndSound has been played
    bool GameEndSoundPlayed = false;

    // this will amke this script get called from any script
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            //Debug.Log("GameManager instance set");
        }
        else
        {
            Destroy(gameObject);
        }

        // Playes one time after game scene started 
        if (!soundPlayed)
        {
            SoundManager.sm.GameStartSound();
            soundPlayed = true;
        }

        // Get Selected car
        selectedCar = PlayerPrefs.GetInt("SelectCar");
        Instantiate(player[selectedCar], playerStartPosition, Quaternion.identity);

    } // end Awake

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);

        // play with old score
        if (PlayerPrefs.GetInt("oldScore") != 0)
        {
            score = PlayerPrefs.GetInt("oldScore");
            PlayerPrefs.SetInt("oldScore", 0);
        }

        // for Star
        totalStar = PlayerPrefs.GetInt("totalStar");
        starText.text = totalStar.ToString();

        // for Total Diamond
        totalDiamond = PlayerPrefs.GetInt("totalDiamond");
        diamondText.text = totalDiamond.ToString();

        // to store score in user mobile
        bestScore = PlayerPrefs.GetInt("bestScore");
        bestText.text = bestScore.ToString();

        //ResetGame();

    } // end start

    // to Reset the Game
    void ResetGame()
    {
        score = 0;
        bestScore = 0;
        totalStar = 0;
        totalDiamond = 0;

        // Update UI elements after resetting scores
        scoreText.text = score.ToString();
        bestText.text = bestScore.ToString();
        diamondText.text = totalDiamond.ToString();
        starText.text = totalStar.ToString();

        // Save the changes to PlayerPrefs
        PlayerPrefs.SetInt("bestScore", bestScore);
        PlayerPrefs.SetInt("totalStar", totalStar);
        PlayerPrefs.SetInt("totalDiamond", totalDiamond);
    } // End ResetGame


    // Update is called once per frame
    void Update()
    {
        if(!isGameStarted && soundPlayed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();
            }
        }
    }// end update

    // On game start
    public void GameStart()
    {
            isGameStarted = true;
            countScore = true;
            StartCoroutine(updateScore());
            platformSpawner.SetActive(true);
    } // end GameStart

    // On game over
    public void GameEnd()
    {
        if (!GameEndSoundPlayed)
        {
            SoundManager.sm.GameEndSound();
            GameEndSoundPlayed = true;
        }

        gameOverPanel.SetActive(true);
        lastScoreText.text = score.ToString();
        countScore = false;
        platformSpawner.SetActive(false);

        if(score > bestScore)
        {
            PlayerPrefs.SetInt("bestScore", score);
            newHighScoreImage.SetActive(true);
        }
    }// End GameEnd

    public void StartWithScore()
    {
        PlayerPrefs.SetInt("oldScore", score);
        SceneManager.LoadScene("MainGameScene");

    } // End StartWithScore

    IEnumerator updateScore()
    {
        while(countScore)
        {
            // this line make wile loop wait for i sec after each loop
            yield return new WaitForSeconds(1f);
            score++;
            if(score > bestScore)
            {
                bestText.text = score.ToString();
            }
            else
            {
                scoreText.text = score.ToString();
            }
        }

    } // end updateScore

    public void ReplayGame()
    {
        SceneManager.LoadScene("MainGameScene");
    } // end ReplayGame

    public void Homepage()
    {
        SceneManager.LoadScene("carGarage");
    } // End Homepage

    public void GetStar()
    {
        SoundManager.sm.StarSound();
        int newStar = totalStar++;
        PlayerPrefs.SetInt("totalStar", newStar);
        starText.text = totalStar.ToString();
    } // end get star

    public void GetDiamond()
    {
        SoundManager.sm.DiamondSound();
        int newDiamond = totalDiamond++;
        PlayerPrefs.SetInt("totalDiamond", newDiamond);
        diamondText.text = totalDiamond.ToString();
    } // end get diamond
}
