using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.Web;
using System;

namespace MyWorkbench.Module.Web.ViewItems
{
    public interface IModelProgressViewItem : IModelViewItem
    {
    }

    [ViewItem(typeof(IModelProgressViewItem))]
    public class ProgresViewItem : ViewItem
    {
        public ASPxProgressBar ProgressBar { get; private set; }

        public ProgresViewItem(IModelProgressViewItem info, Type classType)
            : base(classType, info.Id)
        {
        }

        protected override object CreateControlCore()
        {
            ProgressBar = new ASPxProgressBar();
            return ProgressBar;
        }

        private XafCallbackManager CallbackManager => ((ICallbackManagerHolder)WebWindow.CurrentRequestPage).CallbackManager;

        public int PollingInterval { get; set; }

        public void Start(int maximum)
        {
            var script = CallbackManager.GetScript("ExcecuteLongRunningProcess", $"'{ProgressBar.ClientInstanceName}'", "", false);
            ProgressBar.ClientSideEvents.Init =
                $@"function(s,e) {{ 
                        if(window.timer) window.clearInterval(window.timer);
                        var controlToUpdate = s;
                        window.timer = window.setInterval(function(){{
                        var previous = startProgress;startProgress = function () {{ }};{script}startProgress = previous;}},{PollingInterval});}}";
        }

        public long Position { get; set; }

        public void ProcessAction(string parameter)
        {
            var script = $"{parameter}.SetPosition('{Position}')";
            WebWindow.CurrentRequestWindow.RegisterStartupScript("ExcecuteLongRunningProcess", script, true);
        }
    }
}
