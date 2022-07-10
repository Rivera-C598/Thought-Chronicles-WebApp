using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThoughtChroniclesWebApp.Data;
using ThoughtChroniclesWebApp.Models;
using Microsoft.AspNetCore.Identity;
using X.PagedList;
using ThoughtChroniclesWebApp.Areas.Identity.Pages.Account;

namespace ThoughtChroniclesWebApp.Controllers
{
    public class QnAController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public QnAController(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;


        // GET: QnA
        //public async Task<IActionResult> Index(int? page, string currentFilter, string searchString)
        //{

        //    if(searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = searchString;

        //    var qna = from q in _context.QnA select q;

        //    qna = qna.OrderByDescending(q => q.ID);

        //    if(!String.IsNullOrEmpty(searchString))
        //    {
        //        qna = qna.Where(s => s.TypeOfQuestion.Contains(searchString)
        //        || s.Questions.Contains(searchString)
        //        || s.Answers.Contains(searchString)
        //        || s.Author.Contains(searchString));
        //    }

        //    var pageNumber = page ?? 1;
        //    var pageSize = 5;
        //    return _context.QnA != null ?
        //                View(await qna.ToPagedListAsync(pageNumber, pageSize)) :
        //                Problem("Entity set 'ApplicationDbContext.QnA'  is null.");
        //}

        public async Task<IActionResult> Index(string currentFilter, string searchString, int? page)
        {
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var qna = from q in _context.QnA select q;

            qna = qna.OrderByDescending(q => q.ID);

            if (!String.IsNullOrEmpty(searchString))
            {
                qna = qna.Where(s => s.TypeOfQuestion.Contains(searchString)
                || s.Questions.Contains(searchString)
                || s.Answers.Contains(searchString)
                || s.Author.Contains(searchString));
            }

            var pageNumber = page ?? 1;
            var pageSize = 7; 
            return _context.QnA != null ?
                        View(await qna.ToPagedListAsync(pageNumber, pageSize)) :
                        Problem("Entity set 'ApplicationDbContext.QnA'  is null.");
        }

        // GET: QnA/ShowSearchBar
        public async Task<IActionResult> ShowSearchBar()
        {
            return View();
        }
        //Random, Memes, Games, Riddles, Jokes, PickupLines
        // POST: QnA/ShowRandomSearchResults
        public async Task<IActionResult> ShowRandomSearchResults(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 100;
            return View("Index", await _context.QnA.Where(j => j.TypeOfQuestion.Contains("Random")).ToPagedListAsync(pageNumber, pageSize));
        }
        // POST: QnA/ShowMemeSearchResults
        public async Task<IActionResult> ShowMemeSearchResults(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 100;
            return View("Index", await _context.QnA.Where(j => j.TypeOfQuestion.Contains("Meme")).ToPagedListAsync(pageNumber, pageSize));

        }
        // POST: QnA/ShowGameSearchResults
        public async Task<IActionResult> ShowGameSearchResults(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 100;
            return View("Index", await _context.QnA.Where(j => j.TypeOfQuestion.Contains("Game")).ToPagedListAsync(pageNumber, pageSize));

        }
        // POST: QnA/ShowRiddleSearchResults
        public async Task<IActionResult> ShowRiddleSearchResults(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 100;
            return View("Index", await _context.QnA.Where(j => j.TypeOfQuestion.Contains("Riddle")).ToPagedListAsync(pageNumber, pageSize));

        }
        // POST: QnA/ShowJokeSearchResults
        public async Task<IActionResult> ShowJokeSearchResults(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 100;
            return View("Index", await _context.QnA.Where(j => j.TypeOfQuestion.Contains("Joke")).ToPagedListAsync(pageNumber, pageSize));

        }
        // POST: QnA/ShowPickupLineSearchResults
        public async Task<IActionResult> ShowPickupLineSearchResults(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 100;
            return View("Index", await _context.QnA.Where(j => j.TypeOfQuestion.Contains("Pickup-Line")).ToPagedListAsync(pageNumber, pageSize));

        }
        // POST: QnA/ShowPickupLineSearchResults
        public async Task<IActionResult> ShowScienceSearchResults(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 100;
            return View("Index", await _context.QnA.Where(j => j.TypeOfQuestion.Contains("Science")).ToPagedListAsync(pageNumber, pageSize));

        }
        // POST: QnA/ShowPickupLineSearchResults
        public async Task<IActionResult> ShowMathSearchResults(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 100;
            return View("Index", await _context.QnA.Where(j => j.TypeOfQuestion.Contains("Math")).ToPagedListAsync(pageNumber, pageSize));

        }
        // POST: QnA/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchQuery, int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 100;
            return View("Index", await _context.QnA.Where(j => j.Questions.Contains(SearchQuery) || j.TypeOfQuestion.Contains(SearchQuery)).ToPagedListAsync(pageNumber, pageSize));

        }

        // GET: QnA/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.QnA == null)
            {
                return NotFound();
            }

            var qnA = await _context.QnA
                .FirstOrDefaultAsync(m => m.ID == id);
            if (qnA == null)
            {
                return NotFound();
            }

            return View(qnA);
        }


        [Authorize]
        // GET: QnA/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QnA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TypeOfQuestion,Questions,Answers,Author")] QnA qnA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qnA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            return View(qnA);
        }
        [Authorize]
        // GET: QnA/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.QnA == null)
            {
                return NotFound();
            }

            var qnA = await _context.QnA.FindAsync(id);
            if (qnA == null)
            {
                return NotFound();
            }
            return View(qnA);
        }

        // POST: QnA/Edit/5
       
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TypeOfQuestion,Questions,Answers,Author")] QnA qnA)
        {
            if (id != qnA.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qnA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QnAExists(qnA.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(qnA);
        }
        [Authorize]
        // GET: QnA/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.QnA == null)
            {
                return NotFound();
            }

            var qnA = await _context.QnA
                .FirstOrDefaultAsync(m => m.ID == id);
            if (qnA == null)
            {
                return NotFound();
            }

            return View(qnA);
        }

        // POST: QnA/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.QnA == null)
            {
                return Problem("Entity set 'ApplicationDbContext.QnA'  is null.");
            }
            var qnA = await _context.QnA.FindAsync(id);
            if (qnA != null)
            {
                _context.QnA.Remove(qnA);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QnAExists(int id)
        {
          return (_context.QnA?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
