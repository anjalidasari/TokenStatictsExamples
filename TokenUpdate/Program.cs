using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Threading;
using System;
using System.Data;
using System.Net.Http;
using System.Collections.Generic;

namespace TokenUpdate
{
    class Program
    {
        private static string connectionString = "Server=.;Database=TokenExample;User Id=sa;Password=abc;";

        static void Main(string[] args)
        {
            Console.WriteLine(" Token Price Updater Started...");

            try
            {
                UpdateAllTokenPrices();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Main Error: " + ex.ToString());
            }

            
        }

        static void UpdateAllTokenPrices()
        {
            var tokens = new List<(int Id, string Symbol)>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Id, Symbol FROM Token", con);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tokens.Add((Convert.ToInt32(reader["Id"]), reader["Symbol"].ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" DB Read Error: " + ex.ToString());
                return;
            }

            foreach (var token in tokens)
            {
                try
                {
                    decimal price = GetPriceFromAPI(token.Symbol);

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        UpdateTokenPrice(con, token.Id, price);
                    }

                    Console.WriteLine($" Updated {token.Symbol} => {price}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Update Error for {token.Symbol}: " + ex.ToString());
                }
            }
        }

        static decimal GetPriceFromAPI(string symbol)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://min-api.cryptocompare.com/data/price?fsym={symbol}&tsyms=USD";
                    var response = client.GetStringAsync(url).Result;

                    var json = JObject.Parse(response);

                    if (json["USD"] != null)
                    {
                        return Convert.ToDecimal(json["USD"]);
                    }
                    else
                    {
                        Console.WriteLine($" API did not return USD for {symbol}. Response: {json}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" API Error for {symbol}: " + ex.ToString());
            }

            return 0;
        }

        static void UpdateTokenPrice(SqlConnection con, int tokenId, decimal price)
        {
            SqlCommand updateCmd = new SqlCommand(
                "UPDATE Token SET Price = @Price WHERE Id = @Id", con);
            updateCmd.Parameters.AddWithValue("@Price", price);
            updateCmd.Parameters.AddWithValue("@Id", tokenId);
            updateCmd.ExecuteNonQuery();
        }
    }
}
