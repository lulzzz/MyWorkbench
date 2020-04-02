using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.NodeGenerators;
using Ignyt.BusinessInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWorkbench.Module.ModelGenerators
{
    public class ISingletonBOModelUpdater : ModelNodesGeneratorUpdater<ModelViewsNodesGenerator>
    {
        public override void UpdateNode(DevExpress.ExpressApp.Model.Core.ModelNode node)
        {
            foreach (IModelView view in node.Nodes)
            {
                IModelObjectView objectview = view.AsObjectView;
                if (objectview != null && objectview.ModelClass.TypeInfo.Implements<ISingletonBO>())
                {
                    view.AllowNew = false;
                    view.AllowDelete = false;
                }
            }
        }
    }
}
