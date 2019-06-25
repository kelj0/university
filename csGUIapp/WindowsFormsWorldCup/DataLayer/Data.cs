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
                Console.WriteLine($"Cant fetch url.. Response code: {response.StatusCode}. Fetching again..");
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

        public static async Task<int> GetPlayerYellowCardsForMatch(string playerName,string matchID,string teamCode)
        {
            dynamic response = await GetCountryMatches(teamCode);
            int cards = 0;
            foreach (var match in response)
            {
                if (match.home_team.code == teamCode) {
                    foreach (var ev in match.home_team_events){
                        if(ev.type_of_event == "yellow-card" && ev.player == playerName) { ++cards; }
                    }
                    return cards;
                }
                else
                {
                    foreach (var ev in match.away_team_events){
                        if (ev.type_of_event == "yellow-card" && ev.player == playerName) { ++cards; }
                    }
                    return cards;
                }
            }
            return -100;
        }

        public static async Task<int> GetPlayergoalsForMatch(string playerName, string matchID, string teamCode)
        {
            dynamic response = await GetCountryMatches(teamCode);
            int goals = 0;
            foreach (var match in response)
            {
                if (match.home_team.code == teamCode)
                {
                    foreach (var ev in match.home_team_events)
                    {
                        if (ev.type_of_event == "goal" && ev.player == playerName) { ++goals; }
                    }
                    return goals;
                }
                else
                {
                    foreach (var ev in match.away_team_events)
                    {
                        if (ev.type_of_event == "goal" && ev.player == playerName) { ++goals; }
                    }
                    return goals;
                }
            }
            return -100;
        }


        ///<summary>
        /// Returns number of wins, or -100 if no id was found
        ///</summary
        public static async Task<dynamic> GetCountryResults(string countryName)
        {
            dynamic response = JsonConvert.DeserializeObject(await Task.Run(() => GetUrl(ResultsUrl)));
            foreach (var s in response)
            {
                if (s.country == countryName) return s;
            }
            throw new ArgumentException($"Cant find {countryName} results");
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

        /// <summary>
        /// Gets match id from 2 team, if they didnt play returns 0
        /// </summary>
        public static Match GetMatch(Team t1, string t2)
        {
            foreach (Match match in t1.matches)
            {
                if (match.home_team == t2) return match;
                if (match.away_team == t2) return match;
            }
            throw new ArgumentException($"Match {t1.teamName} vs {t2} doesnt exist!");
        }

        /// <summary>
        /// Reads config file, returns List<string> [0] language | [1] name | [2] fav players(split with ,)
        /// </summary>
        public static List<string> ReadConfigFile() => File.ReadAllText(@"..\..\..\config.txt").Split('|').ToList();

        /// <summary>
        /// Saves app data to config file
        /// </summary>
        public static void SaveDataToFile(Team t,string lng)
        {
            string favPlayers = "";
            foreach (var p in t.players)
            {
                if (p.Value.favorite)
                {
                    favPlayers += p.Value.name + ",";
                }
            }
            favPlayers = favPlayers.Remove(favPlayers.Length - 1);

            FileStream fs = new FileStream(@"..\..\..\config.txt", FileMode.Create, FileAccess.Write, FileShare.Write);
            fs.Close();
            using (StreamWriter sw = new StreamWriter(@"..\..\..\config.txt", true, Encoding.ASCII))
            {
                sw.Write($"{lng}|{t.teamName}|{favPlayers}");
            }
            Console.WriteLine($"Wrote new config to config.txt\n{lng}|{t.teamName}|{favPlayers}");
        }
    }
}
