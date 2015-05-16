using UnityEngine;
using System.Collections;

public class MoterInputHandler : MonoBehaviour {
	public void MoveCharacter(SharedConstants.Movement __dir){
		switch(__dir) {
			case SharedConstants.Movement.forward:
				Debug.Log("Move Forward");
				break;
			case SharedConstants.Movement.back:
				Debug.Log("Move back");
				break;
			case SharedConstants.Movement.left:
				Debug.Log("Move left");
				break;
			case SharedConstants.Movement.right:
				Debug.Log("Move right");
				break;
			case SharedConstants.Movement.down:
				Debug.Log("Move down");
				break;
			case SharedConstants.Movement.up:
				Debug.Log("Move up");
				break;
		}
	}
}
