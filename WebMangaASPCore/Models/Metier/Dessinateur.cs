namespace WebMangaASPCore.Models.Metier
{
    public class Dessinateur
    {
        private int id_dessinateur;
        private String nom_dessinateur;
        private String prenom_dessinateur;

        public int Id_dessinateur { get => id_dessinateur; set => id_dessinateur= value; }
        public String Nom_dessinateur { get => nom_dessinateur; set => nom_dessinateur= value; }
        public String Prenom { get => prenom_dessinateur; set => prenom_dessinateur = value; }
    }
}
