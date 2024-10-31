using System.Collections;
using UnityEngine;

namespace Other {
    public sealed class StartPlatformMovement : MonoBehaviour {
        private void OnTriggerExit(Collider other) {
            if (!other.CompareTag("Player"))
                return;
            StartCoroutine(DestroyObject());
        }

        private IEnumerator DestroyObject() {
            yield return new WaitForSeconds(4);
            Destroy(gameObject);
        }
    }
}
