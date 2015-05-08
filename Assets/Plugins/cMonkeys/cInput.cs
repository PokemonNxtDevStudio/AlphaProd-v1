﻿//#define Use_cInputGUI // Comment out this line to use your own GUI instead of cInput's built-in GUI.

#region Namespaces

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#endregion



public class cInput : MonoBehaviour {

	#region cInput Variables and Properties

// cSkin is DEPRECATED!

	public static float gravity = 3;
	public static float sensitivity = 3;
	public static float deadzone = 0.001f;

	public static bool scanning { get { return _scanning; } } // scanning is read-only
	public static int length {
		get {
			_cInputInit(); // if cInput doesn't exist, create it
			return _inputLength + 1;
		}
	} // length is read-only
	public static bool allowDuplicates {
		get {
			_cInputInit(); // if cInput doesn't exist, create it
			return _allowDuplicates;
		}
		set {
			_allowDuplicates = value;
			PlayerPrefs.SetString("cInput_dubl", value.ToString());
			_exAllowDuplicates = value.ToString();
		}
	}

	// Private variables
	private static bool _allowDuplicates = false;
	private static string[,] _defaultStrings = new string[99, 5];
	private static string[] _inputName = new string[99]; // name of the input action (e.g., "Jump")
	private static KeyCode[] _inputPrimary = new KeyCode[99]; // primary input assigned to action (e.g., "Space")
	private static KeyCode[] _modifierUsedPrimary = new KeyCode[99]; // modfier used on primary input
	private static KeyCode[] _inputSecondary = new KeyCode[99]; // secondary input assigned to action
	private static KeyCode[] _modifierUsedSecondary = new KeyCode[99]; // modfier used on secondary input
	private static List<KeyCode> _modifiers = new List<KeyCode>(); // list that holds the allowed modifiers
	private static List<int> _markedAsAxis = new List<int>(); // list that keeps track of which actions are used to make axis
	private static string[] _axisName = new string[99];
	private static string[] _axisPrimary = new string[99];
	private static string[] _axisSecondary = new string[99];
	private static float[] _individualAxisSens = new float[99]; // individual axis sensitivity settings
	private static float[] _individualAxisGrav = new float[99]; // individual axis gravity settings
	private static float[] _individualAxisDead = new float[99]; // individual axis gravity settings
	private static bool[] _invertAxis = new bool[99];
	private static int[,] _makeAxis = new int[99, 2];
	private static int _inputLength = -1;
	private static int _axisLength = -1;
	private static List<KeyCode> _forbiddenKeys = new List<KeyCode>();

	private static bool[] _getKeyArray = new bool[99]; // values stored for GetKey function
	private static bool[] _getKeyDownArray = new bool[99]; // values stored for GetKeyDown
	private static bool[] _getKeyUpArray = new bool[99]; // values stored for GetKeyUp
	private static bool[] _axisTriggerArray = new bool[99]; // values that help to check if an axis is up or down
	private static float[] _getAxis = new float[99];
	private static float[] _getAxisRaw = new float[99];
	private static float[] _getAxisArray = new float[99];
	private static float[] _getAxisArrayRaw = new float[99];

	// which types of inputs to allow when assigning inputs to actions
	private static bool _allowMouseAxis = false;
	private static bool _allowMouseButtons = true;
	private static bool _allowJoystickButtons = true;
	private static bool _allowJoystickAxis = true;
	private static bool _allowKeyboard = true;

	private static int _numGamepads = 4; // number of gamepads supported by built-in Input Manager settings

	private Vector2 _scrollPosition;
	// these strings are set by ShowMenu() to customize the look of cInput's menu
	private static string _menuHeaderString = "label";
	private static string _menuActionsString = "box";
	private static string _menuInputsString = "box";
	private static string _menuButtonsString = "button";

	/// <summary>Are we scanning inputs to make a new assignment?</summary>
	private static bool _scanning;
	/// <summary>Which index number of the array for inputs to scan for</summary>
	private static int _cScanIndex;
	/// <summary>Which input are we scanning for (primary (1) or secondary (2))?</summary>
	private static int _cScanInput;
	/// <summary>A reference to the cInput object itself</summary>
	private static cInput _cObject;
	private static bool _cKeysLoaded;
	/// <summary>This is used to store all axis raw values so we can see if they changed since scanning began</summary>
	private static Dictionary<string, float> _axisRawValues = new Dictionary<string, float>();

	// External saving related variables
	private static string _exAllowDuplicates;
	private static string _exAxis;
	private static string _exAxisInverted;
	private static string _exDefaults;
	private static string _exInputs;
	private static string _exCalibrations;
	private static string _exCalibrationValues;
	private static bool _externalSaving = false;

	private static Dictionary<string, KeyCode> _string2Key = new Dictionary<string, KeyCode>();

	private static int[] _axisType = new int[10 * _numGamepads];
	/// <summary>The value that should be considered 0 for this axis</summary>
	private static Dictionary<string, float> _axisCalibrationOffset = new Dictionary<string, float>();
	// Note: this wastes one slot since we're ignoring gamepad 0
	private static string[,] _joyStrings = new string[_numGamepads + 1, 11];
	private static string[,] _joyStringsPos = new string[_numGamepads + 1, 11];
	private static string[,] _joyStringsNeg = new string[_numGamepads + 1, 11];

	#endregion // cInput Variables and Properties

	#region Awake/Start/Update functions

	void Awake() {
		DontDestroyOnLoad(this); // Keep this thing from getting destroyed if we change levels.

		// set values to global values
		for (int n = 0; n < 99; n++) {
			_individualAxisSens[n] = -99;
			_individualAxisGrav[n] = -99;
			_individualAxisDead[n] = -99;
		}
	}

	void Start() {
		_CreateDictionary();
		if (_externalSaving) {
			_LoadExternalInputs();
			//Debug.Log("cInput loaded inputs from external source.");
		} else {
			_LoadInputs();
			//Debug.Log("cInput settings loaded inputs from PlayerPrefs.");
		}

		AddModifier(KeyCode.None); // we need to initialize the modifiers with this one
	}

	void Update() {
		if (_scanning) {
			if (_cScanInput != 0) {
				// scan for a button press to assign a key (using the GUI)
				_InputScans();
			} else {
				// this is the part where a button is actually assigned after scanning is complete
				string _prim;
				string _sec;

				if (string.IsNullOrEmpty(_axisPrimary[_cScanIndex])) {
					_prim = _inputPrimary[_cScanIndex].ToString();
				} else {
					_prim = _axisPrimary[_cScanIndex];
				}

				if (string.IsNullOrEmpty(_axisSecondary[_cScanIndex])) {
					_sec = _inputSecondary[_cScanIndex].ToString();
				} else {
					_sec = _axisSecondary[_cScanIndex];
				}

				_ChangeKey(_cScanIndex, _inputName[_cScanIndex], _prim, _sec);
				_scanning = false;
			}
		} else {
			// update values for all inputs
			_CheckInputs();
		}
	}

	#endregion

	public static void Init() {
		_cInputInit(); // if cInput doesn't exist, create it
	}

	private static void _CreateDictionary() {
		if (_string2Key.Count == 0) { // don't create the dictionary more than once
			for (int i = (int)KeyCode.None; i <= (int)KeyCode.Joystick4Button19; i++) {
				KeyCode key = (KeyCode)i;
				_string2Key.Add(key.ToString(), key);
			}

			// Create joystrings dictionaries
			for (int i = 1; i <= _numGamepads; i++) {
				for (int j = 1; j <= 10; j++) {
					_joyStrings[i, j] = "Joy" + i + " Axis " + j;
					_joyStringsPos[i, j] = "Joy" + i + " Axis " + j + "+";
					_joyStringsNeg[i, j] = "Joy" + i + " Axis " + j + "-";
				}
			}
		}
	}

	public static void ForbidKey(KeyCode key) {
		_cInputInit(); // if cInput doesn't exist, create it
		if (!_forbiddenKeys.Contains(key)) {
			_forbiddenKeys.Add(key);
		}
	}

	public static void ForbidKey(string keyString) {
		_cInputInit(); // if cInput doesn't exist, create it
		KeyCode key = _ConvertString2Key(keyString);
		ForbidKey(key);
	}

	#region AddModifier and RemoveModifier functions

	/// <summary>Designates a key for use as a modifier.</summary>
	/// <param name="modifierKey">The KeyCode for the key to be used as a modifier.</param>
	public static void AddModifier(KeyCode modifierKey) {
		_cInputInit(); // if cInput doesn't exist, create it
		_modifiers.Add(modifierKey);
	}

	/// <summary>Designates a key for use as a modifier.</summary>
	/// <param name="modifier">The string name of the key to be used as a modifier.</param>
	public static void AddModifier(string modifier) {
		_cInputInit(); // if cInput doesn't exist, create it
		AddModifier(_ConvertString2Key(modifier));
	}

	/// <summary>Removes a key for use as a modifier.</summary>
	/// <param name="modifierKey">The KeyCode for the key which should no longer be used as a modifier.</param>
	public static void RemoveModifier(KeyCode modifierKey) {
		_cInputInit(); // if cInput doesn't exist, create it
		_modifiers.Remove(modifierKey);
	}

	/// <summary>Removes a key for use as a modifier.</summary>
	/// <param name="modifier">The string name of the key which should no longer be used as a modifier.</param>
	public static void RemoveModifier(string modifier) {
		_cInputInit(); // if cInput doesn't exist, create it
		RemoveModifier(_ConvertString2Key(modifier));
	}

	#endregion

	private static KeyCode _ConvertString2Key(string str) {
		if (String.IsNullOrEmpty(str)) { return KeyCode.None; }
		if (_string2Key.Count == 0) { _CreateDictionary(); }

		if (_string2Key.ContainsKey(str)) {
			KeyCode _key = _string2Key[str];
			return _key;
		} else {
			if (!_IsAxisValid(str)) {
				Debug.Log("cInput error: " + str + " is not a valid input.");
			}

			return KeyCode.None;
		}
	}

	#region SetKey functions

	#region SetKey Overloaded Functions
	// this is for compatibility with UnityScript which doesn't accept default parameters
	public static void SetKey(string action, string primary) {
		SetKey(action, primary, Keys.None, primary, Keys.None);
	}

	public static void SetKey(string action, string primary, string secondary) {
		SetKey(action, primary, secondary, primary, secondary);
	}

	// Defines a Key with a modifier on the primary input
	public static void SetKey(string action, string primary, string secondary, string primaryModifier) {
		SetKey(action, primary, secondary, primaryModifier, secondary);
	}

	#endregion //SetKey Overloaded Functions

	// Defines a Key with modifiers
	public static void SetKey(string action, string primary, string secondary, string primaryModifier, string secondaryModifier) {
		_cInputInit(); // if cInput doesn't exist, create it

		// make sure this key hasn't already been set
		if (_FindKeyByDescription(action) == -1) {
			int _num = _inputLength + 1;
			// make sure we pass valid values for the modifiers
			primaryModifier = (primaryModifier == Keys.None) ? primary : primaryModifier;
			secondaryModifier = (secondaryModifier == Keys.None) ? secondary : secondaryModifier;
			// actually set the key
			_SetDefaultKey(_num, action, primary, secondary, primaryModifier, secondaryModifier);
		} else {
			// skip this warning if we loaded from an external source or we already created the cInput object
			if (_externalSaving == false || GameObject.Find("cInput").GetComponent<cInput>() == null) {
				// Whoops! Key with this name already exists!
				//Debug.LogWarning("A key with the name of " + action + " already exists. You should use ChangeKey() if you want to change an existing key!");
			}
		}
	}

