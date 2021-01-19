using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFavoriteThings.Data.Models
{
    public class Filme
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int FilmeId { get; set; }

        [Required(ErrorMessage = "O titulo do filme é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição do filme é obrigatório")]
        public string Desc { get; set; }

        [Required(ErrorMessage = "A capa (url) do filme é obrigatório")]
        public string Capa { get; set; }

        [Required(ErrorMessage = "O género do filme é obrigatório")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "A data do lançamento do filme é obrigatório")]
        public DateTime DataLancamento { get; set; }

        [Required(ErrorMessage = "A duração do filme (em minutos) é obrigatório")]
        public int Duracao { get; set; }

        [Required(ErrorMessage = "O idioma do filme é obrigatório")]
        public string Idioma { get; set; }

        [Required(ErrorMessage = "O nome da companhia do filme é obrigatório")]
        public string Companhia { get; set; }

        [Required(ErrorMessage = "O nome da distribuição do filme é obrigatório")]
        public string Distribuicao { get; set; }

        public string Idade { get; set; }

        public int Classificacao { get; set; }

        public bool IsYourFavorite { get; set; }
    }
}
