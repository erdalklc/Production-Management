#pragma checksum "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\Fason\_PartialSiparisListesiDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d408ec754ec504f9e7785ac852a91efd1bc4ad4c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Fason.Views_Fason__PartialSiparisListesiDetail), @"mvc.1.0.view", @"/Views/Fason/_PartialSiparisListesiDetail.cshtml")]
namespace EPM_Web.Models.Fason
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
#line 1 "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\_ViewImports.cshtml"
using EPM_Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d408ec754ec504f9e7785ac852a91efd1bc4ad4c", @"/Views/Fason/_PartialSiparisListesiDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Fason__PartialSiparisListesiDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<int>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n<div class=\"dx-card wide-card\">\r\n    ");
#nullable restore
#line 6 "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\Fason\_PartialSiparisListesiDetail.cshtml"
Write(Html.DevExtreme().DataGrid<EPM.Fason.Dto.Fason.SIPARIS_LISTESI_DETAIL> ().ID("edgridDetail")
    .ShowBorders(false)
    .DataSource(
        ob=>ob.Mvc()
        .Controller("Fason").LoadParams(new { ENTEGRATION_HEADER_ID = Model})
        .LoadAction("SiparisListesiDetailLoad")
        .Key("ID")
    ) 
    .Export(e =>
    {
        e.Enabled(true);
    })
    .RemoteOperations(true)
    .FilterRow(f => f.Visible(true))
    .AllowColumnReordering(false)
    .FocusedRowEnabled(true)
    .FocusedRowIndex(0)
    .ColumnAutoWidth(true)
    .ColumnChooser(ch => { ch.Mode(GridColumnChooserMode.Select); ch.Enabled(true); })
    .ShowRowLines(true)
    .ShowColumnLines(true)
    .ColumnHidingEnabled(false)
    .Scrolling(ob => ob.Mode(GridScrollingMode.Virtual))
    .Height("300px")
    .ShowBorders(true)
    .Summary(ob=>ob.TotalItems(items=> {
        items.AddFor(m => m.QUANTITY).Alignment(HorizontalAlignment.Right).SummaryType(SummaryType.Sum).CustomizeText(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral("function(data){return data.value}");
    PopWriter();
}
));
    }))
    .Columns(columns => { 
         
        columns.AddFor(m => m.ID).Visible(false).AllowEditing(false).AllowExporting(true);
        columns.AddFor(m => m.PRODUCT_SIZE).AllowExporting(true);
        columns.AddFor(m => m.QUANTITY).AllowEditing(true).AllowExporting(true); 
    }).OnInitNewRow(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral(" function(e) { e.data.HEADER_ID =");
#nullable restore
#line 39 "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\Fason\_PartialSiparisListesiDetail.cshtml"
                                                       Write(Model);

#line default
#line hidden
#nullable disable
    WriteLiteral(" ; }");
    PopWriter();
}
))
    );

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>");
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
