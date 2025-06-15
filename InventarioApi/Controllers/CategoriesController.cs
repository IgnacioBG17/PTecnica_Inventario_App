using AutoMapper;
using InventarioApi.Data;
using InventarioApi.Models;
using InventarioApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly InventarioContext _context;
        private ResponseDto _response;
        private IMapper _mapper;

        public CategoriesController(InventarioContext context,
                                    IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                IEnumerable<Category> objList = await _context.Categories.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<CategoryDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory(CategoryDto categoriaDto)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoriaDto);
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                _response.Result = _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory(CategoryDto categoriaDto)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoriaDto);
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

                _response.Result = _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpDelete("{id}")]
        public async Task<ResponseDto> DeleteCategory(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category is null)
                {
                    return new ResponseDto() { Message = "Categoria no encontrada" };
                }
                
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
    }

}
