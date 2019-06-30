using System;
using System.Globalization;
using System.Reflection;
using SharpDX;
using SharpDX.Direct3D9;
using PoeHUD.Controllers;
using PoeHUD.Models;
using PoeHUD.Plugins;
using PoeHUD.Poe.Components;
using PoeHUD.Poe.RemoteMemoryObjects;
using PoeHUD.Hud.UI;
using System.Collections.Generic;
using System.Linq;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud;
using System.Collections.Concurrent;

namespace Legion.Main
{
    public partial class Legion : BaseSettingsPlugin<Settings>
    {
        public Version version = Assembly.GetExecutingAssembly().GetName().Version;
        public string PluginVersion;
        public DateTime buildDate;
        public static int idPop;
        public string ImagePath;
        public LargeMapData LargeMapInformation { get; set; }

        public static readonly Dictionary<int, string> LegionIcon = new Dictionary<int, string>
        {
            {174,"abyss"},
            {175,"armour"},
            {176,"bestiary"},
            {177,"breach"},
            {178,"currency"},
            {179,"divination"},
            {180,"essences"},
            {181,"fossils"},
            {182,"fragments"},
            {183,"gems"},
            {184,"generic"},
            {185,"harbinger"},
            {186,"labyrinth"},
            {187,"maps"},
            {188,"perandus"},
            {189,"prophecies"},
            {190,"scarabs"},
            {191,"talismans"},
            {192,"trinkets"},
            {193,"uniques"},
            {194,"weapon"},
            {195,"incubators"}
        };

        public override void Initialise()
        {
            PluginName = "Legion";
            PluginVersion = DateTime.UtcNow.ToString("yyyyMMdd.HHmmss", CultureInfo.InvariantCulture);
            ImagePath = PluginDirectory + @"\resource\";
            _entityCollection = new ConcurrentDictionary<long, EntityWrapper>();
            GameController.Area.AreaChange += AreaChange;
        }

        private void AreaChange(AreaController area)
        {
            _entityCollection.Clear();
        }

        public override void EntityAdded(EntityWrapper entity) { _entityCollection[entity.Id] = entity; }
        public override void EntityRemoved(EntityWrapper entity) { _entityCollection.TryRemove(entity.Id, out _); }
        private ConcurrentDictionary<long, EntityWrapper> _entityCollection;

        public class LargeMapData
        {
            public Camera @Camera { get; set; }
            public PoeHUD.Poe.Elements.Map @MapWindow { get; set; }
            public RectangleF @MapRec { get; set; }
            public Vector2 @PlayerPos { get; set; }
            public float @PlayerPosZ { get; set; }
            public Vector2 @ScreenCenter { get; set; }
            public float @Diag { get; set; }
            public float @K { get; set; }
            public float @Scale { get; set; }

            public LargeMapData(GameController GC)
            {
                @Camera = GC.Game.IngameState.Camera;
                @MapWindow = GC.Game.IngameState.IngameUi.Map;
                @MapRec = @MapWindow.GetClientRect();
                @PlayerPos = GC.Player.GetComponent<Positioned>().GridPos;
                @PlayerPosZ = GC.Player.GetComponent<Render>().Z;
                @ScreenCenter = new Vector2(@MapRec.Width / 2, @MapRec.Height / 2).Translate(0, -20)
                                   + new Vector2(@MapRec.X, @MapRec.Y)
                                   + new Vector2(@MapWindow.LargeMapShiftX, @MapWindow.LargeMapShiftY);
                @Diag = (float)Math.Sqrt(@Camera.Width * @Camera.Width + @Camera.Height * @Camera.Height);
                @K = @Camera.Width < 1024f ? 1120f : 1024f;
                @Scale = @K / @Camera.Height * @Camera.Width * 3f / 4f / @MapWindow.LargeMapZoom;
            }
        }

