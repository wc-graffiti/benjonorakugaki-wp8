using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml.Controls.Maps;

namespace wcg_client
{
    class Spot
    {
        private MapControl map;
        private string name;
        private BasicGeoposition geo;

        public Spot(MapControl map, string name, double lat, double lon)
        {
            this.map = map;
            this.name = name;
            geo = new BasicGeoposition()
            {
                Latitude = lat,
                Longitude = lon,
            };
            AddMapIcon();
        }

        private void AddMapIcon()
        {
            MapIcon icon = new MapIcon();
            icon.Location = new Geopoint(geo);
            icon.NormalizedAnchorPoint = new Point(0.5, 1.0);
            icon.Title = name;
            map.MapElements.Add(icon);
        }
    }
}
