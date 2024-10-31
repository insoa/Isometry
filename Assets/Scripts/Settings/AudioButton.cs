using System;
using UnityEngine;

namespace Settings {
	public sealed class AudioButton : MonoBehaviour {
		public GameObject EnabledIcon;
		public GameObject DisabledIcon;

		public void SetState(ESettingsButtonType type) {
			switch (type) {
				case ESettingsButtonType.Enabled:
					EnabledIcon.SetActive(true);
					DisabledIcon.SetActive(false);
					break;
				case ESettingsButtonType.Disabled:
					EnabledIcon.SetActive(false);
					DisabledIcon.SetActive(true);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}
	}
}