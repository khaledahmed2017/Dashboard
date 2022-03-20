using AutoMapper;
using DemoKhaled.BL.interfaces;
using DemoKhaled.BL.Mapper;
using DemoKhaled.BL.Models;
using DemoKhaled.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demokhaled.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Fields
        private readonly IEmployeeRep empolyee;
        private readonly IMapper mapper;
        #endregion

        #region ctr
        public EmployeeController(IEmployeeRep empolyee, IMapper mapper)
        {
            this.empolyee = empolyee;
            this.mapper = mapper;
        }
        #endregion
        #region Api's

        [HttpGet]
        [Route("~/Api/Employee/GetEmplyees")]
        public IActionResult Get()
        {
            try
            {
                var data = empolyee.get();
                var model = mapper.Map<IEnumerable<EmployeeVm>>(data);
                return Ok(new ApiResponse<IEnumerable<EmployeeVm>>()//ApiResponse is class in Mapper.BL
                {
                    code = "200",
                    status = "OK",
                    Message = "Data Retriaved",
                    data = model,
                    Error = ""
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<EmployeeVm>()
                {
                    code = "404",
                    status = "Not Found",
                    Message = "data Not Found",
                    Error = ex.Message
                });
            }

        }
        [HttpGet]
        [Route("~/Api/Employee/GetEmplyeeById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = empolyee.GetById(id);
                var model = mapper.Map<EmployeeVm>(data);
                return Ok(new ApiResponse<EmployeeVm>()
                {
                    code = "200",
                    status = "OK",
                    Message = "Data Retriaved By Id",
                    data = model
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    code = "404",
                    Message = ex.Message,
                    status = "Not Found ID",
                    Error = "Not Found"
                });
            }

        }
        [HttpPost]
        [Route("~/Api/Employee/PostEmployee")]
        public IActionResult createEmployee(EmployeeVm emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = mapper.Map<Employee>(emp);
                    var data = empolyee.create(model);
                    return Ok(new ApiResponse<Employee>()
                    {
                        code = "201",
                        status = "Created",
                        Message = "Employee added",
                        data = data
                    });
                }
                return Ok(new ApiResponse<string>()// this is else part of if
                {
                    code = "201",
                    status = "Not Valid",
                    Message = "inValid data"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    code = "404",
                    Message = ex.Message,
                    status = "Not Found ID",
                    Error = "Not Found"
                });
            }
        }
        [HttpPut]
        [Route("~/Api/Employee/EditEmployee")]
        public IActionResult UpdataEmployee(EmployeeVm emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = mapper.Map<Employee>(emp);
                    var data = empolyee.update(model);
                    return Ok(new ApiResponse<Employee>()
                    {
                        code = "201",
                        status = "Edited",
                        Message = "Employee modified",
                        data = data
                    });
                }
                return Ok(new ApiResponse<string>()// this is else part of if
                {
                    code = "201",
                    status = "Not Valid",
                    Message = "inValid data"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    code = "404",
                    Message = ex.Message,
                    status = "Not Found ID",
                    Error = "Not Found"
                });
            }
        }
        [HttpDelete]
        [Route("~/Api/Employee/DeleteEmployee")]
        public IActionResult Delete(EmployeeVm emp)
        {
            try
            {
                var model = mapper.Map<Employee>(emp);
                var data = empolyee.delete(model);
                return Ok(new ApiResponse<Employee>()
                {
                    code = "200",
                    status = "OK",
                    Message = "Employee deleted",
                    data = model
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    code = "404",
                    Message = ex.Message,
                    status = "Not Found object",
                    Error = "Not Found object"
                });
            }

        }
        #endregion



    }
    }
