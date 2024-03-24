using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

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
        private bool showText = true;
        private string textToDisplay = "scoopdiddywhoop";

        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += OnButtonPressed;
            //helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
            helper.Events.Display.RenderedWorld += OnRenderedWorld;
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (Context.IsWorldReady && (e.Button == SButton.MouseLeft || IsLeftMouseButtonDown()))
            {
                UpdateStaminaText();
            }
        }

        private bool IsLeftMouseButtonDown()
        {
            return Game1.input.GetMouseState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
        }

        private void UpdateStaminaText()
        {
            this.Monitor.Log($"stamina updated", LogLevel.Debug);
            textToDisplay = Game1.player.stamina.ToString();
        }

        private void OnRenderedWorld(object sender, RenderedWorldEventArgs e)
        {
            if (Context.IsWorldReady && showText)
            {
                SpriteBatch spriteBatch = e.SpriteBatch;

                //this.Monitor.Log($"stamina drawn to screen", LogLevel.Debug);

                // Draw your text onto the screen
                Vector2 position = new Vector2(100, 100); // Example position
                spriteBatch.DrawString(Game1.smallFont, textToDisplay, position, Color.White);
            }
        }
    }
}