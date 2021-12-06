#pragma checksum "D:\Projects\Eren Production Management\EPM.Web\Views\Uretim\_PartialUretimOnayliListeFiltrele.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "65a128c827e9ac8b247a875a5706d28ecdd0bf67"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Uretim.Views_Uretim__PartialUretimOnayliListeFiltrele), @"mvc.1.0.view", @"/Views/Uretim/_PartialUretimOnayliListeFiltrele.cshtml")]
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
#nullable restore
#line 3 "D:\Projects\Eren Production Management\EPM.Web\Views\Uretim\_PartialUretimOnayliListeFiltrele.cshtml"
using EPM.Dto.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"65a128c827e9ac8b247a875a5706d28ecdd0bf67", @"/Views/Uretim/_PartialUretimOnayliListeFiltrele.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Uretim__PartialUretimOnayliListeFiltrele : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Tuple<List<EPM.Production.Dto.Production.MasterList>, EPM.Production.Dto.Production.UretimOnayliListe>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "D:\Projects\Eren Production Management\EPM.Web\Views\Uretim\_PartialUretimOnayliListeFiltrele.cshtml"
  
    var editable = Convert.ToBoolean(ViewData["CanEditUretim"]);
    var onayDegistirir = Convert.ToBoolean(ViewData["OnayliKullanici"]); 


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"dx-card wide-card\">\r\n    ");
#nullable restore
#line 12 "D:\Projects\Eren Production Management\EPM.Web\Views\Uretim\_PartialUretimOnayliListeFiltrele.cshtml"
Write(Html.DevExtreme().DataGrid<EPM.Production.Dto.Production.MasterList>().ID("edgridHeader")
                    .ShowBorders(false)
                    .DataSource(Model.Item1,"ID" )
                    .OnExporting("exportingHeader")
                    .Export(e =>
                    {
                        e.Enabled(true);
                    })
                    .DataSourceOptions(ob=>ob.ReshapeOnPush(false))
     .OnRowUpdating("UpdateMasterRow")
     .OnRowInserting("InsertMasterRow") 
    .FocusedRowEnabled(true)
    .FilterRow(x=>x.Visible(true))
    .FocusedRowIndex(0)
    .Selection(s => s.Mode(SelectionMode.Single))
    .ColumnAutoWidth(true)
    .ShowBorders(true)
    .HeaderFilter(x=>x.Visible(true))
    .Editing(ob => ob.Mode(GridEditMode.Cell).StartEditAction(GridStartEditAction.DblClick).AllowUpdating(editable).AllowDeleting(editable).AllowAdding(false))
    .ColumnResizingMode(ColumnResizingMode.Widget)
    .AllowColumnResizing(true)
    .AllowColumnReordering(true)
    //.Paging(ob=>ob.PageSize(25).Enabled(true))
    .Height("calc(100vh - 200px)")
    .ShowColumnLines(true)
    .ColumnChooser(ch => { ch.Mode(GridColumnChooserMode.Select);ch.Enabled(true); })
    .ShowColumnHeaders(true)
    .OnFocusedRowChanged("gridFocusedRowChanged") 
    .Summary(
    ob=>ob.TotalItems(
        items=> {
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
    .Columns(columns =>
    {
         
            columns.Add().Width(50).Type(GridCommandColumnType.Buttons).Buttons(btn =>
            {
                btn.Add().Icon("fa fa-history").OnClick("LogSearchHeader");
            }); 

        columns.AddFor(m => m.ID).SortOrder(SortOrder.Asc).Width(60).AllowEditing(false);
        columns.AddFor(m => m.STATUS).Width(110).AllowEditing(onayDegistirir).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetStatusList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.APPROVAL_STATUS).Width(110).AllowEditing(onayDegistirir).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetApprovalStatusList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.FLAG_ID).Width(110).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetFlagList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
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
        columns.AddFor(m => m.DEADLINE).Caption("1. DEADLINE").DataType(GridColumnDataType.Date).Format("dd.MM.yyyy").Alignment(HorizontalAlignment.Right);
        columns.AddFor(m => m.DEADLINE_2).Caption("2. DEADLINE").DataType(GridColumnDataType.Date).Format("dd.MM.yyyy").Alignment(HorizontalAlignment.Right);
        columns.AddFor(m => m.DEADLINE_3).Caption("3. DEADLINE").Visible(true).DataType(GridColumnDataType.Date).Format("dd.MM.yyyy").Alignment(HorizontalAlignment.Right);
        columns.AddFor(m => m.DEADLINE_4).Caption("4. DEADLINE").Visible(true).DataType(GridColumnDataType.Date).Format("dd.MM.yyyy").Alignment(HorizontalAlignment.Right);
        columns.AddFor(m => m.SHIPMENT_DATE).DataType(GridColumnDataType.Date).Format("dd.MM.yyyy").Alignment(HorizontalAlignment.Right);
        columns.AddFor(m => m.QUANTITY);
        columns.Add().AllowEditing(false).Width(20);;
    })
    .OnEditingStart(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral(@"

    function(e){
        if(e.data.ID && e.data.ID>0){
            if(e.column.name==""DEADLINE"" && e.data.DEADLINE!=null){
                e.cancel=true;
            }
            if(e.column.name==""DEADLINE_2"" && e.data.DEADLINE_2!=null){
                e.cancel=true;
            }
            if(e.column.name==""DEADLINE_3"" && (e.data.DEADLINE_2==null || e.data.DEADLINE_3!=null)){
                e.cancel=true;
            }
            if(e.column.name==""DEADLINE_4"" && (e.data.DEADLINE_2==null || e.data.DEADLINE_3==null || e.data.DEADLINE_4!=null)){
                e.cancel=true;
            }
        }else{
             if(e.column.name==""DEADLINE_2""){
                e.cancel=true;
            }
            if(e.column.name==""DEADLINE_3""){
                e.cancel=true;
            }
            if(e.column.name==""DEADLINE_4""){
                e.cancel=true;
            }
        }

    }

    ");
    PopWriter();
}
))
    .OnInitNewRow(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral("\r\n    function(e){ }\r\n    ");
    PopWriter();
}
))
    .OnCellPrepared(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral(@"
    function(e){
         if(e.rowType==""data"" && e.column.dataField==""FLAG_ID""){
            if(e.data.FLAG_ID==1){
                e.cellElement.css(""background-color"", ""indianred"");
                e.cellElement.css(""color"", ""white"");
            }else if(e.data.FLAG_ID==3){
                e.cellElement.css(""background-color"", ""yellow"");
            }else{

                e.cellElement.css(""background-color"", ""green"");
                e.cellElement.css(""color"", ""white"");
            }
        }
    }


    ");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Tuple<List<EPM.Production.Dto.Production.MasterList>, EPM.Production.Dto.Production.UretimOnayliListe>> Html { get; private set; }
    }
}
#pragma warning restore 1591
