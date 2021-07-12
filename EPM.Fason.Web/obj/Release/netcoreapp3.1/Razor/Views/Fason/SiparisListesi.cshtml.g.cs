#pragma checksum "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\Fason\SiparisListesi.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "20e77e85d461d5e5b47266c6dfd0762fb7cea342"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.Fason.Views_Fason_SiparisListesi), @"mvc.1.0.view", @"/Views/Fason/SiparisListesi.cshtml")]
namespace EPM_Web.Models.Fason
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
#line 1 "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\_ViewImports.cshtml"
using EPM_Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"20e77e85d461d5e5b47266c6dfd0762fb7cea342", @"/Views/Fason/SiparisListesi.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_Fason_SiparisListesi : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EPM.Fason.Dto.Fason.SIPARIS_FILTER>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\Fason\SiparisListesi.cshtml"
  
    ViewData["Title"] = "Fason Sipariş Listesi";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n<div id=\"form-container\">\r\n    <div class=\"dx-card wide-card\">\r\n        ");
#nullable restore
#line 11 "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\Fason\SiparisListesi.cshtml"
    Write(Html.DevExtreme().Form<EPM.Fason.Dto.Fason.SIPARIS_FILTER>()
        .ID("edOnayliListe").ShowValidationSummary(true)
        .ColCount(Mode.Auto)
        .MinColWidth(150)
        .Items(items =>
        {
            items.AddSimpleFor(m => m.BASLANGIC_TARIHI).Editor(ob => ob.DateBox()).IsRequired(false);
            items.AddSimpleFor(m => m.BITIS_TARIHI).Editor(ob => ob.DateBox()).IsRequired(false);
            items.AddSimpleFor(m => m.MODEL).Editor(ob=>ob.TextBox().ID("edModel")).IsRequired(false);
            items.AddSimpleFor(m => m.COLOR).Editor(ob=>ob.TextBox().ID("edColor")).IsRequired(false);
            items.AddEmpty();
            items.AddButton().HorizontalAlignment(HorizontalAlignment.Right).VerticalAlignment(VerticalAlignment.Center)
                   .ButtonOptions(b => b.Text("FİLTRELE")
                       .Type(ButtonType.Default)
                       .Icon("fas fa-sign-in-alt")
                       .Width("100%")
                       .OnClick("FasonSiparisFiltrele")
                       .UseSubmitBehavior(true)
               );

        })
        .FormData(Model).ShowValidationSummary(false)
    );

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        <div id=""popup""></div>
        <div id=""popupDetay""></div>
        <div id=""edListe"" style=""display:inline-block;float:left; width:70%""> </div>
        <div id=""edListeDetail"" style=""display: inline-block; float: right; width: 29%""> </div>
        <div id=""edListeProcess""></div>
    </div>
</div>


<script>
    function FasonSiparisFiltrele() {
        $(""#edListe"").html("""");
        var data = {
            BASLANGIC_TARIHI: jQuery(""[name=BASLANGIC_TARIHI]"").val(),
            BITIS_TARIHI: jQuery(""[name=BITIS_TARIHI]"").val(),
            MODEL: jQuery(""[name=MODEL]"").val(),
            COLOR: jQuery(""[name=COLOR]"").val(),
        };
        $.ajax({
            type: ""POST"",
            url: '");
#nullable restore
#line 54 "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\Fason\SiparisListesi.cshtml"
             Write(Url.Action("_PartialSiparisListesi", "Fason"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            timeout: 180000,
            success: function (income, status, xmlRequest) {
                $(""#edListe"").html(income);

            }, beforeSend: function () {
                getLoadPanelInstance().show();
            }


        }).always(function () {
            getLoadPanelInstance().hide();
        });
    }

    function ProcessListGetir(ENTEGRATION_ID) {
         var dataProcess = {
             ENTEGRATION_HEADER_ID: ENTEGRATION_ID
        };
        $.ajax({
            type: ""POST"",
            url: '");
#nullable restore
#line 77 "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\Fason\SiparisListesi.cshtml"
             Write(Url.Action("_PartialSiparisProcessList", "Fason"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: dataProcess,
            contentType: 'application/x-www-form-urlencoded',
            timeout: 180000,
            success: function (income, status, xmlRequest) {
                $(""#edListeProcess"").html(income);

            }, beforeSend: function () {
            }


        }).always(function () {
        });
    }
    function gridFocusedRowChanged(e) {
        var data = {
            ENTEGRATION_HEADER_ID: e.row.data.ENTEGRATION_ID
        };
        $.ajax({
            type: ""POST"",
            url: '");
#nullable restore
#line 97 "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\Fason\SiparisListesi.cshtml"
             Write(Url.Action("_PartialSiparisListesiDetail", "Fason"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            data: data,
            contentType: 'application/x-www-form-urlencoded',
            timeout: 180000,
            success: function (income, status, xmlRequest) {
                $(""#edListeDetail"").html(income);

            }, beforeSend: function () {
            }


        }).always(function () {
        });
        ProcessListGetir(e.row.data.ENTEGRATION_ID);

    }

    function cellPrepared(e) {

        if (e.rowType == ""data"" && e.columnIndex == 0) {
            if (e.data.STATUS == 1) {
            } else if (e.data.STATUS == 2) {
                //e.cellElement[0].innerHTML = e.cellElement[0].innerHTML.replace(""video"", ""todo"");
            } else {
                 e.cellElement[0].innerHTML = """";
            }
        }

    }
    function cellClicked(e) {
        var soru = """";
        if (e.row.data.STATUS == 1)
            soru = ""SÜREÇ BAŞLATILACAK<br>Devam edilsin mi?"";
        else if (e.row.data.STATUS == 2)
            soru = ""SÜREÇ SONLAN");
            WriteLiteral(@"DIRILACAK<br>Devam edilsin mi?"";
        var result = DevExpress.ui.dialog.confirm(""<i>"" + soru + ""</i>"", ""ONAY"");
        result.done(function (dialogResult) {
            if (dialogResult) {
                var dataProcess = {
                    list: e.row.data
                };
                $.ajax({
                    type: ""POST"",
                    url: '");
#nullable restore
#line 140 "D:\Projects\Eren Production Management\EPM.Fason.Web\Views\Fason\SiparisListesi.cshtml"
                     Write(Url.Action("SurecIlerlet", "Fason"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                    data: dataProcess,
                    contentType: 'application/x-www-form-urlencoded',
                    timeout: 180000,
                    success: function (income, status, xmlRequest) {
                        console.log(income);
                        if (income[0]) {
                            ProcessListGetir(e.row.data.ENTEGRATION_ID);
                        } else {
                            DevExpress.ui.dialog.confirm(""İŞLEMLER YAPILIRKEN HATAYLA KARŞILAŞILDI<br>"" + income[1], ""HATA"");
                        }
                    }, beforeSend: function () {
                    }


                }).always(function () {
                });
            }
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EPM.Fason.Dto.Fason.SIPARIS_FILTER> Html { get; private set; }
    }
}
#pragma warning restore 1591