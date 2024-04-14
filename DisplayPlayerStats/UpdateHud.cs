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
		private Vector2 staminaTextPosition;
		private Vector2 healthTextPosition;
		private Rectangle backgroundRect;
		private Rectangle sourceStaminaRect;
		private Rectangle destStaminaRect;
		private Rectangle sourceHealthRect;
		private Rectangle destHealthRect;
		private Rectangle windowSafeArea;
		ModConstants modConstants = new ModConstants();

		public void UpdateHudStatusText(Farmer player, Texture2D tileSheet, Texture2D background, RenderingHudEventArgs e)
		{
			// Get the safe area
			windowSafeArea = Utility.getSafeArea();
			int statusIconsDestX = windowSafeArea.Right - modConstants.viewportMarginRightIcons;
			int staminaDestY = windowSafeArea.Bottom - modConstants.viewportMarginBottomStamina;
			int healthDestY = windowSafeArea.Bottom - modConstants.viewportMarginBottomHealth;
			int statusTextX = windowSafeArea.Right - modConstants.viewportMarginRightText;

			// Get stamina and health values
			currentHealth = player.health;
			currentStamina = (int)player.stamina;

			// Create the source rectangles for cropping
			sourceStaminaRect = new Rectangle(modConstants.staminaIconX, modConstants.staminaIconY, modConstants.statusIconWxH, modConstants.statusIconWxH);
			sourceHealthRect = new Rectangle(modConstants.healthIconX, modConstants.healthIconY, modConstants.statusIconWxH, modConstants.statusIconWxH);

			// Scale up the icons
			int destWxH = (int)(modConstants.statusIconWxH * modConstants.scaleFactor);

			// Calculate position for image based on the text position and current game location
			switch (Game1.currentLocation)
			{
				case MineShaft _:
				case Woods _:
				case SlimeHutch _:
				case VolcanoDungeon _:
					destStaminaRect = new Rectangle(statusIconsDestX, staminaDestY, destWxH, destWxH);
					destHealthRect = new Rectangle(statusIconsDestX, healthDestY, destWxH, destWxH);
					staminaTextPosition = new Vector2(statusTextX, staminaDestY);
					healthTextPosition = new Vector2(statusTextX, healthDestY);
				break;
				default:
					backgroundRect = new Rectangle(statusIconsDestX + modConstants.healthBarOffset, healthDestY, destWxH, destWxH);
					destStaminaRect = new Rectangle(statusIconsDestX + modConstants.healthBarOffset, staminaDestY, destWxH, destWxH);
					destHealthRect = new Rectangle(statusIconsDestX + modConstants.healthBarOffset, healthDestY, destWxH, destWxH);
					staminaTextPosition = new Vector2(statusTextX + modConstants.healthBarOffset, staminaDestY);
					healthTextPosition = new Vector2(statusTextX + modConstants.healthBarOffset, healthDestY);
				break;
			}

			// Draw value and icon to the screen
			SpriteBatch spriteBatch = e.SpriteBatch;
			spriteBatch.Draw(background, backgroundRect, Color.White); // requires thought on figuring out coordinates
			spriteBatch.DrawString(Game1.smallFont, $"{currentStamina}", staminaTextPosition, Color.White);
			spriteBatch.Draw(tileSheet, destStaminaRect, sourceStaminaRect, Color.White);
			spriteBatch.DrawString(Game1.smallFont, $"{currentHealth}", healthTextPosition, Color.White);
			spriteBatch.Draw(tileSheet, destHealthRect, sourceHealthRect, Color.White);
		}
	}
}
