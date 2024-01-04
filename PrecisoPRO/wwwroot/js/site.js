// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/*ALTERAÇÕES PAULO - 29112023 */
function maiuscula(z) {
    v = z.value.toUpperCase();
    z.value = v;
}

function limparInputs() {
    // Obtém todos os elementos de entrada (inputs) usando document.querySelectorAll
    var inputs = document.querySelectorAll('input');
   
   
    // Itera sobre os elementos e define o valor de cada input como uma string vazia
    inputs.forEach(function (input) {
        input.value = '';
    });

    var valor = "";
    document.getElementById('estado').value = valor;
}

//mascaras
//mascaras
$(document).ready(function () {
    $('.date').mask('99/99/9999');
    $('.time').mask('00:00:00');
    $('#cep').mask('99.999-999');
    $('#Cep').mask('99.999-999');
    $('.phone').mask('9999-9999');
    $('.telefone').mask('(99)9999-9999');
    $('.celular').mask("(99)99999-9999");
    $('#cnpj').mask('99.999.999/9999-99');
    $('#Cnpj').mask('99.999.999/9999-99');
    $('.cnpj').mask('99.999.999/9999-99');
    $('#telefone').mask('(99)99999-9999');
    $(".senha").mask("xxxxxxxxx");
    $("#cest").mask("99.999.99");
    $("#ProcuraNCM").mask("9999.99.99");
    $("#ncm").mask("9999.99.99");
    $(".decimal").mask("9999,999");
    $(".pr-aliq").mask("9999.999");

});
jQuery(function ($) {
    $("#campoData").mask("99/99/9999");
    $("#campoTelefone").mask("(999) 999-9999");

});
function chamarIncluir() {

    var cont = document.querySelector(".pro-titulo"); //pega o titulo

    alert(cont.innerText);

    var controller = cont.innerText;

  

    $("#modal").load("/" + controller + "/Incluir/", function () {

        $('#modal').modal("show");

    });
}


function buscarCNDs() {

    var cont = document.querySelector(".pro-titulo"); //pega o titulo

    var controller = cont.innerText;

    alert(cont.innerText);

    $("#modal").load("/" + controller + "/ConsultarCnd/", function () {

        $('#modal').modal("show");

    });
}


//adicionar registro de empresas em lote via api
function chamarIncluirEmLote(id) {
   
    
    
    var cont = document.querySelector(".pro-titulo"); //pega o titulo

    var controller = cont.innerText;

    var url = "/" + controller + "/IncluirLote/" + id;


    $("#modal").load("/" + controller + "/IncluirLote/" + id, function () {

        $('#modal').modal("show");
        // Adiciona um ouvinte de eventos para o evento 'hidden.bs.modal' do Bootstrap,
        // que é disparado quando o modal é fechado
        $('#modal').on('hidden.bs.modal', function () {
            recarregarPagina();
        });

    });
}


//function enviarCNPJs() {
//    //coletar cnpjs dos inputs
//    var cnpjs = [];
//    $('form#formularioCNPJs input[type="text"]').each(function () {
//        var cnpjSemPontosBarras = $(this).val().replace(/[^\d]/g, ''); // Remove pontos e barras
//        if (cnpjSemPontosBarras) { // Adicionar verificação se não é vazio
//            cnpjs.push(cnpjSemPontosBarras);
//        }
//       /* alert(cnpjs);*/
//    });

//    //requisição  ajax
//    // Fazer a requisição AJAX para o endpoint da API
//    $.ajax({
//        type: 'POST',
//        url: '/Empresa/AdicionarEmpresaLote',
//        contentType: 'application/x-www-form-urlencoded',
//        data: { cnpjs: cnpjs.join(',') },
//        success: function (data) {
//           // // Processar os resultados
//           //// console.log(data);
//           // // Aqui você pode manipular os dados retornados da API, como salvar no banco de dados ou exibir na página.
//           // window.location.href = '/Empresa/Index';  // Substitua com o URL correto
//        },
//        error: function (error) {
//            console.error(error);
//        }
//    });



//}
//essa tava funcionando
//function enviarCNPJs() {
//    // Coletar CNPJs dos inputs
//    var cnpjs = [];
//    $('form#formularioCNPJs input[type="text"]').each(function () {
//        var cnpjSemPontosBarras = $(this).val().replace(/[^\d]/g, ''); // Remove pontos e barras
//        if (cnpjSemPontosBarras) { // Adicionar verificação se não é vazio
//            cnpjs.push(cnpjSemPontosBarras);
//        }
//    });

//    // Construir a URL com os parâmetros de consulta
//    var url = '/Empresa/AdicionarEmpresaLote?cnpjs=' + cnpjs.join(',');

//    alert(url);
//    // Redirecionar para a action desejada
//    window.location.href = url;
//}
//com ajax
function enviarCNPJs() {
    
    var cnpjs = [];
        $('form#formularioCNPJs input[type="text"]').each(function () {
            var cnpjSemPontosBarras = $(this).val().replace(/[^\d]/g, ''); // Remove pontos e barras
            if (cnpjSemPontosBarras) { // Adicionar verificação se não é vazio
                cnpjs.push(cnpjSemPontosBarras);
            }
        });
    
    
    $.ajax({
        data: { cnpjs: cnpjs.join(',') },
        type: 'GET',
        processData: true,
        success: function () {

            window.location.href = '/Empresa/AdicionarEmpresaLote?cnpjs=' + cnpjs;

        }
    });

};



function recarregarPagina() {
    location.reload(true); // O parâmetro true força o recarregamento do cache
}

//obsoleto, so esta aqui para documentação
function chamarAssociar() {

    var cont = document.querySelector(".pro-titulo"); //pega o titulo

    var controller = cont.innerText;



    $("#modal").load("/" + controller + "/AssociarEmpresa/", function () {

        $('#modal').modal("show");

    });
}

function chamarAssociarIndividual(id) {

    var cont = document.querySelector(".pro-titulo"); //pega o titulo

    var controller = cont.innerText;
   
    $("#modal").load("/" + controller + "/AssociarIndividual/" + id, function () {
        $('#modal').modal("show");

    });
}
//fim obsoleto

//FECHAR O ALERT APOS A MENSAGEM
$(document).ready(function () {
    setTimeout(function () {
        $(".alert").fadeOut("slow", function () {

            $(this).alert('close');
        });

    }, 5000);
});

$(document).ready(function () {

    
    $('#EmpresaIncluir').on('submit', function (e) {
        $.each($('input[aria-required="true"]'), function () {
            if (!this.value || this.value == '') {


                               
                alert('Há campos obrigatórios sem preenchimento, verifique todas as abas!');
                e.preventDefault();
                return false;
            }
        });
    });




});