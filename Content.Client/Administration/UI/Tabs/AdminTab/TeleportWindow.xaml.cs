// SPDX-FileCopyrightText: 2021 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2021 Leo <lzimann@users.noreply.github.com>
// SPDX-FileCopyrightText: 2021 Paul <ritter.paul1+git@googlemail.com>
// SPDX-FileCopyrightText: 2021 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
// SPDX-FileCopyrightText: 2021 Visne <39844191+Visne@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 Paul Ritter <ritter.paul1@googlemail.com>
// SPDX-FileCopyrightText: 2022 mirrorcult <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2022 wrexbe <81056464+wrexbe@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Administration;
using JetBrains.Annotations;
using Robust.Client.AutoGenerated;
using Robust.Client.Console;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;

namespace Content.Client.Administration.UI.Tabs.AdminTab
{
    [GenerateTypedNameReferences]
    [UsedImplicitly]
    public sealed partial class TeleportWindow : DefaultWindow
    {
        private PlayerInfo? _selectedPlayer;

        protected override void EnteredTree()
        {
            SubmitButton.OnPressed += SubmitButtonOnOnPressed;
            PlayerList.OnSelectionChanged += OnListOnOnSelectionChanged;
        }

        private void OnListOnOnSelectionChanged(PlayerInfo? obj)
        {
            _selectedPlayer = obj;
            SubmitButton.Disabled = _selectedPlayer == null;
        }

        private void SubmitButtonOnOnPressed(BaseButton.ButtonEventArgs obj)
        {
            if (_selectedPlayer == null)
                return;
            // Execute command
            IoCManager.Resolve<IClientConsoleHost>().ExecuteCommand(
                $"tpto \"{_selectedPlayer.Username}\"");
        }
    }
}