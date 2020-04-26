using System;

namespace SamlResponseFactory.DataTypes
{
    /// <summary>
    /// SP EntityId
    /// </summary>
    public struct Audience : IEquatable<Audience>
    {
        public string Value { get; }
        public Audience(string value)
        {
            Value = value;
        }
        public static Audience From(string value) => new Audience(value);
        public static implicit operator string(Audience key) => key.Value;
        
        public override string ToString() => Value;

        #region Equals

        public bool Equals(Audience other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Audience other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(Audience left, Audience right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Audience left, Audience right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}