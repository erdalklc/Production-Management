#pragma checksum "D:\Projects\Eren Production Management\EPM.Web\Views\Uretim\_PartialUretimOnayliListeFiltreleDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8aee1b2c3f27d76c99fd7f2ac87bdf008ac3c5cd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Uretim.Views_Uretim__PartialUretimOnayliListeFiltreleDetail), @"mvc.1.0.view", @"/Views/Uretim/_PartialUretimOnayliListeFiltreleDetail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8aee1b2c3f27d76c99fd7f2ac87bdf008ac3c5cd", @"/Views/Uretim/_PartialUretimOnayliListeFiltreleDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Uretim__PartialUretimOnayliListeFiltreleDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<int>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Projects\Eren Production Management\EPM.Web\Views\Uretim\_PartialUretimOnayliListeFiltreleDetail.cshtml"
  
    var editable = Convert.ToBoolean(ViewData["CanEditUretim"]); 

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"dx-card wide-card\">\r\n    ");
#nullable restore
#line 7 "D:\Projects\Eren Production Management\EPM.Web\Views\Uretim\_PartialUretimOnayliListeFiltreleDetail.cshtml"
Write(Html.DevExtreme().DataGrid<EPM.Production.Dto.Production.DetailList> ().ID("edgridDetail")
    .ShowBorders(false)
    //.DataSource(ob=>ob.Array().Data(Model).Key("ID"))
    .DataSource(
        ob=>ob.Mvc()
        .Controller("Uretim").LoadParams(new { HEADER_ID =Model})
        .LoadAction("UretimOnayliDetailLoad")
        .UpdateAction("UretimOnayliDetailUpdate")
        .DeleteAction("UretimOnayliDetailDelete")
        .InsertAction("UretimOnayliDetailInsert")
        .Key("ID")
    )
    .OnExporting("exportingDetail")
    .Export(e =>
    {
        e.Enabled(true);
    })
    .RemoteOperations(true)
    .HeaderFilter(x => x.Visible(true))
    .FilterRow(f => f.Visible(true))
    .Editing(ob => ob.Mode(GridEditMode.Cell).StartEditAction(GridStartEditAction.DblClick).AllowUpdating(editable).AllowDeleting(editable).AllowAdding(true))
    .FocusedRowEnabled(true)
    .FocusedRowIndex(0)
    .ColumnAutoWidth(true)
    .ColumnChooser(ch => { ch.Mode(GridColumnChooserMode.Select); ch.Enabled(true); })
    .ShowRowLines(true)
    .ShowColumnLines(true)
    .ColumnHidingEnabled(false)
    .Paging(ob => ob.PageSize(50).Enabled(true))
    .Height("calc(100vh - 200px)")
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

        columns.Add().Width(60).Type(GridCommandColumnType.Buttons).Buttons(btn => {
            btn.Add().Icon("fa fa-history").OnClick("LogSearchDetail");
            btn.Add().Name(GridColumnButtonName.Delete);
        });
        columns.AddFor(m => m.ID).Visible(false).AllowEditing(false).AllowExporting(true);
        columns.AddFor(m => m.HEADER_ID).SetCellValue(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral(" function(e){ return ");
#nullable restore
#line 48 "D:\Projects\Eren Production Management\EPM.Web\Views\Uretim\_PartialUretimOnayliListeFiltreleDetail.cshtml"
                                                                             Write(Model);

#line default
#line hidden
#nullable disable
    WriteLiteral("}");
    PopWriter();
}
)).Visible(false).AllowExporting(true);
        columns.AddFor(m => m.MARKET).Lookup(look => look.DataSource(d => d.Mvc().Controller("Uretim").LoadAction("GetMarkets").Key("ID").LoadParams(new { all = false })).DisplayExpr("ADI").ValueExpr("ID")).AllowExporting(true);
        columns.AddFor(m => m.PRODUCT_SIZE).AllowExporting(true);
        columns.AddFor(m => m.QUANTITY).AllowEditing(true).AllowExporting(true); 
    }).OnInitNewRow(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral(" function(e) { e.data.HEADER_ID =");
#nullable restore
#line 52 "D:\Projects\Eren Production Management\EPM.Web\Views\Uretim\_PartialUretimOnayliListeFiltreleDetail.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<int> Html { get; private set; }
    }
}
#pragma warning restore 1591
