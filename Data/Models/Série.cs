using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFavoriteThings.Data.Models
{
    public class Série
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SerieId { get; set; }

        [Required(ErrorMessage = "O titulo da série é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição da série é obrigatório")]
        public string Desc { get; set; }

        [Required(ErrorMessage = "A capa (url) da série é obrigatório")]
        public string Capa { get; set; }

        [Required(ErrorMessage = "O género da série é obrigatório")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "A data da lançamento da série é obrigatório")]
        public DateTime DataLancamento { get; set; }

        [Required(ErrorMessage = "A duração da série é obrigatório")]
        public DateTime Duracao { get; set; }

        [Required(ErrorMessage = "O idioma da série é obrigatório")]
        public string Idioma { get; set; }

        [Required(ErrorMessage = "O nome da emissora da série é obrigatório")]
        public string Emissora { get; set; }

        public string Idade { get; set; }

        public int Classificacao { get; set; }

        public bool IsYourFavorite { get; set; }
    }
}
