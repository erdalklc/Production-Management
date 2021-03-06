#pragma checksum "D:\GitProjects\Eren-Production-Management\EPM.Fason.Web\Views\Inspection\_PartialInspectionInlineAQL.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c99a2dac94b0bc29f76cbb27a686fdf68baedf67"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Inspection.Views_Inspection__PartialInspectionInlineAQL), @"mvc.1.0.view", @"/Views/Inspection/_PartialInspectionInlineAQL.cshtml")]
namespace EPM_Web.Models.Inspection
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
#line 1 "D:\GitProjects\Eren-Production-Management\EPM.Fason.Web\Views\_ViewImports.cshtml"
using EPM_Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\GitProjects\Eren-Production-Management\EPM.Fason.Web\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c99a2dac94b0bc29f76cbb27a686fdf68baedf67", @"/Views/Inspection/_PartialInspectionInlineAQL.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Inspection__PartialInspectionInlineAQL : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<int>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("<div class=\"dx-card wide-card\">\r\n    ");
#nullable restore
#line 4 "D:\GitProjects\Eren-Production-Management\EPM.Fason.Web\Views\Inspection\_PartialInspectionInlineAQL.cshtml"
Write(Html.DevExtreme().DataGrid<EPM.Fason.Dto.Fason.PRODUCTION_AQL_INLINE>().ID("edgridHeader")
                    .ShowBorders(false)
                    .DataSource(d => d.Mvc()
        .Controller("Inspection")
        .LoadAction("GetInspectionInlineAQL")
        .LoadParams(new { ENTEGRATION_HEADER_ID = Model })
        .InsertAction("InsertInspectionLine")
        .UpdateAction("UpdateInspectionLine")
        .DeleteAction("DeleteInspectionLine")
        .Key("ID")
    )
    .FocusedRowEnabled(true)
    .FilterRow(x=>x.Visible(true))
    .FocusedRowIndex(0)
    .Selection(s => s.Mode(SelectionMode.Single)) 
    .ColumnAutoWidth(true)
    .ShowBorders(true)
    .Height("500px")
    .Scrolling(sc=>sc.Mode(GridScrollingMode.Virtual))
    .HeaderFilter(x=>x.Visible(true))
    .Editing(ob => ob.Mode(GridEditMode.Cell).StartEditAction(GridStartEditAction.Click).AllowUpdating(true).AllowDeleting(true).AllowAdding(true))
    .ColumnResizingMode(ColumnResizingMode.Widget)
    .AllowColumnResizing(true)
    .AllowColumnReordering(true)
    .ShowColumnLines(true)
    .ColumnChooser(ch => { ch.Mode(GridColumnChooserMode.Select);ch.Enabled(true); })
    .ShowColumnHeaders(true)
    .ColumnMinWidth(60)
    .OnInitNewRow(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral("\r\n    function(e){ \r\n    e.data.HEADER_ID = ");
#nullable restore
#line 34 "D:\GitProjects\Eren-Production-Management\EPM.Fason.Web\Views\Inspection\_PartialInspectionInlineAQL.cshtml"
                  Write(Model);

#line default
#line hidden
#nullable disable
    WriteLiteral("\r\n    }\r\n    ");
    PopWriter();
}
))
    .Columns(columns =>
    {
        columns.Add().Width(50).Type(GridCommandColumnType.Buttons).Buttons(btn =>
        {
            btn.Add().Name(GridColumnButtonName.Delete);
        });

        columns.AddFor(m => m.ID).Width(60).AllowEditing(false);
        columns.AddFor(m => m.PROCESS_ID).Width(110).Lookup(look => look.DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Inspection").LoadAction("GetProcessList").Key("ID")).DisplayExpr("NAME").ValueExpr("ID"));
        columns.AddFor(m => m.DESCRIPTION);
    })
    );

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<int> Html { get; private set; }
    }
}
#pragma warning restore 1591
