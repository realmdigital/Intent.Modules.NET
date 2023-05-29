using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDb.TestApplication.Application.Interfaces;
using MongoDb.TestApplication.Application.SingleIndexEntitySingleParents;
using MongoDb.TestApplication.Domain.Common.Interfaces;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.AspNetCore.Controllers.Controller", Version = "1.0")]

namespace MongoDb.TestApplication.Api.Controllers
{
    [ApiController]
    [Route("api/single-index-entity-single-parents")]
    public class SingleIndexEntitySingleParentsController : ControllerBase
    {
        private readonly ISingleIndexEntitySingleParentsService _appService;
        private readonly IMongoDbUnitOfWork _mongoDbUnitOfWork;

        public SingleIndexEntitySingleParentsController(ISingleIndexEntitySingleParentsService appService,
            IMongoDbUnitOfWork mongoDbUnitOfWork)
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
        public async Task<ActionResult<string>> CreateSingleIndexEntitySingleParent(
            [FromBody] SingleIndexEntitySingleParentCreateDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = default(string);
            result = await _appService.CreateSingleIndexEntitySingleParent(dto, cancellationToken);
            await _mongoDbUnitOfWork.SaveChangesAsync(cancellationToken);
            return Created(string.Empty, result);
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified SingleIndexEntitySingleParentDto.</response>
        /// <response code="400">One or more validation errors have occurred.</response>
        /// <response code="404">Can't find an SingleIndexEntitySingleParentDto with the parameters provided.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SingleIndexEntitySingleParentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SingleIndexEntitySingleParentDto>> FindSingleIndexEntitySingleParentById(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = default(SingleIndexEntitySingleParentDto);
            result = await _appService.FindSingleIndexEntitySingleParentById(id, cancellationToken);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// </summary>
        /// <response code="200">Returns the specified List&lt;SingleIndexEntitySingleParentDto&gt;.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<SingleIndexEntitySingleParentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SingleIndexEntitySingleParentDto>>> FindSingleIndexEntitySingleParents(CancellationToken cancellationToken = default)
        {
            var result = default(List<SingleIndexEntitySingleParentDto>);
            result = await _appService.FindSingleIndexEntitySingleParents(cancellationToken);
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
        public async Task<ActionResult> UpdateSingleIndexEntitySingleParent(
            [FromRoute] string id,
            [FromBody] SingleIndexEntitySingleParentUpdateDto dto,
            CancellationToken cancellationToken = default)
        {
            await _appService.UpdateSingleIndexEntitySingleParent(id, dto, cancellationToken);
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
        public async Task<ActionResult> DeleteSingleIndexEntitySingleParent(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            await _appService.DeleteSingleIndexEntitySingleParent(id, cancellationToken);
            await _mongoDbUnitOfWork.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}