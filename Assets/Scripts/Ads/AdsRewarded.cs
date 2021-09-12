using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Ads {
	[RequireComponent(typeof(Button))]
	public class AdsRewarded : MonoBehaviour, IUnityAdsListener {
		[SerializeField] private bool testMode = true;

		private string gameId = "4305469";

		private static string rewardedVideo = "Rewarded_Android";
		
		[SerializeField] private Button adsButton;

		private void Start(){
			if (adsButton == null) adsButton = GetComponent<Button>();
			adsButton.interactable = Advertisement.IsReady(rewardedVideo);
			
			adsButton.onClick.AddListener(ShowRewardedVideo);
			
			Advertisement.AddListener(this);
			Advertisement.Initialize(gameId);
		}

		public static void ShowRewardedVideo(){
			Advertisement.Show(rewardedVideo);
		}

		public void OnUnityAdsReady(string placementId){
			if (placementId == rewardedVideo) {
				adsButton.interactable = true;
			}
		}

		public void OnUnityAdsDidError(string message){
		}

		public void OnUnityAdsDidStart(string placementId){
		}

		public void OnUnityAdsDidFinish(string placementId, ShowResult showResult){
			if (placementId != "Rewarded_Android") return;
			
			if (showResult == ShowResult.Finished) {
				TestAds.test.IncrementAfterRewardedAds();
			} else if (showResult == ShowResult.Skipped) {
				Debug.Log("Skipped");
			}
			else if (showResult == ShowResult.Failed) {
				Debug.Log("Failed");
			}
		}
	}
}