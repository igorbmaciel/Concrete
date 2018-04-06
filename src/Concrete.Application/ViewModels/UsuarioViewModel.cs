using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete.Application.ViewModels
{
    public sealed class UsuarioViewModel
    {
        public UsuarioViewModel()
        {
            Telefones = new List<TelefoneViewModel>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("Nome do Usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo Email")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("Email do Usuário")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preencha o campo Senha")]
        [MaxLength(20, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Senha { get; set; }

        public ICollection<TelefoneViewModel> Telefones { get; set; }

        public string DataCriacao { get; set; }
        public string DataAtualizacao { get; set; }
        public string DataUltimoLogin { get; set; }
        public string Token { get; set; }
    }
}
