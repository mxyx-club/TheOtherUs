using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmongUs.Data;
using Hazel;
using Reactor.Utilities.Extensions;
using TheOtherRoles.CustomGameModes;
using TheOtherRoles.Modules;
using TheOtherRoles.Objects;
using TheOtherRoles.Utilities;
using TMPro;
using UnityEngine;
using static TheOtherRoles.TheOtherRoles;
using Object = UnityEngine.Object;

namespace TheOtherRoles;

[HarmonyPatch]
public static class TheOtherRoles
{
    public static System.Random rnd = new((int)DateTime.Now.Ticks);

    public static void clearAndReloadRoles()
    {
        ResetButtonCooldown.clearAndReload();
        Jester.clearAndReload();
        Mayor.clearAndReload();
        Portalmaker.clearAndReload();
        Poucher.clearAndReload();
        Mimic.clearAndReload();
        Engineer.clearAndReload();
        Sheriff.clearAndReload();
        Cursed.clearAndReload();
        Deputy.clearAndReload();
        Amnisiac.clearAndReload();
        Lighter.clearAndReload();
        Godfather.clearAndReload();
        Mafioso.clearAndReload();
        Janitor.clearAndReload();
        Detective.clearAndReload();
        Werewolf.clearAndReload();
        TimeMaster.clearAndReload();
        BodyGuard.clearAndReload();
        Veteren.clearAndReload();
        Medic.clearAndReload();
        PrivateInvestigator.clearAndReload();
        Shifter.clearAndReload();
        Swapper.clearAndReload();
        Lovers.clearAndReload();
        Seer.clearAndReload();
        Morphling.clearAndReload();
        Bomber.clearAndReload();
        Camouflager.clearAndReload();
        Cultist.clearAndReload();
        Hacker.clearAndReload();
        Tracker.clearAndReload();
        Vampire.clearAndReload();
        Snitch.clearAndReload();
        Jackal.clearAndReload();
        Sidekick.clearAndReload();
        Follower.clearAndReload();
        Eraser.clearAndReload();
        Spy.clearAndReload();
        Trickster.clearAndReload();
        Cleaner.clearAndReload();
        Undertaker.clearAndReload();
        Warlock.clearAndReload();
        SecurityGuard.clearAndReload();
        Arsonist.clearAndReload();
        BountyHunter.clearAndReload();
        Vulture.clearAndReload();
        Medium.clearAndReload();
        Lawyer.clearAndReload();
        Pursuer.clearAndReload();
        Witch.clearAndReload();
        Jumper.clearAndReload();
        Escapist.clearAndReload();
        Ninja.clearAndReload();
        Blackmailer.clearAndReload();
        Thief.clearAndReload();
        Juggernaut.clearAndReload();
        Miner.clearAndReload();
        Trapper.clearAndReload();
        Terrorist.clearAndReload();
        //Guesser.clearAndReload();
        //Swooper.clearAndReload();
        Yoyo.clearAndReload();
        Doomsayer.clearAndReload();

        // Modifier
        Bait.clearAndReload();
        Aftermath.clearAndReload();
        Bloody.clearAndReload();
        AntiTeleport.clearAndReload();
        Tiebreaker.clearAndReload();
        Sunglasses.clearAndReload();
        Torch.clearAndReload();
        Blind.clearAndReload();
        Watcher.clearAndReload();
        Flash.clearAndReload();
        Radar.clearAndReload();
        Tunneler.clearAndReload();
        Multitasker.clearAndReload();
        Disperser.clearAndReload();
        Mini.clearAndReload();
        Indomitable.clearAndReload();
        Giant.clearAndReload();
        Slueth.clearAndReload();
        Vip.clearAndReload();
        Invert.clearAndReload();
        Chameleon.clearAndReload();
        Armored.clearAndReload();

        // Gamemodes
        HandleGuesser.clearAndReload();
        HideNSeek.clearAndReload();
        PropHunt.clearAndReload();

    }
    /*
    public static class PreventTaskEnd
    {
        public static bool Enable = false;
        public static void clearAndReload()
        {
            Enable = CustomOptionHolder.preventTaskEnd.getBool();
        }
    }
    */
    public static class ResetButtonCooldown
    {
        public static float killCooldown;
        public static void clearAndReload()
        {
            killCooldown = CustomOptionHolder.resteButtonCooldown.getFloat();
        }
    }

    public static class Jester
    {
        public static PlayerControl jester;
        public static Color color = new Color32(236, 98, 165, byte.MaxValue);

        public static bool triggerJesterWin;
        public static bool canCallEmergency = true;
        public static bool canVent;
        public static bool hasImpostorVision;

        public static void clearAndReload()
        {
            jester = null;
            triggerJesterWin = false;
            canCallEmergency = CustomOptionHolder.jesterCanCallEmergency.getBool();
            canVent = CustomOptionHolder.jesterCanVent.getBool();
            hasImpostorVision = CustomOptionHolder.jesterHasImpostorVision.getBool();
        }
    }

    public static class BodyGuard
    {
        public static PlayerControl bodyguard;
        public static PlayerControl guarded;
        public static Color color = new Color32(145, 102, 64, byte.MaxValue);
        public static bool reset = true;
        public static bool usedGuard;
        public static bool guardFlash;
        private static Sprite guardButtonSprite;
        public static PlayerControl currentTarget;

        public static void resetGuarded()
        {
            currentTarget = guarded = null;
            usedGuard = false;
        }


        public static Sprite getGuardButtonSprite()
        {
            if (guardButtonSprite) return guardButtonSprite;
            guardButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.Shield.png", 115f);
            return guardButtonSprite;
        }

        public static void clearAndReload()
        {
            bodyguard = null;
            guardFlash = CustomOptionHolder.bodyGuardFlash.getBool();
            reset = CustomOptionHolder.bodyGuardResetTargetAfterMeeting.getBool();
            guarded = null;
            usedGuard = false;
        }
    }

    public static class Portalmaker
    {
        public static PlayerControl portalmaker;
        public static Color color = new Color32(69, 69, 169, byte.MaxValue);

        public static float cooldown;
        public static float usePortalCooldown;
        public static bool logOnlyHasColors;
        public static bool logShowsTime;
        public static bool canPortalFromAnywhere;

        private static Sprite placePortalButtonSprite;
        private static Sprite usePortalButtonSprite;
        private static Sprite usePortalSpecialButtonSprite1;
        private static Sprite usePortalSpecialButtonSprite2;
        private static Sprite logSprite;

        public static Sprite getPlacePortalButtonSprite()
        {
            if (placePortalButtonSprite) return placePortalButtonSprite;
            placePortalButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.PlacePortalButton.png", 115f);
            return placePortalButtonSprite;
        }

        public static Sprite getUsePortalButtonSprite()
        {
            if (usePortalButtonSprite) return usePortalButtonSprite;
            usePortalButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.UsePortalButton.png", 115f);
            return usePortalButtonSprite;
        }

        public static Sprite getUsePortalSpecialButtonSprite(bool first)
        {
            if (first)
            {
                if (usePortalSpecialButtonSprite1) return usePortalSpecialButtonSprite1;
                usePortalSpecialButtonSprite1 = loadSpriteFromResources("TheOtherRoles.Resources.UsePortalSpecialButton1.png", 115f);
                return usePortalSpecialButtonSprite1;
            }
            else
            {
                if (usePortalSpecialButtonSprite2) return usePortalSpecialButtonSprite2;
                usePortalSpecialButtonSprite2 = loadSpriteFromResources("TheOtherRoles.Resources.UsePortalSpecialButton2.png", 115f);
                return usePortalSpecialButtonSprite2;
            }
        }

        public static Sprite getLogSprite()
        {
            if (logSprite) return logSprite;
            logSprite = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.DoorLogsButton].Image;
            return logSprite;
        }

