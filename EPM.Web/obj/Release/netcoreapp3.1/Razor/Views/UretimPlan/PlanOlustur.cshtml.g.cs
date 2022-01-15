#pragma checksum "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimPlan\PlanOlustur.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0fbfb196b686660622cffe26e5b6a5df7035f358"
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
#line 1 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\_ViewImports.cshtml"
using EPM_Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimPlan\PlanOlustur.cshtml"
using EPM.Plan.Dto.Plan;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0fbfb196b686660622cffe26e5b6a5df7035f358", @"/Views/UretimPlan/PlanOlustur.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimPlan_PlanOlustur : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Plan_Filter>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 5 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimPlan\PlanOlustur.cshtml"
   
    bool canEditPlan = Convert.ToBoolean(ViewData["CanEditPlan"]);
    string sCanEditPlan = "false";
    if(canEditPlan)
    {
        sCanEditPlan = "true";
    }
    IEnumerable<string> Items = new List<string>(){(DateTime.Now.Year-2).ToString(),(DateTime.Now.Year-1).ToString(),(DateTime.Now.Year).ToString(),(DateTime.Now.Year+1).ToString(),(DateTime.Now.Year+2).ToString()};
  
    

#line default
#line hidden
#nullable disable
            WriteLiteral("<div id=\"form-container\">\r\n    <div class=\"dx-card wide-card\">\r\n        ");
