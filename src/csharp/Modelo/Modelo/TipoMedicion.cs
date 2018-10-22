namespace Modelo
{
    public class TipoMedicion
    {
        byte? id;
        string nombre;

        public TipoMedicion(){}

        public TipoMedicion(string nombre)
        {
            this.Nombre = nombre;
        }

        public byte? Id
        {
            get { return id; }
            set { id = value; }
        }        

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public bool mismoNombre(string unNombre)
        {
            return this.nombre == unNombre;
        }

        public override string ToString()
        {
            return string.Format("ID: {0}\tNombre: {1}", Id, Nombre);
        }
    }
}