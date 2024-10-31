using System;
using System.Collections;
using Other;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Ads {
	public sealed class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener {

		public static RewardedAds Instance;
		
		[SerializeField] private string _androidAdUnitId = "Rewarded_Android"; 
		[SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";
		[SerializeField] private Button _rewardAdsButton;
		private bool _isCompleted;

		private string _adUnitId;
		private int _showCount = 0;

		private void Awake() {
			Instance = this;
			_adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
				? _iOSAdUnitId
				: _androidAdUnitId;
			_rewardAdsButton.interactable = false;
		}

		private void Start() {
			StartCoroutine(LoadRewardedAd());
		}

		IEnumerator LoadRewardedAd() {
			yield return new WaitForSeconds(1f);
			LoadAd();
			_rewardAdsButton.onClick.AddListener(ShowAd);
		}

		private void LoadAd() {
			Advertisement.Load(_adUnitId, this);
		}
		
		public void ShowAd() {
			Debug.Log("Showing Ad:" + _adUnitId);
			_rewardAdsButton.interactable = false;
			Advertisement.Show(_adUnitId, this);
		}

		public void OnUnityAdsAdLoaded(string placementId) {
			Debug.Log("Loaded!!!" + placementId);
			if (!_isCompleted) {
				_rewardAdsButton.interactable = true;	
			}
		}

		public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) {
			_rewardAdsButton.interactable = false;
		}

		public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) {
			_rewardAdsButton.interactable = false;
		}

		public void OnUnityAdsShowStart(string placementId) {
			Debug.Log(placementId);
		}

		public void OnUnityAdsShowClick(string placementId) {
			//Debug.Log($"Clicked To Ads: {placementId}");
		}

		public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) {
			_showCount++;
			if (_showCount == 1) {
				if (placementId.Equals(_adUnitId) && showCompletionState == UnityAdsShowCompletionState.COMPLETED) {
					UiManager.Instance.AddBonus();
					//Advertisement.Load(_adUnitId, this);
					_isCompleted = true;
					_rewardAdsButton.onClick.RemoveListener(ShowAd);
					Debug.Log($"Ads Showed to User: {placementId} - {showCompletionState}");
				} else 
				if( placementId.Equals(_adUnitId) && showCompletionState == UnityAdsShowCompletionState.UNKNOWN) {
					_rewardAdsButton.interactable = false;
				} else 
				if (placementId.Equals(_adUnitId) && showCompletionState == UnityAdsShowCompletionState.SKIPPED) {
					_rewardAdsButton.interactable = false;
				}
			}
		}
	}
}