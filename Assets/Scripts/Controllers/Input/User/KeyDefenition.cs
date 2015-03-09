using UnityEngine;

public class KeyDefenition : MonoBehaviour {

    void Start()
    {
        // Keyboard
        cInput.SetKey("Menu", Keys.Escape);

        cInput.SetKey("Forward", Keys.W, Keys.UpArrow);
        cInput.SetKey("Backwards", Keys.S, Keys.DownArrow);
        cInput.SetKey("Left", Keys.A, Keys.LeftArrow);
        cInput.SetKey("Right", Keys.D, Keys.RightArrow);
        cInput.SetKey("Jump", Keys.Space);
        cInput.SetKey("Sprint", Keys.LeftShift, Keys.RightShift);
        cInput.SetKey("Crouch", Keys.LeftControl, Keys.C);

        cInput.SetKey("Poke1", Keys.Alpha1, Keys.Keypad1);
        cInput.SetKey("Poke2", Keys.Alpha2, Keys.Keypad2);
        cInput.SetKey("Poke3", Keys.Alpha3, Keys.Keypad3);
        cInput.SetKey("Poke4", Keys.Alpha4, Keys.Keypad4);
        cInput.SetKey("Poke5", Keys.Alpha5, Keys.Keypad5);
        cInput.SetKey("Poke6", Keys.Alpha6, Keys.Keypad6);
        cInput.SetKey("Move1", Keys.Alpha1);
        cInput.SetKey("Move2", Keys.Alpha2);
        cInput.SetKey("Move3", Keys.Alpha3);
        cInput.SetKey("Move4", Keys.Alpha4);

        cInput.SetKey("Bag", Keys.Tab, Keys.I);
        cInput.SetKey("Map", Keys.M);
        cInput.SetKey("PlayerList", Keys.T);
        cInput.SetKey("Chat", Keys.Enter);
        cInput.SetKey("Use", Keys.E);

        cInput.SetKey("FirstPersonCam", Keys.Plus);
        cInput.SetKey("ThirdPersonCam", Keys.Minus);

        cInput.SetAxis("Horizontal", "Left", "Right");
        cInput.SetAxis("Vertical", "Backwards", "Forward");
    }
}
