using UnityEngine;

public class InputManager : MonoBehaviour
{
	public PlayerInputActions playerActions;
        
	public static PlayerInput PlayerInput;
	private void Awake()
	{
		PlayerInput = new PlayerInput();
		playerActions = new PlayerInputActions();
		BuildPlayerActions();
		playerActions.Enable();
		playerActions.Player.Enable();
	}

	private void FixedUpdate()
	{
		PlayerInput.ResetJustPressedAndReleased();
	}

	private void BuildPlayerActions()
	{
		playerActions.Player.Move.performed += ctx =>
		{
			Vector2 value;
			value = PlayerInput.InputDetectionActive ? ctx.ReadValue<Vector2>() : Vector2.zero;

			if (value.x >= .5f)
				value.x = 1;
			else if (value.x <= -.5f)
				value.x = -1;
			if (value.y >= .5f)
				value.y = 1;
			else if (value.y <= -.5f)
				value.y = -1;

			if (value != PlayerInput.Movement)
			{
				PlayerInput.Movement = value;
				PlayerInput.KeysChanged = true;
			}
		};
		playerActions.Player.Glide.performed += ctx => { PlayerInput.KeysChanged = true; PlayerInput.GlideJustPressed = true; PlayerInput.GlidePressed = true; PlayerInput.GlideJustReleased = false; };
		playerActions.Player.Glide.canceled += ctx => { PlayerInput.KeysChanged = true; PlayerInput.GlideJustPressed = false; PlayerInput.GlidePressed = false; PlayerInput.GlideJustReleased = true; };
		playerActions.Player.Run.performed += ctx => { PlayerInput.KeysChanged = true; PlayerInput.RunJustPressed = true; PlayerInput.RunPressed = true; PlayerInput.RunJustReleased = false; };
		playerActions.Player.Run.canceled += ctx => { PlayerInput.KeysChanged = true; PlayerInput.RunJustPressed = false; PlayerInput.RunPressed = false; PlayerInput.RunJustReleased = true; };
		playerActions.Player.Swim.performed += ctx => { PlayerInput.KeysChanged = true; PlayerInput.SwimJustPressed = true; PlayerInput.SwimPressed = true; PlayerInput.SwimJustReleased = false; };
		playerActions.Player.Swim.canceled += ctx => { PlayerInput.KeysChanged = true; PlayerInput.SwimJustPressed = false; PlayerInput.SwimPressed = false; PlayerInput.SwimJustReleased = true; };
		playerActions.Player.Jump.performed += ctx => { PlayerInput.KeysChanged = true; PlayerInput.JumpJustPressed = true; PlayerInput.JumpPressed = true; PlayerInput.JumpJustReleased = false; };
		playerActions.Player.Jump.canceled += ctx => { PlayerInput.KeysChanged = true; PlayerInput.JumpJustPressed = false; PlayerInput.JumpPressed = false; PlayerInput.JumpJustReleased = true; };
		playerActions.Player.Interact.performed += ctx => { PlayerInput.KeysChanged = true; PlayerInput.InteractJustPressed = true; PlayerInput.InteractPressed = true; PlayerInput.InteractJustReleased = false; };
		playerActions.Player.Interact.canceled += ctx => { PlayerInput.KeysChanged = true; PlayerInput.InteractJustPressed = false; PlayerInput.InteractPressed = false; PlayerInput.InteractJustReleased = true; };
		playerActions.Player.Fly.performed += ctx => { PlayerInput.KeysChanged = true; PlayerInput.FlyJustPressed = true; PlayerInput.FlyPressed = true; PlayerInput.FlyJustReleased = false; };
		playerActions.Player.Fly.canceled += ctx => { PlayerInput.KeysChanged = true; PlayerInput.FlyJustPressed = false; PlayerInput.FlyPressed = false; PlayerInput.FlyJustReleased = true; };
		playerActions.Player.Jetpack.performed += ctx => { PlayerInput.KeysChanged = true; PlayerInput.JetpackJustPressed = true; PlayerInput.JetpackPressed = true; PlayerInput.JetpackJustReleased = false; };
		playerActions.Player.Jetpack.canceled += ctx => { PlayerInput.KeysChanged = true; PlayerInput.JetpackJustPressed = false; PlayerInput.JetpackPressed = false; PlayerInput.JetpackJustReleased = true; };
		playerActions.Player.Dash.performed += ctx => { PlayerInput.KeysChanged = true; PlayerInput.DashJustPressed = true; PlayerInput.DashPressed = true; PlayerInput.DashJustReleased = false; };
		playerActions.Player.Dash.canceled += ctx => { PlayerInput.KeysChanged = true; PlayerInput.DashJustPressed = false; PlayerInput.DashPressed = false; PlayerInput.DashJustReleased = true; };
		playerActions.Player.ToggleMiniMap.performed += ctx => { PlayerInput.ToggleMiniMapJustPressed = true; PlayerInput.ToggleMiniMapPressed = true; PlayerInput.ToggleMiniMapJustReleased = false; };
		playerActions.Player.ToggleMiniMap.canceled += ctx => { PlayerInput.ToggleMiniMapJustPressed = false; PlayerInput.ToggleMiniMapPressed = false; PlayerInput.ToggleMiniMapJustReleased = true; };
		playerActions.Player.ToggleRightPanel.performed += ctx => { PlayerInput.ToggleRightLightingPanelUIJustPressed = true; PlayerInput.ToggleRightLightingPanelUIPressed = true; PlayerInput.ToggleRightLightingPanelUIJustReleased = false; };
		playerActions.Player.ToggleRightPanel.canceled += ctx => { PlayerInput.ToggleRightLightingPanelUIJustPressed = false; PlayerInput.ToggleRightLightingPanelUIPressed = false; PlayerInput.ToggleRightLightingPanelUIJustReleased = true; };
		playerActions.Player.ToggleBlockWindow.performed += ctx => { PlayerInput.ToggleBlockWindowJustPressed = true; PlayerInput.ToggleBlockWindowPressed = true; PlayerInput.ToggleBlockWindowJustReleased = false; };
		playerActions.Player.ToggleBlockWindow.canceled += ctx => { PlayerInput.ToggleBlockWindowJustPressed = false; PlayerInput.ToggleBlockWindowPressed = false; PlayerInput.ToggleBlockWindowJustReleased = true; };
		playerActions.Player.TogglePersonalLighting.performed += ctx => { PlayerInput.TogglePersonalLightingJustPressed = true; PlayerInput.TogglePersonalLightingPressed = true; PlayerInput.TogglePersonalLightingJustReleased = false; };
		playerActions.Player.TogglePersonalLighting.canceled += ctx => { PlayerInput.TogglePersonalLightingJustPressed = false; PlayerInput.TogglePersonalLightingPressed = false; PlayerInput.TogglePersonalLightingJustReleased = true; };

	}
}