	private static void _SetDefaultKey(int _num, string _name, string _input1, string _input2, string pMod, string sMod) {
		_defaultStrings[_num, 0] = _name;
		_defaultStrings[_num, 1] = _input1;
		_defaultStrings[_num, 2] = (string.IsNullOrEmpty(_input2)) ? KeyCode.None.ToString() : _input2;
		_defaultStrings[_num, 3] = string.IsNullOrEmpty(pMod) ? _input1 : pMod;
		_defaultStrings[_num, 4] = string.IsNullOrEmpty(sMod) ? _input2 : sMod;

		if (_num > _inputLength) { _inputLength = _num; }

		_modifierUsedPrimary[_num] = _ConvertString2Key(_defaultStrings[_num, 3]);
		_modifierUsedSecondary[_num] = _ConvertString2Key(_defaultStrings[_num, 4]);
		_SetKey(_num, _name, _input1, _input2);
		_SaveDefaults();
	}

	private static void _SetKey(int _num, string _name, string _input1, string _input2) {
		// input description 
		_inputName[_num] = _name;
		_axisPrimary[_num] = "";

		if (_string2Key.Count == 0) { return; }

		if (!string.IsNullOrEmpty(_input1)) {
			// enter keyboard input in the input array
			KeyCode _keyCode1 = _ConvertString2Key(_input1);
			_inputPrimary[_num] = _keyCode1;

			// enter mouse and gamepad axis inputs in the inputstring array
			string axisName = _ChangeStringToAxisName(_input1);
			if (_input1 != axisName) {
				_axisPrimary[_num] = _input1;
			}
		}

		_axisSecondary[_num] = "";

		if (!string.IsNullOrEmpty(_input2)) {
			// enter input in the alt input array
			KeyCode _keyCode2 = _ConvertString2Key(_input2);
			_inputSecondary[_num] = _keyCode2;

			// enter mouse and gamepad axis inputs in the inputstring array
			string axisName = _ChangeStringToAxisName(_input2);
			if (_input2 != axisName) {
				_axisSecondary[_num] = _input2;
			}
		}
	}

	#endregion

	#region SetAxis and SetAxisSensitivity & related functions

	#region Overloaded SetAxis Functions

	// overload method to allow you to set an axis with two inputs
	public static void SetAxis(string description, string negativeInput, string positiveInput) {
		SetAxis(description, negativeInput, positiveInput, sensitivity, gravity, deadzone);
	}

	// overload method to allow you to set the sensitivity of the axis
	public static void SetAxis(string description, string negativeInput, string positiveInput, float axisSensitivity) {
		SetAxis(description, negativeInput, positiveInput, axisSensitivity, gravity, deadzone);
	}

	// overload method to allow you to set the sensitivity and the gravity an the axis
	public static void SetAxis(string description, string negativeInput, string positiveInput, float axisSensitivity, float axisGravity) {
		SetAxis(description, negativeInput, positiveInput, axisSensitivity, axisGravity, deadzone);
	}

	// overload method to allow you to set an axis with only one input
	public static void SetAxis(string description, string input) {
		SetAxis(description, input, "-1", sensitivity, gravity, deadzone);
	}

	// overload method to allow you to set an axis with only one input, and set sensitivity
	public static void SetAxis(string description, string input, float axisSensitivity) {
		SetAxis(description, input, "-1", axisSensitivity, gravity, deadzone);
	}

	// overload method to allow you to set an axis with only one input, and set sensitivity and gravity
	public static void SetAxis(string description, string input, float axisSensitivity, float axisGravity) {
		SetAxis(description, input, "-1", axisSensitivity, axisGravity, deadzone);
	}

	// overload method to allow you to set an axis with only one input, and set sensitivity, gravity and deadzone
	public static void SetAxis(string description, string input, float axisSensitivity, float axisGravity, float axisDeadzone) {
		SetAxis(description, input, "-1", axisSensitivity, axisGravity, axisDeadzone);
	}

	#endregion

	// This is the function that all other SetAxis overload methods call to actually set the axis
	public static void SetAxis(string description, string negativeInput, string positiveInput, float axisSensitivity, float axisGravity, float axisDeadzone) {
		_cInputInit(); // if cInput doesn't exist, create it
		if (IsKeyDefined(negativeInput)) {
			int _num = _FindAxisByDescription(description); // overwrite existing axis of same name
			if (_num == -1) {
				// this axis doesn't exist, so make a new one
				_num = _axisLength + 1;
			}

			int posInput = -1; // -1 by default, which means no input.
			int negInput = _FindKeyByDescription(negativeInput);

			if (IsKeyDefined(positiveInput)) {
				posInput = _FindKeyByDescription(positiveInput);
				_markedAsAxis.Add(_FindKeyByDescription(positiveInput)); // add the actions in the marked list
				_markedAsAxis.Add(negInput);
			} else if (positiveInput != "-1") {
				// the key isn't defined and we're not passing in -1 as a value, so there's a problem
				Debug.LogError("Can't define Axis named: " + description + ". Please define '" + positiveInput + "' with SetKey() first.");
				return; // break out of this function without trying to assign the axis
			}

			_SetAxis(_num, description, negInput, posInput);
			_individualAxisSens[negInput] = axisSensitivity;
			_individualAxisGrav[negInput] = axisGravity;
			_individualAxisDead[negInput] = axisDeadzone;
			if (posInput >= 0) {
				_individualAxisSens[posInput] = axisSensitivity;
				_individualAxisGrav[posInput] = axisGravity;
				_individualAxisDead[posInput] = axisDeadzone;
			}
		} else {
			Debug.LogError("Can't define Axis named: " + description + ". Please define '" + negativeInput + "' with SetKey() first.");
		}
	}

	private static void _SetAxis(int _num, string _description, int _negative, int _positive) {
		if (_num > _axisLength) {
			_axisLength = _num;
		}

		_invertAxis[_num] = false;
		_axisName[_num] = _description;
		_makeAxis[_num, 0] = _negative;
		_makeAxis[_num, 1] = _positive;
		_SaveAxis();
		_SaveAxInverted();
	}

	// this allows you to set the axis sensitivity directly (after the axis has been defined)
	public static void SetAxisSensitivity(string axisName, float sensitivity) {
		_cInputInit(); // if cInput doesn't exist, create it
		int axis = _FindAxisByDescription(axisName);
		if (axis == -1) {
			// axis not defined!
			Debug.LogError("Cannot set sensitivity of " + axisName + ". Have you defined this axis with SetAxis() yet?");
		} else {
			// axis has been defined
			_individualAxisSens[_makeAxis[axis, 0]] = sensitivity;
			_individualAxisSens[_makeAxis[axis, 1]] = sensitivity;
		}
	}

	// this allows you to set the axis gravity directly (after the axis has been defined)
	public static void SetAxisGravity(string axisName, float gravity) {
		_cInputInit(); // if cInput doesn't exist, create it
		int axis = _FindAxisByDescription(axisName);
		if (axis == -1) {
			// axis not defined!
			Debug.LogError("Cannot set gravity of " + axisName + ". Have you defined this axis with SetAxis() yet?");
		} else {
			// axis has been defined
			_individualAxisGrav[_makeAxis[axis, 0]] = gravity;
			_individualAxisGrav[_makeAxis[axis, 1]] = gravity;
		}
	}

	// this allows you to set the axis deadzone directly (after the axis has been defined)
	public static void SetAxisDeadzone(string axisName, float deadzone) {
		_cInputInit(); // if cInput doesn't exist, create it
		int axis = _FindAxisByDescription(axisName);
		if (axis == -1) {
			// axis not defined!
			Debug.LogError("Cannot set deadzone of " + axisName + ". Have you defined this axis with SetAxis() yet?");
		} else {
			// axis has been defined
			_individualAxisDead[_makeAxis[axis, 0]] = deadzone;
			_individualAxisDead[_makeAxis[axis, 1]] = deadzone;
		}
	}

	#endregion

	#region Calibration functions

	public static void Calibrate() {
		_cInputInit(); // if cInput doesn't exist, create it
		string _saveCals = "";
		_axisCalibrationOffset = _GetAxisRawValues();
		PlayerPrefs.SetString("cInput_calsVals", _CalibrationValuesToString());
		for (int joyNum = 1; joyNum <= _numGamepads; joyNum++) {
			for (int axisNum = 1; axisNum <= 10; axisNum++) {
				int index = 10 * (joyNum - 1) + (axisNum - 1);
				string _joystring = _joyStrings[joyNum, axisNum];
				float axisRaw = Input.GetAxisRaw(_joystring);
				_axisType[index] = (axisRaw < -deadzone) ? 1 : // axis is negative by default
					(axisRaw > deadzone) ? -1 : // axis is positive by default
					0; // axis is 0 by default
				_saveCals += _axisType[index] + "*";
				PlayerPrefs.SetString("cInput_saveCals", _saveCals);
				_exCalibrations = _saveCals;
			}
		}
	}

	private static string _CalibrationValuesToString() {
		string calVals = "";
		foreach (KeyValuePair<string, float> kvp in _axisCalibrationOffset) {
			calVals += kvp.Key + "*" + kvp.Value.ToString() + "#";
		}

		return calVals;
	}

	private static void _CalibrationValuesFromString(string calVals) {
		_axisCalibrationOffset.Clear(); // start with a clean slate
		string[] kvps = calVals.Split('#');
		for (int i = 0; i < kvps.Length - 1; i++) {
			string[] kvp = kvps[i].Split('*');
			_axisCalibrationOffset.Add(kvp[0], float.Parse(kvp[1]));
		}
	}

	private static float _GetCalibratedAxisInput(string description) {
		float rawValue = Input.GetAxisRaw(_ChangeStringToAxisName(description));

		switch (description) {
			case "Mouse Left":
			case "Mouse Right":
			case "Mouse Up":
			case "Mouse Down":
			case "Mouse Wheel Up":
			case "Mouse Wheel Down": { return rawValue; }
		}

		for (int joyNum = 1; joyNum <= _numGamepads; joyNum++) {
			for (int axisNum = 1; axisNum <= 10; axisNum++) {
				string joyNeg = _joyStringsNeg[joyNum, axisNum];
				string joyPos = _joyStringsPos[joyNum, axisNum];

				if (description == joyNeg || description == joyPos) {
					int index = 10 * (joyNum - 1) + (axisNum - 1);
					switch (_axisType[index]) {
						default:
						case 0: {
								return rawValue;
							}
						case 1: {
								return (rawValue + 1) / 2;
							}
						case -1: {
								return (rawValue - 1) / 2;
							}
					}
				}
			}
		}

		Debug.LogWarning("No match found for " + description + " (" + _ChangeStringToAxisName(description) +
						"). This should never happen, in theory. Returning value of " + rawValue);
		return rawValue;
	}

