using CrudTaskManagementSystem.DTO;
using CrudTaskManagementSystem.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.ComponentModel;
using Container = Microsoft.Azure.Cosmos.Container;
namespace CrudTaskManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        public readonly Container container;

        public DeveloperController()
        {
            container = GetContainer();
        }

        [HttpPost]

        public async Task<IActionResult> AddDeveloper(DeveloperModelDto developerModelDto)
        {
            DevelopersEntity developer = new DevelopersEntity();

            try
            {
                developer.DeveloperId= developerModelDto.DeveloperId; 
                developer.DeveloperName= developerModelDto.DeveloperName;
                developer.DeveloperRoll = developerModelDto.DeveloperRoll;

                developer.Id = Guid.NewGuid().ToString();
                developer.UID = developer.Id;
                developer.DocumentType = "Developers";

                developer.CreatedBy = "Mayue UID";
                developer.CreatedByName = "Mayur";
                developer.CreatedOn = DateTime.Now;

                developer.UpdatedBy = "Mayur UID";
                developer.UpdatedByName = "Mayur";
                developer.UpdatedOn = DateTime.Now;

                developer.Version = 1;
                developer.Active= true;
                developer.Archieved = false;

                DevelopersEntity respons = await container.CreateItemAsync(developer);
                
                developerModelDto.UID= respons.UID;
                developerModelDto.DeveloperId= respons.DeveloperId; 
                developerModelDto.DeveloperName= respons.DeveloperName; 
                developerModelDto.DeveloperRoll= respons.DeveloperRoll;

                return Ok(developerModelDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Data add faild"+ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult>GetAllDevelopersData()
        {
            try
            {
                var developers=container.GetItemLinqQueryable<DevelopersEntity>(true).Where(q=> q.DocumentType=="Developers" && q.Active==true && q.Archieved==false).AsEnumerable();
                List<DeveloperModelDto> developerModelList = new List<DeveloperModelDto>();
                foreach (var developer in developers)
                {
                    DeveloperModelDto dev = new DeveloperModelDto();

                    dev.UID= developer.UID;
                    dev.DeveloperId= developer.DeveloperId;
                    dev.DeveloperName= developer.DeveloperName;
                    dev.DeveloperRoll= developer.DeveloperRoll;

                    developerModelList.Add(dev);
                }
                return Ok(developerModelList) ;
            }
            catch (Exception ex)
            {
                return BadRequest("Data get faild"+ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetDeveloperById(string uId)
        {
            try
            {
                var developers = container.GetItemLinqQueryable<DevelopersEntity>(true).Where(q =>q.UID==uId && q.DocumentType == "Developers" && q.Active == true && q.Archieved == false).AsEnumerable().FirstOrDefault();

                DeveloperModelDto model = new DeveloperModelDto();

                model.UID= developers.UID;
                model.DeveloperId= developers.DeveloperId;
                model.DeveloperName= developers.DeveloperName;
                model.DeveloperRoll= developers.DeveloperRoll;

                return Ok(model);
            }
            catch (Exception ex) 
            {
                return BadRequest("Data get faid By Id"+ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult>UpdataDeveloperData(DeveloperModelDto developerModelDto)
        {
            try
            {
                var existingDeveloper= container.GetItemLinqQueryable<DevelopersEntity>(true).Where(q => q.UID == developerModelDto.UID && q.DocumentType == "Developers" && q.Active == true && q.Archieved == false).AsEnumerable().FirstOrDefault();
                existingDeveloper.Archieved = true;

                await container.ReplaceItemAsync(existingDeveloper, existingDeveloper.Id);

                existingDeveloper.Id=Guid.NewGuid().ToString();
                existingDeveloper.UpdatedBy = "Mayur UID";
                existingDeveloper.UpdatedByName = "Mayur";
                existingDeveloper.UpdatedOn = DateTime.Now;

                existingDeveloper.Version = existingDeveloper.Version + 1;
                existingDeveloper.Active = true;
                existingDeveloper.Archieved = false;

                existingDeveloper.UID= developerModelDto.UID;
                existingDeveloper.DeveloperId = developerModelDto.DeveloperId;
                existingDeveloper.DeveloperName = developerModelDto.DeveloperName;
                existingDeveloper.DeveloperRoll= developerModelDto.DeveloperRoll;

                existingDeveloper=await container.CreateItemAsync(existingDeveloper);

                DeveloperModelDto dmdto= new DeveloperModelDto();

                dmdto.UID= developerModelDto.UID;
                dmdto.DeveloperId= developerModelDto.DeveloperId;
                dmdto.DeveloperName= developerModelDto.DeveloperName;
                dmdto.DeveloperRoll= developerModelDto.DeveloperRoll;

                return Ok(dmdto);

            }
            catch (Exception ex)
            {
                return BadRequest("Data Updating faild"+ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult>DelectDeveloperData(string uId)
        {
            try
            {
                var developers = container.GetItemLinqQueryable<DevelopersEntity>(true).Where(q => q.UID == uId && q.DocumentType == "Developers" && q.Active == true && q.Archieved == false).AsEnumerable().FirstOrDefault();
                developers.Active = false;

                DeveloperModelDto model = new DeveloperModelDto();

                await container.ReplaceItemAsync(developers, uId);

                return Ok(true);
            }
            catch (Exception ex) 
            {
                return BadRequest("Data is Not delete"+ex);
            }
        }
        private Container GetContainer()
        {
            string URI = Environment.GetEnvironmentVariable("Cosmos-Uri");
            string PrimaryKey = Environment.GetEnvironmentVariable("Primary-Key");
            string DataBaseName = Environment.GetEnvironmentVariable("DataBase");
            string ContainerName = Environment.GetEnvironmentVariable("Container");

            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Container container = cosmosClient.GetContainer(DataBaseName, ContainerName);
            return container;
        }
    }
}
