using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFavoriteThings.Data.Models
{
    public class Jogo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int JogoId { get; set; }

        [Required(ErrorMessage = "O titulo do jogo é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição do jogo é obrigatório")]
        public string Desc { get; set; }

        [Required(ErrorMessage = "A capa (imagem) do jogo é obrigatório")]
        public string Capa { get; set; }

        [Required(ErrorMessage = "A data de lançamento do jogo é obrigatório")]
        public DateTime DataLancamento { get; set; }

        [Required(ErrorMessage = "O género do jogo é obrigatório")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "A empresa do jogo é obrigatório")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "O editor do jogo é obrigatório")]
        public string Editor { get; set; }

        public bool IsYourFavorite { get; set; }
    }
}
