using AutoMapper;
using FrameWork.Common.ExMethods;
using FrameWork.Common.Utility.Paging;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SinaShop.Application.AccessLevel;
using SinaShop.Application.Contract.ApplicationDTO.AccessLevel;
using SinaShop.Application.Contract.PresentationDTO.ViewInputs;
using SinaShop.Application.Contract.PresentationDTO.ViewModels;
using SinaShop.Domain.Users.AccessLevelAgg.Contract;
using SinaShop.WebApp.Common.Utilities.MessageBox;

namespace SinaShop.WebApp.Pages.Admin.AccessLevel
{
    public class ListAccessLevelModel : PageModel
    {
        private readonly FrameWork.Infrastructure.ILogger _logger;
        private readonly IMapper _Mapper;
        private readonly IMsgBox _MsgBox;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IAccessLevelApplication _AccessLevelApplication;
        private readonly IAccessLevelRepository _AccessLevelRepository;
        public ListAccessLevelModel( IMapper mapper, IMsgBox msgBox, IServiceProvider serviceProvider, IAccessLevelApplication accessLevelApplication, IAccessLevelRepository accessLevelRepository)
        {
            _Mapper = mapper;
            _MsgBox = msgBox;
            _ServiceProvider = serviceProvider;
            _AccessLevelApplication = accessLevelApplication;
            _AccessLevelRepository = accessLevelRepository;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest Request)
        {
            var qData = await _AccessLevelApplication.GetAccessLevelForAdminAsync(new InpGetAccessLevelForAdmin
            {
                Name = null,
                Page = (short)Request.Page,
                Take = (short)Request.PageSize
            });

            var DataGrid = qData.LstItems.ToDataSourceResult(Request);
            DataGrid.Total = (int)qData.PageData.CountAllItem;
            DataGrid.Data = qData.LstItems;
            return new JsonResult(DataGrid);
        }

        public viListAccessLevelModel input { get; set; }
        public vmListAccessLevelModel Data { get; set; }
    }
}
