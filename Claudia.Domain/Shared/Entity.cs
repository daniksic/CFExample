using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Claudia.Domain.Shared
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        public TId Id { get; protected set; }

        protected Entity(TId id)
        {
            if (object.Equals(id, default(TId)))
            {
                throw new ArgumentException("The Id cannot be the type's default value.","id");
            }

            this.Id = id;
        }

        // EFrequires an empty ctor
        protected Entity()
        {
        }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity<TId>;
            if (entity!=null)
            {
                return this.Equals(entity);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public bool Equals(Entity<TId> other)
        {
            if (other==null)
            {
                return false;
            }

            return this.Id.Equals(other.Id);
        }
    }
}
