using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;//Sonradan

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        readonly private ILogger<ProductsController> _logger;//Sonradan

        public ProductsController(
            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;



        }
        [HttpGet]
        public async Task<IActionResult> Get()// task yerine void dersek beklemicek ve singelton harici diğerlerinde dispose edilecek ve proje patlıcak
        {
            return Ok(_productReadRepository.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id,false));
        }


        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)//Product a yani entity e doğrudan bağımlı olmucam şimdilik ViewModel kullandım daha Sonra CQRS ile request nesnelerini kullanıcam
        {
            if (ModelState.IsValid)
            {

            }
            await _productWriteRepository.AddAsync(new Product
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);

            #region ChatGpt Önerisi
            //if (model == null)
            //{
            //    return BadRequest("Model is null");
            //}

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //try
            //{
            //    var product = new Product
            //    {
            //        Name = model.Name,
            //        Price = model.Price,
            //        Stock = model.Stock,
            //    };

            //    await _productWriteRepository.AddAsync(product);
            //    await _productWriteRepository.SaveAsync();

            //    return CreatedAtAction(nameof(Get), new { id = product.Id }, product); // Get metodu varsa bu şekilde dönebilirsiniz
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error occurred while adding a product");
            //    return StatusCode((int)HttpStatusCode.InternalServerError, $"Internal server error: {ex.Message}");
            //}
            #endregion

        }




        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product =await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock=model.Stock;
            product.Name=model.Name;
            product.Price=model.Price;
            await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }



    }
}
