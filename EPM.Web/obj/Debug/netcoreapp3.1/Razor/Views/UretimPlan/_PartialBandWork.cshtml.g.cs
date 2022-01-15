#pragma checksum "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimPlan\_PartialBandWork.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f0bd9dc9843ff7aeb18147804cb36ff0254a8c57"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimPlan.Views_UretimPlan__PartialBandWork), @"mvc.1.0.view", @"/Views/UretimPlan/_PartialBandWork.cshtml")]
namespace EPM_Web.Models.UretimPlan
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\_ViewImports.cshtml"
using EPM_Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f0bd9dc9843ff7aeb18147804cb36ff0254a8c57", @"/Views/UretimPlan/_PartialBandWork.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimPlan__PartialBandWork : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimPlan\_PartialBandWork.cshtml"
   
    var canEditPlan = Convert.ToBoolean(ViewData["CanEditPlan"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div  class=\"dx-card wide-card\" style=\"margin:4px\">\r\n    <div style=\"width:100%;height:30px;align-items:center;justify-content:center;text-align:center;padding:5px\">BANT ÇALIŞAN SAYILARI</div>\r\n ");
#nullable restore
#line 8 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimPlan\_PartialBandWork.cshtml"
Write(Html.DevExtreme().DataGrid<EPM.Plan.Dto.Plan.EpmBandWorkModel> ().ID("edgridWorker")
    .ShowBorders(false)
    .DataSource(
        ob=>ob.Mvc().Controller("UretimPlan") 
        .LoadAction("BandWorkersLoad")
        .UpdateAction("BandWorkersUpdate")
        .LoadParams(new JS("getLoadParameters()"))
        .Key("ROW_ID")
    )
    .Height("calc(100vh - 160px)")
    .OnRowUpdating("onWorkUpdating")
    .FocusedRowEnabled(true)
    .FocusedRowIndex(0)
    .Selection(s => s.Mode(SelectionMode.Single))
    .ColumnAutoWidth(true)
    .ShowBorders(true)
    .Scrolling(ob=>ob.Mode(GridScrollingMode.Virtual))
    .Editing(ob => ob.Mode(GridEditMode.Cell).AllowUpdating(canEditPlan).AllowDeleting(false).AllowAdding(false))
    .ShowColumnLines(true)
    .Sorting(s=>s.Mode(GridSortingMode.None))
    .ShowColumnHeaders(true) 
    .Scrolling(ob=>ob.Mode(GridScrollingMode.Virtual))
    .Columns(columns => {
        columns.AddFor(m => m.ID).Width(60).AllowSorting(false).AllowEditing(false).Visible(false);
        columns.AddFor(m => m.BAND_ID).SortOrder(SortOrder.Asc).Width(110).Caption("BAND").AllowEditing(false).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetBandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.PRODUCT_GROUP_NAME).SortOrder(SortOrder.Asc).Width(110).AllowEditing(false);
        columns.AddFor(m => m.WEEK).SortOrder(SortOrder.Asc).Width(110).AllowEditing(false);
        columns.AddFor(m => m.WORKER).AllowSorting(false).Width(110).AllowEditing(true);
        columns.Add().AllowEditing(false).Width(20);;
    })
    );

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
