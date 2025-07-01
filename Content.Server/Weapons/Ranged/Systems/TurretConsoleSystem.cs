using Content.Shared.Interaction.Events;
using Content.Shared.Weapons.Ranged.Components;
using Content.Shared.Interaction;
using Content.Server.Mind;

namespace Content.Server.Weapons.Ranged.Systems;

/// <summary>
/// Handles players remotely controlling turrets via consoles.
/// </summary>
public sealed class TurretConsoleSystem : EntitySystem
{
    [Dependency] private readonly SharedInteractionSystem _interaction = default!;
    [Dependency] private readonly MindSystem _mind = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<TurretConsoleComponent, InteractHandEvent>(OnInteract);
        SubscribeLocalEvent<TurretConsoleComponent, ComponentShutdown>(OnShutdown);
    }

    private void OnInteract(EntityUid uid, TurretConsoleComponent comp, InteractHandEvent args)
    {
        if (args.Handled)
            return;

        args.Handled = true;

        if (!_interaction.InRangeUnobstructed(args.User, uid))
            return;

        // If already controlling, release control.
        if (comp.Controller == args.User)
        {
            if (_mind.TryGetMind(args.User, out var mindId, out var mind))
            {
                _mind.UnVisit(mindId, mind);
                comp.Controller = null;
            }
            return;
        }

        // Already in use by another player.
        if (comp.Controller != null)
            return;

        if (!_mind.TryGetMind(args.User, out var mindId2, out var mind2))
            return;

        if (!EntityManager.EntityExists(comp.Turret))
            return;

        comp.Controller = args.User;
        _mind.Visit(mindId2, comp.Turret, mind2);
    }

    private void OnShutdown(EntityUid uid, TurretConsoleComponent comp, ComponentShutdown args)
    {
        if (comp.Controller == null)
            return;

        if (_mind.TryGetMind(comp.Controller.Value, out var mindId, out var mind) && mind.VisitingEntity == comp.Turret)
        {
            _mind.UnVisit(mindId, mind);
        }
    }
}
