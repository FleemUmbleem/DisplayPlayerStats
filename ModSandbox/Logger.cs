using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModSandbox
{
	public class Logger
	{
		private int tickCount = 0;

		public void TickLogger()
		{
			// Only display debug info every few seconds
			tickCount++;
			if (tickCount == 180)
			{
				// DEBUG
				//Monitor.Log($"CurrentStamina = {currentStamina}", LogLevel.Debug);
				//Monitor.Log($"x pos: {textPosition.X} | y pos: {textPosition.Y}", LogLevel.Debug);
				tickCount = 0;
			}
		}
	}
}
