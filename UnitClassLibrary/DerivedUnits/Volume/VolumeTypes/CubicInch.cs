﻿/*
    This file is part of Unit Class Library.
    Copyright (C) 2016 Paragon Component Systems, LLC.

    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitClassLibrary.DistanceUnit.DistanceTypes.Imperial.InchUnit;


namespace UnitClassLibrary
{
    public class CubicInch : VolumeType
    {
        private static UnitDimensions _dimensions = new UnitDimensions(1.0, new List<FundamentalUnitType>() { new Inch(), new Inch(), new Inch() }); 
        public override UnitDimensions Dimensions => _dimensions;


        public override string AsStringSingular()
        {
            return "cu. in.";
        }

        public override string AsStringPlural()
        {
            return "cu. in.";
        }
    }
}
