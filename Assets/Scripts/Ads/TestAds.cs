using TMPro;
using UnityEngine;

namespace Ads {
	public class TestAds : MonoBehaviour {
		public static TestAds test;

		[SerializeField] private TextMeshProUGUI textRewarded;
		[SerializeField] private TextMeshProUGUI textInterstitial;
		private int countRewarded = 0;
		private int countInterstitial = 0;

		private void Awake(){
			test = this;
			countRewarded = 0;
			countInterstitial = 0;
			textRewarded.text = countRewarded.ToString();
			textInterstitial.text = countInterstitial.ToString();
		}

		public void RunInterstitialAds(){
			AdsCore.ShowAdsVideo("Interstitial_Android");
		}

		public void RunRewardedAds(){
			AdsRewarded.ShowRewardedVideo();
		}

		public void IncrementAfterRewardedAds(){
			countRewarded++;
			textRewarded.text = countRewarded.ToString();
		}
		public void IncrementAfterInterstitialAds(){
			countInterstitial++;
			textInterstitial.text = countInterstitial.ToString();
		}
	}
}