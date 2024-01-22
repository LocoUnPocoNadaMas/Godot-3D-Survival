using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class Need : Node
{
    private float _value;
    [Export] private float _maxValue;
    [Export] private float _startValue;
    [Export] private float _regenRate;
    [Export] private float _decayRate;
    private NeedBar _needBar;

    public float Value => _value;
    public float DecayRate => _decayRate;
    public float RegenRate => _regenRate;


    public override void _Ready()
    {
        _needBar = GetNode<NeedBar>("NeedBar");
        _value = _startValue;
    }

    public void Add(float amount)
    {
        _value = Value + amount;
        if (Value > _maxValue)
            _value = _maxValue;
    }

    public void Subtract(float amount)
    {
        _value = Value - amount;
        if (Value < 0)
            _value = 0;
    }

    public void UpdateProgressBar()
    {
        _needBar.UpdateValue(Value, _maxValue);
    }
}