	#endregion

	#region ChangeKey functions

	#region ChangeKey (wait for input) functions

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="action">The string name of the key/action you want to change.</param>
	/// <param name="input">Primary = 1, Secondary = 2</param>
	/// <param name="allowMouseAxis">Allow a mouse axis to be bound? Default is false.</param>
	/// <param name="allowMouseButtons">Allow a mouse button to be bound? Default is true.</param>
	/// <param name="allowGamepadAxis">Allow a gamepad axis to be bound? Default is true.</param>
	/// <param name="allowGamepadButtons">Allow a gamepad button to be bound? Default is true.</param>
	/// <param name="allowKeyboard">Allow keyboard keys to be bound? Default is true.</param>
	public static void ChangeKey(string action, int input, bool allowMouseAxis, bool allowMouseButtons, bool allowGamepadAxis, bool allowGamepadButtons, bool allowKeyboard) {
		_cInputInit(); // if cInput doesn't exist, create it
		int _num = _FindKeyByDescription(action);
		_ScanForNewKey(_num, input, allowMouseAxis, allowMouseButtons, allowGamepadAxis, allowGamepadButtons, allowKeyboard);
	}

	#region overloaded ChangeKey(string) functions for UnityScript compatibility

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="action">The string name of the key/action you want to change.</param>
	public static void ChangeKey(string action) {
		ChangeKey(action, 1, _allowMouseAxis, _allowMouseButtons, _allowJoystickAxis, _allowJoystickButtons, _allowKeyboard);
	}

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="action">The string name of the key/action you want to change.</param>
	/// <param name="input">Primary = 1, Secondary = 2</param>
	public static void ChangeKey(string action, int input) {
		ChangeKey(action, input, _allowMouseAxis, _allowMouseButtons, _allowJoystickAxis, _allowJoystickButtons, _allowKeyboard);
	}

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="action">The string name of the key/action you want to change.</param>
	/// <param name="input">Primary = 1, Secondary = 2</param>
	/// <param name="allowMouseAxis">Allow a mouse axis to be bound? Default is false.</param>
	public static void ChangeKey(string action, int input, bool allowMouseAxis) {
		ChangeKey(action, input, allowMouseAxis, _allowMouseButtons, _allowJoystickAxis, _allowJoystickButtons, _allowKeyboard);
	}

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="action">The string name of the key/action you want to change.</param>
	/// <param name="input">Primary = 1, Secondary = 2</param>
	/// <param name="allowMouseAxis">Allow a mouse axis to be bound? Default is false.</param>
	/// <param name="allowMouseButtons">Allow a mouse button to be bound? Default is true.</param>
	public static void ChangeKey(string action, int input, bool allowMouseAxis, bool allowMouseButtons) {
		ChangeKey(action, input, allowMouseAxis, allowMouseButtons, _allowJoystickAxis, _allowJoystickButtons, _allowKeyboard);
	}

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="action">The string name of the key/action you want to change.</param>
	/// <param name="input">Primary = 1, Secondary = 2</param>
	/// <param name="allowMouseAxis">Allow a mouse axis to be bound? Default is false.</param>
	/// <param name="allowMouseButtons">Allow a mouse button to be bound? Default is true.</param>
	/// <param name="allowGamepadAxis">Allow a gamepad axis to be bound? Default is true.</param>
	public static void ChangeKey(string action, int input, bool allowMouseAxis, bool allowMouseButtons, bool allowGamepadAxis) {
		ChangeKey(action, input, allowMouseAxis, allowMouseButtons, allowGamepadAxis, _allowJoystickButtons, _allowKeyboard);
	}

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="action">The string name of the key/action you want to change.</param>
	/// <param name="input">Primary = 1, Secondary = 2</param>
	/// <param name="allowMouseAxis">Allow a mouse axis to be bound? Default is false.</param>
	/// <param name="allowMouseButtons">Allow a mouse button to be bound? Default is true.</param>
	/// <param name="allowGamepadAxis">Allow a gamepad axis to be bound? Default is true.</param>
	/// <param name="allowGamepadButtons">Allow a gamepad button to be bound? Default is true.</param>
	public static void ChangeKey(string action, int input, bool allowMouseAxis, bool allowMouseButtons, bool allowGamepadAxis, bool allowGamepadButtons) {
		ChangeKey(action, input, allowMouseAxis, allowMouseButtons, allowGamepadAxis, allowGamepadButtons, _allowKeyboard);
	}

	#endregion //overloaded ChangeKey(string) functions for UnityScript compatibility

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="index">The input array index of the key you want to change. Useful in for loops for GUI.</param>
	/// <param name="input">Primary = 1, Secondary = 2</param>
	/// <param name="allowMouseAxis">Allow a mouse axis to be bound? Default is false.</param>
	/// <param name="allowMouseButtons">Allow a mouse button to be bound? Default is true.</param>
	/// <param name="allowGamepadAxis">Allow a gamepad axis to be bound? Default is true.</param>
	/// <param name="allowGamepadButtons">Allow a gamepad button to be bound? Default is true.</param>
	/// <param name="allowKeyboard">Allow keyboard keys to be bound? Default is true.</param>
	public static void   ChangeKey(int index, int input, bool allowMouseAxis, bool allowMouseButtons, bool allowGamepadAxis, bool allowGamepadButtons, bool allowKeyboard) {
		_cInputInit(); // if cInput doesn't exist, create it
		_ScanForNewKey(index, input, allowMouseAxis, allowMouseButtons, allowGamepadAxis, allowGamepadButtons, allowKeyboard);
	}

	#region overloaded ChangeKey(int) functions for UnityScript compatibility

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="index">The input array index of the key you want to change. Useful in for loops for GUI.</param>
	public static void ChangeKey(int index) {
		ChangeKey(index, 1, _allowMouseAxis, _allowMouseButtons, _allowJoystickAxis, _allowJoystickButtons, _allowKeyboard);
	}

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="index">The input array index of the key you want to change. Useful in for loops for GUI.</param>
	/// <param name="input">Primary = 1, Secondary = 2</param>
	public static void ChangeKey(int index, int input) {
		ChangeKey(index, input, _allowMouseAxis, _allowMouseButtons, _allowJoystickAxis, _allowJoystickButtons, _allowKeyboard);
	}

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="index">The input array index of the key you want to change. Useful in for loops for GUI.</param>
	/// <param name="input">Primary = 1, Secondary = 2</param>
	/// <param name="allowMouseAxis">Allow a mouse axis to be bound? Default is false.</param>
	public static void ChangeKey(int index, int input, bool allowMouseAxis) {
		ChangeKey(index, input, allowMouseAxis, _allowMouseButtons, _allowJoystickAxis, _allowJoystickButtons, _allowKeyboard);
	}

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="index">The input array index of the key you want to change. Useful in for loops for GUI.</param>
	/// <param name="input">Primary = 1, Secondary = 2</param>
	/// <param name="allowMouseAxis">Allow a mouse axis to be bound? Default is false.</param>
	/// <param name="allowMouseButtons">Allow a mouse button to be bound? Default is true.</param>
	public static void ChangeKey(int index, int input, bool allowMouseAxis, bool allowMouseButtons) {
		ChangeKey(index, input, allowMouseAxis, allowMouseButtons, _allowJoystickAxis, _allowJoystickButtons, _allowKeyboard);
	}

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="index">The input array index of the key you want to change. Useful in for loops for GUI.</param>
	/// <param name="input">Primary = 1, Secondary = 2</param>
	/// <param name="allowMouseAxis">Allow a mouse axis to be bound? Default is false.</param>
	/// <param name="allowMouseButtons">Allow a mouse button to be bound? Default is true.</param>
	/// <param name="allowGamepadAxis">Allow a gamepad axis to be bound? Default is true.</param>
	public static void ChangeKey(int index, int input, bool allowMouseAxis, bool allowMouseButtons, bool allowGamepadAxis) {
		ChangeKey(index, input, allowMouseAxis, allowMouseButtons, allowGamepadAxis, _allowJoystickButtons, _allowKeyboard);
	}

	/// <summary>cInput will wait for the user to press a button, then bind that button to this key. Also see cInput.scanning.</summary>
	/// <param name="index">The input array index of the key you want to change. Useful in for loops for GUI.</param>
	/// <param name="input">Primary = 1, Secondary = 2</param>
	/// <param name="allowMouseAxis">Allow a mouse axis to be bound? Default is false.</param>
	/// <param name="allowMouseButtons">Allow a mouse button to be bound? Default is true.</param>
	/// <param name="allowGamepadAxis">Allow a gamepad axis to be bound? Default is true.</param>
	/// <param name="allowGamepadButtons">Allow a gamepad button to be bound? Default is true.</param>
	public static void ChangeKey(int index, int input, bool allowMouseAxis, bool allowMouseButtons, bool allowGamepadAxis, bool allowGamepadButtons) {
		ChangeKey(index, input, allowMouseAxis, allowMouseButtons, allowGamepadAxis, allowGamepadButtons, _allowKeyboard);
	}

	#endregion //overloaded ChangeKey(int) functions for UnityScript compatibility

	#endregion //ChangeKey (wait for input) functions

	#region ChangeKey set via script (don't wait for input)

	// this lets the dev directly change the key without waiting for the player to push buttons.
	public static void ChangeKey(string action, string primary, string secondary, string primaryModifier, string secondaryModifier) {
		_cInputInit(); // if cInput doesn't exist, create it
		int _num = _FindKeyByDescription(action);

		// set modifiers
		if (String.IsNullOrEmpty(primaryModifier)) {
			primaryModifier = primary;
		}

		if (String.IsNullOrEmpty(secondaryModifier)) {
			secondaryModifier = secondary;
		}

		_modifierUsedPrimary[_num] = _ConvertString2Key(primaryModifier);
		_modifierUsedSecondary[_num] = _ConvertString2Key(secondaryModifier);

		_ChangeKey(_num, action, primary, secondary);
	}

	#region overloaded ChangeKey(string, primary, secondary) function for UnityScript compatibility)

	public static void ChangeKey(string action, string primary) {
		int _num = _FindKeyByDescription(action);
		ChangeKey(action, primary, "", primary, _modifierUsedSecondary[_num].ToString());
	}

	public static void ChangeKey(string action, string primary, string secondary) {
		ChangeKey(action, primary, secondary, primary, secondary);
	}

	public static void ChangeKey(string action, string primary, string secondary, string primaryModifier) {
		ChangeKey(action, primary, secondary, primaryModifier, secondary);
	}

	#endregion

	#endregion //ChangeKey set via script (don't wait for input)

	/// <summary>This starts the process of scanning for a new key (using the GUI to assign an input).</summary>
	/// <param name="num">The index of the input array</param>
	/// <param name="input">Primary or secondary input</param>
	/// <param name="mouseAx">Allow mouse axis (mouse ball) to be bound?</param>
	/// <param name="mouseBut">Allow mouse buttons to be bound?</param>
	/// <param name="joyAx">Allow joystick axes to be bound?</param>
	/// <param name="joyBut">Allow joystick buttons to be bound?</param>
	/// <param name="keyb">Allow keys from the keyboard to be bound?</param>
	private static void _ScanForNewKey(int num, int input, bool mouseAx, bool mouseBut, bool joyAx, bool joyBut, bool keyb) {
		_allowMouseAxis = mouseAx;
		_allowMouseButtons = mouseBut;
		_allowJoystickButtons = joyBut;
		_allowJoystickAxis = joyAx;
		_allowKeyboard = keyb;

		_cScanInput = input;
		_cScanIndex = num;
		_scanning = true;

		_axisRawValues = _GetAxisRawValues(); // get current axis values to make sure they change while scanning
	}

