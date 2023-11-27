using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class Camera3D : Godot.Camera3D
{
    private EventBus _eventBus;

    public override void _Ready()
    {
        _eventBus = GetNode<EventBus>("/root/EventBus");
        _eventBus.MoveCamera += OnMovement;
        _eventBus.RotateCamera += OnRotation;
    }

    private void OnRotation(Vector3 vector3)
    {
        RotationDegrees = vector3;
    }

    private void OnMovement(Vector3 vector3)
    {
        Position = vector3;
        //GlobalPosition = vector3;
    }
}