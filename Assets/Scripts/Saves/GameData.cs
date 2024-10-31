using System.Collections.Generic;
using UnityEngine;

namespace Saves {
	[CreateAssetMenu(fileName = "GameData", menuName = "Databases", order = 4)]
	public sealed class GameData : ScriptableObject {
		public int SelectedItemId;
		public List<int> UnlockedItemsId = new List<int>();

		public int RecordScore;
		public int GlobalMoney;
		public bool AudioEnabled;
		public bool AdsDisabled;

		public void Save() {
			SaveSystem.SaveData(this);
		}

		public void Load() {
			var data = SaveSystem.LoadData();
			SelectedItemId = data.SelectedItemId;
			UnlockedItemsId = data.UnlockedItemsId;
			RecordScore = data.RecordScore;
			GlobalMoney = data.GlobalMoney;
			AudioEnabled = data.AudioEnabled;
			AdsDisabled = data.AdsDisabled;
		}

		public void ClearSave() {
			SelectedItemId = 0;
			GlobalMoney = 20;
			RecordScore = 0;
			UnlockedItemsId.Clear();
			UnlockedItemsId.Add(0);
			AudioEnabled = true;
			AdsDisabled = false;
			Save();
		}
	}
}