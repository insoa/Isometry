using System.Collections;
using System.Net.Http.Headers;
using ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Other {
	public sealed class TileGeneration : MonoBehaviour {
		[SerializeField] private GameObject _tilePrefab;
		[SerializeField] private GameObject _newTile;
		[SerializeField] private GameObject _pickUpBonus;
		public static TileGeneration Instance;

		private void Awake() => Instance = this;

		private void Start() {
			for (var i = 0; i < 50; i++) 
				Spawn();
		}

		public void Spawn() {
			var randomSide = Random.Range(0, 2);
			var randomBonus = Random.Range(0, 9);
			_newTile = ObjectPooler.Instance.SpawnFromPool(
				"Tile",
				_newTile.transform.GetChild(randomSide).transform.position,
				Quaternion.identity
			);
			if (randomBonus == 0) {
				  ObjectPooler.Instance.SpawnFromPool(
					"Bonus",
					new Vector3(_newTile.transform.position.x, 2.5f, _newTile.transform.position.z),
					Quaternion.identity
				);
			}
		}
	}
}