#nullable restore
#line 18 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimPlan\PlanOlustur.cshtml"
    Write(Html.DevExtreme().Form<Plan_Filter>()
        .ID("edOnayliListe").ShowValidationSummary(true)
        .ColCount(10)
        .Items(items =>
        {

            
            items.AddSimpleFor(m => m.BRAND).Editor(ob => ob.Lookup().Value(0).SearchEnabled(true).DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetBrandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimple().Name("SEASON").ColSpan(3).Label(l=>l.Text("SEASON")).Editor(ob => ob.TagBox().ShowSelectionControls(true).SearchEnabled(true).DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetSeasonList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID"));            
            items.AddEmpty();
            items.AddSimpleFor(m => m.MODEL).Editor(ob=>ob.TextBox().ID("edModel")).IsRequired(false);
            items.AddSimpleFor(m => m.COLOR).Editor(ob=>ob.TextBox().ID("edColor")).IsRequired(false);
            items.AddSimpleFor(m => m.ORDER_TYPE).Editor(ob => ob.Lookup().Value(0).SearchEnabled(true).DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetOrderList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimpleFor(m => m.PRODUCTION_TYPE).Editor(ob => ob.Lookup().Value(0).SearchEnabled(true).DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetProductionTypes").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimpleFor(m => m.FABRIC_TYPE).Editor(ob => ob.Lookup().Value(0).SearchEnabled(true).DataSource(d => d.Mvc().LoadParams(new { all = true }).Controller("Uretim").LoadAction("GetFabricList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimple().Name("YEAR").Label(l=>l.Text("YEAR")).ColSpan(3).Editor(ob=>ob.TagBox().ShowSelectionControls(true).ApplyValueMode(EditorApplyValueMode.Instantly).Items(Items).Value(new[]{Items.ToList()[2],Items.ToList()[3]})).IsRequired(false); 
            items.AddEmpty();
            items.AddEmpty();
            items.AddEmpty();
            items.AddEmpty();
            items.AddEmpty();
            items.AddEmpty(); 
            items.AddButton().HorizontalAlignment(HorizontalAlignment.Right).VerticalAlignment(VerticalAlignment.Center)
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
        <div id=""edReport"" style=""height:calc(100vh - 230px)"">
        </div>
    </div>
</div>
<script> 
    var datax = null;
    function UretimOnayliFiltrele() {
        console.log($(""#edOnayliListe"").dxForm(""instance"").getEditor(""SEASON"")._selectedItems);
        if($(""#edOnayliListe"").dxForm(""instance"").getEditor(""SEASON"")._selectedItems.length==0 || $(""#edOnayliListe"").dxForm(""instance"").getEditor(""YEAR"")._selectedItems ==0){
            DevExpress.ui.notify(""Lütfen tüm değerlerin seçildiğinden emin olunuz"", ""error"", 600);
            return;
        } 
        var data = {
            BRAND: jQuery(""[name=BRAND]"").val(),
            SEASON: $(""#edOnayliListe"").dxForm(""instance"").getEditor(""SEASON"")._selectedItems,
            MODEL: jQuery(""[name=MODEL]"").val(),
            COLOR: jQuery(""[name=COLOR]"").val(),
            ORDER_TYPE: jQuery(""[name=ORDER_TYPE]"").val(),
            PRODUCTION_T");
            WriteLiteral(@"YPE: jQuery(""[name=PRODUCTION_TYPE]"").val(),
            FABRIC_TYPE: jQuery(""[name=FABRIC_TYPE]"").val(),
            PLAN_DURUM: jQuery(""[name=PLAN_DURUM]"").val(),
            YEAR: $(""#edOnayliListe"").dxForm(""instance"").getEditor(""YEAR"")._selectedItems,
        };
        console.log($(""#edOnayliListe"").dxForm(""instance"").getEditor(""YEAR"")._selectedItems);
        getLoadPanelInstance().show();
        $.ajax({
            type: ""POST"",
            url: '");
#nullable restore
#line 86 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimPlan\PlanOlustur.cshtml"
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
                    remoteOperations: false
                    
                    ,paging: {
                        pageSize: 50
                    },
                    sorting: {
                        mode: ""multiple""
                    }, export: {
                        enabled: true
                    },
                    filterRow: {
                        visible: true,
                        applyFilte");
            WriteLiteral(@"r: ""auto""
                    },
                     summary: {
                         recalculateWhileEditing: true,
                        skipEmptyValues: true,
                      totalItems: [{
                        column: 'PLANSIZ_ADET', 
                        summaryType: 'sum',
                        displayFormat: '{0}' , 
                      },
                      {
                        column: 'URETIM_ADET',
                        summaryType: 'sum',
                        displayFormat: '{0}'
                      },
                      {
                        column: 'PLANLANAN_ADET',
                        summaryType: 'sum',
                        displayFormat: '{0}'
                      }],
                    },onSelectionChanged(e) {
                          e.component.refresh(true);
                        },
                    headerFilter: { visible: true },
                    editing: {
                        mode: ""cell"",
     ");
            WriteLiteral("                   allowUpdating: ");
#nullable restore
#line 138 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimPlan\PlanOlustur.cshtml"
                                  Write(sCanEditPlan);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    }, columnChooser: {
                        enabled: true,
                        mode:""select""
                    }, customizeColumns: function (columns) {
                        for (var i = 0; i < YEARS.length; i++) {
                            columns.push({
                                caption: YEARS[i].YEAR,
                                isBand: true
                            });
                            for (var a = 0; a < columns.length - 1; a++) {
                                if (a < 21) {
                                    columns[a].fixed = true;
                                    columns[a].allowEditing = false;
                                }
                                if (columns[a].dataField == ""ID"" || columns[a].dataField == ""PRODUCT_GROUP"" || columns[a].dataField == ""MARKET_ID"" || columns[a].dataField == ""FABRIC_TYPE"" || columns[a].dataField == ""ROW_ID"") {
                                    columns[a].visible = false;
          ");
            WriteLiteral(@"                      }
                                else if (columns[a].dataField == ""MARKET"") {
                                    columns[a].width = 100;
                                }
                               else if (columns[a].dataField == ""DEADLINE"" ||columns[a].dataField == ""SHIPMENT_DATE"") {
                                    columns[a].width = 100;
                                    columns[a].dataType = ""date"";
                                }
                                else if (columns[a].dataField == ""DEADLINE_2"" ||columns[a].dataField == ""DEADLINE_3"" ||columns[a].dataField == ""DEADLINE_4"") {
                                    columns[a].visible = false;
                                    columns[a].dataType = ""date"";
                                }
                                else if (columns[a].dataField == ""PLANSIZ_ADET"" || columns[a].dataField == ""URETIM_ADET"" || columns[a].dataField == ""PLANLANAN_ADET"") {
                                    columns[a");
            WriteLiteral(@"].width = 130;
                                    columns[a].dataType = ""number"";
                                     
                                }
                                if (columns[a].dataField == ""PLANLANAN_ADET"") {
                                    columns[a].calculateCellValue = function (rowData) {
                                        var top = 0;
                                        for (var m = 0; m < COLUMNNAMES.length; m++) {
                                            if (rowData[COLUMNNAMES[m].NAME])
                                                top += rowData[COLUMNNAMES[m].NAME];
                                        }
                                        rowData.PLANLANAN_ADET = parseFloat(top);
                                        return rowData.PLANLANAN_ADET;
                                    }
                                }
                                if (columns[a].dataField == ""PLANSIZ_ADET"") {
                                    ");
            WriteLiteral(@"columns[a].calculateCellValue = function (rowData) {
                                        rowData.PLANSIZ_ADET = rowData.URETIM_ADET - rowData.PLANLANAN_ADET;
                                        return rowData.PLANSIZ_ADET;
                                    }
                                }
                                try {
                                    if (columns[a].dataField.startsWith(YEARS[i].YEAR)) {
                                        columns[a].summary = {
                                            totalItems: [{
                                                column: ""2021"",
                                                summaryType: ""sum"",
                                                alignment: ""center"",
                                                displayFormat: ""TOP: {0}""
                                            }]
                                        };
                                        columns[a].ownerBand = columns.length - 1;
       ");
            WriteLiteral(@"                                 columns[a].dataType = ""number"";
                                        columns[a].allowFiltering = false;
                                        columns[a].caption = columns[a].dataField.replace(YEARS[i].YEAR + '_', '') + "".HAFTA"";
                                        columns[a].showInColumnChooser=true;


                                    } else {

                                    }
                                } catch (e) {

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
      ");
            WriteLiteral("                      \"url\": \"");
#nullable restore
#line 224 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimPlan\PlanOlustur.cshtml"
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
                     onCellPrepared : function(e){
                        if(e.column.dataField==""CREATE_MINUTE""){
                            if(e.data){
                            if(e.data.CREATE_MINUTE==0){
                                e.cellElement[0].bgColor=""indianred"";
                            }}
                        }
                         if(e.column.dataField==""PLANSIZ_ADET""){
                            if(e.data){
                            if(e.data.PLANSIZ_ADET>0){
                                e.cellElement[0].bgColor=""indianred"";
                            }}
                        }
                        
                    },
                    focusedRowEnabled: true,
                    focusedRowIndex: 0,
                    focusedColumnIndex: 0 ,
                    showColumnLines: true,
                    showRowLines: true, 
   ");
            WriteLiteral("                 showBorders: true,\r\n                    onContentReady: function (e) { \r\n                         // e.component.refresh(true);\r\n                    },\r\n                    onCellDblClick: function (e) {\r\n                       if(");
#nullable restore
#line 275 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimPlan\PlanOlustur.cshtml"
                     Write(sCanEditPlan);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"){
                        var ok=false;
                        console.log(YEARS);
                        for(var i =0;i<YEARS.length;i++){
                            console.log(e);
                            if(e.column.name.startsWith(YEARS[i].YEAR))
                                ok=true;
                        }
                        if(ok){
                            e.component.cellValue(e.rowIndex, e.columnIndex, e.data[""PLANSIZ_ADET""]);
                            e.component.saveEditData();
                        }
                      }

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Plan_Filter> Html { get; private set; }
    }
}
#pragma warning restore 1591
