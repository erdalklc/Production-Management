#pragma checksum "E:\Eren\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFiltrele.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e1cc3b74d8ff1c247567d4415a603b4d373ab35b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimTakip.Views_UretimTakip__PartialUretimTakipFiltrele), @"mvc.1.0.view", @"/Views/UretimTakip/_PartialUretimTakipFiltrele.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e1cc3b74d8ff1c247567d4415a603b4d373ab35b", @"/Views/UretimTakip/_PartialUretimTakipFiltrele.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimTakip__PartialUretimTakipFiltrele : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EPM.Track.Dto.Track.TrackList_Filter>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n\r\n");
#nullable restore
#line 6 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFiltrele.cshtml"
  
    var editable = new EPM.Core.Helpers.MenuHelper().CanUserEditUretim(this.Context);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"dx-card wide-card\">\r\n    ");
#nullable restore
#line 11 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFiltrele.cshtml"
Write(Html.DevExtreme().DataGrid<EPM.Track.Dto.Track.UretimTakipListesi>().ID("edUretimTakipGrid")
    .ShowBorders(false)
    .DataSource(
        ob => ob.Mvc().Controller("UretimTakip")
        .LoadParams(Model)
        .LoadAction("UretimTakiplLoad")
        .Key("ID")
    )
    .FilterRow(f => f.Visible(true))
    .FocusedRowEnabled(true)
    .FocusedRowIndex(0)
    .Selection(s => s.Mode(SelectionMode.Single))
    .ColumnAutoWidth(true)
    .ShowBorders(true)
    .HeaderFilter(a=>a.Visible(true))
    .Scrolling(ob => ob.Mode(GridScrollingMode.Virtual))
    .Height("calc(100vh - 150px)")
    .ColumnChooser(ch=>ch.Enabled(true).Mode(GridColumnChooserMode.Select))
    .ShowColumnLines(true)
    .ShowColumnHeaders(true)
    .ColumnMinWidth(60)
    .AllowColumnResizing(true)
    .Columns(columns => {
        if (editable)
        {
            columns.Add().Width(50).Type(GridCommandColumnType.Buttons).Buttons(btn => btn.Add().Icon("fa fa-edit").OnClick("EditClicked")); 
        }
        columns.Add().Width(50).Type(GridCommandColumnType.Buttons).Buttons(btn => btn.Add().Icon("fa fa-search").OnClick("DetailSearch"));
        columns.AddFor(m => m.ID).Width(100).AllowEditing(false);
        columns.AddFor(m => m.TAKIP_NO).Width(120).AllowEditing(false);
        columns.AddFor(m => m.APPROVAL_STATUS).Visible(false).Width(110).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetApprovalStatusList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.BRAND).Visible(false).Width(110).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetBrandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.SEASON).Lookup(look => look.DataSource(d => d.Mvc().Controller("Uretim").LoadAction("GetSeasonList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.MODEL);
        columns.AddFor(m => m.COLOR);
        columns.AddFor(m => m.COLLECTION_TYPE).Visible(false).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetCollectionTypes").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.ROYALTY).Visible(false);
        columns.AddFor(m => m.TEMA).Visible(false);
        columns.AddFor(m => m.ANA_TEMA).Visible(false);
        columns.AddFor(m => m.PRODUCT_GROUP).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetProductGroupList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.FABRIC_TYPE).Visible(false).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetFabricList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.PRODUCTION_TYPE).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetProductionTypes").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.ORDER_TYPE).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetOrderList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.RECIPE).Width(140).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetRecipeList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.BAND_ID).Visible(false).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetBandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.DEADLINE).DataType(GridColumnDataType.Date).Format("dd.MM.yyyy");
        columns.AddFor(m => m.SHIPMENT_DATE).DataType(GridColumnDataType.Date).Format("dd.MM.yyyy").Visible(false);
        columns.AddFor(m => m.QUANTITY);
        columns.AddFor(m => m.PROCESS_INFO).Caption("SÜREÇ DURUMU");
        columns.AddFor(m => m.MUSTBE_STATE).Caption("OLMASI GEREKEN DURUMU");
    })
    .Summary(sm =>
    {
        sm.TotalItems(t =>
        {
        t.AddFor(ob => ob.MODEL).SummaryType(SummaryType.Count).CustomizeText(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral("function(data){return data.value}");
    PopWriter();
}
));
            t.AddFor(ob => ob.QUANTITY).SummaryType(SummaryType.Sum).CustomizeText(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral("function(data){return data.value}");
    PopWriter();
}
));
        });
    })
    .OnCellPrepared("edUretimTakipGridCellPrepared")
    );

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n<script>\r\n\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EPM.Track.Dto.Track.TrackList_Filter> Html { get; private set; }
    }
}
#pragma warning restore 1591
