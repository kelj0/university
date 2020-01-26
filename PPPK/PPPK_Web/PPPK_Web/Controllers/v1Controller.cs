using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace PPPK_Web.Controllers
{
    public class v1Controller : ApiController
    {
        [HttpGet]
        public vozilo vozilo(int? id)
        {
            if (id != null)
            {
                return new vozilo { marka = "test", godina_proizvodnje = (int)id };
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public HttpResponseMessage vozac(int? id, string ime, string prezime, string broj_mobitela, string broj_vozacke)
        {
            if (id != null)
            {
                //return new vozac { id = (int)id, ime = ime, prezime=prezime, broj_mobitela=broj_mobitela,broj_vozacke=broj_vozacke };
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                string fullyQualifiedUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority) + $"/Vozaci/Vozac/{id}";
                response.Headers.Location = new Uri(fullyQualifiedUrl);
                return response;
            }
            else
            {
                return null;
            }

            
        }

    }
}
