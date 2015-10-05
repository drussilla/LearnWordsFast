using System;

namespace LearnWordsFast.DAL.Models
{
    public abstract class IdModel : IEquatable<IdModel>
    {
        protected IdModel()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid Id { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public virtual bool Equals(IdModel other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var typedObj = obj as IdModel;
            if (typedObj == null)
            {
                return false;
            }

            return Equals(typedObj);
        }
    }
}