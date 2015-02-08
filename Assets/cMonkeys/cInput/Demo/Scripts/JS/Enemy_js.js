#pragma strict

var bulletPrefab : GameObject;
var  playerTransform : Transform;
private var bulletTimer :float;

	private var _mesh : Transform ;
	private var _turret :Transform ;

	function Start() {
		_mesh = transform.FindChild("Mesh");
		_turret = transform.FindChild("Turret");
	}
	
function Update () {
		if (!_mesh.renderer.isVisible) { // make sure the enemy can't shoot before its full on the screen
			bulletTimer = Time.time - 1;
		}

	transform.Translate(Vector3.forward * 5f * Time.deltaTime);
	if (playerTransform && _mesh.renderer.isVisible && Time.time > bulletTimer + 1.5f) {
		var _bullet : GameObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
		_bullet.transform.LookAt(playerTransform);
		_bullet.tag = "Enemy";
		bulletTimer = Time.time;
	}
}
