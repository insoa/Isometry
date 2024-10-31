using Saves;
using UnityEngine;
using UnityEngine.UI;

namespace Settings {
    public sealed class AudioSettings : MonoBehaviour {
        [SerializeField] private GameData _gameData;
        [SerializeField] private AudioButton _soundAudioButton;

        private void Start() {
            _soundAudioButton.GetComponent<Button>().onClick.AddListener(DisableSound);
            _gameData.Load();
            AudioListener.pause = !_gameData.AudioEnabled;
            _soundAudioButton.SetState(_gameData.AudioEnabled
                ? ESettingsButtonType.Enabled
                : ESettingsButtonType.Disabled);
        }

        private void DisableSound() {
            if (AudioListener.pause) {
                AudioListener.pause = false;
                _soundAudioButton.SetState(ESettingsButtonType.Enabled);
            } else {
                AudioListener.pause = true;
                _soundAudioButton.SetState(ESettingsButtonType.Disabled);
            }
            _gameData.AudioEnabled = !AudioListener.pause;
            _gameData.Save();
        }
    }
}
