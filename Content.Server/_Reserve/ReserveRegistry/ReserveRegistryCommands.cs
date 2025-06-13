using Content.Server.Administration;
using Content.Server.Database;
using Content.Shared.Administration;
using Robust.Shared.Console;

namespace Content.Server.Connection.ReserveRegistry;

[AdminCommand(AdminFlags.Ban)]
public sealed class AddIgnoreListCommand : LocalizedCommands
{
    [Dependency] private readonly IPlayerLocator _playerLocator = default!;
    [Dependency] private readonly IServerDbManager _db = default!;

    public override string Command => "ignorelistadd";

    public override async void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (args.Length == 0)
        {
            shell.WriteError(Loc.GetString("shell-need-minimum-one-argument"));
            shell.WriteLine(Help);
            return;
        }

        if (args.Length > 1)
        {
            shell.WriteError(Loc.GetString("shell-need-exactly-one-argument"));
            shell.WriteLine(Help);
            return;
        }

        var name = args[0];
        var data = await _playerLocator.LookupIdByNameAsync(name);

        if (data == null)
        {
            shell.WriteError(Loc.GetString("cmd-ignorelistadd-not-found", ("username", args[0])));
            return;
        }

        var guid = data.UserId;
        var isIgnored = await _db.GetIgnoreListStatusAsync(guid);
        if (isIgnored)
        {
            shell.WriteLine(Loc.GetString("cmd-ignorelistadd-existing", ("username", data.Username)));
            return;
        }

        await _db.AddToIgnoreListAsync(guid);
        shell.WriteLine(Loc.GetString("cmd-ignorelistadd-added", ("username", data.Username)));
    }

    public override CompletionResult GetCompletion(IConsoleShell shell, string[] args)
    {
        if (args.Length == 1)
        {
            return CompletionResult.FromHint(Loc.GetString("cmd-ignorelistadd-arg-player"));
        }

        return CompletionResult.Empty;
    }
}

[AdminCommand(AdminFlags.Ban)]
public sealed class RemoveIgnoreListCommand : LocalizedCommands
{
    [Dependency] private readonly IPlayerLocator _playerLocator = default!;
    [Dependency] private readonly IServerDbManager _db = default!;

    public override string Command => "ignorelistremove";

    public override async void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (args.Length == 0)
        {
            shell.WriteError(Loc.GetString("shell-need-minimum-one-argument"));
            shell.WriteLine(Help);
            return;
        }

        if (args.Length > 1)
        {
            shell.WriteError(Loc.GetString("shell-need-exactly-one-argument"));
            shell.WriteLine(Help);
            return;
        }

        var name = args[0];
        var data = await _playerLocator.LookupIdByNameAsync(name);

        if (data == null)
        {
            shell.WriteError(Loc.GetString("cmd-ignorelistremove-not-found", ("username", args[0])));
            return;
        }

        var guid = data.UserId;
        var isIgnored = await _db.GetIgnoreListStatusAsync(guid);
        if (!isIgnored)
        {
            shell.WriteLine(Loc.GetString("cmd-ignorelistremove-existing", ("username", data.Username)));
            return;
        }

        await _db.RemoveFromIgnoreListAsync(guid);
        shell.WriteLine(Loc.GetString("cmd-ignorelistremove-removed", ("username", data.Username)));
    }

    public override CompletionResult GetCompletion(IConsoleShell shell, string[] args)
    {
        if (args.Length == 1)
        {
            return CompletionResult.FromHint(Loc.GetString("cmd-ignorelistremove-arg-player"));
        }

        return CompletionResult.Empty;
    }
}
