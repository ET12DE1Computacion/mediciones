using System;

namespace Modelo.Placa
{
    [Serializable]
    public class ExcepcionLectura : Exception
    {
        public ExcepcionLectura() { }
        public ExcepcionLectura(string message) : base(message) { }
        public ExcepcionLectura(string message, Exception inner) : base(message, inner) { }
        protected ExcepcionLectura(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
