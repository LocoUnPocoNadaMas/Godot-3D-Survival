using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class Camera3D : Godot.Camera3D
{
    private EventBus _eventBus;

    public override void _Ready()
    {
        _eventBus = GetNode<EventBus>("/root/EventBus");
        _eventBus.MoveCamera += OnMovement;
    }

    private void OnMovement(Vector3 vector3)
    {
        GD.PrintErr(vector3);
        Position = vector3;
        //GlobalPosition = vector3;
    }
}