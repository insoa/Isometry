using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Other {
	public sealed class LevelInteractionController : MonoBehaviour {
		public static LevelInteractionController Instance;

		[SerializeField] private Camera _camera;
		[SerializeField] private Material _tileMaterial;
		[SerializeField] private List<Color> _tileColors = new List<Color>();
		[SerializeField] private List<Color> _cameraBackgroundColors = new List<Color>();

		[SerializeField] [Range(0, 2)] private float _lerpTime;
		private int _colorIndex;
		private int _colorCameraIndex;
		private float _t;
		private float _tCamera;
		private int _count;
		private int _cameraCount;

		private void Awake() {
			Instance = this;
		}

		private void Start() {
			_count = _tileColors.Count;
			_cameraCount = _cameraBackgroundColors.Count;
			_colorIndex = Random.Range(0, 9);
			_colorCameraIndex = Random.Range(0, 7);
		}

		public void StartChangingColor() {
			StartCoroutine(ChangeTileColor());
		}
	
		public void StartChangingBackgroundColor() {
			StartCoroutine(ChangeBackgroundColor());
		}

		private IEnumerator ChangeTileColor() {
			yield return new WaitForEndOfFrame();
			var previousIndex = _colorIndex;
			_tileMaterial.color = Color.Lerp(_tileMaterial.color, _tileColors[_colorIndex], _lerpTime * Time.deltaTime);
			_t = Mathf.Lerp(_t, 1f, _lerpTime * Time.deltaTime);
			if (_t > .9f) {
				_t = 0f;
				_colorIndex = Random.Range(0, 9);
				if (_colorIndex == previousIndex) {
					_colorIndex++;
				}
				if (_colorIndex >= _count)
					_colorIndex = 0;
				else
					_colorIndex = _colorIndex;
				StopCoroutine(ChangeTileColor());
			} else {
				StartCoroutine(ChangeTileColor());
			}
		}
	
		private IEnumerator ChangeBackgroundColor() {
			yield return new WaitForEndOfFrame();
			var previousIndex = _colorCameraIndex;
			_camera.backgroundColor = Color.Lerp(_camera.backgroundColor, _cameraBackgroundColors[_colorCameraIndex], _lerpTime * Time.deltaTime);
			_tCamera = Mathf.Lerp(_tCamera, 1f, _lerpTime * Time.deltaTime);
			if (_tCamera > .9f) {
				_tCamera = 0f;
				_colorCameraIndex = Random.Range(0, 7);
				if (_colorCameraIndex == previousIndex) {
					_colorCameraIndex++;
				}
				if (_colorCameraIndex >= _cameraCount)
					_colorCameraIndex = 0;
				else
					_colorCameraIndex = _colorCameraIndex;
				StopCoroutine( ChangeBackgroundColor());
			} else {
				StartCoroutine( ChangeBackgroundColor());
			}
		}
	}
}