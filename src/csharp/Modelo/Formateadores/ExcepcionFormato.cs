using System;

namespace Modelo.Formateadores
{
    [Serializable]
    public class ExcepcionFormato : Exception
    {
        public ExcepcionFormato() { }
        public ExcepcionFormato(string message) : base(message) { }
        public ExcepcionFormato(string message, Exception inner) : base(message, inner) { }
        protected ExcepcionFormato(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