        public static void clearAndReload()
        {
            portalmaker = null;
            cooldown = CustomOptionHolder.portalmakerCooldown.getFloat();
            usePortalCooldown = CustomOptionHolder.portalmakerUsePortalCooldown.getFloat();
            logOnlyHasColors = CustomOptionHolder.portalmakerLogOnlyColorType.getBool();
            logShowsTime = CustomOptionHolder.portalmakerLogHasTime.getBool();
            canPortalFromAnywhere = CustomOptionHolder.portalmakerCanPortalFromAnywhere.getBool();
        }


    }

    public static class Cultist
    {
        public static PlayerControl cultist;
        public static PlayerControl currentTarget;
        public static Color color = Palette.ImpostorRed;
        public static List<Arrow> localArrows = new();
        public static bool chatTarget = true;
        public static bool chatTarget2 = true;
        public static bool isCultistGame;
        public static bool needsFollower = true;
        //public static PlayerControl currentFollower;
        public static Sprite buttonSprite;


        public static Sprite getSidekickButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.SidekickButton.png", 115f);
            return buttonSprite;
        }

        public static void clearAndReload()
        {
            if (localArrows != null)
            {
                foreach (Arrow arrow in localArrows)
                    if (arrow?.arrow != null)
                        Object.Destroy(arrow.arrow);
            }
            localArrows = new List<Arrow>();
            cultist = null;
            currentTarget = null;
            //currentFollower = null;
            needsFollower = true;
            chatTarget = true;
            chatTarget2 = true;
        }
    }

    public static class Follower
    {
        public static PlayerControl follower;
        public static PlayerControl currentTarget;
        public static Color color = Palette.ImpostorRed;
        public static List<Arrow> localArrows = new();
        public static bool getsAssassin;
        public static bool chatTarget = true;
        public static bool chatTarget2 = true;

        public static void clearAndReload()
        {
            if (localArrows != null)
            {
                foreach (Arrow arrow in localArrows)
                    if (arrow?.arrow != null)
                        Object.Destroy(arrow.arrow);
            }
            localArrows = new List<Arrow>();
            follower = null;
            currentTarget = null;
            chatTarget = true;
            chatTarget2 = true;
            getsAssassin = CustomOptionHolder.modifierAssassinCultist.getBool();
        }
    }

    public static class Crew
    {
        public static PlayerControl crew;
        public static Color color = Palette.White;
        public static void clearAndReload()
        {
            crew = null;
        }
    }

    public static class Mayor
    {
        public static PlayerControl mayor;
        public static Color color = new Color32(32, 77, 66, byte.MaxValue);
        public static Minigame emergency;
        public static Sprite emergencySprite;
        public static int remoteMeetingsLeft = 1;

        public static bool canSeeVoteColors;
        public static int tasksNeededToSeeVoteColors;
        public static bool meetingButton = true;
        public static int mayorChooseSingleVote;

        public static bool voteTwice = true;

        public static Sprite getMeetingSprite()
        {
            if (emergencySprite) return emergencySprite;
            emergencySprite = loadSpriteFromResources("TheOtherRoles.Resources.EmergencyButton.png", 550f);
            return emergencySprite;
        }

        public static void clearAndReload()
        {
            mayor = null;
            emergency = null;
            emergencySprite = null;
            remoteMeetingsLeft = Mathf.RoundToInt(CustomOptionHolder.mayorMaxRemoteMeetings.getFloat());
            canSeeVoteColors = CustomOptionHolder.mayorCanSeeVoteColors.getBool();
            tasksNeededToSeeVoteColors = (int)CustomOptionHolder.mayorTasksNeededToSeeVoteColors.getFloat();
            meetingButton = CustomOptionHolder.mayorMeetingButton.getBool();
            mayorChooseSingleVote = CustomOptionHolder.mayorChooseSingleVote.getSelection();
            voteTwice = true;
        }
    }

    public static class Engineer
    {
        public static PlayerControl engineer;
        public static Color color = new Color32(0, 40, 245, byte.MaxValue);
        private static Sprite buttonSprite;

        public static bool resetFixAfterMeeting;
        //public static bool expertRepairs = false;
        public static bool remoteFix = true;
        public static int remainingFixes = 1;
        public static bool highlightForImpostors = true;
        public static bool highlightForTeamJackal = true;

        public static bool usedFix;

        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.RepairButton.png", 115f);
            return buttonSprite;
        }

        public static void resetFixes()
        {
            remainingFixes = Mathf.RoundToInt(CustomOptionHolder.engineerNumberOfFixes.getFloat());
            usedFix = false;
        }

        public static void clearAndReload()
        {
            engineer = null;
            resetFixes();
            remoteFix = CustomOptionHolder.engineerRemoteFix.getBool();
            //expertRepairs = CustomOptionHolder.engineerExpertRepairs.getBool();
            resetFixAfterMeeting = CustomOptionHolder.engineerResetFixAfterMeeting.getBool();
            remainingFixes = Mathf.RoundToInt(CustomOptionHolder.engineerNumberOfFixes.getFloat());
            highlightForImpostors = CustomOptionHolder.engineerHighlightForImpostors.getBool();
            highlightForTeamJackal = CustomOptionHolder.engineerHighlightForTeamJackal.getBool();
            usedFix = false;
        }
    }

    public static class PrivateInvestigator
    {
        public static PlayerControl privateInvestigator;
        public static Color color = new Color32(77, 77, 255, byte.MaxValue);
        private static Sprite buttonSprite;
        public static PlayerControl watching;
        public static PlayerControl currentTarget;


        public static bool seeFlashColor;

        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.Watch.png", 115f);
            return buttonSprite;
        }



        public static void clearAndReload(bool clearList = true)
        {
            privateInvestigator = null;
            watching = null;
            currentTarget = null;
            seeFlashColor = CustomOptionHolder.privateInvestigatorSeeColor.getBool();
        }
    }

    public static class Godfather
    {
        public static PlayerControl godfather;
        public static Color color = Palette.ImpostorRed;

        public static void clearAndReload()
        {
            godfather = null;
        }
    }

    public static class Mafioso
    {
        public static PlayerControl mafioso;
        public static Color color = Palette.ImpostorRed;

        public static void clearAndReload()
        {
            mafioso = null;
        }
    }


    public static class Janitor
    {
        public static PlayerControl janitor;
        public static Color color = Palette.ImpostorRed;

        public static float cooldown = 30f;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.CleanButton.png", 115f);
            return buttonSprite;
        }

        public static void clearAndReload()
        {
            janitor = null;
            cooldown = CustomOptionHolder.janitorCooldown.getFloat();
        }
    }

    public static class Sheriff
    {
        public static PlayerControl sheriff;
        public static Color color = new Color32(248, 205, 70, byte.MaxValue);

        public static float cooldown = 30f;
        public static bool canKillNeutrals;
        public static bool canKillArsonist;
        public static bool canKillLawyer;
        public static bool canKillJester;
        public static bool canKillPursuer;
        public static bool canKillDoomsayer;
        public static bool canKillVulture;
        public static bool canKillThief;
        public static bool canKillAmnesiac;
        public static bool canKillProsecutor;
        public static bool spyCanDieToSheriff;
        public static int misfireKills; // Self: 0, Target: 1, Both: 2

        public static PlayerControl currentTarget;

        public static PlayerControl formerDeputy;  // Needed for keeping handcuffs + shifting
        public static PlayerControl formerSheriff;  // When deputy gets promoted...

        public static void replaceCurrentSheriff(PlayerControl deputy)
        {
            if (!formerSheriff) formerSheriff = sheriff;
            sheriff = deputy;
            currentTarget = null;
            cooldown = CustomOptionHolder.sheriffCooldown.getFloat();
        }

        public static void clearAndReload()
        {
            sheriff = null;
            currentTarget = null;
            formerDeputy = null;
            formerSheriff = null;
            misfireKills = CustomOptionHolder.sheriffMisfireKills.getSelection();
            cooldown = CustomOptionHolder.sheriffCooldown.getFloat();
            canKillNeutrals = CustomOptionHolder.sheriffCanKillNeutrals.getBool();
            canKillArsonist = CustomOptionHolder.sheriffCanKillArsonist.getBool();
            canKillLawyer = CustomOptionHolder.sheriffCanKillLawyer.getBool();
            canKillJester = CustomOptionHolder.sheriffCanKillJester.getBool();
            canKillDoomsayer = CustomOptionHolder.sheriffCanKillDoomsayer.getBool();
            canKillPursuer = CustomOptionHolder.sheriffCanKillPursuer.getBool();
            canKillVulture = CustomOptionHolder.sheriffCanKillVulture.getBool();
            canKillThief = CustomOptionHolder.sheriffCanKillThief.getBool();
            canKillAmnesiac = CustomOptionHolder.sheriffCanKillAmnesiac.getBool();
            canKillProsecutor = CustomOptionHolder.sheriffCanKillProsecutor.getBool();
            spyCanDieToSheriff = CustomOptionHolder.spyCanDieToSheriff.getBool();
        }
    }

    public static class Deputy
    {
        public static PlayerControl deputy;
        public static Color color = Sheriff.color;

        public static PlayerControl currentTarget;
        public static List<byte> handcuffedPlayers = new();
        public static int promotesToSheriff; // No: 0, Immediately: 1, After Meeting: 2
        public static bool keepsHandcuffsOnPromotion;
        public static float handcuffDuration;
        public static float remainingHandcuffs;
        public static float handcuffCooldown;
        public static bool knowsSheriff;
        public static Dictionary<byte, float> handcuffedKnows = new();

        private static Sprite buttonSprite;
        private static Sprite handcuffedSprite;

        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.DeputyHandcuffButton.png", 115f);
            return buttonSprite;
        }

        public static Sprite getHandcuffedButtonSprite()
        {
            if (handcuffedSprite) return handcuffedSprite;
            handcuffedSprite = loadSpriteFromResources("TheOtherRoles.Resources.DeputyHandcuffed.png", 115f);
            return handcuffedSprite;
        }

        // Can be used to enable / disable the handcuff effect on the target's buttons
        public static void setHandcuffedKnows(bool active = true, byte playerId = Byte.MaxValue)
        {
            if (playerId == Byte.MaxValue)
                playerId = PlayerControl.LocalPlayer.PlayerId;

            if (active && playerId == PlayerControl.LocalPlayer.PlayerId)
            {
                MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ShareGhostInfo, SendOption.Reliable, -1);
                writer.Write(PlayerControl.LocalPlayer.PlayerId);
                writer.Write((byte)RPCProcedure.GhostInfoTypes.HandcuffNoticed);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
            }

            if (active)
            {
                handcuffedKnows.Add(playerId, handcuffDuration);
                handcuffedPlayers.RemoveAll(x => x == playerId);
            }

            if (playerId == PlayerControl.LocalPlayer.PlayerId)
            {
                HudManagerStartPatch.setAllButtonsHandcuffedStatus(active);
                SoundEffectsManager.play("deputyHandcuff");
            }

        }

        public static void clearAndReload()
        {
            deputy = null;
            currentTarget = null;
            handcuffedPlayers = new List<byte>();
            handcuffedKnows = new Dictionary<byte, float>();
            HudManagerStartPatch.setAllButtonsHandcuffedStatus(false, true);
            promotesToSheriff = CustomOptionHolder.deputyGetsPromoted.getSelection();
            remainingHandcuffs = CustomOptionHolder.deputyNumberOfHandcuffs.getFloat();
            handcuffCooldown = CustomOptionHolder.deputyHandcuffCooldown.getFloat();
            keepsHandcuffsOnPromotion = CustomOptionHolder.deputyKeepsHandcuffs.getBool();
            handcuffDuration = CustomOptionHolder.deputyHandcuffDuration.getFloat();
            knowsSheriff = CustomOptionHolder.deputyKnowsSheriff.getBool();
        }
    }

    public static class Lighter
    {
        public static PlayerControl lighter;
        public static Color color = new Color32(238, 229, 190, byte.MaxValue);

        public static float lighterModeLightsOnVision = 2f;
        public static float lighterModeLightsOffVision = 0.75f;
        public static float flashlightWidth = 0.75f;

        public static void clearAndReload()
        {
            lighter = null;
            flashlightWidth = CustomOptionHolder.lighterFlashlightWidth.getFloat();
            lighterModeLightsOnVision = CustomOptionHolder.lighterModeLightsOnVision.getFloat();
            lighterModeLightsOffVision = CustomOptionHolder.lighterModeLightsOffVision.getFloat();
        }
    }

    public static class Detective
    {
        public static PlayerControl detective;
        public static Color color = new Color32(8, 180, 180, byte.MaxValue);

        public static float footprintIntervall = 1f;
        public static float footprintDuration = 1f;
        public static bool anonymousFootprints;
        public static float reportNameDuration;
        public static float reportColorDuration = 20f;
        public static float timer = 6.2f;

        public static void clearAndReload()
        {
            detective = null;
            anonymousFootprints = CustomOptionHolder.detectiveAnonymousFootprints.getBool();
            footprintIntervall = CustomOptionHolder.detectiveFootprintIntervall.getFloat();
            footprintDuration = CustomOptionHolder.detectiveFootprintDuration.getFloat();
            reportNameDuration = CustomOptionHolder.detectiveReportNameDuration.getFloat();
            reportColorDuration = CustomOptionHolder.detectiveReportColorDuration.getFloat();
            timer = 6.2f;
        }
    }
}

public static class TimeMaster
{
    public static PlayerControl timeMaster;
    public static Color color = new Color32(112, 142, 239, byte.MaxValue);

    public static bool reviveDuringRewind;
    public static float rewindTime = 3f;
    public static float shieldDuration = 3f;
    public static float cooldown = 30f;

    public static bool shieldActive;
    public static bool isRewinding;

    private static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.TimeShieldButton.png", 115f);
        return buttonSprite;
    }

    public static void clearAndReload()
    {
        timeMaster = null;
        isRewinding = false;
        shieldActive = false;
        rewindTime = CustomOptionHolder.timeMasterRewindTime.getFloat();
        shieldDuration = CustomOptionHolder.timeMasterShieldDuration.getFloat();
        cooldown = CustomOptionHolder.timeMasterCooldown.getFloat();
    }
}

public static class Amnisiac
{
    public static PlayerControl amnisiac;
    public static List<Arrow> localArrows = new();
    public static Color color = new(0.5f, 0.7f, 1f, 1f);
    public static List<PoolablePlayer> poolIcons = new();

    public static bool showArrows = true;
    public static bool resetRole;

    private static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.Remember.png", 115f);
        return buttonSprite;
    }

    public static void clearAndReload()
    {
        amnisiac = null;
        showArrows = CustomOptionHolder.amnisiacShowArrows.getBool();
        resetRole = CustomOptionHolder.amnisiacResetRole.getBool();
        if (localArrows != null)
        {
            foreach (Arrow arrow in localArrows)
                if (arrow?.arrow != null)
                    Object.Destroy(arrow.arrow);
        }
        localArrows = new List<Arrow>();
    }
}

public static class Veteren
{
    public static PlayerControl veteren;
    public static Color color = new Color32(255, 77, 0, byte.MaxValue);

    public static float alertDuration = 3f;
    public static float cooldown = 30f;

    public static bool alertActive;

    private static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.Alert.png", 115f);
        return buttonSprite;
    }

    public static void clearAndReload()
    {
        veteren = null;
        alertActive = false;
        alertDuration = CustomOptionHolder.veterenAlertDuration.getFloat();
        cooldown = CustomOptionHolder.veterenCooldown.getFloat();
    }
}

public static class Medic
{
    public static PlayerControl medic;
    public static PlayerControl shielded;
    public static PlayerControl futureShielded;

    public static Color color = new Color32(126, 251, 194, byte.MaxValue);
    public static bool usedShield;

    public static int showShielded;
    public static bool showAttemptToShielded;
    public static bool showAttemptToMedic;
    public static bool unbreakableShield = true;
    public static bool setShieldAfterMeeting;
    public static bool showShieldAfterMeeting;
    public static bool meetingAfterShielding;
    public static bool reset;

    public static Color shieldedColor = new Color32(0, 221, 255, byte.MaxValue);
    public static PlayerControl currentTarget;

    private static Sprite buttonSprite;

    public static void resetShielded()
    {
        currentTarget = shielded = null;
        usedShield = false;
    }
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.ShieldButton.png", 115f);
        return buttonSprite;
    }
    public static bool shieldVisible(PlayerControl target)
    {
        bool hasVisibleShield = false;

        bool isMorphedMorphling = target == Morphling.morphling && Morphling.morphTarget != null && Morphling.morphTimer > 0f;
        if (Medic.shielded != null && ((target == Medic.shielded && !isMorphedMorphling) || (isMorphedMorphling && Morphling.morphTarget == Medic.shielded)))
        {
            hasVisibleShield = Medic.showShielded == 0 || Helpers.shouldShowGhostInfo() // Everyone or Ghost info
                || (Medic.showShielded == 1 && (PlayerControl.LocalPlayer == Medic.shielded || PlayerControl.LocalPlayer == Medic.medic)) // Shielded + Medic
                || (Medic.showShielded == 2 && PlayerControl.LocalPlayer == Medic.medic); // Medic only
            // Make shield invisible till after the next meeting if the option is set (the medic can already see the shield)
            hasVisibleShield = hasVisibleShield && (Medic.meetingAfterShielding || !Medic.showShieldAfterMeeting || PlayerControl.LocalPlayer == Medic.medic || Helpers.shouldShowGhostInfo());
        }
        return hasVisibleShield;
    }
    public static void clearAndReload()
    {
        medic = null;
        shielded = null;
        futureShielded = null;
        currentTarget = null;
        usedShield = false;
        reset = CustomOptionHolder.medicResetTargetAfterMeeting.getBool();
        showShielded = CustomOptionHolder.medicShowShielded.getSelection();
        showAttemptToShielded = CustomOptionHolder.medicShowAttemptToShielded.getBool();
        //      unbreakableShield = true; //CustomOptionHolder.medicBreakShield.getBool();
        unbreakableShield = CustomOptionHolder.medicBreakShield.getBool();
        showAttemptToMedic = CustomOptionHolder.medicShowAttemptToMedic.getBool();
        setShieldAfterMeeting = CustomOptionHolder.medicSetOrShowShieldAfterMeeting.getSelection() == 2;
        showShieldAfterMeeting = CustomOptionHolder.medicSetOrShowShieldAfterMeeting.getSelection() == 1;
        meetingAfterShielding = false;
    }
}

public static class Swapper
{
    public static PlayerControl swapper;
    public static Color color = new Color32(134, 55, 86, byte.MaxValue);
    private static Sprite spriteCheck;
    public static bool canCallEmergency;
    public static bool canOnlySwapOthers;
    public static int charges;
    public static float rechargeTasksNumber;
    public static bool canFixSabotages;
    public static float rechargedTasks;

    public static byte playerId1 = Byte.MaxValue;
    public static byte playerId2 = Byte.MaxValue;

    public static Sprite getCheckSprite()
    {
        if (spriteCheck) return spriteCheck;
        spriteCheck = loadSpriteFromResources("TheOtherRoles.Resources.SwapperCheck.png", 150f);
        return spriteCheck;
    }

    public static void clearAndReload()
    {
        swapper = null;
        playerId1 = Byte.MaxValue;
        playerId2 = Byte.MaxValue;
        canCallEmergency = CustomOptionHolder.swapperCanCallEmergency.getBool();
        canOnlySwapOthers = CustomOptionHolder.swapperCanOnlySwapOthers.getBool();
        canFixSabotages = CustomOptionHolder.swapperCanFixSabotages.getBool();
        charges = Mathf.RoundToInt(CustomOptionHolder.swapperSwapsNumber.getFloat());
        rechargeTasksNumber = Mathf.RoundToInt(CustomOptionHolder.swapperRechargeTasksNumber.getFloat());
        rechargedTasks = Mathf.RoundToInt(CustomOptionHolder.swapperRechargeTasksNumber.getFloat());
    }
}

public static class Lovers
{
    public static PlayerControl lover1;
    public static PlayerControl lover2;
    public static Color color = new Color32(232, 57, 185, byte.MaxValue);

    public static bool bothDie = true;
    public static bool enableChat = true;
    // Lovers save if next to be exiled is a lover, because RPC of ending game comes before RPC of exiled
    public static bool notAckedExiledIsLover;

    public static bool existing()
    {
        return lover1 != null && lover2 != null && !lover1.Data.Disconnected && !lover2.Data.Disconnected;
    }

    public static bool existingAndAlive()
    {
        return existing() && !lover1.Data.IsDead && !lover2.Data.IsDead && !notAckedExiledIsLover; // ADD NOT ACKED IS LOVER
    }

