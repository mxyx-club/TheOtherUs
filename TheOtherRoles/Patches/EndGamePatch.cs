using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheOtherRoles.CustomGameModes;
using TheOtherRoles.Modules;
using TheOtherRoles.Utilities;
using UnityEngine;
using static TheOtherRoles.TheOtherRoles;

namespace TheOtherRoles.Patches;

enum CustomGameOverReason
{
    LoversWin = 10,
    TeamJackalWin,
    MiniLose,
    JesterWin,
    ArsonistWin,
    JuggernautWin,
    VultureWin,
    ProsecutorWin,
    DoomsayerWin,
    WerewolfWin
}

enum WinCondition
{
    Default,
    EveryoneDied,
    LoversTeamWin,
    LoversSoloWin,
    JesterWin,
    JackalWin,
    MiniLose,
    ArsonistWin,
    VultureWin,
    AdditionalLawyerBonusWin,
    AdditionalAlivePursuerWin,
    ProsecutorWin,
    DoomsayerWin,
    WerewolfWin,
    JuggernautWin,
}

static class AdditionalTempData
{
    // Should be implemented using a proper GameOverReason in the future
    public static WinCondition winCondition = WinCondition.Default;
    public static List<WinCondition> additionalWinConditions = new();
    public static List<PlayerRoleInfo> playerRoles = new();
    public static float timer;

    public static void clear()
    {
        playerRoles.Clear();
        additionalWinConditions.Clear();
        winCondition = WinCondition.Default;
        timer = 0;
    }

    internal class PlayerRoleInfo
    {
        public string PlayerName { get; set; }
        public List<RoleInfo> Roles { get; set; }
        public string RoleNames { get; set; }
        public int TasksCompleted { get; set; }
        public int TasksTotal { get; set; }
        public bool IsGuesser { get; set; }
        public int? Kills { get; set; }
        public bool IsAlive { get; set; }
    }
}


[HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
public static class OnGameEndPatch
{
    public static GameOverReason gameOverReason = GameOverReason.HumansByTask;
    public static void Prefix(AmongUsClient __instance, [HarmonyArgument(0)] ref EndGameResult endGameResult)
    {
        gameOverReason = endGameResult.GameOverReason;
        if ((int)endGameResult.GameOverReason >= 10) endGameResult.GameOverReason = GameOverReason.ImpostorByKill;

        // Reset zoomed out ghosts
        toggleZoom(reset: true);
    }

    public static void Postfix(AmongUsClient __instance, [HarmonyArgument(0)] ref EndGameResult endGameResult)
    {
        AdditionalTempData.clear();

        foreach (var playerControl in PlayerControl.AllPlayerControls)
        {
            var roles = RoleInfo.getRoleInfoForPlayer(playerControl);
            var (tasksCompleted, tasksTotal) = TasksHandler.taskInfo(playerControl.Data);
            bool isGuesser = HandleGuesser.isGuesserGm && HandleGuesser.isGuesser(playerControl.PlayerId);
            int? killCount = GameHistory.deadPlayers.FindAll(x => x.killerIfExisting != null && x.killerIfExisting.PlayerId == playerControl.PlayerId).Count;
            if (killCount == 0 && !(new List<RoleInfo>() { RoleInfo.sheriff, RoleInfo.jackal, RoleInfo.juggernaut, RoleInfo.sidekick, RoleInfo.thief }.Contains(RoleInfo.getRoleInfoForPlayer(playerControl, false).FirstOrDefault()) || playerControl.Data.Role.IsImpostor))
            {
                killCount = null;
            }
            string roleString = RoleInfo.GetRolesString(playerControl, true, true, false);
            AdditionalTempData.playerRoles.Add(new AdditionalTempData.PlayerRoleInfo() { PlayerName = playerControl.Data.PlayerName, Roles = roles, RoleNames = roleString, TasksTotal = tasksTotal, TasksCompleted = tasksCompleted, IsGuesser = isGuesser, Kills = killCount, IsAlive = !playerControl.Data.IsDead });

            if (Cultist.isCultistGame)
            {
                GameOptionsManager.Instance.currentNormalGameOptions.NumImpostors = 2;
            }
        }

        // Remove Jester, Arsonist, Vulture, Jackal, former Jackals and Sidekick from winners (if they win, they'll be readded)
        List<PlayerControl> notWinners = new();
        if (Jester.jester != null) notWinners.Add(Jester.jester);
        if (Sidekick.sidekick != null) notWinners.Add(Sidekick.sidekick);
        if (Amnisiac.amnisiac != null) notWinners.Add(Amnisiac.amnisiac);
        if (Jackal.jackal != null) notWinners.Add(Jackal.jackal);
        if (Arsonist.arsonist != null) notWinners.Add(Arsonist.arsonist);
        if (Vulture.vulture != null) notWinners.Add(Vulture.vulture);
        if (Werewolf.werewolf != null) notWinners.Add(Werewolf.werewolf);
        if (Juggernaut.juggernaut != null) notWinners.Add(Juggernaut.juggernaut);
        if (Lawyer.lawyer != null) notWinners.Add(Lawyer.lawyer);
        if (Pursuer.pursuer != null) notWinners.Add(Pursuer.pursuer);
        if (Thief.thief != null) notWinners.Add(Thief.thief);
        if (Doomsayer.doomsayer != null) notWinners.Add(Doomsayer.doomsayer);

        notWinners.AddRange(Jackal.formerJackals);

        List<CachedPlayerData> winnersToRemove = new();
        foreach (CachedPlayerData winner in EndGameResult.CachedWinners.GetFastEnumerator())
        {
            if (notWinners.Any(x => x.Data.PlayerName == winner.PlayerName)) winnersToRemove.Add(winner);
        }
        foreach (var winner in winnersToRemove) EndGameResult.CachedWinners.Remove(winner);

        var everyoneDead = AdditionalTempData.playerRoles.All(x => !x.IsAlive);
        bool jesterWin = Jester.jester != null && gameOverReason == (GameOverReason)CustomGameOverReason.JesterWin;
        bool werewolfWin = gameOverReason == (GameOverReason)CustomGameOverReason.WerewolfWin && Werewolf.werewolf != null && !Werewolf.werewolf.Data.IsDead;
        bool arsonistWin = Arsonist.arsonist != null && gameOverReason == (GameOverReason)CustomGameOverReason.ArsonistWin;
        bool miniLose = Mini.mini != null && gameOverReason == (GameOverReason)CustomGameOverReason.MiniLose;
        bool juggernautWin = gameOverReason == (GameOverReason)CustomGameOverReason.JuggernautWin && Juggernaut.juggernaut != null && !Juggernaut.juggernaut.Data.IsDead;
        bool loversWin = Lovers.existingAndAlive() && (gameOverReason == (GameOverReason)CustomGameOverReason.LoversWin || (GameManager.Instance.DidHumansWin(gameOverReason) && !Lovers.existingWithKiller())); // Either they win if they are among the last 3 players, or they win if they are both Crewmates and both alive and the Crew wins (Team Imp/Jackal Lovers can only win solo wins)
        bool teamJackalWin = gameOverReason == (GameOverReason)CustomGameOverReason.TeamJackalWin && ((Jackal.jackal != null && !Jackal.jackal.Data.IsDead) || (Sidekick.sidekick != null && !Sidekick.sidekick.Data.IsDead));
        bool vultureWin = Vulture.vulture != null && gameOverReason == (GameOverReason)CustomGameOverReason.VultureWin;
        bool prosecutorWin = Lawyer.lawyer != null && gameOverReason == (GameOverReason)CustomGameOverReason.ProsecutorWin;
        bool doomsayerWin = Doomsayer.doomsayer != null && gameOverReason == (GameOverReason)CustomGameOverReason.DoomsayerWin;

        bool isPursurerLose = jesterWin || arsonistWin || miniLose || vultureWin || teamJackalWin;

        // Mini lose
        if (miniLose)
        {
            EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
            CachedPlayerData wpd = new(Mini.mini.Data);
            wpd.IsYou = false; // If "no one is the Mini", it will display the Mini, but also show defeat to everyone
            EndGameResult.CachedWinners.Add(wpd);
            AdditionalTempData.winCondition = WinCondition.MiniLose;
        }

        // Everyone Died
        else if (everyoneDead)
        {
            EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
            AdditionalTempData.winCondition = WinCondition.EveryoneDied;
        }

        // Jester win
        else if (jesterWin)
        {
            EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
            CachedPlayerData wpd = new(Jester.jester.Data);
            EndGameResult.CachedWinners.Add(wpd);
            AdditionalTempData.winCondition = WinCondition.JesterWin;
        }

        // Arsonist win
        else if (arsonistWin)
        {
            EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
            CachedPlayerData wpd = new(Arsonist.arsonist.Data);
            EndGameResult.CachedWinners.Add(wpd);
            AdditionalTempData.winCondition = WinCondition.ArsonistWin;
        }

        // Vulture win
        else if (vultureWin)
        {
            EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
            CachedPlayerData wpd = new(Vulture.vulture.Data);
            EndGameResult.CachedWinners.Add(wpd);
            AdditionalTempData.winCondition = WinCondition.VultureWin;
        }

        // Jester win
        else if (prosecutorWin)
        {
            EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
            CachedPlayerData wpd = new(Lawyer.lawyer.Data);
            EndGameResult.CachedWinners.Add(wpd);
            AdditionalTempData.winCondition = WinCondition.ProsecutorWin;
        }

        else if (doomsayerWin)
        {
            // DoomsayerWin wins if nobody except jackal is alive
            AdditionalTempData.winCondition = WinCondition.DoomsayerWin;
            EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
            var wpd = new CachedPlayerData(Doomsayer.doomsayer.Data);
            EndGameResult.CachedWinners.Add(wpd);
            AdditionalTempData.winCondition = WinCondition.DoomsayerWin;
        }

        // Lovers win conditions
        else if (loversWin)
        {
            // Double win for lovers, crewmates also win
            if (!Lovers.existingWithKiller())
            {
                AdditionalTempData.winCondition = WinCondition.LoversTeamWin;
                EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                {
                    if (p == null) continue;
                    if (p == Lovers.lover1 || p == Lovers.lover2)
                        EndGameResult.CachedWinners.Add(new CachedPlayerData(p.Data));
                    else if (p == Pursuer.pursuer && !Pursuer.pursuer.Data.IsDead)
                        EndGameResult.CachedWinners.Add(new CachedPlayerData(p.Data));
                    else if (p != Jester.jester
                        && p != Jackal.jackal
                        && p != Werewolf.werewolf
                        && p != Doomsayer.doomsayer
                        && p != Juggernaut.juggernaut
                        && p != Sidekick.sidekick
                        && p != Arsonist.arsonist
                        && p != Vulture.vulture
                        && !Jackal.formerJackals.Contains(p)
                        && !p.Data.Role.IsImpostor)
                        EndGameResult.CachedWinners.Add(new CachedPlayerData(p.Data));
                }
            }
            // Lovers solo win
            else
            {
                AdditionalTempData.winCondition = WinCondition.LoversSoloWin;
                EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                EndGameResult.CachedWinners.Add(new CachedPlayerData(Lovers.lover1.Data));
                EndGameResult.CachedWinners.Add(new CachedPlayerData(Lovers.lover2.Data));
            }
        }

        // Jackal win condition (should be implemented using a proper GameOverReason in the future)
        else if (teamJackalWin)
        {
            // Jackal wins if nobody except jackal is alive
            AdditionalTempData.winCondition = WinCondition.JackalWin;
            EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
            CachedPlayerData wpd = new(Jackal.jackal.Data);
            wpd.IsImpostor = false;
            EndGameResult.CachedWinners.Add(wpd);
            // If there is a sidekick. The sidekick also wins
            if (Sidekick.sidekick != null)
            {
                CachedPlayerData wpdSidekick = new(Sidekick.sidekick.Data);
                wpdSidekick.IsImpostor = false;
                EndGameResult.CachedWinners.Add(wpdSidekick);
            }
            foreach (var player in Jackal.formerJackals)
            {
                CachedPlayerData wpdFormerJackal = new(player.Data);
                wpdFormerJackal.IsImpostor = false;
                EndGameResult.CachedWinners.Add(wpdFormerJackal);
            }
        }

        else if (werewolfWin)
        {
            // Werewolf wins if nobody except jackal is alive
            AdditionalTempData.winCondition = WinCondition.WerewolfWin;
            EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
            CachedPlayerData wpd = new(Werewolf.werewolf.Data);
            wpd.IsImpostor = false;
            EndGameResult.CachedWinners.Add(wpd);
        }

        else if (juggernautWin)
        {
            // JuggernautWin wins if nobody except jackal is alive
            AdditionalTempData.winCondition = WinCondition.JuggernautWin;
            EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
            var wpd = new CachedPlayerData(Juggernaut.juggernaut.Data);
            wpd.IsImpostor = false;
            EndGameResult.CachedWinners.Add(wpd);
        }

        // Possible Additional winner: Lawyer
        if (Lawyer.lawyer != null && Lawyer.target != null && (!Lawyer.target.Data.IsDead || Lawyer.target == Jester.jester) && !Pursuer.notAckedExiled && !Lawyer.isProsecutor)
        {
            CachedPlayerData winningClient = null;
            foreach (CachedPlayerData winner in EndGameResult.CachedWinners.GetFastEnumerator())
            {
                if (winner.PlayerName == Lawyer.target.Data.PlayerName)
                    winningClient = winner;
            }
            if (winningClient != null)
            { // The Lawyer wins if the client is winning (and alive, but if he wasn't the Lawyer shouldn't exist anymore)
                if (!EndGameResult.CachedWinners.ToArray().Any(x => x.PlayerName == Lawyer.lawyer.Data.PlayerName))
                    EndGameResult.CachedWinners.Add(new CachedPlayerData(Lawyer.lawyer.Data));
                AdditionalTempData.additionalWinConditions.Add(WinCondition.AdditionalLawyerBonusWin); // The Lawyer wins together with the client
            }
        }

        // Possible Additional winner: Pursuer
        if (Pursuer.pursuer != null && !Pursuer.pursuer.Data.IsDead && !Pursuer.notAckedExiled)
        {
            if (!EndGameResult.CachedWinners.ToArray().Any(x => x.PlayerName == Pursuer.pursuer.Data.PlayerName))
                EndGameResult.CachedWinners.Add(new CachedPlayerData(Pursuer.pursuer.Data));
            AdditionalTempData.additionalWinConditions.Add(WinCondition.AdditionalAlivePursuerWin);
        }

        AdditionalTempData.timer = ((float)(DateTime.UtcNow - (HideNSeek.isHideNSeekGM ? HideNSeek.startTime : PropHunt.startTime)).TotalMilliseconds) / 1000;

        // Reset Settings
        if (TORMapOptions.gameMode == CustomGamemodes.HideNSeek) ShipStatusPatch.resetVanillaSettings();
        RPCProcedure.resetVariables();
        EventUtility.gameEndsUpdate();
    }
}

[HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
public class EndGameManagerSetUpPatch
{
    public static void Postfix(EndGameManager __instance)
    {
        // Delete and readd PoolablePlayers always showing the name and role of the player
        foreach (PoolablePlayer pb in __instance.transform.GetComponentsInChildren<PoolablePlayer>())
        {
            UnityEngine.Object.Destroy(pb.gameObject);
        }
        int num = Mathf.CeilToInt(7.5f);
        List<CachedPlayerData> list = EndGameResult.CachedWinners.ToArray().ToList().OrderBy(delegate (CachedPlayerData b)
        {
            if (!b.IsYou)
            {
                return 0;
            }
            return -1;
        }).ToList();
        for (int i = 0; i < list.Count; i++)
        {
            CachedPlayerData winningPlayerData2 = list[i];
            int num2 = (i % 2 == 0) ? -1 : 1;
            int num3 = (i + 1) / 2;
            float num4 = num3 / (float)num;
            float num5 = Mathf.Lerp(1f, 0.75f, num4);
            float num6 = (i == 0) ? -8 : -1;
            PoolablePlayer poolablePlayer = UnityEngine.Object.Instantiate(__instance.PlayerPrefab, __instance.transform);
            poolablePlayer.transform.localPosition = new Vector3(1f * num2 * num3 * num5, FloatRange.SpreadToEdges(-1.125f, 0f, num3, num), num6 + (num3 * 0.01f)) * 0.9f;
            float num7 = Mathf.Lerp(1f, 0.65f, num4) * 0.9f;
            Vector3 vector = new(num7, num7, 1f);
            poolablePlayer.transform.localScale = vector;
            if (winningPlayerData2.IsDead)
            {
                poolablePlayer.SetBodyAsGhost();
                poolablePlayer.SetDeadFlipX(i % 2 == 0);
            }
            else
            {
                poolablePlayer.SetFlipX(i % 2 == 0);
            }
            poolablePlayer.UpdateFromPlayerOutfit(winningPlayerData2.Outfit, PlayerMaterial.MaskType.None, winningPlayerData2.IsDead, true);

            poolablePlayer.cosmetics.nameText.color = Color.white;
            poolablePlayer.cosmetics.nameText.transform.localScale = new Vector3(1f / vector.x, 1f / vector.y, 1f / vector.z);
            poolablePlayer.cosmetics.nameText.transform.localPosition = new Vector3(poolablePlayer.cosmetics.nameText.transform.localPosition.x, poolablePlayer.cosmetics.nameText.transform.localPosition.y, -15f);
            poolablePlayer.cosmetics.nameText.text = winningPlayerData2.PlayerName;

            foreach (var data in AdditionalTempData.playerRoles)
            {
                if (data.PlayerName != winningPlayerData2.PlayerName) continue;
                var roles =
                poolablePlayer.cosmetics.nameText.text += $"\n{string.Join("\n", data.Roles.Select(x => cs(x.color, x.name)))}";
            }
        }

        // Additional code
        GameObject bonusText = UnityEngine.Object.Instantiate(__instance.WinText.gameObject);
        bonusText.transform.position = new Vector3(__instance.WinText.transform.position.x, __instance.WinText.transform.position.y - 0.5f, __instance.WinText.transform.position.z);
        bonusText.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
        TMPro.TMP_Text textRenderer = bonusText.GetComponent<TMPro.TMP_Text>();
        textRenderer.text = "";
        switch (AdditionalTempData.winCondition)
        {
            case WinCondition.EveryoneDied:
                textRenderer.text = "EveryoneDied".Translate();
                textRenderer.color = Palette.DisabledGrey;
                __instance.BackgroundBar.material.SetColor("_Color", Palette.DisabledGrey);
                break;
            case WinCondition.MiniLose:
                textRenderer.text = "MiniLose".Translate();
                textRenderer.color = Mini.color;
                break;
            case WinCondition.LoversTeamWin:
                textRenderer.text = "LoversTeamWin".Translate();
                textRenderer.color = Lovers.color;
                __instance.BackgroundBar.material.SetColor("_Color", Lovers.color);
                break;
            case WinCondition.LoversSoloWin:
                textRenderer.text = "LoversSoloWin".Translate();
                textRenderer.color = Lovers.color;
                __instance.BackgroundBar.material.SetColor("_Color", Lovers.color);
                break;
            case WinCondition.JesterWin:
                textRenderer.text = "JesterWin".Translate();
                textRenderer.color = Jester.color;
                break;
            case WinCondition.JackalWin:
                textRenderer.text = "JackalWin".Translate();
                textRenderer.color = Jackal.color;
                break;
            case WinCondition.ArsonistWin:
                textRenderer.text = "ArsonistWin".Translate();
                textRenderer.color = Arsonist.color;
                break;
            case WinCondition.VultureWin:
                textRenderer.text = "VultureWin".Translate();
                textRenderer.color = Vulture.color;
                break;
            case WinCondition.ProsecutorWin:
                textRenderer.text = "ProsecutorWin".Translate();
                textRenderer.color = Lawyer.color;
                break;
            case WinCondition.DoomsayerWin:
                textRenderer.text = "DoomsayerWin".Translate();
                textRenderer.color = Doomsayer.color;
                __instance.BackgroundBar.material.SetColor("_Color", Doomsayer.color);
                break;
            case WinCondition.WerewolfWin:
                textRenderer.text = "WerewolfWin".Translate();
                textRenderer.color = Werewolf.color;
                break;
            case WinCondition.JuggernautWin:
                textRenderer.text = "JuggernautWin".Translate();
                textRenderer.color = Juggernaut.color;
                break;
            case WinCondition.Default:
                switch (OnGameEndPatch.gameOverReason)
                {
                    case GameOverReason.ImpostorDisconnect:
                        textRenderer.text = "ImpostorDisconnect".Translate();
                        textRenderer.color = Color.red;
                        break;
                    case GameOverReason.ImpostorByKill:
                        textRenderer.text = "ImpostorByKill".Translate();
                        textRenderer.color = Color.red;
                        break;
                    case GameOverReason.ImpostorBySabotage:
                        textRenderer.text = "ImpostorBySabotage".Translate();
                        textRenderer.color = Color.red;
                        break;
                    case GameOverReason.ImpostorByVote:
                        textRenderer.text = "ImpostorByVote".Translate();
                        textRenderer.color = Color.red;
                        break;
                    case GameOverReason.HumansByTask:
                        textRenderer.text = "HumansByTask".Translate();
                        textRenderer.color = Color.white;
                        break;
                    case GameOverReason.HumansDisconnect:
                        textRenderer.text = "HumansDisconnect".Translate();
                        textRenderer.color = Color.white;
                        break;
                    case GameOverReason.HumansByVote:
                        textRenderer.text = "HumansByVote".Translate();
                        textRenderer.color = Color.white;
                        break;
                }
                break;
        }

        foreach (WinCondition cond in AdditionalTempData.additionalWinConditions)
        {
            if (cond == WinCondition.AdditionalLawyerBonusWin)
            {
                textRenderer.text += $"\n{cs(Lawyer.color, "AdditionalLawyerBonusWin".Translate())}";
            }
            else if (cond == WinCondition.AdditionalAlivePursuerWin)
            {
                textRenderer.text += $"\n{cs(Pursuer.color, "AdditionalAlivePursuerWin".Translate())}";
            }
        }

        if (TORMapOptions.showRoleSummary || HideNSeek.isHideNSeekGM || PropHunt.isPropHuntGM)
        {
            var position = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, Camera.main.nearClipPlane));
            GameObject roleSummary = UnityEngine.Object.Instantiate(__instance.WinText.gameObject);
            roleSummary.transform.position = new Vector3(__instance.Navigation.ExitButton.transform.position.x + 0.1f, position.y - 0.1f, -214f);
            roleSummary.transform.localScale = new Vector3(1f, 1f, 1f);

            var roleSummaryText = new StringBuilder();
            if (HideNSeek.isHideNSeekGM || PropHunt.isPropHuntGM)
            {
                int minutes = (int)AdditionalTempData.timer / 60;
                int seconds = (int)AdditionalTempData.timer % 60;
                roleSummaryText.AppendLine($"<color=#FAD934FF>"+ "gameTime".Translate() + " {minutes:00}:{seconds:00}</color> \n");
            }
            roleSummaryText.AppendLine("endGameInfo".Translate());
            foreach (var data in AdditionalTempData.playerRoles)
            {
                //var roles = string.Join(" ", data.Roles.Select(x => Helpers.cs(x.color, x.name)));
                string roles = data.RoleNames;
                //if (data.IsGuesser) roles += " (Guesser)";
                var taskInfo = data.TasksTotal > 0 ? $" - <color=#FAD934FF>({data.TasksCompleted}/{data.TasksTotal})</color>" : "";
                if (data.Kills != null) taskInfo += $" - <color=#FF0000FF>(" + "killsCount".Translate() + $" {data.Kills})</color>";
                roleSummaryText.AppendLine($"{cs(data.IsAlive ? Color.white : new Color(.7f, .7f, .7f), data.PlayerName)} - {roles}{taskInfo}");
            }
            TMPro.TMP_Text roleSummaryTextMesh = roleSummary.GetComponent<TMPro.TMP_Text>();
            roleSummaryTextMesh.alignment = TMPro.TextAlignmentOptions.TopLeft;
            roleSummaryTextMesh.color = Color.white;
            roleSummaryTextMesh.fontSizeMin = 1.5f;
            roleSummaryTextMesh.fontSizeMax = 1.5f;
            roleSummaryTextMesh.fontSize = 1.5f;

            var roleSummaryTextMeshRectTransform = roleSummaryTextMesh.GetComponent<RectTransform>();
            roleSummaryTextMeshRectTransform.anchoredPosition = new Vector2(position.x + 3.5f, position.y - 0.1f);
            roleSummaryTextMesh.text = roleSummaryText.ToString();
        }
        AdditionalTempData.clear();
    }
}

