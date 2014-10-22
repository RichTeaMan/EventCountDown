using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace EventCountdownScheduledTaskAgent
{
    public class TileFactory
    {
        public static void SetTile()
        {
            var xmasCountdown = EventCountdownLogic.Countdown.Christmas;
            var tileData = new IconicTileData()
            {
                Title = "Days to Christmas",
                WideContent1 = "Event Countdown",
                WideContent2 = string.Empty,
                WideContent3 = string.Empty,
                Count = xmasCountdown.GetDays,
                IconImage = null,
                SmallIconImage = null

            };
            
            var appTile = ShellTile.ActiveTiles.First();
            if (appTile != null)
            {
                
                appTile.Update(tileData);
            }

        }
    }
}
