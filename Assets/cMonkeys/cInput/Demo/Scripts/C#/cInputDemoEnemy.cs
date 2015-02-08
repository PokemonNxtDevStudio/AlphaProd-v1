using UnityEngine;
using System.Collections;

public class cInputDemoEnemy : MonoBehaviour {
	public GameObject bulletPrefab;
	public Transform playerTransform;

	float bulletTimer;

	private Transform _mesh;
	private Transform _turret;

	void Start() {
		_mesh = transform.FindChild("Mesh");
		_turret = transform.FindChild("Turret");
	}

	void Update() {
		if (!_mesh.renderer.isVisible) { // make sure the enemy can't shoot before its full on the screen
			bulletTimer = Time.time - 1;
		}

		transform.Translate(Vector3.forward * 5f * Time.deltaTime);
		if (playerTransform && _mesh.renderer.isVisible && Time.time > bulletTimer + 1.5f) {
			GameObject _bullet = (GameObject)Instantiate(bulletPrefab, _turret.position, Quaternion.identity);
			_bullet.transform.LookAt(playerTransform);
			_bullet.tag = "Enemy";
			bulletTimer = Time.time;
		}
	}
}
