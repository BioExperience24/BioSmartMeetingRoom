using System.Text.Json;
using _2.BusinessLogic.Services.Interface;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BuildingFloorController : ControllerBase
    {
        private readonly IBuildingFloorService _service;

        public BuildingFloorController(IBuildingFloorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems([FromQuery] BuildingFloorVMListQuery query)
        {
            ReturnalModel ret = new();
            ret.Message = "Get success";

            BuildingFloorViewModel vm = new();
            if (query.Building != null)
            {
                vm.BuildingEncId = query.Building;
            }

            ret.Collection = await _service.GetAllItemAsync(vm);

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Show([FromForm] BuildingFloorVMShowFR request)
        {
            ReturnalModel ret = new();
            ret.Status = ReturnalType.Failed;
            ret.Message = "Get failed";

            BuildingFloorViewModel vm = new BuildingFloorViewModel {
                EncId = request.Floor,
                BuildingEncId = request.Building,
            };

            ret.Collection = await _service.GetItemByEntityAsync(vm);
            if (ret.Collection != null)
            {
                ret.Status = ReturnalType.Success;
                ret.Message = "Get success";
            }

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BuildingFloorVMCreateFR request)
        {
            ReturnalModel ret = new();
            ret.Message = "Success create a floor";

            BuildingFloorViewModel vm = new BuildingFloorViewModel {
                BuildingEncId = request.BuildingId,
                Name = request.Name
            };
            
            ret.Collection = await _service.Create(vm);

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] BuildingFloorVMUpdateFR request)
        {
            ReturnalModel ret = new();
            ret.Message = "Success update a floor";

            BuildingFloorViewModel vm = new BuildingFloorViewModel {
                EncId = request.Id,
                BuildingEncId = request.BuildingId,
                Name = request.Name
            };

            ret.Collection = await _service.Update(vm);

            if (ret.Collection == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed the floor not found";
            }

            ret.Collection = null;

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Remove([FromForm] BuildingFloorVMDeleteFR request)
        {
            ReturnalModel ret = new();

            BuildingFloorViewModel vm = new BuildingFloorViewModel {
                EncId = request.Id
            };

            var result = await _service.Delete(vm);

            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed the floor not found";
            } else {
                ret.Message = $"Success delete {result.Name}";
            }

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] BuildingFloorVMUploadFR request)
        {
            ReturnalModel ret = new();

            BuildingFloorViewModel vm = new BuildingFloorViewModel {
                EncId = request.Id
            };

            var (fileName, errMsg) = await _service.DoUploadAsync(request.FileImage);

            if (errMsg != null)
            {
                ret.Status = ReturnalType.Failed;
                // ret.Message = "Failed to upload file";
                ret.Message = errMsg;
                return StatusCode(ret.StatusCode, ret);
            }

            if (fileName != null)
            {
                vm.Image = fileName;
            }

            var result = await _service.UploadAsync(vm);

            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed the floor not found";
            } else {
                ret.Message = $"Success upload {result.Name}";
            }

            return StatusCode(ret.StatusCode, ret);
        }
    }
}
