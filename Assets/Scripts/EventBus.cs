using Godot;

namespace GodotSurvival.Assets.Scripts;

public partial class EventBus : Node
{
    [Signal]
    public delegate void CameraMoveEventHandler(Vector3 vector3);
    
    [Signal]
    public delegate void CameraRotateEventHandler(Vector3 vector3);
}