	/// <summary>Iterates through all of the axes in the Input Manager and saves the values in a dictionary.</summary>
	private static Dictionary<string, float> _GetAxisRawValues() {
		Dictionary<string, float> arv = new Dictionary<string, float>(); // arv means Axis Raw Values

		// these are all manually taken from the Axes that cInput installs in the Input Manager
		arv.Add("Horizontal", Input.GetAxisRaw("Horizontal"));
		arv.Add("Vertical", Input.GetAxisRaw("Vertical"));
		arv.Add("Fire1", Input.GetAxisRaw("Fire1"));
		arv.Add("Fire2", Input.GetAxisRaw("Fire2"));
		arv.Add("Fire3", Input.GetAxisRaw("Fire3"));
		arv.Add("Jump", Input.GetAxisRaw("Jump"));
		arv.Add("Mouse X", Input.GetAxisRaw("Mouse X"));
		arv.Add("Mouse Y", Input.GetAxisRaw("Mouse Y"));
		arv.Add("Mouse Horizontal", Input.GetAxisRaw("Mouse Horizontal"));
		arv.Add("Mouse Vertical", Input.GetAxisRaw("Mouse Vertical"));
		arv.Add("Mouse ScrollWheel", Input.GetAxisRaw("Mouse ScrollWheel"));
		arv.Add("Mouse Wheel", Input.GetAxisRaw("Mouse Wheel"));
		arv.Add("Window Shake X", Input.GetAxisRaw("Window Shake X"));
		arv.Add("Window Shake Y", Input.GetAxisRaw("Window Shake Y"));
		arv.Add("Shift", Input.GetAxisRaw("Shift"));

		string gpString = "";
		for (int gamePad = 1; gamePad <= _numGamepads; gamePad++) {
			for (int axis = 1; axis <= 10; axis++) {
				gpString = "Joy" + gamePad + " Axis " + axis;
				arv.Add(gpString, Input.GetAxis(gpString));
			}
		}

		return arv;
	}

	private static void _ChangeKey(int num, string action, string primary, string secondary) {
		_SetKey(num, action, primary, secondary);
		_SaveInputs();
	}

	#endregion

	#region _DefaultsExist, IsKeyDefined, and IsAxisDefined functions

	private static bool _DefaultsExist() {
		return (_defaultStrings.Length > 0) ? true : false;
	}

	public static bool IsKeyDefined(string keyName) {
		_cInputInit(); // if cInput doesn't exist, create it
		return (_FindKeyByDescription(keyName) >= 0) ? true : false;
	}

	public static bool IsAxisDefined(string axisName) {
		_cInputInit(); // if cInput doesn't exist, create it
		return (_FindAxisByDescription(axisName) >= 0) ? true : false;
	}

	#endregion

	#region CheckInputs function

	/// <summary>This is the magic that updates the values for all the inputs in cInput</summary>
	private void _CheckInputs() {
		bool inputPrimary = false; // a digital button/key; true if it's currently being pushed down
		bool inputSecondary = false; // a digital button/key; true if it's currently being pushed down
		bool axisPrimaryDefined = false; // whether or not an axis has a primary input defined for this element
		bool axisSecondaryDefined = false; // whether or not an axis has a secondary input defined for this element
		float axisPrimaryValue = 0f; // the value of the primary input for this element
		float axisSecondaryValue = 0f; // the value of the secondary input for this element

		#region Update input values

		for (int n = 0; n < _inputLength + 1; n++) {

			#region Handle cInput Keys/Buttons

			inputPrimary = Input.GetKey(_inputPrimary[n]);
			inputSecondary = Input.GetKey(_inputSecondary[n]);

			bool _pModPressed = false; // is the primary modifier currently being pressed?
			bool _sModPressed = false; // is the secondary modifier currently being pressed?
			bool _modifierPressed = false; // is any modifier currently being pressed?

			for (int i = 0; i < _modifiers.Count; i++) {
				if (Input.GetKey(_modifiers[i])) {
					_modifierPressed = true; // at least one modifier is active
					if (!_pModPressed && _modifiers[i] == _modifierUsedPrimary[n]) { _pModPressed = true; }
					if (!_sModPressed && _modifiers[i] == _modifierUsedSecondary[n]) { _sModPressed = true; }
				}
			}

			/* These next two lines are realy ugly, so here's an explanation of the parts:
			 * (_modifierUsedPrimary[n] == _inputPrimary[n]) <-- means there is NO modifier for this input
			 * (!_modifierPressed) <-- means there was NO modifier key pushed
			 * (_modifierUsedPrimary[n] != _inputPrimary[n]) <-- means there IS a modifier for this input
			 * (_pModPressed) <-- means the modifier for this input HAS been pushed.
			 * 
			 * So what this does is checks two things:
			 * If there's no modifier AND no modifier keys are being pressed, we're good to go.
			 * OR
			 * If there is a modifier AND the modifier key is being pressed, we're good to go.
			 * */
			// These bools are used to determine if this key's modifier (if any) is being pushed.
			bool _primaryModifierPassed = (((_modifierUsedPrimary[n] == _inputPrimary[n]) && !_modifierPressed) || ((_modifierUsedPrimary[n] != _inputPrimary[n]) && _pModPressed));
			bool _secondaryModifierPassed = (((_modifierUsedSecondary[n] == _inputSecondary[n]) && !_modifierPressed) || (_modifierUsedSecondary[n] != _inputSecondary[n] && _sModPressed));

			if (!string.IsNullOrEmpty(_axisPrimary[n])) {
				axisPrimaryDefined = true; // this is an axis
				axisPrimaryValue = _GetCalibratedAxisInput(_axisPrimary[n]) * _PosOrNeg(_axisPrimary[n]);
			} else {
				axisPrimaryDefined = false; // this isn't an axis
				// set the value to 1 if the key is being pushed down, otherwise it's zero
				axisPrimaryValue = inputPrimary ? 1f : 0f;
			}

			if (!string.IsNullOrEmpty(_axisSecondary[n])) {
				axisSecondaryDefined = true;
				axisSecondaryValue = _GetCalibratedAxisInput(_axisSecondary[n]) * _PosOrNeg(_axisSecondary[n]);
			} else {
				axisSecondaryDefined = false; // this isn't an axis
				// set the value to 1 if the key is being pushed down, otherwise it's zero
				axisSecondaryValue = inputSecondary ? 1f : 0f;
			}

			#region GetKey
			if ((inputPrimary && _primaryModifierPassed) || (inputSecondary && _secondaryModifierPassed) || (axisPrimaryDefined && axisPrimaryValue > deadzone) || (axisSecondaryDefined && axisSecondaryValue > deadzone)) {
				_getKeyArray[n] = true;
			} else {
				_getKeyArray[n] = false;
			}
			#endregion //GetKey

			#region GetKeyDown
			if ((_primaryModifierPassed && Input.GetKeyDown(_inputPrimary[n])) || (_secondaryModifierPassed && Input.GetKeyDown(_inputSecondary[n]))) {
				_getKeyDownArray[n] = true;
			} else if ((axisPrimaryDefined && axisPrimaryValue > deadzone && !_axisTriggerArray[n]) ||
						(axisSecondaryDefined && axisSecondaryValue > deadzone && !_axisTriggerArray[n])) {
				_axisTriggerArray[n] = true;
				_getKeyDownArray[n] = true;
			} else {
				_getKeyDownArray[n] = false;
			}
			#endregion //GetKeyDown

			#region GetKeyUp
			if ((Input.GetKeyUp(_inputPrimary[n]) && _primaryModifierPassed) || (Input.GetKeyUp(_inputSecondary[n]) && _secondaryModifierPassed)) {
				_getKeyUpArray[n] = true;
			} else if ((axisPrimaryDefined && axisPrimaryValue <= deadzone && _axisTriggerArray[n]) ||
						(axisSecondaryDefined && axisSecondaryValue <= deadzone && _axisTriggerArray[n])) {
				_axisTriggerArray[n] = false;
				_getKeyUpArray[n] = true;
			} else {
				_getKeyUpArray[n] = false;
			}
			#endregion //GetKeyUp

			#endregion //Handle cInput Keys/Buttons

			#region Handle cInput Axes

			// Store global sensitivity, gravity and deadzone so we can change them and restore them later.
			// I know it seems silly to do this every iteration of the loop, but for some reason if we don't, it breaks things.
			float defaultSens = sensitivity;
			float defaultGrav = gravity;
			float defaultDead = deadzone;
			// Set individual sensitivity, gravity and deadzone
			sensitivity = (_individualAxisSens[n] != -99) ? _individualAxisSens[n] : defaultSens;
			gravity = (_individualAxisGrav[n] != -99) ? _individualAxisGrav[n] : defaultGrav;
			deadzone = (_individualAxisDead[n] != -99) ? _individualAxisDead[n] : defaultDead;

			// gets the axis value(s) and apply smoothing (sensitivity/gravity) for non-raw value
			if (axisPrimaryValue > deadzone || axisSecondaryValue > deadzone) {
				// for the raw value, just take the highest value from the primary or secondary input
				_getAxisRaw[n] = Mathf.Max(axisPrimaryValue, axisSecondaryValue);

				// use sensitivity settings to gradually bring the non-raw value up to 1 if not already there
				if (_getAxis[n] < _getAxisRaw[n]) { _getAxis[n] = Mathf.Min(_getAxis[n] + sensitivity * Time.deltaTime, _getAxisRaw[n]); }
			} else {
				// both inputs are less than or equal to deadzone cutoff
				_getAxisRaw[n] = 0; //pretend you're not getting any value at all on the raw axis

				// use gravity settings to gradually bring the non-raw value back down to zero if not already there
				if (_getAxis[n] > 0) { _getAxis[n] = Mathf.Max(0, _getAxis[n] - gravity * Time.deltaTime); }
			}

			// Restore global sensitivity, gravity and deadzone.
			// I know it seems silly to do this every iteration of the loop, but for some reason if we don't, it breaks things.
			sensitivity = defaultSens;
			gravity = defaultGrav;
			deadzone = defaultDead;

			#endregion //Handle cInput Axes

		}

		#endregion //Update input values

		/*
		 * NO LONGER IN THE FOR LOOP ABOVE WHICH GETS THE VALUES OF THE INPUTS!
		*/

		// compile the virtual axes (negative and positive)
		for (int n = 0; n <= _axisLength; n++) {
			int neg = _makeAxis[n, 0];
			int pos = _makeAxis[n, 1];
			if (_makeAxis[n, 1] == -1) {
				// This axis has no "positive" input defined, so use the "negative" axis as the default value
				_getAxisArray[n] = _getAxis[neg];
				_getAxisArrayRaw[n] = _getAxisRaw[neg];
			} else {
				// This axis has both a negative and positive input defined, so combine them for the result
				_getAxisArray[n] = _getAxis[pos] - _getAxis[neg];
				_getAxisArrayRaw[n] = _getAxisRaw[pos] - _getAxisRaw[neg];
			}
		}
	}

