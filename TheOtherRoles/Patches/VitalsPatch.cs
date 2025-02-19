using System;
using System.Collections.Generic;
using System.Linq;
using Hazel;
using TheOtherRoles.Modules;
using TheOtherRoles.Utilities;
using UnityEngine;
using static TheOtherRoles.GameHistory;

namespace TheOtherRoles.Patches;

[Harmony]
public class VitalsPatch
{
    static float vitalsTimer;
    static TMPro.TextMeshPro TimeRemaining;
    private static List<TMPro.TextMeshPro> hackerTexts = new();

    public static void ResetData()
    {
        vitalsTimer = 0f;
        if (TimeRemaining != null)
        {
            UnityEngine.Object.Destroy(TimeRemaining);
            TimeRemaining = null;
        }
    }

    static void UseVitalsTime()
    {
        // Don't waste network traffic if we're out of time.
        if (TORMapOptions.restrictDevices > 0 && TORMapOptions.restrictVitalsTime > 0f && PlayerControl.LocalPlayer.isAlive() && PlayerControl.LocalPlayer != Hacker.hacker)
        {
            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UseVitalsTime, SendOption.Reliable, -1);
            writer.Write(vitalsTimer);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
            RPCProcedure.useVitalsTime(vitalsTimer);
        }
        vitalsTimer = 0f;
    }

    [HarmonyPatch(typeof(VitalsMinigame), nameof(VitalsMinigame.Begin))]
    class VitalsMinigameStartPatch
    {
        static void Postfix(VitalsMinigame __instance)
        {
            vitalsTimer = 0f;

            if (Hacker.hacker != null && PlayerControl.LocalPlayer == Hacker.hacker)
            {
                hackerTexts = new List<TMPro.TextMeshPro>();
                foreach (VitalsPanel panel in __instance.vitals)
                {
                    TMPro.TextMeshPro text = UnityEngine.Object.Instantiate(__instance.SabText, panel.transform);
                    hackerTexts.Add(text);
                    UnityEngine.Object.DestroyImmediate(text.GetComponent<AlphaBlink>());
                    text.gameObject.SetActive(false);
                    text.transform.localScale = Vector3.one * 0.75f;
                    text.transform.localPosition = new Vector3(-0.75f, -0.23f, 0f);

                }
            }
        }
    }

    [HarmonyPatch(typeof(VitalsMinigame), nameof(VitalsMinigame.Update))]
    class VitalsMinigameUpdatePatch
    {
        static bool Prefix(VitalsMinigame __instance)
        {
            vitalsTimer += Time.deltaTime;
            if (vitalsTimer > 0.1f)
                UseVitalsTime();

            if (TORMapOptions.restrictDevices > 0)
            {
                if (TimeRemaining == null)
                {
                    TimeRemaining = UnityEngine.Object.Instantiate(HudManager.Instance.TaskPanel.taskText, __instance.transform);
                    TimeRemaining.alignment = TMPro.TextAlignmentOptions.BottomRight;
                    TimeRemaining.transform.position = Vector3.zero;
                    TimeRemaining.transform.localPosition = new Vector3(1.7f, 4.45f);
                    TimeRemaining.transform.localScale *= 1.8f;
                    TimeRemaining.color = Palette.White;
                }

                if (TORMapOptions.restrictVitalsTime <= 0f && PlayerControl.LocalPlayer != Hacker.hacker && !PlayerControl.LocalPlayer.Data.IsDead)
                {
                    __instance.Close();
                    return false;
                }

                string timeString = TimeSpan.FromSeconds(TORMapOptions.restrictVitalsTime).ToString(@"mm\:ss\.ff");
                TimeRemaining.text = String.Format("adminPatchTime".Translate(), timeString);
                TimeRemaining.gameObject.SetActive(true);
            }

            return true;
        }

        private static void Postfix(VitalsMinigame __instance)
        {
            // Hacker show time since death
            if (Hacker.hacker != null && Hacker.hacker == PlayerControl.LocalPlayer &&
                Hacker.hackerTimer > 0)
                for (var k = 0; k < __instance.vitals.Length; k++)
                {
                    var vitalsPanel = __instance.vitals[k];
                    var player = GameData.Instance.AllPlayers.Get(k);

                    // Hacker update
                    if (!vitalsPanel.IsDead) continue;
                    var deadPlayer = deadPlayers?.Where(x => x.player.PlayerId == player?.PlayerId)?.FirstOrDefault();
                    if (deadPlayer == null || k >= hackerTexts.Count || hackerTexts[k] == null) continue;
                    var timeSinceDeath = (float)(DateTime.UtcNow - deadPlayer.timeOfDeath).TotalMilliseconds;
                    hackerTexts[k].gameObject.SetActive(true);
                    hackerTexts[k].text = Math.Round(timeSinceDeath / 1000) + "s";
                }
            else
                foreach (var text in hackerTexts.Where(text => text != null && text.gameObject != null))
                    text.gameObject.SetActive(false);
        }
    }
}