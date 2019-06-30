using System;
using System.Collections.Generic;
using System.Linq;
using PoeHUD.Poe.Elements;
using PoeHUD.Controllers;
using PoeHUD.Poe.Components;
using PoeHUD.Poe.RemoteMemoryObjects;
using PoeHUD.Hud.AdvancedTooltip;
using PoeHUD.Models.Enums;

namespace Legion.Main
{


    public partial class Legion
    {
        public List<NormalInventoryItem> InventoryItemList { get; set; } = new List<NormalInventoryItem>();
        public Dictionary<String, int> fracturedItemMods = new Dictionary<String, int>();
        public List<String> Legionedimplicits = new List<String>();
        public List<String> allLegionedimplicits = new List<String>();
        public List<String> unusedModifiers = new List<String>();
        public Dictionary<String, int> sharedModifiers = new Dictionary<String, int>();
        public string fracturedImplicit = "";
        public string itemClass = "";
        public List<string> HoveredItems = new List<string>();
        public List<NormalInventoryItem> Inventory = new List<NormalInventoryItem>();

        
    }
}
