using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using System.Text;

namespace SteamIDInfo;

public class Constants
{
    public const string PluginName = "SteamID Info";
    public const string PluginVersion = "1.0.0";
}

public class SteamIDInfo : BasePlugin
{
    public override string ModuleName => Constants.PluginName;

    public override string ModuleVersion => Constants.PluginVersion;

    [ConsoleCommand("steamid_info", "Displays information about the client: SteamID2, SteamID3, SteamID64.")]
    [CommandHelper(minArgs: 1, usage: "[target]", whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
    public void OnCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            return;
        }

        var output = new StringBuilder();
        output.AppendLine("========");
        output.AppendLine("id | name | steamid2 | steamid3 | steamid64");

        var targets = Utilities.ProcessTargetString(command.ArgByIndex(1), player);
        foreach (var target in targets)
        {
            output.AppendLine(GetPlayerInfo(target));
        }

        output.AppendLine("========");

        player.PrintToConsole(output.ToString());
    }

    private string GetPlayerInfo(CCSPlayerController player)
    {
        var authorizedSteamID = player.AuthorizedSteamID;
        return $"{player.UserId} | {player.PlayerName} | {authorizedSteamID?.SteamId2} | {authorizedSteamID?.SteamId3} | {authorizedSteamID?.SteamId64}";
    }
}
