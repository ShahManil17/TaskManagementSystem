using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using System.Collections.Generic;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.Repositories.AdminServices;
using TaskManagementSystem.Core.Repositories.ManagerServices;
using TaskManagementSystem.Core.ResponseModels;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Controllers
{
    [Route("[controller]")]
    public class ServicesController : Controller
    {
        private readonly IAdmin _admin;
        private readonly IManager _manager;
        private readonly IHttpContextAccessor _contextAccesor;
        public ServicesController(IAdmin admin, IManager manager, IHttpContextAccessor contextAccesor)
        {
            _admin = admin;
            _manager = manager;
            _contextAccesor = contextAccesor;
        }

        #region Admin Functionality

        [Authorize(Policy = "addUsers")]
        [HttpGet("addUsers")]
        public async Task<IActionResult> AddUsers()
        {
            ViewBag.Permissions = HomeController.Permissions;
            ReturnObject<List<Roles>> rolesResponse = await _admin.GetRoles();
            if(rolesResponse.IsSuccess)
            {
                ViewBag.Roles = rolesResponse.Result;
            }
            else
            {
                ModelState.AddModelError("Message", rolesResponse.ErrorMessage!=null ? rolesResponse.ErrorMessage : "Something Went Wrong Loading Roles!");
            }
            return View();
        }

        [Authorize(Policy = "addUsers")]
        [HttpPost("addUsers")]
        public async Task<IActionResult> AddUsers(AddUserModel model)
        {
            #region Send data To View
            ViewBag.Permissions = HomeController.Permissions;
            ReturnObject<List<Roles>> rolesResponse = await _admin.GetRoles();
            if (rolesResponse.IsSuccess)
            {
                ViewBag.Roles = rolesResponse.Result;
            }
            else
            {
                ModelState.AddModelError("Message", rolesResponse.ErrorMessage != null ? rolesResponse.ErrorMessage : "Something Went Wrong Loading Roles!");
            }
            #endregion
            if (!ModelState.IsValid)
            {
                return View();
            }
            ReturnObject<Users> insertResponse = await _admin.AddUser(model);
            if(insertResponse.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Message", insertResponse.ErrorMessage != null ? insertResponse.ErrorMessage : "Something Went Wrong Inserting User!");
                return View();
            }
        }

        [Authorize(Policy = "assignRoles")]
        [HttpGet("assignRoles")]
        public async Task<IActionResult> AssignRoles()
        {
            ViewBag.Permissions = HomeController.Permissions;

            #region Get UserDetails
            ReturnObject<List<Users>> usersResponse = await _admin.GetUsers();
            if (usersResponse.IsSuccess)
            {
                ViewBag.Users = usersResponse.Result;
            }
            else
            {
                ModelState.AddModelError("Message", usersResponse.ErrorMessage != null ? usersResponse.ErrorMessage : "Something Went Wrong Loading Users!");
                return View();
            }
            #endregion

            #region Get RoleDetails
            ReturnObject<List<Roles>> RoleResponse = await _admin.GetRoles();
            if (RoleResponse.IsSuccess)
            {
                ViewBag.Roles = RoleResponse.Result;
            }
            else
            {
                ModelState.AddModelError("Message", RoleResponse.ErrorMessage != null ? RoleResponse.ErrorMessage : "Something Went Wrong Loading Roles!");
                return View();
            }
            #endregion

            return View();
        }

        [Authorize(Policy = "assignRoles")]
        [HttpPost("assignRoles")]
        public async Task<IActionResult> AssignRoles(AssignRoleModel model)
        {
            #region Send data To View
            ViewBag.Permissions = HomeController.Permissions;
            ReturnObject<List<Users>> usersResponse = await _admin.GetUsers();
            if (usersResponse.IsSuccess)
            {
                ViewBag.Users = usersResponse.Result;
            }
            else
            {
                ModelState.AddModelError("Message", usersResponse.ErrorMessage != null ? usersResponse.ErrorMessage : "Something Went Wrong Loading Users!");
                return View();
            }

            ReturnObject<List<Roles>> RoleResponse = await _admin.GetRoles();
            if (RoleResponse.IsSuccess)
            {
                ViewBag.Roles = RoleResponse.Result;
            }
            else
            {
                ModelState.AddModelError("Message", RoleResponse.ErrorMessage != null ? RoleResponse.ErrorMessage : "Something Went Wrong Loading Roles!");
                return View();
            }
            #endregion
            if (!ModelState.IsValid)
            {
                return View();
            }

            ReturnObject<Users> changeRoleResponse = await _admin.ChangeRole(model.UserId, model.RoleId);
            if (changeRoleResponse.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Message", changeRoleResponse.ErrorMessage != null ? changeRoleResponse.ErrorMessage : "Something Went Wrong Assigning Role User!");
                return View();
            }
        }

        [Authorize(Policy = "assignPermissions")]
        [HttpGet("assignPermissions")]
        public async Task<IActionResult> AssignPermission()
        {
            ViewBag.Permissions = HomeController.Permissions;
            ReturnObject<List<Permissions>> permissionsResponse = await _admin.GetPermissions();
            if(!permissionsResponse.IsSuccess)
            {
                ModelState.AddModelError("Message", permissionsResponse.ErrorMessage != null ? permissionsResponse.ErrorMessage : "Something Went Wrong Fetching Permissions!");
            }
            else
            {
                ViewBag.AllPermissions = permissionsResponse.Result;
            }
            ReturnObject<List<Roles>> roleResponse = await _admin.GetRoles();
            if (!roleResponse.IsSuccess)
            {
                ModelState.AddModelError("Message", roleResponse.ErrorMessage != null ? roleResponse.ErrorMessage : "Something Went Wrong Fetching Roles!");
            }
            else
            {
                ViewBag.Roles = roleResponse.Result;
            }
            return View();
        }

        [Authorize(Policy = "assignPermissions")]
        [HttpPost("assignPermissions")]
        public async Task<IActionResult> AssignPermission(AssignPermissionModel model)
        {
            #region Passing Data To View
            ViewBag.Permissions = HomeController.Permissions;
            ViewBag.Permissions = HomeController.Permissions;
            ReturnObject<List<Permissions>> permissionsResponse = await _admin.GetPermissions();
            if (!permissionsResponse.IsSuccess)
            {
                ModelState.AddModelError("Message", permissionsResponse.ErrorMessage != null ? permissionsResponse.ErrorMessage : "Something Went Wrong Fetching Permissions!");
            }
            else
            {
                ViewBag.AllPermissions = permissionsResponse.Result;
            }
            ReturnObject<List<Roles>> roleResponse = await _admin.GetRoles();
            if (!roleResponse.IsSuccess)
            {
                ModelState.AddModelError("Message", roleResponse.ErrorMessage != null ? roleResponse.ErrorMessage : "Something Went Wrong Fetching Roles!");
            }
            else
            {
                ViewBag.Roles = roleResponse.Result;
            }
            #endregion
            if (!ModelState.IsValid)
            {
                return View();
            }

            ReturnObject<List<RoleHasPermissions>> updateResponse = await _admin.ChangePermissions(model);
            if(!updateResponse.IsSuccess)
            {
                ModelState.AddModelError("Message", roleResponse.ErrorMessage != null ? roleResponse.ErrorMessage : "Something Went Wrong Updating Permissions!");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Manager Functionality

        [Authorize(Policy = "assignTasks")]
        [HttpGet("assignTasks")]
        public IActionResult AddTask()
        {
            ViewBag.Permissions = HomeController.Permissions;
            return View();
        }

        [Authorize(Policy = "assignTasks")]
        [HttpPost("assignTasks")]
        public async Task<IActionResult> AddTask(AddTaskModel model)
        {
            ViewBag.Permissions = HomeController.Permissions;
            if (!ModelState.IsValid)
            {
                return View();
            }
            ReturnObject<string> insertTaskResponse = await _manager.AddTask(model);
            if (!insertTaskResponse.IsSuccess)
            {
                ModelState.AddModelError("Message", insertTaskResponse.ErrorMessage != null ? insertTaskResponse.ErrorMessage : "Something Went Wrong Inserting Tasks!");
                return View();
            }
            return RedirectToAction("editTask", "Services");
        }

        [Authorize(Policy = "editTask")]
        [HttpGet("editTask")]
        public async Task<IActionResult> EditTask(int? id)
        {
            ViewBag.Permissions = HomeController.Permissions;
            if (id != null)
            {
                ReturnObject<AddTaskModel> oneTaskResponse = await _manager.GetOneTasks(id);
                if (!oneTaskResponse.IsSuccess)
                {
                    ModelState.AddModelError("Message", oneTaskResponse.ErrorMessage != null ? oneTaskResponse.ErrorMessage : "Something Went Wrong Inserting Tasks!");
                    return View();
                }
                ViewBag.TaskData = oneTaskResponse.Result;
                return View("AddTask");
            }
            else
            {
                ReturnObject<List<AddTaskModel>> taskResponse = await _manager.GetAllTasks();
                if(!taskResponse.IsSuccess)
                {
                    ModelState.AddModelError("Message", taskResponse.ErrorMessage != null ? taskResponse.ErrorMessage : "Something Went Wrong Getting Tasks Details!");
                    return View();
                }
                ViewBag.AllTasks = taskResponse.Result;
                return View();
            }
        }

        [Authorize(Policy = "editTask")]
        [HttpPost("editTask")]
        public async Task<IActionResult> EditTask(AddTaskModel model)
        {
            #region Pass data To View
            ViewBag.Permissions = HomeController.Permissions;
            ReturnObject<AddTaskModel> oneTaskResponse = await _manager.GetOneTasks(model.Id);
            if (!oneTaskResponse.IsSuccess)
            {
                ModelState.AddModelError("Message", oneTaskResponse.ErrorMessage != null ? oneTaskResponse.ErrorMessage : "Something Went Wrong Inserting Tasks!");
                return View();
            }
            ViewBag.TaskData = oneTaskResponse.Result;
            #endregion

            ReturnObject<string> updateResponse = await _manager.Updatetask(model);
            if(!updateResponse.IsSuccess)
            {
                ModelState.AddModelError("updateMessage", updateResponse.ErrorMessage != null ? updateResponse.ErrorMessage : "Something Went Wrong Updating Tasks!");
                return View("AddTask");
            }

            return RedirectToAction("editTask");
        }

        [Authorize(Policy = "assignUsers")]
        [HttpGet("assignUsers")]
        public async Task<IActionResult> AssignUsers(int? id, string? level, int? hasSubTask)
        {
            ViewBag.Permissions = HomeController.Permissions;
            if(level == "task" && hasSubTask == 0)
            {
                ReturnObject<List<UserWithTaskModel>>? userSubTaskResponse = await _manager.GetUserWithTask(id);
                if (!userSubTaskResponse.IsSuccess)
                {
                    ModelState.AddModelError("Message", userSubTaskResponse.ErrorMessage != null ? userSubTaskResponse.ErrorMessage : "Something Went Wrong Getting Tasks Details!");
                    return View();
                }
                ViewBag.UserDetails = userSubTaskResponse.Result;
                return View();
            }
            else if(level == "task" && hasSubTask == 1)
            {
                ReturnObject<List<SubTask>> subTaskResponse = await _manager.GetSubTasks(id);
                if(!subTaskResponse.IsSuccess)
                {
                    ModelState.AddModelError("Message", subTaskResponse.ErrorMessage != null ? subTaskResponse.ErrorMessage : "Something Went Wrong Getting Tasks Details!");
                    return View();
                }
                ViewBag.SubTaskData = subTaskResponse.Result;
            }
            else if(level == "subTask")
            {
                ReturnObject<List<UserWithTaskModel>>? userSubTaskResponse = await _manager.GetUserWithSubtask(id);
                if (!userSubTaskResponse.IsSuccess)
                {
                    ModelState.AddModelError("Message", userSubTaskResponse.ErrorMessage != null ? userSubTaskResponse.ErrorMessage : "Something Went Wrong Getting Tasks Details!");
                    return View();
                }
                ViewBag.UserDetails = userSubTaskResponse.Result;
                return View();
            }
            else
            {
                ReturnObject<List<AddTaskModel>> taskResponse = await _manager.GetAllTasks();
                if (!taskResponse.IsSuccess)
                {
                    ModelState.AddModelError("Message", taskResponse.ErrorMessage != null ? taskResponse.ErrorMessage : "Something Went Wrong Getting Tasks Details!");
                    return View();
                }
                ViewBag.AllTasks = taskResponse.Result;
            }
            return View();
        }

        #endregion

        #region User Functionality

        [HttpGet("reqToChangeStatus")]
        public async Task<IActionResult> ReqToChangeStatus(int? subId)
        {
            ViewBag.Permissions = HomeController.Permissions;
            if(subId != null)
            {
                ReturnObject<string> statusResponse = await _manager.GetSubStatus(subId);
                if(!statusResponse.IsSuccess)
                {
                    ModelState.AddModelError("Message", statusResponse.ErrorMessage != null ? statusResponse.ErrorMessage : "Something Went Wrong Getting Tasks Status!");
                    return View();
                }
                ViewBag.Status = statusResponse.Result;
                ViewBag.subId = subId; 
                return View();
            }
            else
            {
                int userId = Convert.ToInt32(_contextAccesor.HttpContext?.Request.Cookies["userId"]);
                ReturnObject<List<SubTask>> subTaskResponse = await _manager.GetUserHasSubTasks(userId);
                if(!subTaskResponse.IsSuccess)
                {
                    ModelState.AddModelError("Message", subTaskResponse.ErrorMessage != null ? subTaskResponse.ErrorMessage : "Something Went Wrong Getting Tasks Details!");
                    return View();
                }
                ViewBag.SubTasks = subTaskResponse.Result;
                ViewBag.UserId = userId;
                return View();
            }
        }

        #endregion
    }
}
