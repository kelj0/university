using PPPK_Web.HELPERS;
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
        public HttpResponseMessage vozac(int? id, string ime, string prezime, string broj_mobitela, string broj_vozacke)
        {
            if (Validators.validID(id))
            {
                DatabaseHandler.updateVozac((int)id, ime, prezime, broj_mobitela, broj_vozacke);
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

        [HttpPost]
        public HttpResponseMessage vozac(int? id)
        {
            if (Validators.validID(id))
            {
                DatabaseHandler.deleteVozac((int)id);
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                string fullyQualifiedUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/Vozaci";
                response.Headers.Location = new Uri(fullyQualifiedUrl);
                return response;
            }
            else{ return null; }
        }

        [HttpGet]
        public HttpResponseMessage vozilo(int? id, string marka, int tipovi_vozila, decimal pocetni_km, decimal trenutni_km, int godina_proizvodnje)
        {
            if (Validators.validID(id))
            {
                DatabaseHandler.updateVozilo((int)id, marka, tipovi_vozila, pocetni_km, trenutni_km, godina_proizvodnje);
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                string fullyQualifiedUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority) + $"/Vozila/Vozilo/{id}";
                response.Headers.Location = new Uri(fullyQualifiedUrl);
                return response;
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public HttpResponseMessage vozilo(int? id)
        {
            if (Validators.validID(id))
            {
                DatabaseHandler.deleteVozilo((int)id);
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                string fullyQualifiedUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/Vozila";
                response.Headers.Location = new Uri(fullyQualifiedUrl);
                return response;
            }
            else { return null; }
        }

        [HttpGet]
        public HttpResponseMessage servis(string mjesto, DateTime datum, decimal cijena, string info, int vozilo_id)
        {
            DatabaseHandler.insertServis(mjesto, datum, cijena, info,vozilo_id);
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            string fullyQualifiedUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/Vozila/";
            response.Headers.Location = new Uri(fullyQualifiedUrl);
            return response;
        }

        [HttpGet]
        public HttpResponseMessage servis(int id, string mjesto, DateTime datum, decimal cijena, string info, int vozilo_id)
        {
            if (Validators.validID(id))
            {
                DatabaseHandler.updateServis(id, mjesto, datum, cijena, info);
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                string fullyQualifiedUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority) + $"/Vozila/Vozilo/{vozilo_id}";
                response.Headers.Location = new Uri(fullyQualifiedUrl);
                return response;
            }
            else { return null; }
        }

        [HttpPost]
        public HttpResponseMessage servis(int? id, int vozilo_id)
        {
            if (Validators.validID(id))
            {
                DatabaseHandler.deleteServis(id);
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                string fullyQualifiedUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority) + $"/Vozila/Vozilo/{vozilo_id}";
                response.Headers.Location = new Uri(fullyQualifiedUrl);
                return response;
            }
            else { return null; }
        }

        [HttpPost]
        public HttpResponseMessage putniNalog(int? id)
        {
            if (Validators.validID(id))
            {
                DatabaseHandler.deletePutniNalog((int)id);
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                string fullyQualifiedUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/PutniNalozi";
                response.Headers.Location = new Uri(fullyQualifiedUrl);
                return response;
            }
            else { return null; }
        }

        [HttpPost]
        public HttpResponseMessage obrisiRutu(int? id)
        {
            if (Validators.validID(id))
            {
                DatabaseHandler.deleteRuta((int)id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else { return Request.CreateResponse(HttpStatusCode.BadRequest); }
        }

        [HttpPost]
        public HttpResponseMessage obrisiRute(int? id)
        {
            if (Validators.validID(id))
            {
                DatabaseHandler.deleteRute((int)id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else { return Request.CreateResponse(HttpStatusCode.BadRequest); }
        }


    }
}
