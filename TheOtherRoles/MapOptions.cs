using System.Collections.Generic;
using TheOtherRoles.Utilities;
using UnityEngine;

namespace TheOtherRoles;

static class TORMapOptions
{
    public static float ButtonCooldown => CustomOptionHolder.resteButtonCooldown.getFloat();
    public static bool PreventTaskEnd => CustomOptionHolder.preventTaskEnd.getBool();
    public static float KillCooddown => GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown;
    public static int NumImpostors => GameOptionsManager.Instance.currentNormalGameOptions.NumImpostors;

    // Set values
    public static int maxNumberOfMeetings = 10;
    public static bool blockSkippingInEmergencyMeetings;
    public static bool noVoteIsSelfVote;
    public static bool hidePlayerNames;
    public static bool ghostsSeeRoles = true;
    public static bool ghostsSeeModifier = true;
    public static bool ghostsSeeInformation = true;
    public static bool ghostsSeeVotes = true;
    public static bool showRoleSummary = true;
    public static bool allowParallelMedBayScans;
    public static bool showLighterDarker;
    public static bool toggleCursor = true;
    public static bool enableSoundEffects = true;
    public static bool enableHorseMode;
    public static bool shieldFirstKill;
    public static bool hideVentAnim;
    public static bool impostorSeeRoles;
    public static bool transparentTasks;
    public static bool hideOutOfSightNametags;
    public static bool ShowVentsOnMap;
    public static bool ShowVentsOnMeetingMap = true;
    public static bool ShowChatNotifications = true;
    public static bool muteLobbyBGM = true;
    public static bool DisableGameEnd;
    public static bool DebugMode;
    public static bool showFPS;
    public static bool disableMedscanWalking;
    public static int restrictDevices;
    // public static float restrictAdminTime = 600f;
    //public static float restrictAdminTimeMax = 600f;
    public static float restrictCamerasTime = 600f;
    public static float restrictCamerasTimeMax = 600f;
    public static float restrictVitalsTime = 600f;
    public static float restrictVitalsTimeMax = 600f;
    public static bool disableCamsRoundOne;
    public static bool isRoundOne = true;
    public static bool camoComms;
    public static bool preventTaskEnd;
    public static bool randomGameStartPosition;
    public static bool allowModGuess;
    public static CustomGamemodes gameMode = CustomGamemodes.Classic;

    // Updating values
    public static int meetingsCount;
    public static List<SurvCamera> camerasToAdd = new();
    public static List<Vent> ventsToSeal = new();
    public static Dictionary<byte, PoolablePlayer> playerIcons = new();
    public static string firstKillName;
    public static PlayerControl firstKillPlayer;

    public static void clearAndReloadMapOptions()
    {
        meetingsCount = 0;
        camerasToAdd.Clear();
        ventsToSeal.Clear();
        playerIcons.Clear();

        maxNumberOfMeetings = Mathf.RoundToInt(CustomOptionHolder.maxNumberOfMeetings.getSelection());
        blockSkippingInEmergencyMeetings = CustomOptionHolder.blockSkippingInEmergencyMeetings.getBool();
        noVoteIsSelfVote = CustomOptionHolder.noVoteIsSelfVote.getBool();
        hidePlayerNames = CustomOptionHolder.hidePlayerNames.getBool();
        hideOutOfSightNametags = CustomOptionHolder.hideOutOfSightNametags.getBool();
        hideVentAnim = CustomOptionHolder.hideVentAnimOnShadows.getBool();
        allowParallelMedBayScans = CustomOptionHolder.allowParallelMedBayScans.getBool();
        disableMedscanWalking = CustomOptionHolder.disableMedbayWalk.getBool();
        camoComms = CustomOptionHolder.enableCamoComms.getBool();
        shieldFirstKill = CustomOptionHolder.shieldFirstKill.getBool();
        impostorSeeRoles = CustomOptionHolder.impostorSeeRoles.getBool();
        transparentTasks = CustomOptionHolder.transparentTasks.getBool();
        restrictDevices = CustomOptionHolder.restrictDevices.getSelection();
        //restrictAdminTime = restrictAdminTimeMax = CustomOptionHolder.restrictAdmin.getFloat();
        restrictCamerasTime = restrictCamerasTimeMax = CustomOptionHolder.restrictCameras.getFloat();
        restrictVitalsTime = restrictVitalsTimeMax = CustomOptionHolder.restrictVents.getFloat();
        disableCamsRoundOne = CustomOptionHolder.disableCamsRound1.getBool();
        randomGameStartPosition = CustomOptionHolder.randomGameStartPosition.getBool();
        ShowVentsOnMap = CustomOptionHolder.ShowVentsOnMap.getBool();
        ShowVentsOnMeetingMap = CustomOptionHolder.ShowVentsOnMeetingMap.getBool();
        preventTaskEnd = CustomOptionHolder.preventTaskEnd.getBool();
        DebugMode = CustomOptionHolder.debugMode.getBool();
        DisableGameEnd = CustomOptionHolder.disableGameEnd.getBool();
        //allowModGuess = false;
        allowModGuess = CustomOptionHolder.allowModGuess.getBool();
        firstKillPlayer = null;
        isRoundOne = true;
    }

    public static void reloadPluginOptions()
    {
        ghostsSeeRoles = Main.GhostsSeeRoles.Value;
        ghostsSeeModifier = Main.GhostsSeeModifier.Value;
        ghostsSeeInformation = Main.GhostsSeeInformation.Value;
        ghostsSeeVotes = Main.GhostsSeeVotes.Value;
        showLighterDarker = Main.ShowLighterDarker.Value;
        toggleCursor = Main.ToggleCursor.Value;
        enableSoundEffects = Main.EnableSoundEffects.Value;
        enableHorseMode = Main.EnableHorseMode.Value;
        showFPS = Main.ShowFPS.Value;
        ShowChatNotifications = Main.ShowChatNotifications.Value;
        muteLobbyBGM = Main.MuteLobbyBGM.Value;

        //Patches.ShouldAlwaysHorseAround.isHorseMode = Main.EnableHorseMode.Value;
    }
    public static void resetDeviceTimes()
    {
        //restrictAdminTime = restrictAdminTimeMax;
        restrictCamerasTime = restrictCamerasTimeMax;
        restrictVitalsTime = restrictVitalsTimeMax;
    }

    // public static bool canUseAdmin  { get { return restrictDevices == 0 || restrictAdminTime > 0f || PlayerControl.LocalPlayer == Hacker.hacker || PlayerControl.LocalPlayer.Data.IsDead; }}

    //public static bool couldUseAdmin { get { return restrictDevices == 0 || restrictAdminTimeMax > 0f  || PlayerControl.LocalPlayer == Hacker.hacker || PlayerControl.LocalPlayer.Data.IsDead; }}

    public static bool canUseCameras => restrictDevices == 0 || restrictCamerasTime > 0f || PlayerControl.LocalPlayer == Hacker.hacker || PlayerControl.LocalPlayer.Data.IsDead;

    public static bool couldUseCameras => restrictDevices == 0 || restrictCamerasTimeMax > 0f || PlayerControl.LocalPlayer == Hacker.hacker || PlayerControl.LocalPlayer.Data.IsDead;

    public static bool canUseVitals => restrictDevices == 0 || restrictVitalsTime > 0f || PlayerControl.LocalPlayer == Hacker.hacker || PlayerControl.LocalPlayer.Data.IsDead;

    public static bool couldUseVitals => restrictDevices == 0 || restrictVitalsTimeMax > 0f || PlayerControl.LocalPlayer == Hacker.hacker || PlayerControl.LocalPlayer.Data.IsDead;

}
