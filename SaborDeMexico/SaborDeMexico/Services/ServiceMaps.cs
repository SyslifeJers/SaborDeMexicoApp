using Newtonsoft.Json;
using SaborDeMexico.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SaborDeMexico.Services
{
    public class ServiceMaps
    {
        private string KeyApiweb = "AIzaSyBXoNJzSBNhbORrCuQKxo1LexEtPuRv8zs";
        private const string ApiBaseAddress = "https://maps.googleapis.com/maps/";
        private HttpClient CrearCliente()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
        public async Task<List<PredicionDeAutoCompletar>> GetListaDeDireccionesPosibles(string text)
        {
            GooglePlaceAutoCompleteResult results = null;
            List<PredicionDeAutoCompletar> completePredictions = new List<PredicionDeAutoCompletar>();
            using (var httpClient = CrearCliente())
            {
                var response = await httpClient.GetAsync($"api/place/autocomplete/json?input={Uri.EscapeUriString(text)}&key={KeyApiweb}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                    {
                        results = await Task.Run(() =>
                           JsonConvert.DeserializeObject<GooglePlaceAutoCompleteResult>(json)
                        ).ConfigureAwait(false);
                        foreach (var item in results.AutoCompletePlaces)
                        {
                            completePredictions.Add(item);
                        }
                    }
                }
            }
            return completePredictions;
        }
        public async Task<GooglePlace> DatosLocationId(string placeId)
        {
            GooglePlace result = null;
            using (var httpClient = CrearCliente())
            {
                var response = await httpClient.GetAsync($"api/place/details/json?placeid={Uri.EscapeUriString(placeId)}&key={KeyApiweb}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                    {
                        result = new GooglePlace(Newtonsoft.Json.Linq.JObject.Parse(json));
                    }
                }
            }

            return result;
        }

        public async Task<ModelGMaps> GetDirections(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude)
        {
            ModelGMaps modelGMaps = new ModelGMaps();
            using (var httpClient = CrearCliente())
            {
                var jsons = JsonConvert.SerializeObject("");
                var content = new StringContent(jsons, Encoding.UTF8, "application/json");
                var url = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + originLatitude.Replace(",", ".") + "," + originLongitude.Replace(",", ".") + "&destinations=" + destinationLatitude.Replace(",", ".") + "," + destinationLongitude.Replace(",", ".") + "&side_of_road=driving&language=es&key=" + KeyApiweb;
                var response = await httpClient.PostAsync(url, content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        modelGMaps = await Task.Run(() =>
                           JsonConvert.DeserializeObject<ModelGMaps>(json)
                        ).ConfigureAwait(false);

                    }
                }
            }

            return modelGMaps;
        }
        public async Task<ModelGMaps> GetPrueba()
        {
            ModelGMaps modelGMaps = new ModelGMaps();
            using (var httpClient = CrearCliente())
            {
                try
                {
                    var jsons = JsonConvert.SerializeObject("");
                    var content = new StringContent(jsons, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync($"https://maps.googleapis.com/maps/api/distancematrix/json?origins=20.62250300,-101.67033560&destinations=20.6264456,-101.6706351&side_of_road=driving&language=es&key=AIzaSyBXoNJzSBNhbORrCuQKxo1LexEtPuRv8zs", content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (!string.IsNullOrWhiteSpace(json))
                        {
                            modelGMaps = await Task.Run(() =>
                               JsonConvert.DeserializeObject<ModelGMaps>(json)
                            ).ConfigureAwait(false);

                        }
                    }
                }
                catch (Exception)
                {

                }
            }

            return modelGMaps;
        }
    }
}
