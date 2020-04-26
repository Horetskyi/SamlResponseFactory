using System;
using SAML2.Utils;

namespace SamlResponseFactory.DataTypes
{
    public struct IssueInstant : IEquatable<IssueInstant>
    {
        public DateTime DateTime { get; }
        public string String => Saml20Utils.ToUtcString(DateTime);

        public IssueInstant(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public override string ToString()
        {
            return String;
        }

        #region Equals

        public bool Equals(IssueInstant other)
        {
            return DateTime.Equals(other.DateTime);
        }

        public override bool Equals(object obj)
        {
            return obj is IssueInstant other && Equals(other);
        }

        public override int GetHashCode()
        {
            return DateTime.GetHashCode();
        }

        public static bool operator ==(IssueInstant left, IssueInstant right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(IssueInstant left, IssueInstant right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}