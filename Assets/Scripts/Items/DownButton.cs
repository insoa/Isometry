using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Items {
	public sealed class DownButton : MonoBehaviour {
		public Button Button;
		public TextMeshProUGUI PriseText;
		public GameObject UnlockedObj;

		public void SetState(EDownButtonType type) {
			switch (type) {
				case EDownButtonType.Locked:
					UnlockedObj.SetActive(false);
					break;
				case EDownButtonType.Unlocked:
					UnlockedObj.SetActive(true);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}
	}
}