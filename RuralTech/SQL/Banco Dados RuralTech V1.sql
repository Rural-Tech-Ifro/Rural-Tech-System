create database RuralTech;
use RuralTech;

create table usuario(
id_usu int primary key auto_increment,
nome_usu varchar(200),
email_usu varchar(200),
senha_usu varchar(10)
);

create table propriedade(
id_pro int primary key auto_increment,
nome_pro varchar(200),
proprietario_pro varchar(200),
logradouro_pro varchar(200),
cep_pro varchar(200),
bairro_pro varchar(200),
complemento_pro varchar(200),
tamanho_pro int,
imagem_pro blob,
id_usu_fk int,
foreign key(id_usu_fk) references usuario(id_usu)
);

create table patrimonio(
id_pat int primary key auto_increment,
nome_pat varchar(200),
valor_pat double,
tipo_pat varchar(100),
imagrem_pat blob,
descricao_pat varchar(500),
id_pro_fk int,
foreign key(id_pro_fk) references propriedade(id_pro)
);

create table equipamento(
id_equi int primary key auto_increment,
valor_equi double,
descricao_equi varchar(200),
tipo_equi varchar(200),
nome_equi varchar(200),
id_pro_fk int,
foreign key(id_pro_fk) references propriedade(id_pro)
);

create table pasto(
id_pas int primary key auto_increment,
limete_pas varchar(100),
descricao_pas varchar(500),
tipo_pas varchar(200),
tamanho_pas float,
imagem_pas blob,
id_pro_fk int,
foreign key(id_pro_fk) references propriedade(id_pro)
);

create table classificacao_animal(
id_cla_ani int primary key auto_increment,
fase_cla_ani varchar(300),
nomeGrupo_cla_ani varchar(300),
sexo_cla_ani varchar(300),
descricao_cla_ani varchar(300),
producaoMaxima_cla_ani varchar(300),
producaoMinima_cla_ani varchar(300),
delimitacao_cla_ani varchar(300)
);

create table paricao(
id_par int primary key auto_increment,
dataParto_par date,
sexo_par varchar(100),
tipo_par varchar(100),
lote_par varchar(100),
detalhamento_par varchar(500),
situacao_par varchar(300)
);

create table peso_animal(
id_peso_ani int primary key auto_increment,
peso_peso_ani double,
observacao_peso_ani varchar(300),
gmd_peso_ani varchar(100),
altura_peso_ani varchar(300),
diferenca_peso_ani varchar(300)
);

create table fornecedor(
id_for int primary key auto_increment,
nome_for varchar(200),
celular_for varchar(50),
telefone_for varchar(50),
cnpjCpf_for varchar(50),
pais_for varchar(200),
estado_for varchar(200),
cidade_for varchar(200),
cep_for varchar(100),
numero_for varchar(100),
logradouro_for varchar(100),
tipo_for varchar(100),
email_for varchar(300)
);
INSERT INTO fornecedor (
    nome_for, celular_for, telefone_for, cnpjCpf_for, pais_for, estado_for, cidade_for, 
    cep_for, numero_for, logradouro_for, tipo_for, email_for
) 
VALUES
('Distribuidora São Paulo', '(11) 91234-5678', '(11) 3456-7890', '12.345.678/0001-90', 'Brasil', 'São Paulo', 'São Paulo', 
'01010-100', '100', 'Rua das Indústrias', 'Atacado', 'contato@distribuidorasampa.com.br'),

('Loja Rio Comercial', '(21) 92345-6789', '(21) 2233-4455', '23.456.789/0001-10', 'Brasil', 'Rio de Janeiro', 'Rio de Janeiro', 
'20030-150', '202', 'Avenida Central', 'Varejo', 'suporte@lojario.com.br');

create table funcionario(
id_fun int primary key auto_increment,
nome_fun varchar(200),
email_fun varchar(200),
telefone_fun varchar(200),
numero_fun varchar(200),
salario_fun double,
celular_fun varchar(200),
logradouro_fun varchar(200),
pais_fun varchar(200),
estado_fun varchar(200),
cidade_fun varchar(200),
cep_fun varchar(50),
cpf_fun varchar(50),
dataNascimento_fun date,
dataPagamento_fun date,
dataAdmissao_fun date
);