        private void DrawToLargeMiniMap(EntityWrapper entity)
        {
            var icon = GetMapIcon(entity);
            if (icon == null)
            {
                return;
            }

            var iconZ = icon.EntityWrapper.GetComponent<Render>().Z;
            var point = LargeMapInformation.ScreenCenter
                        + MapIcon.DeltaInWorldToMinimapDelta(icon.WorldPosition - LargeMapInformation.PlayerPos,
                            LargeMapInformation.Diag, LargeMapInformation.Scale,
                            (iconZ - LargeMapInformation.PlayerPosZ) /
                            (9f / LargeMapInformation.MapWindow.LargeMapZoom));

            var texture = icon.TextureIcon;
            var size = icon.Size * 1.5f;
            texture.DrawPluginImage(Graphics, new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size));


            DrawImageEntity(entity, icon);
            if (Settings.TextLabels && !entity.Path.StartsWith("Metadata/Terrain/Leagues/Legion/Objects/LegionInitiator"))
            {
                var varPath = entity.Path.ToLower();
                var iconColor = GetColorForPath(varPath);
                string textOut = "";
                if (Settings.LootTextLabels)
                {
                    string file = System.IO.Path.GetFileNameWithoutExtension(icon.TextureIcon.FileName.ToString());
                    textOut = file.Remove(1).ToUpper() + file.Substring(1);
                }
                else
                {
                    if (varPath.Contains("karui")) textOut = "Karui";
                    else if (varPath.Contains("eternal")) textOut = "Eternal";
                    else if (varPath.Contains("templar")) textOut = "Templar";
                    else if (varPath.Contains("vaal")) textOut = "Vaal";
                    else if (varPath.Contains("maraketh")) textOut = "Maraketh";
                }
                if (varPath.Contains("monsterchest") || varPath.Contains("general")) textOut = entity.GetComponent<Render>().Name;
                if (textOut.Contains("{")) textOut = textOut.Split('{', '}')[1];

                point.Y = point.Y + 3;
                var text = Graphics.DrawText(textOut, Settings.TextSize, new Vector2(point.X, point.Y + size / 2f), iconColor, FontDrawFlags.Center | FontDrawFlags.Top);
                Graphics.DrawBox(new RectangleF((point.X - text.Width / 2f) - 3, (point.Y + size / 2f) - 1, text.Width + 5, text.Height + 2), new Color(0, 0, 0, 220));
                Graphics.DrawFrame(new RectangleF((point.X - text.Width / 2f) - 3, (point.Y + size / 2f) - 1, text.Width + 5, text.Height + 2), 1, iconColor);
            }
        }

        private Color GetColorForPath(string varPath)
        {
            if (varPath.Contains("karui")) return Settings.KaruiColor;
            else if (varPath.Contains("eternal")) return Settings.EternalColor;
            else if (varPath.Contains("templar")) return Settings.TemplarColor;
            else if (varPath.Contains("vaal")) return Settings.VaalColor;
            else if (varPath.Contains("maraketh")) return Settings.MarakethColor;
            else return Color.White;
        }

