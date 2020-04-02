using System;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.HtmlPropertyEditor.Web;
using Ignyt.BusinessInterface;
using DevExpress.Web.ASPxHtmlEditor;
using System.Collections.Generic;

namespace MyWorkbench.Module.Web.Editors {
    [PropertyEditor(typeof(String), "HtmlWithPlaceholdersLight", false)]
    public class ASPxHtmlPropertyEditorWithPlaceholdersLight : ASPxHtmlPropertyEditor
    {
        public ASPxHtmlPropertyEditorWithPlaceholdersLight(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }
        protected override void ReadEditModeValueCore()
        {
            base.ReadEditModeValueCore();

            if (CurrentObject is IPlaceHoldersProviderLight)
            {
                if (Editor.Toolbars[0].Items.IndexOf(button => button is ToolbarInsertPlaceholderDialogButton) == -1)
                {
                    Editor.Toolbars[0].Items.Add(new ToolbarInsertPlaceholderDialogButton(true));
                }
                Editor.Placeholders.Clear();

                IList<string> placeHolders = ((IPlaceHoldersProviderLight)CurrentObject).Placeholders;

                foreach (string placeholder in placeHolders)
                {
                    Editor.Placeholders.Add(placeholder);
                }
            }
        }
    }

    [PropertyEditor(typeof(String), "HtmlWithPlaceholders", false)]
    public class ASPxHtmlPropertyEditorWithPlaceholders : ASPxHtmlPropertyEditor
    {
        public ASPxHtmlPropertyEditorWithPlaceholders(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }
        protected override void ReadEditModeValueCore()
        {
            base.ReadEditModeValueCore();
            if (CurrentObject is IPlaceHoldersProvider)
            {
                if (Editor.Toolbars[0].Items.IndexOf(button => button is ToolbarInsertPlaceholderDialogButton) == -1)
                {
                    Editor.Toolbars[0].Items.Add(new ToolbarInsertPlaceholderDialogButton(true));
                }
                Editor.Placeholders.Clear();


                IList<string> placeHolders = ((IPlaceHoldersProviderLight)CurrentObject).Placeholders;

                foreach (string placeholder in placeHolders)
                {
                    Editor.Placeholders.Add(placeholder);
                }

                ((IPlaceHoldersProvider)CurrentObject).TargetObjectChanged -= ASPxHtmlPropertyEditorWithPlaceholders_TargetObjectChanged;
                ((IPlaceHoldersProvider)CurrentObject).TargetObjectChanged += ASPxHtmlPropertyEditorWithPlaceholders_TargetObjectChanged;
            }
        }
        void ASPxHtmlPropertyEditorWithPlaceholders_TargetObjectChanged(object sender, EventArgs e)
        {
            WriteTextWithPlaceholders();
        }

        protected override void WriteValueCore()
        {
            base.WriteValueCore();

            if (CurrentObject is IPlaceHoldersProvider)
            {
                WriteTextWithPlaceholders();
            }
        }
        private void WriteTextWithPlaceholders()
        {
            ((IPlaceHoldersProvider)CurrentObject).TextWithPlaceholders = ASPxHtmlEditor.ReplacePlaceholders((string)ControlValue, ((IPlaceHoldersProvider)CurrentObject).TargetObject);
        }
    }
}