    public static PlayerControl otherLover(PlayerControl oneLover)
    {
        if (!existingAndAlive()) return null;
        if (oneLover == lover1) return lover2;
        if (oneLover == lover2) return lover1;
        return null;
    }

    public static bool existingWithKiller()
    {
        return existing() && (lover1 == Jackal.jackal || lover2 == Jackal.jackal
                           || lover1 == Sidekick.sidekick || lover2 == Sidekick.sidekick
                           || lover1 == Juggernaut.juggernaut || lover2 == Juggernaut.juggernaut
                           || lover1 == Werewolf.werewolf || lover2 == Werewolf.werewolf
                           || lover1.Data.Role.IsImpostor || lover2.Data.Role.IsImpostor);
    }

    public static bool hasAliveKillingLover(this PlayerControl player)
    {
        if (!existingAndAlive() || !existingWithKiller())
            return false;
        return player != null && (player == lover1 || player == lover2);
    }

    public static void clearAndReload()
    {
        lover1 = null;
        lover2 = null;
        notAckedExiledIsLover = false;
        bothDie = CustomOptionHolder.modifierLoverBothDie.getBool();
        enableChat = CustomOptionHolder.modifierLoverEnableChat.getBool();
    }

    public static PlayerControl getPartner(this PlayerControl player)
    {
        if (player == null)
            return null;
        if (lover1 == player)
            return lover2;
        if (lover2 == player)
            return lover1;
        return null;
    }
}

public static class Seer
{
    public static PlayerControl seer;
    public static Color color = new Color32(97, 178, 108, byte.MaxValue);
    public static List<Vector3> deadBodyPositions = new();

    public static float soulDuration = 15f;
    public static bool limitSoulDuration;
    public static int mode;

    private static Sprite soulSprite;
    public static Sprite getSoulSprite()
    {
        if (soulSprite) return soulSprite;
        soulSprite = loadSpriteFromResources("TheOtherRoles.Resources.Soul.png", 500f);
        return soulSprite;
    }

    public static void clearAndReload()
    {
        seer = null;
        deadBodyPositions = new List<Vector3>();
        limitSoulDuration = CustomOptionHolder.seerLimitSoulDuration.getBool();
        soulDuration = CustomOptionHolder.seerSoulDuration.getFloat();
        mode = CustomOptionHolder.seerMode.getSelection();
    }
}

public static class Morphling
{
    public static PlayerControl morphling;
    public static Color color = Palette.ImpostorRed;
    private static Sprite sampleSprite;
    private static Sprite morphSprite;

    public static float cooldown = 30f;
    public static float duration = 10f;

    public static PlayerControl currentTarget;
    public static PlayerControl sampledTarget;
    public static PlayerControl morphTarget;
    public static float morphTimer;

    public static void resetMorph()
    {
        morphTarget = null;
        morphTimer = 0f;
        if (morphling == null) return;
        morphling.setDefaultLook();
    }

    public static void clearAndReload()
    {
        resetMorph();
        morphling = null;
        currentTarget = null;
        sampledTarget = null;
        morphTarget = null;
        morphTimer = 0f;
        cooldown = CustomOptionHolder.morphlingCooldown.getFloat();
        duration = CustomOptionHolder.morphlingDuration.getFloat();
    }

    public static Sprite getSampleSprite()
    {
        if (sampleSprite) return sampleSprite;
        sampleSprite = loadSpriteFromResources("TheOtherRoles.Resources.SampleButton.png", 115f);
        return sampleSprite;
    }

    public static Sprite getMorphSprite()
    {
        if (morphSprite) return morphSprite;
        morphSprite = loadSpriteFromResources("TheOtherRoles.Resources.MorphButton.png", 115f);
        return morphSprite;
    }
}

public static class Camouflager
{
    public static PlayerControl camouflager;
    public static Color color = Palette.ImpostorRed;

    public static float cooldown = 30f;
    public static float duration = 10f;
    public static float camouflageTimer;
    public static bool camoComms;

    private static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.CamoButton.png", 115f);
        return buttonSprite;
    }

    public static void resetCamouflage()
    {
        if (isCamoComms()) return;
        camouflageTimer = 0f;
        foreach (PlayerControl p in PlayerControl.AllPlayerControls)
        {
            if ((p == Ninja.ninja && Ninja.isInvisble) || (p == Jackal.jackal && Jackal.isInvisable))
                continue;
            p.setDefaultLook();
            camoComms = false;
        }
    }

    public static void clearAndReload()
    {
        resetCamouflage();
        camoComms = false;
        camouflager = null;
        camouflageTimer = 0f;
        cooldown = CustomOptionHolder.camouflagerCooldown.getFloat();
        duration = CustomOptionHolder.camouflagerDuration.getFloat();
    }
}

public static class Hacker
{
    public static PlayerControl hacker;
    public static Minigame vitals;
    public static Minigame doorLog;
    public static Color color = new Color32(117, 250, 76, byte.MaxValue);

    public static float cooldown = 30f;
    public static float duration = 10f;
    public static float toolsNumber = 5f;
    public static bool onlyColorType;
    public static float hackerTimer;
    public static int rechargeTasksNumber = 2;
    public static int rechargedTasks = 2;
    public static int chargesVitals = 1;
    public static int chargesAdminTable = 1;
    public static bool cantMove = true;

    private static Sprite buttonSprite;
    private static Sprite vitalsSprite;
    private static Sprite logSprite;
    private static Sprite adminSprite;

    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.HackerButton.png", 115f);
        return buttonSprite;
    }

    public static Sprite getVitalsSprite()
    {
        if (vitalsSprite) return vitalsSprite;
        vitalsSprite = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.VitalsButton].Image;
        return vitalsSprite;
    }

    public static Sprite getLogSprite()
    {
        if (logSprite) return logSprite;
        logSprite = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.DoorLogsButton].Image;
        return logSprite;
    }

    public static Sprite getAdminSprite()
    {
        byte mapId = GameOptionsManager.Instance.currentNormalGameOptions.MapId;
        UseButtonSettings button = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.PolusAdminButton]; // Polus
        if (IsSkeld || mapId == 3) button = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.AdminMapButton]; // Skeld || Dleks
        else if (IsMira) button = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.MIRAAdminButton]; // Mira HQ
        else if (IsAirship) button = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.AirshipAdminButton]; // Airship
        else if (IsFungle) button = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.AdminMapButton];  // Hacker can Access the Admin panel on Fungle
        adminSprite = button.Image;
        return adminSprite;
    }

    public static void clearAndReload()
    {
        hacker = null;
        vitals = null;
        doorLog = null;
        hackerTimer = 0f;
        adminSprite = null;
        cooldown = CustomOptionHolder.hackerCooldown.getFloat();
        duration = CustomOptionHolder.hackerHackeringDuration.getFloat();
        onlyColorType = CustomOptionHolder.hackerOnlyColorType.getBool();
        toolsNumber = CustomOptionHolder.hackerToolsNumber.getFloat();
        rechargeTasksNumber = Mathf.RoundToInt(CustomOptionHolder.hackerRechargeTasksNumber.getFloat());
        rechargedTasks = Mathf.RoundToInt(CustomOptionHolder.hackerRechargeTasksNumber.getFloat());
        chargesVitals = Mathf.RoundToInt(CustomOptionHolder.hackerToolsNumber.getFloat()) / 2;
        chargesAdminTable = Mathf.RoundToInt(CustomOptionHolder.hackerToolsNumber.getFloat()) / 2;
        cantMove = CustomOptionHolder.hackerNoMove.getBool();
    }
}

public static class Tracker
{
    public static PlayerControl tracker;
    public static Color color = new Color32(100, 58, 220, byte.MaxValue);
    public static List<Arrow> localArrows = new();

    public static float updateIntervall = 5f;
    public static bool resetTargetAfterMeeting;
    public static bool canTrackCorpses;
    public static float corpsesTrackingCooldown = 30f;
    public static float corpsesTrackingDuration = 5f;
    public static float corpsesTrackingTimer;
    public static int trackingMode;
    public static List<Vector3> deadBodyPositions = new();

    public static PlayerControl currentTarget;
    public static PlayerControl tracked;
    public static bool usedTracker;
    public static float timeUntilUpdate;
    public static Arrow arrow = new(Color.blue);

    public static GameObject DangerMeterParent;
    public static DangerMeter Meter;

    private static Sprite trackCorpsesButtonSprite;
    public static Sprite getTrackCorpsesButtonSprite()
    {
        if (trackCorpsesButtonSprite) return trackCorpsesButtonSprite;
        trackCorpsesButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.PathfindButton.png", 115f);
        return trackCorpsesButtonSprite;
    }

    private static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.TrackerButton.png", 115f);
        return buttonSprite;
    }

    public static void resetTracked()
    {
        currentTarget = tracked = null;
        usedTracker = false;
        if (arrow?.arrow != null) Object.Destroy(arrow.arrow);
        arrow = new Arrow(Color.blue);
        if (arrow.arrow != null) arrow.arrow.SetActive(false);
    }

    public static void clearAndReload()
    {
        tracker = null;
        resetTracked();
        timeUntilUpdate = 0f;
        updateIntervall = CustomOptionHolder.trackerUpdateIntervall.getFloat();
        resetTargetAfterMeeting = CustomOptionHolder.trackerResetTargetAfterMeeting.getBool();
        if (localArrows != null)
        {
            foreach (Arrow arrow in localArrows)
                if (arrow?.arrow != null)
                    Object.Destroy(arrow.arrow);
        }
        deadBodyPositions = new List<Vector3>();
        corpsesTrackingTimer = 0f;
        corpsesTrackingCooldown = CustomOptionHolder.trackerCorpsesTrackingCooldown.getFloat();
        corpsesTrackingDuration = CustomOptionHolder.trackerCorpsesTrackingDuration.getFloat();
        canTrackCorpses = CustomOptionHolder.trackerCanTrackCorpses.getBool();
        trackingMode = CustomOptionHolder.trackerTrackingMethod.getSelection();
        if (DangerMeterParent)
        {
            Meter.gameObject.Destroy();
            DangerMeterParent.Destroy();
        }
    }
}

public static class Vampire
{
    public static PlayerControl vampire;
    public static Color color = Palette.ImpostorRed;

    public static float delay = 10f;
    public static float cooldown = 30f;
    public static bool canKillNearGarlics = true;
    public static bool localPlacedGarlic;
    public static bool garlicsActive = true;
    public static bool garlicButton;

    public static PlayerControl currentTarget;
    public static PlayerControl bitten;
    public static bool targetNearGarlic;

    private static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.VampireButton.png", 115f);
        return buttonSprite;
    }

    private static Sprite garlicButtonSprite;
    public static Sprite getGarlicButtonSprite()
    {
        if (garlicButtonSprite) return garlicButtonSprite;
        garlicButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.GarlicButton.png", 115f);
        return garlicButtonSprite;
    }

    public static void clearAndReload()
    {
        vampire = null;
        bitten = null;
        targetNearGarlic = false;
        localPlacedGarlic = false;
        currentTarget = null;
        garlicsActive = CustomOptionHolder.vampireSpawnRate.getSelection() > 0;
        delay = CustomOptionHolder.vampireKillDelay.getFloat();
        cooldown = CustomOptionHolder.vampireCooldown.getFloat();
        canKillNearGarlics = CustomOptionHolder.vampireCanKillNearGarlics.getBool();
        garlicButton = CustomOptionHolder.vampireGarlicButton.getBool();
    }
}

public static class Snitch
{
    public static PlayerControl snitch;
    public static Color color = new Color32(184, 251, 79, byte.MaxValue);

    public static List<Arrow> localArrows = new();
    public static int taskCountForReveal = 1;
    public static bool seeInMeeting;
    //public static bool canSeeRoles = false;
    //public static bool includeTeamJackal = false;
    //public static bool includeNeutralTeam = false;
    public static bool teamNeutraUseDifferentArrowColor = true;

    public enum includeNeutralTeam
    {
        NoIncNeutral = 0,
        KillNeutral = 1,
        EvilNeutral = 2,
        AllNeutral = 3
    }

    public static includeNeutralTeam Team = includeNeutralTeam.KillNeutral;
    public static TextMeshPro text;


    public static void clearAndReload()
    {
        if (localArrows != null)
        {
            foreach (Arrow arrow in localArrows)
                if (arrow?.arrow != null) Object.Destroy(arrow.arrow);
        }
        localArrows = new List<Arrow>();
        taskCountForReveal = Mathf.RoundToInt(CustomOptionHolder.snitchLeftTasksForReveal.getFloat());
        seeInMeeting = CustomOptionHolder.snitchSeeMeeting.getBool();
        if (text != null) Object.Destroy(text);
        text = null;

        //canSeeRoles = CustomOptionHolder.snitchCanSeeRoles.getBool();
        //includeNeutralTeam = CustomOptionHolder.snitchIncludeNeutralTeam.getBool();
        Team = (includeNeutralTeam)CustomOptionHolder.snitchIncludeNeutralTeam.getSelection();
        teamNeutraUseDifferentArrowColor = CustomOptionHolder.snitchTeamNeutraUseDifferentArrowColor.getBool();
        snitch = null;
    }
}

public static class Werewolf
{
    public static PlayerControl werewolf;
    public static PlayerControl currentTarget;
    public static Color color = new Color32(79, 56, 21, byte.MaxValue);

    // Kill Button 
    public static float killCooldown = 3f;

    // Rampage Button
    public static float rampageCooldown = 30f;
    public static float rampageDuration = 5f;
    public static bool canUseVents;
    public static bool canKill;
    public static bool hasImpostorVision;

    public static Sprite buttonSprite;

    public static Sprite getRampageButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.Rampage.png", 115f);
        return buttonSprite;
    }

    public static Vector3 getRampageVector()
    {
        return new Vector3(-2.7f, -0.06f, 0);
    }

    public static void clearAndReload()
    {
        werewolf = null;
        currentTarget = null;
        canUseVents = false;
        canKill = false;
        hasImpostorVision = false;
        rampageCooldown = CustomOptionHolder.werewolfRampageCooldown.getFloat();
        rampageDuration = CustomOptionHolder.werewolfRampageDuration.getFloat();
        killCooldown = CustomOptionHolder.werewolfKillCooldown.getFloat();

    }
}

