using AutoMapper;
using InventarioApi.Data;
using InventarioApi.Models;
using InventarioApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InventarioContext _context;
        private ResponseDto _response;
        private IMapper _mapper;
        public ProductsController(InventarioContext context,
                                   IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _context.Products
                    .Include(p => p.Category)
                    .ToListAsync();

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                _response.Result = productDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Producto no encontrado";
                    return NotFound(_response);
                }

                var productDto = _mapper.Map<ProductDto>(product);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }

            return Ok(_response);
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductCreateDto productDto)
        {
            try
            {
                // validamos la existencia de categoría
                var categoryExists = await _context.Categories.AnyAsync(c => c.Id == productDto.CategoryId);
                if (!categoryExists)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Categoría no válida.";
                    return NotFound(_response);
                }

                var product = _mapper.Map<Product>(productDto);

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                var createdProductDto = _mapper.Map<ProductDto>(product);
                _response.Result = createdProductDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }

            return CreatedAtAction(nameof(GetProduct), new { id = productDto.Id }, _response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductUpdateDto productDto)
        {

            var productFromDb = await _context.Products.FindAsync(id);
            if (productFromDb == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Producto no encontrado.";
                return NotFound(_response);
            }

            _mapper.Map(productDto, productFromDb);

            try
            {
                await _context.SaveChangesAsync();
                _response.Result = _mapper.Map<ProductDto>(productFromDb);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Producto no encontrado en concurrencia.";
                    return NotFound(_response);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }

            return Ok(_response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Producto no encontrado.";
                    return NotFound(_response);
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                _response.IsSuccess = true;
                _response.Message = "Producto eliminado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = $"Error al eliminar el producto: {ex.Message}";
                return StatusCode(500, _response);
            }
        }

        private bool ProductExists(int id) => _context.Products.Any(e => e.Id == id);

    }
}
