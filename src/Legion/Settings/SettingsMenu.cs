using ImGuiNET;


namespace Legion.Main
{
    public partial class Legion
    {
        private int FilterOption { get; set; } = 0;
        public void InfoDumpMenu(int idIn, out int idPop)
        {
            idPop = idIn;
            ImGui.PushID(idPop);
            Settings.LegionThings.Value = ImGuiExtension.Checkbox(Settings.LegionThings.Value ? "Enable" : "Enable", Settings.LegionThings);
            idPop++;
            ImGui.Spacing();
            ImGui.PopID();
            Settings.DrawChests.Value = ImGuiExtension.Checkbox(Settings.DrawChests.Value ? "Draw chests" : "Draw Chests", Settings.DrawChests);
            idPop++;
            ImGui.PopID();
            Settings.DrawMobs.Value = ImGuiExtension.Checkbox(Settings.DrawMobs.Value ? "Draw Reward Mobs" : "Draw Reward Mobs", Settings.DrawMobs);
            idPop++;
            ImGui.PopID();
            Settings.DrawFancyIcons.Value = ImGuiExtension.Checkbox(Settings.DrawFancyIcons.Value ? "Detailed Loot Icons & Labels" : "Detailed Loot Icons & Labels", Settings.DrawFancyIcons);
            idPop++;
            ImGui.PopID();
            Settings.TextLabels.Value = ImGuiExtension.Checkbox(Settings.TextLabels.Value ? "Icon Text Labels" : "Icon Text Labels", Settings.TextLabels);
            idPop++;
            ImGui.PopID();
            Settings.TextLabels.Value = ImGuiExtension.Checkbox(Settings.TextLabels.Value ? "Use Loot as Text Label" : "Use Loot as Text Label", Settings.TextLabels);
            idPop++;
            ImGui.PopID();
            Settings.TextLabelsOnly.Value = ImGuiExtension.Checkbox(Settings.TextLabelsOnly.Value ? "Text Labels Only" : "Text Labels Only", Settings.TextLabelsOnly);
            idPop++;
            ImGui.PopID();
            Settings.DrawWorldIcons.Value = ImGuiExtension.Checkbox(Settings.DrawWorldIcons.Value ? "Draw Icons in World" : "Draw Icons in World", Settings.DrawWorldIcons);
            idPop++;
            ImGui.PopID();
            Settings.DrawChestsLines.Value = ImGuiExtension.Checkbox(Settings.DrawChestsLines.Value ? "Draw Lines to Chests" : "Draw Lines to Chests", Settings.DrawChestsLines);
            idPop++;
            ImGui.PopID();
            Settings.DrawMobLines.Value = ImGuiExtension.Checkbox(Settings.DrawMobLines.Value ? "Draw Lines to Reward Mobs" : "Draw lines to Reward Mobs", Settings.DrawMobLines);
            idPop++;
            ImGui.PopID();
            Settings.DrawMonolithIcon.Value = ImGuiExtension.Checkbox(Settings.DrawMobLines.Value ? "Draw Monolith Icon" : "Draw Monolith Icon", Settings.DrawMonolithIcon);
            idPop++;
            ImGui.PopID();
            Settings.DrawMonolithLine.Value = ImGuiExtension.Checkbox(Settings.DrawMobLines.Value ? "Draw Line to Monolith Icon" : "Draw Line to Monolith", Settings.DrawMonolithLine);
            idPop++;
            ImGui.PopID();
            Settings.LineThickness.Value = ImGuiExtension.IntSlider("Line Thickness", Settings.LineThickness);
            idPop++;
            ImGui.PopID();
            Settings.LineAlpha.Value = ImGuiExtension.IntSlider("Line Alpha", Settings.LineAlpha);
            idPop++;
            ImGui.PopID();
            Settings.IconSizeWorld.Value = ImGuiExtension.IntSlider("World Icon Size", Settings.IconSizeWorld);
            idPop++;
            ImGui.PopID();
            Settings.IconSizeGeneral.Value = ImGuiExtension.IntSlider("General Icon Size", Settings.IconSizeGeneral);
            idPop++;
            ImGui.PopID();
            Settings.IconSizeLoot.Value = ImGuiExtension.IntSlider("Loot Icon Size", Settings.IconSizeLoot);
            idPop++;
            ImGui.PopID();
            Settings.IconSizeChest.Value = ImGuiExtension.IntSlider("Chest Icon Size", Settings.IconSizeChest);
            idPop++;
            ImGui.PopID();
            Settings.IconSizeHoard.Value = ImGuiExtension.IntSlider("War Hoard Icon Size", Settings.IconSizeHoard);
            idPop++;
            ImGui.PopID();
            Settings.TextSize.Value = ImGuiExtension.IntSlider("Text Label Font Size", Settings.TextSize);
            idPop++;
            ImGui.PopID();
            Settings.KaruiColor = ImGuiExtension.ColorPicker("Karui Color", Settings.KaruiColor);
            idPop++;
            ImGui.PopID();
            Settings.EternalColor = ImGuiExtension.ColorPicker("Eternal Empire Color", Settings.EternalColor);
            idPop++;
            ImGui.PopID();
            Settings.TemplarColor = ImGuiExtension.ColorPicker("Templar Color", Settings.TemplarColor);
            idPop++;
            ImGui.PopID();
            Settings.VaalColor = ImGuiExtension.ColorPicker("Vaal Color", Settings.VaalColor);
            idPop++;
            ImGui.PopID();
            Settings.MarakethColor = ImGuiExtension.ColorPicker("Maraketh Color", Settings.MarakethColor);
            idPop++;
            ImGui.PopID();



            ImGui.TreePop();
        }

        public override void DrawSettingsMenu() 
        {
            ImGui.BulletText($"v{PluginVersion}");
            idPop = 1;
            ImGui.PushStyleVar(StyleVar.ChildRounding, 5.0f);
            ImGuiNative.igGetContentRegionAvail(out var newcontentRegionArea);
            if (ImGui.BeginChild("RightSettings", new System.Numerics.Vector2(newcontentRegionArea.X, newcontentRegionArea.Y), true, WindowFlags.Default))
            {
                InfoDumpMenu(idPop, out var newInt);
                idPop = newInt;
            }
            ImGui.EndChild();
        }
    }
}