	#endregion

	#region GetKey, GetAxis, GetText, and related functions

	#region GetKey functions

	// returns -1 only if there was an error
	private static int _FindKeyByDescription(string description) {
		for (int i = 0; i < _inputName.Length; i++) {
			if (_inputName[i] == description) {
				return i;
			}
		}

		// uh oh, the string didn't match!
		return -1;
	}

	/// <summary>Returns true every frame the input is being pressed</summary>
	public static bool GetKey(string description) {
		_cInputInit(); // if cInput doesn't exist, create it
		if (!_DefaultsExist()) {
			Debug.LogError("No default inputs found. Please setup your default inputs with SetKey first.");
			return false;
		}

		if (!_cKeysLoaded) { return false; } // make sure we've saved/loaded keys before trying to access them.
		int _index = _FindKeyByDescription(description);

		if (_index > -1) {
			return _getKeyArray[_index];
		} else {
			// if we got this far then the string didn't match and there's a problem
			Debug.LogError("Couldn't find a key match for " + description + ". Is it possible you typed it wrong or forgot to setup your defaults after making changes?");
			return false;
		}
	}

	/// <summary>Returns true just once when the input is first pressed down</summary>
	public static bool GetKeyDown(string description) {
		_cInputInit(); // if cInput doesn't exist, create it
		if (!_DefaultsExist()) {
			Debug.LogError("No default inputs found. Please setup your default inputs with SetKey first.");
			return false;
		}

		if (!_cKeysLoaded) { return false; } // make sure we've saved/loaded keys before trying to access them.
		int _index = _FindKeyByDescription(description);

		if (_index > -1) {
			return _getKeyDownArray[_FindKeyByDescription(description)];
		} else {
			// if we got this far then the string didn't match and there's a problem
			Debug.LogError("Couldn't find a key match for " + description + ". Is it possible you typed it wrong or forgot to setup your defaults after making changes?");
			return false;
		}
	}

	/// <summary>Returns true just once when the input is released</summary>
	public static bool GetKeyUp(string description) {
		_cInputInit(); // if cInput doesn't exist, create it
		if (!_DefaultsExist()) {
			Debug.LogError("No default inputs found. Please setup your default inputs with SetKey first.");
			return false;
		}

		if (!_cKeysLoaded) { return false; } // make sure we've saved/loaded keys before trying to access them.
		int _index = _FindKeyByDescription(description);

		if (_index > -1) {
			return _getKeyUpArray[_FindKeyByDescription(description)];
		} else {
			// if we got this far then the string didn't match and there's a problem
			Debug.LogError("Couldn't find a key match for " + description + ". Is it possible you typed it wrong or forgot to setup your defaults after making changes?");
			return false;
		}
	}

	#region GetButton functions -- they just call GetKey functions

	/// <summary>Returns true every frame the input is being pressed</summary>
	public static bool GetButton(string description) {
		return GetKey(description);
	}

	/// <summary>Returns true just once when the input is first pressed down</summary>
	public static bool GetButtonDown(string description) {
		return GetKeyDown(description);
	}

	/// <summary>Returns true just once when the input is released</summary>
	public static bool GetButtonUp(string description) {
		return GetKeyUp(description);
	}

	#endregion //GetButton functions -- they just call GetKey functions

	#endregion //GetKey functions

	#region GetAxis and related functions

	private static int _FindAxisByDescription(string axisName) {
		for (int i = 0; i < _axisName.Length; i++) {
			if (_axisName[i] == axisName) {
				return i;
			}
		}

		return -1; // uh oh, the string didn't match!
	}

	public static float GetAxis(string axisName) {
		_cInputInit(); // if cInput doesn't exist, create it
		if (!_DefaultsExist()) {
			Debug.LogError("No default inputs found. Please setup your default inputs with SetKey first.");
			return 0;
		}

		int index = _FindAxisByDescription(axisName);
		if (index > -1) {
			if (_invertAxis[index]) {
				// this axis should be inverted, so invert the value!
				return _getAxisArray[index] * -1;
			} else {
				// this axis is normal, return the normal value
				return _getAxisArray[index];
			}
		}

		// if we got this far then the string didn't match and there's a problem
		Debug.LogError("Couldn't find an axis match for " + axisName + ". Is it possible you typed it wrong?");
		return 0;
	}

	public static float GetAxisRaw(string axisName) {
		_cInputInit(); // if cInput doesn't exist, create it
		if (!_DefaultsExist()) {
			Debug.LogError("No default inputs found. Please setup your default inputs with SetKey first.");
			return 0;
		}

		int index = _FindAxisByDescription(axisName);
		if (index > -1) {
			if (_invertAxis[index]) {
				// this axis should be inverted, so invert the value!
				return _getAxisArrayRaw[index] * -1;
			} else {
				// this axis is normal, return the normal value
				return _getAxisArrayRaw[index];
			}
		}

		// if we got this far then the string didn't match and there's a problem
		Debug.LogError("Couldn't find an axis match for " + axisName + ". Is it possible you typed it wrong?");
		return 0;
	}

	#endregion //GetAxis and related functions

	#region GetText, _ChangeStringToAxisName, _PosOrNeg functions

	#region Overloaded GetText(string) and GetText(int) functions for UnityScript compatibility

	public static string GetText(string action) {
		return GetText(action, 1);
	}

	public static string GetText(int index) {
		return GetText(index, 0);
	}

	#endregion //Overloaded GetText(string) and GetText(int) functions for UnityScript compatibility

	public static string GetText(string action, int input) {
		int index = _FindKeyByDescription(action);
		return GetText(index, input);
	}

	/// <summary>Get the name of an input using an int. Useful in for loops for GUIs.</summary>
	/// <param name="index">The index of the input.</param>
	/// <param name="input">Label, Primary, or Secondary. (0, 1, 2)</param>
	public static string GetText(int index, int input) {
		_cInputInit(); // if cInput doesn't exist, create it
		// make sure a valid value is passed in
		if (input < 0 || input > 2) {
			Debug.LogWarning("Can't look for text #" + input + " for " + _inputName[index] + " input. Only 0, 1, or 2 is acceptable. Clamping to this range.");
			input = Mathf.Clamp(input, 0, 2);
		}

		string name;

		if (input == 1) {
			if (!string.IsNullOrEmpty(_axisPrimary[index])) {
				name = _axisPrimary[index];
			} else {
				string _prefix = "";
				// if modifier is not empty and isn't the same as the key, and the key isn't empty
				if (_modifierUsedPrimary[index] != KeyCode.None && _modifierUsedPrimary[index] != _inputPrimary[index] && _inputPrimary[index] != KeyCode.None) {
					_prefix = _modifierUsedPrimary[index].ToString() + " + ";
				}
				name = _prefix + _inputPrimary[index].ToString();
			}
		} else if (input == 2) {
			if (!string.IsNullOrEmpty(_axisSecondary[index])) {
				name = _axisSecondary[index];
			} else {
				string _prefix = "";
				// if modifier is not empty and isn't the same as the key, and the key isn't empty
				if (_modifierUsedSecondary[index] != KeyCode.None && _modifierUsedSecondary[index] != _inputSecondary[index] && _inputSecondary[index] != KeyCode.None) {
					_prefix = _modifierUsedSecondary[index].ToString() + " + ";
				}
				name = _prefix + _inputSecondary[index].ToString();
			}
		} else {
			name = _inputName[index];
			return name;
		}

		// check to see if this key is currently waiting to be reassigned
		if (_scanning && (index == _cScanIndex) && (input == _cScanInput)) {
			name = ". . .";
		}

		return name;
	}

	private static string _ChangeStringToAxisName(string description) {
		// First we need to change the name of some of these things. . .
		switch (description) {
			case "Mouse Left": { return "Mouse Horizontal"; }
			case "Mouse Right": { return "Mouse Horizontal"; }
			case "Mouse Up": { return "Mouse Vertical"; }
			case "Mouse Down": { return "Mouse Vertical"; }
			case "Mouse Wheel Up": { return "Mouse Wheel"; }
			case "Mouse Wheel Down": { return "Mouse Wheel"; }
		}

		string joystring = _FindJoystringByDescription(description);
		if (joystring != null) {
			return joystring;
		}

		return description;
	}

	private static string _FindJoystringByDescription(string desc) {
		for (int i = 1; i <= _numGamepads; i++) {
			for (int j = 1; j <= 10; j++) {
				string joyPos = _joyStringsPos[i, j];
				string joyNeg = _joyStringsNeg[i, j];
				if (desc == joyPos || desc == joyNeg) {
					return _joyStrings[i, j];
				}
			}
		}

		return null;
	}

	private static bool _IsAxisValid(string axis) {
		switch (axis) {
			case "Mouse Left": { return true; }
			case "Mouse Right": { return true; }
			case "Mouse Up": { return true; }
			case "Mouse Down": { return true; }
			case "Mouse Wheel Up": { return true; }
			case "Mouse Wheel Down": { return true; }
		}

		bool _state = false;
		for (int i = 1; i <= _numGamepads; i++) {
			for (int j = 1; j <= 10; j++) {
				string joyPos = _joyStringsPos[i, j];
				string joyNeg = _joyStringsNeg[i, j];
				if (axis == joyPos || axis == joyNeg) {
					_state = true;
				}
			}
		}

		return _state;
	}

	// This function returns -1 for negative axes
	private static int _PosOrNeg(string description) {
		int posneg = 1;

		switch (description) {
			case "Mouse Left": { return -1; }
			case "Mouse Right": { return 1; }
			case "Mouse Up": { return 1; }
			case "Mouse Down": { return -1; }
			case "Mouse Wheel Up": { return 1; }
			case "Mouse Wheel Down": { return -1; }
		}

		for (int i = 1; i <= _numGamepads; i++) {
			for (int j = 1; j < 10; j++) {
				string joyPos = _joyStringsPos[i, j];
				string joyNeg = _joyStringsNeg[i, j];
				if (description == joyPos) {
					return 1;
				} else if (description == joyNeg) {
					return -1;
				}
			}
		}

		return posneg;
	}

	#endregion //GetText, _ChangeStringToAxisName, _PosOrNeg functions

	#endregion //GetKey, GetAxis, GetText, and related functions

	#region Save, Load, Reset & Clear functions

	private static void _SaveAxis() {
		int _num = _axisLength + 1;
		string _axName = "";
		string _axNeg = "";
		string _axPos = "";
		string _indAxSens = "";
		string _indAxGrav = "";
		string _indAxDead = "";
		for (int n = 0; n < _num; n++) {
			_axName += _axisName[n] + "*";
			_axNeg += _makeAxis[n, 0] + "*";
			_axPos += _makeAxis[n, 1] + "*";
			_indAxSens += _individualAxisSens[n] + "*";
			_indAxGrav += _individualAxisGrav[n] + "*";
			_indAxDead += _individualAxisDead[n] + "*";
		}

		string _axis = _axName + "#" + _axNeg + "#" + _axPos + "#" + _num;
		PlayerPrefs.SetString("cInput_axis", _axis);
		PlayerPrefs.SetString("cInput_indAxSens", _indAxSens);
		PlayerPrefs.SetString("cInput_indAxGrav", _indAxGrav);
		PlayerPrefs.SetString("cInput_indAxDead", _indAxDead);
		_exAxis = _axis + "¿" + _indAxSens + "¿" + _indAxGrav + "¿" + _indAxDead;
	}

