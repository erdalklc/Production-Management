#pragma checksum "D:\Projects\Eren Production Management\EPM.Web\Views\Uretim\_PartialUretimDetailLog.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b9b38a9150a06d9aceca1e8c56c09fc6bceae011"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Uretim.Views_Uretim__PartialUretimDetailLog), @"mvc.1.0.view", @"/Views/Uretim/_PartialUretimDetailLog.cshtml")]
namespace EPM_Web.Models.Uretim
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
#line 1 "D:\Projects\Eren Production Management\EPM.Web\Views\_ViewImports.cshtml"
using EPM_Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Projects\Eren Production Management\EPM.Web\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b9b38a9150a06d9aceca1e8c56c09fc6bceae011", @"/Views/Uretim/_PartialUretimDetailLog.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Uretim__PartialUretimDetailLog : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<int>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Projects\Eren Production Management\EPM.Web\Views\Uretim\_PartialUretimDetailLog.cshtml"
Write(Html.DevExtreme().DataGrid<EPM.Dto.Loglar.LOG_EPM_MASTER_PRODUCTION_D>
    ().ID("edBantBitisList")
    .ShowBorders(false)
    .DataSource(
    ob => ob.Mvc().Controller("Uretim")
    .LoadParams(new { DETAIL_ID = Model})
    .LoadAction("UretimOnayliDetailLogLoad")
    .Key("ID")
    )
    .FocusedRowEnabled(true)
    .FocusedRowIndex(0)
    .Selection(s => s.Mode(SelectionMode.Single))
    .ColumnAutoWidth(true)
    .ShowBorders(true)
    .Scrolling(ob => ob.Mode(GridScrollingMode.Virtual))
    .Height("calc((100vh / 2))")
    .ShowColumnLines(true)
    .ShowColumnHeaders(true)
    .ColumnMinWidth(60)
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<int> Html { get; private set; }
    }
}
#pragma warning restore 1591