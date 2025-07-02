using Content.Shared.ShipGun;
using Robust.Client.UserInterface;

namespace Content.Client.ShipGun;

public sealed class ShipGunConsoleBoundUserInterface : BoundUserInterface
{
    private UIBox2? _placeholder;

    public ShipGunConsoleBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();
        _placeholder = new UIBox2();
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
    }
}
