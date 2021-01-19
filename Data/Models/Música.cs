using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFavoriteThings.Data.Models
{
    public class Música
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int MusicaId { get; set; }

        [Required(ErrorMessage = "O titulo da música é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição da música é obrigatório")]
        public string Desc { get; set; }

        [Required(ErrorMessage = "A capa (url) da música é obrigatório")]
        public string Capa { get; set; }

        [Required(ErrorMessage = "O género da música é obrigatório")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "A data da lançamento da música é obrigatório")]
        public DateTime DataLancamento { get; set; }

        [Required(ErrorMessage = "O url da música é obrigatório")]
        public string Url { get; set; }

        public string Idade { get; set; }

        public int Classificacao { get; set; }

        public bool IsYourFavorite { get; set; }
    }
}
