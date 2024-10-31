using System;
using System.Collections;
using Saves;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads {
	public sealed class BannerAd : MonoBehaviour {
		public static BannerAd Instance;
		[SerializeField] private BannerPosition _bannerPosition;
		[SerializeField] private string _androidAdUnitId = "Banner_Android"; 
		[SerializeField] private string _iOSAdUnitId = "Banner_iOS";
		[SerializeField] private GameData _gameData;

		private string _adUnitId;

		private void Awake() {
			Instance = this;
			_adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
				? _iOSAdUnitId
				: _androidAdUnitId;
		}

		private void Start() {
			Advertisement.Banner.SetPosition(_bannerPosition);
			if (!_gameData.AdsDisabled) {
				StartCoroutine(StartBanner());	
			}
		}

		IEnumerator StartBanner() {
			yield return new WaitForSeconds(1f);
			LoadBanner();
		}

		public void LoadBanner() {
			BannerLoadOptions options = new BannerLoadOptions {
				loadCallback = OnBannerLoaded,
				errorCallback = OnBannerError
			};
			Advertisement.Banner.Load(_adUnitId, options);
		}

		public void ShowBannerAd() {
			BannerOptions options = new BannerOptions {
				clickCallback = OnBannerClicked,
				hideCallback = OnBannerHidden,
				showCallback = OnBannerShown
			};
			Advertisement.Banner.Show(_adUnitId, options);
		}
		
		private void OnBannerError(string message) {
			Debug.Log($"Banner error: {message}");
		}

		private void OnBannerLoaded() {
			ShowBannerAd();
		}

		private void OnBannerShown() {
			Debug.Log("BannerShowed");
		}

		private void OnBannerHidden() {
			Debug.Log("BannerIsHidden");
		}

		private void OnBannerClicked() {
			Debug.Log("BannerIsClicked");
		}

		public void HideBanner() {
			Advertisement.Banner.Hide();
		}
	}
}