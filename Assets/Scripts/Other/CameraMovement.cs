using System;
using UnityEngine;

namespace Other {
	public sealed class CameraMovement : MonoBehaviour {
	
		[SerializeField] private Transform _target;
		[SerializeField] private Vector3 _offset;
		[SerializeField] private float _dumping;
	
		public static CameraMovement Instance;

		private void Awake() {
			Instance = this;
		}

		private Vector3 _velocity = Vector3.zero;

		private void Update() {
			var movePosition = _target.position + _offset;
			transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref _velocity, _dumping);
		}

		public void NoTarget() {
			var cameraMovement = GetComponent<CameraMovement>();
			cameraMovement.enabled = false;
		}
	}
}