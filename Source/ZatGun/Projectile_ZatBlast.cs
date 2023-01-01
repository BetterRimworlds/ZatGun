using System.Collections.Generic;
using Verse;
using RimWorld;

namespace BetterRimworlds.ZatGun
{
    class Projectile_ZatBlast : Bullet
    {
        public ZatBlast Props => new ZatBlast();
        
        #region Overrides

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            base.Impact(hitThing, false);
            
            this.ZatBlastImpact(hitThing);
        }

        protected void ZatBlastImpact(Thing hitThing)
        {
            /* This is a call to the Impact method of the class we're inheriting from.
             * You can use your favourite decompiler to see what it does, but suffice to say
             * there are useful things in there, like damaging/killing the hitThing.
             */

            /*
             * Null checking is very important in RimWorld.
             * 99% of errors reported are from NullReferenceExceptions (NREs).
             * Make sure your code checks if things actually exist, before they
             * try to use the code that belongs to said things.
             */
            if (Props != null && hitThing != null && hitThing is Pawn hitPawn) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            {
                Hediff psychicallyShocked = hitPawn.health?.hediffSet?.GetFirstHediffOfDef(HediffDefOf.PsychicShock);

                float randomSeverity = Rand.Range(0.15f, 0.30f);
                if (psychicallyShocked != null)
                {
                    // If the pawn has already been shot with the zat gun, the second shot is fatal.
                    Messages.Message("Killing " + hitPawn.Name.ToStringFull + " because of 2nd zat blast.", MessageTypeDefOf.NegativeEvent);
                    hitPawn.Kill(null);
                }
                else
                {
                    float rand = Rand.Value; // This is a random percentage between 0% and 100%
                    if (rand > Props.addHediffChance) // If the percentage falls under the chance, success!
                    {
                        return;
                    }

                    // These three lines create a new health differential or Hediff,
                    // put them on the character, and increase its severity by a random amount.
                    Hediff hediff;

                    // If the pawn is psychically sensitive, or has bad luck, put them in a state of catatonia.
                    int psychicSensitivity = 0;
                    if (hitPawn.story?.traits != null && hitPawn.story.traits.HasTrait(TraitDef.Named("PsychicSensitivity")))
                    {
                        Trait psychicSensitivityTrait = hitPawn.story.traits.GetTrait(TraitDef.Named("PsychicSensitivity"));
                        psychicSensitivity = psychicSensitivityTrait.Degree;
                    }

                    float oddsOfCatatonia = 0.15f;
                    float oddsOfHangover = 0.20f;
                    if (psychicSensitivity > 0 || Rand.Value <= oddsOfCatatonia)
                    {
                        hitPawn.health?.AddHediff(HediffDefOf.CatatonicBreakdown, null, null);
                    }

                    // Chance of just having a "psycic hangover."
                    if (psychicSensitivity < -1 || Rand.Value < oddsOfHangover)
                    {
                        hitPawn.health?.AddHediff(HediffDefOf.PsychicHangover, null, null);
                    }

                    hediff = HediffMaker.MakeHediff(Props.hediffToAdd, hitPawn);
                    hediff.Severity = randomSeverity;
                    hitPawn.health.AddHediff(hediff);
                }
            }
        }
        #endregion Overrides
    }
}