public class Miner
{
    public static readonly List<Vent> Vents = new();
    public static PlayerControl miner;
    public KillButton _mineButton;
    public static DateTime LastMined;
    public static Sprite buttonSprite;

    public static float cooldown = 30f;
    public static Color color = Palette.ImpostorRed;

    public bool CanPlace { get; set; }
    public static Vector2 VentSize { get; set; }

    public static void clearAndReload()
    {
        miner = null;
        cooldown = CustomOptionHolder.minerCooldown.getFloat();
    }

    public static Sprite getMineButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.Mine.png", 115f);
        return buttonSprite;
    }
}

public static class Jackal
{
    public static PlayerControl jackal;
    public static Color color = new Color32(0, 180, 235, byte.MaxValue);
    //public static Color color = new Color32(224, 197, 219, byte.MaxValue);
    public static PlayerControl fakeSidekick;
    public static PlayerControl currentTarget;
    public static List<PlayerControl> formerJackals = new();

    public static float cooldown = 30f;
    public static float createSidekickCooldown = 30f;
    public static bool canUseVents = true;
    public static bool canCreateSidekick = true;
    public static Sprite buttonSprite;
    public static Sprite swoopButton;
    public static bool jackalPromotedFromSidekickCanCreateSidekick = true;
    public static bool canCreateSidekickFromImpostor = true;
    public static bool killFakeImpostor;
    public static bool hasImpostorVision;
    public static bool ImpostorCanFindSidekick;
    public static bool canSabotage;

    public static bool wasTeamRed;
    public static bool wasImpostor;
    public static bool wasSpy;

    public static float chanceSwoop;
    public static bool canSwoop;
    public static float swoopTimer;
    public static float swoopCooldown = 30f;
    public static float duration = 5f;
    public static bool isInvisable;


    public static Sprite getSidekickButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.SidekickButton.png", 115f);
        return buttonSprite;
    }
    public static Sprite getSwoopButtonSprite()
    {
        if (swoopButton) return swoopButton;
        swoopButton = loadSpriteFromResources("TheOtherRoles.Resources.Swoop.png", 115f);
        return swoopButton;
    }

    public static void setSwoop()
    {
        var chance = canSwoop = rnd.NextDouble() < chanceSwoop;
        var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte)CustomRPC.JackalCanSwooper, SendOption.Reliable);
        writer.Write(chance);
        AmongUsClient.Instance.FinishRpcImmediately(writer);
        RPCProcedure.jackalCanSwooper(chance);
    }

    public static void removeCurrentJackal()
    {
        if (!formerJackals.Any(x => x.PlayerId == jackal.PlayerId)) formerJackals.Add(jackal);
        jackal = null;
        currentTarget = null;
        fakeSidekick = null;
        cooldown = CustomOptionHolder.jackalKillCooldown.getFloat();
        createSidekickCooldown = CustomOptionHolder.jackalCreateSidekickCooldown.getFloat();
    }

    public static void clearAndReload()
    {
        jackal = null;
        currentTarget = null;
        fakeSidekick = null;
        formerJackals.Clear();
        cooldown = CustomOptionHolder.jackalKillCooldown.getFloat();
        createSidekickCooldown = CustomOptionHolder.jackalCreateSidekickCooldown.getFloat();
        canUseVents = CustomOptionHolder.jackalCanUseVents.getBool();
        canSabotage = CustomOptionHolder.jackalCanUseSabo.getBool();
        canCreateSidekick = CustomOptionHolder.jackalCanCreateSidekick.getBool();
        jackalPromotedFromSidekickCanCreateSidekick = CustomOptionHolder.jackalPromotedFromSidekickCanCreateSidekick.getBool();
        ImpostorCanFindSidekick = CustomOptionHolder.jackalImpostorCanFindSidekick.getBool();
        canCreateSidekickFromImpostor = CustomOptionHolder.jackalCanCreateSidekickFromImpostor.getBool();
        killFakeImpostor = CustomOptionHolder.jackalKillFakeImpostor.getBool();
        hasImpostorVision = CustomOptionHolder.jackalAndSidekickHaveImpostorVision.getBool();
        wasTeamRed = wasImpostor = wasSpy = false;

        swoopCooldown = CustomOptionHolder.swooperCooldown.getFloat();
        chanceSwoop = CustomOptionHolder.jackalChanceSwoop.getSelection() / 10f;
        duration = CustomOptionHolder.swooperDuration.getFloat();
        isInvisable = false;
    }

}

public static class Sidekick
{
    public static PlayerControl sidekick;
    public static Color color = new Color32(0, 180, 235, byte.MaxValue);

    public static PlayerControl currentTarget;

    public static bool wasTeamRed;
    public static bool wasImpostor;
    public static bool wasSpy;

    public static float cooldown = 30f;
    public static bool canUseVents = true;
    public static bool canKill = true;
    public static bool promotesToJackal = true;
    public static bool hasImpostorVision;

    public static void clearAndReload()
    {
        sidekick = null;
        currentTarget = null;
        cooldown = CustomOptionHolder.jackalKillCooldown.getFloat();
        canUseVents = CustomOptionHolder.sidekickCanUseVents.getBool();
        canKill = CustomOptionHolder.sidekickCanKill.getBool();
        promotesToJackal = CustomOptionHolder.sidekickPromotesToJackal.getBool();
        hasImpostorVision = CustomOptionHolder.jackalAndSidekickHaveImpostorVision.getBool();
        wasTeamRed = wasImpostor = wasSpy = false;
    }
}

public static class Doomsayer
{
    public static PlayerControl doomsayer;

    public static Color color = new Color32(0, 255, 128, byte.MaxValue);
    public static PlayerControl currentTarget;
    public static List<PlayerControl> playerTargetinformation = new();
    public static float cooldown = 30f;
    public static int formationNum = 5;
    public static bool hasMultipleShotsPerMeeting = true;
    public static bool canGuessNeutral;
    //public static bool canGuessImpostor;
    public static bool triggerDoomsayerrWin;
    public static bool canGuess = true;
    public static bool onlineTarger;
    public static float killToWin = 3;
    public static float killedToWin;
    public static bool CanShoot = true;

    public static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.SeerButton.png", 115f);
        return buttonSprite;
    }

    public static string GetInfo(PlayerControl target)
    {
        try
        {
            var random = new System.Random();
            var allRoleInfo = (onlineTarger ? onlineRoleInfos() : allRoleInfos()).OrderBy(_ => random.Next()).ToList();
            var roleInfoTarget = RoleInfo.getRoleInfoForPlayer(target, false).FirstOrDefault();
            var AllMessage = new List<string>();
            allRoleInfo.Remove(RoleInfo.doomsayer);
            allRoleInfo.Remove(roleInfoTarget);
            if (allRoleInfo.Count < formationNum + 2) return string.Format("doomsayerInfoError".Translate(), formationNum + 2);

            var formation = formationNum;
            var x = random.Next(0, formation);
            var message = new StringBuilder();
            var tempNumList = Enumerable.Range(0, allRoleInfo.Count).ToList();
            var temp = (tempNumList.Count > formation ? tempNumList.Take(formation) : tempNumList).OrderBy(_ => random.Next()).ToList();

            message.AppendLine(string.Format("doomsayerInfo".Translate(), target.Data.PlayerName));

            for (int num = 0, tempNum = 0; num < formation; num++, tempNum++)
            {
                var info = allRoleInfo[temp[tempNum]];

                message.Append(num == x ? roleInfoTarget.name : info.name);
                message.Append(num < formation - 1 ? ", " : ';');
            }

            AllMessage.Add(message.ToString());

            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte)CustomRPC.DoomsayerMeeting, SendOption.Reliable);
            writer.WritePacked(AllMessage.Count);
            AllMessage.Do(writer.Write);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
            playerTargetinformation.Clear();

            return $"{message}";
        }
        catch
        {
            return "Doomsayer Error";
        }
    }

    public static void clearAndReload()
    {
        doomsayer = null;
        currentTarget = null;
        killedToWin = 0;
        canGuess = true;
        triggerDoomsayerrWin = false;
        cooldown = CustomOptionHolder.doomsayerCooldown.getFloat();
        //hasMultipleShotsPerMeeting = CustomOptionHolder.doomsayerHasMultipleShotsPerMeeting.getBool();
        canGuessNeutral = CustomOptionHolder.doomsayerCanGuessNeutral.getBool();
        //canGuessImpostor = CustomOptionHolder.doomsayerCanGuessImpostor.getBool();
        formationNum = (int)CustomOptionHolder.doomsayerDormationNum.getFloat();
        killToWin = CustomOptionHolder.doomsayerKillToWin.getFloat();
        onlineTarger = CustomOptionHolder.doomsayerOnlineTarger.getBool();
    }
}

public static class Eraser
{
    public static PlayerControl eraser;
    public static Color color = Palette.ImpostorRed;

    public static List<byte> alreadyErased = new();

    public static List<PlayerControl> futureErased = new();
    public static PlayerControl currentTarget;
    public static float cooldown = 30f;
    public static bool canEraseAnyone;

    private static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.EraserButton.png", 115f);
        return buttonSprite;
    }

    public static void clearAndReload()
    {
        eraser = null;
        futureErased = new List<PlayerControl>();
        currentTarget = null;
        cooldown = CustomOptionHolder.eraserCooldown.getFloat();
        canEraseAnyone = CustomOptionHolder.eraserCanEraseAnyone.getBool();
        alreadyErased = new List<byte>();
    }
}

public static class Spy
{
    public static PlayerControl spy;
    public static Color color = Palette.ImpostorRed;

    public static bool impostorsCanKillAnyone = true;
    public static bool canEnterVents;
    public static bool hasImpostorVision;

    public static void clearAndReload()
    {
        spy = null;
        impostorsCanKillAnyone = CustomOptionHolder.spyImpostorsCanKillAnyone.getBool();
        canEnterVents = CustomOptionHolder.spyCanEnterVents.getBool();
        hasImpostorVision = CustomOptionHolder.spyHasImpostorVision.getBool();
    }
}

public static class Trickster
{
    public static PlayerControl trickster;
    public static Color color = Palette.ImpostorRed;
    public static float placeBoxCooldown = 30f;
    public static float lightsOutCooldown = 30f;
    public static float lightsOutDuration = 10f;
    public static float lightsOutTimer;

    private static Sprite placeBoxButtonSprite;
    private static Sprite lightOutButtonSprite;
    private static Sprite tricksterVentButtonSprite;

    public static Sprite getPlaceBoxButtonSprite()
    {
        if (placeBoxButtonSprite) return placeBoxButtonSprite;
        placeBoxButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.PlaceJackInTheBoxButton.png", 115f);
        return placeBoxButtonSprite;
    }

    public static Sprite getLightsOutButtonSprite()
    {
        if (lightOutButtonSprite) return lightOutButtonSprite;
        lightOutButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.LightsOutButton.png", 115f);
        return lightOutButtonSprite;
    }

    public static Sprite getTricksterVentButtonSprite()
    {
        if (tricksterVentButtonSprite) return tricksterVentButtonSprite;
        tricksterVentButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.TricksterVentButton.png", 115f);
        return tricksterVentButtonSprite;
    }

    public static void clearAndReload()
    {
        trickster = null;
        lightsOutTimer = 0f;
        placeBoxCooldown = CustomOptionHolder.tricksterPlaceBoxCooldown.getFloat();
        lightsOutCooldown = CustomOptionHolder.tricksterLightsOutCooldown.getFloat();
        lightsOutDuration = CustomOptionHolder.tricksterLightsOutDuration.getFloat();
        JackInTheBox.UpdateStates(); // if the role is erased, we might have to update the state of the created objects
    }

}

public static class Cleaner
{
    public static PlayerControl cleaner;
    public static Color color = Palette.ImpostorRed;

    public static float cooldown = 30f;

    private static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.CleanButton.png", 115f);
        return buttonSprite;
    }

    public static void clearAndReload()
    {
        cleaner = null;
        cooldown = CustomOptionHolder.cleanerCooldown.getFloat();
    }
}

public static class Bomber
{
    public static PlayerControl bomber;
    public static Color color = Palette.ImpostorRed;
    public static Color alertColor = Palette.ImpostorRed;

    public static float cooldown = 30f;
    public static float bombDelay = 10f;
    public static float bombTimer = 10f;
    public static bool bombActive;
    public static bool triggerBothCooldowns;
    public static bool canGiveToBomber;
    public static bool hotPotatoMode = false;
    public static PlayerControl currentBombTarget;
    public static bool hasAlerted;
    public static int timeLeft;
    public static PlayerControl currentTarget;
    public static PlayerControl hasBombPlayer;


    private static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.Bomber2.png", 115f);
        return buttonSprite;
    }

    public static void clearAndReload()
    {
        bomber = null;
        bombActive = false;
        hasBombPlayer = null;
        cooldown = CustomOptionHolder.bomberBombCooldown.getFloat();
        bombDelay = CustomOptionHolder.bomberDelay.getFloat();
        bombTimer = CustomOptionHolder.bomberTimer.getFloat();
        triggerBothCooldowns = CustomOptionHolder.bomberTriggerBothCooldowns.getBool();
        canGiveToBomber = CustomOptionHolder.bomberCanGiveToBomber.getBool();
        hotPotatoMode = CustomOptionHolder.bomberHotPotatoMode.getBool();
    }
}

public static class Undertaker
{
    public static PlayerControl undertaker;
    public static Color color = Palette.ImpostorRed;

    public static float dragingDelaiAfterKill;

    public static bool isDraging;
    public static DeadBody deadBodyDraged;
    public static bool canDragAndVent;

    public static float velocity = 1;

    private static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.UndertakerDragButton.png", 115f);
        return buttonSprite;
    }

    public static void clearAndReload()
    {
        undertaker = null;
        isDraging = false;
        canDragAndVent = CustomOptionHolder.undertakerCanDragAndVent.getBool();
        deadBodyDraged = null;
        velocity = CustomOptionHolder.undertakerDragingAfterVelocity.getFloat();
        dragingDelaiAfterKill = CustomOptionHolder.undertakerDragingDelaiAfterKill.getFloat();
    }
}

