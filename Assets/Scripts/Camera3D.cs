using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class Camera3D : Godot.Camera3D
{
    private EventBus _eventBus;

    [ExportCategory("View Rotation")] [Export]
    private float _lookSensibility = -0.5f;

    [Export(PropertyHint.Range, "-85f,-1f,1f")]
    private float _minXRotation = -85f;

    [Export(PropertyHint.Range, "1f,85f,1f")]
    private float _maxXRotation = 85f;

    public override void _Ready()
    {
        _eventBus = GetNode<EventBus>("/root/EventBus");
        _eventBus.MoveCamera += OnMovement;
        _eventBus.RotateCamera += OnRotation;
    }

    private void OnRotation(Vector2 vector2)
    {
        var camRotation = RotationDegrees;
        // Up and Down
        camRotation.X += vector2.Y * _lookSensibility;
        camRotation.X = Mathf.Clamp(camRotation.X, _minXRotation, _maxXRotation);
        // Left and Right
        camRotation.Y += vector2.X * _lookSensibility;
        RotationDegrees = camRotation;
    }

    private void OnMovement(Vector3 vector3)
    {
        Position = vector3;
        //GlobalPosition = vector3;
    }
}