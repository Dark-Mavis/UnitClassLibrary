﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitClassLibrary.DistanceUnit.DistanceTypes.Metric.MeterUnit;
using UnitClassLibrary.ForceUnit;
using UnitClassLibrary.GenericUnit;

namespace UnitClassLibrary.DerivedUnits
{
    class NewtonMeter : MomentType
    {
        public override UnitDimensions Dimensions
        {
            get
            {
                return new UnitDimensions(1.0, new List<FundamentalUnitType>() { new Newton(), new Meter() });
            }
        }
    }
}