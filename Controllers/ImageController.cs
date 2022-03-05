using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using images_api.Dtos;
using images_api.Interfaces;
using images_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace images_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _repository;

        public ImageController(IImageRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            var images = await _repository.GetImages();
            return Ok(images);
        }

        [HttpGet]
        [Route("{id::int}")]
        public async Task<IActionResult> GetImageById([FromRoute] int id)
        {
            var img = await _repository.GetImageById(id);
            return Ok(img);
        }

        [HttpPost]
        public async Task<IActionResult> CreateImage([FromForm] ImageDto img)
        {
            await _repository.CreateImage(img);
            return Ok();
        }

        [HttpDelete]
        [Route("{id::int}")]
        public async Task<IActionResult> DeleteImage([FromRoute] int id)
        {
            await _repository.DeleteImage(id);
            return Ok();
        }

        [HttpPut]
        [Route("{id::int}")]
        public async Task<IActionResult> UpdateImage([FromRoute] int id, [FromForm] ImageDto img)
        {
            await _repository.UpdateImage(id, img);
            return Ok();
        }
    }
}