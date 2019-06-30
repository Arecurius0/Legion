using PoeHUD.Hud.Settings;

namespace Legion
{
    public class Settings : SettingsBase
    {
        public Settings()
        {
            Enable = true;

        }
        public ToggleNode LegionThings { get; set; } = true;
        public ToggleNode DrawWorldIcons { get; set; } = true;
        public ToggleNode DrawMapIcons { get; set; } = true;
        public ToggleNode DrawChests { get; set; } = true;
        public ToggleNode DrawChestsLines { get; set; } = true;
        public ToggleNode DrawMobs { get; set; } = true;
        public ToggleNode DrawMobLines { get; set; } = true;
        public ToggleNode DrawMisc { get; set; } = true;
        public ToggleNode DrawFancyIcons { get; set; } = true;
        public ToggleNode TextLabels { get; set; } = true;
        public ToggleNode LootTextLabels { get; set; } = true;
        public RangeNode<int> IconSizeWorld { get; set; } = new RangeNode<int>(24, 10, 100);
        public RangeNode<int> LineThickness { get; set; } = new RangeNode<int>(4, 1, 10);
        public RangeNode<int> LineAlpha { get; set; } = new RangeNode<int>(110, 10, 255);
        public RangeNode<int> IconSizeGeneral { get; set; } = new RangeNode<int>(28, 10, 100);
        public RangeNode<int> IconSizeLoot { get; set; } = new RangeNode<int>(28, 10, 100);
        public RangeNode<int> IconSizeChest { get; set; } = new RangeNode<int>(24, 10, 100);
        public RangeNode<int> IconSizeHoard { get; set; } = new RangeNode<int>(28, 10, 100);
        public RangeNode<int> IconSizeGeneric { get; set; } = new RangeNode<int>(10, 5, 25);
        public RangeNode<int> TextSize { get; set; } = new RangeNode<int>(10, 5, 25);
        public ColorNode KaruiColor { get; set; } = new SharpDX.Color(255, 140, 0, 255);
        public ColorNode EternalColor { get; set; } = new SharpDX.Color(220, 220, 220, 255);
        public ColorNode TemplarColor { get; set; } = new SharpDX.Color(16, 208, 0, 255);
        public ColorNode VaalColor { get; set; } = new SharpDX.Color(220, 0, 70, 255);
        public ColorNode MarakethColor { get; set; } = new SharpDX.Color(255, 255, 0, 255);
    }
}