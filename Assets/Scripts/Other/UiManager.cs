using System.Collections;
using Saves;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Other {
	public sealed class UiManager : MonoBehaviour {
		public GameData GameData;
		[SerializeField] private TextMeshProUGUI _gameScoreText; 
		[SerializeField] private TextMeshProUGUI _losePanelScoreText;
		[SerializeField] private TextMeshProUGUI _losePanelRecordScoreText;
		[SerializeField] private TextMeshProUGUI _losePanelMoneyText;
		[SerializeField] private GameObject _losePanel;
		[SerializeField] private Button _homeButton;
		[SerializeField] private Button _restartButton;
		[SerializeField] private Button _checkAdsButton;
		[SerializeField] private AudioSource[] _audioSources;
		[SerializeField] private AudioSource _upCoinsShotClip;
		[SerializeField] private AudioSource _moneyClip;
		[SerializeField] private AudioSource _recordScoreClip;
		[SerializeField] private AudioSource _changeLevelClip;
		[SerializeField] private Animation _upMoneyAnimation;
		[SerializeField] private Animation _recordScoreAnimation;
		private int _currentScore;
		private float _timeToUp;
		private int _previousRecord;
		private int _interval;
		public int Money;
		public int AdReward;

		private static UiManager _instance;

		public static UiManager Instance {
			get {
				if (_instance == null)
					_instance = FindObjectOfType<UiManager>(); //:(
				return _instance;
			}
		}

		private void Awake() {
			_losePanelRecordScoreText.text = GameData.RecordScore.ToString();
		}

		private void Start() {
			_homeButton.onClick.AddListener(Load);
			_restartButton.onClick.AddListener(Reload);
			_checkAdsButton.onClick.AddListener(CheckAds);
			_previousRecord = GameData.RecordScore;
			StopCoroutine(AddBonusVisualize());
		}

		public void UpdateScoreValue(int value) {
			_gameScoreText.text = value.ToString();
			_currentScore = value;
			if (_currentScore == 1) {
				_interval = 20;
			}

			if (_currentScore != _interval)
				return;
			LevelInteractionController.Instance.StartChangingColor();
			LevelInteractionController.Instance.StartChangingBackgroundColor();
			_changeLevelClip.Play();
			_interval += 20;
		}

		public void OnShowLosePanel() {
			_gameScoreText.gameObject.SetActive(false);
			_losePanel.SetActive(true);
			var record = GameData.RecordScore;
			_losePanelScoreText.text = _currentScore.ToString();//
			_losePanelRecordScoreText.text = GameData.RecordScore.ToString();
			if (_currentScore > record) {
				GameData.RecordScore = _currentScore;//
				GameData.Save();
				GameData.Load();
			} else {
				_losePanelRecordScoreText.text = record.ToString();
			}

			StartCoroutine(CoinsUp());
			StartCoroutine(NewRecord());
		}

		private IEnumerator CoinsUp() {
			yield return new WaitForSeconds(2f);
			StartCoroutine(CoinsEffect());
		}

		private IEnumerator CoinsEffect() {
			var money = 0;
			for (var i = 0; i != Money; i++) {
				yield return new WaitForSeconds(_timeToUp);
				money++;
				_upCoinsShotClip.Play();
				switch (i) {
					case 10:
						_upCoinsShotClip.pitch = 1.03f;
						_timeToUp = 0.10f;
						break;
					case 15:
						_upCoinsShotClip.pitch = 1.05f;
						_timeToUp = 0.05f;
						break;
					case 30:
						_upCoinsShotClip.pitch = 1.07f;
						_timeToUp = 0.03f;
						break;
					default:
						_timeToUp = _timeToUp;
						break;
				}
				_losePanelMoneyText.text = $"+{money.ToString()}";
			}
			_upMoneyAnimation.Play();
			_moneyClip.Play();
		}

		private IEnumerator NewRecord() {
			var timeToUpdate = 0.05f;
			var score = 0;
			for (var i = 0; i != _currentScore; i++) {
				yield return new WaitForSeconds(timeToUpdate);
				score++;
				if (score == 20) {
					timeToUpdate = 0.02f;
				}
				if (score == 50) {
					timeToUpdate = 0.01f;
				}
				_losePanelScoreText.text = score.ToString();
			}

			if (score <= _previousRecord) 
				yield break;
			_recordScoreClip.Play();
			_recordScoreAnimation.Play();
			_losePanelRecordScoreText.text = _currentScore.ToString();
		}

		public void AddBonus() {
			StartCoroutine(AddBonusVisualize());
		}

		private IEnumerator AddBonusVisualize() {
			GameData.GlobalMoney += AdReward;
			GameData.Save();
			var allMoney = Money + AdReward;
			var uiMoney = 0;
			for (var i = 0; i != allMoney; i++) {
				yield return new WaitForSeconds(_timeToUp);
				_upCoinsShotClip.Play();
				uiMoney++;
				switch (i) {
					case 10:
						_upCoinsShotClip.pitch = 1.03f;
						_timeToUp = 0.02f;
						break;
					case 15:
						_upCoinsShotClip.pitch = 1.05f;
						_timeToUp = 0.01f;
						break;
					default:
						_timeToUp = _timeToUp;
						break;
				}
				_losePanelMoneyText.text = $"+{uiMoney.ToString()}";
			}
			_upMoneyAnimation.Play();
			_moneyClip.Play();
		}

		private void Load() {
			StartCoroutine(LoadMainMenu());
		}

		private void Reload() {
			StartCoroutine(ReloadLevel());
		}

		private IEnumerator ReloadLevel() {
			_audioSources[0].Play();
			yield return new WaitForSeconds(0.5f);
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		}
	
		private IEnumerator LoadMainMenu() {
			_audioSources[0].Play();
			yield return new WaitForSeconds(0.5f);
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}

		private void CheckAds() {
			_audioSources[0].Play();
		}
	}
}