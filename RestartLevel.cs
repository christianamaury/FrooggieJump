using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{

    public static RestartLevel Instance {get; set;}

    //For the Interestial Ads Count..
    int playerDiedRandom;
    int playerDiedCount;

    //Reference of the gameObject
    public GameObject restartCanvasObject;
    //public GameObject shopCanvasObjects;


    // Start is called before the first frame update
    void Start()
    {
        playerDiedCount = 2;

        //..From 1 to 4.
        playerDiedRandom = Random.Range(1, 5);

        //Canvas panel doesn't show when the game starts, Testing voy a apagar esto para un instance.. 
        restartCanvasObject.SetActive(false);
        //shopCanvasObjects.SetActive(false);

        Time.timeScale = 1;
    }

    void Awake ()
    {
        Instance = this;
        restartCanvasObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showingRestartMenu()
    {
        restartCanvasObject.SetActive(true);
        //Turning off TextMesh Pro from the Main Screen.. 
        ManagerG.Instance.bestDistanceText.gameObject.SetActive(false);
        ManagerG.Instance.distanceScoreCountText.gameObject.SetActive(false);
        ManagerG.Instance.bestDistanceTextCount.gameObject.SetActive(false);

        Time.timeScale = 0;
    }

    //Needs to be added to the Restart Button in the Canvas
    public void levelRestart ()
    {
        RandomInterestialAdsShow();

        //Restarting Level.,.. 
        Debug.Log("Button a sido presionado");

        ManagerG.Instance.bestDistanceText.gameObject.SetActive(false);
        ManagerG.Instance.distanceScoreCountText.gameObject.SetActive(false);
        ManagerG.Instance.bestDistanceTextCount.gameObject.SetActive(false);
        restartCanvasObject.SetActive(true);
        Time.timeScale = 0;

    }

    public void restartingActualLevel()
    {
        restartCanvasObject.SetActive(false);
        ManagerG.Instance.bestDistanceText.gameObject.SetActive(true);
        ManagerG.Instance.distanceScoreCountText.gameObject.SetActive(true);
        ManagerG.Instance.bestDistanceTextCount.gameObject.SetActive(true);
        Time.timeScale = 1;

        //Re-load current scene.. 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RandomInterestialAdsShow()
    {
        if(playerDiedCount == playerDiedRandom)
        {
            //Show Interestial videos Ads..
            AdsManager.Instance.showingInterestialAds();
        }
        else
        {
            return;
        }
    }

    public void showingShopStore()
    {
        //Turning off Restart Menu..
        restartCanvasObject.SetActive(false);
        //shopCanvasObjects.SetActive(true);

        //Time.timeScale = 0;     
    }

    public void backToMainMenu()
    {
        //shopCanvasObjects.SetActive(false);
        restartCanvasObject.SetActive(true);

        //Time.timeScale = 0;
    }

    public void openStudioWebsite()
    {
        //Needs to be tested to see if it works in iOS devices, if not use AbsoluteUrl
        Application.OpenURL("https://www.sweetestproductions.com/our-products");
    }
        
    //Quitting Game
    public void quitGame()
    {
        Application.Quit();
    }
}
