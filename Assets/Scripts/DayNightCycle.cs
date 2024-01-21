using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class DayNightCycle : Node3D
{
    [ExportCategory("Day in seconds")]
    [Export] private float _dayLength  = 20f;
    [Export] private float _startTime = 0.5f;

    private float _time;
    private float _timeRate;
    private DirectionalLight3D _sun;
    private DirectionalLight3D _moon;
    
    private WorldEnvironment _environment;

    [ExportCategory("Sun Light")]
    [Export]
    private Gradient _sunColor;
    [Export]
    private Curve _sunIntensity;
    
    [ExportCategory("Moon Light")]
    [Export]
    private Gradient _moonColor;
    [Export]
    private Curve _moonIntensity;
    
    [ExportCategory("Sky")]
    [Export]
    private Gradient _skyTopColor;
    [Export]
    private Gradient _skyHorizonColor;
    
    

    public override void _Ready()
    {
        _timeRate = 1f / _dayLength;
        _time = _startTime;
        _sun = GetNode<DirectionalLight3D>("Sun");
        _moon = GetNode<DirectionalLight3D>("Moon");
        _environment = GetNode<WorldEnvironment>("WorldEnvironment");
    }

    public override void _Process(double delta)
    {
        _time += _timeRate * (float)delta;
        if (_time >= 1f)
            _time = 0f;
        // sun
        var rotation = _sun.RotationDegrees;
        rotation.X = _time * 360 + 90;
        _sun.RotationDegrees = rotation;
        _sun.LightColor = _sunColor.Sample(_time);
        _sun.LightEnergy = _sunIntensity.Sample(_time);

        // moon
        var moonRotation = _moon.RotationDegrees;
        moonRotation.X = _time * 360 + 270;
        _moon.RotationDegrees = moonRotation;
        _moon.LightColor = _moonColor.Sample(_time);
        _moon.LightEnergy = _moonIntensity.Sample(_time);

        _sun.Visible = _sun.LightEnergy > 0;
        _moon.Visible = _moon.LightEnergy > 0;
        
        // sky
        // sky_top_color and sky_horizon_color are reserved words
        _environment.Environment.Sky.SkyMaterial.Set("sky_top_color", _skyTopColor.Sample(_time));
        _environment.Environment.Sky.SkyMaterial.Set("sky_horizon_color", _skyHorizonColor.Sample(_time));
        // ground 
        // ground_bottom_color, ground_horizon_color same
        _environment.Environment.Sky.SkyMaterial.Set("ground_bottom_color", _skyTopColor.Sample(_time));
        _environment.Environment.Sky.SkyMaterial.Set("ground_horizon_color", _skyHorizonColor.Sample(_time));
    }
}