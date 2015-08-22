﻿using System;
using Newtonsoft.Json;

namespace UnitClassLibrary
{
    /// <summary>
    /// Class used for storing Angles that may need to be accessed in a different measurement system
    /// Accepts anything as input
    /// 
    /// For an explanation of why this class is immutable: http://codebetter.com/patricksmacchia/2008/01/13/immutable-types-understand-them-and-use-them/
    /// 
    /// <example>
    /// radians into degrees then returned as string
    /// 
    /// double radians = 10.5;
    /// Angle a = new Angle(AngleType.Radian, radians);
    /// 
    /// a.Degrees.ToString()         //for decimal degrees
    /// a.ToString(AngleType.Degree) //for formatted string
    /// </example>
    /// 
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class Angle : AngularDistance
    {
        #region Constructors

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Angle(): base() { }

        /// <summary>
        /// Create an angle object from an angle value.
        /// </summary>
        /// <param name="internalUnitType">angle unit type</param>
        /// <param name="intrinsicValue">angle value</param>
        [JsonConstructor]
        public Angle(AngleType internalUnitType, double intrinsicValue) :
            base()
        {
            switch (internalUnitType)
            {
                case AngleType.Radian:

                    while (intrinsicValue >= Math.PI *2)
                    {
                        intrinsicValue -= Math.PI * 2;
                    }

                    if (intrinsicValue < 0)
                    {
                        intrinsicValue = (Math.PI * 2) - Math.Abs(intrinsicValue);
                    }

                    _intrinsicValue = intrinsicValue;

                    _internalUnitType = AngleType.Radian;

                    break;
                case AngleType.Degree:

                    while (intrinsicValue >= 360)
                    {
                        intrinsicValue = intrinsicValue - 360;
                    }

                    if (intrinsicValue < 0)
                    {
                        intrinsicValue = 360 - Math.Abs(intrinsicValue);
                    }

                    _intrinsicValue = intrinsicValue;

                    _internalUnitType = AngleType.Degree; 

                    break;
            }
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="passedAngle">angle to copy</param>
        public Angle(Angle passedAngle): base(passedAngle){ }

        /// <summary>
        /// Turns an angular distance into the corresponding angle
        /// </summary>
        /// <param name="angularDistance"></param>
        public Angle(AngularDistance angularDistance)
            : this(angularDistance.InternalUnitType, angularDistance.GetValue(angularDistance.InternalUnitType)) {}
        
        #endregion

        #region Overloaded Operators

        /* You may notice that we do not overload the increment and decrement operators nor do we overload multiplication and division.
         * This is because the user of this library does not know what is being internally stored and those operations will not return useful information. 
         */

        /// <summary>
        /// adds the two angles together
        /// </summary>
        /// <param name="a1">first angle</param>
        /// <param name="a2">second angle</param>
        /// <returns>the sum of the two angles</returns>
        public static Angle operator +(Angle a1, Angle a2)
        {
            //add the two Angle together
            //return a new Angle with the new value
            return new Angle(a1._internalUnitType, a1._intrinsicValue + a2.GetValue(a1._internalUnitType));
        }

        /// <summary>
        /// subtracts one angle from the other
        /// </summary>
        /// <param name="a1">the angle to be subtracted from</param>
        /// <param name="a2">the angle to subtract</param>
        /// <returns>the result of the first angle minus the second</returns>
        public static Angle operator -(Angle a1, Angle a2)
        {
            //subtract the two Angles together
            //return a new Angle with the new value
            return new Angle(a1._internalUnitType, a1._intrinsicValue - a2.GetValue(a1._internalUnitType));
        }

        /// <summary>
        /// multiples the angle by a double
        /// </summary>
        /// <param name="a"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Angle operator *(Angle a, double d)
        {
            return new Angle(a._internalUnitType, a._intrinsicValue * d);
        }

        /// <summary>
        /// This override determines how this object is inserted into hashtables.
        /// </summary>
        /// <returns>same hashcode as any double would</returns>
        public override int GetHashCode()
        {
            //cannot use InternalUnitType because this would give a different hash depending on how the object was crr
            return this.GetValue(AngleType.Degree).GetHashCode();
        }

        /// <summary>
        /// The value and unit in terms of what the object was created with. 
        /// If you want it in a different unit use ToString(AngleType)
        /// </summary>
        /// <returns>Should never return anything</returns>
        public override string ToString()
        {
            return this._intrinsicValue + " " + this._internalUnitType;
        }

        /// <summary>
        /// Finds the angle that points 180 degrees from this one
        /// </summary>
        /// <returns>Angle 180 degrees from this one</returns>
        public Angle Reverse()
        {
            return this - new Angle(AngleType.Degree, 180);
        }

        /// <summary>
        /// Returns an Angle object that is negated
        /// </summary>
        /// <returns>Angle that is same but from opposite colckwise direction </returns>
        public new Angle Negate()
        {
            return new Angle(AngleType.Degree, this.Degrees - (this.Degrees *2));
        }

        public Boolean Equals(Angle b)
        {
            var thisType = this._internalUnitType;
            var thisValue = this._intrinsicValue;
            var angle_1 = new AngularDistance(thisType, thisValue);

            if (b == null)
                return false;

            var otherType = b._internalUnitType;
            var otherValue = b._intrinsicValue;
            var angle_2 = new AngularDistance(otherType, otherValue);

            return angle_1 == angle_2;

        }

        public static Angle ArcSin(double r)
        {
            if (r > 1.0)
            {
                r = Math.Floor(r);
                if (r != 1.0)
                {
                    throw new ArgumentOutOfRangeException("The arcsin function does not take values greater than 1.0");
                }
            }
            else if (r < -1.0)
            {
                r = Math.Ceiling(r);
                if (r != -1.0)
                {
                    throw new ArgumentOutOfRangeException("The arcsin function does not take values less than -1.0");
                }
            }
            return new Angle(AngleType.Radian, Math.Asin(r));
        }

        public static Angle ArcCos(double r)
        {
            if (r > 1.0)
            {
                r = Math.Floor(r);
                if (r != 1.0)
                {
                    throw new ArgumentOutOfRangeException("The arccos function does not take values greater than 1.0");
                }
            }
            else if (r < -1.0)
            {
                r = Math.Ceiling(r);
                if (r != -1.0)
                {
                    throw new ArgumentOutOfRangeException("The arccos function does not take values less than -1.0");
                }
            }
            return new Angle(AngleType.Radian, Math.Acos(r));
        }

        #endregion
    }
}
