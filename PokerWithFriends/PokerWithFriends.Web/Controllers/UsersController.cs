using PokerWithFriends.Service.Domains;
using PokerWithFriends.Service.Requests;
using PokerWithFriends.Service.Services;
using PokerWithFriends.Service;
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
    public class UsersController : ApiController
    {
        readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet, Route("api/users")]
        public HttpResponseMessage GetAllUsers()
        {
            List<User> users = usersService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        [HttpGet, Route("api/users/{id}")]
        public HttpResponseMessage GetUserById(int id)
        {
            User user = usersService.GetUserById(id);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        [HttpPost, Route("api/users")]
        public HttpResponseMessage Create(UserRequest req)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            int id = usersService.Create(req);
            return Request.CreateResponse(HttpStatusCode.Created, id);
        }

        [HttpPut, Route("api/users/{id}")]
        public HttpResponseMessage Update(UserUpdateRequest req)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            usersService.Update(req);
            return Request.CreateResponse(HttpStatusCode.OK, "Succesfully Updated");
        }

        [HttpDelete, Route("api/users/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            usersService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, ModelState);
        }

        [HttpPost, Route("api/users/login")]
        public HttpResponseMessage Login(LoginRequest login)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            try
            {

                User user = usersService.Login(login);
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch(InvalidLoginException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}