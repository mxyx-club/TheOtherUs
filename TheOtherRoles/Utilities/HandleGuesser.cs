using TheOtherRoles.CustomGameModes;
using UnityEngine;

namespace TheOtherRoles.Utilities
{
    public static class HandleGuesser
    {
        private static Sprite targetSprite;
        public static bool isGuesserGm = false;
        public static bool hasMultipleShotsPerMeeting = false;
        public static bool killsThroughShield = true;
        public static bool evilGuesserCanGuessSpy = true;
        public static bool guesserCantGuessSnitch = false;

        public static Sprite getTargetSprite()
        {
            if (targetSprite) return targetSprite;
            targetSprite = loadSpriteFromResources("TheOtherRoles.Resources.TargetIcon.png", 150f);
            return targetSprite;
        }

        public static bool isGuesser(byte playerId)
        {
            if (Doomsayer.doomsayer != null) return Doomsayer.doomsayer.PlayerId == playerId;
            return isGuesserGm ? GuesserGM.isGuesser(playerId) : Guesser.isGuesser(playerId);
        }

        public static void clear(byte playerId)
        {
            if (isGuesserGm) GuesserGM.clear(playerId);
            else Guesser.clear(playerId);
        }

        public static bool CanMultipleShots(PlayerControl dyingTarget)
        {
            if (dyingTarget == CachedPlayer.LocalPlayer.PlayerControl)
                return false;

            if (isGuesser(CachedPlayer.LocalPlayer.PlayerId)
                && remainingShots(CachedPlayer.LocalPlayer.PlayerId) > 1
                && hasMultipleShotsPerMeeting)
                return true;

            return CachedPlayer.LocalPlayer.PlayerControl == Doomsayer.doomsayer && Doomsayer.hasMultipleShotsPerMeeting &&
                   Doomsayer.CanShoot;
        }

        public static int remainingShots(byte playerId, bool shoot = false)
        {
            if (Doomsayer.doomsayer != null && Doomsayer.doomsayer.PlayerId == playerId) return 15;
            return isGuesserGm ? GuesserGM.remainingShots(playerId, shoot) : Guesser.remainingShots(playerId, shoot);
        }

        public static void clearAndReload()
        {
            Guesser.clearAndReload();
            GuesserGM.clearAndReload();
            isGuesserGm = TORMapOptions.gameMode == CustomGamemodes.Guesser;
            if (isGuesserGm)
            {
                guesserCantGuessSnitch = CustomOptionHolder.guesserGamemodeCantGuessSnitchIfTaksDone.getBool();
                hasMultipleShotsPerMeeting = CustomOptionHolder.guesserGamemodeHasMultipleShotsPerMeeting.getBool();
                killsThroughShield = CustomOptionHolder.guesserGamemodeKillsThroughShield.getBool();
                evilGuesserCanGuessSpy = CustomOptionHolder.guesserGamemodeEvilCanKillSpy.getBool();
            }
            else
            {
                guesserCantGuessSnitch = CustomOptionHolder.guesserCantGuessSnitchIfTaksDone.getBool();
                hasMultipleShotsPerMeeting = CustomOptionHolder.guesserHasMultipleShotsPerMeeting.getBool();
                killsThroughShield = CustomOptionHolder.guesserKillsThroughShield.getBool();
                evilGuesserCanGuessSpy = CustomOptionHolder.guesserEvilCanKillSpy.getBool();
            }

        }
    }
}
