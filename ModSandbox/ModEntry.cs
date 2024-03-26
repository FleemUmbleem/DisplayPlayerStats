using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Locations;
using xTile.Tiles;
using static System.Net.Mime.MediaTypeNames;

namespace ModSandbox
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
		
		private Texture2D tileSheet;
		private string cursorsTileSheetPath = "\\Content\\LooseSprites\\cursors.xnb";
		private Farmer player;
		private UpdateHud updateHud;
		private Logger logger;
		//private float previousUiScale;
		//private RenderingHudEventArgs renderingHudEventArgs;

		public override void Entry(IModHelper helper)
		{
			tileSheet = helper.ModContent.Load<Texture2D>(Path.Combine(Constants.GamePath + cursorsTileSheetPath));

			renderingHudEventArgs = new RenderingHudEventArgs();

			// Subscribe to the game's ticks
			helper.Events.Display.RenderingHud += OnRenderingHud;
			//helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
		}

        private void OnRenderingHud(object sender, RenderingHudEventArgs e)
		{
			// Initialize things
			renderingHudEventArgs = e;
			player = Game1.player;
			updateHud = new UpdateHud();
			logger = new Logger();

			updateHud.UpdateHudStatusText(player, tileSheet, e);

			logger.TickLogger();
		}

		//private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
		//{
		//	if (!Context.IsWorldReady)
		//		return;

		//	float currentUiScale = Game1.options.uiScale;

		//	if (currentUiScale != previousUiScale)
		//	{
		//		OnRenderingHud(sender, renderingHudEventArgs);
		//	}
		//}
    }
}