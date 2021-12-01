#pragma checksum "E:\Eren\Eren Production Management\EPM.Web\Views\UretimPlan\_PartialKapasitePlanUyum.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0e84ee51a5d236206bccbf5ac652f0be8b96fecf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.UretimPlan.Views_UretimPlan__PartialKapasitePlanUyum), @"mvc.1.0.view", @"/Views/UretimPlan/_PartialKapasitePlanUyum.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e84ee51a5d236206bccbf5ac652f0be8b96fecf", @"/Views/UretimPlan/_PartialKapasitePlanUyum.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_UretimPlan__PartialKapasitePlanUyum : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimPlan\_PartialKapasitePlanUyum.cshtml"
   
    int[] years = { DateTime.Now.Year - 1, DateTime.Now.Year, DateTime.Now.Year + 1 };

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"dx-card wide-card\">\r\n    ");
#nullable restore
#line 5 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimPlan\_PartialKapasitePlanUyum.cshtml"
Write(Html.DevExtreme().Form()
        .ID("edFilter").ShowValidationSummary(true)
        .ColCount(8)
        .Items(items =>
        {
            items.AddEmpty();
            items.AddEmpty();
            items.AddEmpty();
            items.AddSimple().Editor(ob => ob.Lookup().Name("edProductGroup").OnSelectionChanged("RefreshChart").DataSource(d => d.Mvc().LoadParams(new { all = false }).Controller("Uretim").LoadAction("GetBandList").Key("ID")).DisplayExpr("ADI").ValueExpr("ID")).IsRequired(false);
            items.AddSimple().Editor(ob => ob.Lookup().Name("edYear").OnSelectionChanged("RefreshChart").DataSource(d => d.Array().Data(years)));
            items.AddEmpty();
            items.AddEmpty(); 
            items.AddEmpty();

        })
        .FormData(Model).ShowValidationSummary(false)
    );

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n</div>   \r\n<div style=\"clear:both\">\r\n    <div class=\"dx-card wide-card\">\r\n    <div style=\"width:88%;max-width:100%;\">\r\n        ");
#nullable restore
#line 28 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimPlan\_PartialKapasitePlanUyum.cshtml"
    Write(Html.DevExtreme().Chart()
    .ID("edChart")
    .Title(t=>t.Text("Plan Kapasite Uyum"))
    .CommonSeriesSettings(s => s
        .ArgumentField("WEEK")
        .Label(l=>l.Visible(true).ShowForZeroValues(false).Format(x=>x.Type(Format.FixedPoint).Precision(0)))
        .Type(SeriesType.Bar)
        .HoverMode(ChartSeriesHoverMode.AllArgumentPoints)
        .SelectionMode(ChartSeriesSelectionMode.AllArgumentPoints)

    ).OnPointClick(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral("\r\n        function (e) {\r\n                        Chart_Clicked(e.target.data.WEEK);\r\n                    }\r\n    ");
    PopWriter();
}
)) 
    .Series(s =>
    {
        s.Add().ValueField("QUANTITY").Name("PLAN TOPLAM").Type(SeriesType.Line).Label(l=>l.Visible(true).CustomizeText(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral("function(e){console.log(e);if(e.value==0) return \"\"; else return e.value;}");
    PopWriter();
}
)).Font(f=>f.Size("9"))).HoverMode(ChartSeriesHoverMode.IncludePoints); 
        s.Add().ValueField("CAPACITY_RELEASED").Label(l=>l.Visible(false).Font(f=>f.Size("10px"))).Name("KAPASİTE").Type(SeriesType.StackedBar).HoverMode(ChartSeriesHoverMode.OnlyPoint); 
        
    }) 
    .Legend(l => l
        .VerticalAlignment(VerticalEdge.Bottom)
        .HorizontalAlignment(HorizontalAlignment.Center)
        .ItemTextPosition(Position.Top)
    ).ArgumentAxis(a => a.Type(AxisScaleType.Discrete)
       .VisualRange(range => range.StartValue(1).EndValue(52))
    )
    .Crosshair(c=>c.Enabled(true)
    .Color("#949494")
    .Width(3)
    .DashStyle(DashStyle.LongDash)
    .Label(l=> l.Visible(true).Format(x=>x.Type(Format.FixedPoint).Precision(0)).BackgroundColor("red")
    .Font(f=>f.Color("#ffff").Size(12)) ) )
    .DataSource(ob=>
        ob.Mvc()
        .Controller("UretimPlan")
        .LoadAction("KapasitePlanUyumLoad")
        .LoadParams(new { YEAR = new JS("GetYear"), BAND_GROUP = new JS("GetGroup") })
        )
);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>  \r\n    </div>  \r\n</div>\r\n\r\n<div class=\"dx-card wide-card\">\r\n    <div style=\"width:88%;max-width:100%;\">\r\n        ");
#nullable restore
#line 75 "E:\Eren\Eren Production Management\EPM.Web\Views\UretimPlan\_PartialKapasitePlanUyum.cshtml"
    Write(Html.DevExtreme().Chart()
    .ID("edChart2")
    .Title(t=>t.Text("Kapasite Doluluk Oranı"))
    .CommonSeriesSettings(s => s
        .ArgumentField("WEEK")
        .Label(l=>l.Visible(true).Format(x=>x.Type(Format.Percent).Precision(0)))
        .Type(SeriesType.Bar)
        .HoverMode(ChartSeriesHoverMode.AllArgumentPoints)
        .SelectionMode(ChartSeriesSelectionMode.AllArgumentPoints)

    ).OnPointClick(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral("\r\n        function (e) {\r\n                        Chart_Clicked(e.target.data.WEEK);\r\n                    }\r\n    ");
    PopWriter();
}
))
    .RedrawOnResize(true)
    .AdjustOnZoom(true)
    .Series(s =>
    {
        s.Add().ValueField("PERFORMANS").Label(l=>l.Visible(true).CustomizeText(item => new global::Microsoft.AspNetCore.Mvc.Razor.HelperResult(async(__razor_template_writer) => {
    PushWriter(__razor_template_writer);
    WriteLiteral("function(e){console.log(e);if(e.value==0) return \"\"; else return parseFloat(e.value * 100).toFixed(2) +\" %\";}");
    PopWriter();
}
)).Font(f=>f.Size("9"))).Name("PERFORMANS"); 
    })
    .Legend(l => l
        .VerticalAlignment(VerticalEdge.Bottom)
        .HorizontalAlignment(HorizontalAlignment.Center)
        .ItemTextPosition(Position.Top)
    ).ArgumentAxis(a => a.Type(AxisScaleType.Discrete)
       .VisualRange(range => range.StartValue(1).EndValue(52))
    )
    .DataSource(ob=>
        ob.Mvc()
        .Controller("UretimPlan")
        .LoadAction("KapasitePlanPerformansLoad")
        .LoadParams(new { YEAR = new JS("GetYear"), BAND_GROUP = new JS("GetGroup") })
        )
);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    </div>
</div>
<script>
   
    function GetYear() { 
        return jQuery(""[name=edYear]"").val();
    }
    function RefreshChart() {
        $(""#edChart"").dxChart(""getDataSource"").reload();
        $(""#edChart2"").dxChart(""getDataSource"").reload();
    }
    function GetGroup() { 
        return jQuery(""[name=edProductGroup]"").val();
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
