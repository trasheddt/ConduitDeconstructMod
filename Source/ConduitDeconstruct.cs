using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;


namespace ConduitDeconstruct
{
    public class Designator_DeconstructPowerConduit : Designator_Deconstruct
    {
        public override void DesignateThing(Thing t)
        {
            if (t.def.CanHaveFaction && t.Faction != Faction.OfPlayer)
            {
                t.SetFaction(Faction.OfPlayer, null);
            }
            if (t.def.IsFrame || t.def.IsBlueprint)
            {
                t.Destroy(DestroyMode.Deconstruct);
            }
            else
            {
                base.Map.designationManager.AddDesignation(new Designation(t, DesignationDefOf.Deconstruct));
            }
        }

        public override AcceptanceReport CanDesignateThing(Thing t)
        {
            // Support for vanilla power conduits
            bool CanDesignateVanilla = (base.CanDesignateThing(t).Accepted && t.def == ThingDefOf.PowerConduit);

            // Support for Haplo_X1's PowerSwitch
            bool CanDesignatePowerConduitInvisible = (t is Blueprint_Build && (t.def.entityDefToBuild as ThingDef) == ThingDefOf.PowerConduit || t.def.defName == "PowerConduitInvisible");

            // Support for Industrialisation
            bool CanDesignateIndustrialisation = (t is Blueprint_Build && (t.def.entityDefToBuild as ThingDef) == ThingDefOf.PowerConduit || t.def.defName == "Ind_IndustrialCable");
            
            return CanDesignateVanilla || CanDesignatePowerConduitInvisible || CanDesignateIndustrialisation;
        }

        public override void SelectedUpdate()
        {
            base.SelectedUpdate();
            OverlayDrawHandler.DrawPowerGridOverlayThisFrame();
        }

        public Designator_DeconstructPowerConduit()
        {
            defaultLabel = "Deconstruct Conduits";
            this.icon = ContentFinder<UnityEngine.Texture2D>.Get("ToolbarIcon/ConduitDeconstructIcon", true);
            hotKey = null;
        }
    }
}
