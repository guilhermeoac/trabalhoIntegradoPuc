// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Variaveis globais
var categoriaId = 0;
var clienteId = 0;
var clienteNome = '';
var clienteSenha = '';
var locacao = null;
var confirmLocacao = false;

//Ação botão selecionar categoria
$('.buttonCategoriaSelecioda').click(function () {
    categoriaId = $(this).attr('categoriaId');
    var categoriaString = $(this).attr('categoriaString');
    $('#tituloCategoria').html(categoriaString);
    $('#conteudoReserva').show();
    $('#reservaTbody').html('<tr><td colspan="8">Nenhum veículo encontrado</td></tr>');
});

//Ação botão buscar carros dispiniveis
$('#buttonBuscarReserva').click(function () {
    $('#reservaTbody').html('<tr><td colspan="8">Nenhum veículo encontrado</td></tr>');
    var dataReservaInicio = $('#reservaDataInicio').val();
    var dataReservaFim = $('#reservaDataFim').val();
    var categoria = categoriaId;

    if (dataReservaInicio != null && dataReservaInicio.length > 0 && dataReservaFim != null && dataReservaFim.length > 0) {
        if (new Date(dataReservaInicio).getTime() <= new Date(dataReservaFim).getTime()) {
            if (categoria > 0) {
                var parametros = {
                    DataReservaInicio: dataReservaInicio,
                    DataReservaFim: dataReservaFim,
                    Categoria: categoria
                };
                $.post("/home/BuscarCarrosDisponíveisPorData/", parametros)
                    .done(function (retorno) {
                        if (retorno != null) {
                            updateTabelaReserva(retorno);
                        } else alert('Ocorreu um erro, tente novamente mais tarde');
                    })
                    .fail(function (error) {
                        alert('Ocorreu um erro, tente novamente mais tarde');
                    });
            } else alert('Selecione uma categoria válida');
        } else alert('A data inicial deve ser menor que a data final');
    } else alert('Insira uma data válida');
});


//Função para atualizar dinamicamente a tabela de reserva
function updateTabelaReserva(veiculos) {
    var html = '';
    if (veiculos.length > 0) {
        for (var i = 0; i < veiculos.length; i++) {
            var veiculo = veiculos[i];

            html += '<tr veiculoId="' + veiculo.id + '" marca="' + veiculo.marca + '" modelo="' + veiculo.modelo + '" placa="' + veiculo.placa + '" ano="' + veiculo.ano + '" combustivel="' + veiculo.combustivel + '" valorHora="' + veiculo.valorHora + '">';
            html += '<th scope="row">' + (i+1) +'</th>';
            html += '<td>' + veiculo.marca +'</td>';
            html += '<td>' + veiculo.modelo +'</td>';
            html += '<td>' + veiculo.placa +'</td>';
            html += '<td>' + veiculo.ano +'</td>';
            html += '<td>' + veiculo.combustivel+'</td>';
            html += '<td>R$' + veiculo.valorHora.toFixed(2)+'</td>';
            html += '<td><a class="btn btn-primary text-white cursor_pointer buttonSelecionarVeiculo" role="button">Selecionar</a></td>';
            html += '</tr>';
        }
    }
    if (html.length > 2) $('#reservaTbody').html(html);
    else $('#reservaTbody').html('<tr><td colspan="8">Nenhum veículo encontrado</td></tr>');

    //Ação botão selecionar veiculo para locacao
    $('.buttonSelecionarVeiculo').click(function () {
        var veiculoId = $(this).parent().parent().attr('veiculoId');
        var marca = $(this).parent().parent().attr('marca');
        var modelo = $(this).parent().parent().attr('modelo');
        var placa = $(this).parent().parent().attr('placa');
        var ano = $(this).parent().parent().attr('ano');
        var combustivel = $(this).parent().parent().attr('combustivel');
        var valorHora = $(this).parent().parent().attr('valorHora');
        var dataInicio = new Date($('#reservaDataInicio').val());
        var dataFim = new Date($('#reservaDataFim').val());
        var totalHora = Math.abs(dataInicio - dataFim) / 36e5;

        locacao = {
            ValorHora: valorHora,
            DataInicioLocacao: $('#reservaDataInicio').val(),
            DataFimLocacao: $('#reservaDataFim').val(),
            ClienteId: clienteId,
            VeiculoId: veiculoId
        };

        $('#detalheVeiculoModal').modal('toggle');
        $('#veiculoTitulo').html(marca + ' - ' + modelo);
        $('#placaSpan').html(placa);
        $('#anoSpan').html(ano);
        $('#combustivelSpan').html(combustivel);
        $('#valorHoraSpan').html('R$ ' + Number(valorHora).toFixed(2));
        $('#dataInicoSpan').html(dataInicio.toLocaleString());
        $('#dataFimSpan').html(dataFim.toLocaleString());
        $('#totalHorasTitulo').html(totalHora + ' h');
        $('#valorTotalTitulo').html('R$ ' + (totalHora * Number(valorHora)).toFixed(2));
    });
}

