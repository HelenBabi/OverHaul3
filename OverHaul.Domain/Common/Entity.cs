using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Domain.Common
{
    public abstract class Entity : IEntity
    {
        public Entity()
        {
            Id= Guid.NewGuid();
            CreateAt = DateTime.Now;
            RowVersion=new byte[0];
        }
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        [DefaultValue(new byte[0])]
        public byte[] RowVersion { get; set; } = new byte[0];
    }
}
