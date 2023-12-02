using MiniSuperPF.Models;

namespace MiniSuperPF.ModelsDTOs
{

    public class UserDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string? Cedula { get; set; }
        public string Contrasennia{ get; set; } = null!;
        public string? Direccion{ get; set; }
        public string NumeroTelefono { get; set; } = null!;
        public int IdRol { get; set; }
        public int IdEstado{ get; set; }

        public string EstadoDescripcion { get; set; } = null!;
        public string RolDescripcion { get; set; } = null!;


    }
}
