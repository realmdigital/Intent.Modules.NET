using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDb.TestApplication.Application.IdTypeOjectIdStrs;
using MongoDb.TestApplication.Application.Interfaces;
using MongoDb.TestApplication.Domain.Common.Interfaces;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.Controllers.Controller", Version = "1.0")]

namespace MongoDb.TestApplication.Api.Controllers
{
    [ApiController]
    [Route("api/id-type-oject-id-strs")]
    public class IdTypeOjectIdStrsController : ControllerBase
    {
        private readonly IIdTypeOjectIdStrsService _appService;
        private readonly IMongoDbUnitOfWork _mongoDbUnitOfWork;

        public IdTypeOjectIdStrsController(IIdTypeOjectIdStrsService appService, IMongoDbUnitOfWork mongoDbUnitOfWork)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
            _mongoDbUnitOfWork = mongoDbUnitOfWork ?? throw new ArgumentNullException(nameof(mongoDbUnitOfWork));
        }

        /// <summary>
        /// </summary>
        /// <response code="201">Successfully created.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> CreateIdTypeOjectIdStr(
            [FromBody] IdTypeOjectIdStrCreateDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = default(string);
            result = await _appService.CreateIdTypeOjectIdStr(dto);
            await _mongoDbUnitOfWork.SaveChangesAsync(cancellationToken);
            return Created(string.Empty, result);
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified IdTypeOjectIdStrDto.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        /// <response code="404">Can't find an IdTypeOjectIdStrDto with the parameters provided.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IdTypeOjectIdStrDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IdTypeOjectIdStrDto>> FindIdTypeOjectIdStrById(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = default(IdTypeOjectIdStrDto);
            result = await _appService.FindIdTypeOjectIdStrById(id);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified List&lt;IdTypeOjectIdStrDto&gt;.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<IdTypeOjectIdStrDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<IdTypeOjectIdStrDto>>> FindIdTypeOjectIdStrs(CancellationToken cancellationToken = default)
        {
            var result = default(List<IdTypeOjectIdStrDto>);
            result = await _appService.FindIdTypeOjectIdStrs();
            return Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <response code="204">Successfully updated.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateIdTypeOjectIdStr(
            [FromRoute] string id,
            [FromBody] IdTypeOjectIdStrUpdateDto dto,
            CancellationToken cancellationToken = default)
        {
            await _appService.UpdateIdTypeOjectIdStr(id, dto);
            await _mongoDbUnitOfWork.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Successfully deleted.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteIdTypeOjectIdStr(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            await _appService.DeleteIdTypeOjectIdStr(id);
            await _mongoDbUnitOfWork.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}