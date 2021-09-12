using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads {
	public class AdsCore : MonoBehaviour, IUnityAdsListener {
		[SerializeField] private bool testMode = true;

		private string gameId = "4305469";

		private string video = "Interstitial_Android";
		private string banner = "Banner_Android";
		// private string rewardedVideo = "Rewarded_Android";

		private void Start(){
			Advertisement.AddListener(this);
			Advertisement.Initialize(gameId, testMode);

			#region Banner

			StartCoroutine(ShowBannerWhenInitialized());
			Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

			#endregion
		}

		public static void ShowAdsVideo(string placementId){
			if (Advertisement.IsReady())
				Advertisement.Show(placementId);
			else
				Debug.Log("Advertisement if not ready.");
		}

		private IEnumerator ShowBannerWhenInitialized(){
			while (!Advertisement.isInitialized) {
				yield return new WaitForSeconds(0.5f);
			}
			Advertisement.Banner.Show(banner);
		}

		public void OnUnityAdsReady(string placementId){ }
		public void OnUnityAdsDidError(string message){ }
		public void OnUnityAdsDidStart(string placementId){ }

		public void OnUnityAdsDidFinish(string placementId, ShowResult showResult){
			if (placementId != "Interstitial_Android") return;
			
			if (showResult == ShowResult.Finished) {
				TestAds.test.IncrementAfterInterstitialAds();
			} else if (showResult == ShowResult.Skipped) {
				Debug.Log("Skipped");
			}
			else if (showResult == ShowResult.Failed) {
				Debug.Log("Failed");
			}
		}
	}
}