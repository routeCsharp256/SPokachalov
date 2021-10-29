using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id:long}")]
        public async Task<ActionResult<MerchItemResponse>> GetMerch(long id, CancellationToken token)
        {
            var merchItem = await _service.GetMerch(id, token);
            if (merchItem is null)
                return new MerchItemResponse();

            return new MerchItemResponse()
            {
                ItemId = merchItem.ItemId,
                ItemName = merchItem.ItemName
            };
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
                ItemName = x.ItemName
            }).ToList();
            ;
        }
    }
}