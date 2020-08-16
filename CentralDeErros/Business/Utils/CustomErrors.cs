using System;

namespace CentralDeErros.Business.Utils
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        { }
    }

    public class DuplicatedEntity : Exception
    {
        public DuplicatedEntity(string message) : base(message)
        { }
    }
}
