#pragma checksum "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2cf09e179969bdcdcb6526c7c48bb33f43541058"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimTakip.Views_UretimTakip__PartialUretimTakipFason), @"mvc.1.0.view", @"/Views/UretimTakip/_PartialUretimTakipFason.cshtml")]
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
#line 5 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
using EPM.Fason.Dto.Fason;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2cf09e179969bdcdcb6526c7c48bb33f43541058", @"/Views/UretimTakip/_PartialUretimTakipFason.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimTakip__PartialUretimTakipFason : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Tuple<PRODUCTION_HEADER, List<PRODUCTION_PROCESS>, List<PRODUCTION_FASON_USERS>>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 7 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
   
    var quantity = Model.Item1.DETAIL.Sum(ob => ob.QUANTITY);
    var dateval = Model.Item1.DEADLINE.ToString("dd.MM.yyyy");

#line default
#line hidden
#nullable disable
            WriteLiteral("<div id=\"form-container\">\r\n   \r\n    <h6 style=\"margin:10px 0\">");
#nullable restore
#line 13 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
                         Write(Model.Item1.SEASON);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 13 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
                                             Write(Model.Item1.MODEL);

#line default
#line hidden
#nullable disable
            WriteLiteral(".");
#nullable restore
#line 13 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
                                                                Write(Model.Item1.COLOR);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 13 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
                                                                                   Write(quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" ADETLİK SİPARİŞ İÇİN BİLGİLERİ GİRİNİZ </h6>
    <div id=""formContainer""></div>

    <div class=""dx-card wide-card"" style=""width:49%;min-height:400px;display:inline-block;float:left"">
        <div class=""dx-field"" style=""background-color:#03a9f4;margin:5px;padding:10px;color:white"">
            <div class=""dx-field-label"" style=""color:white"">SÜREÇ LİSTESİ</div>
        </div>
        <div id=""plannedList"" style=""margin:5px""></div>
    </div>
    <div class=""dx-card wide-card"" style=""width: 49%;min-height:400px;display: inline-block; float: right"">
        <div class=""dx-field"" style=""background-color:#03a9f4;margin:5px; padding:10px;color:white"">
            <div class=""dx-field-label "" style=""color:white"">SÜREÇ AŞAMALARI</div>
        </div>
        <div id=""doingList"" style=""margin:5px""></div>
    </div>
</div>


<script>

    $(function () {
        $(""#formContainer"").dxForm({
            labelLocation: ""top"", // or ""left"" | ""right""
            colCount: 6,
            items: [{
");
            WriteLiteral("                editorType: \"dxSelectBox\",\r\n                name: \"firmaBilgi\",\r\n                label: {\r\n                    text: \"FİRMA\"\r\n                },\r\n                editorOptions: {\r\n                    items: ");
#nullable restore
#line 44 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
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
                    text: ""TERMİN TARİHİ""
                }, editorOptions: {
                    min: new Date(),
                    displayFormat: ""dd.MM.yyyy"",
                    value: new Date(");
#nullable restore
#line 57 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
                               Write(Model.Item1.DEADLINE.Year);

#line default
#line hidden
#nullable disable
            WriteLiteral("+\",\" +");
#nullable restore
#line 57 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
                                                               Write(Model.Item1.DEADLINE.Month);

#line default
#line hidden
#nullable disable
            WriteLiteral("+\",\" +");
#nullable restore
#line 57 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
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
                    text: ""SİPARİŞ OLUŞTUR"",
                    type: ""default"",
                    icon: ""link"",
                    width: ""100%"",
                    onClick: function (e) {
                        var result = DevExpress.ui.dialog.confirm(""Süreç Oluşturulup Firma Bilgilendirilecektir. Onaylıyor musunuz ?"", ""Süreç Oluşturma"");
                        result.done(function (dialogResult) {
                            if (dialogResult) {
                                var firmaBilgi = $(""#formContainer"").dxForm(""instance"").getEditor(""firmaBilgi"").option(""value"");
                                var terminTarihi = $(""#formContainer"").dxForm(""instance"").getEditor(""terminTarihi"").op");
            WriteLiteral("tion(\"text\");\r\n                                var formData = {\r\n                                    header: ");
#nullable restore
#line 80 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
                                       Write(Json.Serialize(Model.Item1));

#line default
#line hidden
#nullable disable
            WriteLiteral(@",
                                    plan: others,
                                    firmaBilgi: firmaBilgi,
                                    terminTarihi: terminTarihi
                                }
                                console.log(formData);
                                $.ajax({
                                    type: ""POST"",
                                    url: '");
#nullable restore
#line 88 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
                                     Write(Url.Action("FasonSiparisOlusturAsync2", "UretimTakip"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                                    data: formData,
                                    async: false,
                                    contentType: 'application/json',
                                    timeout: 180000,
                                    success: function (income, status, xmlRequest) {
                                        $(""#edReportDetail"").html(income);

                                    }, beforeSend: function () {
                                    }


                                }).always(function () {
                                });
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
#line 111 "D:\Projects\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
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
                tasks.splice(e.fromIndex, 1);
                listPlanned.option(""items"", ");
            WriteLiteral(@"tasks);
            }
        }, onContentReady: function () {
            for (var i = 0; i < tasks.length; i++) {
                $(""#idText"" + tasks[i].ID + """").dxNumberBox({
                    showSpinButtons: true,
                    value: tasks[i].TIME,
                    hint: ""Gün"",
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
        repaintChangesOnly: true,
        keyExpr: ""ID"",
        allowItemDeleting: true,
        itemDeleteMode: ""swipe"",
        itemTemplate: function (data, index) {
     ");
            WriteLiteral(@"       return $(""<div>"")
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
                listOthers.option(""items"", others);
            },
            onRemove: function (e) {
                others.splice(e.fromIndex, 1);
                listOthers.o");
            WriteLiteral(@"ption(""items"", others);
            }
        }, onContentReady: function () {
            for (var i = 0; i < others.length; i++) {
                $(""#idSecondText"" + others[i].ID + """").dxNumberBox({
                    showSpinButtons: true,
                    hint:""Gün"",
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
            tasks[index].TIME = val;
        } else {
            others[index].TIME = val;
        }
    }
    $(document).ready(function () {
    });
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Tuple<PRODUCTION_HEADER, List<PRODUCTION_PROCESS>, List<PRODUCTION_FASON_USERS>>> Html { get; private set; }
    }
}
#pragma warning restore 1591