public static class Poucher
{
    public static PlayerControl poucher;
    public static Color color = Palette.ImpostorRed;
    public static List<PlayerControl> killed = new();



    public static void clearAndReload(bool clearList = true)
    {
        poucher = null;
        if (clearList) killed = new List<PlayerControl>();

    }
}

public static class Mimic
{
    public static PlayerControl mimic;
    public static bool hasMimic;
    public static Color color = Palette.ImpostorRed;
    public static List<PlayerControl> killed = new();



    public static void clearAndReload(bool clearList = true)
    {
        mimic = null;
        if (clearList) hasMimic = false;

    }
}

public static class Warlock
{

    public static PlayerControl warlock;
    public static Color color = Palette.ImpostorRed;

    public static PlayerControl currentTarget;
    public static PlayerControl curseVictim;
    public static PlayerControl curseVictimTarget;

    public static float cooldown = 30f;
    public static float rootTime = 5f;

    private static Sprite curseButtonSprite;
    private static Sprite curseKillButtonSprite;

    public static Sprite getCurseButtonSprite()
    {
        if (curseButtonSprite) return curseButtonSprite;
        curseButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.CurseButton.png", 115f);
        return curseButtonSprite;
    }

    public static Sprite getCurseKillButtonSprite()
    {
        if (curseKillButtonSprite) return curseKillButtonSprite;
        curseKillButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.CurseKillButton.png", 115f);
        return curseKillButtonSprite;
    }

    public static void clearAndReload()
    {
        warlock = null;
        currentTarget = null;
        curseVictim = null;
        curseVictimTarget = null;
        cooldown = CustomOptionHolder.warlockCooldown.getFloat();
        rootTime = CustomOptionHolder.warlockRootTime.getFloat();
    }

    public static void resetCurse()
    {
        HudManagerStartPatch.warlockCurseButton.Timer = HudManagerStartPatch.warlockCurseButton.MaxTimer;
        HudManagerStartPatch.warlockCurseButton.Sprite = getCurseButtonSprite();
        HudManagerStartPatch.warlockCurseButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
        currentTarget = null;
        curseVictim = null;
        curseVictimTarget = null;
    }
}

public static class Yoyo
{
    public static PlayerControl yoyo;
    public static Color color = Palette.ImpostorRed;

    public static float blinkDuration;
    public static float markCooldown;
    public static bool markStaysOverMeeting;
    public static bool hasAdminTable;
    public static float adminCooldown;
    public static float SilhouetteVisibility => (silhouetteVisibility == 0 && (PlayerControl.LocalPlayer == yoyo || PlayerControl.LocalPlayer.Data.IsDead)) ? 0.1f : silhouetteVisibility;
    public static float silhouetteVisibility;

    public static Vector3? markedLocation;

    private static Sprite markButtonSprite;

    public static Sprite getMarkButtonSprite()
    {
        if (markButtonSprite) return markButtonSprite;
        markButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.YoyoMarkButtonSprite.png", 115f);
        return markButtonSprite;
    }
    private static Sprite blinkButtonSprite;

    public static Sprite getBlinkButtonSprite()
    {
        if (blinkButtonSprite) return blinkButtonSprite;
        blinkButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.YoyoBlinkButtonSprite.png", 115f);
        return blinkButtonSprite;
    }

    public static void markLocation(Vector3 position)
    {
        markedLocation = position;
    }

    public static void clearAndReload()
    {
        blinkDuration = CustomOptionHolder.yoyoBlinkDuration.getFloat();
        markCooldown = CustomOptionHolder.yoyoMarkCooldown.getFloat();
        markStaysOverMeeting = CustomOptionHolder.yoyoMarkStaysOverMeeting.getBool();
        hasAdminTable = CustomOptionHolder.yoyoHasAdminTable.getBool();
        adminCooldown = CustomOptionHolder.yoyoAdminTableCooldown.getFloat();
        silhouetteVisibility = CustomOptionHolder.yoyoSilhouetteVisibility.getSelection() / 10f;

        markedLocation = null;

    }
}


public static class SecurityGuard
{
    public static PlayerControl securityGuard;
    public static Color color = new Color32(195, 178, 95, byte.MaxValue);

    public static float cooldown = 30f;
    public static int remainingScrews = 7;
    public static int totalScrews = 7;
    public static int ventPrice = 1;
    public static int camPrice = 2;
    public static int placedCameras;
    public static float duration = 10f;
    public static int maxCharges = 5;
    public static int rechargeTasksNumber = 3;
    public static int rechargedTasks = 3;
    public static int charges = 1;
    public static bool cantMove = true;
    public static Vent ventTarget;
    public static Minigame minigame;

    private static Sprite closeVentButtonSprite;
    public static Sprite getCloseVentButtonSprite()
    {
        if (closeVentButtonSprite) return closeVentButtonSprite;
        closeVentButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.CloseVentButton.png", 115f);
        return closeVentButtonSprite;
    }

    private static Sprite placeCameraButtonSprite;
    public static Sprite getPlaceCameraButtonSprite()
    {
        if (placeCameraButtonSprite) return placeCameraButtonSprite;
        placeCameraButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.PlaceCameraButton.png", 115f);
        return placeCameraButtonSprite;
    }

    private static Sprite animatedVentSealedSprite;
    private static float lastPPU;
    public static Sprite getAnimatedVentSealedSprite()
    {
        float ppu = 185f;
        if (SubmergedCompatibility.IsSubmerged) ppu = 120f;
        if (lastPPU != ppu)
        {
            animatedVentSealedSprite = null;
            lastPPU = ppu;
        }
        if (animatedVentSealedSprite) return animatedVentSealedSprite;
        animatedVentSealedSprite = loadSpriteFromResources("TheOtherRoles.Resources.AnimatedVentSealed.png", ppu);
        return animatedVentSealedSprite;
    }

    private static Sprite staticVentSealedSprite;
    public static Sprite getStaticVentSealedSprite()
    {
        if (staticVentSealedSprite) return staticVentSealedSprite;
        staticVentSealedSprite = loadSpriteFromResources("TheOtherRoles.Resources.StaticVentSealed.png", 160f);
        return staticVentSealedSprite;
    }

    private static Sprite fungleVentSealedSprite;
    public static Sprite getFungleVentSealedSprite()
    {
        if (fungleVentSealedSprite) return fungleVentSealedSprite;
        fungleVentSealedSprite = loadSpriteFromResources("TheOtherRoles.Resources.FungleVentSealed.png", 160f);
        return fungleVentSealedSprite;
    }


    private static Sprite submergedCentralUpperVentSealedSprite;
    public static Sprite getSubmergedCentralUpperSealedSprite()
    {
        if (submergedCentralUpperVentSealedSprite) return submergedCentralUpperVentSealedSprite;
        submergedCentralUpperVentSealedSprite = loadSpriteFromResources("TheOtherRoles.Resources.CentralUpperBlocked.png", 145f);
        return submergedCentralUpperVentSealedSprite;
    }

    private static Sprite submergedCentralLowerVentSealedSprite;
    public static Sprite getSubmergedCentralLowerSealedSprite()
    {
        if (submergedCentralLowerVentSealedSprite) return submergedCentralLowerVentSealedSprite;
        submergedCentralLowerVentSealedSprite = loadSpriteFromResources("TheOtherRoles.Resources.CentralLowerBlocked.png", 145f);
        return submergedCentralLowerVentSealedSprite;
    }

    private static Sprite camSprite;
    public static Sprite getCamSprite()
    {
        if (camSprite) return camSprite;
        camSprite = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.CamsButton].Image;
        return camSprite;
    }

    private static Sprite logSprite;
    public static Sprite getLogSprite()
    {
        if (logSprite) return logSprite;
        logSprite = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.DoorLogsButton].Image;
        return logSprite;
    }

    public static void clearAndReload()
    {
        securityGuard = null;
        ventTarget = null;
        minigame = null;
        duration = CustomOptionHolder.securityGuardCamDuration.getFloat();
        maxCharges = Mathf.RoundToInt(CustomOptionHolder.securityGuardCamMaxCharges.getFloat());
        rechargeTasksNumber = Mathf.RoundToInt(CustomOptionHolder.securityGuardCamRechargeTasksNumber.getFloat());
        rechargedTasks = Mathf.RoundToInt(CustomOptionHolder.securityGuardCamRechargeTasksNumber.getFloat());
        charges = Mathf.RoundToInt(CustomOptionHolder.securityGuardCamMaxCharges.getFloat()) / 2;
        placedCameras = 0;
        cooldown = CustomOptionHolder.securityGuardCooldown.getFloat();
        totalScrews = remainingScrews = Mathf.RoundToInt(CustomOptionHolder.securityGuardTotalScrews.getFloat());
        camPrice = Mathf.RoundToInt(CustomOptionHolder.securityGuardCamPrice.getFloat());
        ventPrice = Mathf.RoundToInt(CustomOptionHolder.securityGuardVentPrice.getFloat());
        cantMove = CustomOptionHolder.securityGuardNoMove.getBool();
    }
}

public static class Arsonist
{
    public static PlayerControl arsonist;
    public static Color color = new Color32(238, 112, 46, byte.MaxValue);

    public static float cooldown = 30f;
    public static float duration = 3f;
    public static bool triggerArsonistWin;

    public static PlayerControl currentTarget;
    public static PlayerControl douseTarget;
    public static List<PlayerControl> dousedPlayers = new();

    private static Sprite douseSprite;
    public static Sprite getDouseSprite()
    {
        if (douseSprite) return douseSprite;
        douseSprite = loadSpriteFromResources("TheOtherRoles.Resources.DouseButton.png", 115f);
        return douseSprite;
    }

    private static Sprite igniteSprite;
    public static Sprite getIgniteSprite()
    {
        if (igniteSprite) return igniteSprite;
        igniteSprite = loadSpriteFromResources("TheOtherRoles.Resources.IgniteButton.png", 115f);
        return igniteSprite;
    }

    public static bool dousedEveryoneAlive()
    {
        return PlayerControl.AllPlayerControls.ToArray().All(x => { return x == arsonist || x.Data.IsDead || x.Data.Disconnected || dousedPlayers.Any(y => y.PlayerId == x.PlayerId); });
    }

    public static void clearAndReload()
    {
        arsonist = null;
        currentTarget = null;
        douseTarget = null;
        triggerArsonistWin = false;
        dousedPlayers = new List<PlayerControl>();
        foreach (PoolablePlayer p in TORMapOptions.playerIcons.Values)
        {
            if (p != null && p.gameObject != null) p.gameObject.SetActive(false);
        }
        cooldown = CustomOptionHolder.arsonistCooldown.getFloat();
        duration = CustomOptionHolder.arsonistDuration.getFloat();
    }
}

public static class Guesser
{
    public static PlayerControl niceGuesser;
    //public static PlayerControl evilGuesser;
    public static List<PlayerControl> evilGuesser = new();
    public static Color color = new Color32(255, 255, 0, byte.MaxValue);

    public static int remainingShotsEvilGuesser = 2;
    public static int remainingShotsNiceGuesser = 2;
    public static bool hasMultipleShotsPerMeeting;
    public static bool assassinMultipleShotsPerMeeting;
    public static bool showInfoInGhostChat = true;
    public static bool killsThroughShield = true;
    public static bool assassinKillsThroughShield = true;
    public static bool evilGuesserCanGuessSpy = true;
    public static bool guesserCantGuessSnitch;
    public static bool evilGuesserCanGuessCrewmate = true;

    public static bool isGuesser(byte playerId)
    {
        foreach (PlayerControl item in evilGuesser)
        {
            if (item.PlayerId == playerId && evilGuesser != null)
            {
                return true;
            }
        }
        if (niceGuesser != null && niceGuesser.PlayerId == playerId)
        {
            return true;
        }
        return false;
    }

    public static void clear(byte playerId)
    {
        if (niceGuesser != null && niceGuesser.PlayerId == playerId)
        {
            niceGuesser = null;
        }
        foreach (PlayerControl item in evilGuesser)
        {
            if (item.PlayerId == playerId && evilGuesser != null)
            {
                evilGuesser = null;
            }
        }
    }

    public static int remainingShots(byte playerId, bool shoot = false)
    {
        int result = remainingShotsEvilGuesser;
        if (niceGuesser != null && niceGuesser.PlayerId == playerId)
        {
            result = remainingShotsNiceGuesser;
            if (shoot)
            {
                remainingShotsNiceGuesser = Mathf.Max(0, remainingShotsNiceGuesser - 1);
            }
        }
        else if (shoot)
        {
            remainingShotsEvilGuesser = Mathf.Max(0, remainingShotsEvilGuesser - 1);
        }
        return result;
    }

    public static void clearAndReload()
    {
        niceGuesser = null;
        evilGuesser = new List<PlayerControl>();
        guesserCantGuessSnitch = CustomOptionHolder.guesserCantGuessSnitchIfTaksDone.getBool();
        remainingShotsEvilGuesser = Mathf.RoundToInt(CustomOptionHolder.modifierAssassinNumberOfShots.getFloat());
        remainingShotsNiceGuesser = Mathf.RoundToInt(CustomOptionHolder.guesserNumberOfShots.getFloat());
        hasMultipleShotsPerMeeting = CustomOptionHolder.guesserHasMultipleShotsPerMeeting.getBool();
        assassinMultipleShotsPerMeeting = CustomOptionHolder.modifierAssassinMultipleShotsPerMeeting.getBool();
        showInfoInGhostChat = CustomOptionHolder.guesserShowInfoInGhostChat.getBool();
        killsThroughShield = CustomOptionHolder.guesserKillsThroughShield.getBool();
        assassinKillsThroughShield = CustomOptionHolder.modifierAssassinKillsThroughShield.getBool();
        evilGuesserCanGuessSpy = CustomOptionHolder.guesserEvilCanKillSpy.getBool();
        evilGuesserCanGuessCrewmate = CustomOptionHolder.guesserEvilCanKillCrewmate.getBool();
    }
}

public static class BountyHunter
{
    public static PlayerControl bountyHunter;
    public static Color color = Palette.ImpostorRed;

    public static Arrow arrow;
    public static float bountyDuration = 30f;
    public static bool showArrow = true;
    public static float bountyKillCooldown;
    public static float punishmentTime = 15f;
    public static float arrowUpdateIntervall = 10f;

