#pragma checksum "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "54e2e43a9facbc01fbfe588ef06814167cc75c3d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimTakip.Views_UretimTakip__PartialUretimTakipFason), @"mvc.1.0.view", @"/Views/UretimTakip/_PartialUretimTakipFason.cshtml")]
namespace EPM_Web.Models.UretimTakip
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
#nullable restore
#line 4 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
using EPM.Fason.Dto.Fason;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"54e2e43a9facbc01fbfe588ef06814167cc75c3d", @"/Views/UretimTakip/_PartialUretimTakipFason.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimTakip__PartialUretimTakipFason : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<PRODUCTION_LIST_V>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
            WriteLiteral("<div id=\"formContainer\"> \r\n    <div class=\"dx-card wide-card\">\r\n        ");
#nullable restore
#line 7 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
    Write(Html.DevExtreme().DataGrid<EPM.Fason.Dto.Fason.PRODUCTION_LIST_V> ().ID("edgridProcess")
    .DataSource( Model)
    .Export(e =>
    {
        e.Enabled(true);
    })
    .RemoteOperations(true)
    .FocusedRowEnabled(true)
    .FocusedRowIndex(0)
    .ShowRowLines(true)
    .ShowColumnLines(true)
    .ColumnHidingEnabled(false)
    .Height("calc(100vh - 450px)")
    .ShowBorders(true)
    .Columns(columns => {
        columns.AddFor(m => m.DETAIL_ID).Width(70).Visible(false).AllowSorting(false).SortIndex(0).SortOrder(SortOrder.Desc).AllowEditing(false).AllowExporting(true);
        columns.AddFor(m => m.HEADER_ID).Width(70).Visible(false).AllowSorting(false).AllowEditing(false).AllowExporting(true);
        columns.AddFor(m => m.ENTEGRATION_ID).Width(70).AllowSorting(false).Visible(false).AllowEditing(false).AllowExporting(true);
        columns.AddFor(m => m.PROCESS_NAME).Width(170).AllowSorting(false).AllowExporting(true);
        columns.AddFor(m => m.STATUS_EX).Width(170).AllowSorting(false).AllowExporting(true);
        columns.AddFor(m => m.START_DATE).Width(170).AllowSorting(false).Format("dd.MM.yyyy").AllowExporting(true);
        columns.AddFor(m => m.END_DATE).Width(170).AllowSorting(false).Format("dd.MM.yyyy").AllowExporting(true);
    })
    );

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<PRODUCTION_LIST_V>> Html { get; private set; }
    }
}
#pragma warning restore 1591