	private static void _SaveAxInverted() {
		int _num = _axisLength + 1;
		string _axInv = "";

		for (int n = 0; n < _num; n++) {
			_axInv += _invertAxis[n] + "*";
		}

		PlayerPrefs.SetString("cInput_axInv", _axInv);
		_exAxisInverted = _axInv;
	}

	private static void _SaveDefaults() {
		// saving default inputs
		int _num = _inputLength + 1;
		string _defName = "";
		string _def1 = "";
		string _def2 = "";
		string _defmod1 = "";
		string _defmod2 = "";
		for (int n = 0; n < _num; n++) {

			_defName += _defaultStrings[n, 0] + "*";
			_def1 += _defaultStrings[n, 1] + "*";
			_def2 += _defaultStrings[n, 2] + "*";
			_defmod1 += _defaultStrings[n, 3] + "*";
			_defmod2 += _defaultStrings[n, 4] + "*";
		}

		string _Default = _defName + "#" + _def1 + "#" + _def2 + "#" + _defmod1 + "#" + _defmod2;
		PlayerPrefs.SetInt("cInput_count", _num);
		PlayerPrefs.SetString("cInput_defaults", _Default);
		_exDefaults = _num + "¿" + _Default;
	}

	public static void _SaveInputs() {
		int _num = _inputLength + 1;
		// *** save input configuration ***
		string _descr = "";
		string _inp = "";
		string _alt_inp = "";
		string _inpStr = "";
		string _alt_inpStr = "";
		string _modifierStr = "";
		string _alt_modifierStr = "";

		for (int n = 0; n < _num; n++) {
			// make the strings
			_descr += _inputName[n] + "*";
			_inp += _inputPrimary[n] + "*";
			_alt_inp += _inputSecondary[n] + "*";
			_inpStr += _axisPrimary[n] + "*";
			_alt_inpStr += _axisSecondary[n] + "*";
			_modifierStr += _modifierUsedPrimary[n] + "*";
			_alt_modifierStr += _modifierUsedSecondary[n] + "*";
		}

		// save the strings to the PlayerPrefs
		PlayerPrefs.SetString("cInput_descr", _descr);
		PlayerPrefs.SetString("cInput_inp", _inp);
		PlayerPrefs.SetString("cInput_alt_inp", _alt_inp);
		PlayerPrefs.SetString("cInput_inpStr", _inpStr);
		PlayerPrefs.SetString("cInput_alt_inpStr", _alt_inpStr);
		PlayerPrefs.SetString("cInput_modifierStr", _modifierStr);
		PlayerPrefs.SetString("cInput_alt_modifierStr", _alt_modifierStr);
		_exInputs = _descr + "¿" + _inp + "¿" + _alt_inp + "¿" + _inpStr + "¿" + _alt_inpStr + "¿" + _modifierStr + "¿" + _alt_modifierStr;
	}

	public static string externalInputs {
		get {
			return _exAllowDuplicates + "æ" + _exAxis + "æ" + _exAxisInverted + "æ" + _exDefaults + "æ" + _exInputs +
					"æ" + _exCalibrations + "æ" + _exCalibrationValues;
		}
	}

	public static void LoadExternal(string externString) {
		_cInputInit(); // if cInput doesn't exist, create it
		string[] tmpExternalStrings = externString.Split('æ');
		_exAllowDuplicates = tmpExternalStrings[0];
		_exAxis = tmpExternalStrings[1];
		_exAxisInverted = tmpExternalStrings[2];
		_exDefaults = tmpExternalStrings[3];
		_exInputs = tmpExternalStrings[4];
		_exCalibrations = tmpExternalStrings[5];
		_exCalibrationValues = tmpExternalStrings[6];
		_LoadExternalInputs();
	}

	private static void _LoadInputs() {
		if (!PlayerPrefs.HasKey("cInput_count")) { return; }
		if (PlayerPrefs.HasKey("cInput_dubl")) {
			if (PlayerPrefs.GetString("cInput_dubl") == "True") {
				allowDuplicates = true;
			} else {
				allowDuplicates = false;
			}
		}

		int _count = PlayerPrefs.GetInt("cInput_count");
		_inputLength = _count - 1;

		string _defaults = PlayerPrefs.GetString("cInput_defaults");
		string[] ar_defs = _defaults.Split('#');
		string[] ar_defName = ar_defs[0].Split('*');
		string[] ar_defPrime = ar_defs[1].Split('*');
		string[] ar_defSec = ar_defs[2].Split('*');
		string[] ar_modPrime = ar_defs[3].Split('*');
		string[] ar_modSec = ar_defs[4].Split('*');

		for (int n = 0; n < ar_defName.Length - 1; n++) {
			_SetDefaultKey(n, ar_defName[n], ar_defPrime[n], ar_defSec[n], ar_modPrime[n], ar_modSec[n]);
		}

		if (PlayerPrefs.HasKey("cInput_inp")) {
			string _descr = PlayerPrefs.GetString("cInput_descr");
			string _inp = PlayerPrefs.GetString("cInput_inp");
			string _alt_inp = PlayerPrefs.GetString("cInput_alt_inp");
			string _inpStr = PlayerPrefs.GetString("cInput_inpStr");
			string _alt_inpStr = PlayerPrefs.GetString("cInput_alt_inpStr");
			string _modifierStr = PlayerPrefs.GetString("cInput_modifierStr");
			string _alt_modifierStr = PlayerPrefs.GetString("cInput_alt_modifierStr");

			string[] ar_descr = _descr.Split('*');
			string[] ar_inp = _inp.Split('*');
			string[] ar_alt_inp = _alt_inp.Split('*');
			string[] ar_inpStr = _inpStr.Split('*');
			string[] ar_alt_inpStr = _alt_inpStr.Split('*');
			string[] ar_modifierStr = _modifierStr.Split('*');
			string[] ar_alt_modifierStr = _alt_modifierStr.Split('*');

			for (int n = 0; n < ar_descr.Length - 1; n++) {
				if (ar_descr[n] == _defaultStrings[n, 0]) {
					_inputName[n] = ar_descr[n];
					_inputPrimary[n] = _ConvertString2Key(ar_inp[n]);
					_inputSecondary[n] = _ConvertString2Key(ar_alt_inp[n]);
					_axisPrimary[n] = ar_inpStr[n];
					_axisSecondary[n] = ar_alt_inpStr[n];
					_modifierUsedPrimary[n] = _ConvertString2Key(ar_modifierStr[n]);
					_modifierUsedSecondary[n] = _ConvertString2Key(ar_alt_modifierStr[n]);
				}
			}

			// fixes inputs when defaults are being changed
			for (int m = 0; m < ar_defName.Length - 1; m++) {
				for (int n = 0; n < ar_descr.Length - 1; n++) {
					if (ar_descr[n] == _defaultStrings[m, 0]) {
						_inputName[m] = ar_descr[n];
						_inputPrimary[m] = _ConvertString2Key(ar_inp[n]);
						_inputSecondary[m] = _ConvertString2Key(ar_alt_inp[n]);
						_axisPrimary[m] = ar_inpStr[n];
						_axisSecondary[m] = ar_alt_inpStr[n];
						_modifierUsedPrimary[n] = _ConvertString2Key(ar_modifierStr[n]);
						_modifierUsedSecondary[n] = _ConvertString2Key(ar_alt_modifierStr[n]);
					}
				}
			}
		}

		if (PlayerPrefs.HasKey("cInput_axis")) {

			string _invAx = PlayerPrefs.GetString("cInput_axInv");
			string[] _axInv = _invAx.Split('*');
			string _ax = PlayerPrefs.GetString("cInput_axis");

			string[] _axis = _ax.Split('#');
			string[] _axName = _axis[0].Split('*');
			string[] _axNeg = _axis[1].Split('*');
			string[] _axPos = _axis[2].Split('*');

			int _axCount = int.Parse(_axis[3]);
			for (int n = 0; n < _axCount; n++) {
				int _neg = int.Parse(_axNeg[n]);
				int _pos = int.Parse(_axPos[n]);
				_SetAxis(n, _axName[n], _neg, _pos);
				if (_axInv[n] == "True") {
					_invertAxis[n] = true;
				} else {
					_invertAxis[n] = false;
				}
			}
		}

		if (PlayerPrefs.HasKey("cInput_indAxSens")) {
			string _tmpAxisSens = PlayerPrefs.GetString("cInput_indAxSens");
			string[] _arrAxisSens = _tmpAxisSens.Split('*');
			for (int n = 0; n < _arrAxisSens.Length - 1; n++) {
				_individualAxisSens[n] = float.Parse(_arrAxisSens[n]);
			}
		}

		if (PlayerPrefs.HasKey("cInput_indAxGrav")) {
			string _tmpAxisGrav = PlayerPrefs.GetString("cInput_indAxGrav");
			string[] _arrAxisGrav = _tmpAxisGrav.Split('*');
			for (int n = 0; n < _arrAxisGrav.Length - 1; n++) {
				_individualAxisGrav[n] = float.Parse(_arrAxisGrav[n]);
			}
		}

		if (PlayerPrefs.HasKey("cInput_indAxDead")) {
			string _tmpAxisDead = PlayerPrefs.GetString("cInput_indAxDead");
			string[] _arrAxisDead = _tmpAxisDead.Split('*');
			for (int n = 0; n < _arrAxisDead.Length - 1; n++) {
				_individualAxisDead[n] = float.Parse(_arrAxisDead[n]);
			}
		}

		// calibration loading
		if (PlayerPrefs.HasKey("cInput_saveCals")) {
			string _saveCals = PlayerPrefs.GetString("cInput_saveCals");
			string[] _saveCalsArr = _saveCals.Split('*');
			for (int n = 0; n < _saveCalsArr.Length - 1; n++) {
				_axisType[n] = int.Parse(_saveCalsArr[n]);
			}
		}

		if (PlayerPrefs.HasKey("cInput_calsVals")) {
			string _calsVals = PlayerPrefs.GetString("cInput_calsVals");
			_CalibrationValuesFromString(_calsVals);
		}

		_cKeysLoaded = true;
	}

