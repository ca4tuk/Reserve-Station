// SPDX-FileCopyrightText: 2025 сачтик <ca4tuk@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Threading.Tasks;
using Content.Server.ReserveRegistry;
using Content.Shared._Reserve.CCCVars;
using Robust.Shared.Network;

namespace Content.Server.Connection
{
    public sealed partial class ConnectionManager : IPostInjectInit, IDisposable
    {
        private ReserveRegistryChecker? _registryChecker;


        private void InitializeReserveRegistry()
        {
            var url = _cfg.GetCVar(CCCVars.ReserveRegistryUrl);
            var token = _cfg.GetCVar(CCCVars.ReserveRegistryApiToken);
            _registryChecker = new ReserveRegistryChecker(url, token);
        }

        private async Task<ReserveRegistryChecker.RegistryBanData?> QueryReserveRegistryAsync(NetUserData user)
        {
            if (!_cfg.GetCVar(CCCVars.ReserveRegistryEnabled) || _registryChecker == null)
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
        }

        public void PostInject()
        {
            _sawmill = _logManager.GetSawmill("Connection");
            InitializeReserveRegistry();
        }
    }
}
