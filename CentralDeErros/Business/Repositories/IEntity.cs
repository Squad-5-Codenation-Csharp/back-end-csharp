using System;
using System.Collections.Generic;
using System.Text;

namespace CentralDeErros.Data.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }

        bool Active { get; set; }
    }
}