	private static void _LoadExternalInputs() {
		_externalSaving = true;
		// splitting the external strings
		string[] externalStringAxes = _exAxis.Split('¿');
		string[] externalStringDefaults = _exDefaults.Split('¿');
		string[] externalStringInputs = _exInputs.Split('¿');

		allowDuplicates = (_exAllowDuplicates == "True") ? true : false;

		int _count = int.Parse(externalStringDefaults[0]);
		_inputLength = _count - 1;

		string _defaults = externalStringDefaults[1];
		string[] ar_defs = _defaults.Split('#');
		string[] ar_defName = ar_defs[0].Split('*');
		string[] ar_defPrime = ar_defs[1].Split('*');
		string[] ar_defSec = ar_defs[2].Split('*');
		string[] ar_modPrime = ar_defs[3].Split('*');
		string[] ar_modSec = ar_defs[4].Split('*');

		for (int n = 0; n < ar_defName.Length - 1; n++) {
			_SetDefaultKey(n, ar_defName[n], ar_defPrime[n], ar_defSec[n], ar_modPrime[n], ar_modSec[n]);
		}

		if (!string.IsNullOrEmpty(externalStringInputs[0])) {
			string _descr = externalStringInputs[0];
			string _inp = externalStringInputs[1];
			string _alt_inp = externalStringInputs[2];
			string _inpStr = externalStringInputs[3];
			string _alt_inpStr = externalStringInputs[4];
			string _modifierStr = externalStringInputs[5];
			string _alt_modifierStr = externalStringInputs[6];

			string[] ar_descr = _descr.Split('*');
			string[] ar_inp = _inp.Split('*');
			string[] ar_alt_inp = _alt_inp.Split('*');
			string[] ar_inpStr = _inpStr.Split('*');
			string[] ar_alt_inpStr = _alt_inpStr.Split('*');
			string[] ar_modifierStr = _modifierStr.Split('*');
			string[] ar_alt_modifierStr = _alt_modifierStr.Split('*');

			for (int n = 0; n < ar_descr.Length - 1; n++) {
				if (ar_descr[n] == _defaultStrings[n, 0]) {
					_inputName[n] = ar_descr[n];
					_inputPrimary[n] = _ConvertString2Key(ar_inp[n]);
					_inputSecondary[n] = _ConvertString2Key(ar_alt_inp[n]);
					_axisPrimary[n] = ar_inpStr[n];
					_axisSecondary[n] = ar_alt_inpStr[n];
					_modifierUsedPrimary[n] = _ConvertString2Key(ar_modifierStr[n]);
					_modifierUsedSecondary[n] = _ConvertString2Key(ar_alt_modifierStr[n]);
				}
			}

			// fixes inputs when defaults are being changed
			for (int m = 0; m < ar_defName.Length - 1; m++) {
				for (int n = 0; n < ar_descr.Length - 1; n++) {
					if (ar_descr[n] == _defaultStrings[m, 0]) {
						_inputName[m] = ar_descr[n];
						_inputPrimary[m] = _ConvertString2Key(ar_inp[n]);
						_inputSecondary[m] = _ConvertString2Key(ar_alt_inp[n]);
						_axisPrimary[m] = ar_inpStr[n];
						_axisSecondary[m] = ar_alt_inpStr[n];
						_modifierUsedPrimary[n] = _ConvertString2Key(ar_modifierStr[n]);
						_modifierUsedSecondary[n] = _ConvertString2Key(ar_alt_modifierStr[n]);
					}
				}
			}
		}

		if (!string.IsNullOrEmpty(externalStringAxes[0])) {

			string _invAx = _exAxisInverted;
			string[] _axInv = _invAx.Split('*');
			string _ax = externalStringAxes[0];

			string[] _axis = _ax.Split('#');
			string[] _axName = _axis[0].Split('*');
			string[] _axNeg = _axis[1].Split('*');
			string[] _axPos = _axis[2].Split('*');

			int _axCount = int.Parse(_axis[3]);
			for (int n = 0; n < _axCount; n++) {
				int _neg = int.Parse(_axNeg[n]);
				int _pos = int.Parse(_axPos[n]);
				_SetAxis(n, _axName[n], _neg, _pos);
				if (_axInv[n] == "true") {
					_invertAxis[n] = true;
				} else {
					_invertAxis[n] = false;
				}
			}
		}

		if (!string.IsNullOrEmpty(externalStringAxes[1])) {
			string _tmpAxisSens = externalStringAxes[1];
			string[] _arrAxisSens = _tmpAxisSens.Split('*');
			for (int n = 0; n < _arrAxisSens.Length - 1; n++) {
				_individualAxisSens[n] = float.Parse(_arrAxisSens[n]);
			}
		}

		if (!string.IsNullOrEmpty(externalStringAxes[2])) {
			string _tmpAxisGrav = externalStringAxes[2];
			string[] _arrAxisGrav = _tmpAxisGrav.Split('*');
			for (int n = 0; n < _arrAxisGrav.Length - 1; n++) {
				_individualAxisGrav[n] = float.Parse(_arrAxisGrav[n]);
			}
		}

		if (!string.IsNullOrEmpty(externalStringAxes[3])) {
			string _tmpAxisDead = externalStringAxes[3];
			string[] _arrAxisDead = _tmpAxisDead.Split('*');
			for (int n = 0; n < _arrAxisDead.Length - 1; n++) {
				_individualAxisDead[n] = float.Parse(_arrAxisDead[n]);
			}
		}

		// calibration loading
		if (!string.IsNullOrEmpty(_exCalibrations)) {
			string _saveCals = _exCalibrations;
			string[] _saveCalsArr = _saveCals.Split('*');
			for (int n = 1; n <= _saveCalsArr.Length - 2; n++) {
				_axisType[n] = int.Parse(_saveCalsArr[n]);
			}
		}

		if (!string.IsNullOrEmpty(_exCalibrationValues)) {
			_CalibrationValuesFromString(_exCalibrationValues);
		}

		_cKeysLoaded = true;
	}

	public static void ResetInputs() {
		_cInputInit(); // if cInput doesn't exist, create it
		// reset inputs to default values
		for (int n = 0; n < _inputLength + 1; n++) {
			_SetKey(n, _defaultStrings[n, 0], _defaultStrings[n, 1], _defaultStrings[n, 2]);
			_modifierUsedPrimary[n] = _ConvertString2Key(_defaultStrings[n, 3]);
			_modifierUsedSecondary[n] = _ConvertString2Key(_defaultStrings[n, 4]);
		}

		for (int n = 0; n < _axisLength; n++) {
			_invertAxis[n] = false;
		}

		Clear();
		_SaveDefaults();
		_SaveInputs();
		_SaveAxInverted();
	}

	public static void Clear() {
		_cInputInit(); // if cInput doesn't exist, create it
		Debug.LogWarning("Clearing out all cInput related values from PlayerPrefs");
		PlayerPrefs.DeleteKey("cInput_axInv");
		PlayerPrefs.DeleteKey("cInput_axis");
		PlayerPrefs.DeleteKey("cInput_indAxSens");
		PlayerPrefs.DeleteKey("cInput_indAxGrav");
		PlayerPrefs.DeleteKey("cInput_indAxDead");
		PlayerPrefs.DeleteKey("cInput_count");
		PlayerPrefs.DeleteKey("cInput_defaults");
		PlayerPrefs.DeleteKey("cInput_descr");
		PlayerPrefs.DeleteKey("cInput_inp");
		PlayerPrefs.DeleteKey("cInput_alt_inp");
		PlayerPrefs.DeleteKey("cInput_inpStr");
		PlayerPrefs.DeleteKey("cInput_alt_inpStr");
		PlayerPrefs.DeleteKey("cInput_dubl");
		PlayerPrefs.DeleteKey("cInput_saveCals");
		PlayerPrefs.DeleteKey("cInput_calsVals");
		PlayerPrefs.DeleteKey("cInput_modifierStr");
		PlayerPrefs.DeleteKey("cInput_alt_modifierStr");
	}

	#endregion //Save, Load, Reset & Clear functions

	#region InvertAxis and IsAxisInverted functions

	// this sets the inversion of axisName to invertedStatus
	public static bool AxisInverted(string axisName, bool invertedStatus) {
		_cInputInit(); // if cInput doesn't exist, create it
		int index = _FindAxisByDescription(axisName);
		if (index > -1) {
			_invertAxis[index] = invertedStatus;
			_SaveAxInverted();
			return invertedStatus;
		}

		// if we got this far then the string didn't match and there's a problem.
		Debug.LogWarning("Couldn't find an axis match for " + axisName + " while trying to set inversion status. Is it possible you typed it wrong?");
		return false;
	}

	// this just returns inversion status of axisName
	public static bool AxisInverted(string axisName) {
		_cInputInit(); // if cInput doesn't exist, create it
		int index = _FindAxisByDescription(axisName);
		if (index > -1) {
			return _invertAxis[index];
		}

		// if we got this far then the string didn't match and there's a problem.
		Debug.LogWarning("Couldn't find an axis match for " + axisName + " while trying to get inversion status. Is it possible you typed it wrong?");
		return false;
	}

	#endregion

	#region ShowMenu functions

	public static bool ShowMenu() {
		_cInputInit(); // if cInput doesn't exist, create it
		Debug.LogError("cInput.ShowMenu() has been deprecated. Please use the appropriate cGUI variable, such as cGUI.showingAnyGUI");
		return false;
	}

	#region overloaded ShowMenu functions

	public static void ShowMenu(bool state) {
		ShowMenu(state, _menuHeaderString, _menuActionsString, _menuInputsString, _menuButtonsString);
	}

	public static void ShowMenu(bool state, string menuHeader) {
		ShowMenu(state, menuHeader, _menuActionsString, _menuInputsString, _menuButtonsString);
	}

	public static void ShowMenu(bool state, string menuHeader, string menuActions) {
		ShowMenu(state, menuHeader, menuActions, _menuInputsString, _menuButtonsString);
	}

	public static void ShowMenu(bool state, string menuHeader, string menuActions, string menuInputs) {
		ShowMenu(state, menuHeader, menuActions, menuInputs, _menuButtonsString);
	}

	#endregion overloaded showMenu functions

	// this is an old method of showing the menu, it's just in for backwards compatibility - 
	public static void ShowMenu(bool state, string menuHeader, string menuActions, string menuInputs, string menuButtons) {
		_cInputInit(); // if cInput doesn't exist, create it
		Debug.LogError("cInput.ShowMenu() has been deprecated. Please use the appropriate cGUI function, such as cGUI.ToggleGUI()");
	}

	#endregion

	private static void _cInputInit() {
		if (_cObject == null) {
			GameObject cObject;
			if (GameObject.Find("cObject")) {
				// GameObject named cObject already exists
				cObject = GameObject.Find("cObject");
			} else {
				// We need to create a GameObject named cObject
				cObject = new GameObject();
				cObject.name = "cObject";
			}

			// make sure the GameObject also has the cInput component attached
			if (cObject.GetComponent<cInput>() == null) {
				_cObject = cObject.AddComponent<cInput>();
			}

#if Use_cInputGUI

			// make sure the GameObject also has the cInputGUI component attached
		

#endif

		}
	}

	private void _CheckingDuplicates(int _num, int _count) {
		if (allowDuplicates) { return; }

		for (int n = 0; n < length; n++) {
			if (_count == 1) {
				if (_num != n && _inputPrimary[_num] == _inputPrimary[n] && _modifierUsedPrimary[_num] == _modifierUsedPrimary[n]) {
					_inputPrimary[n] = KeyCode.None;
				}

				if (_inputPrimary[_num] == _inputSecondary[n] && _modifierUsedPrimary[_num] == _modifierUsedSecondary[n]) {
					_inputSecondary[n] = KeyCode.None;
				}
			}

			if (_count == 2) {
				if (_inputSecondary[_num] == _inputPrimary[n] && _modifierUsedSecondary[_num] == _modifierUsedPrimary[n]) {
					_inputPrimary[n] = KeyCode.None;
				}

				if (_num != n && _inputSecondary[_num] == _inputSecondary[n] && _modifierUsedSecondary[_num] == _modifierUsedSecondary[n]) {
					_inputSecondary[n] = KeyCode.None;
				}
			}
		}
	}

