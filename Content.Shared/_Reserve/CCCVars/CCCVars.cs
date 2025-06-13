using Robust.Shared.Configuration;

namespace Content.Shared._Reserve.CCCVars;

/// <summary>
///     Reserve modules console variables
/// </summary>
[CVarDefs]
// ReSharper disable once InconsistentNaming
public sealed class CCCVars
{
    /// <summary>
    /// Making everyone a pacifist at the end of a round.
    /// </summary>
    public static readonly CVarDef<bool> PeacefulRoundEnd =
        CVarDef.Create("game.peaceful_end", true, CVar.SERVERONLY);

    /// <summary>
    /// Переключатель статуса активности системы Reserve Registry
    /// </summary>
    public static readonly CVarDef<bool> ReserveRegistryEnabled =
        CVarDef.Create("reregistry.enabled", true, CVar.SERVERONLY);

    /// <summary>
    /// Переключатель скипа администрации. Если игрок администратор - не проверять его через реестр
    /// </summary>
    public static readonly CVarDef<bool> ReserveRegistrySkipAdmins =
        CVarDef.Create("reregistry.skip_admins", true, CVar.SERVERONLY | CVar.CONFIDENTIAL);

    /// <summary>
    /// Базовый URL API Reserve Registry
    /// </summary>
    public static readonly CVarDef<string> ReserveRegistryUrl =
        CVarDef.Create("reregistry.url", "", CVar.SERVERONLY | CVar.CONFIDENTIAL);

    /// <summary>
    /// API токен Reserve Registry
    /// </summary>
    public static readonly CVarDef<string> ReserveRegistryApiToken =
        CVarDef.Create("reregistry.api_token", "", CVar.SERVERONLY | CVar.CONFIDENTIAL);
}
