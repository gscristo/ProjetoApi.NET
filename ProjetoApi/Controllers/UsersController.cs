using Api.Model;
using Microsoft.Extensions.Configuration;
using Core.Infrastructure.Exceptions;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace Api.Controllers

{   [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICreateUsers _createUsers;
        //private readonly IDeleteUsers _deleteUsers;
        private readonly IUpdateUsers _updateUsers;
        private readonly IGetAllUsers _getAllUsers;

        public UsersController(ICreateUsers createUsers,
                              //IDeleteUsers deleteUsers,
                              IUpdateUsers updateUsers,
                              IGetAllUsers getAllUsers
                              )
        {
            _createUsers = createUsers;
            //_deleteUsers = deleteUsers;
            _updateUsers = updateUsers;
            _getAllUsers = getAllUsers;

        }


        [HttpPost("GetAll")]
        public async Task<IActionResult> Get([FromBody] PaginationRequest paginationRequest)
        {
            try
            {
                var model = await _getAllUsers.Execute(paginationRequest.PageSize, paginationRequest.PageIndex, paginationRequest.Sort, paginationRequest.Direction);

                return Ok(Result.Create(model, HttpStatusCode.OK, "Operação executada com sucesso!"));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUsersRequest createUsersRequest)
        {
            try
            {
                var result = await _createUsers.Execute(createUsersRequest);

                return Ok(Result.Create(result, System.Net.HttpStatusCode.OK, "Operação executada com sucesso!"));
            }
            catch (ApiDomainException domainException)
            {
                return BadRequest(Result.Create(domainException.Errors, HttpStatusCode.BadRequest, "Ops! Algo de errado aconteceu, verifique se o código do produto ja existe ou se a unidade é válida."));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUsersRequest updateUsersRequest)
        {
            try    
            {
                var result = await _updateUsers.Execute(updateUsersRequest);

                return Ok(Result.Create(updateUsersRequest, System.Net.HttpStatusCode.OK, "Operação executada com sucesso!"));
            }
            catch (ApiDomainException domainException)
            {
                return BadRequest(Result.Create(domainException.Errors, HttpStatusCode.BadRequest, "Erro ao executar a operação"));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
