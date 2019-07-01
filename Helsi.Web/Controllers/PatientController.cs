using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Helsi.DomainLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Helsi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPatientService _patientService;

        public PatientController(IMapper mapper, IPatientService patientService)
        {
            _mapper = mapper;
            _patientService = patientService;
        }

        // GET: api/Patient
        [HttpGet]
        public IActionResult Get()
        {
            var patients = _patientService.GetPatients();

            return new JsonResult(patients);
        }

        // GET: api/Patient/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var patient = _patientService.GetPatient(id);

            return new JsonResult(patient);
        }

        // POST: api/Patient
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Patient/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _patientService.Deactivate(id);
        }
    }
}
