using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
	static PlayerInput _pi;

	private void Awake()
	{
		_pi = GetComponent<PlayerInput>();
	}

	public static bool OnJump()
	{
		return _pi.actions.FindAction("Jump").WasPressedThisFrame();
	}

	public static bool MoveLeft()
	{
		return _pi.actions.FindAction("LeftMove").IsPressed();
	}

	public static bool MoveRight()
	{
		return _pi.actions.FindAction("RightMove").IsPressed();
	}

	public static bool WalkButtonPressed()
	{
		return _pi.actions.FindAction("Walk").WasPressedThisFrame();
	}

	public static bool WalkButtonReleased()
	{
		return _pi.actions.FindAction("Walk").WasReleasedThisFrame();
	}

	public static bool OnSwitch()
	{
		return _pi.actions.FindAction("Switch").WasPressedThisFrame();
	}

	public static bool OnPause()
	{
		return _pi.actions.FindAction("PauseButton").WasPressedThisFrame();
	}
}