//Ação botão confirmar locacao veiculo
$('#buttonConfirmarLocacao').click(function () {
    if (clienteId != null && clienteId > 0) {
        $('#confirmarSenhaModal').modal('toggle');
        $('#clienteNomeLabel').html(clienteNome);
    } else {
        $('#loginModal').modal('toggle');
        confirmLocacao = true;
    }
});

//Ação botão confirmar confirmar identidade
$('#buttonConfirmarIdentidade').click(function () {
    if ($('#confirmarIdentidadeSenha').val() != null && $('#confirmarIdentidadeSenha').val().length > 4) {
        if (clienteSenha == $('#confirmarIdentidadeSenha').val().trim()) {
            criarLocacao();
            $('#confirmarSenhaModal').modal('toggle');
        } else alert('Senha inálida');
    } else alert('Insira uma senha válida');
});

//Função para criar a locacao
function criarLocacao() {
    $.post("/home/InserirLocacao/", locacao)
        .done(function (retorno) {
            if (retorno.id != null && retorno.id > 0) {
                $('#conteudoReserva').hide();
                $('#reservaDataInicio').val('');
                $('#reservaDataFim').val('');
                $('#reservaTbody').html('<tr><td colspan="8">Nenhum veículo encontrado</td></tr>');
                $('#detalheVeiculoModal').modal('toggle');
                alert('Locação criada com sucesso!');
            } else alert('Ocorreu um erro, tente novamente mais tarde');
        })
        .fail(function (error) {
            alert('Ocorreu um erro, tente novamente mais tarde');
        });
}

//Ação botão buscar locacoes realizadas
$('#buttonBuscarLocacao').click(function () {
    $('#locacaoTbody').html('<tr><td colspan="11">Nenhuma locação encontrada</td></tr>');
    var dataLocacaoInicio = $('#locacaoDataInicio').val();
    var dataLocacaoFim = $('#locacaoDataFim').val();
    var cliente = clienteId;

    if (dataLocacaoInicio != null && dataLocacaoInicio.length > 0 && dataLocacaoFim != null && dataLocacaoFim.length > 0) {
        if (new Date(dataLocacaoInicio).getTime() <= new Date(dataLocacaoFim).getTime()) {
            if (cliente > 0) {
                var parametros = {
                    DataLocacaoInicio: dataLocacaoInicio,
                    DataLocacaoFim: dataLocacaoFim,
                    ClienteId: cliente
                };
                $.post("/home/BuscarLocacoesDoClientePorData/", parametros)
                    .done(function (retorno) {
                        if (retorno != null) {
                            updateTabelaLocacao(retorno);
                        } else alert('Ocorreu um erro, tente novamente mais tarde');
                    })
                    .fail(function (error) {
                        alert('Ocorreu um erro, tente novamente mais tarde');
                    });
            } else alert('Faça login para continuar a busca');
        } else alert('A data inicial deve ser menor que a data final');
    } else alert('Insira uma data válida');
});

