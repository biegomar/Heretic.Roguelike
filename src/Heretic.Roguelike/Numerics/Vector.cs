﻿using System;

namespace Heretic.Roguelike.Numerics;

    /// <summary>
    /// The vector class.
    /// The calculations are based on general vector mathematics.
    /// </summary>
    public class Vector
    {
        /// <summary>
        /// X
        /// </summary>
        public float X { get; init; }
        
        /// <summary>
        /// Y
        /// </summary>
        public float Y { get; init; }
        
        /// <summary>
        /// Z
        /// </summary>
        public float Z { get; init; }

        /// <summary>
        /// The base constructor.
        /// </summary>
        public Vector() : this(0, 0, 0) { }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="x">value x.</param>
        /// <param name="y">value y.</param>
        /// <param name="z">value z.</param>
        public Vector(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Operator overload +.
        /// </summary>
        /// <param name="v1">The first vector to add.</param>
        /// <param name="v2">The second vector to add.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        /// <summary>
        /// Operator overload -.
        /// </summary>
        /// <param name="v1">The first vector to sub.</param>
        /// <param name="v2">The second vector to sub.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        /// <summary>
        /// Operator overload * (scalar multiplication).
        /// </summary>
        /// <param name="v1">The vector to scale.</param>
        /// <param name="scalar">The scale factor.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector operator *(Vector v1, float scalar) 
        { 
            return new Vector(v1.X * scalar, v1.Y * scalar, v1.Z * scalar);
        }

        /// <summary>
        /// Operator overload * (scalar multiplication).
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>The resulting vector.</returns>
        public static float operator *(Vector v1, Vector v2)
        {
            return DotProduct(v1, v2);
        }

        /// <summary>
        /// Operator overload == (equality).
        /// </summary>
        /// <param name="v1">The first vector to compare.</param>
        /// <param name="v2">The second vector to compare.</param>
        /// <returns>The resulting vector.</returns>
        public static bool operator ==(Vector v1, Vector v2)
        {
            var tolerance = .001f;
            if (ReferenceEquals(v1, v2))
                return true;

            if (v1 is null || v2 is null)
                return false;

            return Math.Abs(v1.X - v2.X) < tolerance 
                   && Math.Abs(v1.Y - v2.Y) < tolerance
                   && Math.Abs(v1.Z - v2.Z) < tolerance;
        }

        /// <summary>
        /// Operator overload != (inequality).
        /// </summary>
        /// <param name="v1">The first vector to compare.</param>
        /// <param name="v2">The second vector to compare.</param>
        /// <returns>The resulting vector.</returns>
        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }       

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Vector other)
            {
                return this == other;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        /// <summary>
        /// The length of the vector.
        /// </summary>
        /// <returns>The length of the vector.</returns>
        public float Length()
        {
            return (float)Math.Sqrt(SquaredLength());
        }

        /// <summary>
        /// The squared length of the vector.
        /// </summary>
        /// <returns>The squared length of the vector.</returns>
        public float SquaredLength()
        {
            return PowerOfTwo(this.X) + PowerOfTwo(this.Y) + PowerOfTwo(this.Z);
        }

        /// <summary>
        /// The distance to another vector.
        /// </summary>
        /// <param name="v">The vector to which the distance is to be calculated.</param>
        /// <returns>The distance of the two vectors.</returns>
        public float Distance(Vector v)
        {
            return Distance(this, v);
        }
        
        /// <summary>
        /// The distance of two vectors.
        /// </summary>
        /// <param name="v1">The first vector to which the distance is to be calculated.</param>
        /// <param name="v2">The second vector to which the distance is to be calculated.</param>
        /// <returns>The distance of the two vectors.</returns>
        public static float Distance(Vector v1, Vector v2)
        {
            var differenceVector = v1 - v2;
            var lengthOfDifferenceVector = differenceVector.Length();

            return lengthOfDifferenceVector;
        }

        /// <summary>
        /// Dot product of two vectors.
        /// </summary>
        /// <param name="v">The second vector for the dot product.</param>
        /// <returns></returns>
        public float DotProduct(Vector v)
        {
            return DotProduct(this, v);
        }

        /// <summary>
        /// Dot product of two vectors.
        /// </summary>
        /// <param name="v1">The first vector for the dot product.</param>
        /// <param name="v2">The second vector for the dot product.</param>
        /// <returns></returns>
        public static float DotProduct(Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        /// <summary>
        /// Cross product of two vectors.
        /// </summary>
        /// <param name="v">The second vector for the cross product.</param>
        /// <returns>The resulting vector.</returns>
        public Vector CrossProduct(Vector v)
        {
            return CrossProduct(this, v);
        }

        /// <summary>
        /// Cross product of two vectors.
        /// </summary>
        /// <param name="v1">The first vector for the cross product.</param>
        /// <param name="v2">The second vector for the cross product.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector CrossProduct(Vector v1, Vector v2)
        {
            return new Vector()
            {
                X = v1.Y * v2.Z - v1.Z * v2.Y,
                Y = v1.Z * v2.X - v1.X * v2.Z,
                Z = v1.X * v2.Y - v1.Y * v2.X
            };
        }
        
        public override string ToString()
        {
            return $"[{this.X}, {this.Y}, {this.Z}]";
        }

        public static Vector Zero => new Vector(0, 0, 0);
        public static Vector One => new Vector(1, 1, 1);
        public static Vector Up => new Vector(0, 1, 0);
        public static Vector Down => new Vector(0, -1, 0);
        public static Vector Left => new Vector(-1, 0, 0);
        public static Vector Right => new Vector(1, 0, 0);

        /// <summary>
        /// The power of 2.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The power of 2 of the number.</returns>
        private float PowerOfTwo(float value)
        {
            return value * value;
        }       
    }