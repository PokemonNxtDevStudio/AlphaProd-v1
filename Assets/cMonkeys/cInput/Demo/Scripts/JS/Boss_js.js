#pragma strict

var bulletPrefab : GameObject;
var renderedMesh : Renderer ;
var playerTransform : Transform;
private var bulletTimer : float;

	function Start() {
		if (renderer) {
			renderedMesh = renderer;
		}
	}

function Update () {
if (!renderedMesh) { Destroy(gameObject); return; }

	transform.Translate(Vector3.forward * 5f * Time.deltaTime);
	if (playerTransform &&  renderedMesh.isVisible && Time.time > bulletTimer + 1.5f) {
		// bullet 1
		var _bullet : GameObject  = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
		_bullet.transform.LookAt(playerTransform);
		_bullet.tag = "Enemy";
		// bullet 2
		_bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
		var offset : Vector3 = new Vector3(playerTransform.position.x - 10, playerTransform.position.y, playerTransform.position.z);
		_bullet.transform.LookAt(offset);
		_bullet.tag = "Enemy";
		// bullet 3
		_bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
		offset = new Vector3(playerTransform.position.x + 10, playerTransform.position.y, playerTransform.position.z);
		_bullet.transform.LookAt(offset);
		_bullet.tag = "Enemy";
		// update bulletTimer
		bulletTimer = Time.time;
	}
}
