using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using PetApi;
using Xunit;

namespace PetApiTest
{
    public class PetApiTest
    {
        [Fact]
        public async Task Should_Success_When_Add_New_Pet()
        {
            // given
            TestServer server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            HttpClient client = server.CreateClient();
            await client.GetAsync("api/resetPets");

            Pet pet = new Pet(name: "Baymax", type: "dog", color: "white", price: 1000);
            StringContent stringContent =
                new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, "application/json");

            // when
            var response = await client.PostAsync("api/addPet", stringContent);

            // then
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var actualPet = JsonConvert.DeserializeObject<Pet>(responseBody);
            Assert.Equal(pet, actualPet);
        }

        [Fact]
        public async Task Should_Return_All_Pets_When_Get_All_Pets()
        {
            // given
            TestServer server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            HttpClient client = server.CreateClient();
            await client.GetAsync("api/resetPets");

            Pet baymaxDog = new Pet(name: "Baymax", type: "dog", color: "white", price: 1000);
            await client.PostAsync("api/addPet",
                new StringContent(JsonConvert.SerializeObject(baymaxDog), Encoding.UTF8, "application/json"));
            Pet oldblackCat = new Pet(name: "Old Black", type: "cat", color: "black", price: 500);
            await client.PostAsync("api/addPet",
                new StringContent(JsonConvert.SerializeObject(oldblackCat), Encoding.UTF8, "application/json"));

            // when
            var response = await client.GetAsync("api/getAllPets");

            // then
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var actualPets = JsonConvert.DeserializeObject<List<Pet>>(responseBody);
            Assert.Equal(new List<Pet>() { baymaxDog, oldblackCat }, actualPets);
        }

        [Fact]
        public async Task Should_Return_Pet_Info_When_Find_Pet_By_Name_Successfully()
        {
            // given
            TestServer server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            HttpClient client = server.CreateClient();
            await client.GetAsync("api/resetPets");

            Pet baymaxDog = new Pet(name: "Baymax", type: "dog", color: "white", price: 1000);
            await client.PostAsync("api/addPet",
                new StringContent(JsonConvert.SerializeObject(baymaxDog), Encoding.UTF8, "application/json"));
            Pet oldblackCat = new Pet(name: "Old Black", type: "cat", color: "black", price: 500);
            await client.PostAsync("api/addPet",
                new StringContent(JsonConvert.SerializeObject(oldblackCat), Encoding.UTF8, "application/json"));

            // when
            var response = await client.GetAsync("api/findPetByName?name=Baymax");

            // then
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var actualPet = JsonConvert.DeserializeObject<Pet>(responseBody);
            Assert.Equal(baymaxDog, actualPet);
        }

        // removePetByName
        [Fact]
        public async Task Should_Success_When_Remove_Pet()
        {
            // given
            TestServer server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            HttpClient client = server.CreateClient();
            await client.GetAsync("api/resetPets");

            Pet baymaxDog = new Pet(name: "Baymax", type: "dog", color: "white", price: 1000);
            await client.PostAsync("api/addPet",
                new StringContent(JsonConvert.SerializeObject(baymaxDog), Encoding.UTF8, "application/json"));

            // when
            var removeResponse = await client.DeleteAsync("api/removePetByName?name=Baymax");
            var getAllResponse = await client.GetAsync("api/getAllPets");

            // then
            removeResponse.EnsureSuccessStatusCode();
            var responseBody = await getAllResponse.Content.ReadAsStringAsync();
            var actualPets = JsonConvert.DeserializeObject<List<Pet>>(responseBody);
            Assert.Equal(0, actualPets.Count);
        }
    }
}