using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyFavoriteThings.Data.Models
{
    public class Livro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int LivroId { get; set; }

        [Required(ErrorMessage = "O titulo do livro é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição do livro é obrigatório")]
        public string Desc { get; set; }

        [Required(ErrorMessage = "A capa (url) do livro é obrigatório")]
        public string Capa { get; set; }

        [Required(ErrorMessage = "O género do livro é obrigatório")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "A data da lançamento do livro é obrigatório")]
        public DateTime DataLancamento { get; set; }

        [Required(ErrorMessage = "O url do livro é obrigatório")]
        public string Url { get; set; }

        public string Idade { get; set; }

        public int Classificacao { get; set; }

        public bool IsYourFavorite { get; set; }
    }
}
