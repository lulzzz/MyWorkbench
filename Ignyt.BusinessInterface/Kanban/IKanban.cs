using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignyt.BusinessInterface.Kanban
{
    public interface IKanban
    {
        Guid Oid { get; }
        string IStatus { get; }
        string IDescription { get; }
        string ImageUrl { get; }
    }
}
