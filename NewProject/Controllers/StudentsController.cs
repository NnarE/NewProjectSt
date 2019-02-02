using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewProject.Models;

namespace NewProject.Controllers
{
    public class StudentsController : Controller
    {
        private readonly NewProjectContext _context;
        public StudentsController(NewProjectContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            string searchGrdId = Request.Query.FirstOrDefault(x => x.Key == "[0].GradeRefId").Value;

            IQueryable<Grade> gradeList = from o in _context.Grade
                                          where o.GradeId == Int32.Parse(searchGrdId)
                                          select o;

            IQueryable<Grade> allGrades = from grd in _context.Grade select grd;
            IQueryable<Student> studentList =
                 from s in _context.Student
                 from g in gradeList
                 where s.GradeRefId == g.GradeId
                 select new Student
                 {
                     Grade = g,
                     Id = s.Id,
                     Name = s.Name,
                     LastName = s.LastName
                 };
            //join
            IQueryable<Student> studs =
                  from s in _context.Student
                  from g in _context.Grade
                  from a in _context.Ambion
                  where s.AmbionRefId == a.AmbionId
                  where s.GradeRefId == g.GradeId
                  select new Student
                  {
                      Grade = g,
                      Ambion = a,
                      Id = s.Id,
                      Name = s.Name,
                      LastName = s.LastName
                  };
            ViewData["selectlist"] = new SelectList(await allGrades.Distinct().ToListAsync(), "GradeId", "GradeName");
            ViewData["selectlist1"] = new SelectList(await allGrades.Distinct().ToListAsync(), "AmbionId", "AmbionName");
            IQueryable<Student> studentnewList;
            if (searchGrdId == null)
            {
                studentnewList = studs;
            }
            else
            {
                studentnewList = studentList;
            }
            return View(await studentnewList.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //select
            IQueryable<Grade> GradesQuery = from m in _context.Grade
                                            where m.Status==true
                                            select m;
                                            
            IQueryable<Ambion> AmbionsQuery = from n in _context.Ambion
                                             orderby n.AmbionId                              
                                              select n;

            

            ViewData["selectlist"] = new SelectList(await GradesQuery.Distinct().ToListAsync(), "GradeId", "GradeName");
            ViewData["selectlist1"] = new SelectList(await AmbionsQuery.Distinct().ToListAsync(), "AmbionId", "AmbionName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student students)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(students);
                    int id = await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                throw new InvalidOperationException();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Student
                .Include(s => s.Grade)
                .Include(f => f.Ambion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IQueryable<object> GradesQuery = from m in _context.Grade
                                             orderby m.GradeId
                                             select m;
            IQueryable<object> AmbionsQuery = from n in _context.Ambion
                                              orderby n.AmbionId
                                              select n;
            ViewData["selectlist2"] = new SelectList(await GradesQuery.Distinct().ToListAsync(), "GradeId", "GradeName");
            ViewData["selectlist3"] = new SelectList(await AmbionsQuery.Distinct().ToListAsync(), "AmbionId", "AmbionName");
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(student).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewData["GradeRefId"] = new SelectList(_context.Set<Grade>(), "GradeId", "GradeId", student.GradeRefId);
            ViewData["AmbionRefId"] = new SelectList(_context.Set<Ambion>(), "AmbionId", "AmbionId", student.AmbionRefId);

            return View(student);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Student
                .Include(s => s.Grade)
                .Include(n => n.Ambion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);


        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var student = await _context.Student.FindAsync(id);
                _context.Student.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }
       

    }
}