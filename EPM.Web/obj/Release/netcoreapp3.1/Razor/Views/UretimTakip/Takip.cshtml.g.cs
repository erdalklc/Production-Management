#pragma checksum "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\Takip.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "44eff677044ff424bfc1503fad999c5721fb0886"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimTakip.Views_UretimTakip_Takip), @"mvc.1.0.view", @"/Views/UretimTakip/Takip.cshtml")]
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
#line 1 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\Takip.cshtml"
using EPM.Core.FormModels.Uretim;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\Takip.cshtml"
using EPM.Core.Repository;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"44eff677044ff424bfc1503fad999c5721fb0886", @"/Views/UretimTakip/Takip.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimTakip_Takip : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UretimOnayliListe>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<div id=\"form-container\">\r\n    <div class=\"dx-card wide-card\">\r\n        ");
#nullable restore
#line 8 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\Takip.cshtml"
    Write(Html.DevExtreme().Form<EPM.Core.FormModels.Uretim.UretimOnayliListe>()
        .ID("edOnayliListe").ShowValidationSummary(true)
        .ColCount(9)
        .Items(items =>
        {

            items.AddSimpleFor(m => m.BRAND).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetBrandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimpleFor(m => m.SEASON).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetSeasonList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimpleFor(m => m.MODEL).Editor(ob=>ob.TextBox().ID("edModel")).IsRequired(false);
            items.AddSimpleFor(m => m.COLOR).Editor(ob=>ob.TextBox().ID("edColor")).IsRequired(false);
            items.AddSimpleFor(m => m.ORDER_TYPE).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetOrderList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimpleFor(m => m.RECIPE).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetRecipeList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimpleFor(m => m.PRODUCTION_TYPE).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetProductionTypes").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimpleFor(m => m.FABRIC_TYPE).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetFabricList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddButton().HorizontalAlignment(HorizontalAlignment.Left).VerticalAlignment(VerticalAlignment.Center)
                   .ButtonOptions(b => b.Text("FİLTRELE")
                       .Type(ButtonType.Default)
                       .Icon("fas fa-sign-in-alt")
                       .Width("100%")
                       .OnClick("UretimTakipFiltrele")
                       .UseSubmitBehavior(true)
               );

        })
        .FormData(Model).ShowValidationSummary(false)
    );

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        <div id=""popup""></div>
        <div id=""popupDetay""></div>
        <div id=""edListe"">

        </div>
    </div>
</div>

