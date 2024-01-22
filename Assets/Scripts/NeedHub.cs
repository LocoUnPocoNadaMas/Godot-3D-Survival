using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class NeedHub : Control
{
    private Need _health;
    private Need _thirst;
    private Need _hunger;
    private Need _sleep;
    
    [Export] private float _noThirstHealthDecay;
    [Export] private float _noHungerHealthDecay;

    public override void _Ready()
    {
        _health = GetNode<Need>("Health");
        _thirst = GetNode<Need>("Thirst");
        _hunger = GetNode<Need>("Hunger");
        _sleep = GetNode<Need>("Sleep");
    }

    public override void _Process(double delta)
    {
        _thirst.Subtract(_thirst.DecayRate * (float)delta);
        _hunger.Subtract(_hunger.DecayRate * (float)delta);
        _sleep.Add(_sleep.RegenRate * (float)delta);

        if (_thirst.Value <= 0f)
            _health.Subtract(_noThirstHealthDecay * (float)delta);
        if (_hunger.Value <= 0f)
            _health.Subtract(_noHungerHealthDecay * (float)delta);
        
        _health.UpdateProgressBar();
        _hunger.UpdateProgressBar();
        _thirst.UpdateProgressBar();
        _sleep.UpdateProgressBar();
    }
}