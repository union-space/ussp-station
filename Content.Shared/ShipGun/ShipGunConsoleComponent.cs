using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

namespace Content.Shared.ShipGun;

/// <summary>
/// Component for a console controlling a ship gun.
/// Stores reference to the gun entity being controlled.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class ShipGunConsoleComponent : Component
{
    /// <summary>
    /// The gun this console is linked to.
    /// </summary>
    [DataField("gun")]
    public EntityUid? Gun;
}

[Serializable, NetSerializable]
public sealed class ShipGunConsoleInterfaceState : BoundUserInterfaceState
{
    public readonly float Angle;
    public readonly int Distance;

    public ShipGunConsoleInterfaceState(float angle, int distance)
    {
        Angle = angle;
        Distance = distance;
    }
}

[Serializable, NetSerializable]
public enum ShipGunConsoleAction : byte
{
    RotateLeft,
    RotateRight,
    Fire,
    Reload,
    SetDistance1,
    SetDistance2,
    SetDistance3,
}

[Serializable, NetSerializable]
public sealed class ShipGunConsoleActionMessage : BoundUserInterfaceMessage
{
    public readonly ShipGunConsoleAction Action;

    public ShipGunConsoleActionMessage(ShipGunConsoleAction action)
    {
        Action = action;
    }
}

[Serializable, NetSerializable]
public enum ShipGunConsoleUiKey : byte
{
    Key
}
