﻿using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;

namespace _3.BusinessLogic.Services.Interface;

public interface IEmployeeService : IBaseService<EmployeeViewModel>
{
    Task<IEnumerable<EmployeeVMResp>> GetItemsAsync();
    Task<int> GetCountAsync();
    Task<IEnumerable<EmployeeViewModel>> GetItemsWithoutUserAsync();
    Task<EmployeeViewModel?> CreateAsync(EmployeeVMCreateFR request);
    Task<EmployeeViewModel?> UpdateAsync(string id, EmployeeVMDefaultFR request);
    Task<EmployeeViewModel?> UpdateProfile(string id, EmployeeVMDefaultFR request);
    Task<EmployeeViewModel?> UpdateVipAsync(string id, EmployeeVMUpdateVipFR request);
    Task<EmployeeViewModel?> DeleteAsync(string id);
    Task<EmployeeViewModel> GetProfileAsync();
    Task<ReturnalModel> ImportAsync(EmployeeVMImportFR request);
    Task<ReturnalModel> GetHeadEmployeesAsync();
}
