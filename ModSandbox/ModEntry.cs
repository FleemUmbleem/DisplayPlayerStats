using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Characters;
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
		private int currentStamina;
		private int currentHealth;
		private int tickCount = 0;
		private int staminaIconX = 1;
		private int staminaIconY = 412;
		private int statusIconWxH = 14;
		private int healthIconX = 17;
		private int healthIconY = 412;
		private Texture2D tileSheet;
		private Texture2D staminaIcon;
		private Rectangle sourceStaminaRect;
		private Rectangle destStaminaRect;
		private Rectangle sourceHealthRect;
		private Rectangle destHealthRect;
		private string cursorsTileSheetPath = "\\Content\\LooseSprites\\cursors.xnb";
		private float scaleFactor = 2.0f;

		public override void Entry(IModHelper helper)
		{
			tileSheet = helper.ModContent.Load<Texture2D>(Path.Combine(Constants.GamePath + cursorsTileSheetPath));

			// Subscribe to the game's ticks
			helper.Events.Display.RenderingHud += OnRenderingHud;
		}

        private void OnRenderingHud(object sender, RenderingHudEventArgs e)
		{
			// Get a reference to the player
			Farmer player = Game1.player;

			// Reload viewport
			int statusDestX = Game1.viewport.Width - 210;
			int staminaDestY = Game1.viewport.Height - 50;
			int healthDestY = Game1.viewport.Height - 80;
			int statusTextX = Game1.viewport.Width - 170;

			// Get stamina and health values
			currentHealth = player.health;
			currentStamina = (int)player.stamina;

			// Create the source rectangles for cropping
			sourceStaminaRect = new Rectangle(staminaIconX, staminaIconY, statusIconWxH, statusIconWxH);
			sourceHealthRect = new Rectangle(healthIconX, healthIconY, statusIconWxH, statusIconWxH);

			// Calculate the destination rectangles for rendering
			int destWxH = (int)(statusIconWxH * scaleFactor);
			destStaminaRect = new Rectangle(statusDestX, staminaDestY, destWxH, destWxH);
			destHealthRect = new Rectangle(statusDestX, healthDestY, destWxH, destWxH);

			// Calculate position for image based on the text position
			Vector2 staminaTextPosition = new Vector2(statusTextX, staminaDestY);
			Vector2 helathTextPosition = new Vector2(statusTextX, healthDestY);

			// Draw value and icon to the screen
			SpriteBatch spriteBatch = e.SpriteBatch;
            spriteBatch.DrawString(Game1.smallFont,$"{currentStamina}", staminaTextPosition, Color.White);
			spriteBatch.Draw(tileSheet, destStaminaRect, sourceStaminaRect, Color.White);
			spriteBatch.DrawString(Game1.smallFont, $"{currentHealth}", helathTextPosition, Color.White);
			spriteBatch.Draw(tileSheet, destHealthRect, sourceHealthRect, Color.White);

			// Only display debug info every few seconds
			tickCount++;
			if (tickCount == 180)
			{
				// DEBUG
				Monitor.Log($"CurrentStamina = {currentStamina}", LogLevel.Debug);
				//Monitor.Log($"x pos: {textPosition.X} | y pos: {textPosition.Y}", LogLevel.Debug);
				tickCount = 0;
			}
		}        
    }
}