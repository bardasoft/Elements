﻿using Elements.Designer;
using System.Collections;

namespace Elements.Controls.Button
{
    internal class ButtonDesigner : BaseControlDesigner
    {
        protected override void PreFilterProperties(IDictionary properties)
        {
            // properties.Remove("Text");

            base.PreFilterProperties(properties);
        }
    }
}