using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class Player : CharacterBody3D
{
    private Camera3D _camera3D;
    private Node3D _head;

    [ExportCategory("Movement")]
    [Export] private float _moveSpeed = 5f;
    [Export] private float _jumpForce = 5f;
    private float _gravity = 9.8f;

    [ExportCategory("View Rotation")]
    [Export] private float _lookSensibility = 0.5f;
    [Export(PropertyHint.Range, "-85f,-1f,1f")] private float _minXRotation = -85f;
    [Export(PropertyHint.Range, "1f,85f,1f")] private float _maxXRotation = 85f;

    private Vector2 _mouseDirection;
    private EventBus _eventBus;

    public override void _Ready()
    {
        _eventBus = GetNode<EventBus>("/root/EventBus");
        _head = GetNode<Node3D>("Head");
    }
    
    public override void _Process(double delta)
    {
        GD.PrintErr(_head.GlobalPosition);
        _eventBus.EmitSignal(EventBus.SignalName.MoveCamera, _head.GlobalPosition);
    }
    
    private void _Input()
    {
        
    }

}