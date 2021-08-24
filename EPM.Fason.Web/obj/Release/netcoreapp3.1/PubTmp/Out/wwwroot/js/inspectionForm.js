function FasonSiparisFiltrele() {
    $("#edListe").html("");
    var data = {
        BASLANGIC_TARIHI: jQuery("[name=BASLANGIC_TARIHI]").val(),
        BITIS_TARIHI: jQuery("[name=BITIS_TARIHI]").val(), 
        MODEL_RENK: jQuery("[name=MODEL_RENK]").val(), 
        SEASON: jQuery("[name=SEASON]").val(),
        FIRMA_ID: jQuery("[name=FIRMA_ID]").val(),
    };
    $.ajax({
        type: "POST",
        url: urlPartialSiparisListesi,
        data: data,
        contentType: 'application/x-www-form-urlencoded',
        timeout: 180000,
        success: function (income, status, xmlRequest) {
            $("#edListe").html(income);

        }, beforeSend: function () {
            getLoadPanelInstance().show();
        }


    }).always(function () {
        getLoadPanelInstance().hide();
    });
}

function gridFocusedRowChanged(e) {
    var data = {
        ENTEGRATION_HEADER_ID: e.row.data.ENTEGRATION_ID
    };
    $.ajax({
        type: "POST",
        url: urlPartialSiparisListesiDetail,
        data: data,
        contentType: 'application/x-www-form-urlencoded',
        timeout: 180000,
        success: function (income, status, xmlRequest) {
            $("#edListeDetail").html(income);

        }, beforeSend: function () {
        }


    }).always(function () {
    });
    SiparisIslemleri(e.row.data.ENTEGRATION_ID);

}

function SiparisIslemleri(ENTEGRATION_ID) {
    var data = {
        ENTEGRATION_HEADER_ID: ENTEGRATION_ID
    };
    $.ajax({
        type: "POST",
        url: urlPartialSiparisIslemler,
        data: data,
        contentType: 'application/x-www-form-urlencoded',
        timeout: 180000,
        success: function (income, status, xmlRequest) {
            $("#edListeProcess").html(income);

        }, beforeSend: function () {
        }


    }).always(function () {
    });
}

