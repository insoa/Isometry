using System.Collections;
using UnityEngine;

namespace Other {
    public sealed class BonusController : MonoBehaviour {
        
        [SerializeField] private ParticleSystem _getEffect;
        private bool _isSelected;

        private void GetBonus() {
            PlayerController.Instance.SetCoinsResult();
            PlayerController.Instance.GetAnimationActivate();
            _getEffect.Play();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            _isSelected = true;
            StartCoroutine(DestroyBonus());
        }

        private void BonusRotation() {
            if (!_isSelected) {
                transform.Rotate(0, 4, 0);
            }
        }

        private void OnTriggerEnter(Collider collider) {
            if (collider.CompareTag("BackWall")) {
                StartCoroutine(DestroyBonus());
            }
            
            if (collider.CompareTag("Player")) {
                GetBonus();
            }
                
        }

        private void FixedUpdate() {
            BonusRotation();
        }

        private IEnumerator DestroyBonus() {
            yield return new WaitForSeconds(4f);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.SetActive(false);
            _isSelected = false;
        }
    }
}
