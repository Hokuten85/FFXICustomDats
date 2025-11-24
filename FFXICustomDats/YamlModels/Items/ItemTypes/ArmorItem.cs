using FFXICustomDats.Data.XiDatEntities;
using FFXICustomDats.YamlModels.Items.ItemAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using static System.Reflection.Metadata.BlobBuilder;

namespace FFXICustomDats.YamlModels.Items.ItemTypes
{
    public partial class ArmorItem : Item
    {
        public ArmorItem() { }
        public ArmorItem(Data.XiDatEntities.Item item, ItemString strings, ItemEquipment equipment) : base(item, strings)
        {
            Equipment = new Equipment()
            {
                Level = equipment.Level,
                Slots = Helpers.BitsToEnumList<Slot>(equipment.Slot),
                Races = [(Race)equipment.Races],
                Jobs = Helpers.JobBitsToEnumList(equipment.Jobs),
                SuperiorLevel = equipment.SuperiorLevel,
                ShieldSize = equipment.ShieldSize,
                MaxCharges = equipment.MaxCharges,
                CastingTime = equipment.CastingTime,
                UseDelay = equipment.UseDelay,
                ReuseDelay = equipment.ReuseDelay,
                Unknown1 = equipment.Unknown1,
                Ilevel = equipment.ILevel,
                Unknown2 = equipment.Unknown2,
                Unknown3 = equipment.Unknown3,
            };
        }

        [YamlMember(Alias = "equipment", ApplyNamingConventions = false, Order = 8)]
        public Equipment Equipment { get; set; } = new Equipment();
    }
}
