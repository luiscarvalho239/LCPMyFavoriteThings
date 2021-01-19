using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFavoriteThings.Data.Models
{
    public class Tutorial
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TutorialId { get; set; }

        [Required(ErrorMessage = "O titulo do tutorial é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O texto do tutorial é obrigatório")]
        public string Texto { get; set; }

        [Required(ErrorMessage = "A capa (url) do tutorial é obrigatório")]
        public string Capa { get; set; }

        [Required(ErrorMessage = "A categoria (ex: Programação) do tutorial é obrigatório")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "O url do tutorial é obrigatório")]
        public string Url { get; set; }

        public int Classificacao { get; set; }

        public bool IsYourFavorite { get; set; }
    }
}
