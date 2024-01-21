using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class DayNightCycle : Node3D
{
    [Export] private float _dayLength  = 20f;
    [Export] private float _startTime = 0.3f;

    private float _time;
    private float _timeRate;
    private DirectionalLight3D _sun;
    
    
}