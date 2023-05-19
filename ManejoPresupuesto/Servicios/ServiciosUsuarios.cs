namespace ManejoPresupuesto.Servicios
{
    public interface IServiciosUsuarios
    {
        int ObtenerUsuasriosId();
    }
    public class ServiciosUsuarios: IServiciosUsuarios
    {
        public int ObtenerUsuasriosId() 
        {
            return 1;
        }
    }
}
