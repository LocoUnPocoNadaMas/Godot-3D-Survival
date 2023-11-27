using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class EventBus : Node
{
    [Signal]
    public delegate void MoveCameraEventHandler(Vector3 vector3);
}