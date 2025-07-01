using Robust.Shared.GameStates;

namespace Content.Shared.Weapons.Ranged.Components;

/// <summary>
/// Added to a console that allows remotely controlling a turret.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class TurretConsoleComponent : Component
{
    /// <summary>
    /// The turret entity that this console controls.
    /// </summary>
    [DataField("turret", required: true)]
    public EntityUid Turret;

    /// <summary>
    /// Current entity controlling the turret via this console.
    /// </summary>
    [DataField, ViewVariables]
    public EntityUid? Controller;
}
