using Content.Shared.ShipGun;
using Content.Shared.Weapons.Ranged.Components;
using Content.Shared.Weapons.Ranged.Systems;
using Robust.Server.GameObjects;
using Robust.Shared.GameStates;
using Robust.Shared.Player;

namespace Content.Server.ShipGun;

/// <summary>
/// System for handling ship gun console interactions.
/// </summary>
public sealed class ShipGunConsoleSystem : EntitySystem
{
    [Dependency] private readonly UserInterfaceSystem _ui = default!;
    [Dependency] private readonly SharedGunSystem _gun = default!;
    [Dependency] private readonly TransformSystem _transform = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<ShipGunConsoleComponent, BoundUIOpenedEvent>(OnUiOpened);
        SubscribeLocalEvent<ShipGunConsoleComponent, ShipGunConsoleActionMessage>(OnAction);
    }

    private void OnUiOpened(EntityUid uid, ShipGunConsoleComponent component, BoundUIOpenedEvent args)
    {
        var state = new ShipGunConsoleInterfaceState(0f, 1);
        _ui.SetUiState(uid, ShipGunConsoleUiKey.Key, state);
    }

    private void OnAction(EntityUid uid, ShipGunConsoleComponent component, ShipGunConsoleActionMessage msg)
    {
        if (component.Gun == null)
            return;

        var gunUid = component.Gun.Value;
        if (!TryComp<ShipGunComponent>(gunUid, out var gunComp))
            return;

        switch (msg.Action)
        {
            case ShipGunConsoleAction.RotateLeft:
                gunComp.Angle -= 15f;
                _transform.SetLocalRotation(gunUid, Angle.FromDegrees(gunComp.Angle));
                break;
            case ShipGunConsoleAction.RotateRight:
                gunComp.Angle += 15f;
                _transform.SetLocalRotation(gunUid, Angle.FromDegrees(gunComp.Angle));
                break;
            case ShipGunConsoleAction.Fire:
                if (gunComp.NextFire > Timing.CurTime)
                    break;
                if (TryComp<GunComponent>(gunUid, out var gun))
                {
                    _gun.AttemptShoot(gunUid, gun);
                    gunComp.NextFire = Timing.CurTime + TimeSpan.FromSeconds(gunComp.Cooldown);
                }
                break;
            case ShipGunConsoleAction.Reload:
                if (TryComp<BallisticAmmoProviderComponent>(gunUid, out var provider))
                {
                    _gun.BallisticAmmoCockGun(uid, gunUid, provider);
                }
                break;
            case ShipGunConsoleAction.SetDistance1:
                gunComp.Distance = 1;
                break;
            case ShipGunConsoleAction.SetDistance2:
                gunComp.Distance = 2;
                break;
            case ShipGunConsoleAction.SetDistance3:
                gunComp.Distance = 3;
                break;
        }
    }
}
