using UnityEngine;

namespace Other {
	public sealed class FrameController : MonoBehaviour {
		private void Start() {
			Application.targetFrameRate = 60;
		}
	}
}