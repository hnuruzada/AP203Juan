using AP203Juan.DAL;
using AP203Juan.Extensions;
using AP203Juan.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AP203Juan.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            if (slider == null) return NotFound();
            if (slider.ImageFile != null)
            {
                if (!slider.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Sekil duzgun formatda deyil!");
                    return View();
                }
                if (!slider.ImageFile.IsSizeOk(5))
                {
                    ModelState.AddModelError("ImageFile", "Sekil max 5 mb ola biler");
                    return View();
                }
                slider.Image = slider.ImageFile.SaveImg(_env.WebRootPath, "assets/sliderImage");

            }
            else
            {
                ModelState.AddModelError("ImageFile", "Sekil elave edin");
                return View();
            }

            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if(id==null) return BadRequest();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id,Slider slider)
        {
            if (!ModelState.IsValid) return View();

            Slider existSlider = _context.Sliders.FirstOrDefault(s=>s.Id==id);

            if(existSlider==null) return BadRequest();
            if (slider.ImageFile != null)
            {
                if (!slider.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Sekil duzgun formatda deyil");
                    return View();

                }
                if (!slider.ImageFile.IsSizeOk(5))
                {
                    ModelState.AddModelError("ImageFile", "Max 5mb ola biler");
                    return View();
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/sliderImage", existSlider.Image);
                existSlider.Image = slider.ImageFile.SaveImg(_env.WebRootPath, "assets/sliderImage");
            }

            existSlider.SubTitle = slider.SubTitle;
            existSlider.Title = slider.Title;
            existSlider.Description = slider.Description;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
