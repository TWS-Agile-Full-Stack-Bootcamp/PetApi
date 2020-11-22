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

            Pet pet = new Pet(name: "Baymax", type: "dog", color: "white", price: 1000);
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, "application/json");

            // when
            var response = await client.PostAsync("api/addPet", stringContent);

            // then
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var actualPet = JsonConvert.DeserializeObject<Pet>(responseBody);
            Assert.Equal(pet, actualPet);
        }
    }
}