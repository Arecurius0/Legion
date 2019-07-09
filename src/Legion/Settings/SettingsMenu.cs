using ImGuiNET;


namespace Legion.Main
{
    public partial class Legion
    {
        public void SettingsMenu()
        {
            ImGui.PushID(idPop);
            if (ImGui.CollapsingHeader("Basics", TreeNodeFlags.Framed | TreeNodeFlags.DefaultOpen))
            {
                Settings.LegionThings.Value = ImGuiExtension.Checkbox(Settings.LegionThings.Value ? "Enable Features" : "Enable Features", Settings.LegionThings);
                Settings.DrawChests.Value = ImGuiExtension.Checkbox(Settings.DrawChests.Value ? "Draw chests" : "Draw Chests", Settings.DrawChests);
                Settings.DrawMobs.Value = ImGuiExtension.Checkbox(Settings.DrawMobs.Value ? "Draw Reward Mobs" : "Draw Reward Mobs", Settings.DrawMobs);
            }
            if (ImGui.CollapsingHeader("Icons", TreeNodeFlags.Framed | TreeNodeFlags.DefaultOpen))
            {
                Settings.DrawFancyIcons.Value = ImGuiExtension.Checkbox(Settings.DrawFancyIcons.Value ? "Detailed Loot Icons & Labels" : "Detailed Loot Icons & Labels", Settings.DrawFancyIcons);
                Settings.DrawMonolithIcon.Value = ImGuiExtension.Checkbox(Settings.DrawMobLines.Value ? "Draw Monolith Icon" : "Draw Monolith Icon", Settings.DrawMonolithIcon);
                Settings.IconSizeGeneral.Value = ImGuiExtension.IntSlider("Generals Icon Size", Settings.IconSizeGeneral);
                Settings.IconSizeLoot.Value = ImGuiExtension.IntSlider("Loot Icon Size", Settings.IconSizeLoot);
                Settings.IconSizeChest.Value = ImGuiExtension.IntSlider("Chest Icon Size", Settings.IconSizeChest);
                Settings.IconSizeHoard.Value = ImGuiExtension.IntSlider("War Hoard Icon Size", Settings.IconSizeHoard);
            }
            if (ImGui.CollapsingHeader("Text Labels", TreeNodeFlags.Framed | TreeNodeFlags.DefaultOpen))
            {
                Settings.TextLabels.Value = ImGuiExtension.Checkbox(Settings.TextLabels.Value ? "Icon Text Labels" : "Icon Text Labels", Settings.TextLabels);
                Settings.LootTextLabels.Value = ImGuiExtension.Checkbox(Settings.LootTextLabels.Value ? "Loot Type as Text Label" : "Loot Type as Text Label", Settings.LootTextLabels);
                Settings.TextLabelsOnly.Value = ImGuiExtension.Checkbox(Settings.TextLabelsOnly.Value ? "Only show Text Labels " : "Only show Text Labels", Settings.TextLabelsOnly);
                Settings.TextSize.Value = ImGuiExtension.IntSlider("Text Label Font Size", Settings.TextSize);
                Settings.TextWrap.Value = ImGuiExtension.IntSlider("Text Wrap Limit", Settings.TextWrap);
                Settings.TextYAdjust.Value = ImGuiExtension.IntSlider("Text Y Position Adjust", Settings.TextYAdjust);
            }
            if (ImGui.CollapsingHeader("World Icons", TreeNodeFlags.Framed | TreeNodeFlags.DefaultOpen))
            {
                Settings.DrawWorldIcons.Value = ImGuiExtension.Checkbox(Settings.DrawWorldIcons.Value ? "Draw Icons in World" : "Draw Icons in World", Settings.DrawWorldIcons);
                Settings.IconSizeWorld.Value = ImGuiExtension.IntSlider("World Icon Size", Settings.IconSizeWorld);
            }

            if (ImGui.CollapsingHeader("Lines to Things", TreeNodeFlags.Framed | TreeNodeFlags.DefaultOpen))
            {
                Settings.DrawChestsLines.Value = ImGuiExtension.Checkbox(Settings.DrawChestsLines.Value ? "Draw Lines to Chests" : "Draw Lines to Chests", Settings.DrawChestsLines);
                Settings.DrawMobLines.Value = ImGuiExtension.Checkbox(Settings.DrawMobLines.Value ? "Draw Lines to Reward Mobs" : "Draw Lines to Reward Mobs", Settings.DrawMobLines);
                Settings.DrawMonolithLine.Value = ImGuiExtension.Checkbox(Settings.DrawMobLines.Value ? "Draw Line to Monolith" : "Draw Line to Monolith", Settings.DrawMonolithLine);
                Settings.LineThickness.Value = ImGuiExtension.IntSlider("Line Thickness", Settings.LineThickness);
                Settings.LineAlpha.Value = ImGuiExtension.IntSlider("Line Alpha", Settings.LineAlpha);
            }

            if (ImGui.CollapsingHeader("Color Selection", TreeNodeFlags.Framed | TreeNodeFlags.DefaultOpen))
            {
                ImGui.Text("Color Selection:");
                Settings.KaruiColor = ImGuiExtension.ColorPicker("Karui Color", Settings.KaruiColor);
                Settings.EternalColor = ImGuiExtension.ColorPicker("Eternal Empire Color", Settings.EternalColor);
                Settings.TemplarColor = ImGuiExtension.ColorPicker("Templar Color", Settings.TemplarColor);
                Settings.VaalColor = ImGuiExtension.ColorPicker("Vaal Color", Settings.VaalColor);
                Settings.MarakethColor = ImGuiExtension.ColorPicker("Maraketh Color", Settings.MarakethColor);

            }
        }

        public override void DrawSettingsMenu()
        {
            ImGui.BulletText($"v{PluginVersion}");
            ImGui.PushStyleVar(StyleVar.ChildRounding, 5.0f);
            ImGuiNative.igGetContentRegionAvail(out var newcontentRegionArea);
            if (ImGui.BeginChild("RightSettings", new System.Numerics.Vector2(newcontentRegionArea.X, newcontentRegionArea.Y), true, WindowFlags.Default))
            {
                SettingsMenu();
            }
            ImGui.EndChild();
        }
    }
}