[HarmonyPatch(typeof(LogicGameFlowNormal), nameof(LogicGameFlowNormal.CheckEndCriteria))]
class CheckEndCriteriaPatch
{
    public static bool Prefix(ShipStatus __instance)
    {
        if (!GameData.Instance) return false;
        if (DestroyableSingleton<TutorialManager>.InstanceExists) // InstanceExists | Don't check Custom Criteria when in Tutorial
            return true;
        var statistics = new PlayerStatistics(__instance);
        if (CheckAndEndGameForMiniLose(__instance)) return false;
        if (CheckAndEndGameForJesterWin(__instance)) return false;
        if (CheckAndEndGameForArsonistWin(__instance)) return false;
        if (CheckAndEndGameForVultureWin(__instance)) return false;
        if (CheckAndEndGameForDoomsayerWin(__instance)) return false;
        if (CheckAndEndGameForSabotageWin(__instance)) return false;
        if (CheckAndEndGameForTaskWin(__instance)) return false;
        if (CheckAndEndGameForProsecutorWin(__instance)) return false;
        if (CheckAndEndGameForWerewolfWin(__instance, statistics)) return false;
        if (CheckAndEndGameForJuggernautWin(__instance, statistics)) return false;
        if (CheckAndEndGameForLoverWin(__instance, statistics)) return false;
        if (CheckAndEndGameForJackalWin(__instance, statistics)) return false;
        if (CheckAndEndGameForImpostorWin(__instance, statistics)) return false;
        if (CheckAndEndGameForCrewmateWin(__instance, statistics)) return false;
        return false;
    }