        private void DrawToSmallMiniMap(EntityWrapper entity)
        {
            var icon = GetMapIcon(entity);
            if (icon == null)
            {
                return;
            }

            var smallMinimap = GameController.Game.IngameState.IngameUi.Map.SmallMinimap;
            var playerPos = GameController.Player.GetComponent<Positioned>().GridPos;
            var posZ = GameController.Player.GetComponent<Render>().Z;
            const float scale = 240f;
            var mapRect = smallMinimap.GetClientRect();
            var mapCenter = new Vector2(mapRect.X + mapRect.Width / 2, mapRect.Y + mapRect.Height / 2).Translate(0, 0);
            var diag = Math.Sqrt(mapRect.Width * mapRect.Width + mapRect.Height * mapRect.Height) / 2.0;
            var iconZ = icon.EntityWrapper.GetComponent<Render>().Z;
            var point = mapCenter + MapIcon.DeltaInWorldToMinimapDelta(icon.WorldPosition - playerPos, diag, scale, (iconZ - posZ) / 20);
            var texture = icon.TextureIcon;
            var size = icon.Size;
            var rect = new RectangleF(point.X - size / 2f, point.Y - size / 2f, size, size);
            mapRect.Contains(ref rect, out var isContain);
            if (isContain)
            {
                texture.DrawPluginImage(Graphics, rect);

                if (Settings.TextLabels && !entity.Path.StartsWith("Metadata/Terrain/Leagues/Legion/Objects/LegionInitiator"))
                {
                    var varPath = entity.Path.ToLower();
                    var iconColor = GetColorForPath(varPath);
                    string textOut = "";
                    if (Settings.LootTextLabels)
                    {
                        string file = System.IO.Path.GetFileNameWithoutExtension(icon.TextureIcon.FileName.ToString());
                        textOut = file.Remove(1).ToUpper() + file.Substring(1);
                    }
                    else
                    {
                        if (varPath.Contains("karui")) textOut = "Karui";
                        else if (varPath.Contains("eternal")) textOut = "Eternal";
                        else if (varPath.Contains("templar")) textOut = "Templar";
                        else if (varPath.Contains("vaal")) textOut = "Vaal";
                        else if (varPath.Contains("maraketh")) textOut = "Maraketh";
                    }
                    if (varPath.Contains("monsterchest") || varPath.Contains("general")) textOut = entity.GetComponent<Render>().Name;
                        if (textOut.Contains("{")) textOut = textOut.Split('{', '}')[1];
                    point.Y = point.Y + 3;
                    var text = Graphics.DrawText(textOut, Settings.TextSize, new Vector2(point.X, point.Y + size / 2f), iconColor, FontDrawFlags.Center | FontDrawFlags.Top);
                    Graphics.DrawBox(new RectangleF((point.X - text.Width / 2f) - 3, (point.Y + size / 2f) - 1, text.Width + 5, text.Height + 2), new Color(0, 0, 0, 220));
                    Graphics.DrawFrame(new RectangleF((point.X - text.Width / 2f) - 3, (point.Y + size / 2f) - 1, text.Width + 5, text.Height + 2), 1, iconColor);
                }
            }
            DrawImageEntity(entity, icon);

        }

        public void DrawImageEntity(EntityWrapper entity, MapIcon icon)
        {
            if (icon == null)
            {
                return;
            }
            var varPath = entity.Path.ToLower();
            var iconColor = GetColorForPath(varPath);

            var texture = icon.TextureIcon;
            var screenPosition = GameController.Game.IngameState.Camera.WorldToScreen(entity.Pos, entity);
            var playerEntity = new EntityWrapper(GameController, GameController.Player.InternalEntity);
            var playerPosition = GameController.Game.IngameState.Camera.WorldToScreen(GameController.Player.Pos, playerEntity);

            var stats = entity.GetComponent<Stats>();

            
            if (Settings.DrawChestsLines && varPath.Contains("chest") || Settings.DrawChestsLines && varPath.Contains("general") || Settings.DrawMobLines && stats.StatDictionary.ContainsKey(2468))
                Graphics.DrawLine(playerPosition, screenPosition, Settings.LineThickness, new Color((int)iconColor.R, (int)iconColor.G, (int)iconColor.B, Settings.LineAlpha));
            if (Settings.DrawWorldIcons)
                if (Settings.DrawChests && varPath.Contains("chest") || Settings.DrawMobs && varPath.Contains("general") || Settings.DrawMobs && stats.StatDictionary.ContainsKey(2468))
                {
                    float worldIconSize = icon.Size * Settings.IconSizeWorld / 10;
                    texture.DrawPluginImage(Graphics, new RectangleF((screenPosition.X - worldIconSize/2), screenPosition.Y - worldIconSize/2, worldIconSize, worldIconSize));
                }
        }

