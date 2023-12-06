$(function () {// metodo que se ejecuta cuando ha terminado de cargar la pagina

    if (model.id > 0) {
        cargarProvincias(model.idDepartamento);
        cargarDistritos(model.idProvincia);
    }

});


//const OpenModal = (id) => {
//    //console.log("hola mundo", id);
//    const invoiceID = document.getElementById("txtid").value;


//    fetch(`${addOrEditDetail}?id=${id}&purchaseId=${invoiceID}`)
//        .then(response => {
//            if (response.ok) return response.text();
//            throw new Error(response.statusText);
//            // The API call was successful!

//        }).then(htmlText => {

//            // Convert the HTML string into a document object
//            //var parser = new DOMParser();
//            //var doc = parser.parseFromString(html, 'text/html');

//            // Get the image file

//            //document.getElementById('modalBody').innerHTML = doc;

//            $('#modalBody').html(htmlText);

//            CrearEventoProducto();


//        }).catch(function (err) {
//            // There was an error

//            console.warn('Something went wrong.', err);
//        });

//}


//const SaveDetailInvoice = () => {
//    event.preventDefault()

//    const form = document.getElementById('formInvoiceDetail');
//    let formData = new FormData();

//    for (let i = 0; i < form.length; i++) {
//        formData.append(form[i].name, form[i].value);
//    }


//    fetch(`${addOrEditDetail}`, {
//        method: 'POST',
//        body: formData,
//    })
//        .then(response => {
//            if (response.ok) return response?.json()
//            throw new Error(response.statusText);
//            // The API call was successful!

//        }).then(data => {

//            if (data.errorId == 0) {
//                alert("Saved Success");
//                location.reload(true);
//            }

//        }).catch(function (err) {

//            alert('An error ocurred!')
//            console.warn('Something went wrong.', err);
//            return false;
//        });

//    return false;
//}

const getProvincias = () => {

    const IdDepartamento = document.getElementById("IdDepartamento").value;
    cargarProvincias(IdDepartamento);

}
const cargarProvincias = (IdDepartamento) => {
    fetch(`${urlGetProvincias}?id=${IdDepartamento}`)
        .then(request => {
            if (request.ok) {
                return request.json();
            }
            throw new Error("a ocurrido un error");
        })
        .then(data => {
            const selectProvincia = document.getElementById('IdProvincia');
            let options = "";

            if (data.items) {
                for (let item of data.items) {
                    options += `<option value="${item.value}"  ${(model.idProvincia == item.value ? "selected" : "")}  >${item.text}</option>`;
                }
            }

            $(selectProvincia).html(options);

        }).catch(error => console.log(error));
}

const getDistritos = () => {

    const IdProvincia = document.getElementById("IdProvincia").value;
    cargarDistritos(IdProvincia);

}

const cargarDistritos = (IdProvincia)=>{
    fetch(`${urlGetDistritos}?id=${IdProvincia}`)
        .then(request => {
            if (request.ok) {
                return request.json();
            }
            throw new Error("a ocurrido un error");
        })
        .then(data => {
            const selectDistrito = document.getElementById('IdDistrito');
            let options = "";

            if (data.items) {
                for (let item of data.items) {
                    options += `<option value="${item.value}" ${(model.idDistrito == item.value ? "selected" : "")}>${item.text}</option>`;
                }
            }

            $(selectDistrito).html(options);

        }).catch(error => console.log(error));
}