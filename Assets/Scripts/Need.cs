using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class Need : Node
{
    private float _value;
    [Export] private float _maxValue;
    [Export] private float _startValue;
    [Export] private float _regenValue;
    [Export] private float _decayRate;
    private NeedBar _needBar;

    public override void _Ready()
    {
        _needBar = GetNode<NeedBar>("NeedBar");
    }

    private void Add(float amount)
    {
        _value += amount;
        if (_value > _maxValue)
            _value = _maxValue;
    }

    private void Subtract(float amount)
    {
        _value -= amount;
        if (_value < 0)
            _value = 0;
    }

    private void UpdateProgressBar()
    {
        _needBar.UpdateValue(_value, _maxValue);
    }
}