	private void _CheckingDuplicateStrings(int _num, int _count) {
		if (allowDuplicates) { return; }

		for (int n = 0; n < length; n++) {
			if (_count == 1) {
				if (_num != n && _axisPrimary[_num] == _axisPrimary[n]) {
					_axisPrimary[n] = "";
					_inputPrimary[n] = KeyCode.None;
				}

				if (_axisPrimary[_num] == _axisSecondary[n]) {
					_axisSecondary[n] = "";
					_inputSecondary[n] = KeyCode.None;
				}
			}

			if (_count == 2) {
				if (_axisSecondary[_num] == _axisPrimary[n]) {
					_axisPrimary[n] = "";
					_inputPrimary[n] = KeyCode.None;
				}

				if (_num != n && _axisSecondary[_num] == _axisSecondary[n]) {
					_axisSecondary[n] = "";
					_inputSecondary[n] = KeyCode.None;
				}
			}
		}
	}

	/// <summary>This is where we detect what input is being pressed to assign inputs using the GUI</summary>
	private void _InputScans() {
		KeyCode _tmpModifier = KeyCode.None;
		if (Input.GetKey(KeyCode.Escape)) {
			if (_cScanInput == 1) {
				_inputPrimary[_cScanIndex] = KeyCode.None;
				_axisPrimary[_cScanIndex] = "";
				_cScanInput = 0;
			}

			if (_cScanInput == 2) {
				_inputSecondary[_cScanIndex] = KeyCode.None;
				_axisSecondary[_cScanIndex] = "";
				_cScanInput = 0;
			}
		}

		#region keyboard + mouse + joystick button scanning

		if (_scanning && Input.anyKeyDown && !Input.GetKey(KeyCode.Escape)) {
			KeyCode _key = KeyCode.None;

			for (int i = (int)KeyCode.None; i < 450; i++) {
				KeyCode _ckey = (KeyCode)i;
				if (_ckey.ToString().StartsWith("Mouse")) {
					if (!_allowMouseButtons) {
						continue;
					}
				} else if (_ckey.ToString().StartsWith("Joystick")) {
					if (!_allowJoystickButtons) {
						continue;
					}
				} else if (!_allowKeyboard) {
					continue;
				}

				// loop through modifier list and set the input key
				for (int n = 0; n < _modifiers.Count; n++) {
					for (int m = 0; m < _modifiers.Count; m++) {
						if (Input.GetKeyDown(_modifiers[n])) {
							return;
						}
					}

					if (Input.GetKeyDown(_ckey)) {
						_key = _ckey;
						_tmpModifier = _ckey; // if this doesn't change it means there is no modifier used to set this input
						bool markedAsAxis = false; // has this key been marked as an axis?
						for (int m = 0; m < _markedAsAxis.Count; m++) {
							if (_cScanIndex == _markedAsAxis[m]) {
								markedAsAxis = true;
								break; // no need to loop through the rest
							}
						}

						// check if modifier is been pressed and that the inputs aren't part of an axis
						if (Input.GetKey(_modifiers[n]) && !markedAsAxis) {
							_tmpModifier = _modifiers[n]; // if this is being set here it means we have a modifier being pressed down
							break;
						}
					}
				}
			}

			if (_key != KeyCode.None) {
				bool _keyCleared = true;
				// check if the entered key is forbidden
				for (int b = 0; b < _forbiddenKeys.Count; b++) {
					if (_key == _forbiddenKeys[b]) {
						_keyCleared = false;
					}
				}

				if (_keyCleared) {
					if (_cScanInput == 1) {
						_inputPrimary[_cScanIndex] = _key;
						_modifierUsedPrimary[_cScanIndex] = _tmpModifier; // set the modifier being used 
						_axisPrimary[_cScanIndex] = "";
						_CheckingDuplicates(_cScanIndex, _cScanInput);
					}

					if (_cScanInput == 2) {
						_inputSecondary[_cScanIndex] = _key;
						_modifierUsedSecondary[_cScanIndex] = _tmpModifier; // set the modifier being used
						_axisSecondary[_cScanIndex] = "";
						_CheckingDuplicates(_cScanIndex, _cScanInput);
					}
				}

				_cScanInput = 0;
			}
		}

		#region mouse scroll wheel scanning (considered to be a mousebutton)

		if (_allowMouseButtons) {
			//if (!Mathf.Approximately(_axisRawValues["Mouse Wheel"], Input.GetAxisRaw("Mouse Wheel"))) {
			if (Input.GetAxis("Mouse Wheel") > 0 && !Input.GetKey(KeyCode.Escape)) {
				if (_cScanInput == 1) {
					_axisPrimary[_cScanIndex] = "Mouse Wheel Up";
				}

				if (_cScanInput == 2) {
					_axisSecondary[_cScanIndex] = "Mouse Wheel Up";
				}

				_CheckingDuplicateStrings(_cScanIndex, _cScanInput);
				_cScanInput = 0;
			} else if (Input.GetAxis("Mouse Wheel") < 0 && !Input.GetKey(KeyCode.Escape)) {
				if (_cScanInput == 1) {
					_axisPrimary[_cScanIndex] = "Mouse Wheel Down";
				}

				if (_cScanInput == 2) {
					_axisSecondary[_cScanIndex] = "Mouse Wheel Down";
				}

				_CheckingDuplicateStrings(_cScanIndex, _cScanInput);
				_cScanInput = 0;
			}
			//}
		}

		#endregion //mouse scroll wheel scanning (considered to be a mousebutton)

		#endregion // keyboard + mouse + joystick button scanning

		#region mouse axis scanning

		if (_allowMouseAxis) {
			//if (!Mathf.Approximately(_axisRawValues["Mouse Horizontal"], Input.GetAxisRaw("Mouse Horizontal"))) {
			if (Input.GetAxis("Mouse Horizontal") < -deadzone && !Input.GetKey(KeyCode.Escape)) {

				if (_cScanInput == 1) {
					_axisPrimary[_cScanIndex] = "Mouse Left";
				}

				if (_cScanInput == 2) {
					_axisSecondary[_cScanIndex] = "Mouse Left";
				}

				_CheckingDuplicateStrings(_cScanIndex, _cScanInput);
				_cScanInput = 0;
			} else if (Input.GetAxis("Mouse Horizontal") > deadzone && !Input.GetKey(KeyCode.Escape)) {
				if (_cScanInput == 1) {
					_axisPrimary[_cScanIndex] = "Mouse Right";
				}

				if (_cScanInput == 2) {
					_axisSecondary[_cScanIndex] = "Mouse Right";
				}

				_CheckingDuplicateStrings(_cScanIndex, _cScanInput);
				_cScanInput = 0;
			}
			//}

			//if (!Mathf.Approximately(_axisRawValues["Mouse Vertical"], Input.GetAxisRaw("Mouse Vertical"))) {
			if (Input.GetAxis("Mouse Vertical") > deadzone && !Input.GetKey(KeyCode.Escape)) {
				if (_cScanInput == 1) {
					_axisPrimary[_cScanIndex] = "Mouse Up";
				}

				if (_cScanInput == 2) {
					_axisSecondary[_cScanIndex] = "Mouse Up";
				}

				_CheckingDuplicateStrings(_cScanIndex, _cScanInput);
				_cScanInput = 0;
			} else if (Input.GetAxis("Mouse Vertical") < -deadzone && !Input.GetKey(KeyCode.Escape)) {
				if (_cScanInput == 1) {
					_axisPrimary[_cScanIndex] = "Mouse Down";
				}

				if (_cScanInput == 2) {
					_axisSecondary[_cScanIndex] = "Mouse Down";
				}

				_CheckingDuplicateStrings(_cScanIndex, _cScanInput);
				_cScanInput = 0;
			}
			//}
		}

		#endregion // mouse axis scanning

		#region joystick axis scanning

		if (_allowJoystickAxis) {
			float scanningDeadzone = 0.25f;
			for (int i = 1; i <= _numGamepads; i++) {
				for (int j = 1; j <= 10; j++) {
					string _joystring = _joyStrings[i, j];
					string _joystringPos = _joyStringsPos[i, j];
					string _joystringNeg = _joyStringsNeg[i, j];

					float axisRaw = Input.GetAxisRaw(_joystring);

					if (!Mathf.Approximately(_axisRawValues[_joystring], axisRaw)) {

						#region Special Xbox gamepad trigger handling
						// Xbox triggers are bound both to axis 3 (+/-) and axes 9 (+) and 10 (+)
						// the problem with letting them bind to axis 3 is if both are pressed,
						// it returns -1 + 1 which is 0, which is the same as neither of them being pressed
						// this prevents them from being bound to the same axis, so they can both be
						// pressed without interfering with each other.
						if (j == 3) {
							// if this is the gamepad's 3rd axis we want to check if either
							// axis 9 or 10 is also returning a value

							string lTrigger = _joyStringsPos[i, 9];
							string rTrigger = _joyStringsPos[i, 10];

							// if axis 9 or 10 has a positive value above scanningDeadzone, use that axis instead of axis 3
							if (_GetCalibratedAxisInput(lTrigger) > scanningDeadzone) {
								_joystringPos = lTrigger;
								_joystringNeg = lTrigger;
							} else if (_GetCalibratedAxisInput(rTrigger) > scanningDeadzone) {
								_joystringPos = rTrigger;
								_joystringNeg = rTrigger;
							}
						}
						#endregion //Special Xbox gamepad trigger handling

						float axis = (axisRaw < 0) ? // if the raw value is negative
							_GetCalibratedAxisInput(_joystringNeg) : // axis is the calibrated input of the negative axis
							_GetCalibratedAxisInput(_joystringPos); // else it's the calibrated input of the positive axis

						if (_scanning && Mathf.Abs(axis) > scanningDeadzone && !Input.GetKey(KeyCode.Escape)) {
							//Debug.Log("Calibrated value: " + axis + ". Raw value: " + Input.GetAxisRaw(_joystring));
							if (_cScanInput == 1) {
								if (axis > scanningDeadzone) {
									_axisPrimary[_cScanIndex] = _joystringPos;
								} else if (axis < -scanningDeadzone) {
									_axisPrimary[_cScanIndex] = _joystringNeg;
								}

								_CheckingDuplicateStrings(_cScanIndex, _cScanInput);
								_cScanInput = 0;
								break;
							} else if (_cScanInput == 2) {
								if (axis > scanningDeadzone) {
									_axisSecondary[_cScanIndex] = _joystringPos;
								} else if (axis < -scanningDeadzone) {
									_axisSecondary[_cScanIndex] = _joystringNeg;
								}

								_CheckingDuplicateStrings(_cScanIndex, _cScanInput);
								_cScanInput = 0;
								break;
							}
						}
					}
				}
			}
		}

		#endregion // joystick axis scanning

	}
}
