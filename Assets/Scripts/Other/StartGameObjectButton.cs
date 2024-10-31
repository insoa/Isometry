using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Other {
	public sealed class StartGameObjectButton : MonoBehaviour, IPointerDownHandler {
		[SerializeField] private Animation _animation;
		[SerializeField] private Animator _cameraAnimator;
		[SerializeField] private Animator _buttonPanelAnim;
		[SerializeField] private Animator _nameAnimator;
		[SerializeField] private AudioSource[] _audio;
		private bool _isInteraction;

		private void Start() {
			StartCoroutine(InteractionButton());
			_buttonPanelAnim.Play("OpenPanel");
			_nameAnimator.Play("GameNameAnimation");
		}
	
		public void OnPointerDown(PointerEventData eventData) {
			if (!_isInteraction)
				return;
			_animation.Play();
			StartCoroutine(OpenScene());
			_buttonPanelAnim.Play("ClosePanel");
			_nameAnimator.Play("GameNameClose");
		}

		private IEnumerator OpenScene() {
			foreach (var clip in _audio) {
				clip.Play();
			}
			_cameraAnimator.Play("CameraMoveBack");
			yield return new WaitForSeconds(1f);
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
			_buttonPanelAnim.Play("ClosePanel");
		}

		IEnumerator InteractionButton() {
			_isInteraction = false;
			yield return new WaitForSeconds(3.5f);
			_isInteraction = true;
		}
	}
}