using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class DayNightCycle : Node3D
{
    [ExportCategory("In seconds")]
    [Export] private float _dayLength  = 20f;
    [Export] private float _startTime = 0.5f;

    private float _time;
    private float _timeRate;
    private DirectionalLight3D _sun;

    [ExportCategory("Mixed")]
    [Export]
    private Gradient _sunColor;
    [Export]
    private Curve _sunIntensity;

    public override void _Ready()
    {
        _timeRate = 1f / _dayLength;
        _time = _startTime;
        _sun = GetNode<DirectionalLight3D>("Sun");
    }

    public override void _Process(double delta)
    {
        _time += _timeRate * (float)delta;
        if (_time >= 1f)
            _time = 0f;
        var rotation = _sun.RotationDegrees;
        rotation.X = _time * 360 + 90;
        _sun.RotationDegrees = rotation;
        _sun.LightColor = _sunColor.Sample(_time);
    }
}