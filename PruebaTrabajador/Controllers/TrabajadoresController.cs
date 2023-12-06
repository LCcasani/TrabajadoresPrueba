using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PruebaTrabajador.Context;
using PruebaTrabajador.Models;

namespace PruebaTrabajador.Controllers
{

    public class TrabajadoresController: Controller
    {

        private readonly TrabajadoresPruebaContext _context;

        public TrabajadoresController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(String sexo="")
        {
            ViewBag.sexo = sexo;

            if (!string.IsNullOrEmpty(sexo))
            {

                var listFilter = await _context.Trabajadores.Where(x => x.Sexo.Trim().Equals(sexo)).ToListAsync();
                listFilter = listFilter ?? new List<Trabajadore>();
                return View(listFilter);
            }
            var list = await _context.Trabajadores.ToListAsync();
            list = list ?? new List<Trabajadore>();
            return View(list);

        }


        public async Task<IActionResult> Detail(int? id)
        {

            if (_context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            return View(trabajadore);

        }


        //GET: Trabajadores/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Trabajadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Trabajadore trabajadore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabajadore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trabajadore);
        }



        //GET: Trabajadores/Edit/5
        public async Task<IActionResult> AddorEdit(int id)
        {
            if (_context.Trabajadores == null)
            {
                return NotFound();
            }

            ViewBag.Departamento = _context.Departamentos.Select(x => new SelectListItem { Text = x.NombreDepartamento, Value = x.Id.ToString() }).ToList();
            
         
            var trabajadore = await _context.Trabajadores.FindAsync(id);

            if (id == -1)
            {
                Trabajadore accessP = new Trabajadore();

              
                accessP.Id = id;
              

                return View(accessP);

            }
            else
            {
                if (trabajadore == null)
                {
                    return NotFound();
                }
                return View(trabajadore);
            }
        }

        //POST: Trabajadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit(int id, [FromForm] Trabajadore trabajadore)
        {
            if (ModelState.IsValid)
            {


                if (id == -1)
                {
                    trabajadore.Id = 0;
                    await _context.Trabajadores.AddAsync(trabajadore);

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _context.Update(trabajadore);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }


            }
            return View(trabajadore);
        }

        // GET: Trabajadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {


            var trabajadores = await _context.Trabajadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadores == null)
            {
                return NotFound();
            }

            return View(trabajadores);
        }

        // POST: Trabajadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var trabajadores = await _context.Trabajadores.FindAsync(id);
            if (trabajadores != null)
            {
                _context.Trabajadores.Remove(trabajadores);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabajadoresExists(int id)
        {
            return (_context.Trabajadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        public async Task<IActionResult> GetProvincias(int id)
        {
            var provincias = await _context.Provincia .Where(x => x.IdDepartamento == id).Select(x => new SelectListItem { Text = x.NombreProvincia, Value = x.Id.ToString() }).ToListAsync();
            return Ok(new { items = provincias });
        }

        public async Task<IActionResult> GetDistritos(int id)
        {
            var distritos = await _context.Distritos.Where(x=>x.IdProvincia == id).Select(x => new SelectListItem { Text = x.NombreDistrito, Value = x.Id.ToString() }).ToListAsync();
            return Ok(new { items = distritos });
        }


    }

}