        private MapIcon GetMapIcon(EntityWrapper e)
        {
            var varPath = e.Path.ToLower();
            var stats = e.GetComponent<Stats>();
            var iconColor = GetColorForPath(varPath);

            // Normal mobs and chests
            if (!e.Path.StartsWith("Metadata/Monsters/LegionLeague/MonsterChest") && !e.Path.Contains("General") && !e.Path.Contains("LegionInitiator") || e.HasComponent<Chest>())
            {
                if (stats.StatDictionary.TryGetValue(2468, out int minimapIcon))
                {
                    LegionIcon.TryGetValue(minimapIcon, out string iconType);
                    if (Settings.DrawFancyIcons && iconType != null)
                        return new MapIcon(e, new HudTexture(ImagePath + iconType + ".png", Color.White), () => Settings.DrawMapIcons, Settings.IconSizeLoot);
                    else
                        return new MapIcon(e, new HudTexture(ImagePath + "Legion.png", iconColor), () => Settings.DrawMapIcons, Settings.IconSizeLoot);
                }
            }
            // Generals
            else if (e.Path.StartsWith("Metadata/Monsters/LegionLeague/LegionKaruiGeneralFish")
                || e.Path.StartsWith("Metadata/Monsters/LegionLeague/LegionMarakethGeneral")
                || e.Path.StartsWith("Metadata/Monsters/LegionLeague/LegionEternalEmpireGeneral")
                || e.Path.StartsWith("Metadata/Monsters/LegionLeague/LegionVaalGeneral")
                || e.Path.StartsWith("Metadata/Monsters/LegionLeague/LegionTemplarGeneral"))
            {
                if (Settings.DrawMobs)
                    return new MapIcon(e, new HudTexture(ImagePath + "Legion_lg.png", iconColor), () => Settings.DrawMapIcons, Settings.IconSizeGeneral);
            }
            // War Hoards
            else if (e.Path.StartsWith("Metadata/Monsters/LegionLeague/MonsterChestVaal2")
                || e.Path.StartsWith("Metadata/Monsters/LegionLeague/MonsterChestTemplar2")
                || e.Path.StartsWith("Metadata/Monsters/LegionLeague/MonsterChestEternalEmpire2")
                || e.Path.StartsWith("Metadata/Monsters/LegionLeague/MonsterChestMaraketh2")
                || e.Path.StartsWith("Metadata/Monsters/LegionLeague/MonsterChestKarui2"))
            {
                if (Settings.DrawChests)
                    return new MapIcon(e, new HudTexture(ImagePath + "Chest_Large.png", iconColor), () => Settings.DrawMapIcons, Settings.IconSizeHoard);
            }
            // Normal Chests
            else if (e.Path.StartsWith("Metadata/Monsters/LegionLeague/MonsterChest"))
            {
                if (Settings.DrawChests)
                    return new MapIcon(e, new HudTexture(ImagePath + "Chest_Small.png", iconColor), () => Settings.DrawMapIcons, Settings.IconSizeChest);
            }
            // Pillar
            else if (e.Path.StartsWith("Metadata/Terrain/Leagues/Legion/Objects/LegionInitiator"))
            {
                    return new MapIcon(e, new HudTexture(ImagePath + "pillar.png", iconColor), () => Settings.DrawMapIcons, 30);
            }
            return null;
        }

        public override void Render()
        {
#if !DEBUG
            try { 
#endif
            if(Settings.LegionThings)
            foreach (var entity in _entityCollection.Values.ToList())
            {
                if (entity.Path.StartsWith("Metadata/Terrain/Leagues/Legion/Objects/LegionInitiator") && entity.IsTargetable)
                {
                    if (GameController.Game.IngameState.IngameUi.Map.LargeMap.IsVisible)
                    {
                        LargeMapInformation = new LargeMapData(GameController);
                        if (entity is null) continue;
                        DrawToLargeMiniMap(entity);
                    }
                    else if (GameController.Game.IngameState.IngameUi.Map.SmallMinimap.IsVisible)
                    {
                        if (entity is null) continue;
                        DrawToSmallMiniMap(entity);
                    }
                }
                if (entity is null || !entity.IsLegion || !entity.IsFrozenInTime && !entity.IsActive) continue;
                if (entity.IsAlive && entity.HasComponent<Monster>() || entity.HasComponent<Chest>() && !entity.GetComponent<Chest>().IsOpened)
                {
                    if (!entity.HasComponent<Chest>() || entity.IsActive && !entity.IsFrozenInTime)
                    { 
                        if (GameController.Game.IngameState.IngameUi.Map.LargeMap.IsVisible)
                        {
                            LargeMapInformation = new LargeMapData(GameController);
                            if (entity is null) continue;
                            DrawToLargeMiniMap(entity);
                        }
                        else if (GameController.Game.IngameState.IngameUi.Map.SmallMinimap.IsVisible)
                        {
                            if (entity is null) continue;
                            DrawToSmallMiniMap(entity);
                        }
                    }
                }
            }
#if !DEBUG
            }
            catch
            {

            }
#endif
        }
    }
}
