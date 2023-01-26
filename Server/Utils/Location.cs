using System;
using HelpDesk.Shared.Models;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;
using Microsoft.AspNetCore.Hosting;

namespace HelpDesk.Server.Utils
{
	public class Location
	{
        private readonly IWebHostEnvironment _appEnvironment;

		public Location(IWebHostEnvironment appEnvironment)
		{
            _appEnvironment = appEnvironment;
		}

		public Localizacion GetLocationByIp(string ipAddress)
        {
            CityResponse response;
            using var reader = new DatabaseReader(_appEnvironment.ContentRootPath + "//Content//GeoLite2-City.mmdb");

            try
            {
                response = reader.City(ipAddress);
                Localizacion localizacion = new();

                localizacion.Longitude = (double)response.Location.Longitude;
                localizacion.Latitude = (double)response.Location.Latitude;

                foreach (var value in response.MostSpecificSubdivision.Names)
                {
                    if (value.Key == "es")
                    {
                        localizacion.Town = value.Value;
                    }
                }

                foreach (var value in response.City.Names)
                {
                    if (value.Key == "es")
                    {
                        localizacion.City = value.Value;
                    }
                }

                foreach (var value in response.Country.Names)
                {
                    if (value.Key == "es")
                    {
                        localizacion.Country = value.Value;
                    }
                }

                return localizacion;
            }
            catch (Exception)
            {
                return null;
            }
        }
	}
}

