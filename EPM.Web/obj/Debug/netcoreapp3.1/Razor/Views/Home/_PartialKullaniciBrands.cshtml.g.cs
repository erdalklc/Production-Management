#pragma checksum "E:\Eren\Eren Production Management\EPM.Web\Views\Home\_PartialKullaniciBrands.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d4467336dddfbbb89709cb04a0aef2cabbf27da3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Home.Views_Home__PartialKullaniciBrands), @"mvc.1.0.view", @"/Views/Home/_PartialKullaniciBrands.cshtml")]
namespace EPM_Web.Models.Home
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
#line 1 "E:\Eren\Eren Production Management\EPM.Web\Views\_ViewImports.cshtml"
using EPM_Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\Eren\Eren Production Management\EPM.Web\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d4467336dddfbbb89709cb04a0aef2cabbf27da3", @"/Views/Home/_PartialKullaniciBrands.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Home__PartialKullaniciBrands : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<string>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\Eren\Eren Production Management\EPM.Web\Views\Home\_PartialKullaniciBrands.cshtml"
Write(Html.DevExtreme().DataGrid<EPM.Admin.Dto.Admin.KullaniciMarka>
    ().ID("edGridKullaniciBrands")
    .ShowBorders(false)
    .DataSource(
    ob=>ob.Mvc().Controller("Home")
    .LoadAction("GetUserBrands")
    .UpdateAction("UserBrandsUpdate")
    .OnBeforeSend(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral(" function(actionName,e){if(actionName ==\'update\'){e.data.USER_CODE=\'");
#nullable restore
#line 9 "E:\Eren\Eren Production Management\EPM.Web\Views\Home\_PartialKullaniciBrands.cshtml"
                                                                                        Write(Model);

#line default
#line hidden
#nullable disable
    WriteLiteral("\';}}");
    PopWriter();
}
))
    .LoadParams(new { USER_CODE = Model })
    .Key("BRAND_ID")
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
        columns.AddFor(m => m.BRAND_NAME).Width(120).AllowEditing(false);
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