    private static bool CheckAndEndGameForMiniLose(ShipStatus __instance)
    {
        if (Mini.triggerMiniLose)
        {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.MiniLose, false);
            return true;
        }
        return false;
    }

    private static bool CheckAndEndGameForJesterWin(ShipStatus __instance)
    {
        if (Jester.triggerJesterWin)
        {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.JesterWin, false);
            return true;
        }
        return false;
    }

    private static bool CheckAndEndGameForArsonistWin(ShipStatus __instance)
    {
        if (Arsonist.triggerArsonistWin)
        {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.ArsonistWin, false);
            return true;
        }
        return false;
    }

    private static bool CheckAndEndGameForVultureWin(ShipStatus __instance)
    {
        if (Vulture.triggerVultureWin)
        {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.VultureWin, false);
            return true;
        }
        return false;
    }

    private static bool CheckAndEndGameForSabotageWin(ShipStatus __instance)
    {
        if (MapUtilities.Systems == null) return false;
        var systemType = MapUtilities.Systems.ContainsKey(SystemTypes.LifeSupp) ? MapUtilities.Systems[SystemTypes.LifeSupp] : null;
        if (systemType != null)
        {
            LifeSuppSystemType lifeSuppSystemType = systemType.TryCast<LifeSuppSystemType>();
            if (lifeSuppSystemType != null && lifeSuppSystemType.Countdown < 0f)
            {
                EndGameForSabotage(__instance);
                lifeSuppSystemType.Countdown = 10000f;
                return true;
            }
        }
        var systemType2 = MapUtilities.Systems.ContainsKey(SystemTypes.Reactor) ? MapUtilities.Systems[SystemTypes.Reactor] : null;
        if (systemType2 == null)
        {
            systemType2 = MapUtilities.Systems.ContainsKey(SystemTypes.Laboratory) ? MapUtilities.Systems[SystemTypes.Laboratory] : null;
        }
        if (systemType2 != null)
        {
            ICriticalSabotage criticalSystem = systemType2.TryCast<ICriticalSabotage>();
            if (criticalSystem != null && criticalSystem.Countdown < 0f)
            {
                EndGameForSabotage(__instance);
                criticalSystem.ClearSabotage();
                return true;
            }
        }
        return false;
    }

    private static bool CheckAndEndGameForTaskWin(ShipStatus __instance)
    {
        if (TORMapOptions.preventTaskEnd) return false;
        if ((HideNSeek.isHideNSeekGM && !HideNSeek.taskWinPossible) || PropHunt.isPropHuntGM) return false;
        if (GameData.Instance.TotalTasks > 0
            && GameData.Instance.TotalTasks <= GameData.Instance.CompletedTasks
            //&& !PreventTaskEnd.Enable
            )
        {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame(GameOverReason.HumansByTask, false);
            return true;
        }
        return false;
    }

    private static bool CheckAndEndGameForDoomsayerWin(ShipStatus __instance)
    {
        if (Doomsayer.triggerDoomsayerrWin)
        {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.DoomsayerWin, false);
            return true;
        }
        return false;
    }

    private static bool CheckAndEndGameForProsecutorWin(ShipStatus __instance)
    {
        if (Lawyer.triggerProsecutorWin)
        {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.ProsecutorWin, false);
            return true;
        }
        return false;
    }

    private static bool CheckAndEndGameForLoverWin(ShipStatus __instance, PlayerStatistics statistics)
    {
        if (statistics.TeamLoversAlive == 2 && statistics.TotalAlive <= 3)
        {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.LoversWin, false);
            return true;
        }
        return false;
    }

    private static bool CheckAndEndGameForJackalWin(ShipStatus __instance, PlayerStatistics statistics)
    {
        if (statistics.TeamJackalAlive >= statistics.TotalAlive - statistics.TeamJackalAlive &&
            statistics.TeamImpostorsAlive == 0 &&
            statistics.TeamWerewolfAlive == 0 &&
            statistics.TeamJuggernautAlive == 0 &&
            !(statistics.TeamJackalHasAliveLover &&
            statistics.TeamLoversAlive == 2) &&
            !killingCrewAlive())
        {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.TeamJackalWin, false);
            return true;
        }
        return false;
    }

    private static bool CheckAndEndGameForWerewolfWin(ShipStatus __instance, PlayerStatistics statistics)
    {
        if (
            statistics.TeamWerewolfAlive >= statistics.TotalAlive - statistics.TeamWerewolfAlive &&
            statistics.TeamImpostorsAlive == 0 &&
            statistics.TeamJackalAlive == 0 &&
            statistics.TeamJuggernautAlive == 0 &&
            !(statistics.TeamWerewolfHasAliveLover &&
            statistics.TeamLoversAlive == 2) &&
            !killingCrewAlive()
        )
        {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.WerewolfWin, false);
            return true;
        }
        return false;
    }

    private static bool CheckAndEndGameForJuggernautWin(ShipStatus __instance, PlayerStatistics statistics)
    {
        if (
            statistics.TeamJuggernautAlive >= statistics.TotalAlive - statistics.TeamJuggernautAlive &&
            statistics.TeamImpostorsAlive == 0 &&
            statistics.TeamJackalAlive == 0 &&
            statistics.TeamWerewolfAlive == 0 &&
            !(statistics.TeamJuggernautHasAliveLover &&
              statistics.TeamLoversAlive == 2) &&
            !killingCrewAlive()
        )
        {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.JuggernautWin, false);
            return true;
        }

        return false;
    }

    private static bool CheckAndEndGameForImpostorWin(ShipStatus __instance, PlayerStatistics statistics)
    {
        if (HideNSeek.isHideNSeekGM || PropHunt.isPropHuntGM)
            if (0 != statistics.TotalAlive - statistics.TeamImpostorsAlive) return false;

        if (statistics.TeamImpostorsAlive >= statistics.TotalAlive - statistics.TeamImpostorsAlive &&
            statistics.TeamJackalAlive == 0 &&
            statistics.TeamWerewolfAlive == 0 &&
            statistics.TeamJuggernautAlive == 0 &&
            !(statistics.TeamImpostorHasAliveLover &&
            statistics.TeamLoversAlive == 2) &&
            !killingCrewAlive())
        {
            //__instance.enabled = false;
            GameOverReason endReason;
            switch (GameData.LastDeathReason)
            {
                case DeathReason.Exile:
                    endReason = GameOverReason.ImpostorByVote;
                    break;
                case DeathReason.Kill:
                    endReason = GameOverReason.ImpostorByKill;
                    break;
                default:
                    endReason = GameOverReason.ImpostorByVote;
                    break;
            }
            GameManager.Instance.RpcEndGame(endReason, false);
            return true;
        }
        return false;
    }

    private static bool CheckAndEndGameForCrewmateWin(ShipStatus __instance, PlayerStatistics statistics)
    {
        if (HideNSeek.isHideNSeekGM && HideNSeek.timer <= 0 && !HideNSeek.isWaitingTimer)
        {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame(GameOverReason.HumansByVote, false);
            return true;
        }
        if (PropHunt.isPropHuntGM && PropHunt.timer <= 0 && PropHunt.timerRunning)
        {
            GameManager.Instance.RpcEndGame(GameOverReason.HumansByVote, false);
            return true;
        }
        if (statistics.TeamImpostorsAlive == 0 &&
            statistics.TeamJackalAlive == 0 &&
            statistics.TeamJuggernautAlive == 0 &&
            statistics.TeamWerewolfAlive == 0)
        {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame(GameOverReason.HumansByVote, false);
            return true;
        }
        return false;
    }

    private static void EndGameForSabotage(ShipStatus __instance)
    {
        //__instance.enabled = false;
        GameManager.Instance.RpcEndGame(GameOverReason.ImpostorBySabotage, false);
        return;
    }

}

