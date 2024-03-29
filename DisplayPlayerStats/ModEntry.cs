using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Locations;
using System;

namespace ModSandbox
{
	/// <summary>The mod entry point.</summary>
	internal sealed class ModEntry : Mod
	{
		private Texture2D? tileSheet;
		private Farmer player;
		private UpdateHud updateHud;
		public int tickCount = 0;
		ModConstants modConstants = new ModConstants();
		bool complete = false;

		/// <summary>The mod entry point, called after the mod is first loaded.</summary>
		/// <param name="helper">Provides simplified APIs for writing mods.</param>
		public override void Entry(IModHelper helper)
		{
			// Load in tilesheet
			tileSheet = helper.ModContent.Load<Texture2D>(Path.Combine(Constants.GamePath + modConstants.cursorsTileSheetPath));

			// Subscribe to the HUD rendering ticks
			helper.Events.Display.RenderingHud += OnRenderingHud;
		}

		private void OnRenderingHud(object sender, RenderingHudEventArgs e)
		{
			// Initialize player and HUD class
			player = Game1.player;
			updateHud = new UpdateHud();

			

            // Update the HUD
            if (tileSheet != null)
			{
				updateHud.UpdateHudStatusText(player, tileSheet, e);
			}

			// Log every 180 ticks (roughly 3 seconds)
			TickLogger();
		}

		public void TickLogger()
		{
			// Only display debug info every few seconds
			tickCount++;
			if (tickCount == 240)
			{

				switch (Game1.currentLocation)
				{
					case CommunityCenter _:
					Monitor.Log($"============== START", LogLevel.Debug);

					for (int i = 0; i < 12; i++)
					{
						bool[] bundleCompletionStatus = Game1.netWorldState.Value.Bundles[i];

						// Iterate through the array and print the completion status of each item
						for (int j = 0; j < bundleCompletionStatus.Length; j++)
						{
							bool itemCompletionStatus = bundleCompletionStatus[j];
							Monitor.Log($"Item {j + 1} Completion Status: {itemCompletionStatus}", LogLevel.Debug);
						}
					}
					Monitor.Log($"============== END", LogLevel.Debug);
					break;
					default:
					break;
				}
				// Place debug logs here - leaving some commented as they are good reference
				//Monitor.Log($"OnRenderingHudCalled - zoomLevel: {Game1.options.zoomLevel} - WxH: {Game1.viewport.Width}x{Game1.viewport.Height}", LogLevel.Debug);
				//Monitor.Log($"SafeArea WxH: {testSafeArea.Width}x{testSafeArea.Height}", LogLevel.Debug);
				//Monitor.Log($"SafeArea TxB: {testSafeArea.Top}x{testSafeArea.Bottom}", LogLevel.Debug);
				//Monitor.Log($"SafeArea LxR: {testSafeArea.Left}x{testSafeArea.Right}", LogLevel.Debug);
				tickCount = 0;
			}
		}
	}
}