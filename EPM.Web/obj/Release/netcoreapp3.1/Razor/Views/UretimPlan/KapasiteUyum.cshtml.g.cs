#pragma checksum "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\KapasiteUyum.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2662a9558cab0eb56f9555d33af4828eee699de2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimPlan.Views_UretimPlan_KapasiteUyum), @"mvc.1.0.view", @"/Views/UretimPlan/KapasiteUyum.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2662a9558cab0eb56f9555d33af4828eee699de2", @"/Views/UretimPlan/KapasiteUyum.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimPlan_KapasiteUyum : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\KapasiteUyum.cshtml"
   
    bool canEditPlan = Convert.ToBoolean(ViewData["CanEditPlan"]);
    string sCanEditPlan = "false";
    if(canEditPlan)
    {
        sCanEditPlan = "true";
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div id=\"form-container\">\r\n    ");
#nullable restore
#line 11 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\KapasiteUyum.cshtml"
Write(Html.Partial("_PartialKapasitePlanUyum"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    
        <div id=""popupDetay""></div>
</div>
 
<script>
    function GetYear() { 
        return jQuery(""[name=edYear]"").val();
    }

    function GetGroup() { 
        return jQuery(""[name=edProductGroup]"").val();
    }
    var datax = null;
    function Chart_Clicked(WEEK_VALUE,SERIES_NAME) {
        var data = {
            BAND: GetGroup(),
            YEAR: GetYear(),
            WEEK: WEEK_VALUE 
        };
        getLoadPanelInstance().show();
        console.log(SERIES_NAME);
        if(SERIES_NAME==""ÜRETİM GERÇEKLEŞEN""){
               $.ajax({
            type: ""POST"",
            url: '");
#nullable restore
#line 36 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\KapasiteUyum.cshtml"
             Write(Url.Action("_PartialGetUretimGerceklesenListByChart", "UretimPlan"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            async: true,
            success: function (income, status, xmlRequest) {
                var formData = JSON.parse(income);

                var popupOptions = {
                    contentTemplate: function () {
                      
                        return  ""<div id='edReport' style='height:calc(100vh - 100px)'></div>"";
                    },
                    width: function () { return ""1200px""; },
                    height: function () { return ""calc(100% - 20px)""; },
                    showTitle: true,
                    title: GetYear()+"" - ""+WEEK_VALUE +"". HAFTA ÜRETM GERÇEKLEŞEN DETAY"",
                    dragEnabled: true,
                    closeOnOutsideClick: true,
                    onContentReady: function () {
                        try { 
                            $(""#edReport"").dxDataGrid({
                            dataSource: formData,
           ");
            WriteLiteral(@"                 keyExpr: ""ROW_ID"",
                            loadPanel: {
                                enabled: true
                            },columnChooser: {
                                enabled: true
                            }, export: {
                                enabled: true
                            },
                            remoteOperations: false,
                            columns:[
                               {
                                    dataField:""MODEL_ADI"",
                                    caption:""MODEL""
                               },
                               {
                                    dataField:""RENK_ADI"",
                                    caption:""RENK""
                               },
                               {
                                    dataField:""BEDEN_ADI"",
                                    caption:""BEDEN""
                               },
                               {
           ");
            WriteLiteral(@"                         dataField:""SEZON_ADI"",
                                    caption:""SEZON""
                               },
                               {
                                    dataField:""PAZAR_ADI"",
                                    caption:""PAZAR""
                               },
                               {
                                    dataField:""SIPARIS_TIPI"",
                                    caption:""SİPARİŞ TİPİ""
                               },
                               {
                                    dataField:""KESIM_FOYU_NO"",
                                    caption:""KESİM FÖYÜ NO""
                               },
                               {
                                    dataField:""START_WEEK"",
                                    caption:""HAFTA""
                               },
                               {
                                    dataField:""START_YEAR"",
                                    capti");
            WriteLiteral(@"on:""YIL""
                               },
                               {
                                    dataField:""MIKTAR"",
                                    caption:""MİKTAR""
                               }

                            ],
                            paging: {
                                pageSize: 100
                            },
                            sorting: {
                                mode: ""multiple""
                            },summary: {
                              totalItems: [{
                                column: 'MIKTAR',
                                summaryType: 'sum',
                                displayFormat: '{0}'
                              }],
                            },
                            filterRow: {
                                visible: true,
                                applyFilter: ""auto""
                            },
                            headerFilter: { visible: true },
        ");
            WriteLiteral(@"                    focusedRowEnabled: true,
                            focusedRowIndex: 0,
                            focusedColumnIndex: 0 ,
                            showColumnLines: true,
                            showRowLines: true,
                            rowAlternationEnabled: true,
                            showBorders: true,
                            columnMinWidth: 80,
                            showBorders: true,
                        }).dxDataGrid(""instance""); 
                } catch (e) { }
                    }

                };

                $(""#popupDetay"").dxPopup(popupOptions).dxPopup(""instance"").show(); 


            }
            , beforeSend: function () { 
            }


        }).always(function () {
            getLoadPanelInstance().hide();
        });
        }
        else if(SERIES_NAME==""PLAN TOPLAM""){

             $.ajax({
            type: ""POST"",
            url: '");
#nullable restore
#line 158 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\KapasiteUyum.cshtml"
             Write(Url.Action("_PartialGetPlanListByChartBandGiris", "UretimPlan"));

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

                var popupOptions = {
                    contentTemplate: function () {
                      
                        return  ""<div id='edReport' style='height:calc(100vh - 100px)'></div>"";
                    },
                    width: function () { return ""calc(100% - 20px)""; },
                    height: function () { return ""calc(100% - 20px)""; },
                    showTitle: true,
                    title: GetYear()+"" - ""+WEEK_VALUE +"". HAFTA DETAY"",
                    dragEnabled: true,
                    closeOnOutsideClick: true,
                    onContentReady: function () {
                        try { $");
            WriteLiteral(@"(""#edReport"").dxDataGrid({
                    dataSource: formData,
                    keyExpr: ""ROW_ID"",
                    loadPanel: {
                        enabled: true
                    },columnChooser: {
                        enabled: true
                    }, export: {
                        enabled: true
                    },
                    remoteOperations: false
                    
                    ,paging: {
                        pageSize: 100
                    },
                    sorting: {
                        mode: ""multiple""
                    },summary: {
                      totalItems: [{
                        column: 'PLANSIZ_ADET',
                        summaryType: 'sum',
                        displayFormat: '{0}'
                      },
                      {
                        column: 'URETIM_ADET',
                        summaryType: 'sum',
                        displayFormat: '{0}'
                      },
");
            WriteLiteral(@"                      {
                        column: 'PLANLANAN_ADET',
                        summaryType: 'sum',
                        displayFormat: '{0}'
                      }],
                    },
                    filterRow: {
                        visible: true,
                        applyFilter: ""auto""
                    },
                    headerFilter: { visible: true },
                    editing: {
                        mode: ""cell"",
                        allowUpdating: ");
#nullable restore
#line 220 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\KapasiteUyum.cshtml"
                                  Write(sCanEditPlan);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    } , customizeColumns: function (columns) {
                        for (var i = 0; i < YEARS.length; i++) {
                            columns.push({
                                caption: YEARS[i].YEAR,
                                isBand: true
                            });

                            for (var a = 0; a < columns.length - 1; a++) {
                                if (a < 18) {
                                    columns[a].fixed = true;
                                    columns[a].allowEditing = false;
                                }
                                if (columns[a].dataField == ""ID"" || columns[a].dataField == ""PRODUCT_GROUP"" || columns[a].dataField == ""MARKET_ID"" || columns[a].dataField == ""FABRIC_TYPE"" || columns[a].dataField == ""ROW_ID"") {
                                    columns[a].visible = false;
                                }
                                else if (columns[a].dataField == ""MARKET"") {
              ");
            WriteLiteral(@"                      columns[a].width = 100;
                                }
                                else if (columns[a].dataField == ""DEADLINE"" ||columns[a].dataField == ""SHIPMENT_DATE"") {
                                    columns[a].width = 100;
                                    columns[a].dataType = ""date"";
                                }
                                else if (columns[a].dataField == ""PLANSIZ_ADET"" || columns[a].dataField == ""URETIM_ADET"" || columns[a].dataField == ""PLANLANAN_ADET"") {
                                    columns[a].width = 130;
                                    columns[a].dataType = ""number"";
                                }
                                if (columns[a].dataField == ""PLANLANAN_ADET"") {
                                    columns[a].calculateCellValue = function (rowData) {
                                        var top = 0;
                                        for (var m = 0; m < COLUMNNAMES.length; m++) {
          ");
            WriteLiteral(@"                                  if (rowData[COLUMNNAMES[m].NAME])
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
                                    if (columns[a].dataField.startsWith(YEARS[i].YEAR)) {
                                        colu");
            WriteLiteral(@"mns[a].summary = {
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
                                } catch (e) {

                                }
                            }
      ");
            WriteLiteral(@"                  }
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
#line 298 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\KapasiteUyum.cshtml"
                               Write(Url.Action("UpdateTaskBandGiris", "UretimPlan"));

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
                    },onCellPrepared : function(e){
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
                    rowAlter");
            WriteLiteral("nationEnabled: true,\r\n                    showBorders: true,\r\n                    onContentReady: function (e) {\r\n                    },\r\n                    onCellDblClick: function (e) {\r\n                      if(");
#nullable restore
#line 348 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\KapasiteUyum.cshtml"
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
                }).dxDataGrid(""instance""); } catch (e) { }
                    }

                };

                $(""#popupDetay"").dxPopup(popupOptions).dxPopup(""instance"").show(); 


            }
            , beforeSend: function () { 
            }


        }).always(function () {
           ");
            WriteLiteral(" getLoadPanelInstance().hide();\r\n        });\r\n        }else if(SERIES_NAME ==\"KAPASİTE\"){\r\n              $.ajax({\r\n            type: \"POST\",\r\n            url: \'");
#nullable restore
#line 385 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimPlan\KapasiteUyum.cshtml"
             Write(Url.Action("_PartialGeKapasiteListByChartBandGiris", "UretimPlan"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            async: true,
            success: function (income, status, xmlRequest) {
                var formData = JSON.parse(income);

                var popupOptions = {
                    contentTemplate: function () {
                      
                        return  ""<div id='edReport' style='height:calc(100vh - 100px)'></div>"";
                    },
                    width: function () { return ""900px""; },
                    height: function () { return ""calc(100% - 300px)""; },
                    showTitle: true,
                    title: GetYear()+"" - ""+WEEK_VALUE +"". HAFTA KAPASİTE DETAY"",
                    dragEnabled: true,
                    closeOnOutsideClick: true,
                    onContentReady: function () {
                        try { 
                            $(""#edReport"").dxDataGrid({
                            dataSource: formData,
                    ");
            WriteLiteral(@"        keyExpr: ""ROW_ID"",
                            loadPanel: {
                                enabled: true
                            },columnChooser: {
                                enabled: true
                            }, export: {
                                enabled: true
                            },
                            remoteOperations: false,
                            columns:[
                               {
                                    dataField:""MODEL"",
                                    caption:""MODEL""
                               },
                               {
                                    dataField:""WEEK"",
                                    caption:""HAFTA""
                               },
                               {
                                    dataField:""YEAR"",
                                    caption:""YIL""
                               },
                               {
                                  ");
            WriteLiteral(@"  dataField:""QUANTITY"",
                                    caption:""PLAN TOPLAM""
                               },
                               {
                                    dataField:""SURE"",
                                    caption:""ÜRÜN YAPILIŞ SÜRESİ""
                               },
                               {
                                    dataField:""KISI"",
                                    caption:""BANT KİŞİ SAYISI""
                               },
                               {
                                    dataField:""WORK_TIME"",
                                    caption:""BANT ÇALIŞMA SÜRESİ""
                               },
                               {
                                    dataField:""EXPECTED_QUANTITY"",
                                    caption:""BEKLENEN ÜRETİM KAPASİTESİ""
                               }

                            ],
                            height:""100%"",
                            paging: {
   ");
            WriteLiteral(@"                             pageSize: 10
                            },
                            sorting: {
                                mode: ""multiple""
                            },summary: {
                              totalItems: [{
                                column: 'EXPECTED_QUANTITY',
                                summaryType: 'sum',
                                displayFormat: '{0}'
                              },
                              {
                                column: 'WORK_TIME',
                                summaryType: 'sum',
                                displayFormat: '{0}'
                              },
                              {
                                column: 'KISI',
                                summaryType: 'sum',
                                displayFormat: '{0}'
                              }],
                            },
                            pager: {
                              visible: true,
");
            WriteLiteral(@"                              allowedPageSizes: [5, 10, 'all'],
                              showPageSizeSelector: true,
                              showInfo: true,
                              showNavigationButtons: true,
                            },
                            filterRow: {
                                visible: true,
                                applyFilter: ""auto""
                            },
                            headerFilter: { visible: true },
                            focusedRowEnabled: true,
                            focusedRowIndex: 0,
                            focusedColumnIndex: 0 ,
                            showColumnLines: true,
                            showRowLines: true,
                            rowAlternationEnabled: true,
                            showBorders: true,
                            columnMinWidth: 80,
                            showBorders: true,
                        }).dxDataGrid(""instance""); 
           ");
            WriteLiteral(@"     } catch (e) { }
                    }

                };

                $(""#popupDetay"").dxPopup(popupOptions).dxPopup(""instance"").show(); 


            }
            , beforeSend: function () { 
            }


        }).always(function () {
            getLoadPanelInstance().hide();
        });
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
