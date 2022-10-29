using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace BetterRimworlds.ZatGun
{
    public class ZatBlast : DefModExtension
    {
        #pragma warning disable CS0649 // disable the warning that these fields aren't initialized, because the game will handle that for us.
        // This statement means the default chance to add the hediff is 5%. It will be overwritten by anything you set in XML.
        // If you want, you could even leave the tag out of the XML safely.
        public float addHediffChance = 0.90f; 
        // We don't give the HediffDef any default value because we're setting it in XML.
        // If you need to give defs a default value, you need to declare them like this and initialize them after the game loads defs, otherwise you will get an error!
        public HediffDef hediffToAdd = HediffDefOf.PsychicShock;
        #pragma warning restore CS0649 // restore the warning. Not needed here, but useful if you have code below this point.
    }
}