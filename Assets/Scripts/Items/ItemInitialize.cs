using Saves;
using UnityEngine;

namespace Items {
	public sealed class ItemInitialize : MonoBehaviour {

		[SerializeField] private GameObject[] _itemSkins;
		[SerializeField] private GameData _gameData;
		
		private void Start() {
			for (var i = 0; i < _itemSkins.Length; i++) {
				if (_gameData.SelectedItemId == i)
					_itemSkins[i].SetActive(true);
			}
		}
	}
}