using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using System.Threading;

namespace DataLayer
{
    public static class Data
    {
        private const string TeamsUrl = "https://world-cup-json-2018.herokuapp.com/teams";
        private const string MatchesUrl = "https://world-cup-json-2018.herokuapp.com/matches/country?fifa_code=";
        private const string ResultsUrl = "https://world-cup-json-2018.herokuapp.com/teams/results";
        

        ///<summary>
        /// Fetches given url, returns response content
        ///</summary
        public static async Task<string> GetUrl(string url)
        {
            var client = new RestClient(url);
            var response = await client.ExecuteTaskAsync(new RestRequest());

            if(!response.IsSuccessful)
            {
                Console.WriteLine($"Response code: {response.StatusCode}");
                response = client.Execute(new RestRequest());

                if (!response.IsSuccessful)
                {
                    throw new WebException("Problems with fetching url, please check your network connection");
                }
            }
            return response.Content;

        }

        ///<summary>
        /// Returns country names as List<string>
        ///</summary
        public static async Task<List<string>> GetCountryNames()
        {
            dynamic response = JsonConvert.DeserializeObject(await Task.Run(() => GetUrl(TeamsUrl)));
            List<string> country = new List<string>();
            foreach (var s in response) { country.Add((string)s.country); }
            return country;
        }

        /// <summary>
        /// Returns country code, throws ArgumentException if cant find country with that name
        /// </summary>
        public static async Task<string> GetCountryCode(string favoriteTeam)
        {
            await GetUrl(TeamsUrl);
            dynamic response = JsonConvert.DeserializeObject(await Task.Run(() => GetUrl(TeamsUrl)));
            foreach (var s in response)
            {
                if (s.country == favoriteTeam)
                    return s.fifa_code;
            }
            throw new System.ArgumentException($"Cant find country with {favoriteTeam} name");

        }

        ///<summary>
        /// Returns number of wins, or -100 if no id was found
        ///</summary
        public static async Task<int> GetCountryWins(int id)
        {
            dynamic response = JsonConvert.DeserializeObject(await Task.Run(() => GetUrl(ResultsUrl)));
            foreach (var s in response)
            {
                if (s.id == id)
                    return s.wins;
            }
            return -100;
        }

        /// <summary>
        /// Returns country matches in dynamic obj
        /// </summary>
        public static async Task<dynamic> GetCountryMatches(string fifa_code)
        {
            return JsonConvert.DeserializeObject(await Task.Run(() => GetUrl(MatchesUrl + fifa_code)));
        }

        ///<summary>
        /// Returns first eleven of team in match
        ///</summary
        public static async Task<List<string>> GetMatchFirstEleven(string fifa_id, string fifa_code)
        {
            dynamic response = JsonConvert.DeserializeObject(await Task.Run(() => GetUrl(MatchesUrl + fifa_code)));
            List<string> starting_eleven = new List<string>();
            foreach (var match in response)
            {
                if (match.fifa_id == fifa_id)
                {

                    if (match.home_team.code == fifa_code)
                    {
                        foreach (var player in match.home_team_statistics.starting_eleven)
                            starting_eleven.Add((string)player.name);
                        return starting_eleven;
                    }
                    else
                    {
                        foreach(var player in match.away_team_statistics.starting_eleven)
                            starting_eleven.Add((string)player.name);
                        return starting_eleven;
                    }
                }
            }
            throw new ArgumentException("Problems fetching first eleven, maybe your fifa_id is wrong?");
        }

    }
}
