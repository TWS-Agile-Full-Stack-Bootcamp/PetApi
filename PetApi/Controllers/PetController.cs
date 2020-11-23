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
        private static IList<Pet> pets = new List<Pet>();
        
        [HttpPost("addPet")]
        public Pet AddPet(Pet pet)
        {
            pets.Add(pet);
            return pet;
        }

        [HttpGet("getAllPets")]
        public IEnumerable<Pet> GetAllPets()
        {
            return pets;
        }
        
        [HttpGet("findPetByName")]
        public Pet FindPetByName(string name)
        {
            return pets.First(pet => pet.Name.Equals(name));
        }

        [HttpDelete("removePetByName")]
        public void RemovePetByName(string name)
        {
            var pet = pets.First(pet => pet.Name.Equals(name));
            pets.RemoveAt(pets.IndexOf(pet));
        }

        [HttpPut("modifyPetPrice")]
        public Pet ModifyPetPrice(Pet pet)
        {
            var currentPet = pets.First(pet => pet.Name.Equals(pet.Name));
            currentPet.Price = pet.Price;
            return currentPet;
        }

        [HttpGet("findPetsByType")]
        public IEnumerable<Pet> FindPetsByType(string type)
        {
            return pets.Where(pet => pet.Type.Equals(type));
        }
        
        [HttpGet("resetPets")]
        public void ResetPets()
        {
            pets.Clear();
        }
    }
}
