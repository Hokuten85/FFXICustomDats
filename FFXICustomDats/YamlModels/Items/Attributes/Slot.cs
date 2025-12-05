using static FFXICustomDats.YamlModels.SharedAttributes.JobHelpers;
using static System.Net.Mime.MediaTypeNames;

namespace FFXICustomDats.YamlModels.Items.ItemAttributes
{
    public enum Slot { None = 0, Back, Body, Ears, Feet, Hands, Head, Legs, Neck, Rings, Sub, Waist, Ammo, Range, Main, Attachment };

    public static class SlotHelpers
    {
        public enum SLOTTYPE
        {
            Zero = 0,
            SLOT_MAIN,
            SLOT_SUB,
            SLOT_RANGED,
            SLOT_AMMO,
            SLOT_HEAD,
            SLOT_BODY,
            SLOT_HANDS,
            SLOT_LEGS,
            SLOT_FEET,
            SLOT_NECK,
            SLOT_WAIST,
            SLOT_EAR1,
            SLOT_EAR2,
            SLOT_RING1,
            SLOT_RING2,
            SLOT_BACK,
            SLOT_LINK1,
            SLOT_LINK2,
        };

        public readonly static Dictionary<SLOTTYPE, Slot> Map = new()
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
        };

        public static Dictionary<Slot, SLOTTYPE> RMap()
        {
            return Map.Where(x => !(new[] { SLOTTYPE.SLOT_EAR2, SLOTTYPE.SLOT_RING2 }).Contains(x.Key)).ToDictionary(x => x.Value, y => y.Key);
        }

        public static ushort YamlListToDBValue(List<Slot> slotList)
        {
            var dbValue = (ushort)Helpers.YamlListToDBValue(RMap(), slotList);

            if (slotList.Contains(Slot.Ears))
            {
                dbValue |= 1 << ((int)SLOTTYPE.SLOT_EAR2 - 1);
            }

            if (slotList.Contains(Slot.Rings))
            {
                dbValue |= 1 << ((int)SLOTTYPE.SLOT_RING2 - 1);
            }

            return dbValue;
        }

        public static bool IsEqual(List<Slot> slotList, ushort dbSlots)
        {
            var dbList = Helpers.DBValueToYamlList(Map, dbSlots);
            return Helpers.AreEqual(slotList, dbList);
        }
    }
}
