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
    [Export(PropertyHint.Range, "-1f,0f,0.1f")]
    private float _lookSensibility = -0.5f;

    [Export(PropertyHint.Range, "-85f,-1f,1f")]
    private float _minXRotation = -85f;

    [Export(PropertyHint.Range, "1f,85f,1f")]
    private float _maxXRotation = 85f;

    private Vector2 _mouseDirection;
    private EventBus _eventBus;

    public override void _Ready()
    {
        _eventBus = GetNode<EventBus>("/root/EventBus");
        _head = GetNode<Node3D>("Head");
        // Never, Never use Confined
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }
    
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseEvent)
        {
            
            var camRotation = _head.RotationDegrees;
            // Up and Down
            camRotation.X += mouseEvent.Relative.Y * _lookSensibility;
            camRotation.X = Mathf.Clamp(camRotation.X, _minXRotation, _maxXRotation);
            // Left and Right
            camRotation.Y += mouseEvent.Relative.X * _lookSensibility;
            
            _head.RotationDegrees = camRotation;
            
            _eventBus.EmitSignal(EventBus.SignalName.CameraRotate, camRotation);
            
        }
    }

    public override void _Process(double delta)
    {
        _eventBus.EmitSignal(EventBus.SignalName.CameraMove, _head.GlobalPosition);
    }
    
    public override void _PhysicsProcess(double delta)
    {
        if (!IsOnFloor())
        {
            var vel = Velocity;
            vel.Y -= _gravity * (float)delta;
            Velocity = vel;
        }
        else
        {
            if (Input.IsActionPressed("Jump"))
            {
                var vel = Velocity;
                vel.Y = _jumpForce;
                Velocity = vel;
            }
        }

        var input = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        
        var direction = _head.Basis.Z * input.Y + _head.Basis.X * input.X;
        // avoid slowing down when looking down
        direction.Y = 0;
        direction = direction.Normalized();
        
        // another approach but slowing down
        // var direction = (_head.Transform.Basis * new Vector3(input.X, 0, input.Y)).Normalized();

        var velocity = Velocity;
        velocity.X = direction.X * _moveSpeed;
        velocity.Z = direction.Z * _moveSpeed;
        Velocity = velocity;
        
        MoveAndSlide();
    }
    
}