#pragma checksum "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b5368b5d3a46437d76b0fd2c1a6e918a6d0065e9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimIzle.Views_UretimIzle_UretimIzle), @"mvc.1.0.view", @"/Views/UretimIzle/UretimIzle.cshtml")]
namespace EPM_Web.Models.UretimIzle
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5368b5d3a46437d76b0fd2c1a6e918a6d0065e9", @"/Views/UretimIzle/UretimIzle.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimIzle_UretimIzle : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
  
    ViewData["Title"] = "UretimIzle";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<style>
    #dateBoxProduct{
        margin:auto;
    }
    .productDetailCardHeader {
        font-weight: bold;
        font-size: 24px;
        justify-content: center;
        text-align: center;
        padding-top: 5px;
        padding-bottom: 10px;
    }

    .detailItemEx {
        width: 49%;
        text-align: left;
        display: inline-block;
        font-size: 13px;
    }
    .detailItemValue {
        width: 49%;
        text-align: center;
        display: inline-block;
        font-size: 13px; 
        font-weight:bold;
    }
</style>


<div id=""form-container"">
    <div class=""dx-card wide-card"">
        ");
#nullable restore
#line 36 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
    Write(Html.DevExtreme().Form<EPM.Production.Monitoring.Dto.Models.FilterModel>()
        .ID("edOnayliListe").ShowValidationSummary(true)
        .Items(items =>
        {
            items.AddGroup().ColCount(8).Items(groupItems =>
            {
                groupItems.AddSimpleFor(m => m.BRAND).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetBrandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.SEASON).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetSeasonList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.MODEL).Editor(ob => ob.TextBox().ID("edModel")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.COLOR).Editor(ob => ob.TextBox().ID("edColor")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.ORDER_TYPE).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetOrderList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.PRODUCT_GROUP).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetProductGroupList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.BAND).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetBandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddButton().HorizontalAlignment(HorizontalAlignment.Left).VerticalAlignment(VerticalAlignment.Center)
                  .ButtonOptions(b => b.Text("FİLTRELE")
                      .Type(ButtonType.Default)
                      .Icon("fas fa-sign-in-alt")
                      .Width("100%")
                      .OnClick("UretimIzleFilter")
                      .UseSubmitBehavior(true)
              );

            });



        })
        .FormData(Model).ShowValidationSummary(false)
    );

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


        <div id=""popupDetay""></div>
    </div>

    <div class=""dx-card wide-card"" style=""width: 100%; height: calc(100vh - 128px); "">
        <div class=""dx-card wide-card"" id=""weekDetailCard"" style=""width:225px;height:100%;display:inline-block;margin:0 2px"">
            <div id=""weekDetail""></div>
        </div>
        <div class=""dx-card wide-card"" id=""productDetailCard"" style=""width: 200px; height: 100%; display: inline-block; margin: 0 2px"">
            <div id=""productDetail""></div>
        </div>
        <div class=""dx-card wide-card"" style=""width: calc(100% - 485px);overflow-y:auto; height: 100%; display: inline-block; "">
            <div class=""dx-card wide-card"" id=""productPlan"" style=""width: 49%; height: 200px; display: inline-block; margin: 0 2px"">

            </div>
            <div class=""dx-card wide-card"" id=""productBar"" style=""width: 49%; height: 200px; display: inline-block; margin: 0 2px"">

            </div>

            <div class=""dx-card wide-card"" id=""released");
            WriteLiteral(@"Tasnif"" style=""width: 49%; height: 250px; display: inline-block; margin: 0 2px"">

            </div>
            <div class=""dx-card wide-card"" id=""releasedBant"" style=""width: 49%; height: 250px; display: inline-block; margin: 0 2px "">

            </div>
            <div class=""dx-card wide-card"" id=""releasedKalite"" style=""width: 49%; height: 250px; display: inline-block; margin: 0 2px "">

            </div>
            <div class=""dx-card wide-card"" id=""releasedKaliteG"" style=""width: 49%; height: 250px; display: inline-block; margin: 0 2px "">

            </div>
            <div class=""dx-card wide-card"" id=""releasedProductList"" style=""width: 100%; height: 450px; display: inline-block; margin: 0 2px "">
                ");
#nullable restore
#line 98 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
            Write(Html.DevExtreme().DataGrid<EPM.Production.Monitoring.Dto.Models.ProductionModel>().ID("edUretimTakipGrid")
    .ShowBorders(false)
    .FilterRow(f => f.Visible(true))
    .FocusedRowEnabled(true)
    .FocusedRowIndex(0)
    .Selection(s => s.Mode(SelectionMode.Single))
    .ColumnAutoWidth(true)
    .ShowBorders(true)
    .HeaderFilter(a=>a.Visible(true))
    .Paging(o => o.PageSize(50))
    .Height(450)
    .ShowColumnLines(true)
    .ShowColumnHeaders(true)
    .ColumnMinWidth(60)
    .AllowColumnResizing(true)
    .Columns(columns => { 
        columns.Add().Width(50).Type(GridCommandColumnType.Buttons).Buttons(btn => btn.Add().Icon("fa fa-search").OnClick("DetailSearch"));
        columns.AddFor(m => m.ID).Width(100).AllowEditing(false); 
        columns.AddFor(m => m.APPROVAL_STATUS).Visible(false).Width(110).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetApprovalStatusList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.BRAND).Visible(false).Width(110).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetBrandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.SEASON).Lookup(look => look.DataSource(d => d.Mvc().Controller("Uretim").LoadAction("GetSeasonList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));
        columns.AddFor(m => m.MODEL);
        columns.AddFor(m => m.COLOR);
        columns.AddFor(m => m.COMPANY_NAME);
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
            WriteLiteral(@"
            </div>
        </div>
    </div>



</div>


<script>
    function DetailSearch(e) {
        var width = """";
        var height = """";
        if (e.row.data.PRODUCTION_TYPE == 1) {
            width = ""calc(100% - 20px)"";
            height = ""calc(100% - 10px)"";
            title = ""SATIN ALMA DETAYI"";
        }
        else {
            width = ""calc(100% / 2)"";
            height = ""calc(100% - 250px)"";
            title = ""FASON SİPARİŞ SÜREÇ DURUMLARI"";
        }
        var header_id = e.row.data.ID;
        tempHeaderId = header_id;
        var popupOptions = {
            contentTemplate: function () {
                var data = {
                    HEADER_ID: tempHeaderId,
                    RECIPE: e.row.data.RECIPE
                };
                return  $.ajax({
                    type: ""GET"",
                    url: '");
#nullable restore
#line 184 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
                     Write(Url.Action("_PartialUretimTakipDetay", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                    data: data,
                    async: false,
                    success: function (status) {
                    }
                }).responseText;
            },
            width: function () { return width; },
            height: function () { return height; },
            showTitle: true,
            title: title,
            dragEnabled: true,
            closeOnOutsideClick: true
        };

        $(""#popupDetay"").dxPopup(popupOptions).dxPopup(""instance"").show();
    }
     function gridSatinAlmaFocusedRowChanged(e) {
        console.log(e.row.data);
        setTimeout(function () {
            $(""#edProcessInformations"").load('");
#nullable restore
#line 204 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
                                         Write(Url.Action("_PartialProcessInformations", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', {\r\n                PO_HEADER_ID: e.row.data.PO_HEADER_ID,\r\n                HEADER_ID: e.row.data.HEADER_ID,\r\n                DETAIL_ID: e.row.data.DETAIL_TAKIP_NO\r\n            });\r\n\r\n\r\n            $(\"#edOrmeTab\").load(\'");
#nullable restore
#line 211 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
                             Write(Url.Action("_PartialEgemenOrmeList", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', {\r\n                TAKIP_NO: e.row.data.TAKIP_NO,\r\n                DETAIL_TAKIP_NO: e.row.data.DETAIL_TAKIP_NO\r\n            });\r\n\r\n            $(\"#edKumasDepoTab\").load(\'");
#nullable restore
#line 216 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
                                  Write(Url.Action("_PartialKumasDepoList", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', {\r\n                ITEM_ID: e.row.data.ITEM_ID,\r\n                PO_HEADER_ID: e.row.data.PO_HEADER_ID\r\n            });\r\n            $(\"#edKesimTab\").load(\'");
#nullable restore
#line 220 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
                              Write(Url.Action("_PartialKesimFoyuList", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', {\r\n                ITEM_ID: e.row.data.ITEM_ID,\r\n                PO_HEADER_ID: e.row.data.PO_HEADER_ID,\r\n                RENK_DETAY: e.row.data.RENKDETAY,\r\n            });\r\n            $(\"#edBantTab\").load(\'");
#nullable restore
#line 225 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
                             Write(Url.Action("_PartialBantList", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', {\r\n                ITEM_ID: e.row.data.ITEM_ID,\r\n                PO_HEADER_ID: e.row.data.PO_HEADER_ID,\r\n                RENK_DETAY: e.row.data.RENKDETAY,\r\n            });\r\n            $(\"#edKaliteTab\").load(\'");
#nullable restore
#line 230 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
                               Write(Url.Action("_PartialKaliteList", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"', {
                ITEM_ID: e.row.data.ITEM_ID,
                PO_HEADER_ID: e.row.data.PO_HEADER_ID,
                RENK_DETAY: e.row.data.RENKDETAY,
            });
        }, 200);


    }
    function edUretimTakipGridCellPrepared() {

    }
    var weekDetail, productDetail;
    function ClearChart() {
        try { $(""#productBar"").dxChart(""instance"").option(""dataSource"", null); } catch (e) { }
        try { $(""#releasedTasnif"").dxChart(""instance"").option(""dataSource"", null); } catch (e) { }
        try { $(""#releasedBant"").dxChart(""instance"").option(""dataSource"", null); } catch (e) { }
        try { $(""#releasedKalite"").dxChart(""instance"").option(""dataSource"", null); } catch (e) { }
        try { $(""#releasedKaliteG"").dxChart(""instance"").option(""dataSource"", null); } catch (e) { }
        try { $(""#productPlan"").dxDataGrid(""instance"").option(""dataSource"", null); } catch (e) { }
        try { $(""#edUretimTakipGrid"").dxDataGrid(""instance"").option(""dataSource"", null); } catch (e) {");
            WriteLiteral(@" }
    }
    function ClearGrid() {
        $(""#productBar"").dxChart(""instance"").option(""dataSource"", null);
    }
    function ClearBottom() {
        $(""#hfKesim"").html(0);
        $(""#hfTasnif"").html(0);
        $(""#hfBant"").html(0);
        $(""#hfKalite"").html(0);
    }
    function ClearRealTime() {
        $(""#trKesim"").html(0);
        $(""#trTasnif"").html(0);
        $(""#trBant"").html(0);
        $(""#trKalite"").html(0);
    }
    function UretimIzleFilter() {
        ClearChart();
        ClearBottom();
        try { $(""#weekDetail"").dxList(""instance"").option(""dataSource"", null); } catch (e) { }
        try { $(""#productDetail"").dxList(""instance"").option(""dataSource"", null); } catch (e) { }
        var data = {
            BRAND: jQuery(""[name=BRAND]"").val(),
            SEASON: jQuery(""[name=SEASON]"").val(),
            BAND: jQuery(""[name=BAND]"").val(),
            MODEL: jQuery(""[name=MODEL]"").val(),
            COLOR: jQuery(""[name=COLOR]"").val(),
            ORDER_TYPE:");
            WriteLiteral(" jQuery(\"[name=ORDER_TYPE]\").val(),\r\n            PRODUCT_GROUP: jQuery(\"[name=PRODUCT_GROUP]\").val()\r\n        };\r\n        $.ajax({\r\n            type: \"POST\",\r\n            url: \'");
#nullable restore
#line 283 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
             Write(Url.Action("UretimIzleFilter", "UretimIzle"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            timeout: 180000,
            success: function (income, status, xmlRequest) {

                weekDetail= $(""#weekDetail"").dxList({
                    dataSource: new DevExpress.data.DataSource({
                        store: new DevExpress.data.ArrayStore({
                            key: ""ID"",
                            data: income
                        }),
                        paginate: false
                    }),
                    height: ""100%"",
                    itemTemplate: function (data) {
                        var html = ""<div style='text-align:center;padding-right:15px;'> <span style='font-weight:bold;font-size:11px'>"" + data.WEEK + "". Hafta </span><br> <span style='font-weight:bold;color:indianred'>  ("" + moment(data.START_DATE).format(""DD.MM.YYYY"") + "" - "" + moment(data.END_DATE).format(""DD.MM.YYYY"") + "")</span></div>"";
                        return html;
   ");
            WriteLiteral(@"                 },
                    showSelectionControls: true,
                    selectionMode: ""all"",
                    pageLoadMode: ""scrollBottom"",
                    onSelectionChanged: function (data) {
                        haftaModel = weekDetail.dxList(""instance"").option(""selectedItems"");
                        try { $(""#productDetail"").dxList(""instance"").option(""dataSource"", null); } catch (e) { }
                        ClearChart();
                        GetProductDetail();
                    }
                });

            }, beforeSend: function () {
                getLoadPanelInstance().show();
            }


        }).always(function () {
            getLoadPanelInstance().hide();
        });
    }

    var haftaModel = null;
    var productModel = null;
    function GetProductDetail() {
        ClearChart();
        ClearBottom();

        if (haftaModel == null || haftaModel.length == 0)
            return;
        data = {
            mod");
            WriteLiteral(@"el: haftaModel,
            filterModel: {
                BRAND: jQuery(""[name=BRAND]"").val(),
                SEASON: jQuery(""[name=SEASON]"").val(),
                BAND: jQuery(""[name=BAND]"").val(),
                MODEL: jQuery(""[name=MODEL]"").val(),
                COLOR: jQuery(""[name=COLOR]"").val(),
                ORDER_TYPE: jQuery(""[name=ORDER_TYPE]"").val(),
                PRODUCT_GROUP: jQuery(""[name=PRODUCT_GROUP]"").val()
            }
        }
        $.ajax({
            type: ""POST"",
            url: '");
#nullable restore
#line 345 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
             Write(Url.Action("GetProductList", "UretimIzle"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            timeout: 180000,
            success: function (income, status, xmlRequest) {
                productDetail = $(""#productDetail"").dxList({
                    dataSource: new DevExpress.data.DataSource({
                        store: new DevExpress.data.ArrayStore({
                            key: ""ID"",
                            data: income
                        }),
                        paginate: false
                    }),
                    height: ""100%"",
                    //searchEnabled: true,
                    searchExpr: ""MODEL"",
                    itemTemplate: function (data) {
                        var html = ""<div> <span style='font-weight:bold;'>"" + data.MODEL + ""</span>.<span style='font-weight:bold'>"" + data.COLOR + ""</span></div>"";
                        return html;
                    },
                    showSelectionControls: true,
                ");
            WriteLiteral(@"    //pageLoadMode: ""scrollBottom"",
                    selectionMode: ""all"",
                    onSelectionChanged: function (data) {
                        productModel = productDetail.dxList(""instance"").option(""selectedItems"");
                        GetProductionDetails();
                    }
                });

            }, beforeSend: function () {
                getLoadPanelInstance().show();
            }


        }).always(function () {
            getLoadPanelInstance().hide();
        });
    }


    function GetProductionDetails() {
        ClearChart();
        ClearBottom();
        if (haftaModel == null || haftaModel.length == 0)
            return;
        if (productModel == null || productModel.length == 0)
            return;

        data = {
            haftaModel: haftaModel,
            productModel: productModel,
            filterModel: {
                BRAND: jQuery(""[name=BRAND]"").val(),
                SEASON: jQuery(""[name=SEASON]"").val(");
            WriteLiteral(@"),
                BAND: jQuery(""[name=BAND]"").val(),
                MODEL: jQuery(""[name=MODEL]"").val(),
                COLOR: jQuery(""[name=COLOR]"").val(),
                ORDER_TYPE: jQuery(""[name=ORDER_TYPE]"").val(),
                PRODUCT_GROUP: jQuery(""[name=PRODUCT_GROUP]"").val()
            }
        }
        console.log(data);
        $.ajax({
            type: ""POST"",
            url: '");
#nullable restore
#line 409 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
             Write(Url.Action("GetProductionDetails", "UretimIzle"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: JSON.stringify( data),
            contentType: 'application/json',
            timeout: 180000,
            success: function (income, status, xmlRequest) {
                var totalPLAN = 0;
                var totalORDER = 0;
                for (var i = 0; i < income.Item1.length; i++) {
                    totalORDER += income.Item1[i].ORDER_QUANTITY;
                    totalPLAN += income.Item1[i].QUANTITY;
                }
                var bar = [{
                    arg: ""SİPARİŞ"",
                    val: totalORDER
                }, {
                    arg: ""HAFTALIK PLAN"",
                    val: totalPLAN
                }, {
                    arg: ""KESİM"",
                    val: income.Item2.KESIM
                }, {
                    arg: ""TASNİF"",
                    val: income.Item2.TASNIF
                }, {
                    arg: ""BANT"",
                    val: income.Item2.BANT
                }, {
                    arg");
            WriteLiteral(@": ""KALİTE"",
                    val: income.Item2.KALITE
                    }];
                var barKesim = [];
                var barTasnif = [];
                var barBant = [];
                var barKalite = [];
                var barKaliteG = [];
                var kaliteGSum = 0;
                for (var i = 0; i < income.Item3.length; i++) {
                    var marketReleased = income.Item3[i];
                    if (marketReleased.TYPE == ""KESİM"")
                        barKesim.push({ arg: marketReleased.PAZAR_ADI, val: marketReleased.MIKTAR });
                    if (marketReleased.TYPE == ""TASNİF"")
                        barTasnif.push({ arg: marketReleased.PAZAR_ADI, val: marketReleased.MIKTAR });
                    if (marketReleased.TYPE == ""BANT"")
                        barBant.push({ arg: marketReleased.PAZAR_ADI, val: marketReleased.MIKTAR });
                    if (marketReleased.TYPE == ""KALİTE"")
                        barKalite.push({ arg: marketReleas");
            WriteLiteral(@"ed.PAZAR_ADI, val: marketReleased.MIKTAR });
                    if (marketReleased.TYPE == ""KALİTEG"") {
                        barKaliteG.push({ arg: marketReleased.PAZAR_ADI, val: marketReleased.MIKTAR });
                        kaliteGSum += marketReleased.MIKTAR;
                    }
                }
                bar.push({ arg: ""KALİTE GERÇEKLEŞEN"", val: kaliteGSum });


                try { $(""#edUretimTakipGrid"").dxDataGrid(""instance"").option(""dataSource"", income.Item4); } catch (e) { }
                $(""#releasedTasnif"").dxChart({
                    commonSeriesSettings: {
                        label: {
                            visible: true,
                            format: {
                                type: ""fixedPoint"",
                                precision: 0
                            }
                        }
                    },
                    dataSource: barTasnif,
                    palette: ""Material"",
                    paletteExt");
            WriteLiteral(@"ensionMode: ""Extrapolate"",
                    legend: {
                        visible: false
                    },
                    series: {
                        type: ""bar""
                    }, tooltip: {
                        enabled: true
                    },
                    argumentAxis: {
                        tickInterval: 5,
                        label: {
                            format: {
                                type: ""decimal""
                            }, font: {
                                color: ""black"",
                                size: 8
                            }
                        }

                    },
                    title: {
                        text: ""Tasnif"",
                        font: {
                            color: ""black"",
                            size: 14
                        }
                    },
                });
                $(""#releasedBant"").dxChart({
               ");
            WriteLiteral(@"     commonSeriesSettings: {
                        label: {
                            visible: true,
                            format: {
                                type: ""fixedPoint"",
                                precision: 0
                            }
                        }
                    },
                    dataSource: barBant,
                    palette: ""Office"",
                    paletteExtensionMode: ""Extrapolate"",
                    legend: {
                        visible: false
                    },
                    series: {
                        type: ""bar""
                    }, tooltip: {
                        enabled: true
                    },
                    argumentAxis: {
                        tickInterval: 5,
                        label: {
                            format: {
                                type: ""decimal""
                            }, font: {
                                color: ""black"",
     ");
            WriteLiteral(@"                           size: 8
                            }
                        }
                    },
                    title: {
                        text: ""Bant"",
                        font: {
                            color: ""black"",
                            size: 14
                        }
                    },
                });
                $(""#releasedKalite"").dxChart({
                    commonSeriesSettings: {
                        label: {
                            visible: true,
                            format: {
                                type: ""fixedPoint"",
                                precision: 0
                            }
                        }
                    },
                    dataSource: barKalite,
                    palette: ""Pastel"",
                    paletteExtensionMode: ""Extrapolate"",
                    legend: {
                        visible: false
                    },
                    s");
            WriteLiteral(@"eries: {
                        type: ""bar""
                    }, tooltip: {
                        enabled: true
                    },
                    argumentAxis: {
                        tickInterval: 5,
                        label: {
                            format: {
                                type: ""decimal""
                            }, font: {
                                color: ""black"",
                                size: 8
                            }
                        }
                    },
                    title: {
                        text: ""Kalite"",
                        font: {
                            color: ""black"",
                            size: 14
                        }
                    },
                });
                $(""#releasedKaliteG"").dxChart({
                    commonSeriesSettings: {
                        label: {
                            visible: true,
                            format:");
            WriteLiteral(@" {
                                type: ""fixedPoint"",
                                precision: 0
                            }
                        }
                    },
                    dataSource: barKaliteG,
                    palette: ""Bright"",
                    paletteExtensionMode: ""Extrapolate"",
                    legend: {
                        visible: false
                    },
                    series: {
                        type: ""bar""
                    }, tooltip: {
                        enabled: true
                    },
                    argumentAxis: {
                        tickInterval: 5,
                        label: {
                            format: {
                                type: ""decimal""
                            }, font: {
                                color: ""black"",
                                size: 8
                            }
                        }
                    },
                    ti");
            WriteLiteral(@"tle: {
                        text: ""Kalite Gerçekleşen"",
                        font: {
                            color: ""black"",
                            size: 14
                        }
                    },
                });
                $(""#productBar"").dxChart({
                    title: {
                        text: ""Üretim Durumu"",
                        font: {
                            color: ""black"",
                            size:14
                        }
                    },
                    commonSeriesSettings: {
                        label: {
                            visible: true,
                            format: {
                                type: ""fixedPoint"",
                                precision: 0
                            }
                        }
                    },
                    dataSource: bar,
                    palette: ""soft"",
                    legend: {
                        visible: fals");
            WriteLiteral(@"e
                    },
                    series: {
                        type: ""bar""
                    }, tooltip: {
                        enabled: true
                    },
                    argumentAxis: {
                        tickInterval: 5,
                        label: {
                            format: {
                                type: ""decimal""
                            }, font: {
                                color: ""black"",
                                size: 8
                            }
                        }
                    }
                });
                $(""#hfKesim"").html(income.Item2.KESIM);
                $(""#hfTasnif"").html(income.Item2.TASNIF);
                $(""#hfBant"").html(income.Item2.BANT);
                $(""#hfKalite"").html(income.Item2.KALITE);
                $(""#productPlan"").dxDataGrid({
                    dataSource: income.Item1,
                    keyExpr: 'ID',
                    selection: {
   ");
            WriteLiteral(@"                     mode: ""single""
                    },
                    hoverStateEnabled: true,
                    showBorders: true,
                    columns: [""MARKET_NAME"", ""QUANTITY"", ""ORDER_QUANTITY""],
                    showBorders: true,
                    summary: {
                        totalItems: [{
                            column: ""QUANTITY"",
                            summaryType: ""sum""
                        }, {
                            column: ""ORDER_QUANTITY"",
                                summaryType: ""sum""
                            }]
                    }
                });

            }, beforeSend: function () {
                getLoadPanelInstance().show();
            }


        }).always(function () {
            getLoadPanelInstance().hide();
        });
    }


</script>
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
