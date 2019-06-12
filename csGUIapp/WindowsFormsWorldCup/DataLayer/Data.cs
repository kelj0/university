using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace DataLayer
{ 
    public class Data
    {
        private const string TeamsUrl    = "https://world-cup-json-2018.herokuapp.com/teams";
        private const string MatchesUrl  = "https://world-cup-json-2018.herokuapp.com/matches/country?fifa_code=";
        private const string ResultsUrl  = "https://world-cup-json-2018.herokuapp.com/teams/results";

        public string GetUrl(string url)
        {
            var client = new RestClient(url);

            var response = client.Execute(new RestRequest());

            return response.Content;
        }


    }
}
