using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using ShowCase.Data.Models.ApiModels.Project;
using ShowCase.Util.StaticClasses;
using ShowCase.Service.DataManagers;

namespace ShowCase.Api.Controllers
{
    public class ProjectsController : BaseController
    {
        public ProjectsController(ProjectManager projectManager)
        {
            ProjectManager = projectManager;
        }

        private ProjectManager ProjectManager { get; }

        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var onlyPublished = !User.Identity.IsAuthenticated;

            var getProjectsResult = await ProjectManager.GetProjectsAsync(onlyPublished);
            if (getProjectsResult.Success)
            {
                return Ok(getProjectsResult.ReturningValue.ToArray().Adapt<ListProjectsApiModel[]>());
            }
            else
            {
                return StatusCode(500, getProjectsResult.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var onlyPublished = !User.Identity.IsAuthenticated;

            var getProjectResult = await ProjectManager.GetProjectAsync(id, onlyPublished);
            if (getProjectResult.Success)
            {
                return Ok(getProjectResult.ReturningValue.Adapt<ProjectApiModel>());
            }
            else
            {
                return StatusCode(500, getProjectResult.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostProject(CreateProjectApiModel model)
        {
            if (ModelState.IsValid)
            {
                var createProjectResult = await ProjectManager.CreateProjectAsync(model);
                if (createProjectResult.Success)
                {
                    return Ok(createProjectResult.Message);
                }
                else
                {
                    return StatusCode(createProjectResult.StatusCode, createProjectResult.Message);
                }
            }
            else
            {
                return BadRequest(ReturningMessages.InvalidDataSupplied());
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutProject(EditProjectApiModel model)
        {
            if (ModelState.IsValid)
            {
                var updateProjectResult = await ProjectManager.UpdateProjectAsync(model);
                if (updateProjectResult.Success)
                {
                    return Ok(updateProjectResult.Message);
                }
                else
                {
                    return StatusCode(updateProjectResult.StatusCode, updateProjectResult.Message);
                }
            }
            else
            {
                return BadRequest(ReturningMessages.InvalidDataSupplied());
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var deleteProjectResult = await ProjectManager.DeleteProjectAsync(id);
            if (deleteProjectResult.Success)
            {
                return Ok(deleteProjectResult.Message);
            }
            else
            {
                return StatusCode(deleteProjectResult.StatusCode, deleteProjectResult.Message);
            }
        }

        #endregion
    }
}