var formData = null;
function AQLYAP(ENTEGRATION_ID, TYPE) {
    var title = "AQL";
    if (TYPE == 1)
        title = "AQL ÜRETİM";
    else
        title = "AQL ÇORLU";

    loadPanel.show();
    var popupOptions = {
        contentTemplate: function () {

            //return "<div  style='width:65%;display:inline-block;float:left'><div id='gridForm'></div><div id='values1' ><div id='aqlStandart' class='gauge-manuel' style='float:left;text-align: right;color:green'></div><div id='aqlValidation' class='gauge-manuel' style='float:right;text-align: left;color:indianred'></div><div id='formInformation'>....</div></div></div><div id='gridNumbers' style='width:35%;display:inline-block;float:right'></div ><div style='clear:both'><div  id='gridAQL'></div></div><div id='saveButton' style='margin-top:10px'></div>";
            return $.ajax({
                type: "POST",
                url: urlPopupHtml,
                data: "",
                async: false,
                contentType: 'application/x-www-form-urlencoded',
                timeout: 180000,
                success: function (income, status, xmlRequest) {
                }, beforeSend: function () {
                }
            }).responseText;
        },
        width: function () { return "1210px"; },
        height: function () { return "100vh"; },
        showTitle: true,
        title: title,
        dragEnabled: true,
        closeOnOutsideClick: true,
        onShown: function () {

            var data = {
                ENTEGRATION_HEADER_ID: ENTEGRATION_ID,
                TYPE: TYPE
            };
            var resText = $.ajax({
                type: "POST",
                url: urlCreateAQL,
                data: data,
                async: false,
                contentType: 'application/x-www-form-urlencoded',
                timeout: 180000,
                success: function (income, status, xmlRequest) {
                }, beforeSend: function () {
                }
            }).responseText;
            formData = JSON.parse(resText);
            CalculateFormStatus();
            var _totalItems = [];
            var columnsIn = formData.Item2[0];
            for (var key in columnsIn) {
                if (key.startsWith("MN") || key.startsWith("MJ")) {
                    _totalItems.push({
                        column: key,
                        summaryType: "sum",
                        displayFormat: "{0}"
                    });
                }
            }
            $("#gridNumbers").dxDataGrid({
                dataSource: formData.Item3,
                keyExpr: "ID", scrolling: {
                    mode: "virtual"
                }, sorting: {
                    mode: "none"
                },
                columnAutoWidth: true,
                showBorders: true, focusedRowEnabled: true,
                focusedRowIndex: 0,
                focusedColumnIndex: 0,
                showColumnLines: true,
                showRowLines: true,
                height: "300px",
                loadPanel: {
                    enabled: true
                },
                editing: {
                    mode: "cell",
                    allowUpdating: true
                }, onRowUpdating: function (e) {
                    var package = {
                        new: null, old: null
                    };
                    package.newValue = e.newData;
                    package.oldValue = e.oldData;
                    var d = $.Deferred();
                    var settings = {
                        "async": false,
                        "url": urlUpdateNumbers,
                        "method": "POST",
                        "timeout": 0,
                        "headers": {
                            "Content-Type": "application/json"
                        },
                        "data": JSON.stringify(package),
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
                            d.reject("İşlem hatası :" + e.message);
                        }

                    }).fail(function (jqXHR, textStatus) {
                        d.reject("Kayıt Hatası : " + textStatus);
                    });
                    e.cancel = d.promise();
                }, summary: {
                    totalItems: [{
                        column: "QUANTITY",
                        summaryType: "sum",
                        displayFormat: "{0}"
                    }, {
                        column: "QUANTITY_RELEASE",
                        summaryType: "sum",
                        displayFormat: "{0}"
                    }, {
                        column: "QUANTITY_SAMPLE",
                        summaryType: "sum",
                        displayFormat: "{0}"
                    }]
                }, onRowUpdated: function (e) {
                    CalculateFormStatus();
                },
                onCellPrepared: function (e) {
                    if (e.rowType == "header") {
                        e.cellElement.css("text-align", "center");
                    }
                },
                customizeColumns: function (columns) {

                    try {
                        for (var i = 0; i < columns.length; i++) {
                            if (columns[i].dataField == "ID")
                                columns[i].visible = false;
                            if (columns[i].dataField == "PRODUCT_SIZE")
                                columns[i].allowEditing = false;
                            if (columns[i].dataField == "QUANTITY")
                                columns[i].allowEditing = false;
                            if (columns[i].dataField == "QUANTITY_SAMPLE")
                                columns[i].allowEditing = true;
                            if (columns[i].dataField == "HEADER_ID")
                                columns[i].visible = false;

                        }
                    } catch (e) {
                    }


                }
            });
            $("#gridAQL").dxDataGrid({
                dataSource: formData.Item2,
                keyExpr: "QUESTION_ID", scrolling: {
                    mode: "virtual"
                }, sorting: {
                    mode: "none"
                },
                columnAutoWidth: true,
                showBorders: true, focusedRowEnabled: true,
                focusedRowIndex: 0,
                focusedColumnIndex: 0,
                showColumnLines: true,
                showRowLines: true,
                height: "470px",
                width: "100%",
                loadPanel: {
                    enabled: true
                },
                editing: {
                    mode: "cell",
                    allowUpdating: true
                },
                onEditingStart: function (e) {
                    if (QuantityRelease == 0) {
                        DevExpress.ui.notify("Lütfen Gerçekleşen Adetleri Giriniz");
                        e.cancel = true;
                    }
                }, onRowUpdating: function (e) {

                    var package = {
                        new: null, old: null
                    };
                    package.newValue = e.newData;
                    package.oldValue = e.oldData;
                    var d = $.Deferred();
                    var settings = {
                        "async": false,
                        "url": urlUpdateAQL,
                        "method": "POST",
                        "timeout": 0,
                        "headers": {
                            "Content-Type": "application/json"
                        },
                        "data": JSON.stringify(package),
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
                            d.reject("İşlem hatası :" + e.message);
                        }

                    }).fail(function (jqXHR, textStatus) {
                        d.reject("Kayıt Hatası : " + textStatus);
                    });
                    e.cancel = d.promise();
                }
                , summary: {
                    totalItems: _totalItems
                },
                onRowUpdated: function (e) {
                    CalculateFormStatus();
                },
                onCellPrepared: function (e) {
                    if (e.rowType == "header") {
                        e.cellElement.css("text-align", "center");
                    }
                },
                customizeColumns: function (columns) {
                    try {
                        for (var i = 0; i < columns.length; i++) {
                            if (!columns[i].isBand) {
                                if (columns[i].dataField.startsWith('MJ_')) {

                                    columns.push({
                                        caption: columns[i].dataField.replace('MJ_', ''),
                                        isBand: true
                                    });
                                    for (var a = 0; a < columns.length; a++) {
                                        if (columns[a].dataField == 'MN_' + columns[i].dataField.replace('MJ_', '')) {
                                            columns[a].ownerBand = columns.length - 1;
                                            columns[a].caption = "MINOR";
                                        }
                                    }
                                    columns[i].ownerBand = columns.length - 1;
                                    columns[i].caption = "MAJOR";
                                }
                                if (columns[i].dataField == "QUESTION_ID")
                                    columns[i].visible = false;
                                if (columns[i].dataField == "QUESTION")
                                    columns[i].allowEditing = false;
                                if (columns[i].dataField == "HEADER_ID")
                                    columns[i].visible = false;
                            }

                        }
                    } catch (e) {
                    }


                }
            });
            $("#gridForm").dxForm({
                colCount: 4,
                formData: formData.Item1,
                items: [{
                    dataField: "START_DATE",
                    caption: "TARİH",
                    editorType: "dxDateBox",
                    editorOptions: {
                        disabled: true
                    }

                },
                {
                    dataField: "DESCRIPTION",
                    colSpan: 2,
                    editorType: "dxTextArea",
                    caption: "DESCRIPTION"
                },
                {
                    dataField: "CHECKED_INLINE",
                    editorType: "dxCheckBox",
                    editorOptions: {
                        text: "INLINE TEST NOTLARINI KONTROL ETTİM",
                    }, label: {
                        text: " "
                    }
                }, {
                    dataField: "PRODUCTION_HEADER.SEASON",
                    label: {
                        text: "SEASON"
                    },
                    editorOptions: {
                        disabled: true
                    }
                }, {
                    dataField: "PRODUCTION_HEADER.MODEL", label: {
                        text: "MODEL"
                    },
                    editorOptions: {
                        disabled: true
                    }
                }, {
                    dataField: "PRODUCTION_HEADER.COLOR"
                    , label: {
                        text: "COLOR"
                    },
                    editorOptions: {
                        disabled: true
                    }
                }, {
                    dataField: "FIRMA_ADI", label: {
                        text: "FIRMA"
                    },
                    editorOptions: {
                        disabled: true
                    }
                }]
            });
            $("#saveButton").dxButton({
                stylingMode: "contained",
                text: "KAYDET",
                type: "danger",
                width: "47%",
                onClick: function () {
                    var result = DevExpress.ui.dialog.confirm("İşlemler Kaydedilecektir. Devam Edilsin mi ?", "ONAY");
                    result.done(function (dialogResult) {
                        if (!dialogResult)
                            return;
                        var status = 0;
                        if (QuantityRelease > 0) {
                            if (formData.Item4.ACCEPT_QUANTITY >= minorCount / 4 + majorCount)
                                status = 1;
                        }
                        if (jQuery("[name=CHECKED_INLINE]").val()=="true") {
                            var package = {
                                CHECKED_INLINE: jQuery("[name=CHECKED_INLINE]").val(),
                                DESCRIPTION: jQuery("[name=DESCRIPTION]").val(),
                                ENTEGRATION_ID: ENTEGRATION_ID,
                                STATUS: status,
                                TYPE: TYPE
                            };

                            loadPanel.show();
                            var settings = {
                                "async": true,
                                "url": urlSaveAQL,
                                "method": "POST",
                                "timeout": 0,
                                "headers": {
                                    "Content-Type": "application/json"
                                },
                                "data": JSON.stringify(package),
                            };
                            $.ajax(settings).done(function (response) {

                                $("#popupDetay").dxPopup("hide");
                                loadPanel.hide();
                                try {
                                    var parsedIncome = response;
                                    if (parsedIncome.isOK) {
                                        $("#popupDetay").dxPopup("hide");
                                        loadPanel.hide();
                                        DevExpress.ui.dialog.custom({ messageHtml: "AQL başarıyla kaydedildi", title: "BİLGİ", buttons: [{ text: "TAMAM", }] }).show();
                                        SiparisIslemleri(ENTEGRATION_ID);
                                    } else {
                                        $("#popupDetay").dxPopup("hide");
                                        loadPanel.hide();
                                        DevExpress.ui.dialog.custom({ messageHtml: "İşlemler yapılırken hatayla karşılaşıldı : " + parsedIncome.errorText, title: "HATA", buttons: [{ text: "TAMAM", }] }).show();
                                    }
                                } catch (e) {
                                }

                            }).fail(function (jqXHR, textStatus) {
                                $("#popupDetay").dxPopup("hide");
                                loadPanel.hide();
                                DevExpress.ui.dialog.custom({ messageHtml: "İşlemler yapılırken hatayla karşılaşıldı : " + textStatus, title: "HATA", buttons: [{ text: "TAMAM", }] }).show();
                            });
                        } else {

                            DevExpress.ui.dialog.custom({ messageHtml: "LÜTFEN <b>'INLINE TEST NOTLARINI KONTROL ETTİM'</b> KUTUCUĞUNUN İŞARETLİ OLDUĞUNDAN EMİN OLUNUZ!", title: "HATA", buttons: [{ text: "TAMAM", }] }).show();
                        }
                       
                    });
                }
            }); 
            $("#inlineAQLButton").dxButton({
                stylingMode: "contained",
                text: "INLINE KONTROL",
                type: "success",
                width: "47%",
                onClick: function () {
                    INLINEAQLYAP(ENTEGRATION_ID);
                }
            });
            loadPanel.hide();
        }

    };

    $("#popupDetay").dxPopup(popupOptions).dxPopup("instance").show();


}

