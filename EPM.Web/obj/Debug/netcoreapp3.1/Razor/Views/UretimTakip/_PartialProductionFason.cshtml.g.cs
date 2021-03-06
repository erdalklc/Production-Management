#pragma checksum "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ea40a53b2eb6aaae53be3856790aa129d8e60ce3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimTakip.Views_UretimTakip__PartialProductionFason), @"mvc.1.0.view", @"/Views/UretimTakip/_PartialProductionFason.cshtml")]
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
#line 2 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml"
using EPM.Fason.Dto.Fason;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea40a53b2eb6aaae53be3856790aa129d8e60ce3", @"/Views/UretimTakip/_PartialProductionFason.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimTakip__PartialProductionFason : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Tuple<PRODUCTION_HEADER, List<PRODUCTION_PROCESS>, List<PRODUCTION_FASON_USERS>>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 6 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml"
  
    var quantity = Model.Item1.DETAIL.Sum(ob => ob.QUANTITY);
    var dateval = Model.Item1.DEADLINE.ToString("dd.MM.yyyy");

#line default
#line hidden
#nullable disable
            WriteLiteral("<div id=\"form-container\">\r\n\r\n\r\n    <h6 style=\"margin:10px 0\">");
#nullable restore
#line 13 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml"
                         Write(Model.Item1.SEASON);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 13 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml"
                                             Write(Model.Item1.MODEL);

#line default
#line hidden
#nullable disable
            WriteLiteral(".");
