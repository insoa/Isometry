using Saves;
using UnityEngine;

namespace Items {
	public sealed class ItemVisualize : MonoBehaviour {

		[SerializeField] private GameObject[] _itemSkins;
		[SerializeField] private GameData _gameData;

		public static ItemVisualize Instance;

		private void Awake() {
			Instance = this;
		}

		private void Start() {
			for (var i = 0; i < _itemSkins.Length; i++) {
				if (_gameData.SelectedItemId == i) {
					_itemSkins[i].SetActive(true);
				}
			}
		}

		public void ChangeItemPrefab() {

			foreach (var item in _itemSkins) {
				item.SetActive(false);
			}
			
			for (var i = 0; i < _itemSkins.Length; i++) {
				if (_gameData.SelectedItemId == i) {
					_itemSkins[i].SetActive(true);
				}
			}
		}
	}
}