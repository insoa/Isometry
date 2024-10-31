using System.Collections;
using Saves;
using UnityEngine;

namespace Settings {
	public sealed class AudioGamePlayManager : MonoBehaviour {
		[SerializeField] private GameData _gameData;
		[SerializeField] private AudioListener _audioListener;
		[SerializeField] private AudioSource _startClip;
		
		private void Start() {
			_gameData.Load();
			AudioListener.pause = !_gameData.AudioEnabled;
			StartCoroutine(StartPlay());
		}

		IEnumerator StartPlay() {
			yield return new WaitForSeconds(0.6f);
			_startClip.Play();
		}
	}
}