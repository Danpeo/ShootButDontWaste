using Godot;
using Platformer.Scripts.Constants;

namespace Platformer.Scripts;

public class World
{
    public static float GetGravity() => 
        ProjectSettings.GetSetting(SettingConstant.Gravity).AsSingle();
}