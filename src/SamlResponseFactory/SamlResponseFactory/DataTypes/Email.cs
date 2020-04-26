using System;

namespace SamlResponseFactory.DataTypes
{
    public struct Email : IEquatable<Email>
    {
        public string Value { get; }
        public Email(string value)
        {
            Value = value;
        }
        public static Email From(string value) => new Email(value);
        public static implicit operator string(Email key) => key.Value;
        
        public override string ToString() => Value;

        #region Equals

        public bool Equals(Email other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Email other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(Email left, Email right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Email left, Email right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}