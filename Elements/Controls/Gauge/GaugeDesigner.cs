﻿using Elements.Designer;
using System.Collections;

namespace Elements.Controls.Gauge
{
    internal class GaugeDesigner : BaseControlDesigner
    {
        protected override void PreFilterProperties(IDictionary properties)
        {
            properties.Remove("Text");

            base.PreFilterProperties(properties);
        }
    }
}