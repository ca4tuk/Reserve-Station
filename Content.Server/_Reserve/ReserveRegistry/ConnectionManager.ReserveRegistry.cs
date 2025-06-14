// SPDX-FileCopyrightText: 2025 ReserveBot <211949879+ReserveBot@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 сачтик <ca4tuk@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Threading.Tasks;
using Content.Server._Reserve.ReserveRegistry;
using Content.Shared._Reserve.CCCVars;
using Robust.Shared.Configuration;
using Robust.Shared.Network;

namespace Content.Server._Reserve.Connection
{
    public sealed class ConnectionManager : IPostInjectInit, IDisposable
    {
        [Dependency] private readonly IConfigurationManager _cfg = default!;
        [Dependency] private readonly ILogManager _logManager = default!;

        private ISawmill _sawmill = default!;

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


        public async Task<ReserveRegistryChecker.RegistryBanData?> QueryReserveRegistryAsync(NetUserData user)
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
