using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads {
	public sealed class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener {
		public static InterstitialAd Instance;

		[SerializeField] private string _androidAdUnitId = "Interstitial_Android";
		[SerializeField] private string _iOSAdUnitId = "Interstitial_iOS";

		private string _adUnitId;

		private void Awake() {
			Instance = this;
			_adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
				? _iOSAdUnitId
				: _androidAdUnitId;
		}

		public void LoadAd() {
			Advertisement.Load(_adUnitId, this);
		} 
		private void ShowAd() {
			Debug.Log("Showing Ad:" + _adUnitId);
			Advertisement.Show(_adUnitId, this);
		}

		public void OnUnityAdsAdLoaded(string placementId) {
			ShowAd();
			Debug.Log(placementId + "- Loaded" + "+" + _adUnitId);
		}

		public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) {
			Debug.Log($"Error Loading Ad Unit: {_adUnitId} - {placementId} - {error.ToString()} - {message}");
		}

		public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) {
			Debug.Log($"Error Show Ad Unit: {placementId} - {error.ToString()} - {message}");
		}

		public void OnUnityAdsShowStart(string placementId) {
		}

		public void OnUnityAdsShowClick(string placementId) {
			Debug.Log($"Clicked To Ads: {placementId}");
		}

		public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) {
			Debug.Log($"Ads Showed to User: {placementId} - {showCompletionState}");
			//
		}
	}
}