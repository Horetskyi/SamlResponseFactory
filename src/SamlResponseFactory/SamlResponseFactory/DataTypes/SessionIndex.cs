using System;

namespace SamlResponseFactory.DataTypes
{
    public struct SessionIndex : IEquatable<SessionIndex>
    {
        public string Value { get; }
        public SessionIndex(string value)
        {
            Value = value;
        }
        public static SessionIndex From(string value) => new SessionIndex(value);
        public static implicit operator string(SessionIndex key) => key.Value;

        public override string ToString() => Value;

        #region Equals

        public bool Equals(SessionIndex other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is SessionIndex other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(SessionIndex left, SessionIndex right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SessionIndex left, SessionIndex right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}