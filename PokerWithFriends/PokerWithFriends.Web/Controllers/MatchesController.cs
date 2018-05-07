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
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class MatchesController : ApiController
    {
        readonly IMatchesService matchesService; 

        public MatchesController(IMatchesService matchesService)
        {
            this.matchesService = matchesService;
        }

        public HttpResponseMessage GetAll()
        {
            List<Match> matches = matchesService.GetAllMatches();
            return Request.CreateResponse(HttpStatusCode.OK, matches);
        }

        public HttpResponseMessage GetById(int id)
        {
            Match match = matchesService.GetMatchById(id);
            return Request.CreateResponse(HttpStatusCode.OK, match);
        }

        public HttpResponseMessage Create(MatchRequest req)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            int id = matchesService.CreateMatch(req);
            return Request.CreateResponse(HttpStatusCode.Created, id);
        }

        public HttpResponseMessage Update(MatchUpdateRequest req)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            matchesService.UpdateMatch(req);
            return Request.CreateResponse(HttpStatusCode.OK, "Updated");
        }

        public HttpResponseMessage Delete(int id)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            matchesService.DeleteMatch(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
        }
    }
}