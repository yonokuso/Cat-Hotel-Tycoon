using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;


public class RewardedAdsButton : MonoBehaviour
{
    private RewardedAd rewardedAd;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // �ʱ�ȭ.
        });
    }

    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
  private string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
  private string adUnitId = "unexpected_platform";
#endif


    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    public void LoadRewardedAd() // ���� �ε��ϱ�
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");


        // create our request used to load the ad.
        var adRequest = new AdRequest(); //

        // send the request to load the ad.
        RewardedAd.Load(adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError(error + "�� ������ ���� �ε忡 �����߽��ϴ�.");
              
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
            });
    }


    public void ShowAd() //���� ����
    {
        const string rewardMsg =
            "�����带 �ްڽ��ϱ�? Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                //���� ȹ���ϱ�
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
        else
        {
            LoadRewardedAd();
        }
    }

    private void RegisterReloadHandler(RewardedAd ad) //���� ��ε�
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += (null);
        {
            Debug.Log("Rewarded Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }


}