#pragma strict


	var rocketPrefab:  GameObject ;
	var shootPoint :Transform;
	var target :Transform;
	private var damping : float = 20;
	private var shootDelay: float  = 3;
	private var shootTime :float ;

	function Start() {
		shootDelay = Random.Range(3, 7);
		shootTime = shootDelay;
	}

	function Update() {
		if (target && transform.FindChild("Box10").renderer.isVisible) {
			var rotate : Quaternion = Quaternion.LookRotation(target.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * damping);
			transform.eulerAngles = Vector3(0, transform.eulerAngles.y, 0);
			if (Time.time > shootTime) {
				Shoot();
				shootTime = Time.time + shootDelay;
			}
		}
	}

	function Shoot() {
		var _bullet :GameObject  = Instantiate(rocketPrefab, shootPoint.position, Quaternion.identity);
		_bullet.transform.LookAt(target);
		_bullet.tag = "Enemy";
	}

