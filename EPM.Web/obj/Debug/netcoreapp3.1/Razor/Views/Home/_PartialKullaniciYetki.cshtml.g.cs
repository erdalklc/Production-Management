#pragma checksum "E:\Eren\Eren Production Management\EPM.Web\Views\Home\_PartialKullaniciYetki.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e26034bbd0ab509cae519ad07d1d5ebb66bde0a1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Home.Views_Home__PartialKullaniciYetki), @"mvc.1.0.view", @"/Views/Home/_PartialKullaniciYetki.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e26034bbd0ab509cae519ad07d1d5ebb66bde0a1", @"/Views/Home/_PartialKullaniciYetki.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Home__PartialKullaniciYetki : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"dx-card wide-card\">\r\n    <div style=\"width:470px;display:inline-block;float:left\">\r\n        ");
#nullable restore
#line 3 "E:\Eren\Eren Production Management\EPM.Web\Views\Home\_PartialKullaniciYetki.cshtml"
    Write(Html.DevExtreme().DataGrid<EPM.Core.Models.ADAccount> ().ID("edGridKullaniciListe")
    .ShowBorders(false)
    .DataSource(
        ob=>ob.Mvc().Controller("Home")
        .LoadAction("GetUserList")
        .Key("USER_CODE")
    )
    .FilterRow(f => f.Visible(true))
    .FocusedRowEnabled(true)
    .FocusedRowIndex(0)
    .Selection(s => s.Mode(SelectionMode.Single))
    .ColumnAutoWidth(true)
    .ShowBorders(true)
    .Height(450)
    .ShowColumnLines(true)
    .ShowColumnHeaders(true)
    .ColumnMinWidth(30)
    .Scrolling(ob=>ob.Mode(GridScrollingMode.Virtual))
    .Columns(columns => {
        columns.AddFor(m => m.USER_CODE).Width(80);
        columns.AddFor(m => m.USER_NAME).Caption("DESCIRIPTION").Width(120);
        columns.AddFor(m => m.USER_EMAIL).Caption("E-MAIL").Width(250);
    })
    .OnFocusedRowChanged("gridKullaniciFocusedRowChanged")
    );

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    </div>
    <div style=""width:400px;display:inline-block;float:left"" id=""edKullaniciYetki"">

    </div>

    <div style=""width:220px;display:inline-block;float:left"" id=""edKullaniciFabricTypes"">

    </div>
    <div style=""width:220px;display:inline-block;float:left"" id=""edKullaniciProductionTypes"">

    </div>
    <div style=""width:220px;display:inline-block;float:left"" id=""edKullaniciBrands"">

    </div>
</div>
 ");
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