    public static float arrowUpdateTimer;
    public static float bountyUpdateTimer;
    public static PlayerControl bounty;
    public static TextMeshPro cooldownText;

    public static void clearAndReload()
    {
        arrow = new Arrow(color);
        bountyHunter = null;
        bounty = null;
        arrowUpdateTimer = 0f;
        bountyUpdateTimer = 0f;
        if (arrow != null && arrow.arrow != null) Object.Destroy(arrow.arrow);
        arrow = null;
        if (cooldownText != null && cooldownText.gameObject != null) Object.Destroy(cooldownText.gameObject);
        cooldownText = null;
        foreach (PoolablePlayer p in TORMapOptions.playerIcons.Values)
        {
            if (p != null && p.gameObject != null) p.gameObject.SetActive(false);
        }


        bountyDuration = CustomOptionHolder.bountyHunterBountyDuration.getFloat();
        bountyKillCooldown = CustomOptionHolder.bountyHunterReducedCooldown.getFloat();
        punishmentTime = CustomOptionHolder.bountyHunterPunishmentTime.getFloat();
        showArrow = CustomOptionHolder.bountyHunterShowArrow.getBool();
        arrowUpdateIntervall = CustomOptionHolder.bountyHunterArrowUpdateIntervall.getFloat();
    }
}

public static class Juggernaut
{
    public static PlayerControl juggernaut;
    public static Color color = new Color32(140, 0, 77, byte.MaxValue);
    public static PlayerControl currentTarget;

    public static float cooldown = 30f;
    public static float reducedkill = 5f;
    public static bool hasImpostorVision;
    public static bool canVent;

    public static void setkill()
    {
        cooldown -= reducedkill;
        if (cooldown <= 0f) cooldown = 0f;
    }

    public static void clearAndReload()
    {
        juggernaut = null;
        currentTarget = null;
        hasImpostorVision = CustomOptionHolder.juggernautHasImpVision.getBool();
        canVent = CustomOptionHolder.juggernautCanVent.getBool();
        cooldown = CustomOptionHolder.juggernautCooldown.getFloat();
        reducedkill = CustomOptionHolder.juggernautReducedkillEach.getFloat();
    }
}

public static class Vulture
{
    public static PlayerControl vulture;
    public static Color color = new Color32(139, 69, 19, byte.MaxValue);
    public static List<Arrow> localArrows = new();
    public static float cooldown = 30f;
    public static int vultureNumberToWin = 4;
    public static int eatenBodies;
    public static bool triggerVultureWin;
    public static bool canUseVents = true;
    public static bool showArrows = true;
    private static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.VultureButton.png", 115f);
        return buttonSprite;
    }

    public static void clearAndReload()
    {
        vulture = null;
        vultureNumberToWin = Mathf.RoundToInt(CustomOptionHolder.vultureNumberToWin.getFloat());
        eatenBodies = 0;
        cooldown = CustomOptionHolder.vultureCooldown.getFloat();
        triggerVultureWin = false;
        canUseVents = CustomOptionHolder.vultureCanUseVents.getBool();
        showArrows = CustomOptionHolder.vultureShowArrows.getBool();
        if (localArrows != null)
        {
            foreach (Arrow arrow in localArrows)
                if (arrow?.arrow != null)
                    Object.Destroy(arrow.arrow);
        }
        localArrows = new List<Arrow>();
    }
}


public static class Medium
{
    public static PlayerControl medium;
    public static DeadPlayer target;
    public static DeadPlayer soulTarget;
    public static Color color = new Color32(98, 120, 115, byte.MaxValue);
    public static List<Tuple<DeadPlayer, Vector3>> deadBodies = new();
    public static List<Tuple<DeadPlayer, Vector3>> futureDeadBodies = new();
    public static List<SpriteRenderer> souls = new();
    public static DateTime meetingStartTime = DateTime.UtcNow;

    public static float cooldown = 30f;
    public static float duration = 3f;
    public static bool oneTimeUse;
    public static float chanceAdditionalInfo;

    private static Sprite soulSprite;

    enum SpecialMediumInfo
    {
        SheriffSuicide,
        ThiefSuicide,
        ActiveLoverDies,
        PassiveLoverSuicide,
        LawyerKilledByClient,
        JackalKillsSidekick,
        ImpostorTeamkill,
        SubmergedO2,
        WarlockSuicide,
        BodyCleaned,
    }

    public static Sprite getSoulSprite()
    {
        if (soulSprite) return soulSprite;
        soulSprite = loadSpriteFromResources("TheOtherRoles.Resources.Soul.png", 500f);
        return soulSprite;
    }

    private static Sprite question;
    public static Sprite getQuestionSprite()
    {
        if (question) return question;
        question = loadSpriteFromResources("TheOtherRoles.Resources.MediumButton.png", 115f);
        return question;
    }

    public static void clearAndReload()
    {
        medium = null;
        target = null;
        soulTarget = null;
        deadBodies = new List<Tuple<DeadPlayer, Vector3>>();
        futureDeadBodies = new List<Tuple<DeadPlayer, Vector3>>();
        souls = new List<SpriteRenderer>();
        meetingStartTime = DateTime.UtcNow;
        cooldown = CustomOptionHolder.mediumCooldown.getFloat();
        duration = CustomOptionHolder.mediumDuration.getFloat();
        oneTimeUse = CustomOptionHolder.mediumOneTimeUse.getBool();
        chanceAdditionalInfo = CustomOptionHolder.mediumChanceAdditionalInfo.getSelection() / 10f;
    }

    public static string getInfo(PlayerControl target, PlayerControl killer, DeadPlayer.CustomDeathReason deathReason)
    {
        string msg = "";

        List<SpecialMediumInfo> infos = new();
        // collect fitting death info types.
        // suicides:
        if (killer == target)
        {
            if ((target == Sheriff.sheriff || target == Sheriff.formerSheriff) && deathReason != DeadPlayer.CustomDeathReason.LoverSuicide) infos.Add(SpecialMediumInfo.SheriffSuicide);
            if (target == Lovers.lover1 || target == Lovers.lover2) infos.Add(SpecialMediumInfo.PassiveLoverSuicide);
            if (target == Thief.thief && deathReason != DeadPlayer.CustomDeathReason.LoverSuicide) infos.Add(SpecialMediumInfo.ThiefSuicide);
            if (target == Warlock.warlock && deathReason != DeadPlayer.CustomDeathReason.LoverSuicide) infos.Add(SpecialMediumInfo.WarlockSuicide);
        }
        else
        {
            if (target == Lovers.lover1 || target == Lovers.lover2) infos.Add(SpecialMediumInfo.ActiveLoverDies);
            if (target.Data.Role.IsImpostor && killer.Data.Role.IsImpostor && Thief.formerThief != killer) infos.Add(SpecialMediumInfo.ImpostorTeamkill);
        }
        if (target == Sidekick.sidekick && (killer == Jackal.jackal || Jackal.formerJackals.Any(x => x.PlayerId == killer.PlayerId))) infos.Add(SpecialMediumInfo.JackalKillsSidekick);
        if (target == Lawyer.lawyer && killer == Lawyer.target) infos.Add(SpecialMediumInfo.LawyerKilledByClient);
        if (Medium.target.wasCleaned) infos.Add(SpecialMediumInfo.BodyCleaned);

        if (infos.Count > 0)
        {
            var selectedInfo = infos[rnd.Next(infos.Count)];
            switch (selectedInfo)
            {
                case SpecialMediumInfo.SheriffSuicide:
                    msg = getString("mediumSheriffSuicide");
                    break;
                case SpecialMediumInfo.WarlockSuicide:
                    msg = getString("mediumWarlockSuicide");
                    break;
                case SpecialMediumInfo.ThiefSuicide:
                    msg = getString("mediumThiefSuicide");
                    break;
                case SpecialMediumInfo.ActiveLoverDies:
                    msg = getString("mediumActiveLoverDies");
                    break;
                case SpecialMediumInfo.PassiveLoverSuicide:
                    msg = getString("mediumPassiveLoverSuicide");
                    break;
                case SpecialMediumInfo.LawyerKilledByClient:
                    msg = getString("mediumLawyerKilledByClient");
                    break;
                case SpecialMediumInfo.JackalKillsSidekick:
                    msg = getString("mediumJackalKillsSidekick");
                    break;
                case SpecialMediumInfo.ImpostorTeamkill:
                    msg = getString("mediumImpostorTeamkill");
                    break;
                case SpecialMediumInfo.BodyCleaned:
                    msg = getString("mediumBodyCleaned");
                    break;
            }
        }
        else
        {
            int randomNumber = rnd.Next(4);
            string typeOfColor = isLighterColor(Medium.target.killerIfExisting) ? ModTranslation.getString("mediumSoulPlayerLighter") : ModTranslation.getString("mediumSoulPlayerDarker"); ;
            float timeSinceDeath = (float)(meetingStartTime - Medium.target.timeOfDeath).TotalMilliseconds;
            var roleString = RoleInfo.GetRolesString(Medium.target.player, false);
            var roleInfo = RoleInfo.getRoleInfoForPlayer(Medium.target.player); 
            if (randomNumber == 0)
            {
                if (!roleInfo.Contains(RoleInfo.impostor) && !roleInfo.Contains(RoleInfo.crewmate)) msg = string.Format(ModTranslation.getString("mediumQuestion1"), RoleInfo.GetRolesString(Medium.target.player, false));
                else msg = string.Format(ModTranslation.getString("mediumQuestion5"), roleString);
            }
            else if (randomNumber == 1) msg = string.Format(ModTranslation.getString("mediumQuestion2"), typeOfColor);
            else if (randomNumber == 2) msg = string.Format(ModTranslation.getString("mediumQuestion3"), Math.Round(timeSinceDeath / 1000));
            else msg = string.Format(ModTranslation.getString("mediumQuestion4"), RoleInfo.GetRolesString(Medium.target.killerIfExisting, false, false, true)) + ".";
        }

        if (rnd.NextDouble() < chanceAdditionalInfo)
        {
            int count = 0;
            string condition = "";
            var alivePlayersList = PlayerControl.AllPlayerControls.ToArray().Where(pc => !pc.Data.IsDead);
            switch (rnd.Next(3))
            {
                case 0:
                    count = alivePlayersList.Where(pc => pc.Data.Role.IsImpostor || new List<RoleInfo>() { RoleInfo.jackal, RoleInfo.sidekick, RoleInfo.sheriff, RoleInfo.thief }.Contains(RoleInfo.getRoleInfoForPlayer(pc, false).FirstOrDefault())).Count();
                    condition = ModTranslation.getString($"mediumKiller{(count == 1 ? "" : "Plural")}");
                    break;
                case 1:
                    count = alivePlayersList.Where(Helpers.roleCanUseVents).Count();
                    condition = ModTranslation.getString($"mediumPlayerUseVents{(count == 1 ? "" : "Plural")}");
                    break;
                case 2:
                    count = alivePlayersList.Where(pc => isNeutral(pc) && pc != Jackal.jackal && pc != Sidekick.sidekick && pc != Thief.thief).Count();
                    condition = ModTranslation.getString($"mediumPlayerNeutral{(count == 1 ? "" : "Plural")}");
                    break;
                case 3:
                    //count = alivePlayersList.Where(pc =>
                    break;
            }
            msg += $"\n" + string.Format(ModTranslation.getString("mediumAskPrefix"), string.Format(ModTranslation.getString($"mediumStillAlive{(count == 1 ? "" : "Plural")}"), string.Format(condition, count)));
        }

        return string.Format(ModTranslation.getString("mediumSoulPlayerPrefix"), Medium.target.player.Data.PlayerName) + msg;
    }
}

public static class Lawyer
{
    public static PlayerControl lawyer;
    public static PlayerControl target;
    public static Color color = new Color32(134, 153, 25, byte.MaxValue);
    public static Sprite targetSprite;
    public static bool triggerProsecutorWin;
    public static bool isProsecutor;
    public static bool canCallEmergency = true;
    public static bool targetKnows;

    public static float vision = 1f;
    public static bool lawyerKnowsRole;
    public static bool targetCanBeJester;
    public static bool targetWasGuessed;

    public static Sprite getTargetSprite()
    {
        if (targetSprite) return targetSprite;
        targetSprite = loadSpriteFromResources("", 150f);
        return targetSprite;
    }

    public static void clearAndReload(bool clearTarget = true)
    {
        lawyer = null;
        if (clearTarget)
        {
            target = null;
            targetWasGuessed = false;
        }
        isProsecutor = false;
        triggerProsecutorWin = false;
        vision = CustomOptionHolder.lawyerVision.getFloat();
        targetKnows = CustomOptionHolder.lawyerTargetKnows.getBool();
        lawyerKnowsRole = CustomOptionHolder.lawyerKnowsRole.getBool();
        targetCanBeJester = CustomOptionHolder.lawyerTargetCanBeJester.getBool();
        canCallEmergency = CustomOptionHolder.lawyerCanCallEmergency.getBool();
    }
}

public static class Pursuer
{
    public static PlayerControl pursuer;
    public static PlayerControl target;
    public static Color color = Lawyer.color;
    public static List<PlayerControl> blankedList = new();
    public static int blanks;
    public static Sprite blank;
    public static bool notAckedExiled;

    public static float cooldown = 30f;
    public static int blanksNumber = 5;

    public static Sprite getTargetSprite()
    {
        if (blank) return blank;
        blank = loadSpriteFromResources("TheOtherRoles.Resources.PursuerButton.png", 115f);
        return blank;
    }

    public static void clearAndReload()
    {
        pursuer = null;
        target = null;
        blankedList = new List<PlayerControl>();
        blanks = 0;
        notAckedExiled = false;

        cooldown = CustomOptionHolder.pursuerCooldown.getFloat();
        blanksNumber = Mathf.RoundToInt(CustomOptionHolder.pursuerBlanksNumber.getFloat());
    }
}

public static class Witch
{
    public static PlayerControl witch;
    public static Color color = Palette.ImpostorRed;

    public static List<PlayerControl> futureSpelled = new();
    public static PlayerControl currentTarget;
    public static PlayerControl spellCastingTarget;
    public static float cooldown = 30f;
    public static float spellCastingDuration = 2f;
    public static float cooldownAddition = 10f;
    public static float currentCooldownAddition;
    public static bool canSpellAnyone;
    public static bool triggerBothCooldowns = true;
    public static bool witchVoteSavesTargets = true;

