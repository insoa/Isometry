using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads {
	public sealed class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener {

		[SerializeField] private string _androidGameId;
		[SerializeField] private string _iOSGameId;
		[SerializeField] private bool _testMode;

		private string _gameId;

		private void Awake() {
			InitializeAds();
		}

		private void InitializeAds() {
			_gameId = (Application.platform == RuntimePlatform.IPhonePlayer) 
				? _iOSGameId 
				: _androidGameId;
			
			Advertisement.Initialize(_gameId, _testMode);
		}
		
		public void OnInitializationComplete() {
		  Debug.Log("Ads Initialization complete");
		}

		public void OnInitializationFailed(UnityAdsInitializationError error, string message) {
			Debug.Log($"Ads Initialization Failed: {error.ToString()} - {message}");
		}
	}
}