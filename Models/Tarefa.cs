using System;
using System.ComponentModel.DataAnnotations;

namespace GestorTarefas.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Descricao { get; set; } = string.Empty;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public bool Concluida { get; set; }
    }
}
