#pragma checksum "E:\Eren\Eren Production Management\EPM.Web\Views\Uretim\UretimListesi.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cdcf08f7d10a0d51bbb2b91732a113b710b0b98a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Uretim.Views_Uretim_UretimListesi), @"mvc.1.0.view", @"/Views/Uretim/UretimListesi.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cdcf08f7d10a0d51bbb2b91732a113b710b0b98a", @"/Views/Uretim/UretimListesi.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Uretim_UretimListesi : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EPM.Production.Dto.Production.UretimOnayliListe>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 4 "E:\Eren\Eren Production Management\EPM.Web\Views\Uretim\UretimListesi.cshtml"
   
    var status = Model.STATUS;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div id=\"form-container\">\r\n    <div class=\"dx-card wide-card\">\r\n        ");
#nullable restore
#line 10 "E:\Eren\Eren Production Management\EPM.Web\Views\Uretim\UretimListesi.cshtml"
    Write(Html.DevExtreme().Form<EPM.Production.Dto.Production.UretimOnayliListe>()
        .ID("edOnayliListe").ShowValidationSummary(true)
        .Items(items =>
        {
            items.AddGroup().ColCount(8).Items(groupItems =>
            {
                groupItems.AddSimpleFor(m => m.APPROVAL_STATUS).Editor(ob => ob.Lookup().Disabled(true).DataSource(d => d.Mvc().Controller("Uretim").LoadAction("GetApprovalStatusList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.BRAND).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetBrandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.SEASON).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetSeasonList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.MODEL).Editor(ob => ob.TextBox().ID("edModel")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.COLOR).Editor(ob => ob.TextBox().ID("edColor")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.ORDER_TYPE).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetOrderList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.PRODUCTION_TYPE).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetProductionTypes").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.FABRIC_TYPE).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetFabricList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.STATUS).Visible(false);

            });

            items.AddGroup().ColCount(8).Items(groupItems =>
            {
                groupItems.AddSimpleFor(m => m.BAND_ID).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetBandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.RECIPE).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetRecipeList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddSimpleFor(m => m.PRODUCT_GROUP).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetProductGroupList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
                groupItems.AddEmpty();
                groupItems.AddEmpty();
                groupItems.AddEmpty();
                groupItems.AddEmpty();
                groupItems.AddButton().HorizontalAlignment(HorizontalAlignment.Left).VerticalAlignment(VerticalAlignment.Center)
                   .ButtonOptions(b => b.Text("FİLTRELE")
                       .Type(ButtonType.Default)
                       .Icon("fas fa-sign-in-alt")
                       .Width("100%")
                       .OnClick("UretimOnayliFiltrele")
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
</div>

<div id=""edReport"" style=""width:69.5%;display:inline-block;float:left"">

</div>

<div id=""edReportDetail"" style=""width:29.5%;display:inline-block;float:right"">

</div>
<script>
    function UpdateMasterRow(e) {

        var data = {
            ""key"": e.key,
            ""values"": JSON.stringify(e.newData)
        };
        res = $.ajax({
            url: '");
#nullable restore
#line 73 "E:\Eren\Eren Production Management\EPM.Web\Views\Uretim\UretimListesi.cshtml"
             Write(Url.Action("UretimOnayliUpdate","Uretim"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            method: ""PUT"",
            data: data,
            success: function (data) {

            },
            error: function (textStatus, errorThrown) {
                //d.reject(textStatus.statusText +"" ""+errorThrown);
            }
        })
        e.cancel = res.promise();

    }

     function InsertMasterRow(e){


        //console.log(e);

    }

    function DeleteMasterRow(e) {
        var data = {
            ""key"": e.key
        };
        res = $.ajax({
            url: '");
#nullable restore
#line 99 "E:\Eren\Eren Production Management\EPM.Web\Views\Uretim\UretimListesi.cshtml"
             Write(Url.Action("UretimOnayliDelete", "Uretim"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            method: ""DELETE"",
            data: data,
            success: function (data) {

            },
            error: function (textStatus, errorThrown) {
                //d.reject(textStatus.statusText +"" ""+errorThrown);
            }
        })
        e.cancel = res.promise();

    }
    function gridFocusedRowChanged(e) {
        var data = {
                ID: e.row.data.ID
            };
            $.ajax({
                type: ""POST"",
                url: '");
#nullable restore
#line 118 "E:\Eren\Eren Production Management\EPM.Web\Views\Uretim\UretimListesi.cshtml"
                 Write(Url.Action("_PartialUretimOnayliListeFiltreleDetail", "Uretim"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                data: data,
                contentType: 'application/x-www-form-urlencoded',
                timeout: 180000,
                success: function (income, status, xmlRequest) {
                    $(""#edReportDetail"").html(income);

                }, beforeSend: function () {
                }


            }).always(function () {
            });
    }
    function exportingHeader(e) {
        var workbook = new ExcelJS.Workbook();
        var worksheet = workbook.addWorksheet('Employees');

        DevExpress.excelExporter.exportDataGrid({
            component: e.component,
            worksheet: worksheet,
            autoFilterEnabled: true
        }).then(function () {
            workbook.xlsx.writeBuffer().then(function (buffer) {
                saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'EPM_PRODUCTION_HEADER.xlsx');
            });
        });
        e.cancel = true;
    }
    function exportingDetail(e) {
        var workbook =");
            WriteLiteral(@" new ExcelJS.Workbook();
        var worksheet = workbook.addWorksheet('Employees');

        DevExpress.excelExporter.exportDataGrid({
            component: e.component,
            worksheet: worksheet,
            autoFilterEnabled: true
        }).then(function () {
            workbook.xlsx.writeBuffer().then(function (buffer) {
                saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'EPM_PRODUCTION_DETAIL.xlsx');
            });
        });
        e.cancel = true;
    }
    function LogSearchHeader(e) {
        var header_id = e.row.data.ID;
        var popupOptions = {
            contentTemplate: function () {
                var data = {
                    HEADER_ID: header_id
                };
                return $.ajax({
                    type: ""GET"",
                    url: '");
#nullable restore
#line 171 "E:\Eren\Eren Production Management\EPM.Web\Views\Uretim\UretimListesi.cshtml"
                     Write(Url.Action("_PartialUretimLog", "Uretim"));

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
            width: function () { return ""950px""; },
            height: function () { return ""calc(100% - 350px)""; },
            showTitle: true,
            title: ""SATIR TARİHÇESİ"",
            dragEnabled: true,
            closeOnOutsideClick: true
        };

        $(""#popupDetay"").dxPopup(popupOptions).dxPopup(""instance"").show();

    }
    function LogSearchDetail(e) {
         var detail_id = e.row.data.ID;
        var popupOptions = {
            contentTemplate: function () {
                var data = {
                    DETAIL_ID: detail_id
                };
                return $.ajax({
                    type: ""GET"",
                    url: '");
#nullable restore
#line 198 "E:\Eren\Eren Production Management\EPM.Web\Views\Uretim\UretimListesi.cshtml"
                     Write(Url.Action("_PartialUretimDetailLog", "Uretim"));

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
            width: function () { return ""calc(100% / 2)""; },
            height: function () { return ""calc(100% - 350px)""; },
            showTitle: true,
            title: ""SATIR TARİHÇESİ"",
            dragEnabled: true,
            closeOnOutsideClick: true
        };

        $(""#popupDetay"").dxPopup(popupOptions).dxPopup(""instance"").show();
    }
    function UretimOnayliFiltrele() {
        $(""#edReportDetail"").html("""");
        var data = {
            BRAND: jQuery(""[name=BRAND]"").val(),
            SEASON: jQuery(""[name=SEASON]"").val(),
            MODEL: jQuery(""[name=MODEL]"").val(),
            COLOR: jQuery(""[name=COLOR]"").val(),
            ORDER_TYPE: jQuery(""[name=ORDER_TYPE]"").val(),
            PRODUCTION_TYPE: jQuery(""[name=PRODUCTION_TYPE]"").val(),
            FABRIC_TYPE: jQuer");
            WriteLiteral(@"y(""[name=FABRIC_TYPE]"").val(),
            RECIPE: jQuery(""[name=RECIPE]"").val(),
            PRODUCT_GROUP: jQuery(""[name=PRODUCT_GROUP]"").val(),
            BAND_ID: jQuery(""[name=BAND_ID]"").val(),
            APPROVAL_STATUS: jQuery(""[name=APPROVAL_STATUS]"").val(),
            STATUS: ");
#nullable restore
#line 229 "E:\Eren\Eren Production Management\EPM.Web\Views\Uretim\UretimListesi.cshtml"
               Write(status);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n        };\r\n        console.log(data);\r\n        $.ajax({\r\n            type: \"POST\",\r\n            url: \'");
#nullable restore
#line 234 "E:\Eren\Eren Production Management\EPM.Web\Views\Uretim\UretimListesi.cshtml"
             Write(Url.Action("_PartialUretimOnayliListeFiltrele", "Uretim"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            timeout: 180000,
            success: function (income, status, xmlRequest) {
                $(""#edReport"").html(income);

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EPM.Production.Dto.Production.UretimOnayliListe> Html { get; private set; }
    }
}
#pragma warning restore 1591
