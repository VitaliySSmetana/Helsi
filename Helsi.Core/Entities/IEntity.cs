using System;
using System.Collections.Generic;
using System.Text;

namespace Helsi.Core.Entities
{
    public interface IEntity : IEntity<int>
    {
        new int Id { get; set; }
    }

    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
