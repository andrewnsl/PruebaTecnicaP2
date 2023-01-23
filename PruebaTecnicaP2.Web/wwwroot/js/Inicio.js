function inicio() {
    this.excelFileToJSON = (file) => {
        const reader = new FileReader();
        reader.onload = async (e) => {
            this.Loader(true);
            const data = e.target.result;
            const workbook = XLSX.read(data, { type: "array" });
            const sheetName = workbook.SheetNames[0];
            const worksheet = workbook.Sheets[sheetName];
            const json = XLSX.utils.sheet_to_json(worksheet);
            await this.subirData(json);
            
        };
        reader.readAsArrayBuffer(file);
    }

    this.subirData = async (json) => {
        await $.ajax({
            url: '/Home/CargarArchivo',
            type: 'POST',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: async function (result) {
                if (result.code === 4) {
                    $("#flExcel").val("");
                    await fnInicio.CargarData();

                } else {
                    this.Loader(false)
                }
                
            },  
            error: function (ex) {
                alert(ex);
                this.Loader(false);
            }
        });
    }

    this.CargarData = async () => {
        this.Loader(true);
        await $.ajax({
            url: '/Home/ObtenerPuntos',
            type: 'GET',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: async function (result) {
                if (result.code === 4) {
                    var html = '';

                    result.result.forEach(element => {
                        html += "<tr>";
                        html += "<td>";
                        html += element.uid;
                        html += "</td>";
                        html += "<td>";
                        html += element.valor;
                        html += "</td>";
                        html += "<td>";
                        html += element.factura;
                        html += "</td>";
                        html += "<td>";
                        html += element.uid_cms;
                        html += "</td>";
                        html += "<td>";
                        html += element.id_sucursal;
                        html += "</td>";
                        html += "<td>";
                        html += element.id_comercio;
                        html += "</td>";
                        html += "<td>";
                        html += element.id_transaccion;
                        html += "</td>";
                        html += "</tr>";
                    });

                    var element = $("#tblBody");
                    element.html(html);
                }

            },
            error: function (ex) {
                alert(ex);
            }
        });

        this.Loader(false);
    }

    this.Loader = (show) => {
        if (show) {
            $("#loaderC").addClass("loader-show");
            $('body').addClass("body-loader");
        } else {
            $("#loaderC").removeClass("loader-show");
            $('body').removeClass("body-loader");           
        }
    }

    this.Validar = (files) => {
        if (files.length == 0) {
            fnInicio.ErrorCampo("El archivo es requerido");
            return false;
        }
        var filename = files[0].name;
        var extension = filename.substring(filename.lastIndexOf(".")).toUpperCase();
        if (extension == '.XLS' || extension == '.XLSX') {
            fnInicio.Valido();
            return true;
        } else {
            fnInicio.ErrorCampo("Seleccionar un archivo de excel valido");
            return false;
        }
    }

    this.ErrorCampo = (mensaje) => {
        var element = $("#vlflExcel");
        var elementInput = $("#flExcel");

        element.addClass("label-invalido");
        elementInput.addClass("input-invalido");

        element.html("");
        element.html(mensaje);
    }

    this.Valido = () => {
        var element = $("#vlflExcel");
        var elementInput = $("#flExcel");
        element.removeClass("label-invalido");
        elementInput.removeClass("input-invalido");
        element.html("");
    }
} 

var fnInicio = new inicio();

jQuery(async function () {

    await fnInicio.CargarData();

    $("#flExcel").on('change', function () {
        fnInicio.Validar(this.files);        
    });

    $("#btnCargar").on('click', function () {

        var files = $("#flExcel")[0].files;
        var valido = fnInicio.Validar(files);
        if (valido) {
            fnInicio.excelFileToJSON(files[0]);
        }        
    });
});