#nullable restore
#line 13 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml"
                                                                Write(Model.Item1.COLOR);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 13 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml"
                                                                                   Write(quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" ADETL??K S??PAR???? ??????N B??LG??LER?? G??R??N??Z </h6>

    <div id=""formContainer"">

    </div>
    <div class=""dx-card wide-card"" style=""width:49%;min-height:400px;display:inline-block;float:left"">
        <div class=""dx-field"" style=""background-color:#03a9f4;margin:5px;padding:10px;color:white"">
            <div class=""dx-field-label"" style=""color:white"">S??RE?? L??STES??</div>
        </div>
        <div id=""plannedList"" style=""margin:5px""></div>
    </div>
    <div class=""dx-card wide-card"" style=""width: 49%;min-height:400px;display: inline-block; float: right"">
        <div class=""dx-field"" style=""background-color:#03a9f4;margin:5px; padding:10px;color:white"">
            <div class=""dx-field-label "" style=""color:white"">S??RE?? A??AMALARI</div>
        </div>
        <div id=""doingList"" style=""margin:5px""></div>
    </div>

</div>


<script>

    $(function () {

        $(""#formContainer"").dxForm({
            labelLocation: ""top"", // or ""left"" | ""right""
            colCount: 6,
           ");
            WriteLiteral(" items: [{\r\n                editorType: \"dxSelectBox\",\r\n                name: \"firmaBilgi\",\r\n                label: {\r\n                    text: \"F??RMA\"\r\n                },\r\n                editorOptions: {\r\n                    items: ");
#nullable restore
#line 48 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml"
                      Write(Html.Raw(Json.Serialize(Model.Item3)));

#line default
#line hidden
#nullable disable
            WriteLiteral(@",
                    value: 0, valueExpr: ""ID"",
                    icon: ""link"",
                    displayExpr: ""NAME"",
                }
            }, {
                editorType: ""dxDateBox"",
                name: ""terminTarihi"",
                label: {
                    text: ""TERM??N TAR??H??""
                }, editorOptions: {
                    min: new Date(),
                    displayFormat: ""yyyy.MM.dd"",
                    value: new Date(");
#nullable restore
#line 61 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml"
                               Write(Model.Item1.DEADLINE.Year);

#line default
#line hidden
#nullable disable
            WriteLiteral("+\",\" +");
#nullable restore
#line 61 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml"
                                                               Write(Model.Item1.DEADLINE.Month);

#line default
#line hidden
#nullable disable
            WriteLiteral("+\",\" +");
#nullable restore
#line 61 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml"
                                                                                                Write(Model.Item1.DEADLINE.Day);

#line default
#line hidden
#nullable disable
            WriteLiteral(@")
                }
            }, {
                itemType: ""empty""
            }, {
                itemType: ""empty""
            }, {
                itemType: ""empty""
            }, {
                itemType: ""button"",
                alignment: ""center"",
                buttonOptions: {
                    text: ""S??PAR???? OLU??TUR"",
                    type: ""default"",
                    icon: ""link"",
                    width: ""100%"",
                    onClick: function (e) {
                        var result = DevExpress.ui.dialog.confirm(""S??re?? Olu??turulup Firma Bilgilendirilecektir. Onayl??yor musunuz ?"", ""S??re?? Olu??turma"");
                        result.done(function (dialogResult) {
                            if (dialogResult) {
                                var firmaBilgi = $(""#formContainer"").dxForm(""instance"").getEditor(""firmaBilgi"").option(""value"");
                                var terminTarihi = $(""#formContainer"").dxForm(""instance"").getEditor(""terminTarihi"").op");
            WriteLiteral(@"tion(""text"");
                                var firmaBilgi = {
                                    FIRMA_ID: firmaBilgi,
                                    TERMIN_TARIHI: terminTarihi
                                }
                                if (firmaBilgi.FIRMA_ID > 0) {

                                    var formData = {
                                        header: ");
#nullable restore
#line 90 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml"
                                           Write(Json.Serialize(Model.Item1));

#line default
#line hidden
#nullable disable
            WriteLiteral(@",
                                        plan: others,
                                        formHeader: firmaBilgi
                                    }

                                    loadPanel.show();
                                    $.ajax({
                                        type: ""POST"",
                                        url: '");
#nullable restore
#line 98 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml"
                                         Write(Url.Action("FasonSiparisOlustur", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                                        data: formData,
                                        async: true,
                                        dataType: ""json"",
                                        //contentType: 'application/json',
                                        timeout: 180000,
                                        success: function (income, status, xmlRequest) {
                                            $(""#popup"").dxPopup(""hide"");
                                            loadPanel.hide();
                                        }, error: function (xhr, status, error) {

                                            DevExpress.ui.dialog.custom({ messageHtml: ""HATA : "" + xhr.status + ': ' + xhr.statusText, title: ""HATA"", buttons: [{ text: ""TAMAM"", }] }).show();
                                            $(""#popup"").dxPopup(""hide"");
                                            loadPanel.hide();
                                        }
                                ");
            WriteLiteral(@"    }).always(function () {
                                        loadPanel.hide();
                                    });
                                } else {

                                    DevExpress.ui.dialog.custom({ messageHtml: ""L??tfen firma bilgisi se??iniz"", title: ""HATA"", buttons: [{ text: ""TAMAM"", }] }).show();
                                }

                            }
                        });
                    }
                }
            }]
        });

    });
        var tasks = [];
        var others =  ");
#nullable restore
#line 130 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\UretimTakip\_PartialProductionFason.cshtml"
                 Write(Html.Raw(Json.Serialize(Model.Item2)));

#line default
#line hidden
#nullable disable
            WriteLiteral(@";
        var listPlanned = $(""#plannedList"").dxList({
            items: tasks, selectionMode: ""none"",
            repaintChangesOnly: true, focusStateEnabled: false,
            keyExpr: ""ID"",
            itemTemplate: function (data, index) {
                return $(""<div>"")
                    .append($(""<div style='display:inline-block;'>"").text(data.NAME))
                    .append($(""<div style='display:inline-block;width:50px;float:right' id='idText"" + data.ID + ""' data-value='"" + data.TIME + ""' data-sirano='"" + data.ID + ""' >""));
            },
            itemDragging: {
                allowReordering: true,
                group: ""tasks"",
                onDragStart: function (e) {
                    e.itemData = tasks[e.fromIndex];
                },
                onAdd: function (e) {
                    tasks.splice(e.toIndex, 0, e.itemData);
                    listPlanned.option(""items"", tasks);
                },
                onRemove: function (e) {
           ");
            WriteLiteral(@"         tasks.splice(e.fromIndex, 1);
                    listPlanned.option(""items"", tasks);
                }
            }, onContentReady: function () {
                for (var i = 0; i < tasks.length; i++) {
                    $(""#idText"" + tasks[i].ID + """").dxNumberBox({
                        showSpinButtons: true,
                        value: tasks[i].TIME,
                        hint: ""G??n"",
                        onValueChanged: function (data) {
                            for (var a = 0; a < tasks.length; a++) {
                                if (tasks[a].ID == parseFloat(data.element[0].id.replace(""idText"", """"))) {
                                    tasks[a].TIME = data.value;
                                }
                            }
                        }
                    });
                }

            }
        }).dxList(""instance"");

    var listOthers = $(""#doingList"").dxList({
        items: others, selectionMode: ""none"",
        repaintChang");
            WriteLiteral(@"esOnly: true,
        keyExpr: ""ID"",
        allowItemDeleting: true,
        itemDeleteMode: ""swipe"",
        itemTemplate: function (data, index) {
            return $(""<div>"")
                .append($(""<div style='display:inline-block;'>"").text(data.NAME))
                .append($(""<div style='display:inline-block;width:50px;float:right' id='idSecondText"" + data.ID + ""' data-value='"" + data.TIME + ""' data-sirano='"" + data.ID + ""' >""));
        },
        onItemDeleted: function (e) {
            if (e.itemData) {
                tasks.push(e.itemData);
                listPlanned.option(""items"", tasks);
                listOthers.option(""items"", others);
            }
        },
        itemDragging: {
            allowReordering: true,
            group: ""tasks"",
            onDragStart: function (e) {
                e.itemData = others[e.fromIndex];
            },
            onAdd: function (e) {
                others.splice(e.toIndex, 0, e.itemData);
                listOth");
            WriteLiteral(@"ers.option(""items"", others);
            },
            onRemove: function (e) {
                others.splice(e.fromIndex, 1);
                listOthers.option(""items"", others);
            }
        }, onContentReady: function () {
            for (var i = 0; i < others.length; i++) {
                $(""#idSecondText"" + others[i].ID + """").dxNumberBox({
                    showSpinButtons: true,
                    hint: ""G??n"",
                    value: others[i].TIME,
                    onValueChanged: function (data) {
                        for (var a = 0; a < others.length; a++) {
                            if (others[a].ID == parseFloat(data.element[0].id.replace(""idSecondText"", """"))) {
                                others[a].TIME = data.value;
                            }
                        }
                    }
                });
            }
        }
    }).dxList(""instance"");

    function inputChange(array, index, val) {
        if (array == 0) {
        ");
            WriteLiteral("    tasks[index].TIME = val;\r\n        } else {\r\n            others[index].TIME = val;\r\n        }\r\n    }\r\n    $(document).ready(function () {\r\n    });\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Tuple<PRODUCTION_HEADER, List<PRODUCTION_PROCESS>, List<PRODUCTION_FASON_USERS>>> Html { get; private set; }
    }
}
#pragma warning restore 1591
