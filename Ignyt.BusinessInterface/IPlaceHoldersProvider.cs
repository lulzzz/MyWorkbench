using System;
using System.Collections.Generic;

namespace Ignyt.BusinessInterface {
    public interface IPlaceHoldersProvider {
        IList<String> Placeholders { get; }
        Object TargetObject { get; }
        string TextWithPlaceholders { get; set; }
        event EventHandler TargetObjectChanged;
    }

    public interface IPlaceHoldersProviderLight
    {
        IList<String> Placeholders { get; }
        //string TextWithPlaceholders { get; set; }
    }
}
