using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class Camera3D : Godot.Camera3D
{
    private EventBus _eventBus;

    public override void _Ready()
    {
        _eventBus = GetNode<EventBus>("/root/EventBus");
        _eventBus.CameraMove += OnMovement;
        _eventBus.CameraRotate += OnCameraRotate;
    }

    private void OnCameraRotate(Vector3 vector3)
    {
        RotationDegrees = vector3;
    }

    private void OnMovement(Vector3 vector3)
    {
        Position = vector3;
    }
}