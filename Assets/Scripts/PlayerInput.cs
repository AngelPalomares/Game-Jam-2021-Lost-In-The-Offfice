using UnityEngine;

public class PlayerInput
{
    public Vector2 Threshold = new Vector2(0.1f, 0.4f);

    public bool InputDetectionActive = true;

    public Vector2 Movement;
    public bool JumpPressed = false;
    public bool SwimPressed = false;
    public bool GlidePressed = false;
    public bool InteractPressed = false;
    public bool JetpackPressed = false;
    public bool RunPressed = false;
    public bool DashPressed = false;
    public bool FlyPressed = false;
    public bool ShootPressed = false;
    public bool ReloadPressed = false;
    public bool SwitchWeaponPressed = false;
    public bool PushPressed = false;
    public bool ToggleMiniMapPressed = false;
    public bool ToggleRightLightingPanelUIPressed = false;
    public bool ToggleBlockWindowPressed = false;
    public bool TogglePersonalLightingPressed = false;

    public bool JumpJustPressed = false;
    public bool SwimJustPressed = false;
    public bool GlideJustPressed = false;
    public bool InteractJustPressed = false;
    public bool JetpackJustPressed = false;
    public bool RunJustPressed = false;
    public bool DashJustPressed = false;
    public bool FlyJustPressed = false;
    public bool ShootJustPressed = false;
    public bool ReloadJustPressed = false;
    public bool SwitchWeaponJustPressed = false;
    public bool PushJustPressed = false;
    public bool ToggleMiniMapJustPressed = false;
    public bool ToggleRightLightingPanelUIJustPressed = false;
    public bool ToggleBlockWindowJustPressed = false;
    public bool TogglePersonalLightingJustPressed = false;

    public bool JumpJustReleased = false;
    public bool SwimJustReleased = false;
    public bool GlideJustReleased = false;
    public bool InteractJustReleased = false;
    public bool JetpackJustReleased = false;
    public bool RunJustReleased = false;
    public bool DashJustReleased = false;
    public bool FlyJustReleased = false;
    public bool ShootJustReleased = false;
    public bool ReloadJustReleased = false;
    public bool SwitchWeaponJustReleased = false;
    public bool PushJustReleased = false;
    public bool ToggleMiniMapJustReleased = false;
    public bool ToggleRightLightingPanelUIJustReleased = false;
    public bool ToggleBlockWindowJustReleased = false;
    public bool TogglePersonalLightingJustReleased = false;
        
        
        
    public bool KeysChanged = false;

    public void ResetJustPressedAndReleased()
    {
        //Pressed
        JumpJustPressed = false;
        SwimJustPressed = false;
        GlideJustPressed = false;
        InteractJustPressed = false;
        JetpackJustPressed = false;
        RunJustPressed = false;
        DashJustPressed = false;
        FlyJustPressed = false;
        ShootJustPressed = false;
        ReloadJustPressed = false;
        SwitchWeaponJustPressed = false;
        PushJustPressed = false;
        ToggleMiniMapJustPressed = false;
        ToggleRightLightingPanelUIJustPressed = false;
        ToggleBlockWindowJustPressed = false;
        TogglePersonalLightingJustPressed = false;

        //Released
        JumpJustReleased = false;
        SwimJustReleased = false;
        GlideJustReleased = false;
        InteractJustReleased = false;
        JetpackJustReleased = false;
        RunJustReleased = false;
        DashJustReleased = false;
        FlyJustReleased = false;
        ShootJustReleased = false;
        ReloadJustReleased = false;
        SwitchWeaponJustReleased = false;
        PushJustReleased = false;
        ToggleMiniMapJustReleased = false;
        ToggleRightLightingPanelUIJustReleased = false;
        ToggleBlockWindowJustReleased = false;
        TogglePersonalLightingJustReleased = false;
            
        KeysChanged = false;
    }
}