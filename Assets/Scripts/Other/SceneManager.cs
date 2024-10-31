using UnityEngine;

namespace Other {
	public sealed class SceneManager : MonoBehaviour {
		public void Restart() {
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}
}