<script>

    function UretimTakipFiltrele() {
        $(""#edListe"").html("""");
        var data = {
            BRAND: jQuery(""[name=BRAND]"").val(),
            SEASON: jQuery(""[name=SEASON]"").val(),
            MODEL: jQuery(""[name=MODEL]"").val(),
            COLOR: jQuery(""[name=COLOR]"").val(),
            ORDER_TYPE: jQuery(""[name=ORDER_TYPE]"").val(),
            PRODUCTION_TYPE: jQuery(""[name=PRODUCTION_TYPE]"").val(),
            FABRIC_TYPE: jQuery(""[name=FABRIC_TYPE]"").val(),
            RECIPE: jQuery(""[name=RECIPE]"").val(),
        };
        $.ajax({
            type: ""POST"",
            url: '");
#nullable restore
#line 58 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\Takip.cshtml"
             Write(Url.Action("_PartialUretimTakipFiltrele", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            timeout: 180000,
            success: function (income, status, xmlRequest) {
                $(""#edListe"").html(income);

            }, beforeSend: function () {
                getLoadPanelInstance().show();
            }


        }).always(function () {
            getLoadPanelInstance().hide();
        });
    }

    function EditClicked(e) {
        var header_id = e.row.data.ID;
        tempHeaderId = header_id;
        var popupOptions = {
            width: 420,
            height: 320,
            contentTemplate: function () {
                var data = {
                    HEADER_ID: tempHeaderId
                };
                return  $.ajax({
                    type: ""GET"",
                    url: '");
#nullable restore
#line 87 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\Takip.cshtml"
                     Write(Url.Action("_PartialProductionOrderList", "UretimTakip"));

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
            showTitle: true,
            title: ""SATIN ALMA SİPARİŞ BAĞLANTISI"",
            dragEnabled: false,
            closeOnOutsideClick: true
        };

        $(""#popup"").dxPopup(popupOptions).dxPopup(""instance"").show();
    }
    var tempHeaderId = 0;
    function DetailSearch(e) {
        var header_id = e.row.data.ID;
        tempHeaderId = header_id;
        var header_id = e.row.data.ID;
        tempHeaderId = header_id;
        var popupOptions = {
            contentTemplate: function () {
                var data = {
                    HEADER_ID: tempHeaderId
                };
                return  $.ajax({
                    type: ""GET"",
                    url: '");
#nullable restore
#line 115 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\Takip.cshtml"
                     Write(Url.Action("_PartialUretimTakipSatinAlmaDetayi", "UretimTakip"));

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
            width: function () { return ""calc(100% - 20px)""; },
            height: function () { return ""calc(100% - 10px)""; },
            showTitle: true,
            title: ""SATIN ALMA DETAYI"",
            dragEnabled: true,
            closeOnOutsideClick: true
        };

        $(""#popupDetay"").dxPopup(popupOptions).dxPopup(""instance"").show();
    }

    function InitProductionOrders(e) {
        e.data.HEADER_ID = tempHeaderId;
    }

    function gridSatinAlmaFocusedRowChanged(e) {
        console.log(e.row.data);
        setTimeout(function () {
            $(""#edProcessInformations"").load('");
#nullable restore
#line 140 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\Takip.cshtml"
                                         Write(Url.Action("_PartialProcessInformations", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', {\r\n                PO_HEADER_ID: e.row.data.PO_HEADER_ID,\r\n                HEADER_ID: e.row.data.HEADER_ID,\r\n                DETAIL_ID: e.row.data.DETAIL_TAKIP_NO\r\n            });\r\n\r\n\r\n            $(\"#edOrmeTab\").load(\'");
#nullable restore
#line 147 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\Takip.cshtml"
                             Write(Url.Action("_PartialEgemenOrmeList", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', {\r\n                TAKIP_NO: e.row.data.TAKIP_NO,\r\n                DETAIL_TAKIP_NO: e.row.data.DETAIL_TAKIP_NO\r\n            });\r\n\r\n            $(\"#edKumasDepoTab\").load(\'");
#nullable restore
#line 152 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\Takip.cshtml"
                                  Write(Url.Action("_PartialKumasDepoList", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', {\r\n                ITEM_ID: e.row.data.ITEM_ID,\r\n                PO_HEADER_ID: e.row.data.PO_HEADER_ID\r\n            });\r\n            $(\"#edKesimTab\").load(\'");
#nullable restore
#line 156 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\Takip.cshtml"
                              Write(Url.Action("_PartialKesimFoyuList", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', {\r\n                ITEM_ID: e.row.data.ITEM_ID,\r\n                PO_HEADER_ID: e.row.data.PO_HEADER_ID,\r\n                RENK_DETAY: e.row.data.RENKDETAY,\r\n            });\r\n            $(\"#edBantTab\").load(\'");
#nullable restore
#line 161 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\Takip.cshtml"
                             Write(Url.Action("_PartialBantList", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\', {\r\n                ITEM_ID: e.row.data.ITEM_ID,\r\n                PO_HEADER_ID: e.row.data.PO_HEADER_ID,\r\n                RENK_DETAY: e.row.data.RENKDETAY,\r\n            });\r\n            $(\"#edKaliteTab\").load(\'");
#nullable restore
#line 166 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\Takip.cshtml"
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
    function edGridProcessRowPrepared(e) {
        if (e.rowType != ""data"")
            return;
        e.rowElement.css('backgroundColor', e.data.COLOR);
    }
    function edUretimTakipGridCellPrepared(e) {
        if (e.rowType != ""data"")
            return;
        if (e.column.dataField == 'END_DATE' || e.column.dataField == 'PROCESS_INFO' || e.column.dataField == 'MUSTBE_STATE') {

            e.cellElement.css('backgroundColor', e.data.BACKGROUND_COLOR);
        }
    }

</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UretimOnayliListe> Html { get; private set; }
    }
}
#pragma warning restore 1591
