#pragma checksum "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\Admin\_PartialKullaniciProductionTypes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "077e968444b6112915d63b79e61f39e855a0db6e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Admin.Views_Admin__PartialKullaniciProductionTypes), @"mvc.1.0.view", @"/Views/Admin/_PartialKullaniciProductionTypes.cshtml")]
namespace EPM_Web.Models.Admin
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"077e968444b6112915d63b79e61f39e855a0db6e", @"/Views/Admin/_PartialKullaniciProductionTypes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin__PartialKullaniciProductionTypes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<string>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\Admin\_PartialKullaniciProductionTypes.cshtml"
Write(Html.DevExtreme().DataGrid<EPM.Admin.Dto.Admin.KullaniciProductionTypes>
    ().ID("edGridKullanicipProductionTypes")
    .ShowBorders(false)
    .DataSource(
    ob=>ob.Mvc().Controller("Admin")
    .LoadAction("GetUserProductionTypes")
    .UpdateAction("UserProductionTypeUpdate")
    .OnBeforeSend(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral(" function(actionName,e){if(actionName ==\'update\'){e.data.USER_CODE=\'");
#nullable restore
#line 9 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\Admin\_PartialKullaniciProductionTypes.cshtml"
                                                                                        Write(Model);

#line default
#line hidden
#nullable disable
    WriteLiteral("\';}}");
    PopWriter();
}
))
    .LoadParams(new { USER_CODE = Model })
    .Key("PRODUCTION_TYPE_ID")
    )
    .FocusedRowEnabled(true)
    .FocusedRowIndex(0)
    .Height(450)
    .Selection(s => s.Mode(SelectionMode.Single))
    .ShowBorders(true)
    .Editing(ob => ob.Mode(GridEditMode.Cell).AllowUpdating(true))
    .AllowColumnResizing(true)
    .ShowColumnLines(true)
    .ShowColumnHeaders(true)
    .ColumnMinWidth(30)
    .Sorting(sr=>sr.Mode(GridSortingMode.None))
    .Scrolling(ob=>ob.Mode(GridScrollingMode.Virtual))
    .Columns(columns =>
    {
        columns.AddFor(m => m.ISACTIVE).Width(80).AllowEditing(true);
        columns.AddFor(m => m.PRODUCTION_NAME).Width(120).AllowEditing(false);
    })
    );

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<string> Html { get; private set; }
    }
}
#pragma warning restore 1591
