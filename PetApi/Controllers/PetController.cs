using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PetApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class PetController : ControllerBase
    {
        private IList<Pet> pets = new List<Pet>();
        
        [HttpPost("addPet")]
        public Pet AddPet(Pet pet)
        {
            pets.Add(pet);
            return pet;
        }
    }
}
