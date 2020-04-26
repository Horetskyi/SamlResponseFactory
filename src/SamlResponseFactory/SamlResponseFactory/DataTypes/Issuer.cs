using System;

namespace SamlResponseFactory.DataTypes
{
    /// <summary>
    /// IdP EntityId
    /// </summary>
    public struct Issuer : IEquatable<Issuer>
    {
        public string Value { get; }
        public Issuer(string value)
        {
            Value = value;
        }
        public static Issuer From(string value) => new Issuer(value);
        public static implicit operator string(Issuer key) => key.Value;
        
        public override string ToString() => Value;

        #region Equals

        public bool Equals(Issuer other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Issuer other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(Issuer left, Issuer right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Issuer left, Issuer right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}