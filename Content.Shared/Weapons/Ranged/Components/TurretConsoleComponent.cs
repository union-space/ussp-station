using Robust.Shared.GameStates;

namespace Content.Shared.Weapons.Ranged.Components;

/// <summary>
/// Added to a console that allows remotely controlling a turret.
/// </summary>

[RegisterComponent, NetworkedComponent]
public sealed partial class TurretConsoleComponent : Component
{
    public const string TurretPort = "TurretControl";

    /// <summary>
    /// The turret entity that this console controls.
    /// Set when linked via <see cref="TurretPort"/>.
    /// </summary>
    [ViewVariables]
    public EntityUid? Turret;

    /// <summary>
    /// Current entity controlling the turret via this console.
    /// </summary>
    [DataField, ViewVariables]
    public EntityUid? Controller;
}
