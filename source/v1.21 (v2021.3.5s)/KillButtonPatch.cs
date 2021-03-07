﻿using HarmonyLib;
using Hazel;
using Il2CppSystem.Reflection;



namespace SheriffMod
{
    [HarmonyPatch]
    public static class KillButtonPatch
    {
 

        [HarmonyPatch(typeof(KillButtonManager), "PerformKill")]
        static bool Prefix(MethodBase __originalMethod)
        {
            if (Sheriff.instance == null) return true;
            if (PlayerController.LocalPlayer == Sheriff.instance.parent)
            {
                if (Sheriff.instance.SheriffKillTimer() == 0)
                {

                    if (PlayerControl.LocalPlayer.MPEOHLJNPOB)
                    {
                        if (Sheriff.instance.closestPlayer == null) return true;

                        //Target is Crewmate
                        if (Sheriff.instance.closestPlayer.PKMHEDAKKHE.LGEGJEHCFOG == false)
                        {
                            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SheriffKill, Hazel.SendOption.Reliable);
                            writer.Write(PlayerControl.LocalPlayer.PlayerId);
                            writer.Write(PlayerControl.LocalPlayer.PlayerId);
                            writer.EndMessage();
                            PlayerControl.LocalPlayer.MurderPlayer(PlayerControl.LocalPlayer);

                        }
                        else
                        {
                            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SheriffKill, Hazel.SendOption.Reliable);
                            writer.Write(PlayerControl.LocalPlayer.PlayerId);
                            writer.Write(Sheriff.instance.closestPlayer.PlayerId);
                            writer.EndMessage();
                            PlayerControl.LocalPlayer.MurderPlayer(Sheriff.instance.closestPlayer);

                        }
                        PlayerControl.LocalPlayer.SetKillTimer(CustomGameOptions.SheriffKillCD);
                        Sheriff.instance.killTimer = CustomGameOptions.SheriffKillCD;
                            

                    }
                }

                return false;
            }
            return true;



        }


    }

}