function INLINEAQLYAP(ENTEGRATION_ID) {
    

    loadPanel.show();
    var popupOptions = {
        contentTemplate: function () {
            var data = {
                ENTEGRATION_HEADER_ID: ENTEGRATION_ID
            };
            var resText = $.ajax({
                type: "POST",
                url: urlInlineAQL,
                data: data,
                async: false,
                contentType: 'application/x-www-form-urlencoded',
                timeout: 180000,
                success: function (income, status, xmlRequest) {
                }, beforeSend: function () {
                }
            }).responseText;
            loadPanel.hide();
            return resText;
        },
        width: function () { return "800px"; },
        height: function () { return "600px"; },
        showTitle: true,
        title: "INLINE KONTROL",
        dragEnabled: true,
        closeOnOutsideClick: true,
        onShown: function () {
             
        }

    };

    $("#popup").dxPopup(popupOptions).dxPopup("instance").show();


}
var QuantitySample = 0;
var Quantity = 0;
var QuantityRelease = 0;
var majorCount = 0;
var minorCount = 0;
var columnsMinor = [];
var columnsMajor = [];
function CalculateFormStatus() {
    QuantitySample = 0;
    Quantity = 0;
    QuantityRelease = 0;
    majorCount = 0;
    minorCount = 0;
    columnsMinor = [];
    columnsMajor = [];
    for (var i = 0; i < formData.Item3.length; i++) {
        QuantitySample += formData.Item3[i].QUANTITY_SAMPLE;
        Quantity += formData.Item3[i].QUANTITY;
        QuantityRelease += formData.Item3[i].QUANTITY_RELEASE;
    }
    var columnsIn = formData.Item2[0];
    for (var key in columnsIn) {
        if (key.startsWith("MN")) {
            columnsMinor.push(key);
        }
        if (key.startsWith("MJ")) {
            columnsMajor.push(key);
        }
    }
    for (var i = 0; i < formData.Item2.length; i++) {
        for (var a = 0; a < columnsMajor.length; a++) {
            majorCount += formData.Item2[i][columnsMajor[a]];
        }
        for (var a = 0; a < columnsMinor.length; a++) {
            minorCount += formData.Item2[i][columnsMinor[a]];
        }
    }
    $("#aqlStandart").html(formData.Item4.ACCEPT_QUANTITY);
    $("#aqlValidation").html(minorCount / 4 + majorCount);
    if (QuantityRelease > 0) {
        if (formData.Item4.ACCEPT_QUANTITY >= minorCount / 4 + majorCount) {
            $("#formInformation").html("ONAYLANDI");
            $("#formInformation").css("color", "green");
            $("#aqlValidation").css("color", "green");
        } else {
            $("#formInformation").html("BAŞARISIZ");
            $("#formInformation").css("color", "indianred");
            $("#aqlValidation").css("color", "indianred");
        }
    } else {
        $("#formInformation").html("RELEASE ADETLERİ GİRİNİZ");
        $("#formInformation").css("color", "indianred");
        $("#aqlValidation").css("color", "indianred");
    }

}

function InspectionListAQLSearch(e) {
    var type = e.row.data.TYPE;
    var entegration_id = e.row.data.ENTEGRATION_HEADER_ID;
    AQLYAP(entegration_id, type);
}

function InspectionListInlineAQLSearch(e) { 
    var entegration_id = e.row.data.ENTEGRATION_HEADER_ID;
    INLINEAQLYAP(entegration_id);
}