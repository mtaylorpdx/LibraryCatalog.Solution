using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace LibraryCatalog.Controllers
{
  [Authorize]
  public class TitlesController : Controller
  {
    private readonly LibraryCatalogContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TitlesController(UserManager<ApplicationUser> userManager, LibraryCatalogContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userTitle = _db.Titles.Where(entry => entry.User.Id == currentUser.Id);
      return View(userTitle);
    }

    public ActionResult Create()
    {
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Title title, int AuthorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      title.User = currentUser;
      _db.Titles.Add(title);
      if (AuthorId != 0)
      {
        _db.Book.Add(new Book() { AuthorId = AuthorId, TitleId = title.TitleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTitle = _db.Titles
          .Include(title => title.Authors)
          .ThenInclude(join => join.Author)
          .FirstOrDefault(title => title.TitleId == id);
      return View(thisTitle);
    }

    public ActionResult Edit(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(titles => titles.TitleId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View(thisTitle);
    }

    [HttpPost]
    public ActionResult Edit(Title title, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.Book.Add(new Book() { AuthorId = AuthorId, TitleId = title.TitleId });
      }
      _db.Entry(title).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddAuthor(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(titles => titles.TitleId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View(thisTitle);
    }

    [HttpPost]
    public ActionResult AddAuthor(Title title, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.Book.Add(new Book() { AuthorId = AuthorId, TitleId = title.TitleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(titles => titles.TitleId == id);
      return View(thisTitle);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTitle = _db.Titles.FirstOrDefault(titles => titles.TitleId == id);
      _db.Titles.Remove(thisTitle);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteAuthor(int joinId)
    {
      var joinEntry = _db.Book.FirstOrDefault(entry => entry.BookId == joinId);
      _db.Book.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Search(string search)
    {
      List<Title> model = _db.Titles.Where(title => title.BookName.Contains(search)).ToList();
      return View(model);
    }
  }
}


