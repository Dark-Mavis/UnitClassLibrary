﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitClassLibrary
{
    public abstract class Unit
    {
        abstract public IUnitType UnitType { get; }
        abstract public Measurement Measurement { get; }

        public double ConversionFactor { get { return UnitType.ConversionFactor; } }
        public UnitDimensions Dimensions { get { return UnitType.Dimensions(); } }

        public double ConversionFromThisTo(IUnitType unit)
        {
            return this.ConversionFactor / unit.ConversionFactor;
        }

        public bool IsPositive()
        {
            return Measurement.Value > 0;
        }
        public Measurement MeasurementIn(IUnitType unitType)
        {
            var result = this.Measurement * ConversionFromThisTo(unitType);
            return result;
        }

        public double ValueIn(IUnitType unitType)
        {
            return this.Measurement.Value*ConversionFromThisTo(unitType);
        }

        abstract public Unit Invert();
        abstract public Unit Multiply(Unit unit);
        //abstract public Unit<T> Multiply<T>(Measurement scalar) where T : IUnitType;
        //abstract public Unit<T> Divide<T>(Measurement divisor) where T : IUnitType;

        abstract public Unit Multiply(Measurement scalar);
        abstract public Unit Divide(Measurement divisor);


        public Unit Divide(Unit unit)
        {
            return this.Multiply(unit.Invert());
        }


        public static Unit operator *(Unit unit1, Unit unit2)
        {
            return unit1.Multiply(unit2);
        }
        public static Unit operator *(Unit unit, Measurement m)
        {
            return unit.Multiply(m);
        }
        public static Unit operator *(Measurement m, Unit unit)
        {
            return unit*m;
        }
        public static Unit operator /(Unit unit1, Unit unit2)
        {
            return unit1.Divide(unit2);
        }
        public static Unit operator /(Unit unit, Measurement divisor)
        {
            return unit.Divide(divisor);
        }
        public static bool operator ==(Unit unit1, Unit unit2)
        {
            return _HaveTheSameDimensions(unit1, unit2) && _ValuesAreEqual(unit1, unit2);
        }
        public static bool operator !=(Unit unit1, Unit unit2)
        {
            return !(unit1 == unit2);
        }

        protected static bool _HaveTheSameDimensions(Unit unit1, Unit unit2)
        {
            return UnitDimensions.HaveSameDimensions(unit1.UnitType.Dimensions(), unit2.UnitType.Dimensions());
        }
        protected static bool _ValuesAreEqual(Unit unit1, Unit unit2)
        {
            return unit1.Measurement == unit2.MeasurementIn(unit1.UnitType);
        }
    }
}