using Entities.Models;
using Entities.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proje1.DTOs;

namespace Proje1.Controllers
{
    [Route("Admin/[action]")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        [HttpGet]
        public IActionResult ListCategory()
        {
            List<CategoryDto> categoryDto = new List<CategoryDto>();
            categoryDto = _categoryRepository.GetAllQuery().Select(x => new CategoryDto
            {
                CategoryID = x.CategoryId,
                Name = x.Name,
            }).ToList();
            if (TempData["ModelErrorCat"] != null)
                ModelState.AddModelError("", TempData["ModelErrorCat"].ToString());

            return View(categoryDto);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryDto categoryDto)
        {
            if (CategoryNameControl(categoryDto.Name))
            {
                ModelState.AddModelError("", "There is a category with the same name");
                return View(categoryDto);
            }
            else
            {
                _categoryRepository.Add(new Category { Name = categoryDto.Name });
                return RedirectToAction(nameof(ListCategory));
            }
            return View();
        }

        [HttpGet]
        public IActionResult RemoveCategory(string? CategoryID)
        {

            int CategoryIDValue = 0;
            if (RouteValueControl(CategoryID, out CategoryIDValue))
            {
                if (!AnyUsingCategoryControl(CategoryIDValue))
                {
                    _categoryRepository.Remove(_categoryRepository.First(x => x.CategoryId.Equals(CategoryIDValue)));

                }
                else
                    TempData["ModelErrorCat"] = "The selected category cannot be deleted, it is using";
            }
            else
                TempData["ModelErrorCat"] = "There is a error , Please try again";
            return RedirectToAction(nameof(ListCategory));
        }

        [HttpGet]
        public IActionResult UpdateCategory(string CategoryID)
        {
            int CategoryIDValue = 0;
            if (RouteValueControl(CategoryID, out CategoryIDValue) && HasCategoryControl(CategoryIDValue))
            {

                CategoryDto categoryDto = _categoryRepository.GetWhere(x => x.CategoryId == CategoryIDValue).Select(y => new CategoryDto
                {
                    CategoryID = y.CategoryId,
                    Name = y.Name,
                }).First();
                return View(categoryDto);
            }
            else
                TempData["ModelErrorCat"] = "There is a error , Please try again";
            return RedirectToAction(nameof(ListCategory));
        }

        [HttpPost]
        public IActionResult UpdateCategory(CategoryDto categoryDto)
        {
            if (HasCategoryControl(categoryDto.CategoryID.Value))
            {
                if (CategoryNameControl(categoryDto.Name))
                {
                    ModelState.AddModelError("", "There is a category with the same name");
                    return View(categoryDto);
                }
                else
                {
                    // Category category = _categoryRepository.First(x => x.CategoryId == categoryDto.CategoryID.Value);
                    _categoryRepository.Update(new Category { Name = categoryDto.Name, CategoryId = categoryDto.CategoryID.Value });
                    return RedirectToAction(nameof(ListCategory));
                }
            }
            else
            {
                ModelState.AddModelError("", "There is a error , Please try again.");
                return View(categoryDto);
            }
        }

        bool RouteValueControl(string? value, out int ID)
        {
            if (value != null && int.TryParse(value, out ID))
            {
                return true;
            }
            else
            {
                ID = 0;
                return false;
            }
        }

        bool AnyUsingCategoryControl(int CategoryID)
        {
            bool result = (_categoryRepository.Any(x => x.Activities.Any(y => y.UserActivities.Any(z => z.Activity.CategoryId.Equals(CategoryID)))) || !HasCategoryControl(CategoryID));
            return result;
        }
        bool HasCategoryControl(int CategoryID)
        {
            return _categoryRepository.Any(x => x.CategoryId == CategoryID);

        }
        bool CategoryNameControl(string Name)
        {
            return _categoryRepository.Any(x => x.Name.ToLower() == Name);
        }
    }
}
