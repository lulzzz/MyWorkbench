﻿using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Common {
    [DefaultProperty("Description")]
    [ImageName("Action_Debug_Breakpoint_Toggle")]
    public class TimeZone : BaseObject {
        public TimeZone(Session session)
            : base(session) {
        }
        public TimeZone() { }

        private string fValue;
        public string Value {
            get => fValue;
            set => SetPropertyValue(nameof(Value), ref fValue, value);
        }

        private string fDescription;
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }
    }
}