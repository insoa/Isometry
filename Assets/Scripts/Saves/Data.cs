using System;
using System.Collections.Generic;

namespace Saves {
	[Serializable]
	public class Data {
		public int SelectedItemId;
		public List<int> UnlockedItemsId = new List<int>();

		public int RecordScore;
		public int GlobalMoney;
		public bool AudioEnabled;
		public bool AdsDisabled;

		public Data(GameData data) {
			SelectedItemId = data.SelectedItemId;
			UnlockedItemsId = data.UnlockedItemsId;
			RecordScore = data.RecordScore;
			GlobalMoney = data.GlobalMoney;
			AudioEnabled = data.AudioEnabled;
			AdsDisabled = data.AdsDisabled;
		}
	}
}