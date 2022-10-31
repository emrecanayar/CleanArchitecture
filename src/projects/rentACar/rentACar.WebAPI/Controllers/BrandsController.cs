using Core.Application.Requests;
using Core.Application.ResponseTypes.Concrete;
using Core.CrossCuttingConcerns.Filters;
using Microsoft.AspNetCore.Mvc;
using rentACar.Application.Features.Brands.Commands.CreateBrand;
using rentACar.Application.Features.Brands.Dtos;
using rentACar.Application.Features.Brands.Models;
using rentACar.Application.Features.Brands.Queries.GetByIdBrand;
using rentACar.Application.Features.Brands.Queries.GetListBrand;
using rentACar.Application.Features.Brands.Queries.GetListBrandPaginate;
using rentACar.Domain.Entities;
using rentACar.WebAPI.Controllers.Base;

namespace rentACar.WebAPI.Controllers
{
    public class BrandsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
        {
            CreatedBrandDto result = await Mediator.Send(createBrandCommand);
            return Created("", result);
        }

        [HttpGet("GetListPaginate")]
        public async Task<IActionResult> GetListPaginate([FromQuery] PageRequest pageRequest)
        {
            GetListBrandPaginateQuery getListBrandPaginateQuery = new() { PageRequest = pageRequest };
            CustomResponseDto<BrandListModel> result = await Mediator.Send(getListBrandPaginateQuery);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            GetListBrandQuery getListBrandQuery = new();
            CustomResponseDto<List<BrandListDto>> result = await Mediator.Send(getListBrandQuery);
            return CreateActionResult(result);
        }

        [ServiceFilter(typeof(NotFoundFilter<Brand>))]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdBrandQuery getByIdBrandQuery)
        {
            CustomResponseDto<BrandGetByIdDto> result = await Mediator.Send(getByIdBrandQuery);
            return Ok(result);
        }
    }
}