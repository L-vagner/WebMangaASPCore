namespace WebMangaASPCore.Models.Metier
{
    public class Scenariste
    {
        private int id_scenariste;
        private String nom_scenariste;
        private String prenom_scenariste;

        public int Id_scenariste { get => id_scenariste; set => id_scenariste = value; }
        public String Nom_scenariste { get => nom_scenariste; set => nom_scenariste = value; }
        public String Prenom_scenariste { get => prenom_scenariste; set => prenom_scenariste = value; }
    }
}
