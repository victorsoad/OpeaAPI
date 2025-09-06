using Opea.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Opea.Application.DTOs
{
    // DTO (Data Transfer Object) para transferir dados entre as camadas.    
    public class ClienteDto
    {
        public Guid Id { get; set; }

        [Required]
        public string NomeEmpresa { get; set; }

        public PorteEmpresa PorteEmpresa { get; set; }
    }
}