    private static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.SpellButton.png", 115f);
        return buttonSprite;
    }

    private static Sprite spelledOverlaySprite;
    public static Sprite getSpelledOverlaySprite()
    {
        if (spelledOverlaySprite) return spelledOverlaySprite;
        spelledOverlaySprite = loadSpriteFromResources("TheOtherRoles.Resources.SpellButtonMeeting.png", 225f);
        return spelledOverlaySprite;
    }


    public static void clearAndReload()
    {
        witch = null;
        futureSpelled = new List<PlayerControl>();
        currentTarget = spellCastingTarget = null;
        cooldown = CustomOptionHolder.witchCooldown.getFloat();
        cooldownAddition = CustomOptionHolder.witchAdditionalCooldown.getFloat();
        currentCooldownAddition = 0f;
        canSpellAnyone = CustomOptionHolder.witchCanSpellAnyone.getBool();
        spellCastingDuration = CustomOptionHolder.witchSpellCastingDuration.getFloat();
        triggerBothCooldowns = CustomOptionHolder.witchTriggerBothCooldowns.getBool();
        witchVoteSavesTargets = CustomOptionHolder.witchVoteSavesTargets.getBool();
    }
}

public static class Ninja
{
    public static PlayerControl ninja;
    public static Color color = Palette.ImpostorRed;

    public static PlayerControl ninjaMarked;
    public static PlayerControl currentTarget;
    public static float cooldown = 30f;
    public static float traceTime = 1f;
    public static bool knowsTargetLocation;
    public static float invisibleDuration = 5f;

    public static float invisibleTimer;
    public static bool isInvisble;
    private static Sprite markButtonSprite;
    private static Sprite killButtonSprite;
    public static Arrow arrow = new(Color.black);
    public static Sprite getMarkButtonSprite()
    {
        if (markButtonSprite) return markButtonSprite;
        markButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.NinjaMarkButton.png", 115f);
        return markButtonSprite;
    }

    public static Sprite getKillButtonSprite()
    {
        if (killButtonSprite) return killButtonSprite;
        killButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.NinjaAssassinateButton.png", 115f);
        return killButtonSprite;
    }

    public static void clearAndReload()
    {
        ninja = null;
        currentTarget = ninjaMarked = null;
        cooldown = CustomOptionHolder.ninjaCooldown.getFloat();
        knowsTargetLocation = CustomOptionHolder.ninjaKnowsTargetLocation.getBool();
        traceTime = CustomOptionHolder.ninjaTraceTime.getFloat();
        invisibleDuration = CustomOptionHolder.ninjaInvisibleDuration.getFloat();
        invisibleTimer = 0f;
        isInvisble = false;
        if (arrow?.arrow != null) Object.Destroy(arrow.arrow);
        arrow = new Arrow(Color.black);
        if (arrow.arrow != null) arrow.arrow.SetActive(false);
    }
}

public static class Jumper
{
    public static PlayerControl jumper;
    public static Color color = new Color32(204, 155, 20, byte.MaxValue); // mint

    public static float jumperJumpTime = 30f;
    public static float jumperChargesOnPlace = 1f;
    public static bool resetPlaceAfterMeeting;
    //    public static float jumperChargesGainOnMeeting = 2f;
    //public static float jumperMaxCharges = 3f;
    public static float jumperCharges = 1f;

    public static Vector3 jumpLocation;

    private static Sprite jumpMarkButtonSprite;
    private static Sprite jumpButtonSprite;
    public static bool usedPlace;

    public static Sprite getJumpMarkButtonSprite()
    {
        if (jumpMarkButtonSprite) return jumpMarkButtonSprite;
        jumpMarkButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.JumperButton.png", 115f);
        return jumpMarkButtonSprite;
    }

    public static Sprite getJumpButtonSprite()
    {
        if (jumpButtonSprite) return jumpButtonSprite;
        jumpButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.JumperJumpButton.png", 115f);
        return jumpButtonSprite;
    }

    public static void resetPlaces()
    {
        jumperCharges = Mathf.RoundToInt(CustomOptionHolder.jumperChargesOnPlace.getFloat());
        jumpLocation = Vector3.zero;
        usedPlace = false;
    }

    public static void clearAndReload()
    {
        resetPlaces();
        jumpLocation = Vector3.zero;
        jumper = null;
        resetPlaceAfterMeeting = true;
        jumperCharges = 1f;
        jumperJumpTime = CustomOptionHolder.jumperJumpTime.getFloat();
        jumperChargesOnPlace = CustomOptionHolder.jumperChargesOnPlace.getFloat();
        //      jumperChargesGainOnMeeting = CustomOptionHolder.jumperChargesGainOnMeeting.getFloat();
        //jumperMaxCharges = CustomOptionHolder.jumperMaxCharges.getFloat();
        usedPlace = false;
    }
}

public static class Escapist
{
    public static PlayerControl escapist;
    public static Color color = Palette.ImpostorRed;

    public static float escapistEscapeTime = 30f;
    public static float escapistChargesOnPlace = 1f;
    public static bool resetPlaceAfterMeeting;
    //    public static float jumperChargesGainOnMeeting = 2f;
    //public static float escapistMaxCharges = 3f;
    public static float escapistCharges = 1f;

    public static Vector3 escapeLocation;

    private static Sprite escapeMarkButtonSprite;
    private static Sprite escapeButtonSprite;
    public static bool usedPlace;

    public static Sprite getEscapeMarkButtonSprite()
    {
        if (escapeMarkButtonSprite) return escapeMarkButtonSprite;
        escapeMarkButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.Mark.png", 115f);
        return escapeMarkButtonSprite;
    }

    public static Sprite getEscapeButtonSprite()
    {
        if (escapeButtonSprite) return escapeButtonSprite;
        escapeButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.Recall.png", 115f);
        return escapeButtonSprite;
    }

    public static void resetPlaces()
    {
        escapistCharges = Mathf.RoundToInt(CustomOptionHolder.escapistChargesOnPlace.getFloat());
        escapeLocation = Vector3.zero;
        usedPlace = false;
    }

    public static void clearAndReload()
    {
        resetPlaces();
        escapeLocation = Vector3.zero;
        escapist = null;
        resetPlaceAfterMeeting = true;
        escapistCharges = 1f;
        escapistEscapeTime = CustomOptionHolder.escapistEscapeTime.getFloat();
        escapistChargesOnPlace = CustomOptionHolder.escapistChargesOnPlace.getFloat();
        //      jumperChargesGainOnMeeting = CustomOptionHolder.jumperChargesGainOnMeeting.getFloat();
        //escapistMaxCharges = CustomOptionHolder.escapistMaxCharges.getFloat();
        usedPlace = false;
    }
}

public static class Blackmailer
{
    public static PlayerControl blackmailer;
    public static Color color = Palette.ImpostorRed;
    public static Color blackmailedColor = Palette.White;

    public static bool alreadyShook;
    public static PlayerControl blackmailed;
    public static PlayerControl currentTarget;
    public static float cooldown = 30f;
    private static Sprite blackmailButtonSprite;
    private static Sprite overlaySprite;
    public static Sprite getBlackmailOverlaySprite()
    {
        if (overlaySprite) return overlaySprite;
        overlaySprite = loadSpriteFromResources("TheOtherRoles.Resources.BlackmailerOverlay.png", 100f);
        return overlaySprite;
    }

    public static Sprite getBlackmailLetterSprite()
    {
        if (overlaySprite) return overlaySprite;
        overlaySprite = loadSpriteFromResources("TheOtherRoles.Resources.BlackmailerLetter.png", 115f);
        return overlaySprite;
    }

    public static Sprite getBlackmailButtonSprite()
    {
        if (blackmailButtonSprite) return blackmailButtonSprite;
        blackmailButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.BlackmailerBlackmailButton.png", 115f);
        return blackmailButtonSprite;
    }

    public static void clearAndReload()
    {
        blackmailer = null;
        currentTarget = null;
        blackmailed = null;
        cooldown = CustomOptionHolder.blackmailerCooldown.getFloat();
    }
}

public static class Thief
{
    public static PlayerControl thief;
    public static Color color = new Color32(71, 99, 45, Byte.MaxValue);
    public static PlayerControl currentTarget;
    public static PlayerControl formerThief;

    public static float cooldown = 30f;

    public static bool suicideFlag;  // Used as a flag for suicide

    public static bool hasImpostorVision;
    public static bool canUseVents;
    public static bool canKillSheriff;
    public static bool canStealWithGuess;

    public static void clearAndReload()
    {
        thief = null;
        suicideFlag = false;
        currentTarget = null;
        formerThief = null;
        hasImpostorVision = CustomOptionHolder.thiefHasImpVision.getBool();
        cooldown = CustomOptionHolder.thiefCooldown.getFloat();
        canUseVents = CustomOptionHolder.thiefCanUseVents.getBool();
        canKillSheriff = CustomOptionHolder.thiefCanKillSheriff.getBool();
        canStealWithGuess = CustomOptionHolder.thiefCanStealWithGuess.getBool();
    }

    public static bool isFailedThiefKill(PlayerControl target, PlayerControl killer, RoleInfo targetRole)
    {
        return killer == thief && !target.Data.Role.IsImpostor && !new List<RoleInfo> { RoleInfo.jackal, canKillSheriff ? RoleInfo.sheriff : null, RoleInfo.sidekick }.Contains(targetRole);
    }
}

public static class Trapper
{
    public static PlayerControl trapper;
    public static Color color = new Color32(110, 57, 105, byte.MaxValue);

    public static float cooldown = 30f;
    public static int maxCharges = 5;
    public static int rechargeTasksNumber = 3;
    public static int rechargedTasks = 3;
    public static int charges = 1;
    public static int trapCountToReveal = 2;
    public static List<PlayerControl> playersOnMap = new();
    public static bool anonymousMap;
    public static int infoType; // 0 = Role, 1 = Good/Evil, 2 = Name
    public static float trapDuration = 5f;

    private static Sprite trapButtonSprite;

    public static Sprite getButtonSprite()
    {
        if (trapButtonSprite) return trapButtonSprite;
        trapButtonSprite = loadSpriteFromResources("TheOtherRoles.Resources.Trapper_Place_Button.png", 115f);
        return trapButtonSprite;
    }

    public static void clearAndReload()
    {
        trapper = null;
        cooldown = CustomOptionHolder.trapperCooldown.getFloat();
        maxCharges = Mathf.RoundToInt(CustomOptionHolder.trapperMaxCharges.getFloat());
        rechargeTasksNumber = Mathf.RoundToInt(CustomOptionHolder.trapperRechargeTasksNumber.getFloat());
        rechargedTasks = Mathf.RoundToInt(CustomOptionHolder.trapperRechargeTasksNumber.getFloat());
        charges = Mathf.RoundToInt(CustomOptionHolder.trapperMaxCharges.getFloat()) / 2;
        trapCountToReveal = Mathf.RoundToInt(CustomOptionHolder.trapperTrapNeededTriggerToReveal.getFloat());
        playersOnMap = new List<PlayerControl>();
        anonymousMap = CustomOptionHolder.trapperAnonymousMap.getBool();
        infoType = CustomOptionHolder.trapperInfoType.getSelection();
        trapDuration = CustomOptionHolder.trapperTrapDuration.getFloat();
    }
}

public static class Terrorist
{
    public static PlayerControl terrorist;
    public static Color color = Palette.ImpostorRed;

    public static Bomb bomb;
    public static bool isPlanted;
    public static bool isActive;
    public static float destructionTime = 20f;
    public static float destructionRange = 2f;
    public static float hearRange = 30f;
    public static float defuseDuration = 3f;
    public static float bombCooldown = 15f;
    public static float bombActiveAfter = 3f;
    public static bool selfExplosion => destructionTime + bombActiveAfter <= 1;

    private static Sprite buttonSprite;

    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.Bomb_Button_Plant.png", 115f);
        return buttonSprite;
    }

    public static void clearBomb(bool flag = true)
    {
        if (bomb != null)
        {
            Object.Destroy(bomb.bomb);
            Object.Destroy(bomb.background);
            bomb = null;
        }
        isPlanted = false;
        isActive = false;
        if (flag) SoundEffectsManager.stop("bombFuseBurning");
    }

    public static void clearAndReload()
    {
        clearBomb(false);
        terrorist = null;
        bomb = null;
        isPlanted = false;
        isActive = false;
        destructionTime = CustomOptionHolder.terroristBombDestructionTime.getFloat();
        destructionRange = CustomOptionHolder.terroristBombDestructionRange.getFloat() / 10;
        hearRange = CustomOptionHolder.terroristBombHearRange.getFloat() / 10;
        defuseDuration = CustomOptionHolder.terroristDefuseDuration.getFloat();
        bombCooldown = CustomOptionHolder.terroristBombCooldown.getFloat();
        bombActiveAfter = CustomOptionHolder.terroristBombActiveAfter.getFloat();
        Bomb.clearBackgroundSprite();
    }
}

// Modifier
public static class Bait
{
    public static List<PlayerControl> bait = new();
    public static Dictionary<DeadPlayer, float> active = new();
    public static Color color = new Color32(0, 247, 255, byte.MaxValue);

    public static float reportDelayMin;
    public static float reportDelayMax;
    public static bool showKillFlash = true;

    public static void clearAndReload()
    {
        bait = new List<PlayerControl>();
        active = new Dictionary<DeadPlayer, float>();
        reportDelayMin = CustomOptionHolder.modifierBaitReportDelayMin.getFloat();
        reportDelayMax = CustomOptionHolder.modifierBaitReportDelayMax.getFloat();
        if (reportDelayMin > reportDelayMax) reportDelayMin = reportDelayMax;
        showKillFlash = CustomOptionHolder.modifierBaitShowKillFlash.getBool();
    }
}
public class Aftermath
{
    public static PlayerControl aftermath;
    public static Color color = new Color32(165, 255, 165, byte.MaxValue);

    public static void clearAndReload()
    {
        aftermath = null;
    }
}

