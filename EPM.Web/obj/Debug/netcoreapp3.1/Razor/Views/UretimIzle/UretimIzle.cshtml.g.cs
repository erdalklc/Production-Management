#pragma checksum "E:\Eren\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8d7a4f93cdb3fce2321324e3aef62c13591fb55b"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8d7a4f93cdb3fce2321324e3aef62c13591fb55b", @"/Views/UretimIzle/UretimIzle.cshtml")]
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
            WriteLiteral("\r\n\r\n\r\n<div id=\"form-container\">\r\n    <div class=\"dx-card wide-card\">\r\n        ");
#nullable restore
#line 10 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
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

    <div class=""dx-card wide-card"" id=""weekDetailCard"" style=""width:265px;height:calc(100vh - 135px);display:inline-block;margin:0 2px"">
        <div id=""weekDetail""></div>
    </div>
    <div class=""dx-card wide-card"" id=""productDetailCard"" style=""width:200px;height:calc(100vh - 135px);display:inline-block;margin:0 2px"">
        <div id=""productDetail""></div>
    </div>
</div>


<script>
    var weekDetail, productDetail;
    function UretimIzleFilter() { 
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
#line 67 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
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
                        var html = ""<div> <span style='font-weight:bold;font-size:11px'>"" + data.WEEK + "". Hafta</span> <span style='font-style:italic;color:indianred'> ("" + moment(data.START_DATE).format(""DD.MM.YYYY"") + "" - "" + moment(data.END_DATE).format(""DD.MM.YYYY"") + "")</span></div>"";
                        return html;
                    },
                    showSelectionControls: true,
      ");
            WriteLiteral(@"              selectionMode: ""single"", pageLoadMode: ""scrollBottom"",
                    onSelectionChanged: function (data) {
                        GetProductDetail(data.addedItems[0]);
                    }
                });

            }, beforeSend: function () {
                getLoadPanelInstance().show();
            }


        }).always(function () {
            getLoadPanelInstance().hide();
        });
    }

    function GetProductDetail(haftaModel) {
        data = {
            model: haftaModel,
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
        ");
            WriteLiteral("    type: \"POST\",\r\n            url: \'");
#nullable restore
#line 117 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimIzle\UretimIzle.cshtml"
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
                    height: ""100%"",
                    itemTemplate: function (data) {
                        var html = ""<div> <span style='font-weight:bold;'>"" + data.MODEL + ""</span>.<span style='font-weight:bold'>"" + data.COLOR + ""</span></div>"";
                        return html;
                    },
                    showSelectionControls: true, pageLoadMode: ""scrollBottom"",
                    selectionMode: ""single"",
                }).dxList(""instance"");

            }, beforeSend: ");
            WriteLiteral("function () {\r\n                getLoadPanelInstance().show();\r\n            }\r\n\r\n\r\n        }).always(function () {\r\n            getLoadPanelInstance().hide();\r\n        });\r\n    }\r\n\r\n\r\n</script>\r\n");
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
