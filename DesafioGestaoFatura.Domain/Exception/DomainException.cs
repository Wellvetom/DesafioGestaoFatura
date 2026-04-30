using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Domain.Exception
{
    public class DomainException : IOException
    {
        public DomainException(string message) : base(message) { }
    }
}
