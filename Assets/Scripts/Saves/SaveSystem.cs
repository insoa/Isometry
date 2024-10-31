using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Saves {
	public static class SaveSystem {
		
		public static void SaveData(GameData gameData) {
			BinaryFormatter formatter = new BinaryFormatter();
			var path = Application.persistentDataPath + "/GameData.save";
			FileStream stream = new FileStream(path, FileMode.Create);
			
			Data data = new Data(gameData);
			
			formatter.Serialize(stream, data);
			stream.Close();
		}


		public static Data LoadData() {
			var path = Application.persistentDataPath + "/GameData.save";
			if (File.Exists(path)) {
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream stream = new FileStream(path, FileMode.Open);
				Data data = formatter.Deserialize(stream) as Data;
				stream.Close();

				
				return data;
			} else {
				Debug.Log("Not Found");
				return null;
			}
		}
		
	}
}