using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Maps.Web;
using DevExpress.ExpressApp.Maps.Web.Helpers;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using System.Linq;

namespace MyWorkbench.Module.Web.Controllers {
    public class MapDetailViewController : ViewController {
        private Session Session {
            get {
                return ((XPObjectSpace)this.ObjectSpace).Session;
            }
        }

        private MapProvider Provider {
            get {
                MapProvider provider = MapProvider.Bing;

                if (MyWorkbench.BaseObjects.Constants.Constants.SettingMapProvider(this.Session) == SettingMapProvider.Bing)
                    provider = MapProvider.Bing;
                else
                    provider = MapProvider.Google;

                return provider;
            }
        }

        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();

            if (View is ListView listView)
                this.ListView(listView);

            if (View is DetailView detailView) 
                this.DetailView(detailView);
        }

        private void ListView(ListView ListView) {
            if (ListView.Editor is WebMapsListEditor mapsListEditor && mapsListEditor.MapViewer != null) {
                if (ListView.IsRoot) {
                    // List Editor
                    MapSettings mapSettings = mapsListEditor.MapViewer.MapSettings;
                    mapSettings.Provider = this.Provider;
                    mapSettings.ZoomLevel = 2;
                    mapSettings.IsMarkersTooltipsEnabled = true;
                    mapSettings.IsControlsEnabled = true;
                    mapSettings.IsShowDetailsEnabled = true;
                    mapSettings.Type = MapType.RoadMap;
                } else {
                    // Nested List Property Editor
                    MapSettings mapSettings = mapsListEditor.MapViewer.MapSettings;
                    mapSettings.Provider = this.Provider;
                    mapSettings.ZoomLevel = 2;
                    mapSettings.IsMarkersTooltipsEnabled = true;
                    mapSettings.IsControlsEnabled = true;
                    mapSettings.IsShowDetailsEnabled = true;
                    mapSettings.Type = MapType.RoadMap;
                }
            }

            if (ListView.Editor is ASPxGridListEditor gridListEditor) {
                foreach (WebMapsPropertyEditor mapsPropertyEditor in gridListEditor.PropertyEditors.OfType<WebMapsPropertyEditor>()) {
                    mapsPropertyEditor.ControlCreated += MapsPropertyEditor_ControlCreated;
                }
            }
        }

        private void DetailView(DetailView DetailView) {
            foreach (ViewItem viewItem in DetailView.Items) {
                if (viewItem is WebMapsPropertyEditor mapsPropertyEditor && mapsPropertyEditor.MapViewer != null) {
                    mapsPropertyEditor.ControlCreated += MapsPropertyEditor_ControlCreated;
                    // Property Editor in a detail view
                    MapSettings mapSettings = mapsPropertyEditor.MapViewer.MapSettings;
                    mapSettings.Provider = this.Provider;
                    mapSettings.ZoomLevel = 10;

                }
            }
        }

        private void MapsPropertyEditor_ControlCreated(object sender, System.EventArgs e) {
            if (sender is WebMapsPropertyEditor mapsPropertyEditor && mapsPropertyEditor.MapViewer != null) {
                // Property Editor in a column of a list view
                MapSettings mapSettings = mapsPropertyEditor.MapViewer.MapSettings;
                mapSettings.Provider = this.Provider;
                mapSettings.ZoomLevel = 10;
            }
        }
    }
}
