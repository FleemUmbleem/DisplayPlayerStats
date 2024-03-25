using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
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
            // Subscribe to the game's update tick
            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
            //helper.Events.Input.ButtonPressed += OnButtonPressed;
            //helper.Events.Display.RenderedWorld += OnRenderedWorld;
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (Context.IsWorldReady && (e.Button == SButton.MouseLeft || IsLeftMouseButtonDown()))
            {
                UpdateStaminaText();
            }
        }

        private void OnUpdateTicked(object sender, EventArgs e)
        {
            // Get a reference to the player
            Farmer player = Game1.player;

            // Use reflection to access the private field for stamina
            var field = typeof(Farmer).GetField("stamina", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null)
            {
                // Get the value of the stamina field
                float staminaFloat = (float)field.GetValue(player);

                // Parse the float as an integer
                int stamina = (int)staminaFloat;


                Monitor.Log($"Current Stamina: {stamina}");
                // Now you have the stamina value, you can do whatever you want with it
                // For example, you could log it to the console
                Monitor.Log($"Current int Stamina: {stamina}");
            }
            else
            {
                // Log an error if the field couldn't be found
                Monitor.Log("Unable to find stamina field.", LogLevel.Error);
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