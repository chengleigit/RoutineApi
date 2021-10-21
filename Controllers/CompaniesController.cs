using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.DTO;
using Routine.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController:ControllerBase
    {
        public readonly ICompanyRepository _companyRepository;
        public readonly IMapper _mapper;
        public CompaniesController(ICompanyRepository companyRepository,IMapper mapper)
        {
            _companyRepository = companyRepository ??
                throw new ArgumentNullException(nameof(companyRepository));
            _mapper=mapper?? throw new ArgumentNullException(nameof(mapper ));
        }


        /// <summary>
        /// 获取所有公司
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("companies")]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetCompanies() 
        {
            var companies = await _companyRepository.GetCompaniesAsync();

            var companyDTOs = _mapper.Map<IEnumerable<CompanyDTO>>(companies);

            return Ok(companyDTOs); 
        }

        /// <summary>
        /// 根据ID获取公司
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{companyId}")]
        public async Task<ActionResult<CompanyDTO>> GetCompany(Guid companyId)
        {
            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CompanyDTO>(company));
        }
    }
}
