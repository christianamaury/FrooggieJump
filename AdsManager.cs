using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;


//For Event Handlers..
using System;

public class AdsManager : MonoBehaviour
{

    public static AdsManager Instance {get; set;}

    //Android Reference Ads..
    private string androidAppID = "ca-app-pub-3187572158588519~6942387145";
    private string androidBanner = "ca-app-pub-3187572158588519/7967354892";
    private string androidInterestialID = "ca-app-pub-3187572158588519/3948558144";


    private string appID = "ca-app-pub-3187572158588519~6959038011";
    private string interestialAdsUnit = "ca-app-pub-3187572158588519/7294327354";
    private string testAds = "ca-app-pub-3940256099942544/2934735716";
    private string testInterestialdsAds = "ca-app-pub-3940256099942544/8691691433";
    private string bannerID = "ca-app-pub-3187572158588519/2828221313";

    private BannerView bannerView;
    private InterstitialAd interstitialAds;

    private void Awake()
    {
        Instance = this;

    #if UNITY_IOS
    //Initializing App ID.. 
    MobileAds.Initialize(appID);
    #endif

    #if UNITY_ANDROID
    //Initializing App ID...,
    
    Debug.Log("Android Dispositivo... settiando cosas");    
    MobileAds.Initialize(androidAppID);
    #endif

    }

    // Start is called before the first frame update
    void Start()
    {
        //Calling Request Banner and Interestial Ads..
        this.requestBanner();
        this.requestingVideoAds();
    }

    public void requestingVideoAds()
    {
        #if UNITY_IOS
        //Initializing process..
        this.interstitialAds = new InterstitialAd(interestialAdsUnit);

        //Creating an empty Adrequest..
        AdRequest request = new AdRequest.Builder().Build();

        //Load the interestial with the Request..
        this.interstitialAds.LoadAd(request);
        #endif

        #if UNITY_ANDROID
        //Initializing Process..,.
        this.interstitialAds = new InterstitialAd(androidInterestialID);

        //Creating an empty AdRequest..
        AdRequest request = new AdRequest.Builder().Build();

        //Load the interestial with the Request..
        this.interstitialAds.LoadAd(request);
        #endif
    }

    public void showingInterestialAds ()
    {
        if (this.interstitialAds.IsLoaded())
        {
            //Display Ads..
            this.interstitialAds.Show();
        }
        //Isn't ready then bro.. 
        else
        {
            Debug.Log("Isn't Ready yet");
        }
        //Interestial Ads Behaviour..
        this.interstitialAds.OnAdOpening += HandleOnAdOpened;
        this.interstitialAds.OnAdClosed += HandleOnAdClosed;
        

    }

    public void requestBanner()
    {
        #if UNITY_IOS
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);
        //Creating an empty Ad Request..
        AdRequest request = new AdRequest.Builder().Build();
        //Loading banner with the request..
        bannerView.LoadAd(request);
        #endif

        #if UNITY_ANDROID
        bannerView = new BannerView (androidBanner, AdSize.Banner, AdPosition.Bottom);
        //Creating an empty Ad Request..
        AdRequest request = new AdRequest.Builder().Build();

        //Loading Banner with the request..
        bannerView.LoadAd(request);
        #endif
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }

    public void HandleOnAdOpened (object sender, EventArgs args)
    {

    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {

    }

    public void HandleOnAdLeavingApplication (object sender, EventArgs args)
    {

    }

}



