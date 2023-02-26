//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

//namespace Function
//{
    public class tuple
    {
        public class t2<i1, i2>
        {
            public i1 I1 { get; private set; }
            public i2 I2 { get; private set; }

            internal t2(i1 Item1, i2 Item2)
            {
                I1 = Item1;
                I2 = Item2;
            }
        }

        public class t3<i1, i2, i3>
        {
            public i1 I1 { get; private set; }
            public i2 I2 { get; private set; }
            public i3 I3 { get; private set; }

            internal t3(i1 Item1, i2 Item2, i3 Item3)
            {
                I1 = Item1;
                I2 = Item2;
                I3 = Item3;
            }
        }

        public class t4<i1, i2, i3, i4>
        {
            public i1 I1 { get; private set; }
            public i2 I2 { get; private set; }
            public i3 I3 { get; private set; }
            public i4 I4 { get; private set; }

            internal t4(i1 Item1, i2 Item2, i3 Item3, i4 Item4)
            {
                I1 = Item1;
                I2 = Item2;
                I3 = Item3;
                I4 = Item4;
            }
        }
        
        
        public class t5<i1, i2, i3, i4, i5>
        {
            public i1 I1 { get; private set; }
            public i2 I2 { get; private set; }
            public i3 I3 { get; private set; }
            public i4 I4 { get; private set; }
            public i5 I5 { get; private set; }

            internal t5(i1 Item1, i2 Item2, i3 Item3, i4 Item4, i5 Item5)
            {
                I1 = Item1;
                I2 = Item2;
                I3 = Item3;
                I4 = Item4;
                I5 = Item5;
            }
        }

        public class t6<i1, i2, i3, i4, i5, i6>
        {
            public i1 I1 { get; private set; }
            public i2 I2 { get; private set; }
            public i3 I3 { get; private set; }
            public i4 I4 { get; private set; }
            public i5 I5 { get; private set; }
            public i6 I6 { get; private set; }

            internal t6(i1 Item1, i2 Item2, i3 Item3, i4 Item4, i5 Item5, i6 Item6)
            {
                I1 = Item1;
                I2 = Item2;
                I3 = Item3;
                I4 = Item4;
                I5 = Item5;
                I6 = Item6;
            }
        }

    public class t7<i1, i2, i3, i4, i5, i6, i7>
    {
        public i1 I1 { get; set; }
        public i2 I2 { get; set; }
        public i3 I3 { get; set; }
        public i4 I4 { get; set; }
        public i5 I5 { get; set; }
        public i6 I6 { get; set; }
        public i7 I7 { get; set; }

        internal t7(i1 Item1, i2 Item2, i3 Item3, i4 Item4, i5 Item5, i6 Item6, i7 Item7)
        {
            I1 = Item1;
            I2 = Item2;
            I3 = Item3;
            I4 = Item4;
            I5 = Item5;
            I6 = Item6;
            I7 = Item7;
        }
    }
    
    /*
    public class columns_types<type, type_int>
    {
        public type Type_sql { get; set; }
        public type_int Type_net { get; set; }

        internal columns_types(type Item1, type_int Item2)
        {
            Type_sql = Item1;
            Type_net = Item2;
        }
    }   
    */
}
//}
