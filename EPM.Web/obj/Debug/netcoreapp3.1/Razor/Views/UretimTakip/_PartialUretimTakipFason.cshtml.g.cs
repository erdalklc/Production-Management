#pragma checksum "E:\Eren\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "405ed421eefe2bbf508253b73af3d1c233086a93"
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
#nullable restore
#line 7 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
using EPM.Core.FormModels.FasonTakip;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"405ed421eefe2bbf508253b73af3d1c233086a93", @"/Views/UretimTakip/_PartialUretimTakipFason.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimTakip__PartialUretimTakipFason : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Tuple<PRODUCTION_HEADER, List<PRODUCTION_PROCESS>, List<PRODUCTION_FASON_USERS>>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n");
            WriteLiteral(@" 
<div id=""form-container"">
    <div id=""formContainer""></div>
     
    <div class=""dx-card wide-card"" style=""width:49%;min-height:400px;display:inline-block;float:left"">
        <div class=""dx-field"" style=""background-color:#03a9f4;margin:5px;padding:10px;color:white"">
            <div class=""dx-field-label"" style=""color:white"" >SÜREÇ LİSTESİ</div>
        </div>
        <div id=""plannedList"" style=""margin:5px""></div>
    </div>
    <div class=""dx-card wide-card"" style=""width: 49%;min-height:400px;display: inline-block; float: right"">
        <div class=""dx-field"" style=""background-color:#03a9f4;margin:5px; padding:10px;color:white"">
            <div class=""dx-field-label "" style=""color:white"" >SÜREÇ AŞAMALARI</div>
        </div>
        <div id=""doingList"" style=""margin:5px""></div>
    </div>
</div>


<script>

    $(function () {
        $(""#formContainer"").dxForm({ 
            labelLocation: ""top"", // or ""left"" | ""right""
            colCount:6,
            items: [{
          ");
            WriteLiteral("      dataField: \"firmaBilgi\",\r\n                editorType: \"dxSelectBox\", \r\n                editorOptions: {\r\n                    items: ");
#nullable restore
#line 37 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
                      Write(Html.Raw(Json.Serialize(Model.Item3)));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n                    value: 0, valueExpr: \"ID\",\r\n                    displayExpr: \"NAME\",\r\n                }\r\n            }]\r\n        });\r\n    }); \r\n    var tasks = ");
#nullable restore
#line 44 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimTakip\_PartialUretimTakipFason.cshtml"
           Write(Html.Raw(Json.Serialize(Model.Item2)));

#line default
#line hidden
#nullable disable
            WriteLiteral(@";
    var listPlanned = $(""#plannedList"").dxList({
        items: tasks, selectionMode: ""none"",
        height: 400,
        repaintChangesOnly: true, focusStateEnabled: false,
        keyExpr: ""ID"",
        itemTemplate: function (data, index) {
            return $(""<div>"")
                .append($(""<div style='display:inline-block;width:49%'>"").text(data.NAME))
                .append($(""<div style='display:inline-block;width:49%' id='idText"" + data.ID + ""' data-value='"" + data.TIME + ""' data-sirano='"" + data.ID + ""' >"")); 
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
                listPlann");
            WriteLiteral(@"ed.option(""items"", tasks);
            }
        }, onContentReady: function () {
            for (var i = 0; i < tasks.length; i++) {
                $(""#idText"" + tasks[i].ID + """").dxTextBox({
                    value: tasks[i].TIME,
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

    var others = [];
    var listOthers = $(""#doingList"").dxList({
        items: others, selectionMode: ""none"",
        repaintChangesOnly: true, height: 400,
        keyExpr: ""ID"",
        itemTemplate: function (data, index) {
            return $(""<div>"")
                .append($(""<div style='display:inline-block;width:49");
            WriteLiteral(@"%'>"").text(data.NAME))
                .append($(""<div style='display:inline-block;width:49%' id='idSecondText"" + data.ID + ""' data-value='"" + data.TIME + ""' data-sirano='"" + data.ID + ""' >""));
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
                listOthers.option(""items"", others);
            }
        }, onContentReady: function () {
            for (var i = 0; i < others.length; i++) {
                $(""#idSecondText"" + others[i].ID + """").dxTextBox({
                    value: others[i].TIME,
                    onValueChanged: function (data) {
                        for (var a = 0");
            WriteLiteral(@"; a < others.length; a++) {
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
