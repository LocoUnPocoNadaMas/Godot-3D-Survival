using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class NeedBar : ProgressBar
{
    private float _value;
    private float _maxValue;
    [Export] private Color _color;

    public override void _Ready()
    {
        var sb = new StyleBoxFlat();
        AddThemeStyleboxOverride("fill", sb);
        sb.BgColor = new Color(_color);
    }


    public void UpdateValue(float newValue, float max)
    {
        MaxValue = max;
        Value = newValue;
    }
}