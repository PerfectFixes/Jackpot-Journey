using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IronSourceScript : MonoBehaviour
{
    private readonly string appKey = "193ffe21d";
    private bool autoClicker;
    private bool isTutorial;
    private static IronSourceScript instance;

    private Randomizer randomizer;
    private IncreaseLuckTimer increaseLuckTimer;
    private GameObject bannerBackup;

    private bool isFirstTime;
    private Animator autoClickerAnimator;
    private Animator increaseLuckAnimator;

    private TMP_Text errorMessageText;
    private Animator errorMessageAnimator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        errorMessageAnimator = GameObject.Find("Error_Message_Display").GetComponent<Animator>();
        errorMessageText = GameObject.Find("Error_Message_Display").GetComponent<TMP_Text>();
        randomizer = GameObject.Find("Randomize_Number").GetComponent<Randomizer>();
        bannerBackup = GameObject.Find("AD_BANNER_PANEL");
        increaseLuckTimer = GameObject.Find("Increase_Luck_Timer").GetComponent<IncreaseLuckTimer>();
        autoClickerAnimator = GameObject.Find("Auto_Clicker").GetComponent<Animator>();
        increaseLuckAnimator = GameObject.Find("Increase_Luck").GetComponent<Animator>();
        
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            isTutorial = false;
            errorMessageAnimator = GameObject.Find("Error_Message_Display").GetComponent<Animator>();
            errorMessageText = GameObject.Find("Error_Message_Display").GetComponent<TMP_Text>();
            randomizer = GameObject.Find("Randomize_Number").GetComponent<Randomizer>();
            bannerBackup = GameObject.Find("AD_BANNER_PANEL");
            increaseLuckTimer = GameObject.Find("Increase_Luck_Timer").GetComponent<IncreaseLuckTimer>();
            autoClickerAnimator = GameObject.Find("Auto_Clicker").GetComponent<Animator>();
            increaseLuckAnimator = GameObject.Find("Increase_Luck").GetComponent<Animator>();
            if (isFirstTime)
            {
                StartCoroutine(EnableAds(true));
            }
            else
            {

                StartCoroutine(EnableAds(false));
            }     
            
        }
        else
        {
            isTutorial = true;
            StopAllCoroutines();
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        isFirstTime = true;
        autoClicker = false;
        IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO);
        IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO);
        IronSource.Agent.init(appKey, IronSourceAdUnits.BANNER);
        if (!isTutorial)
        {
            StartCoroutine(EnableAds(true));
        }
    }
    public IEnumerator RestartAdTimer()
    {
        yield return new WaitForSeconds(0.5f);
        print("RRRRest");
        isFirstTime = true;
        StopAllCoroutines();
    }
    private IEnumerator EnableAds(bool isLongCooldown)
    {
        yield return null;
        if (isLongCooldown)
        {
            autoClickerAnimator.SetBool("isAdReady", false);
            increaseLuckAnimator.SetBool("isAdReady", false);
            yield return new WaitForSeconds(120);//Set time to 240

            isFirstTime = false;
            autoClickerAnimator.SetBool("isAdReady", true);
            increaseLuckAnimator.SetBool("isAdReady", true);
            yield return new WaitForSeconds(30);//Set time to 60

            StartCoroutine(EnableAds(false));
        }      
        else
        {

            autoClickerAnimator.SetBool("isAdReady", false);
            increaseLuckAnimator.SetBool("isAdReady", false);
            yield return new WaitForSeconds(60);//Set time to 180

            autoClickerAnimator.SetBool("isAdReady", true);
            increaseLuckAnimator.SetBool("isAdReady", true);
            yield return new WaitForSeconds(30);//Set time to 60

            StartCoroutine(EnableAds(false));
        }

    }
    private void OnEnable()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;

        //Banner related
        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;

        //Add AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;

    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
    public void LoadBanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    }
    public void DestroyBanner()
    {
        IronSource.Agent.destroyBanner();
    }
    private void SdkInitializationCompletedEvent() 
    { 
        IronSource.Agent.validateIntegration();
        LoadBanner();
    }
    public void ShowRewardedAd(string rewardName)
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            StopAllCoroutines();
            StartCoroutine(EnableAds(false));
            if (rewardName == "Auto Clicker")
            {
                autoClicker = true;
            }
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            errorMessageAnimator.Play("Error_Message_Display");
            errorMessageText.text = "No ads currently available";
        }
    }
    /***************** Banner Callbacks *****************/

    //Invoked once the banner has loaded
    void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo)
    {
        print("Banner Loaded");     
    }
    //Invoked when the banner loading process has failed.
    void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError)
    {
        bannerBackup.SetActive(true);
        print("Failed to load the banner");
        StartCoroutine(ReloadBanner());
    }
    // Invoked when end user clicks on the banner ad
    void BannerOnAdClickedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Notifies the presentation of a full screen content following user click
    void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Notifies the presented screen has been dismissed
    void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Invoked when the user leaves the app
    void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo)
    {
    }
    IEnumerator ReloadBanner()
    {
        LoadBanner();
        yield return new WaitForSeconds(0.5f);
        print("reloading");
    }
    /***************** Banner Callbacks *****************/

    /***************** Rewarded Callbacks *****************/

    // Indicates that there’s an available ad.
    // The adInfo object includes information about the ad that was loaded successfully
    // This replaces the RewardedVideoAvailabilityChangedEvent(true) event
    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
    }
    // Indicates that no ads are available to be displayed
    // This replaces the RewardedVideoAvailabilityChangedEvent(false) event
    void RewardedVideoOnAdUnavailable()
    {
    }
    // The Rewarded Video ad view has opened. Your activity will loose focus.
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
    }
    // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
    }
    // The user completed to watch the video, and should be rewarded.
    // The placement parameter will include the reward data.
    // When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        
        if (autoClicker)
        {
            StartCoroutine(randomizer.AutoClicker());
            print("Gave the player autoClicker");
        }
        else 
        {
            StartCoroutine(increaseLuckTimer.LuckTimer());
            print("Gave the player DoubleWin");
        }
        autoClicker = false; 
    }
    // The rewarded video ad was failed to show.
    void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
    {
        autoClicker = false;
    }
    // Invoked when the video ad was clicked.
    // This callback is not supported by all networks, and we recommend using it only if
    // it’s supported by all networks you included in your build.
    void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
    }

    /***************** Rewarded Callbacks *****************/
}
