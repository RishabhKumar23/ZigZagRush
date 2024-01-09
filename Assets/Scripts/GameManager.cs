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

    // this will amke this script get called from any script
    //public static GameManager Instance;

    private void Awake()
    {
        Debug.Log("GameManager Awake");
        if(instance == null)
        {
            instance = this;

            //Debug.Log("GameManager instance set");
        }
        else
        {
            Destroy(gameObject);
        }
        // Get Selected car
        selectedCar = PlayerPrefs.GetInt("SelectCar");
        Instantiate(player[selectedCar], playerStartPosition, Quaternion.identity);

    } // end Awake

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);

        Debug.Log("Scene Name" + SceneManager.GetActiveScene().name);

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

    } // end start

    // Update is called once per frame
    void Update()
    {
        if(!isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();
            }
        }
    }// end update

    // on game start
    public void GameStart()
    {
        isGameStarted = true;
        countScore = true;
        StartCoroutine(updateScore());
        platformSpawner.SetActive(true);
    } // end GameStart

    // on game over
    public void GameEnd()
    {
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
        Debug.Log("StartWithScore => started");
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
            // scoreText.text = score.ToString("D5");
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
        // Debug.Log("GetStar called start");
        
        int newStar = totalStar++;
        PlayerPrefs.SetInt("totalStar", newStar);
        starText.text = totalStar.ToString();
        
        // Debug.Log("GetStar called end");

    } // end get star

    public void GetDiamond()
    {
        // Debug.Log("GetDiamond called start");
        
        int newDiamond = totalDiamond++;
        PlayerPrefs.SetInt("totalDiamond", newDiamond);
        diamondText.text = totalDiamond.ToString();
        
        // Debug.Log("GetDiamond called start");

    } // end get diamond
}
