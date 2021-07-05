#pragma checksum "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\PlanOlustur.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cca1b019c6c667d37f01af511e37041d93bc931c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimPlan.Views_UretimPlan_PlanOlustur), @"mvc.1.0.view", @"/Views/UretimPlan/PlanOlustur.cshtml")]
namespace EPM_Web.Models.UretimPlan
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
#line 2 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\PlanOlustur.cshtml"
using EPM.Core.FormModels.Uretim;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cca1b019c6c667d37f01af511e37041d93bc931c", @"/Views/UretimPlan/PlanOlustur.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimPlan_PlanOlustur : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UretimOnayliListe>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n \r\n<div id=\"form-container\">\r\n    <div class=\"dx-card wide-card\">\r\n        ");
#nullable restore
#line 8 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\PlanOlustur.cshtml"
    Write(Html.DevExtreme().Form<EPM.Core.FormModels.Uretim.UretimOnayliListe>()
        .ID("edOnayliListe").ShowValidationSummary(true)
        .ColCount(8)
        .Items(items =>
        {

            items.AddSimpleFor(m => m.BRAND).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetBrandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimpleFor(m => m.SEASON).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetSeasonList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimpleFor(m => m.MODEL).Editor(ob=>ob.TextBox().ID("edModel")).IsRequired(false);
            items.AddSimpleFor(m => m.COLOR).Editor(ob=>ob.TextBox().ID("edColor")).IsRequired(false);
            items.AddSimpleFor(m => m.ORDER_TYPE).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetOrderList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimpleFor(m => m.PRODUCTION_TYPE).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetProductionTypes").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimpleFor(m => m.FABRIC_TYPE).Editor(ob => ob.Lookup().DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetFabricList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddButton().HorizontalAlignment(HorizontalAlignment.Left).VerticalAlignment(VerticalAlignment.Center)
                   .ButtonOptions(b => b.Text("FİLTRELE")
                       .Type(ButtonType.Default)
                       .Icon("fas fa-sign-in-alt")
                       .Width("100%")
                       .OnClick("UretimOnayliFiltrele")
                       .UseSubmitBehavior(true)
               );

        })
        .FormData(Model).ShowValidationSummary(false)
    );

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


    </div>
</div>

<div id=""form-container"">
    <div class=""dx-card wide-card"">
        <div id=""edReport"" style=""height:calc(100vh - 150px)"">
        </div>
    </div>
