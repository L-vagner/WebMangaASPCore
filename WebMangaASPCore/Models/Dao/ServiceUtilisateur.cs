using System.Data;
using WebMangaASPCore.Models.MesExceptions;
using WebMangaASPCore.Models.Metier;
using WebMangaASPCore.Models.Persistance;

namespace WebMangaASPCore.Models.Dao
{
	public class ServiceUtilisateur
	{
		public Utilisateur getUtilisateur(String nom)
		{
			DataTable dt;
			Utilisateur unUti = null;
			String mysql = "SELECT numUtil, nomUtil, MotPasse, Salt, role FROM utilisateur " +
				"where nomUtil = '" + nom + "'";
			Serreurs er = new Serreurs("Erreur sur recherche d'un utilisateur.", "Service.getUtilisateur");
			try
			{
				dt = DBInterface.Lecture(mysql, er);
				if (dt.IsInitialized && dt.Rows.Count > 0)
				{
					unUti = new Utilisateur();
					DataRow dataRow = dt.Rows[0];
					unUti.NumUtil = Int16.Parse(dataRow[0].ToString());
					unUti.NomUtil = dataRow[1].ToString();
					unUti.MotPasse = dataRow[2].ToString();
					unUti.Salt = dataRow[3].ToString();
					unUti.Role = dataRow[4].ToString();
				}
				return unUti;
			}
			catch (MonException e)
			{
				throw e;
			}
			catch (Exception exc)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), exc.Message);
			}
		}

		public String getRole(String nom)
		{
			DataTable dt;
			Utilisateur unUti = null;
			String mysql = "SELECT role FROM utilisateur " +
				"where nomUtil = '" + nom + "'";
			Serreurs er = new Serreurs("Erreur sur recherche d'un utilisateur.", "Service.getUtilisateur");
			try
			{
				String Role="";
				dt = DBInterface.Lecture(mysql, er);
				if (dt.IsInitialized && dt.Rows.Count > 0)
				{
					DataRow dataRow = dt.Rows[0];
					Role = dataRow[0].ToString();
				}
				return Role;
			}
			catch (MonException e)
			{
				throw e;
			}
			catch (Exception exc)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), exc.Message);
			}
		}
	}
}
