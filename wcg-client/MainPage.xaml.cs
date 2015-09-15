using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Net.Http;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=391641 を参照してください

namespace wcg_client
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Geolocator _geolocator;

        private Spot ownPlace;
        private List<Spot> spots = new List<Spot>();

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// このページがフレームに表示されるときに呼び出されます。
        /// </summary>
        /// <param name="e">このページにどのように到達したかを説明するイベント データ。
        /// このプロパティは、通常、ページを構成するために使用します。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // MapControlにBingマップを重ねる
            mainMap.Style = MapStyle.None; // オリジナルのタイルを消す

            var mapTile = new MapTileSource
            {
                DataSource =
                    new HttpMapTileDataSource("http://ecn.t1.tiles.virtualearth.net/tiles/r{quadkey}.png?g=1&mkt=ja-jp")
            };
            mainMap.TileSources.Insert(0, mapTile);

            getSpots();
        }

        private async void getSpots()
        {
            Debug.WriteLine("Start getSpot()");
            // 自己位置を取得
            // var accessStatus = await Geolocator.RequestAccessAsync(); // Win10
            if (_geolocator == null)
            {
                _geolocator = new Geolocator();
            }
            _geolocator.ReportInterval = 60000;
            _geolocator.DesiredAccuracyInMeters = (uint)Math.Round((double)App.Current.Resources["defaultAccuracy"]);

            var pos = await _geolocator.GetGeopositionAsync();
            var lat = pos.Coordinate.Point.Position.Latitude;
            var lon = pos.Coordinate.Point.Position.Longitude;

            Debug.WriteLine("coord: " + lat.ToString() + "," + lon.ToString());
            ownPlace = new Spot(mainMap, "", lat, lon);

            // HTTP リクエスト (Spotリスト取得)
            var URL = (string)App.Current.Resources["WcgServiceUrl"] + "spot/" + lat + "/" + lon + "/" + _geolocator.DesiredAccuracyInMeters.ToString();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(URL);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(json.ToString());

            var results = JsonConvert.DeserializeObject<List<spotApiResult>>(json);
            foreach(var result in results)
            {
                Debug.WriteLine("Add " + result.name);
                spots.Add(new Spot(mainMap, result.name, double.Parse(result.lat), double.Parse(result.lon) ));
            }


            // リスト展開 (spotsに格納)


            _geolocator = null;
        }

    }
}
