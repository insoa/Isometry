using System;
using UnityEngine;

namespace Saves {
	public sealed class SaveManager : MonoBehaviour {
		[SerializeField] private GameData _gameData;

		private void Start() {
			Debug.Log("DATA IS LOADED");
			_gameData.Load();
		}
	}
}