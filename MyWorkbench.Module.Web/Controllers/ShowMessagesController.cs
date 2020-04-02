using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using Ignyt.Framework;
using MyWorkbench.Module.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWorkbench.Module.Web.Controllers
{
    public class ShowMessagesController : WindowController, IXafCallbackHandler
    {
        private List<IMessageInformation> _userMessages;
        private List<IMessageInformation> UserMessages {
            get {
                this._userMessages = MessageProvider.Messages.Where(g => g.UserID == Guid.Parse(SecuritySystem.CurrentUserId.ToString())).ToList();
                return this._userMessages;
            }
        }

        public ShowMessagesController()
        {
            TargetWindowType = WindowType.Main;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.ViewChanged += Frame_ViewChanged;
            ((WebWindow)Window).PagePreRender += CurrentRequestWindow_PagePreRender;
        }

        protected override void OnDeactivated()
        {
            ((WebWindow)Window).PagePreRender -= CurrentRequestWindow_PagePreRender;
            Frame.ViewChanged -= Frame_ViewChanged;
            base.OnDeactivated();
        }

        void Frame_ViewChanged(object sender, ViewChangedEventArgs e)
        {
            if ((e.SourceFrame != null) && (e.SourceFrame.View != null))
            {
                e.SourceFrame.View.ControlsCreated += View_ControlsCreated;
            }
        }

        void View_ControlsCreated(object sender, EventArgs e)
        {
            RegisterXafCallackHandler();
        }
        private void RegisterXafCallackHandler()
        {
            if (XafCallbackManager != null)
            {
                XafCallbackManager.RegisterHandler("KA18958", this);
            }
        }

        protected XafCallbackManager XafCallbackManager {
            get {
                return WebWindow.CurrentRequestPage != null ? ((ICallbackManagerHolder)WebWindow.CurrentRequestPage).CallbackManager : null;
            }
        }

        void CurrentRequestWindow_PagePreRender(object sender, EventArgs e)
        {
            WebWindow window = (WebWindow)sender;

            this.ExecuteTrialAccount(window);

            this.ExecuteUserMessages(this);
        }

        private void ExecuteTrialAccount(WebWindow window)
        {
            //if ((bool)HttpContext.Current.Session["TrialAccount"] == true)
            //    ToastMessageHelper.ShowSuccessMessage(this.Application, String.Format("You have {0} days left in your trial", HttpContext.Current.Session["DaysLeftTrial"]), InformationPosition.Bottom, true);
        }

        private void ExecuteUserMessages(Controller Controller)
        {
            foreach (IMessageInformation message in this.UserMessages)
            {
                if (message.MessageType == MessageTypes.Success)
                    this.ExecuteInfoMessage(Controller, message.MessageText, message.Position == Position.Top ? InformationPosition.Top : InformationPosition.Bottom);
                else if (message.MessageType == MessageTypes.Warning)
                    this.ExecuteWarningMessage(Controller, message.MessageText, message.Position == Position.Top ? InformationPosition.Top : InformationPosition.Bottom);
                else if (message.MessageType == MessageTypes.Error)
                    this.ExecuteErrorMessage(Controller, message.Exception, message.Position == Position.Top ? InformationPosition.Top : InformationPosition.Bottom);

                MessageProvider.DeRegisterMessage(message);
            }
        }

        private void ExecuteInfoMessage(Controller Controller, string Message, InformationPosition Position)
        {
            ToastMessageHelper.ShowSuccessMessage(Controller.Application, Message, Position);
        }

        private void ExecuteWarningMessage(Controller Controller, string Message, InformationPosition Position)
        {
            ToastMessageHelper.ShowWarningMessage(Controller.Application, Message, Position);
        }

        private void ExecuteErrorMessage(Controller Controller, Exception Exception, InformationPosition Position)
        {
            ToastMessageHelper.ShowErrorMessage(Controller.Application, Exception, Position);
        }

        public void ProcessAction(string parameter)
        {
            if (IsSuitableView() && (Frame.View != null))
            {
                Frame.View.ObjectSpace.Refresh();
            }
        }

        protected virtual bool IsSuitableView()
        {
            return Frame.View != null && Frame.View.IsRoot && !(Frame.View is DetailView) && !(Frame is NestedFrame) && !(Frame is PopupWindow);
        }
    }
}
