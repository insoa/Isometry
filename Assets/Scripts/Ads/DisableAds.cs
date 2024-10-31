using Items;
using Saves;
using UnityEngine;
using UnityEngine.UI;

namespace Ads {
	public sealed class DisableAds : MonoBehaviour {
		[SerializeField] private GameData _gameData;
		[SerializeField] private GameObject _buy;
		[SerializeField] private GameObject _owned;
		[SerializeField] private Button _buyButton;

		private void Start() {
			if (_gameData.AdsDisabled) {
				OfferButtonDisable();
			}
		}

		public void DisableAd() {
			OfferButtonDisable();
			_gameData.AdsDisabled = true;
			_gameData.GlobalMoney += 1000;
			ItemsController.Instance.GlobalCoinsValue += 1000;
			ItemsController.Instance.AddBonus();
			_gameData.Save();
			Debug.Log("Ads Disabled");
		}

		private void OfferButtonDisable() {
			_owned.SetActive(true);
			_buy.SetActive(false);
			_buyButton.interactable = false;
		}
	}
}
