using System.Data;
using WebMangaASPCore.Models.Persistance;
using WebMangaASPCore.Models.MesExceptions;
using WebMangaASPCore.Models.Metier;

namespace WebMangaASPCore.Models.Dao
{
	public class ServiceManga
	{
		public static DataTable GetTousLesMangas()
		{
			DataTable mesMangas;
			Serreurs er = new Serreurs("Erreur sur le lecture des Mangas", "Manga.getMangas()");
			try
			{
				String mysql = "SELECT manga.id_manga, lib_genre, nom_dessinateur, nom_scenariste, prix, titre, couverture ";
				mysql += "from manga join genre on manga.id_genre=genre.id_genre ";
				mysql += "join dessinateur on manga.id_dessinateur=dessinateur.id_dessinateur ";
				mysql += "join scenariste on manga.id_scenariste=scenariste.id_scenariste ";
				mysql += "order by manga.id_manga";

				mesMangas = DBInterface.Lecture(mysql, er);

				return mesMangas;
			}
			catch (MonException e)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
			}
		}

		public static Manga GetunManga(String id)
		{
			DataTable dt;
			Manga unManga = null;
			Serreurs er = new Serreurs("Erreur sur lecture des Mangas", "ServiceManga.GetunManga()");
			try
			{
				String mysql = "SELECT id_manga, id_genre, id_dessinateur, id_scenariste, titre, prix, couverture ";
				mysql += "from Manga ";
				mysql += "where id_manga = " + id;
				dt = DBInterface.Lecture(mysql, er);
				if (dt.IsInitialized && dt.Rows.Count > 0)
				{
					unManga = new Manga();
					DataRow dataRow = dt.Rows[0];
					unManga.Id_manga = int.Parse(dataRow[0].ToString());
					unManga.Id_genre = int.Parse(dataRow[1].ToString());
					unManga.Id_dessinateur = int.Parse(dataRow[2].ToString());
					unManga.Id_scenariste = int.Parse(dataRow[3].ToString());
					unManga.Titre = dataRow[4].ToString();
					unManga.Prix = Double.Parse(dataRow[5].ToString());
					unManga.Couverture = dataRow[6].ToString();
					return unManga;
				}
				else
					return null;
			}
			catch (MonException e)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
			}
		}

		public static void UpdateManga(Manga unM)
		{
			String preparedTitre = unM.Titre.Replace("\'", "\'\'");
			String preparedCouverture = unM.Couverture.Replace("\'", "\'\'");
			Serreurs er = new Serreurs("Erreur sur l'ecriture des Mangas", "ServiceManga.UpdateManga()");
			String requete = "UPDATE Manga SET "
					+ "id_scenariste = " + unM.Id_scenariste
					+ ", id_dessinateur = " + unM.Id_dessinateur
					+ ", id_genre = " + unM.Id_genre
					+ ", titre = '" + preparedTitre
					+ "', Prix = " + unM.Prix.ToString().Replace(',', '.')
					+ ", couverture = '" + preparedCouverture
					+ "' WHERE id_manga = " + unM.Id_manga;
			try
			{
				DBInterface.Execute_Transaction(requete);
			}
			catch (MonException erreur)
			{
				throw erreur;
			}
		}

		public static void AddManga(Manga unM)
		{
			String preparedTitre = unM.Titre.Replace("\'", "\'\'");
			String preparedCouverture = unM.Couverture.Replace("\'", "\'\'");
			Serreurs er = new Serreurs("Erreur sur l'ecriture des Mangas", "ServiceManga.UpdateManga()");
			String requete = "INSERT INTO Manga (id_dessinateur, id_scenariste, id_genre, titre, prix, couverture) "
					+ "values(" + unM.Id_scenariste
					+ " , " + unM.Id_dessinateur
					+ " , " + unM.Id_genre
					+ " , '" + preparedTitre
					+ "' , " + unM.Prix.ToString().Replace(',', '.')
					+ " , '" + preparedCouverture + "');";
			try
			{
				DBInterface.Execute_Transaction(requete);
			}
			catch (MonException erreur)
			{
				throw erreur;
			}
		}

		public static DataTable GetTousLesDessinateurs()
		{
			DataTable mesDessinateurs;
			Serreurs er = new Serreurs("Erreur sur la lecture des Dessinateurs", "Manga.getDessinateurs()");
			try
			{
				String mysql = "SELECT id_dessinateur, nom_dessinateur, prenom_dessinateur ";
				mysql += "from dessinateur";

				mesDessinateurs = DBInterface.Lecture(mysql, er);

				return mesDessinateurs;
			}
			catch (MonException e)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
			}
		}

		public static DataTable GetTousLesScenaristes()
		{
			DataTable mesDessinateurs;
			Serreurs er = new Serreurs("Erreur sur la lecture des Scenaristes", "Manga.getScenaristes()");
			try
			{
				String mysql = "SELECT id_scenariste, nom_scenariste, prenom_scenariste ";
				mysql += "from scenariste";

				mesDessinateurs = DBInterface.Lecture(mysql, er);

				return mesDessinateurs;
			}
			catch (MonException e)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
			}
		}

		public static DataTable GetTousLesGenres()
		{
			DataTable mesDessinateurs;
			Serreurs er = new Serreurs("Erreur sur la lecture des Genres", "Manga.getGenres()");
			try
			{
				String mysql = "SELECT id_genre, lib_genre ";
				mysql += "from genre";

				mesDessinateurs = DBInterface.Lecture(mysql, er);

				return mesDessinateurs;
			}
			catch (MonException e)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
			}
		}

		public static DataTable GetTitres()
		{
			DataTable mesTitres;
			Serreurs er = new Serreurs("Erreur sur la lecture des Titres", "Manga.getTitres()");
			try
			{
				String mysql = "SELECT titre ";
				mysql += "from manga";

				mesTitres = DBInterface.Lecture(mysql, er);

				return mesTitres;
			}
			catch (MonException e)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
			}
		}

		public static DataTable GetMangas(string recherche)
		{

			DataTable mesMangas;
			Serreurs er = new Serreurs("Erreur sur le lecture des Mangas", "Manga.getMangas()");
			try
			{
				String mysql = "SELECT manga.id_manga, lib_genre, nom_dessinateur, nom_scenariste, prix, titre, couverture ";
				mysql += "from manga join genre on manga.id_genre=genre.id_genre ";
				mysql += "join dessinateur on manga.id_dessinateur=dessinateur.id_dessinateur ";
				mysql += "join scenariste on manga.id_scenariste=scenariste.id_scenariste ";
				mysql += "where titre COLLATE UTF8_GENERAL_CI like concat('%', '" + recherche + "', '%') ";
				mysql += "order by manga.id_manga";

				mesMangas = DBInterface.Lecture(mysql, er);

				return mesMangas;
			}
			catch (MonException e)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
			}
		}
	}
}
