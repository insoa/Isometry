using UnityEngine;

namespace Other {
	public sealed class PlatformInteractionMovement : MonoBehaviour {
	
		[SerializeField] private Transform _target;
		[SerializeField] private Vector3 _offset;
		[SerializeField] private float _dumping;

		private Vector3 _velocity = Vector3.zero;

		private void FixedUpdate() {
			var movePosition = _target.position + _offset;
			transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref _velocity, _dumping);
		}
	}
}