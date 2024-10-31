using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Other {
	public sealed class TileController : MonoBehaviour {
		
		private Rigidbody _rigidBody;

		private void Start() {
			_rigidBody = GetComponent<Rigidbody>();
		}

		private void Update() {
			if (!PlayerController.Instance._isDeath)
				return;
			StopAllCoroutines();
		}

		private void OnTriggerEnter(Collider other) {
			if (!other.CompareTag("Player"))
				return;
			TileGeneration.Instance.Spawn();
			StartCoroutine(TilesMovement());
		}

		private IEnumerator TilesMovement() {
			yield return new WaitForSeconds(0.9f);
			StartCoroutine(DestroyingTiles());
		}

		private IEnumerator DestroyingTiles() {
			yield return new WaitForSeconds(2.1f);
			gameObject.SetActive(false);
		}
	}
}