INSERT INTO funcionario (
    nome_fun, email_fun, telefone_fun, numero_fun, salario_fun, celular_fun, logradouro_fun, 
    pais_fun, estado_fun, cidade_fun, cep_fun, cpf_fun, dataNascimento_fun, dataPagamento_fun, dataAdmissao_fun
) 
VALUES
('João Silva', 'joao.silva@email.com', '(11) 2345-6789', '123', 2500.00, '(11) 98765-4321', 'Rua das Flores, 123', 
'Brasil', 'São Paulo', 'São Paulo', '01000-000', '123.456.789-00', '1985-03-15', '2024-11-20', '2020-08-01'),

('Maria Oliveira', 'maria.oliveira@email.com', '(21) 98765-4321', '456', 3000.00, '(21) 99123-4567', 'Avenida Brasil, 456', 
'Brasil', 'Rio de Janeiro', 'Rio de Janeiro', '20000-000', '987.654.321-00', '1990-07-22', '2024-11-25', '2021-02-10');


create table compra(
id_com int primary key auto_increment,
dataPagamento_com date,
codigo_com varchar(100),
formaPagamento_com varchar(100),
dataCompra_com date,
quantidadeParcela int,
id_fun_fk int,
foreign key(id_fun_fk) references funcionario(id_fun)
);
select * from Compra;
create table fornecedor_compra(
id_for_com int primary key auto_increment,
id_for_fk int,
foreign key(id_for_fk) references fornecedor(id_for),
id_com_fk int,
foreign key(id_com_fk) references compra(id_com)
);
select * from Fornecedor_Compra;

create table despesa(
id_des int primary key auto_increment,
numeroDocumento_des varchar(100),
dataVencimento_des date,
valorTotal_des double,
centroCusto varchar(100),
jurosParcelas varchar(100),
paga_des varchar(100),
repetir_des varchar(100),
formaPagamento_des varchar(100),
dataEmissao_des date,
categoria_des varchar(100),
desconto_des varchar(100),
observacao_des varchar(500),
descricao_des varchar(500),
dataPagamento_des varchar(100),
lancamentoParcelado_des boolean,
conta_des varchar(100),
periodo_des varchar(100)
);

create table fornecedor_despesa(
id_for_des int primary key auto_increment,
id_for_fk int,
foreign key(id_for_fk) references fornecedor(id_for),
id_des_fk int,
foreign key(id_des_fk) references despesa(id_des)
);

create table estoque(
id_est int primary key auto_increment,
categoria_est varchar(200),
dataVencimento_est date,
preco_est double,
subTotal_est double,
centroCusto_est varchar(200),
valorTotal_est double,
quantidade_est int,
dataPagamento_est date,
pagamentoParcelado_est boolean,
paga_est boolean,
unidadeMedida_est varchar(200),
valorFrete_est double,
formaPagamento_est varchar(200),
dataEntrada_est date,
modalidadeFrete_est varchar(200),
repetir_est varchar(200),
numeroDocumento_est varchar(200),
observacao_est varchar(200),
dataEmisao_est varchar(200),
jurosParcela_est varchar(200),
desconto_est double,
id_pro_fk int,
foreign key(id_pro_fk) references propriedade(id_pro)
);

create table fornecedor_estoque(
id_for_est int primary key auto_increment,
id_for_fk int,
foreign key(id_for_fk) references fornecedor(id_for),
id_est_fk int,
foreign key(id_est_fk) references estoque(id_est)
);

create table produto(
id_prod int primary key auto_increment,
precoCusto_prod double,
unidadeSaida_prod varchar(200),
unidadeEntrada_prod varchar(200),
precoVenda_prod double,
quantidade_prod int,
observacao_prod varchar(200),
nome_prod varchar(200),
dataVencimento_prod date
);
INSERT INTO produto (precoCusto_prod, unidadeSaida_prod, unidadeEntrada_prod, precoVenda_prod, quantidade_prod, observacao_prod, nome_prod, dataVencimento_prod) VALUES (10.50, 'unidade', 'caixa', 15.75, 50, 'Produto perecível', 'Arroz Integral', '2024-12-31');
INSERT INTO produto (precoCusto_prod, unidadeSaida_prod, unidadeEntrada_prod, precoVenda_prod, quantidade_prod, observacao_prod, nome_prod, dataVencimento_prod) VALUES (7.25, 'unidade', 'caixa', 12.00, 100, 'Produto não perecível', 'Feijão Preto', '2025-06-30');


