using System;

namespace SamlResponseFactory.DataTypes
{
    /// <summary>
    /// Target URL, Destination of the Response
    /// </summary>
    public struct Recipient : IEquatable<Recipient>
    {
        public string Value { get; }
        public Recipient(string value)
        {
            Value = value;
        }
        public static Recipient From(string value) => new Recipient(value);
        public static implicit operator string(Recipient key) => key.Value;
        
        public override string ToString() => Value;

        #region Equals

        public bool Equals(Recipient other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Recipient other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(Recipient left, Recipient right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Recipient left, Recipient right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}