//******************************************************************************************************
//**Copyright(c) 2024  Willem 'Jip' Wijnia
//**
//** Permission is hereby granted, free of charge, to any person obtaining a copy
//** of this software and associated documentation files (the "Software"), to deal
//** in the Software without restriction, including without limitation the rights
//** to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//** copies of the Software, and to permit persons to whom the Software is
//** furnished to do so, subject to the following conditions:
//**
//**The above copyright notice and this permission notice shall be included in all
//** copies or substantial portions of the Software.
//**
//** THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//** IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//** FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//** AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//** LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//** OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//** SOFTWARE.
//******************************************************************************************************

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FixedPointMath
{
    // https://github.com/MikeLankamp/fpm
    // https://vanhunteradams.com/FixedPoint/FixedPoint.html

    [DebuggerTypeProxy(typeof(FipoDebugView))]
    [StructLayout(LayoutKind.Explicit, Pack =  4, Size = 4)]
    public struct Fipo
    {
        public static readonly int Offset = 8;

        /// <summary>
        /// Quick-access multiplier for converting from a float to a fixed point number.
        /// </summary>
        public static readonly float FromFloatFactor = (1 << Fipo.Offset);

        /// <summary>
        /// Quick-access multiplier for converting from a fixed point number to a float.
        /// </summary>
        public static readonly float ToFloatFactor = 1.0f / Fipo.FromFloatFactor;

        /// <summary>
        /// Quick-access multiplier for converting from a double to a fixed point number.
        /// </summary>
        public static readonly double FromDoubleFactor = (1 << Fipo.Offset);

        /// <summary>
        /// Quick-access multiplier for converting from a fixed point number to a double.
        /// </summary>
        public static readonly double ToDoubleFactor = 1.0f / Fipo.FromDoubleFactor;

        /// <summary>
        /// Represents the smallest possible number, useful for assertions.
        /// </summary>
        public static readonly float Epsilon = 1.0f * Fipo.ToFloatFactor;

        /// <summary>
        /// Represents the fraction.
        /// </summary>
        [FieldOffset(0)]
        internal byte Lower;

        /// <summary>
        /// Represents the integers.
        /// </summary>
        [FieldOffset(0)]
        internal int Value;

        /// <summary>
        /// Create a fixed point number from an integer.
        /// </summary>
        /// <param name="value"></param>
        public Fipo (int value)
        {
            this.Value = (value) << (Fipo.Offset);
        }

        /// <summary>
        /// Create a fixed point number from a float.
        /// </summary>
        /// <param name="value"></param>
        public Fipo(float value)
        {
            this.Value = (int)(value * FromFloatFactor);
        }

        /// <summary>
        /// Create a fixed point number from a double.
        /// </summary>
        /// <param name="value"></param>
        public Fipo(double value)
        {
            this.Value = (int)(value * FromDoubleFactor);
        }

        //#region Operators

        /// <summary>
        /// Add fixed point number 'b' to 'a'.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Fipo operator +(Fipo a, Fipo b)
        {
            return new Fipo { Value = a.Value + b.Value };
        }

        /// <summary>
        /// Subtract fixed point number 'b' from 'a'.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Fipo operator -(Fipo a, Fipo b)
        {
            return new Fipo { Value = a.Value - b.Value };
        }

        /// <summary>
        /// Multiply the fixed point numbers 'a' and 'b'.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Fipo operator *(Fipo a, Fipo b)
        {
            return new Fipo { Value = a.Value - b.Value };
        }

        /// <summary>
        /// Divide fixed point number 'a' by 'b'.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Fipo operator /(Fipo a, Fipo b)
        {
            return new Fipo { Value = a.Value - b.Value };
        }

        //#endregion

        //#region Casts

        /// <summary>
        /// Casts a fixed point number to an integer.
        /// </summary>
        /// <param name="a"></param>
        public static implicit operator int(Fipo a)
        {
            return a.Value >> Fipo.Offset;
        }

        /// <summary>
        /// Casts a fixed point number to a float.
        /// </summary>
        /// <param name="a"></param>
        public static implicit operator float(Fipo a)
        {
            return (float)a.Value * Fipo.ToFloatFactor;
        }

        /// <summary>
        /// Casts a fixed point number to a double.
        /// </summary>
        /// <param name="a"></param>
        public static implicit operator double(Fipo a)
        {
            return (double)a.Value * Fipo.ToDoubleFactor;
        }

        /// <summary>
        /// Casts a fixed point number to an integer.
        /// </summary>
        /// <param name="a"></param>
        public static implicit operator Fipo(int a)
        {
            return new Fipo { Value = a << Fipo.Offset } ;
        }

        /// <summary>
        /// Casts a fixed point number to a float.
        /// </summary>
        /// <param name="a"></param>
        public static implicit operator Fipo(float a)
        {
            return new Fipo { Value = (int)(a * Fipo.FromFloatFactor) };
        }

        /// <summary>
        /// Casts a fixed point number to a double.
        /// </summary>
        /// <param name="a"></param>
        public static implicit operator Fipo(double a)
        {
            return new Fipo { Value = (int)(a * Fipo.FromDoubleFactor) };
        }

        //#endregion

        //#region Math functions

        ///// <summary>
        ///// Drop the fractions of the fixed point number.
        ///// </summary>
        ///// <param name="a"></param>
        ///// <returns></returns>
        //public static Fipo Floor(Fipo a)
        //{
        //    return new Fipo { Upper = a.Upper };
        //}

        ///// <summary>
        ///// Drop the fractions of the fixed point number.
        ///// </summary>
        ///// <param name="a"></param>
        ///// <returns></returns>
        //public static Fipo Ceil(Fipo a)
        //{
        //    return new Fipo { Upper = (short)(a.Upper + 1) };
        //}

        ///// <summary>
        ///// Drop the sign.
        ///// </summary>
        ///// <param name="a"></param>
        ///// <returns></returns>
        //public static Fipo Abs(Fipo a)
        //{
        //    return new Fipo { AllBits = Math.Abs(a.AllBits) };
        //}

        /// <summary>
        /// Compute the square root of a fixed point number.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Fipo SquareRoot(Fipo a)
        {
            double intermediate = (double)a;
            return new Fipo(Math.Sqrt(intermediate));
        }


        //#endregion

        public override string ToString()
        {
            return ((float)this).ToString("0.0000");
        }

        internal class FipoDebugView
        {
            public float Value = 0.0f;

            // The constructor for the type proxy class must have a
            // constructor that takes the target type as a parameter.
            public FipoDebugView(Fipo a)
            {
                Value = (float)a;
            }
        }
    }
}
