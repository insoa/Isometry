using System;
using System.Collections;
using Ads;
using Saves;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Other {
	public sealed class PlayerController : MonoBehaviour {
		[SerializeField] private int _speed;
		[SerializeField] private GameData _gameData;
		[SerializeField] private AudioSource _deathSound;
		[SerializeField] private AudioSource _bonus;
		[SerializeField] private AudioSource _tapSound;
		[SerializeField] private ParticleSystem _deathEffect;
		[SerializeField] private GameObject[] _playerObjects;
		[SerializeField] private AudioSource _playerDestroy;
		[SerializeField] private GameObject _wall;
		private Vector3 _direction;
		private int _score;
		private int _coins;
		public bool _isDeath;
		public GameObject PickUpScoreAnimation;
		private int _deathСount;
		private int _previousRange;

		public static PlayerController Instance;

		private void Awake() {
			Instance = this;
		}

		private void Start() {
			_deathEffect.gameObject.SetActive(false);
			_isDeath = false;
			_direction = Vector3.zero;
			_gameData.Load();
		}

		IEnumerator ShowAd() {
			yield return new WaitForSeconds(0.5f);
			var range = Random.Range(0, 5);
			if (range == 0 && !_gameData.AdsDisabled) {
				InterstitialAd.Instance.LoadAd();
			}
		}

		private void Update() {
			_wall.transform.position = gameObject.transform.position;
			
#if UNITY_EDITOR
			if (_isDeath)
				return;

			if (Input.GetKeyDown(KeyCode.Space)) {
				OnClick();
			}

#elif UNITY_ANDROID
    if (_isDeath)
			return;

		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) {
				OnClick();
			}
		}
#endif

			var amountToMove = _speed * Time.deltaTime;
			transform.Translate(_direction * amountToMove);
		}

		private void OnClick() {
			_score++;

			_tapSound.Play();
			_direction = _direction == Vector3.forward ? Vector3.left : Vector3.forward;

			switch (_score) {
				case 100:
					_speed += 1;
					break;
				case 200:
					_speed += 1;
					break;
				case 300:
					_speed += 1;
					break;
			}

			UiManager.Instance.UpdateScoreValue(_score);
		}

		private void PlayerDeath() {
			_isDeath = true;
			StartCoroutine(DestroyPlayerObject());
			UiManager.Instance.OnShowLosePanel();
			_deathSound.Play();
			StartCoroutine(ShowAd());
		}

		private void OnTriggerEnter(Collider collider) {
			if (collider.CompareTag("DeathZone")) {
				_deathСount++;
				_deathEffect.gameObject.SetActive(true);
				if (_deathСount == 1) {
					PlayerDeath();
				}
			}

			if (collider.CompareTag("Explosive")) {
				collider.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
			}
		}

		public void GetAnimationActivate() {
			StartCoroutine(ScoreAnimationActivation());
		}

		private IEnumerator ScoreAnimationActivation() {
			PickUpScoreAnimation.SetActive(true);
			yield return new WaitForSeconds(0.5f);
			PickUpScoreAnimation.SetActive(false);
		}

		IEnumerator DestroyPlayerObject() {
			yield return new WaitForSeconds(0f);
			CameraMovement.Instance.NoTarget();
			_deathEffect.Play();
			_deathEffect.transform.SetParent(null);
			foreach (var child in _playerObjects) {
				child.SetActive(false);
			}

			_playerDestroy.Play();
		}

		public void SetCoinsResult() {
			_gameData.GlobalMoney += 1;
			_coins += 1;
			UiManager.Instance.Money = _coins;
			_gameData.Save();
			_bonus.PlayOneShot(_bonus.clip);
		}
	}
}