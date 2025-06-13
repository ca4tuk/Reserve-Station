using System.Threading.Tasks;
using Content.Server.ReserveRegistry;
using Content.Shared._Reserve.CCCVars;
using Robust.Shared.Network;

namespace Content.Server.Connection
{
    public sealed partial class ConnectionManager : IPostInjectInit, IDisposable
    {
        private ReserveRegistryChecker? _registryChecker;
        private bool _enabled;
        private string _token = string.Empty;
        private string _url = string.Empty;

        private void InitializeReserveRegistry()
        {
            _cfg.OnValueChanged(CCCVars.ReserveRegistryEnabled, UpdateState, true);
            _cfg.OnValueChanged(CCCVars.ReserveRegistryApiToken, UpdateToken, true);
            _cfg.OnValueChanged(CCCVars.ReserveRegistryUrl, UpdateUrl, true);
            _registryChecker = new ReserveRegistryChecker(_url, _token);
        }

        private void UpdateToken(string token)
        {
            _token = token;
            _registryChecker = new ReserveRegistryChecker(_url, _token);
        }

        private void UpdateUrl(string url)
        {
            _url = url;
            _registryChecker = new ReserveRegistryChecker(_url, _token);
        }

        private void UpdateState(bool state)
        {
            _enabled = state;
        }


        private async Task<ReserveRegistryChecker.RegistryBanData?> QueryReserveRegistryAsync(NetUserData user)
        {
            if (!_enabled || _registryChecker == null)
                return null;

            try
            {
                var data = await _registryChecker.CheckBanAsync(Guid.Parse(user.UserId.ToString()));
                return data;
            }
            catch (Exception ex)
            {
                _sawmill.Warning($"[Reserve Registry] Ошибка при проверке " +
                                 $"{user.UserName} ({user.UserId}): {ex.Message}");
                return null;
            }
        }

        public void Dispose()
        {
            _registryChecker?.Dispose();
            _cfg.UnsubValueChanged(CCCVars.ReserveRegistryEnabled, UpdateState);
            _cfg.UnsubValueChanged(CCCVars.ReserveRegistryUrl, UpdateUrl);
            _cfg.UnsubValueChanged(CCCVars.ReserveRegistryApiToken, UpdateToken);
        }

        public void PostInject()
        {
            _sawmill = _logManager.GetSawmill("Connection");
            InitializeReserveRegistry();
        }
    }
}
