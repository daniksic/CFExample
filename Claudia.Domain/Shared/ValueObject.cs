using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Claudia.Domain.Shared
{
    public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (obj ==null)
            {
                return false;
            }

            T other = obj as T;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            int startValue = 19;
            int multiplyer = 59;

            int hashCode = startValue;
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value !=null)
                {
                    hashCode = hashCode*multiplyer + value.GetHashCode();
                }
            }

            return hashCode;
        }


        public virtual bool Equals(T other)
        {
            if (other==null)
                    return false;
            

            Type t = GetType();
            Type otherType = other.GetType();

            if (t != otherType)
                    return false;


            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                object val1 = field.GetValue(other);
                object val2 = field.GetValue(this);

                if (val1 != null)
                {
                    if (val2 != null) return false;
                }

                else if (!val1.Equals(val2))
                {
                    return false;
                }
            }

            return true;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            Type t = GetType();

            var fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));

                t = t.BaseType;
            }

            return fields;
        }

        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            return !(x == y);
        }
    }
}
