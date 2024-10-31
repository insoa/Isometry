using System;
using UnityEngine;
using UnityEngine.UI;

namespace Items {
	[Serializable]
	public sealed class Item : MonoBehaviour {
		public int ItemId;
		public int Prise;
		public Button ItemCellButton;
		public GameObject Selector;
		public Animation ItemAnim;
		public EButtonType CurrentLocalState;
		public bool IsEquipped;
		public bool IsLocked;
		public bool IsPurchased;
		public EItemRareType ItemRareType;

		[SerializeField] private GameObject _lockBackground;
		[SerializeField] private GameObject _lockIcon;

		public void SetState(EButtonType type) {
			CurrentLocalState = type;
			switch (type) {
				case EButtonType.Lock:
					_lockBackground.SetActive(true);
					_lockIcon.SetActive(true);
					IsLocked = true;
					break;
				case EButtonType.Unlock:
					_lockBackground.SetActive(false);
					_lockIcon.SetActive(false);
					IsLocked = false;
					break;
				default:
					_lockBackground.SetActive(true);
					_lockIcon.SetActive(true);
					break;
			}
		}

		public void SetSelected(bool isSelected) {
			Selector.SetActive(isSelected);
			IsEquipped = isSelected;
		}
	}
}