#pragma checksum "E:\Eren\Eren Production Management\EPM.Web\Views\Uretim\_PartialUretimliListeDikeyFiltrele.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "29cb2aed3b04cd9cde74c342bd3dcf0def0cc139"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Uretim.Views_Uretim__PartialUretimliListeDikeyFiltrele), @"mvc.1.0.view", @"/Views/Uretim/_PartialUretimliListeDikeyFiltrele.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"29cb2aed3b04cd9cde74c342bd3dcf0def0cc139", @"/Views/Uretim/_PartialUretimliListeDikeyFiltrele.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Uretim__PartialUretimliListeDikeyFiltrele : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EPM.Production.Dto.Production.UretimOnayliListe>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<div class=\"dx-card wide-card\">\r\n    ");
#nullable restore
#line 5 "E:\Eren\Eren Production Management\EPM.Web\Views\Uretim\_PartialUretimliListeDikeyFiltrele.cshtml"
Write(Html.DevExtreme().DataGrid<EPM.Production.Dto.Production.VerticalList> ().ID("edgridHeader")
    .ShowBorders(false)
    .DataSource(
        ob=>ob.Mvc().Controller("Uretim")
        .LoadParams(Model)
        .LoadAction("UretimListeDikeyLoad")
        .Key("DETAIL_ID")
    )
    .OnExporting("exportingDikeyListe")
    .Export(e =>
    {
        e.Enabled(true);
    })
    .FilterRow(f => f.Visible(true))
    .FocusedRowEnabled(true)
    .FocusedRowIndex(0) 
    .AllowColumnResizing(true) 
    .Selection(s => s.Mode(SelectionMode.Single))
    .ColumnAutoWidth(true)
    .Pager(p=>p.Visible(true).ShowInfo(true).ShowPageSizeSelector(true).ShowNavigationButtons(true))
    .ShowBorders(true)
    //.Scrolling(ob=>ob.Mode(GridScrollingMode.Standard))
    .Paging(p=> {
        p.Enabled(true);
        p.PageSize(50);
    })
    .Height("calc(100vh - 200px)")
    .ShowColumnLines(true)
    .ColumnChooser(ch => { ch.Mode(GridColumnChooserMode.Select);ch.Enabled(true); })
    .ShowColumnHeaders(true)
    .ColumnMinWidth(60)
    .Summary(ob=>ob.TotalItems(items=> {
    items.AddFor(m => m.QUANTITY).Alignment(HorizontalAlignment.Right).SummaryType(SummaryType.Sum).CustomizeText(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral("function(data){return data.value}");
    PopWriter();
}
)); 
        items.AddFor(m => m.MODEL).Alignment(HorizontalAlignment.Right).SummaryType(SummaryType.Count).CustomizeText(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral("function(data){return data.value}");
    PopWriter();
}
));
    }))
    //.Scrolling(ob=>ob.Mode(GridScrollingMode.Virtual))
    .Columns(columns =>
    {
        //columns.Add().Width(50).Type(GridCommandColumnType.Buttons).Buttons(btn => btn.Add().Icon("fa fa-history").OnClick("LogSearchHeader"));
        columns.AddFor(m => m.ID).Width(60).AllowEditing(false);
        columns.AddFor(m => m.APPROVAL_STATUS).Width(110).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetApprovalStatusList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.BRAND).Width(110).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetBrandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.SUB_BRAND).Width(110).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetSubBrandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.SEASON).Lookup(look => look.DataSource(d => d.Mvc().Controller("Uretim").LoadAction("GetSeasonList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.MODEL);
        columns.AddFor(m => m.COLOR);
        columns.AddFor(m => m.COLLECTION_TYPE).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetCollectionTypes").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.ROYALTY);
        columns.AddFor(m => m.TEMA);
        columns.AddFor(m => m.ANA_TEMA);
        columns.AddFor(m => m.PRODUCT_GROUP).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetProductGroupList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.FABRIC_TYPE).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetFabricList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.PRODUCTION_TYPE).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetProductionTypes").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.ORDER_TYPE).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetOrderList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.RECIPE).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetRecipeList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.BAND_ID).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetBandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.DEADLINE).Caption("1. DEADLINE").AllowEditing(false).DataType(GridColumnDataType.Date).Format("dd.MM.yyyy").Alignment(HorizontalAlignment.Right);
        columns.AddFor(m => m.DEADLINE_2).Caption("2. DEADLINE").DataType(GridColumnDataType.Date).Format("dd.MM.yyyy").Alignment(HorizontalAlignment.Right);
        columns.AddFor(m => m.DEADLINE_3).Caption("3. DEADLINE").Visible(true).DataType(GridColumnDataType.Date).Format("dd.MM.yyyy").Alignment(HorizontalAlignment.Right);
        columns.AddFor(m => m.DEADLINE_4).Caption("4. DEADLINE").Visible(true).DataType(GridColumnDataType.Date).Format("dd.MM.yyyy").Alignment(HorizontalAlignment.Right);
        columns.AddFor(m => m.SHIPMENT_DATE).DataType(GridColumnDataType.Date).Format("dd.MM.yyyy").Alignment(HorizontalAlignment.Right);
        columns.AddFor(m => m.MARKET).Lookup(look => look.DataSource(d => d.Mvc().Controller("Uretim").LoadAction("GetMarkets").Key("ID").LoadParams(new { all = false })).DisplayExpr("ADI").ValueExpr("ID")).AllowExporting(true);
        columns.AddFor(m => m.PRODUCT_SIZE);
        columns.AddFor(m => m.QUANTITY);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EPM.Production.Dto.Production.UretimOnayliListe> Html { get; private set; }
    }
}
#pragma warning restore 1591
