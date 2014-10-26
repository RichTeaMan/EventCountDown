using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace EventCountdownUI
{
    public class TileFactory
    {
        public static void SetTile()
        {
            var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150BlockAndText01);

            //var tileImage = tileXml.GetElementsByTagName("image")[0] as XmlElement;
            //tileImage.SetAttribute("src", "ms-appx:///Assets/trees.png");

            var xmasCountdown = EventCountdownLogic.Countdown.Christmas;
            var tileText = tileXml.GetElementsByTagName("text");
            (tileText[0] as XmlElement).InnerText = xmasCountdown.NextDate.GetDays.ToString();

            (tileText[1] as XmlElement).InnerText = "Days to Christmas";
            //(tileText[2] as XmlElement).InnerText = "Row 2";
            //(tileText[3] as XmlElement).InnerText = "Row 3";

            var tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
    }
}
