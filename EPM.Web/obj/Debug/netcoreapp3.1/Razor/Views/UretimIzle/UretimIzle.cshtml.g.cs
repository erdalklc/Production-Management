#pragma checksum "E:\Eren\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d217b1e881e17d3432fb6db624fd6eb2f4b15926"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d217b1e881e17d3432fb6db624fd6eb2f4b15926", @"/Views/UretimIzle/UretimIzle.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimIzle_UretimIzle : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
  
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
#line 36 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
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

    <div class=""dx-card wide-card"" style=""width: 100%; height: calc(100vh - 135px); "">
        <div class=""dx-card wide-card"" id=""weekDetailCard"" style=""width:225px;height:100%;display:inline-block;margin:0 2px"">
            <div id=""weekDetail""></div>
        </div>
        <div class=""dx-card wide-card"" id=""productDetailCard"" style=""width: 200px; height: 100%; display: inline-block; margin: 0 2px"">
            <div id=""productDetail""></div>
        </div>
        <div class=""dx-card wide-card"" style=""width: calc(100% - 485px); height: 100%; display: inline-block; "">
            <div class=""dx-card wide-card"" id=""productPlan"" style=""width: 49%; height: 200px; display: inline-block; margin: 0 2px"">

            </div>
            <div class=""dx-card wide-card"" id=""productBar"" style=""width: 49%; height: 200px; display: inline-block; margin: 0 2px"">

            </div>
            <div class=""dx-card wide-card"" id=""productDate"" style=""width:");
            WriteLiteral(" 100%; display: inline-block; margin: 0 2px\">\r\n                ");
