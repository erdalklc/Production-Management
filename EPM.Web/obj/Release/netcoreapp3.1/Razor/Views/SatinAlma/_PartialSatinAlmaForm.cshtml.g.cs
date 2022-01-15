#pragma checksum "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\SatinAlma\_PartialSatinAlmaForm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0e5b49ac4d7d86e6b358877b49bf70022ef3441d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EPM_Web.Models.SatinAlma.Views_SatinAlma__PartialSatinAlmaForm), @"mvc.1.0.view", @"/Views/SatinAlma/_PartialSatinAlmaForm.cshtml")]
namespace EPM_Web.Models.SatinAlma
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e5b49ac4d7d86e6b358877b49bf70022ef3441d", @"/Views/SatinAlma/_PartialSatinAlmaForm.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63f34ed16a806b7cdcb157e2caccd86ef5edb5c0", @"/Views/_ViewImports.cshtml")]
    public class Views_SatinAlma__PartialSatinAlmaForm : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\SatinAlma\_PartialSatinAlmaForm.cshtml"
  
    string[] allowed = new string[] { ".xlsx", ".xls" };

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"widget-container\">\r\n    ");
#nullable restore
#line 6 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\SatinAlma\_PartialSatinAlmaForm.cshtml"
Write(Html.DevExtreme().FileUploader()
            .ID("file-uploader")
            .Name("myFile")
            .Multiple(false)
            .InvalidFileExtensionMessage("Dosya tipi desteklenmiyor")
            .AllowedFileExtensions(allowed).LabelText("Yada Excel Dosyasını Sürükleyip Bırakınız")
            .UploadMode(FileUploadMode.Instantly)
            .UploadUrl(Url.Action("SatinAlmaListesiAktarExcelYukle", "SatinAlma"))
            //.OnValueChanged("fileUploader_valueChanged")
            .SelectButtonText("DOSYA SEÇİNİZ")
            .UploadButtonText("Yükle")
            .UploadedMessage("Şablon Başarıyla Yüklendi")
            .UploadFailedMessage("Dosya Yüklenemedi")
            .OnUploaded("FilesUploaded")
            .OnUploadError("FailedUpload")
    );

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 22 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\SatinAlma\_PartialSatinAlmaForm.cshtml"
Write(Html.DevExtreme().Form()
    .ColCount(1)
    .ID("edSatinAlmaUploadForm")
    .Visible(false)
    .Items(items =>
    {
        items.AddSimple().Editor(ob => ob.DateBox()
              .Name("ED_TERMIN")
              .ID("ED_TERMIN")
              .Value(DateTime.Now)
              .ValidationMessageMode(ValidationMessageMode.Always))
              .IsRequired(true)
              .Label(lbl => lbl.Text("TERMİN"));
        items.AddSimple().Editor(ob => ob.Lookup()
            .Name("ED_INVENTORIES")
            .ID("ED_INVENTORIES")
            .ValidationMessageMode(ValidationMessageMode.Always)
                .SearchEnabled(true)
                .ID("ED_INVENTORIES")
                .DataSource(ob => ob.Mvc()
                .Controller("SatinAlma")
                .LoadAction("GetInventories")
                .Key("CODE"))
                .DisplayExpr("CODE")
                .ValueExpr("CODE"))
                .IsRequired(true)
                    .Label(lbl => lbl.Text("STOK YERİ"));
        items.AddSimple().Editor(ob => ob.Lookup()
            .Name("ED_VENDOR").ValidationMessageMode(ValidationMessageMode.Always)
            .ID("ED_VENDOR")
            .SearchEnabled(true)
            .OnSelectionChanged("VendorSelectionChanged")
            .DataSource(ob => ob.Mvc()
            .Controller("SatinAlma")
            .LoadAction("GetVendorList")
            .Key("VENDOR_SITE_ID"))
            .DisplayExpr("VENDOR_NAME")
            .ValueExpr("VENDOR_SITE_ID"))
            .IsRequired(true)
            .Label(lbl => lbl.Text("Tedarikçi"));
        items.AddSimple().Editor(ob => ob.Lookup()
            .Name("ED_PB")
            .ID("ED_PB").ValidationMessageMode(ValidationMessageMode.Always)
            .SearchEnabled(true)
            .DataSource(ob => ob.Mvc()
            .Controller("SatinAlma")
            .LoadAction("GetParaBirimleriList")
            .OnBeforeSend("ParaBirimiBeforeSend")
            .Key("CURRENCY_CODE"))
            .DisplayExpr("CURRENCY_CODE")
            .ValueExpr("CURRENCY_CODE")).IsRequired(true)
            .Label(lbl => lbl.Text("PARA BİRİMİ"));

        items.AddButton().ButtonOptions(ob => ob.Text("AKTAR").Width("100%").OnClick("SatinAlmaAktar").UseSubmitBehavior(true).Type(ButtonType.Default));

    }
    ));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


</div>

<script>
    var uploadOK = false;
    function FilesUploaded(e) {
        uploadOK = true;
        $(""#edSatinAlmaUploadForm"").dxForm(""instance"").option(""visible"", true)
    }
    function FailedUpload(e) {
        e.message += "" "" + e.error.responseText;
        uploadOK = false; $(""#edSatinAlmaUploadForm"").dxForm(""instance"").option(""visible"", false);
    }
    function SatinAlmaAktar() {
        if (uploadOK) {

            if ($(""#edSatinAlmaUploadForm"").dxForm(""instance"").validate().isValid) {
                var data = {
                    ""STOK_YERI"": $(""#ED_INVENTORIES"").data(""dxLookup"").option(""value""),
                    ""TEDARIK"": $(""#ED_VENDOR"").data(""dxLookup"").option(""value""),
                    ""PB"": $(""#ED_VENDOR"").data(""dxLookup"").option(""value""),
                    ""TERMIN"": $(""#ED_TERMIN"").data(""dxDateBox"").option(""value""),
                }; 
                var settings = {
                    ""async"": false,
                    ""url"": """);
#nullable restore
#line 105 "D:\GitProjects\Eren-Production-Management\EPM.Web\Views\SatinAlma\_PartialSatinAlmaForm.cshtml"
                       Write(Url.Action("SatinAlmaListesiAktar", "SatinAlma"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                    ""method"": ""POST"",
                    ""timeout"": 0,
                    ""headers"": {
                        ""Content-Type"": ""application/json""
                    },
                    ""data"": JSON.stringify(data),
                };
                $.ajax(settings).done(function (response) { 
                }).fail(function (jqXHR, textStatus) {
                });
            } else {

                DevExpress.ui.notify(""Lütfen tüm bilgileri seçiniz"", ""warning"");
            }

        } else {
            DevExpress.ui.notify(""Lütfen oluşturulacak satın alma formu için excel dosyasını yükleyiniz"", ""warning"");
        }
    }
    function VendorSelectionChanged(e) {
        $(""#ED_PB"").data(""dxLookup"").getDataSource().reload();
");
            WriteLiteral(@"    }
    function ParaBirimiBeforeSend(method, ajaxOptions) {
        var data = {
            ""VENDOR_SITE_ID"": $(""#ED_VENDOR"").data(""dxLookup"").option(""value"")
        };
        ajaxOptions.data = data;
    }

    function getFileUploaderInstance() {
        return $(""#file-uploader"").dxFileUploader(""instance"");
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
