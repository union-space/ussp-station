using Robust.Shared.GameStates;

namespace Content.Server.ShipGun;

/// <summary>
/// Component for a ship mounted gun controlled via a console.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class ShipGunComponent : Component
{
    /// <summary>
    /// Current rotation angle in degrees.
    /// </summary>
    [ViewVariables]
    public float Angle;

    /// <summary>
    /// Current targeting distance level (1-3).
    /// </summary>
    [ViewVariables]
    public int Distance = 1;

    /// <summary>
    /// Next time the gun can fire.
    /// </summary>
    public TimeSpan NextFire;

    [DataField("cooldown")]
    public float Cooldown = 1f;
}
