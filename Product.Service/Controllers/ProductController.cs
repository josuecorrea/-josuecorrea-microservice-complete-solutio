﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Application.ProductUseCase.Request;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Product.Service.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController : ControllerBase
    {
        private  readonly ILogger<ProductController> _logger;
        private  readonly IMediator _mediator;
        
        public ProductController(ILogger<ProductController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public async Task<IActionResult> GetById (int? id)
        {
            return Ok("Produto enviado!");
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest createProductRequest)
        {
            try
            {
                _logger.LogInformation($"Create new product with parameters {createProductRequest}");

               var response = await _mediator.Send(createProductRequest).ConfigureAwait(false);

                if (response.Errors.Any())
                {
                    return BadRequest(response.Errors);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred while create new product with parameters {JsonSerializer.Serialize(createProductRequest)}");

               return BadRequest();
            }
        }
    }
}