//Função para atualizar dinamicamente a tabela de locacao
function updateTabelaLocacao(locacoes) {
    var html = '';
    if (locacoes.length > 0) {
        for (var i = 0; i < locacoes.length; i++) {
            var locacao = locacoes[i];

            html += '<tr>';
            html += '<th scope="row">' + (i + 1) + '</th>';
            html += '<td>' + locacao.veiculo.marca + '</td>';
            html += '<td>' + locacao.veiculo.modelo + '</td>';
            html += '<td>' + locacao.veiculo.placa + '</td>';
            html += '<td>' + locacao.veiculo.ano + '</td>';
            html += '<td>' + locacao.veiculo.combustivel + '</td>';
            html += '<td>' + new Date(locacao.dataInicioLocacao).toLocaleDateString() + '</td>';
            html += '<td>' + new Date(locacao.dataFimLocacao).toLocaleDateString() + '</td>';
            html += '<td>R$ ' + locacao.valorHora.toFixed(2) + '</td>';
            html += '<td>' + locacao.totalHora + ' h</td>';
            html += '<td>R$ ' + locacao.valorTotal.toFixed(2) + '</td>';
            html += '</tr>';
        }
    }
    if (html.length > 2) $('#locacaoTbody').html(html);
    else $('#locacaoTbody').html('<tr><td colspan="11">Nenhuma locação encontrada</td></tr>');
}


$('#buttonAbrirLogin').click(function () {
    $('#login_cpf').val('');
    $('#login_senha').val('');
});

//Ação botão fazer login
$('#buttonLogin').click(function () {
    var cpf = $('#login_cpf').val();
    var senha = $('#login_senha').val();

    if (cpf != null && cpf.length > 0) {
        if (senha != null && senha.length>0) {
           
            var parametros = {
                Cpf: cpf,
                Senha: senha
            };
            $.post("/home/BuscarUsuarioPorCpfESenha/", parametros)
                .done(function (retorno) {
                    if (retorno.id != null && retorno.id > 0) {
                        updateTelaLogin(retorno);
                        if (confirmLocacao) criarLocacao();
                    } else alert('Usuário e/ou senha não encontrados');
                })
                .fail(function (error) {
                    alert('Ocorreu um erro, tente novamente mais tarde');
                });
            
        } else alert('Senha inválida');
    } else alert('Insira um CPF válido');
});

//Função que atualiza componentes da tela quando loga
function updateTelaLogada() {
    $('#loginLi').hide();
    $('#logoutLi').show();
    $('#locacaoLi').show();
    $('#clienteNomeLi').show();
}

//Ação botão locacao
$('#locacaoLi').click(function () {
    $('#conteudoInicial').hide();
    $('#conteudoReserva').hide();
    $('#conteudoLocacao').show();
    $('#conteudoPrivacidade').hide();
});

//Ação botão privacidade
$('#privacidadeButton').click(function () {
    $('#conteudoInicial').hide();
    $('#conteudoReserva').hide();
    $('#conteudoLocacao').hide();
    $('#conteudoPrivacidade').show();
});

//Ação botão inicio
$('#inicioButton').click(function () {
    updateChamarInicio();
});

//Ação botão logotipo
$('#logoButton').click(function () {
    updateChamarInicio();
});

//Função que atualiza o layout ao clicar em home
function updateChamarInicio() {
    $('#conteudoInicial').show();
    $('#conteudoReserva').hide();
    $('#conteudoLocacao').hide();
    $('#conteudoPrivacidade').hide();
    $('#reservaDataInicio').val('');
    $('#reservaDataFim').val('');
    $('#locacaoDataInicio').val('');
    $('#locacaoDataFim').val('');
    $('#reservaTbody').html('<tr><td colspan="8">Nenhum veículo encontrado</td></tr>');
    $('#locacaoTbody').html('<tr><td colspan="11">Nenhuma locação encontrada</td></tr>');
}

//Ação botão logout
$('#logoutLi').click(function () {
    $('#logoutModal').modal('toggle');
});

//Ação botão confirmar logout
$('#logoutConfirmButton').click(function () {
    $('#loginLi').show();
    $('#logoutLi').hide();
    $('#locacaoLi').hide();
    $('#clienteNomeLi').hide();
    clienteId = 0;
    clienteNome = '';
    clienteSenha = '';
    $('#clienteNometext').html('');
    updateChamarInicio();
    $('#logoutModal').modal('toggle');
    confirmLocacao = false;
});

