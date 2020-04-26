using System;

namespace SamlResponseFactory.DataTypes
{
    public struct RequestId : IEquatable<RequestId>
    {
        public string Value { get; }
        public RequestId(string value)
        {
            Value = value;
        }
        public static RequestId From(string value) => new RequestId(value);
        public static implicit operator string(RequestId key) => key.Value;

        public override string ToString() => Value;

        #region Equals

        public bool Equals(RequestId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is RequestId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(RequestId left, RequestId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RequestId left, RequestId right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}