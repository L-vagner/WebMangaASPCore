using Microsoft.AspNetCore.Mvc;
using WebMangaASPCore.Models.Dao;
using WebMangaASPCore.Models.MesExceptions;
using WebMangaASPCore.Models.Metier;

namespace WebMangaASPCore.Controllers
{
    public class MangaController : Controller
    {
        public IActionResult Index()
        {
            System.Data.DataTable mesMangas = null;
            if (!LoggedIn())
                return RedirectToAction("Index", "Home");
            try
            {
                mesMangas = ServiceManga.GetTousLesMangas();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des mangas : " + e.Message);
            }
			ViewBag.titre = "Liste des Mangas";
			return View(mesMangas);
        }

        [HttpGet]
        public IActionResult Modifier(string id)
        {
            if (!LoggedIn())
                return RedirectToAction("Index", "Home");
            Manga unManga = null;
            System.Data.DataTable mesDessinateurs = null;
            System.Data.DataTable mesScenaristes = null;
            System.Data.DataTable mesGenres = null;

            try
            {
                unManga = ServiceManga.GetunManga(id);
                mesDessinateurs = ServiceManga.GetTousLesDessinateurs();
                mesScenaristes = ServiceManga.GetTousLesScenaristes();
                mesGenres = ServiceManga.GetTousLesGenres();
                ViewBag.Dessinateurs = mesDessinateurs;
                ViewBag.Scenaristes = mesScenaristes;
                ViewBag.Genres = mesGenres;
                return View(unManga);
            }
            catch (MonException e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Modifier(Manga unM)
        {
            if (!LoggedIn())
                return RedirectToAction("Index", "Home");
            try
            {
                ServiceManga.UpdateManga(unM);
                return View();
            }
            catch (MonException e)
            {
                return NotFound();
            }
        }


        public IActionResult Rechercher()
        {
            System.Data.DataTable mesMangas = null;
            if (!LoggedIn())
                return RedirectToAction("Index", "Home");
            try
            {
                mesMangas = ServiceManga.GetTitres();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des titres:" + e.Message);
            }
            return View(mesMangas);

        }

        [HttpPost]
        public IActionResult Rechercher(string titre, string titre_libre)
        {
            System.Data.DataTable mesMangas = null;
            try
            {
                if (!String.IsNullOrEmpty(titre))
                {
                    mesMangas = ServiceManga.GetMangas(titre);
                }
                else
                {
                    mesMangas = ServiceManga.GetMangas(titre_libre);
                }

            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des mangas : " + e.Message);
            }
			ViewBag.titre = "Résultats de la recherche de mangas";
			return View("Index", mesMangas);
        }

        public IActionResult Ajouter()
        {

			if (!LoggedIn())
				return RedirectToAction("Index", "Home");
			System.Data.DataTable mesDessinateurs = null;
			System.Data.DataTable mesScenaristes = null;
			System.Data.DataTable mesGenres = null;
			try
            {
				mesDessinateurs = ServiceManga.GetTousLesDessinateurs();
				mesScenaristes = ServiceManga.GetTousLesScenaristes();
				mesGenres = ServiceManga.GetTousLesGenres();
				ViewBag.Dessinateurs = mesDessinateurs;
				ViewBag.Scenaristes = mesScenaristes;
				ViewBag.Genres = mesGenres;
			} catch(MonException e)
            {
				ModelState.AddModelError("Erreur", "Erreur lors de la récupération des mangas : " + e.Message);
			}

            return View();
        }

        [HttpPost]
        public IActionResult Ajouter(Manga unM)
        {
			if (!LoggedIn())
				return RedirectToAction("Index", "Home");
			try
			{
				ServiceManga.AddManga(unM);
				return RedirectToAction("Index", "Home");
			}
			catch (MonException e)
			{
				return NotFound();
			}
		}


		public bool LoggedIn()
        {
            return (HttpContext.Session.GetString("login") != null && HttpContext.Session.GetString("role") == "Admin");
        }
    }

}