</div>
<script>

    var datax = null;
    function UretimOnayliFiltrele() {
        var data = {
            BRAND: jQuery(""[name=BRAND]"").val(),
            SEASON: jQuery(""[name=SEASON]"").val(),
            MODEL: jQuery(""[name=MODEL]"").val(),
            COLOR: jQuery(""[name=COLOR]"").val(),
            ORDER_TYPE: jQuery(""[name=ORDER_TYPE]"").val(),
            PRODUCTION_TYPE: jQuery(""[name=PRODUCTION_TYPE]"").val(),
            FABRIC_TYPE: jQuery(""[name=FABRIC_TYPE]"").val(),
        };
        getLoadPanelInstance().show();
        $.ajax({
            type: ""POST"",
            url: '");
#nullable restore
#line 60 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\PlanOlustur.cshtml"
             Write(Url.Action("_PartialGetPlanList", "UretimPlan"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            async: true,
            success: function (income, status, xmlRequest) {
                var formData = JSON.parse(income).DATA;
                var YEARS = JSON.parse(income).YEARS;
                var COLUMNNAMES = JSON.parse(income).COLUMNNAMES;
                $(""#edReport"").dxDataGrid({
                    dataSource: formData,
                    keyExpr: ""ROW_ID"",
                    loadPanel: {
                        enabled: true
                    },
                    remoteOperations: false,
                    scrolling: {
                        mode: 'virtual'
                    },
                    sorting: {
                        mode: ""multiple""
                    },
                    filterRow: {
                        visible: true,
                        applyFilter: ""auto""
                    },
                    headerFilter: { visible: true },
");
            WriteLiteral(@"                    editing: {
                        mode: ""cell"",
                        allowUpdating: true
                    }, columnChooser: {
                        enabled: true
                    }, customizeColumns: function (columns) {
                        for (var i = 0; i < YEARS.length; i++) {
                            columns.push({
                                caption: YEARS[i].YEAR,
                                isBand: true
                            });
                            for (var a = 0; a < columns.length - 1; a++) {
                                if (a < 16) {
                                    columns[a].fixed = true;
                                    columns[a].allowEditing = false;
                                }
                                if (columns[a].dataField == ""ID"" || columns[a].dataField == ""PRODUCT_GROUP"" || columns[a].dataField == ""MARKET_ID"" || columns[a].dataField == ""FABRIC_TYPE"" || columns[a].dataField == ""ROW_ID"") {
 ");
            WriteLiteral(@"                                   columns[a].visible = false;
                                }
                                else if (columns[a].dataField == ""MARKET"") {
                                    columns[a].width = 100;
                                }
                                else if (columns[a].dataField == ""DEADLINE"") {
                                    columns[a].width = 100;
                                    columns[a].dataType = ""date"";
                                }
                                else if (columns[a].dataField == ""PLANSIZ_ADET"" || columns[a].dataField == ""URETIM_ADET"" || columns[a].dataField == ""PLANLANAN_ADET"") {
                                    columns[a].dataType = ""number"";
                                }
                                if (columns[a].dataField == ""PLANLANAN_ADET"") {
                                    columns[a].calculateCellValue = function (rowData) {
                                        var top = 0;
          ");
            WriteLiteral(@"                              for (var m = 0; m < COLUMNNAMES.length; m++) {
                                            if (rowData[COLUMNNAMES[m].NAME])
                                                top += rowData[COLUMNNAMES[m].NAME];
                                        }
                                        rowData.PLANLANAN_ADET = parseFloat(top);
                                        return rowData.PLANLANAN_ADET;
                                    }
                                }
                                if (columns[a].dataField == ""PLANSIZ_ADET"") {
                                    columns[a].calculateCellValue = function (rowData) {
                                        rowData.PLANSIZ_ADET = rowData.URETIM_ADET - rowData.PLANLANAN_ADET;
                                        return rowData.PLANSIZ_ADET;
                                    }
                                }
                                try {
                                    if (columns");
            WriteLiteral(@"[a].dataField.startsWith(YEARS[i].YEAR)) {
                                        columns[a].summary = {
                                            totalItems: [{
                                                column: ""2021"",
                                                summaryType: ""sum"",
                                                alignment: ""center"",
                                                displayFormat: ""TOP: {0}""
                                            }]
                                        };
                                        columns[a].ownerBand = columns.length - 1;
                                        columns[a].dataType = ""number"";
                                        columns[a].allowFiltering = false;
                                        columns[a].caption = columns[a].dataField.replace(YEARS[i].YEAR + '_', '') + "".HAFTA"";


                                    } else {

                                    }
                                }");
            WriteLiteral(@" catch (e) {

                                }
                            }
                        }
                    },
                    onRowUpdating: function (e) {
                        var package = {
                            new: null, old:null
                        };
                        package.newValue = e.newData;
                        package.oldValue = e.oldData;
                        var d = $.Deferred();
                        var settings = {
                            ""async"": false,
                            ""url"": """);
#nullable restore
#line 166 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\PlanOlustur.cshtml"
                               Write(Url.Action("UpdateTask", "UretimPlan"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                            ""method"": ""POST"",
                            ""timeout"": 0,
                            ""headers"": {
                                ""Content-Type"": ""application/json""
                            },
                            ""data"": JSON.stringify(package),
                        };
                        $.ajax(settings).done(function (response) {
                            try {
                                var parsedIncome = response;
                                if (parsedIncome.isOK) {
                                    d.resolve();
                                } else {
                                    d.reject(parsedIncome.errorText);
                                }
                            } catch (e) {

                                d.reject(""İşlem hatası :"" + e.message);
                            }

                        }).fail(function (jqXHR, textStatus) {
                            d.reject(""Kayıt Hatası : "" + text");
            WriteLiteral(@"Status);
                        });
                        e.cancel = d.promise();
                    },
                    focusedRowEnabled: true,
                    focusedRowIndex: 0,
                    focusedColumnIndex: 0 ,
                    showColumnLines: true,
                    showRowLines: true,
                    rowAlternationEnabled: true,
                    showBorders: true,
                    onContentReady: function (e) {
                    },
                    onCellDblClick: function (e) {
                        e.component.cellValue(e.rowIndex, e.columnIndex, e.data[""PLANSIZ_ADET""]);
                        e.component.saveEditData();

                    },
                    columnMinWidth: 80,
                    showBorders: true,
                }).dxDataGrid(""instance"");


            }
            , beforeSend: function () { 
            }


        }).always(function () {
            getLoadPanelInstance().hide();
        });
    ");
            WriteLiteral("}\r\n</script>");
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
