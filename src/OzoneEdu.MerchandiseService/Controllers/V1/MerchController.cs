using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services.Interfaces;
using OzonEdu.MerchApi.HttpModels;

namespace OzoneEdu.MerchandiseService.Controllers.V1
{
    [ApiController]
    [Route("v1/api/merch")]
    [Produces("application/json")]
    public sealed class MerchController : ControllerBase
    {
        private readonly IMerchService _service;

        public MerchController(IMerchService service)
        {
            _service = service;
        }

        [HttpGet("{merchTypeId:int}")]
        public async Task<ActionResult<MerchItemResponse>> GetMerch(int merchTypeId,
            [FromQuery]MerchItemRequest request, 
            CancellationToken token)
        {
            var merchId = await _service.CreateMerch(request.MerchCustomerId, request.Sku,
                (long)merchTypeId,
                (long)IssueType.Manual, token);
            bool isDone = false;
            isDone = await _service.CheckMerch(request.MerchCustomerId, merchTypeId, token);
            isDone = await _service.CheckAvailableStokMerch(request.Sku, token);
            isDone = await _service.ReserveOnStokMerch(request.Sku, token);

            var merchReq = await _service.SetConfirmStatusMerch(merchId, isDone, token);
            if (merchReq)
                return Ok(new MerchItemResponse(){ItemId = merchTypeId});
            else
                return BadRequest();
        }

        [HttpGet("info/{id:long}")]
        public async Task<ActionResult<List<MerchItemResponse>>> GetMerchByUserId(long id, CancellationToken token)
        {
            var merchItem = await _service.GetMerchesByUserId(id, token);
            if (merchItem is null)
                return NotFound();

            return merchItem.Select(x => new MerchItemResponse()
            {
                ItemId = x.ItemId,
            }).ToList();
            ;
        }
    }
}