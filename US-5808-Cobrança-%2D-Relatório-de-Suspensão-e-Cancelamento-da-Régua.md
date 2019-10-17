#5808

# 1.	DESCRIÇÃO DA USER STORY

|**COMO** administrador **QUERO** gerar um relatório contendo os dados de suspensão e cancelamento da Régua de Cobrança **PARA** análise da área de Finanças.  |
|--|

# 2.	ESCOPO DA HISTÓRIA

_Solicitação realizada devido à análise do Power Curve._

# 3.	PRÉ CONDIÇÕES

* A plataforma deve possuir cliente(s) inadimplente(s).<br>

# 4.	COMO ACESSAR
**Visualizar relatório de Suspensão:**
1.	Acessar a Vivo Plataforma Digital como Administrador;
1.	Clicar em _"Administração"_;
1.	Clicar em _"Régua de Cobrança"_;
1.	Clicar em _"Relatórios de Cobrança"_;
1.	Selecionar _"Relatório de Suspensão e Cancelamento"_;
1.	Especificar intervalo de datas;
1.	Clicar no botão _"Executar"_; 
<br><br>

# 5.	CRITÉRIOS DE ACEITE

## Cenário 1:  Gerar Relatório de Suspensão e Cancelamento da Régua de Cobrança 

**DADO** que o administrador selecionou o _"Relatório de Suspensão e Cancelamento da Régua de Cobrança"_ na tela _"Relatórios de Cobrança"_<br>
**E** clicou no botão _"Executar"_ após especificar o intervalo de datas<br> 
**QUANDO** a plataforma gerar o Relatório de Suspensão e Cancelamento da Régua de Cobrança <br>
**ENTÃO** a plataforma exibirá uma pop-up contendo os dados conforme o Anexo 8.1<br>

### **Informações Adicionais** ###
- A plataforma deverá exibir o relatório contendo os dados conforme o anexo 8.1.<br>
- A plataforma deverá permitir que o usuário realize o download do relatório.<br>

----
# **6.	FLUXO DE NEGÓCIO**

### 6.1. Fluxo da Régua de Cobrança

_Não se aplica_<br>

# **7.	PROTÓTIPOS**

_Não se aplica_<br>

# **8.	ANEXOS**

### 8.1. Relatório de Suspensão e Cancelamento da Régua de Cobrança
<table>
<tr><td><b>Coluna</b></td><td><b>Campo</b></td><td><b>Descrição</b></td><td><b>Formato</b></td><td><b>Exemplo</b></td></tr>
<tr><td> Nome </td><td> FirstName + LastName </td><td> Nesta coluna será apresentada o nome do usuário </td><td> Alfanumérico </td><td> Luiz Felipe Marcondes</td></tr>
<tr><td> Código do cliente </td><td> CustomerCode </td><td> Nesta coluna será apresentada o código do cliente </td><td> Alfanumérico </td><td> PENDENTE </td></tr>
<tr><td> Código da Loja </td><td> StoreCode </td><td> Nesta coluna será apresentada o código da loja </td><td> Alfanumérico </td><td> 4.012.179 </td></tr>
<tr><td> Status do cliente </td><td> StatusCustomer </td><td> Nesta coluna será apresentada o status do cliente </td><td> Alfanumérico </td><td> Ativo ou Suspenso</td></tr>
<tr><td> Segmento </td><td> Segment </td><td> Nesta coluna será apresentada o segmento do cliente </td><td> Alfanumérico </td><td> Massivo </td></tr>
<tr><td> CPF </td><td> CPF </td><td> Nesta coluna será apresentada o CPF do cliente </td><td> Alfanumérico </td><td> 44158605245 </td></tr>
<tr><td> CNPJ </td><td> CNPJ </td><td> Nesta coluna será apresentada o CNPJ do cliente </td><td> Alfanumérico </td><td> 81476775000130 </td></tr>
<tr><td> Número de Faturamento </td><td> CustomerBillingPhone </td><td> Nesta coluna será apresentada o número de faturamento do cliente </td><td> Numérico </td><td> 11 911223344 </td></tr>
<tr><td> E-mail do cliente </td><td> CustomerEmail </td><td> Nesta coluna será apresentada o e-mail do cliente </td><td> Alfanumérico </td><td> Luiz.santos4@globalweb.com.br </td></tr>
<tr><td> Fatura </td><td> Invoice </td><td> Nesta coluna será apresentada o número da fatura </td><td> Alfanumérico </td><td> VVL-1-00007853 </td></tr>
<tr><td> Status do Pagamento </td><td> PaymentStatus </td><td> Nesta coluna será apresentada o status do pagamento </td><td> Alfanumérico </td><td> Vencido </td></tr>
<tr><td> Método de Pagamento </td><td> PaymentMethod </td><td> Nesta coluna será apresentada o método do pagamento </td><td> Alfanumérico </td><td> Cartão de Crédito </td></tr>
<tr><td> Ciclo de Faturamento </td><td> CycleReference </td><td> Nesta coluna será apresentada o mês/ano do ciclo da fatura </td><td> MM/AAAA </td><td> 01/2019 </td></tr>
<tr><td> Total da Fatura </td><td> TotalRetailPrice </td><td> Nesta coluna será apresentada o valor total da fatura </td><td> Nesta coluna será apresentada o valor total da fatura
Numérico </td><td> R$ 1.500,00 </td></tr>
<tr><td> Vencimento da Fatura </td><td> DueDate </td><td> Nesta coluna será apresentada a data de vencimento da fatura </td><td> DD/MM/AAAA </td><td> 05/01/2019 </td></tr>
<tr><td> Status da Régua </td><td> PENDENTE </td><td> Nesta coluna exibirá o status da régua de cobrança </td><td> Alfanumérico </td><td> PENDENTE </td></tr>
<tr><td> Início da Régua </td><td> PENDENTE </td><td> Nesta coluna exibirá a data de início da régua de cobrança </td><td> DD/MM/AAAA </td><td> 11/10/2019 </td></tr>
<tr><td> Data Aging </td><td> PENDENTE </td><td> Nesta coluna exibirá a data em que a fatura entrou no último aging da régua de cobrança </td><td> DD/MM/AAAA </td><td> 11/10/2019 </td></tr>
<tr><td> Aging </td><td> PENDENTE </td><td> Nesta coluna exibirá o aging da última ação da régua de cobrança </td><td> Alfanumérico </td><td> 006 </td></tr>
<tr><td> Filtro </td><td> PENDENTE </td><td> Nesta coluna exibirá se o cliente está dentro do filtro da régua de cobrança </td><td> Alfanumério </td><td> Sim ou Não </td></tr>
<tr><td> Flag Suspensão </td><td> SUSP </td><td> Nesta coluna exibirá se a régua de cobrança está suspensa para o cliente</td><td> Alfanumério </td><td> Sim ou Não </td></tr>
<tr><td> Flag Cancelamento </td><td> CAN </td><td> Nesta coluna exibirá se a régua de cobrança está cancelada para o cliente</td><td> Alfanumério </td><td> Sim ou Não </td></tr>
<tr><td> Flag Crédito </td><td> PENDENTE </td><td> Nesta coluna exibirá se a régua de cobrança está cancelada para o cliente</td><td> Alfanumério </td><td> Sim ou Não </td></tr>
</table>