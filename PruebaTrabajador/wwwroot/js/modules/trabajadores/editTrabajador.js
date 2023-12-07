$(function () {// metodo que se ejecuta cuando ha terminado de cargar la pagina

    if (model.id > 0) {
        cargarProvincias(model.idDepartamento);
        cargarDistritos(model.idProvincia);
    }

});




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