internal class PlayerStatistics
{
    public int TeamImpostorsAlive { get; set; }
    public int TeamJackalAlive { get; set; }
    public int TeamLoversAlive { get; set; }
    public int TotalAlive { get; set; }
    public bool TeamImpostorHasAliveLover { get; set; }
    public bool TeamJackalHasAliveLover { get; set; }
    public int TeamWerewolfAlive { get; set; }
    public bool TeamWerewolfHasAliveLover { get; set; }
    public int TeamJuggernautAlive { get; set; }
    public bool TeamJuggernautHasAliveLover { get; set; }

    public PlayerStatistics(ShipStatus __instance)
    {
        GetPlayerCounts();
    }

    private bool isLover(NetworkedPlayerInfo p)
    {
        return (Lovers.lover1 != null && Lovers.lover1.PlayerId == p.PlayerId) || (Lovers.lover2 != null && Lovers.lover2.PlayerId == p.PlayerId);
    }

    private void GetPlayerCounts()
    {
        var numJackalAlive = 0;
        var numImpostorsAlive = 0;
        var numLoversAlive = 0;
        var numTotalAlive = 0;
        var numJuggernautAlive = 0;
        var impLover = false;
        var jackalLover = false;
        var numWerewolfAlive = 0;
        var werewolfLover = false;
        var juggernautLover = false;

        foreach (var playerInfo in GameData.Instance.AllPlayers.GetFastEnumerator())
        {
            if (!playerInfo.Disconnected)
            {
                if (!playerInfo.IsDead)
                {
                    numTotalAlive++;

                    bool lover = isLover(playerInfo);
                    if (lover) numLoversAlive++;

                    if (playerInfo.Role.IsImpostor)
                    {
                        numImpostorsAlive++;
                        if (lover) impLover = true;
                    }
                    if (Jackal.jackal != null && Jackal.jackal.PlayerId == playerInfo.PlayerId)
                    {
                        numJackalAlive++;
                        if (lover) jackalLover = true;
                    }
                    if (Sidekick.sidekick != null && Sidekick.sidekick.PlayerId == playerInfo.PlayerId)
                    {
                        numJackalAlive++;
                        if (lover) jackalLover = true;
                    }
                    if (Juggernaut.juggernaut != null && Juggernaut.juggernaut.PlayerId == playerInfo.PlayerId)
                    {
                        numJuggernautAlive++;
                        if (lover) juggernautLover = true;
                    }
                    if (Werewolf.werewolf != null && Werewolf.werewolf.PlayerId == playerInfo.PlayerId)
                    {
                        numWerewolfAlive++;
                        if (lover) werewolfLover = true;
                    }
                }
            }
        }

        TeamJackalAlive = numJackalAlive;
        TeamImpostorsAlive = numImpostorsAlive;
        TeamLoversAlive = numLoversAlive;
        TotalAlive = numTotalAlive;
        TeamImpostorHasAliveLover = impLover;
        TeamJackalHasAliveLover = jackalLover;
        TeamWerewolfHasAliveLover = werewolfLover;
        TeamWerewolfAlive = numWerewolfAlive;
        TeamJuggernautAlive = numJuggernautAlive;
        TeamJuggernautHasAliveLover = juggernautLover;
    }
}
