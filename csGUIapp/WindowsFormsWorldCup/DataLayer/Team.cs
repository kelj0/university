using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Team
    {
        public string teamName { get; set; }
        public string teamCode { get; set; }
        public List<string> firsteleven = new List<string>();
        public List<string> substitutions = new List<string>();
        public Dictionary<string, Player> players = new Dictionary<string, Player>();
        public List<Match> matches = new List<Match>();
 

        public Team(string tName,string tCode)
        {
            teamName = tName;
            teamCode = tCode;
        }

        public string GetPlayerNumber(string p) => players[p].shirt_number;

        public string GetPlayerPosition(string p) => players[p].position;

        public bool IsPlayerCaptain(string p) => players[p].captain;

        public int GetPlayerGoals(string p) => players[p].goals;

        public int GetPlayerCards(string p) => players[p].cards;


        public async Task<bool> SetUp()
        {
            dynamic response = await Data.GetCountryMatches(teamCode);

            if (response[0].home_team.code == teamCode)
            {
                foreach (var pl in response[0].home_team_statistics.starting_eleven)
                {
                    firsteleven.Add((string)pl.name);
                    players.Add((string)pl.name, new Player() {
                        name = (string)pl.name,
                        shirt_number = (string)pl.shirt_number,
                        position = (string)pl.position,
                        captain = Convert.ToBoolean(pl.captain),
                        cards = 0,
                        goals = 0
                    });
                }
                foreach (var pl in response[0].home_team_statistics.substitutes)
                {
                    substitutions.Add((string)pl.name);
                    players.Add((string)pl.name, new Player() {
                        name = (string)pl.name,
                        shirt_number = (string)pl.shirt_number,
                        position = (string)pl.position,
                        captain = Convert.ToBoolean(pl.captain),
                        cards = 0,
                        goals = 0
                    });
                }

            }
            else
            {
                foreach (var pl in response[0].away_team_statistics.starting_eleven)
                {
                    firsteleven.Add((string)pl.name);
                    players.Add((string)pl.name, new Player() {
                        name = (string)pl.name,
                        shirt_number = (string)pl.shirt_number,
                        position = (string)pl.position,
                        captain = Convert.ToBoolean(pl.captain),
                        cards = 0,
                        goals = 0
                    });
                }
                foreach (var pl in response[0].away_team_statistics.substitutes)
                {
                    substitutions.Add((string)pl.name);
                    players.Add((string)pl.name, new Player() {
                        name = (string)pl.name,
                        shirt_number = (string)pl.shirt_number,
                        position = (string)pl.position,
                        captain = Convert.ToBoolean(pl.captain),
                        cards = 0,
                        goals = 0
                    });
                }
            }
            
            foreach (var match in response)
            {
                matches.Add(new Match() {
                    attendance = (string)match.attendance,
                    away_team = (string)match.away_team.country,
                    home_team = (string)match.home_team.country,
                    location = (string)match.location,
                    score = $"{match.home_team.goals}:{match.away_team.goals}",
                    winner = (string)match.winner
                });
                if (match.home_team.code == teamCode)
                {
                    foreach (var ev in match.home_team_events)
                    {
                        if (ev.type_of_event == "goal")
                        {
                            players[(string)ev.player].goals++;
                        }
                        if (ev.type_of_event == "yellow-card")
                        {

                            players[(string)ev.player].cards++;
                        }
                    }
                }
                else
                {
                    foreach (var ev in match.away_team_events)
                    {
                        if (ev.type_of_event == "goal")
                        {
                            players[(string)ev.player].goals++;
                        }
                        if (ev.type_of_event == "yellow-card")
                        {
                            players[(string)ev.player].cards++;
                        }
                    }
                }
            }
            return true;
        }
    }
}

