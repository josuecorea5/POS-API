using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.UseCases.Products.Commands;
using POS.Application.UseCases.Products.Queries;

namespace POS.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediador;

		public ProductsController(IMediator mediador)
		{
			_mediador = mediador;
		}

		[HttpGet]
		[Authorize(Roles = "BASIC")]
		public async Task<IActionResult> GetAll()
		{
			var response = await _mediador.Send(new GetAllProductsQuery());

			if (response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "BASIC")]
		public async Task<IActionResult> GetById(int id)
		{
			var response = await _mediador.Send(new GetProductQuery { Id = id });

			if (response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Create(CreateProductCommand command)
		{
			var response = await _mediador.Send(command);

			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Update(int id, UpdateProductCommand command)
		{
			var product = await _mediador.Send(new GetProductQuery { Id = id });

			if (!product.Success)
				return NotFound(product.Message);

			var response = await _mediador.Send(command);

			if(response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Delete(int id)
		{
			var product = await _mediador.Send(new GetProductQuery { Id = id });

			if (!product.Success)
				return NotFound(product.Message);

			var response = await _mediador.Send(new DeleteProductCommand { Id = id});

			if (response.Success)
				return Ok(response);

			return BadRequest(response.Message);
		}
	}
}