#nullable restore
#line 85 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
            Write(Html.DevExtreme().Calendar()
            .ID("dateBoxProduct")
            .Value(DateTime.Now)
            .Disabled(false)
            .ZoomLevel(CalendarZoomLevel.Month)
            .OnValueChanged("dateBoxProduct_valueChanged")
        );

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </div>
            <div class=""dx-card wide-card"" id=""productDetailValues"" style=""width: 100%; height: 150px; display: inline-block; margin: 0 2px"">
                <div class=""dx-card wide-card"" id=""productKesim"" style=""width: 24.4%; height: 200px; display: inline-block; margin: 0 2px"">
                    <div class=""productDetailCardHeader"">KESİM</div>
                    <div style=""padding:5px 15px"">
                        <div class=""detailItemEx"">HAFTALIK</div>
                        <div id=""hfKesim"" class=""detailItemValue"">0</div>
                    </div>
                    <div style=""padding:5px 15px"">
                        <div class=""detailItemEx"">TARİH</div>
                        <div id=""trKesim"" class=""detailItemValue"">0</div>
                    </div>
                </div>
                <div class=""dx-card wide-card"" id=""productTasnif"" style=""width: 24.4%; height: 200px; display: inline-block; margin: 0 2px"">
                    <div class=""productDeta");
            WriteLiteral(@"ilCardHeader"">TASNİF</div>
                    <div style=""padding:5px 15px"">
                        <div class=""detailItemEx"">HAFTALIK</div>
                        <div id=""hfTasnif"" class=""detailItemValue"">0</div>
                    </div>
                    <div style=""padding:5px 15px"">
                        <div class=""detailItemEx"">TARİH</div>
                        <div id=""trTasnif"" class=""detailItemValue"">0</div>
                    </div>
                </div>
                <div class=""dx-card wide-card"" id=""productBant"" style=""width: 24.4%; height: 200px; display: inline-block; margin: 0 2px"">
                    <div class=""productDetailCardHeader"">BANT</div>
                    <div style=""padding:5px 15px"">
                        <div class=""detailItemEx"">HAFTALIK</div>
                        <div id=""hfBant"" class=""detailItemValue"">0</div>
                    </div>
                    <div style=""padding:5px 15px"">
                        <div class=""detailItemEx"">");
            WriteLiteral(@"TARİH</div>
                        <div id=""trBant"" class=""detailItemValue"">0</div>
                    </div>
                </div>
                <div class=""dx-card wide-card"" id=""productKalite"" style=""width: 24.4%; height: 200px; display: inline-block; margin: 0 2px"">
                    <div class=""productDetailCardHeader"">KALİTE</div>
                    <div style=""padding:5px 15px"">
                        <div class=""detailItemEx"">HAFTALIK</div>
                        <div id=""hfKalite"" class=""detailItemValue"">0</div>
                    </div>
                    <div style=""padding:5px 15px"">
                        <div class=""detailItemEx"">TARİH</div>
                        <div id=""trKalite""  class=""detailItemValue"">0</div>
                    </div>
                </div>
            </div>
        </div>
    </div>



</div>


<script>
    var weekDetail, productDetail;
    function UretimIzleFilter() {
        $(""#hfKesim"").html(0);
        $(""#hfTasnif"").html");
            WriteLiteral(@"(0);
        $(""#hfBant"").html(0);
        $(""#hfKalite"").html(0);

        $(""#trKesim"").html(0);
        $(""#trTasnif"").html(0);
        $(""#trBant"").html(0);
        $(""#trKalite"").html(0);
        var data = {
            BRAND: jQuery(""[name=BRAND]"").val(),
            SEASON: jQuery(""[name=SEASON]"").val(),
            BAND: jQuery(""[name=BAND]"").val(),
            MODEL: jQuery(""[name=MODEL]"").val(),
            COLOR: jQuery(""[name=COLOR]"").val(),
            ORDER_TYPE: jQuery(""[name=ORDER_TYPE]"").val(),
            PRODUCT_GROUP: jQuery(""[name=PRODUCT_GROUP]"").val()
        };
        $.ajax({
            type: ""POST"",
            url: '");
#nullable restore
#line 170 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
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
                        })
                    }),
                    height: ""100%"",
                    itemTemplate: function (data) {
                        var html = ""<div style='text-align:center;padding-right:15px;'> <span style='font-weight:bold;font-size:11px'>"" + data.WEEK + "". Hafta </span><br> <span style='font-weight:bold;color:indianred'>  ("" + moment(data.START_DATE).format(""DD.MM.YYYY"") + "" - "" + moment(data.END_DATE).format(""DD.MM.YYYY"") + "")</span></div>"";
                        return html;
                    },
                    s");
            WriteLiteral(@"howSelectionControls: true,
                    selectionMode: ""single"", pageLoadMode: ""scrollBottom"",
                    onSelectionChanged: function (data) {
                        haftaModel = data.addedItems[0];
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
        data = {
            model: haftaModel,
            filterModel: {
                BRAND: jQuery(""[name=BRAND]"").val(),
                SEASON: jQuery(""[name=SEASON]"").val(),
                BAND: jQuery(""[name=BAND]"").val(),
                MODEL: jQuery(""[name=MODEL]"").val(),
                COLOR: jQuery(""[name=COLOR]"").val(),
                ORDER_TYPE: jQuery(""[name=ORDER_TYPE]"").val(),");
            WriteLiteral("\r\n                PRODUCT_GROUP: jQuery(\"[name=PRODUCT_GROUP]\").val()\r\n            }\r\n        }\r\n        $.ajax({\r\n            type: \"POST\",\r\n            url: \'");
#nullable restore
#line 223 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
             Write(Url.Action("GetProductList", "UretimIzle"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            timeout: 180000,
            success: function (income, status, xmlRequest) {
                productDetail= $(""#productDetail"").dxList({
                    dataSource: new DevExpress.data.DataSource({
                        store: new DevExpress.data.ArrayStore({
                            key: ""ID"",
                            data: income
                        })
                    }),
                    height: ""100%"", searchEnabled: true,
                    searchExpr: ""MODEL"",
                    itemTemplate: function (data) {
                        var html = ""<div> <span style='font-weight:bold;'>"" + data.MODEL + ""</span>.<span style='font-weight:bold'>"" + data.COLOR + ""</span></div>"";
                        return html;
                    },
                    showSelectionControls: true, pageLoadMode: ""scrollBottom"",
                    selectionMode: ""single"",
      ");
            WriteLiteral(@"              onSelectionChanged: function (data) {
                        productModel = data.addedItems[0];
                        GetProductionDetails();
                    }
                }).dxList(""instance"");

            }, beforeSend: function () {
                getLoadPanelInstance().show();
            }


        }).always(function () {
            getLoadPanelInstance().hide();
        });
    }

    function TarihFiltrele(TarihDeger) {
         var data = {
            haftaModel: haftaModel,
            productModel: productModel,
            filterModel: {
                BRAND: jQuery(""[name=BRAND]"").val(),
                SEASON: jQuery(""[name=SEASON]"").val(),
                BAND: jQuery(""[name=BAND]"").val(),
                MODEL: jQuery(""[name=MODEL]"").val(),
                COLOR: jQuery(""[name=COLOR]"").val(),
                ORDER_TYPE: jQuery(""[name=ORDER_TYPE]"").val(),
                PRODUCT_GROUP: jQuery(""[name=PRODUCT_GROUP]"").val()
             },");
            WriteLiteral("\r\n             Tarih: TarihDeger\r\n        }\r\n          $.ajax({\r\n            type: \"POST\",\r\n            url: \'");
#nullable restore
#line 276 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
             Write(Url.Action("GetProductionDetailsByDate", "UretimIzle"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            timeout: 180000,
            success: function (income, status, xmlRequest) {

                $(""#trKesim"").html(income.KESIM);
                $(""#trTasnif"").html(income.TASNIF);
                $(""#trBant"").html(income.BANT);
                $(""#trKalite"").html(income.KALITE);

            }, beforeSend: function () {
                getLoadPanelInstance().show();
            }


        }).always(function () {
            getLoadPanelInstance().hide();
        });
    }
    function dateBoxProduct_valueChanged(data) {
        TarihFiltrele(moment(data.value).format(""DD.MM.YYYY""));

    }
    function GetProductionDetails() { 
        TarihFiltrele(moment($(""#dateBoxProduct"").dxCalendar(""instance"").option(""value"")).format(""DD.MM.YYYY""))
        data = {
            haftaModel: haftaModel,
            productModel: productModel,
            filterModel: {
                BRAND: ");
            WriteLiteral(@"jQuery(""[name=BRAND]"").val(),
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
#line 318 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
             Write(Url.Action("GetProductionDetails", "UretimIzle"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            timeout: 180000,
            success: function (income, status, xmlRequest) {
                var totalPLAN = 0;
                var totalORDER = 0;
                for (var i = 0; i < income.Item2.length; i++) {
                    totalORDER += income.Item2[i].ORDER_QUANTITY;
                    totalPLAN += income.Item2[i].QUANTITY;
                }
                var bar = [{
                    arg: ""SİPARİŞ"",
                    val: totalORDER
                }, {
                    arg: ""HAFTALIK PLAN"",
                    val: totalPLAN
                }, {
                    arg: ""KESİM"",
                    val: income.Item3.KESIM
                }, {
                    arg: ""TASNİF"",
                    val: income.Item3.TASNIF
                }, {
                    arg: ""BANT"",
                    val: income.Item3.BANT
                }, {
                    arg");
            WriteLiteral(@": ""KALİTE"",
                    val: income.Item3.KALITE
                    }];
                $(""#productBar"").dxChart({
                    dataSource: bar,
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
                            }
                        }
                    },
                    title: ""Üretim Durumu""
                });
                $(""#hfKesim"").html(income.Item3.KESIM);
                $(""#hfTasnif"").html(income.Item3.TASNIF);
                $(""#hfBant"").html(income.Item3.BANT);
                $(""#hfKalite"").html(income.Item3.KALITE);
                $(""");
            WriteLiteral(@"#productPlan"").dxDataGrid({
                    dataSource: income.Item2,
                    keyExpr: 'ID',
                    selection: {
                        mode: ""single""
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
