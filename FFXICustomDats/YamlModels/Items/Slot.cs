using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXICustomDats.YamlModels.Items
{
    public enum Slot { None = 0, Back, Body, Ears, Feet, Hands, Head, Legs, Neck, Rings, Sub, Waist, Ammo, Range, Main, Attachment };

    public partial class SlotConversion
    {
        private enum SLOTTYPE
        {
            SLOT_MAIN = 0x00001,
            SLOT_SUB = 0x00002,
            SLOT_RANGED = 0x00004,
            SLOT_AMMO = 0x00008,
            SLOT_HEAD = 0x00010,
            SLOT_BODY = 0x00020,
            SLOT_HANDS = 0x00040,
            SLOT_LEGS = 0x00080,
            SLOT_FEET = 0x00100,
            SLOT_NECK = 0x00200,
            SLOT_WAIST = 0x00400,
            SLOT_EAR1 = 0x00800,
            SLOT_EAR2 = 0x01000,
            SLOT_RING1 = 0x02000,
            SLOT_RING2 = 0x04000,
            SLOT_BACK = 0x08000,
            SLOT_LINK1 = 0x10000,
            SLOT_LINK2 = 0x20000,
        };

        private readonly static Dictionary<SLOTTYPE, Slot> SlotDict = new()
        {
            { SLOTTYPE.SLOT_MAIN, Slot.Main },
            { SLOTTYPE.SLOT_SUB, Slot.Sub },
            { SLOTTYPE.SLOT_RANGED, Slot.Range },
            { SLOTTYPE.SLOT_AMMO, Slot.Ammo },
            { SLOTTYPE.SLOT_HEAD, Slot.Head },
            { SLOTTYPE.SLOT_BODY, Slot.Body },
            { SLOTTYPE.SLOT_HANDS, Slot.Hands },
            { SLOTTYPE.SLOT_LEGS, Slot.Legs },
            { SLOTTYPE.SLOT_FEET, Slot.Feet },
            { SLOTTYPE.SLOT_NECK, Slot.Neck },
            { SLOTTYPE.SLOT_WAIST, Slot.Waist },
            { SLOTTYPE.SLOT_EAR1, Slot.Ears },
            { SLOTTYPE.SLOT_EAR2, Slot.Ears },
            { SLOTTYPE.SLOT_RING1, Slot.Rings },
            { SLOTTYPE.SLOT_RING2, Slot.Rings },
            { SLOTTYPE.SLOT_BACK, Slot.Back },
            //{ SLOTTYPE.SLOT_LINK1, Slot.CanUse },
            //{ SLOTTYPE.SLOT_LINK2, Slot.CanTradeNpc },
        };

        public static List<Slot> ConvertBitSlotsToYaml(ushort slots)
        {
            var slotsList = new List<Slot>();
            foreach (var slot in SlotDict.Keys)
            {
                if ((slots & (ushort)slot) > 0)
                {
                    if (SlotDict.TryGetValue(slot, out Slot yamlSlot))
                    {
                        slotsList.Add(yamlSlot);
                    }
                }
            }
            return slotsList;
        }
    }  
}
