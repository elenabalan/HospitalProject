using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class Entity
    {
        #region Public static members

        public static bool operator ==(Entity left, Entity right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (ReferenceEquals(left, null))
                return false;

            return left.Equals(right);
        }
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left==right);
        }
        #endregion

        #region Public members

        public virtual long Id { get; set; }

        public virtual bool Equals(Entity other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(other, this))
                return true;

            long thisId = Id;
            long otherId = other.Id;
            if (!IsTransient(this) && !IsTransient(other) && Equals(thisId, otherId))
            {
                Type thisType = GetUnproxiedType();
                Type otherType = other.GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) || otherType.IsAssignableFrom(thisType);
            }
            return false;
        }

        #endregion

        #region Non-public static members

        private static bool IsTransient(Entity entity)
        {
            return entity != null && Equals(entity.Id, default(long));
        }

        #endregion
        
        #region   Non-public members

        protected virtual Type GetUnproxiedType()
        {
            return GetType();
        }

        #endregion
    }
}
