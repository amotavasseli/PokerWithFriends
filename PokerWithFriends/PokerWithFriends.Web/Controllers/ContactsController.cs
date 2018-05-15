using PokerWithFriends.Service.Domains;
using PokerWithFriends.Service.Requests;
using PokerWithFriends.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PokerWithFriends.Web.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContactsController : ApiController
    {
        readonly IContactsService contactsService; 
        public ContactsController(IContactsService contactsService)
        {
            this.contactsService = contactsService;
        }

        [HttpGet, Route("api/contacts")]
        public HttpResponseMessage GetAll()
        {
            List<Contact> contacts = contactsService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, contacts);
        }

        [HttpGet, Route("api/contacts/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            Contact contact = contactsService.GetById(id);
            return Request.CreateResponse(HttpStatusCode.OK, contact);
        }

        [HttpPost, Route("api/contacts")]
        public HttpResponseMessage Create(ContactRequest req)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
            int id = contactsService.Create(req);
            return Request.CreateResponse(HttpStatusCode.Created, id);
        }

        [HttpPut, Route("api/contact/{id}")]
        public HttpResponseMessage Update(ContactUpdateRequest req)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
            contactsService.Update(req);
            return Request.CreateResponse(HttpStatusCode.OK, ModelState);
        }

        [HttpDelete, Route("api/contact/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
            contactsService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, ModelState);
        }
    }
}