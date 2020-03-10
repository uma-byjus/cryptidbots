using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHidingAttribute : PropertyAttribute {
    public string ConditionalSourceField = "";
    public bool HideInInspector = false;

    public ConditionalHidingAttribute(string conditionalSourceField) {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = false;
    }

    public ConditionalHidingAttribute(string conditionalSourceField, bool hideInInspector) {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = hideInInspector;
    }
}
