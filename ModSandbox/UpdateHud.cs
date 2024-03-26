using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI.Events;
using StardewValley.Locations;
using StardewValley;

namespace ModSandbox
{
	public class UpdateHud
	{
		private int currentStamina;
	 	private int currentHealth;
		private int staminaIconX = 1;
		private int staminaIconY = 412;
		private int healthIconX = 17;
		private int healthIconY = 412;
		private int statusIconWxH = 14;
		private int healthBarOffset = 50;
		private int viewportMarginRightIcons = 200;
		private int viewportMarginRightText = 170;
		private int viewportMarginBottomStamina = 50;
		private int viewportMarginBottomHealth = 80;
		private Vector2 staminaTextPosition;
		private Vector2 healthTextPosition;
		private Rectangle sourceStaminaRect;
		private Rectangle destStaminaRect;
		private Rectangle sourceHealthRect;
		private Rectangle destHealthRect;
		private float scaleFactor = 2.0f;

		public void UpdateHudStatusText(Farmer player, Texture2D tileSheet, RenderingHudEventArgs e)
		{
			// Reload viewport
			int statusDestX = Game1.viewport.Width - viewportMarginRightIcons;
			int staminaDestY = Game1.viewport.Height - viewportMarginBottomStamina;
			int healthDestY = Game1.viewport.Height - viewportMarginBottomHealth;
			int statusTextX = Game1.viewport.Width - viewportMarginRightText;

			// Get stamina and health values
			currentHealth = player.health;
			currentStamina = (int)player.stamina;

			// Create the source rectangles for cropping
			sourceStaminaRect = new Rectangle(staminaIconX, staminaIconY, statusIconWxH, statusIconWxH);
			sourceHealthRect = new Rectangle(healthIconX, healthIconY, statusIconWxH, statusIconWxH);

			// Scale up the icons
			int destWxH = (int)(statusIconWxH * scaleFactor);

			// Calculate position for image based on the text position and current game location
			switch (Game1.currentLocation)
			{
				case MineShaft _:
				case Woods _:
				case SlimeHutch _:
				case VolcanoDungeon _:
				case Farm _:
					destStaminaRect = new Rectangle(statusDestX, staminaDestY, destWxH, destWxH);
					destHealthRect = new Rectangle(statusDestX, healthDestY, destWxH, destWxH);
					staminaTextPosition = new Vector2(statusTextX, staminaDestY);
					healthTextPosition = new Vector2(statusTextX, healthDestY);
				break;
				default:
					destStaminaRect = new Rectangle(statusDestX + healthBarOffset, staminaDestY, destWxH, destWxH);
					destHealthRect = new Rectangle(statusDestX + healthBarOffset, healthDestY, destWxH, destWxH);
					staminaTextPosition = new Vector2(statusTextX + healthBarOffset, staminaDestY);
					healthTextPosition = new Vector2(statusTextX + healthBarOffset, healthDestY);
				break;
			}

			// Draw value and icon to the screen
			SpriteBatch spriteBatch = e.SpriteBatch;
			spriteBatch.DrawString(Game1.smallFont, $"{currentStamina}", staminaTextPosition, Color.White);
			spriteBatch.Draw(tileSheet, destStaminaRect, sourceStaminaRect, Color.White);
			spriteBatch.DrawString(Game1.smallFont, $"{currentHealth}", healthTextPosition, Color.White);
			spriteBatch.Draw(tileSheet, destHealthRect, sourceHealthRect, Color.White);
		}
	}
}
