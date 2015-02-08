#pragma strict

var guiSpeed : GUIText;
var pauseText : GUIText;
var resetText : GUIText;
var bulletPrefab : GameObject;

private var speed : float;
private var steer : float;
private var shootTimer : float;

private var pause : boolean;
private var menu : boolean;


function Update () {

		// notice we use Unity's Input class here (instead of cInput) for calling the input config menus because we dont wan't the player to change this input
		// call our custom made GUI Menu
		if (Input.GetKeyDown(KeyCode.Escape) && !cInput.scanning) {
			cGUI.ToggleGUI();
			pause = cGUI.showingAnyGUI;
		}
	
	// only check for inputs when the input menu is not up
	if (!cGUI.showingAnyGUI) {
		// toggle pause on/off - notice we use GetKeyDown for this because we don't want repeated input on this
		if (cInput.GetKeyDown("Pause")) {
			pause = !pause;
		}

		if (!pause) {
			// shoot a bullet every .25 seconds as long the 'Shoot' key is held down
			if (cInput.GetKey("Shoot") && Time.time > shootTimer + 0.25f) {
					var _leftShoot : Vector3 = Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
					var _rightShoot : Vector3 = Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
					var _bulletL : GameObject = Instantiate(bulletPrefab, _leftShoot, Quaternion.identity)as GameObject;
					_bulletL.tag = "Player";
					var _bulletR : GameObject  = Instantiate(bulletPrefab, _rightShoot, Quaternion.identity)as GameObject;
					_bulletR.tag = "Player";
					shootTimer = Time.time;
			}

			// speed up
			if (cInput.GetKey("Up")) {
				speed += (.15f);
			}

			// slow down
			if (cInput.GetKey("Down")) {
				speed -= (.15f);
			}

			// steer left or right - notice we use GetAxis for this
			var horizMovement : float = cInput.GetAxis("Horizontal");
			steer = -horizMovement * 45;
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, steer);
			transform.Translate(Vector3.right * horizMovement * 30 * Time.deltaTime);

			// clamp the eulerangles
			steer = Mathf.Clamp(steer, -45, 45);
			// clamp min and max speed
			speed = Mathf.Clamp(speed, 5, 10f);
			// keep the plane at the same height and clap the horizontal position
			var locX :float = Mathf.Clamp(transform.position.x, -24, 24f);
			transform.position = new Vector3(locX, 5, transform.position.z);
			// move the plane
			transform.Translate(Vector3.forward * speed * Time.deltaTime);

			// show speed on screen
			var _speed : int = (speed * 10);
			guiSpeed.text = "Speed: " + _speed;
		}
	}

	// set timescale to 0 to pause the game
	if (pause) {
		Time.timeScale = 0;
		pauseText.enabled = true;
	} else {
		Time.timeScale = 1;
		pauseText.enabled = false;
	}
}

// show the restart text when the player is dead
function OnDisable() {
	if (resetText) {
		resetText.enabled = true;
	}
}