public static class Bloody
{
    public static List<PlayerControl> bloody = new();
    public static Dictionary<byte, float> active = new();
    public static Dictionary<byte, byte> bloodyKillerMap = new();

    public static float duration = 5f;

    public static void clearAndReload()
    {
        bloody = new List<PlayerControl>();
        active = new Dictionary<byte, float>();
        bloodyKillerMap = new Dictionary<byte, byte>();
        duration = CustomOptionHolder.modifierBloodyDuration.getFloat();
    }
}

public static class AntiTeleport
{
    public static List<PlayerControl> antiTeleport = new();
    public static Vector3 position;

    public static void clearAndReload()
    {
        antiTeleport = new List<PlayerControl>();
        position = Vector3.zero;
    }

    public static void setPosition()
    {
        if (position == Vector3.zero) return;  // Check if this has been set, otherwise first spawn on submerged will fail
        if (antiTeleport.FindAll(x => x.PlayerId == PlayerControl.LocalPlayer.PlayerId).Count > 0)
        {
            PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(position);
            if (SubmergedCompatibility.IsSubmerged)
            {
                SubmergedCompatibility.ChangeFloor(position.y > -7);
            }
        }
    }
}

public static class Tiebreaker
{
    public static PlayerControl tiebreaker;

    public static bool isTiebreak;

    public static void clearAndReload()
    {
        tiebreaker = null;
        isTiebreak = false;
    }
}

public static class Indomitable
{
    public static PlayerControl indomitable;
    public static Color color = new Color32(0, 247, 255, byte.MaxValue);


    public static void clearAndReload()
    {
        indomitable = null;
    }
}

public static class Cursed
{
    public static PlayerControl cursed;
    public static Color crewColor = new Color32(0, 247, 255, byte.MaxValue);
    public static Color impColor = Palette.ImpostorRed;
    public static Color color = crewColor;
    public static void clearAndReload()
    {
        cursed = null;
    }
}

public static class Slueth
{
    public static PlayerControl slueth;
    public static Color color = new Color32(48, 21, 89, byte.MaxValue);
    public static List<PlayerControl> reported = new();

    public static void clearAndReload()
    {
        slueth = null;
        reported = new List<PlayerControl>();
    }
}

public static class Swooper
{
    public static PlayerControl swooper;
    public static Color color = new Color32(224, 197, 219, byte.MaxValue);


    public static void clearAndReload()
    {
        swooper = null;
    }
}

public static class Blind
{
    public static PlayerControl blind;
    public static Color color = new Color32(48, 21, 89, byte.MaxValue);


    public static void clearAndReload()
    {
        blind = null;
    }
}

public static class Watcher
{
    public static PlayerControl watcher;
    public static Color color = new Color32(48, 21, 89, byte.MaxValue);


    public static void clearAndReload()
    {
        watcher = null;
    }
}

public static class Radar
{
    public static PlayerControl radar;
    public static List<Arrow> localArrows = new();
    public static PlayerControl ClosestPlayer;
    public static Color color = new Color32(255, 0, 128, byte.MaxValue);
    public static bool showArrows = true;



    public static void clearAndReload()
    {
        radar = null;
        showArrows = true;
        if (localArrows != null)
        {
            foreach (Arrow arrow in localArrows)
                if (arrow?.arrow != null)
                    Object.Destroy(arrow.arrow);
        }
        localArrows = new List<Arrow>();
    }
}

public static class Tunneler
{
    public static PlayerControl tunneler;
    public static Color color = new Color32(48, 21, 89, byte.MaxValue);


    public static void clearAndReload()
    {
        tunneler = null;
    }
}

public static class Sunglasses
{
    public static List<PlayerControl> sunglasses = new();
    public static int vision = 1;

    public static void clearAndReload()
    {
        sunglasses = new List<PlayerControl>();
        vision = CustomOptionHolder.modifierSunglassesVision.getSelection() + 1;
    }
}

public static class Torch
{
    public static List<PlayerControl> torch = new();
    public static float vision = 1;

    public static void clearAndReload()
    {
        torch = new List<PlayerControl>();
        vision = CustomOptionHolder.modifierTorchVision.getFloat();
    }
}

public static class Flash
{
    public static List<PlayerControl> flash = new();
    public static float speed = 1f;

    public static void clearAndReload()
    {
        flash = new List<PlayerControl>();
        speed = CustomOptionHolder.modifierFlashSpeed.getFloat();
    }
}

public static class Multitasker
{
    public static List<PlayerControl> multitasker = new();

    public static void clearAndReload()
    {
        multitasker = new List<PlayerControl>();
    }
}

public static class Disperser
{
    public static PlayerControl disperser;
    public static Color color = new Color32(48, 21, 89, byte.MaxValue);

    public static float cooldown = 30f;
    public static int remainingDisperses = 1;
    public static bool dispersesToVent;
    private static Sprite buttonSprite;

    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.Disperse.png", 115f);
        return buttonSprite;
    }
    public static void clearAndReload()
    {
        disperser = null;
        cooldown = CustomOptionHolder.modifierDisperserCooldown.getFloat();
        remainingDisperses = CustomOptionHolder.modifierDisperserNumberOfUses.getSelection() + 1;
        dispersesToVent = CustomOptionHolder.modifierDisperserDispersesToVent.getBool();
    }
}

public static class Mini
{
    public static PlayerControl mini;
    public static Color color = Color.yellow;
    public const float defaultColliderRadius = 0.2233912f;
    public const float defaultColliderOffset = 0.3636057f;

    public static float growingUpDuration = 400f;
    public static bool isGrowingUpInMeeting = true;
    public static DateTime timeOfGrowthStart = DateTime.UtcNow;
    public static DateTime timeOfMeetingStart = DateTime.UtcNow;
    public static float ageOnMeetingStart;
    public static bool triggerMiniLose;

    public static void clearAndReload()
    {
        mini = null;
        triggerMiniLose = false;
        growingUpDuration = CustomOptionHolder.modifierMiniGrowingUpDuration.getFloat();
        isGrowingUpInMeeting = CustomOptionHolder.modifierMiniGrowingUpInMeeting.getBool();
        timeOfGrowthStart = DateTime.UtcNow;
    }

    public static float growingProgress()
    {
        float timeSinceStart = (float)(DateTime.UtcNow - timeOfGrowthStart).TotalMilliseconds;
        return Mathf.Clamp(timeSinceStart / (growingUpDuration * 1000), 0f, 1f);
    }

    public static bool isGrownUp()
    {
        return growingProgress() == 1f;
    }

}

public static class Giant
{
    public static PlayerControl giant;
    public static float speed = 0.75f;
    public static float size = 1.12f;

    public static void clearAndReload()
    {
        giant = null;
        speed = CustomOptionHolder.modifierGiantSpped.getFloat();
    }
}

public static class Vip
{
    public static List<PlayerControl> vip = new();
    public static bool showColor = true;

    public static void clearAndReload()
    {
        vip = new List<PlayerControl>();
        showColor = CustomOptionHolder.modifierVipShowColor.getBool();
    }
}

public static class Invert
{
    public static List<PlayerControl> invert = new();
    public static int meetings = 3;

    public static void clearAndReload()
    {
        invert = new List<PlayerControl>();
        meetings = (int)CustomOptionHolder.modifierInvertDuration.getFloat();
    }
}

public static class Chameleon
{
    public static List<PlayerControl> chameleon = new();
    public static float minVisibility = 0.2f;
    public static float holdDuration = 1f;
    public static float fadeDuration = 0.5f;
    public static Dictionary<byte, float> lastMoved;

    public static void clearAndReload()
    {
        chameleon = new List<PlayerControl>();
        lastMoved = new Dictionary<byte, float>();
        holdDuration = CustomOptionHolder.modifierChameleonHoldDuration.getFloat();
        fadeDuration = CustomOptionHolder.modifierChameleonFadeDuration.getFloat();
        minVisibility = CustomOptionHolder.modifierChameleonMinVisibility.getSelection() / 10f;
    }

    public static float visibility(byte playerId)
    {
        float visibility = 1f;
        if (lastMoved != null && lastMoved.ContainsKey(playerId))
        {
            var tStill = Time.time - lastMoved[playerId];
            if (tStill > holdDuration)
            {
                if (tStill - holdDuration > fadeDuration) visibility = minVisibility;
                else visibility = ((1 - ((tStill - holdDuration) / fadeDuration)) * (1 - minVisibility)) + minVisibility;
            }
        }
        if (PlayerControl.LocalPlayer.Data.IsDead && visibility < 0.1f)
        {  // Ghosts can always see!
            visibility = 0.1f;
        }
        return visibility;
    }

    public static void update()
    {
        foreach (var chameleonPlayer in chameleon)
        {
            if ((chameleonPlayer == Ninja.ninja && Ninja.isInvisble) || (chameleonPlayer == Jackal.jackal && Jackal.isInvisable)) continue;  // Dont make Ninja visible...
            // check movement by animation
            PlayerPhysics playerPhysics = chameleonPlayer.MyPhysics;
            var currentPhysicsAnim = playerPhysics.Animations.Animator.GetCurrentAnimation();
            if (currentPhysicsAnim != playerPhysics.Animations.group.IdleAnim)
            {
                lastMoved[chameleonPlayer.PlayerId] = Time.time;
            }
            // calculate and set visibility
            float visibility = Chameleon.visibility(chameleonPlayer.PlayerId);
            float petVisibility = visibility;
            if (chameleonPlayer.Data.IsDead)
            {
                visibility = 0.5f;
                petVisibility = 1f;
            }

            try
            {  // Sometimes renderers are missing for weird reasons. Try catch to avoid exceptions
                chameleonPlayer.cosmetics.currentBodySprite.BodySprite.color = chameleonPlayer.cosmetics.currentBodySprite.BodySprite.color.SetAlpha(visibility);
                if (DataManager.Settings.Accessibility.ColorBlindMode) chameleonPlayer.cosmetics.colorBlindText.color = chameleonPlayer.cosmetics.colorBlindText.color.SetAlpha(visibility);
                chameleonPlayer.SetHatAndVisorAlpha(visibility);
                chameleonPlayer.cosmetics.skin.layer.color = chameleonPlayer.cosmetics.skin.layer.color.SetAlpha(visibility);
                chameleonPlayer.cosmetics.nameText.color = chameleonPlayer.cosmetics.nameText.color.SetAlpha(visibility);
                foreach (var rend in chameleonPlayer.cosmetics.currentPet.renderers)
                    rend.color = rend.color.SetAlpha(petVisibility);
                foreach (var shadowRend in chameleonPlayer.cosmetics.currentPet.shadows)
                    shadowRend.color = shadowRend.color.SetAlpha(petVisibility);
            }
            catch { }
        }

    }
}
public static class Armored
{
    public static PlayerControl armored;

    public static bool isBrokenArmor = false;
    public static void clearAndReload()
    {
        armored = null;
        isBrokenArmor = false;
    }
}

public static class Shifter
{
    public static PlayerControl shifter;

    public static PlayerControl futureShift;
    public static PlayerControl currentTarget;

    private static Sprite buttonSprite;
    public static Sprite getButtonSprite()
    {
        if (buttonSprite) return buttonSprite;
        buttonSprite = loadSpriteFromResources("TheOtherRoles.Resources.ShiftButton.png", 115f);
        return buttonSprite;
    }

    public static void shiftRole(PlayerControl player1, PlayerControl player2, bool repeat = true)
    {
        if (Guesser.niceGuesser != null && Guesser.niceGuesser == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Guesser.niceGuesser = player1;
        }
        else if (Mayor.mayor != null && Mayor.mayor == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Mayor.mayor = player1;
        }
        else if (Portalmaker.portalmaker != null && Portalmaker.portalmaker == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Portalmaker.portalmaker = player1;
        }
        else if (Engineer.engineer != null && Engineer.engineer == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Engineer.engineer = player1;
        }
        else if (PrivateInvestigator.privateInvestigator != null && PrivateInvestigator.privateInvestigator == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            PrivateInvestigator.privateInvestigator = player1;
        }
        else if (Sheriff.sheriff != null && Sheriff.sheriff == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            if (Sheriff.formerDeputy != null && Sheriff.formerDeputy == Sheriff.sheriff)
                Sheriff.formerDeputy = player1; // Shifter also shifts info on promoted deputy (to get handcuffs)
            Sheriff.sheriff = player1;
        }
        else if (Deputy.deputy != null && Deputy.deputy == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Deputy.deputy = player1;
        }
        else if (BodyGuard.bodyguard != null && BodyGuard.bodyguard == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            BodyGuard.bodyguard = player1;
        }
        else if (Lighter.lighter != null && Lighter.lighter == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Lighter.lighter = player1;
        }
        else if (Jumper.jumper != null && Jumper.jumper == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Jumper.jumper = player1;
        }
        else if (Detective.detective != null && Detective.detective == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Detective.detective = player1;
        }
        else if (TimeMaster.timeMaster != null && TimeMaster.timeMaster == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            TimeMaster.timeMaster = player1;
        }
        else if (Veteren.veteren != null && Veteren.veteren == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Veteren.veteren = player1;
        }
        else if (Medic.medic != null && Medic.medic == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Medic.medic = player1;
        }
        else if (Swapper.swapper != null && Swapper.swapper == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Swapper.swapper = player1;
        }
        else if (Seer.seer != null && Seer.seer == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Seer.seer = player1;
        }
        else if (Hacker.hacker != null && Hacker.hacker == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Hacker.hacker = player1;
        }
        else if (Tracker.tracker != null && Tracker.tracker == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Tracker.tracker = player1;
        }
        else if (Snitch.snitch != null && Snitch.snitch == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Snitch.snitch = player1;
        }
        else if (Spy.spy != null && Spy.spy == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Spy.spy = player1;
        }
        else if (SecurityGuard.securityGuard != null && SecurityGuard.securityGuard == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            SecurityGuard.securityGuard = player1;
        }
        else if (Medium.medium != null && Medium.medium == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Medium.medium = player1;
        }
        else if (Trapper.trapper != null && Trapper.trapper == player2)
        {
            if (repeat) shiftRole(player2, player1, false);
            Trapper.trapper = player1;
        }
    }

    public static void clearAndReload()
    {
        shifter = null;
        currentTarget = null;
        futureShift = null;
    }
}