//Função que atualiza a o modal de login
function updateTelaLogin(cliente) {
    $('#loginModal').modal('toggle');
    $('#login_cpf').val('');
    $('#login_senha').val('');
    clienteId = cliente.id;
    clienteNome = cliente.nome;
    clienteSenha = cliente.senha;
    $('#clienteNometext').html(clienteNome);
    updateTelaLogada();
}

//Ação botão confirmar cadastro do cliente
$('#cadastroClienteButton').click(function () {
    if ($('#cadastro_nome').val() != null && $('#cadastro_nome').val().length > 0) {
        if ($('#cadastro_cpf').val() != null && $('#cadastro_cpf').val().length >= 14) {
            if ($('#cadastro_data_nascimento').val() != null && $('#cadastro_data_nascimento').val().length >= 10) {
                if ($('#cadastro_cep').val() != null && $('#cadastro_cep').val().length >=8) {
                    if ($('#cadastro_rua').val() != null && $('#cadastro_rua').val().length > 2) {
                        if ($('#cadastro_numero').val() != null && $('#cadastro_numero').val().length > 0) {
                            if ($('#cadastro_cidade').val() != null && $('#cadastro_cidade').val().length > 1) {
                                if ($('#cadastro_estado').val() != null && $('#cadastro_estado').val().length >= 2) {
                                    if ($('#cadastro_senha').val() != null && $('#cadastro_senha').val().length > 4) {
                                        if ($('#cadastro_confirmar_senha').val() != null && $('#cadastro_confirmar_senha').val().length>4) {
                                            if ($('#cadastro_senha').val() == $('#cadastro_confirmar_senha').val()) {

                                                var endereco = $('#cadastro_rua').val().trim() + ' - ' + $('#cadastro_numero').val() + ', ' + $('#cadastro_cidade').val().trim() + '/' + $('#cadastro_estado').val().trim() + ' - ' + $('#cadastro_cep').val();
                                                var parametros = {
                                                    Cpf: $('#cadastro_cpf').val().trim(),
                                                    Senha: $('#cadastro_senha').val().trim(),
                                                    DataNascimento: $('#cadastro_data_nascimento').val(),
                                                    Endereco: endereco,
                                                    Nome: $('#cadastro_nome').val().trim()
                                                };
                                                $.post("/home/InserirUsuario/", parametros)
                                                    .done(function (retorno) {
                                                        if (retorno.id != null && retorno.id > 0) {
                                                            $('#cadastroModal').modal('toggle');
                                                            updateTelaLogin(retorno);
                                                            alert('Usuário cadastrado com sucesso!');
                                                            cleanCampos();
                                                            if (confirmLocacao) criarLocacao();
                                                        } else alert('Usuário e/ou senha não encontrados');
                                                    })
                                                    .fail(function (error) {
                                                        alert('Ocorreu um erro, tente novamente mais tarde');
                                                    });

                                            } else alert('Senha não confirmada');
                                        } else alert('Senha deve conter mais que 4 caracteres');
                                    } else alert('Senha deve conter mais que 4 caracteres');
                                } else alert('Insira um estado válido');
                            } else alert('Insira uma cidade válida');
                        } else alert('Insira uma número válido');
                    } else alert('Insira uma rua válido');
                } else alert('Insira um cep válido');
            } else  alert('Insira um nascimento válido');
        } else alert('Insira um cpf válido');
    } else alert('Insira um nome válido');
});

//Função de limpar campos formulario cadastro cliente
function cleanCampos() {
    $('#cadastro_nome').val('');
    $('#cadastro_cpf').val('');
    $('#cadastro_data_nascimento').val('');
    $('#cadastro_cep').val('');
    $('#cadastro_rua').val('');
    $('#cadastro_numero').val('');
    $('#cadastro_cidade').val('');
    $('#cadastro_estado').val('');
    $('#cadastro_senha').val('');
    $('#cadastro_confirmar_senha').val('');
}

//Mascara cpf
function mCPF(cpf) {
    cpf = cpf.replace(/\D/g, "")
    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2")
    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2")
    cpf = cpf.replace(/(\d{3})(\d{1,2})$/, "$1-$2")
    return cpf
}

function fMasc(objeto, mascara) {
    obj = objeto
    masc = mascara
    setTimeout("fMascEx()", 1)
}

function fMascEx() {
    obj.value = masc(obj.value)
}