create table venda(
id_ven int primary key auto_increment,
formaRecebimento varchar(100),
dataVenda_ven date,
preco_ven double,
valorTotal_ven double,
valorDesconto double
);

create table produto_venda(
id_prod_ven int primary key auto_increment,
id_prod_fk int,
foreign key(id_prod_fk) references produto(id_prod),
id_ven_fk int,
foreign key(id_ven_fk) references venda(id_ven)
);

create table produto_compra(
id_prod_com int primary key auto_increment,
id_prod_fk int,
foreign key(id_prod_fk) references produto(id_prod),
id_com_fk int,
foreign key(id_com_fk) references compra(id_com)
);

create table transporte(
id_tra int primary key auto_increment,
cpf_tra varchar(50),
cnpj_tra varchar(50),
nome_tra varchar(200),
inscricaoEstadual_tra varchar(200),
estado_tra varchar(200),
cidade_tra varchar(200),
bairro_tra varchar(200),
rua_tra varchar(200),
cep_tra varchar(50),
id_est_fk int,
foreign key(id_est_fk) references estoque(id_est)
);
select * from produto_compra;

create table animal(
id_ani int primary key auto_increment,
brinco_ani varchar(100),
sexo_ani varchar(100),
raca_ani varchar(100),
classificacao_ani varchar(500),
origem_ani varchar(100)
);

create table ordenha(
id_ord int primary key auto_increment,
totalLitros_ord varchar(100),
id_ani_fk int,
foreign key(id_ani_fk) references animal(id_ani)
);

create table apartacao(
id_apa int primary key auto_increment,
lote_apa varchar(100),
observacao_apa varchar(500),
situacao_apa varchar(500),
dataTransferencia_apa date,
id_ani_fk int,
foreign key(id_ani_fk) references animal(id_ani)
);

create table descarte_animal(
id_des_ani int primary key auto_increment,
motivo_des_ani varchar(600),
causa_des_ani varchar(500),
dataDescarte_des_ani date,
id_ani_fk int,
foreign key(id_ani_fk) references animal(id_ani)
);

create table exame(
id_exa int primary key auto_increment,
tipo_exa varchar(300),
realizado_exa boolean,
resultado_exa varchar(1000),
data_exa date,
id_ani_fk int,
foreign key(id_ani_fk) references animal(id_ani)
);

create table inseminacao(
id_ins int primary key auto_increment,
tipo_ins varchar(200),
observacao_ins varchar(500),
data_ins date,
id_ani_fk int,
foreign key(id_ani_fk) references animal(id_ani),
id_fun_fk int,
foreign key(id_fun_fk) references funcionario(id_fun)
);

create table vacina(
id_vac int primary key auto_increment,
nome_vac varchar(300),
diasCarencia_vac int,
estado_vac varchar(300),
inscricaoEstadual_vac varchar(300),
quantidade_vac int,
unidadeEntrada_vac varchar(400),
unidadeSaida_vac varchar(400),
observacao_vac varchar(500)
);

create table medicamento(
id_med int primary key auto_increment,
nome_med varchar(300),
diasCarencia_med int,
estado_med varchar(300),
inscricaoEstadual_med varchar(300),
quantidade_med int,
unidadeEntrada_med varchar(400),
unidadeSaida_med varchar(400),
observacao_med varchar(500)
);

create table aplicacao_medicamento(
id_apli_med int primary key auto_increment,
dataAplicacao_apli_med date,
observacao_apli_med varchar(400),
aplicada_apli_med boolean,
dosagem_apli_med varchar(100),
aplicador_apli_med varchar(200),
id_ani_fk int,
foreign key(id_ani_fk) references animal(id_ani)
);

create table tarefa(
id_tar int primary key auto_increment,
nome_tar varchar(300),
prioridade_tar varchar(100),
descricao_tar varchar(500)
);

create table recebimento(
id_rec int primary key auto_increment,
numeroDocumento_rec int,
centroCusto_rec varchar(100),
dataEmissao_rec date,
desconto_rec double,
dataPagamento_rec date,
formaPagamento_rec varchar(100),
conta_rec varchar(100),
observacao_rec varchar(1000),
paga_rec boolean,
pagamentoParcelado_rec boolean,
jurosParcela_rec double,
categoria_rec varchar(200),
valorTotal_rec double,
dataVencimento_rec date,
descricao_rec varchar(200),
id_for_fk int,
foreign key(id_for_fk) references fornecedor(id_for)
);