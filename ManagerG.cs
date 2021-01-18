using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerG : MonoBehaviour
{
    //Reference of our player... 
    private FroggieMovement player;

    //Track player movement..
    private Transform playerCurrentMovement;

    private const int COINS_COUNT = 4;

    //Random Ads Purposes..
    private int adsShowNumber = 3;
    private int adsRandomNumber;

    //Voy a usar esta variable para chequiar si es cierta, para activar movimiento en el test movement.. 
    public bool isGameStarted = false;

    public static ManagerG Instance { get; set; }

    //UI & UI Field
    public Text scoreText, modifierText;
    private float score, coinScore, modifierScore;

    private int lastScore;

    //Text Mesh Pro Variables & Components..
    public TextMeshProUGUI potionsCount;
    public TextMeshProUGUI coinsCountText;
    public TextMeshProUGUI distanceScoreCountText;
    public TextMeshProUGUI bestDistanceText;
    public TextMeshProUGUI bestDistanceTextCount;


    public float scoreSaved;
    public int pointNumberCount;
    public int coinsCount = 0;
    public int potionsCost = 80;
    public int addingPotion = 0;

    //Variables meant to be used whenever the player dies..
    public TextMeshProUGUI transferredDistanceScore;
    public TextMeshProUGUI transferredBestDistanceScore;

    // Use this for initialization
    void Start()
    {
        isGameStarted = false;
        //Getting a random number.
        //adsRandomNumber = Random.Range(1, 4);

        //When the player grabs a bee, ads 4 points.

        //These are meant to only show up when the player dies.. 
        //transferredDistanceScore = GetComponent<TextMeshProUGUI>();
        //transferredBestDistanceScore = GetComponent<TextMeshProUGUI>();


        //Getting those components..
        //distanceScoreCountText = GetComponent<TextMeshProUGUI>();
        //bestDistanceText = GetComponent<TextMeshProUGUI>();
        //coinsCountText = GetComponent<TextMeshProUGUI>();
        //potionsCount = GetComponent<TextMeshProUGUI>();

        //Re-activate some of these things for the second update.. 
        //Gathering Best Distance Score.. 
        distanceScoreCountText.text = PlayerPrefs.GetFloat("Best Distance",0).ToString();


        bestDistanceTextCount.text = PlayerPrefs.GetFloat("Best Distance", 0).ToString("0");
        //potionsCount.text = PlayerPrefs.GetInt("Potions", 0).ToString();
        //coinsCountText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
        distanceScoreCountText.text = playerCurrentMovement.transform.position.z.ToString("0");
        //distanceScoreCountText.text = player.transform.position.z.ToString("0");

        transferredDistanceScore.text = PlayerPrefs.GetFloat("Old Score", 0).ToString("0");
        transferredBestDistanceScore.text = bestDistanceTextCount.text;



        
        

    }
    private void Awake()
    {
        Instance = this;

        //Getting those components..
        //distanceScoreCountText = GetComponent<TextMeshProUGUI>();
        //bestDistanceText = GetComponent<TextMeshProUGUI>();
        //oinsCountText = GetComponent<TextMeshProUGUI>();
       // potionsCount = GetComponent<TextMeshProUGUI>();

        modifierScore = 1;

        //Player Component.. 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FroggieMovement>();

        //Player Transform.. 
        playerCurrentMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        //As soon as the game started, we would be updaing that information.. 
        //UpdateScore();

        modifierText.text = "x" + modifierScore.ToString("0.0");
        //coinText.text = coinScore.ToString("0");
        scoreText.text = score.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        //Activate this classes for the second update.. 
        //BuyingPotionsInGame();
        //UsingPotions();

        SavingDistance();
        

        //All of this must removed from the Froggie Movement script which it was already implemented.
        //If the game hasn't started yet, then don't count no points.. 
        if (MobileInput.Instance.Tap || Input.GetKeyDown(KeyCode.X))
        {
            //Game has started already
            isGameStarted = true;

            //Moving player then (CHECK FROGGIE MOVEMENT SCRIPT IN ORDER TO REMOVE IT FROM THERE)..
            //player.ForwardMovement(isGameStarted);

            //player.ForwardMovement(isGameStarted);
        }

        //If the game has started..
        if (isGameStarted == true)
        {
            //Updating New Score, Implementation..


            //Starting player movement. 
            //FroggieMovement.Instance.ForwardMovement(isGameStarted);

            //Testing Purposes.
            //FroggieMovement.Instance.testingMovement(isGameStarted);

            //Saving Score..
            //SavingDistance();

            //Moving player forward.. 
            //player.ForwardMovement(isGameStarted);
            //Bump the score up.. 
            score += (Time.deltaTime * modifierScore);

            //This one too.. 
            if (lastScore != (int)score)
            {
                //This line can be deleted, we're just testing.. 
                lastScore = (int)score;

                //This can be put out of the if statement, we're just testing..
                Debug.Log(lastScore);
                //Testing.. ESTE ES EL QUE ESTA JODIENDO
                scoreText.text = score.ToString("0");
            }
            //Maybe calling UpdateScore up here.. 

        }
    }
    public void UpdateScore()
    {
        //scoreText.text = "Score:" + scoreText.ToString();
        scoreText.text = score.ToString();
        //coinText.text = coinText.ToString();

    }

    public void CollectingCoins()
    {
        coinsCount = coinsCount + COINS_COUNT;
        if (coinsCount > PlayerPrefs.GetInt("Coins", 0))
        {
            //Saving coins..
            PlayerPrefs.SetInt("Coins", coinsCount);
            coinsCountText.text = coinsCount.ToString();
        }
    }

    public void BuyingPotionsInGame()
    {
        if (PlayerPrefs.GetInt("Coins", 0) >= potionsCost)
        {
            int currentAmmount = PlayerPrefs.GetInt("Coins", 0);

            //Adding potion..
            addingPotion = addingPotion + 5;

            PlayerPrefs.SetInt("Potions", addingPotion);
            //Placing potion information on the text..
            potionsCount.text = addingPotion.ToString();

            //Minues Initial Potions Costs.. 
            currentAmmount = currentAmmount - potionsCost;

            PlayerPrefs.SetInt("Coins", currentAmmount);
            coinsCountText.text = currentAmmount.ToString();
        }
    }

    public void UsingPotions()
    {
        if (PlayerPrefs.GetInt("Potions", 0) > 0)
        {
            int availablePotions = PlayerPrefs.GetInt("Potions", 0);
            availablePotions = availablePotions - 1;

            //Granting potions effect..
            float potionEffect = 23f;

            //Assign variable to the method.. 
            Stamima.Instance.pickingUpStamina(potionEffect);


            //How many potions do you have available now.. 
            potionsCount.text = availablePotions.ToString();

            //Saving those new available potions now..
            PlayerPrefs.SetInt("Potions", availablePotions);
        }

    }

    public void SavingDistance()
    {
        //Distance from player.. Changed from Player to PlayerMovement (And that's a Transform Variable).
        distanceScoreCountText.text = playerCurrentMovement.transform.position.z.ToString("0");
        scoreSaved = playerCurrentMovement.transform.position.z;

        //Saving Old Score..
        PlayerPrefs.SetFloat("Old Score", scoreSaved);

        if (scoreSaved > PlayerPrefs.GetFloat("Best Distance", 0))
        {
            //Saving New Score
            PlayerPrefs.SetFloat("Best Distance", scoreSaved); ;

            //Aqui quite un 0 en la primera linea.. 
            bestDistanceTextCount.text = scoreSaved.ToString();
            //transferredBestDistanceScore.text = scoreSaved.ToString();
            //transferredDistanceScore.text = scoreSaved.ToString();

            transferredDistanceScore.text = PlayerPrefs.GetFloat("Old Score", 0).ToString("0");
         
            /*
            //If player breaks the record, Sweet display a random number..
            if (adsShowNumber == adsRandomNumber)
            {
                //Showing Video Ads..
                //AdsManager.Instance.showingInterestialAds();

                //Selecting a different random number..
                adsRandomNumber = Random.Range(1, 4);
            }
            */
            

        }

        transferredDistanceScore.text = PlayerPrefs.GetFloat("Old Score", 0).ToString("0");
    }

    //Coin..
    public void GetCoin()
    {
        //coinScore += COINS_SCORE;

        coinScore++;
        //coinText.text = coinScore.ToString("0");
        //score += COINS_SCORE;


        //instead they used scoreText, but its actually this one below
        //coinText.text = coinScore.ToString();

        //Testing aqui!
        scoreText.text = score.ToString();

        //This is how it looks on the video.. 
        //scoreText.text = scoreText.text = score.ToString("0");
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
        //UpdateScore();
    }

}
