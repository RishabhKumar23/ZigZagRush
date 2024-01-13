using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// _car == _index

public class SelectCar : MonoBehaviour
{
    // creating instance (note: it must be same as class name)
    public static SelectCar instance;

    [SerializeField] Button PrevBtn;
    [SerializeField] Button nextBtn;
    [SerializeField] Button useBtn;
    [SerializeField] GameObject buyPanel;

    int currentCar;
    string ownCarIndex;
    Color redColor = new Color(1f, 0.1f, 0.1f, 1f);
    Color greenColor = new Color(0.5f, 1f, 0.4f, 1f);

    int haveStars, haveDiamonds;
    int carValue = 700;

    [Header("Buy Panel")]
    public Text haveStarText;
    public Text haveDiamondsText;
    public Text needMoreText;
    public Button buyCarButton;
    public Button closePanelButton;
    public Button buyStar_diamond_btn;

    // Start is called before the first frame update
    private void Start()
    {
        haveStars = PlayerPrefs.GetInt("totalStar");
        haveDiamonds = PlayerPrefs.GetInt("totalDiamond");

    } // end start()

    private void Awake()
    {
        Debug.Log("SelectCar Awake");
        if(instance == null)
        {
            instance = this;
        }

        ChangeCar(0);

    } // end Awake()

    // Update is called once per frame
    void Update()
    {

        
    } // end update()

    public void ChangeCar(int _change)
    {
        currentCar += _change;
        ChooseCar(currentCar);

        ownCarIndex = "CarNumber" + currentCar;
        if(useBtn != null)
        {
            if (PlayerPrefs.GetInt(ownCarIndex) == 1)
            {
                // useBtn.GetComponent<Image>().color = greenColor;
                useBtn.GetComponentInChildren<Text>().text = "";

            }
            else
            {
                // useBtn.GetComponent<Image>().color = redColor;
                useBtn.GetComponentInChildren<Text>().text = "BUY";
            }
        }
        else
        {
            Debug.LogError("useBtn is not assigned in the Unity Editor.");
        }

    } // end changeCar()

    void ChooseCar(int _car)
    {
        PrevBtn.interactable = (_car != 0);
        nextBtn.interactable = (_car != transform.childCount - 1);

        for (int i = 0; i < transform.childCount; i++)
        {
            string CarNumber = "CarNumber" + i;
            if(i == 0)
            {
                PlayerPrefs.SetInt(CarNumber, 1);
            }
            transform.GetChild(i).gameObject.SetActive(i == _car);
        }

    } // end chooseCar()


    public void UseButtonClick()
    {
        haveStars = PlayerPrefs.GetInt("totalStar");
        haveDiamonds = PlayerPrefs.GetInt("totalDiamonds");

        if (PlayerPrefs.GetInt(ownCarIndex) == 1)
        {
            PlayerPrefs.SetInt("SelectCar", currentCar);
            SceneManager.LoadScene("MainGameScene");

        }
        else
        {
            buyPanel.SetActive(true);
            // when buyPanel is active then inactive below buttons
            haveStarText.text = "You Have " + haveStars + " Start";
            haveDiamondsText.text = "You Have " + haveDiamonds + " Diamond";

            if(haveStars < carValue)
            {
                int needStarInt = carValue - haveStars;
                buyCarButton.interactable = false;
                needMoreText.text = needStarInt + " more star needed";
            }
            else
            {
                buyCarButton.interactable = true;
                needMoreText.text = "Value: " + carValue + " stars";
            }
            if(haveDiamonds < 1)
            {
                buyStar_diamond_btn.interactable = false;
            }
            PrevBtn.interactable = false;
            nextBtn.interactable = false;
            useBtn.interactable = false;

        }
    } // end UseButtonClick()


    public void ClosePanel()
    {
        buyPanel.SetActive(false);
        PrevBtn.interactable = true;
        nextBtn.interactable = true;
        useBtn.interactable = true;

    }

    public void BuyStar()
    {
        haveDiamonds -= 1;
        haveStars += 10;
        PlayerPrefs.SetInt("totalStar", haveStars);
        PlayerPrefs.SetInt("totalDiamonds", haveDiamonds);
        SetText();

    } // end buy star

    public void EarnStar()
    {
        // haveStars = PlayerPrefs.GetInt("totalStar");
        haveStars += 100;
        PlayerPrefs.SetInt("totalStar", haveStars);
        SetText();

    } // end EarnStar()

    void SetText()
    {
        buyPanel.SetActive(true);

        haveStarText.text = "You Have " + haveStars + " stars";
        haveDiamondsText.text = "You Have " + haveDiamonds + " Diamonds";

        if(haveStars < carValue)
        {
            int needStarInt = carValue - haveStars;
            buyCarButton.interactable = false;
            needMoreText.text = needStarInt + " more Star needed";
        }
        PrevBtn.interactable = false;
        nextBtn.interactable = false ;
        useBtn.interactable = false;

    } // end SetText()

    public void BuyThisCar()
    {
        PlayerPrefs.SetInt(ownCarIndex, 1);
        haveStars -= carValue;
        PlayerPrefs.SetInt("totalStar", haveStars);
        int currentMinOne = currentCar - 1;
        ChangeCar(currentMinOne);
        ClosePanel();

    } // end BuyThisCar()
}
