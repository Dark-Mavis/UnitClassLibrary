﻿using System.Collections.Generic;
using UnitClassLibrary.GenericUnit;
using UnitClassLibrary.TimeUnit.TimeTypes;

namespace UnitClassLibrary.TimeUnit
{
    public class Time : FundamentalUnit<ITimeType>
    {
        public Time(ITimeType TimeType, double passedDouble, double errorMargin)
            : base(TimeType,passedDouble,errorMargin) { }

        private Time(StronglyTypedUnit<ITimeType> toCopy)
            : base(toCopy)
        {
        }

        new public Time Negate()
        {
            return new Time((this as StronglyTypedUnit<ITimeType>).Negate());
        }
    }
}