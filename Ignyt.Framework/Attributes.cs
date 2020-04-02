﻿using System;
using System.Linq;
using System.Reflection;

namespace MyWorkbench.Framework {
    public static class Attributes {
        public static T GetAttribute<T>(Enum enumValue) where T : Attribute {
            T attribute;

            MemberInfo memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

            if (memberInfo != null) {
                attribute = (T)memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
                return attribute;
            }

            return null;
        }
    }
}
