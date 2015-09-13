using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=391641 を参照してください

namespace wcg_client
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Spot ownPlace;
        private List<Spot> spots;

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
            mainMap.Style = MapStyle.None; // オリジナルのタイルを消す

            var mapTile = new MapTileSource
            {
                DataSource =
                    new HttpMapTileDataSource("http://ecn.t1.tiles.virtualearth.net/tiles/r{quadkey}.png?g=1&mkt=ja-jp")
            };
            mainMap.TileSources.Insert(0, mapTile);

            // 自己位置を取得


            // HTTP リクエスト (Spotリスト取得)


            // リスト展開 (spotsに格納)


            // TODO

        }
    }
}
