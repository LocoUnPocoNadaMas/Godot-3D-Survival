using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class Player : CharacterBody3D
{
    private Camera3D _camera3D;
    private Node3D _head;

    [ExportCategory("Movement")] [Export] private float _moveSpeed = 5f;
    [Export] private float _jumpForce = 5f;
    private float _gravity = 9.8f;

    private Vector2 _mouseDirection;
    private EventBus _eventBus;

    public override void _Ready()
    {
        _eventBus = GetNode<EventBus>("/root/EventBus");
        _head = GetNode<Node3D>("Head");
    }

    public override void _Process(double delta)
    {
        _eventBus.EmitSignal(EventBus.SignalName.MoveCamera, _head.GlobalPosition);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseEvent)
        {
            // Increase Camera Rotation
            _eventBus.EmitSignal(EventBus.SignalName.RotateCamera, mouseEvent.Relative);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        var input = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        var velocity = Velocity;
        velocity.X = input.X * _moveSpeed;
        velocity.Z = input.Y * _moveSpeed;
        Velocity = velocity;
        MoveAndSlide();
    }
}