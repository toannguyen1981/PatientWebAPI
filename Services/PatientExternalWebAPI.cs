using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PatientWebAPI.Helpers;
using PatientWebAPI.Models;
using PatientWebAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PatientWebAPI.Services
{
    public class PatientExternalWebAPI : IPatientExternalWebApi
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClient;
        private readonly IMemoryCache _memoryCache;

        public PatientExternalWebAPI(IConfiguration configuration, IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory;
            _memoryCache = memoryCache;
        }

        public async Task<string> GetJWTToken(Credential credential, string url)
        {
            string token = null;
            var json = JsonConvert.SerializeObject(credential);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            using (var client = _httpClient.CreateClient())
            {
                var response = await client.SendAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    token = JObject.Parse(result)[GlobalConstants.PatientAPIReturnedJWT].ToString();
                }
            }

            return token;
        }

        /// <summary>
        /// Returns a list of patient visits based on the patient's id
        /// </summary>
        /// <param name="ssn">Patient's social security number</param>
        /// <returns>return type List<DTOPatientLabVisit></returns>
        public async Task<List<DTOPatientLabVisit>> GetPatientLabVisit(string ssn)
        {
            string jwt = null;
            List<DTOPatientLabVisit> patientVisitList = null;

            //retrieve the username and password from AppSettings.json
            var credential = new Credential
            {
                Identifier = _configuration.GetValue<string>(GlobalConstants.PatientLabVisitIdentifier),
                Password = _configuration.GetValue<string>(GlobalConstants.PatientLabVisitPassword)
            };

            //check if the token's value is already cached before calling the external api for a new token
            jwt = GlobalFunctions.GetCacheItem<string>(_memoryCache, credential.Identifier);

            if (String.IsNullOrEmpty(jwt))
            {
                jwt = await GetJWTToken(credential, _configuration.GetValue<string>(GlobalConstants.CredentialsAuthenticationUrl));
                GlobalFunctions.AddCacheItem<string>(_memoryCache, credential.Identifier, jwt);
            }

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_configuration.GetValue<string>(GlobalConstants.PatientLabVisitUrl) + $"?SSN={ssn}")
            };

            request.Headers.Add("Authorization", $"bearer {jwt}");
            using (var client = _httpClient.CreateClient())
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    patientVisitList = JsonConvert.DeserializeObject<List<DTOPatientLabVisit>>(result);
                }
            }

            return patientVisitList;
        }
        public Task GetPatientLabResults(string ssn)
        {
            throw new NotImplementedException();
        }
        public Task GetPatientMedication(string ssn)
        {
            throw new NotImplementedException();
        }
        public Task GetPatientVaccination(string ssn)
        {
            throw new NotImplementedException();
        }
    }
}
